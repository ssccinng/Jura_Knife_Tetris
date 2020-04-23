using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{
    static class seacher
    {
        static public List<pos> findallplace(simpboard Board)
        {
            List<mino> allpos = new List<mino>();
            bool[,,] visit = new bool[40, 10, 4];
            Queue<mino_stat> minoque = new Queue<mino_stat>();
            minoque.Enqueue(new mino_stat(Board.piece.minopos, Board.piece.stat));
            visit[Board.piece.minopos.x, Board.piece.minopos.y, Board.piece.stat] = true;
            mino_gene minogen = new mino_gene();
            mino temp = Board.piece.clone();
            while (minoque.Count != 0)
            {
                mino_stat node = minoque.Dequeue();

                //visit[node.minoopos.x, node.minoopos.y, node.stat] = true; // 硬降
                temp.setpos(node.minoopos);
                temp.setstat(node.stat);
                if (temp.left_rotation(ref Board) != -1)
                {
                    if (!visit[temp.minopos.x, temp.minopos.y, temp.stat])
                    {
                        visit[temp.minopos.x, temp.minopos.y, temp.stat] = true;
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 2));
                    }
                }

                temp.setpos(node.minoopos);
                temp.setstat(node.stat);
                if (temp.right_rotation(ref Board) != -1)
                {
                    if (!visit[temp.minopos.x, temp.minopos.y, temp.stat])
                    {
                        visit[temp.minopos.x, temp.minopos.y, temp.stat] = true;
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 3));
                    }
                }

                temp.setpos(node.minoopos);
                temp.setstat(node.stat);

                if (temp.left_move(ref Board))
                {
                    if (!visit[temp.minopos.x, temp.minopos.y, temp.stat])
                    {
                        visit[temp.minopos.x, temp.minopos.y, temp.stat] = true;
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 0));
                    }
                }
                temp.setpos(node.minoopos);
                temp.setstat(node.stat);
                if (temp.right_move(ref Board))
                {
                    if (!visit[temp.minopos.x, temp.minopos.y, temp.stat])
                    {
                        visit[temp.minopos.x, temp.minopos.y, temp.stat] = true;
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 1));
                    }
                }

                temp.setpos(node.minoopos);
                temp.setstat(node.stat);

                temp.soft_drop_floor(ref field);
                //{
                    if (!visit[temp.minopos.x, temp.minopos.y, temp.stat])
                    {
                        visit[temp.minopos.x, temp.minopos.y, temp.stat] = true;
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 4));
                    }
                //}

                allpos.Add(temp.clone());



                //source.setpos
            }
        }

    }
}
