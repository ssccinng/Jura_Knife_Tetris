using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{
    static class search_tspin
    {
        static public List<mino> findalltslot(simpboard Board)
        {
            List<mino> allpos = new List<mino>();
            bool[,,] visit = new bool[42, 12, 4];
            bool[,,] visit1 = new bool[42, 12, 4];
            Queue<mino_stat> minoque = new Queue<mino_stat>();
            minoque.Enqueue(new mino_stat(Board.piece.minopos, Board.piece.stat));
            visit[Board.piece.minopos.x + 2, Board.piece.minopos.y + 2, Board.piece.stat] = true;
            mino_gene minogen = new mino_gene();
            mino temp = defaultop.demino.getmino(2);
            temp.setpos(19, 3);
            while (minoque.Count != 0)
            {
                mino_stat node = minoque.Dequeue();

                //visit[node.minoopos.x, node.minoopos.y, node.stat] = true; // 硬降
                temp.setpos(node.minoopos);
                temp.setstat(node.stat);
                if (temp.left_rotation(ref Board) != -1)
                {
                    if (!visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat])
                    {
                        visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat] = true;
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 2));
                    }
                }

                temp.setpos(node.minoopos);
                temp.setstat(node.stat);
                if (temp.right_rotation(ref Board) != -1)
                {
                    if (!visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat])
                    {
                        visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat] = true;
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 3));
                    }
                }

                temp.setpos(node.minoopos);
                temp.setstat(node.stat);

                if (temp.left_move(ref Board))
                {
                    if (!visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat])
                    {
                        visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat] = true;
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 0));
                    }
                }
                temp.setpos(node.minoopos);
                temp.setstat(node.stat);
                if (temp.right_move(ref Board))
                {
                    if (!visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat])
                    {
                        visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat] = true;
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 1));
                    }
                }

                temp.setpos(node.minoopos);
                temp.setstat(node.stat);

                int softdis = temp.soft_drop_floor(ref Board);
                //{
                if (!visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat])
                {
                    visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat] = true;
                    minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 4));
                }
                //}
                if (!visit1[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat] && temp.spinlast)  // istspin spin状态多一维
                {
                    visit1[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat] = true;
                    allpos.Add(temp.clone());
                }



                //source.setpos
            }

            return allpos;
        }
    }
}
