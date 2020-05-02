using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{
    public static class search_tspin
    {
        static public List<mino> findalltslot(simpboard Board)
        {
            List<mino> allpos = new List<mino>(); // 返回所有t的状态 
            bool[,,,] visit = new bool[42, 12, 4, 2];
            bool[,,,] visit1 = new bool[42, 12, 4, 2];
            Queue<mino_stat> minoque = new Queue<mino_stat>();
            minoque.Enqueue(new mino_stat(Board.piece.minopos, Board.piece.stat));
            visit[Board.piece.minopos.x + 2, Board.piece.minopos.y + 2, Board.piece.stat, 0] = true;
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
                    if (!visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat, 1])
                    {
                        visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat, 1] = true;
                        visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat, 0] = true;
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 2, true));
                    }
                }

                temp.setpos(node.minoopos);
                temp.setstat(node.stat);
                if (temp.right_rotation(ref Board) != -1)
                {
                    if (!visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat, 1])
                    {
                        visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat, 1] = true;
                        visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat, 0] = true;
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 3, true));
                    }
                }

                temp.setpos(node.minoopos);
                temp.setstat(node.stat);

                if (temp.left_move(ref Board))
                {
                    if (!visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat, 0])
                    {
                        visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat, 0] = true;
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 0));
                    }
                }
                temp.setpos(node.minoopos);
                temp.setstat(node.stat);
                if (temp.right_move(ref Board))
                {
                    if (!visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat, 0])
                    {
                        visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat, 0] = true;
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 1));
                    }
                }

                temp.setpos(node.minoopos);
                temp.setstat(node.stat);

                int softdis = temp.soft_drop_floor(ref Board);
                //{
                if (!visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat, 0])
                {
                    visit[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat, 0] = true;
                    //if (softdis == 0)
                    //    minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 4));
                    //else
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 4));
                }
                //}
                if (!visit1[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat, 0] && node.spinlast)  // istspin spin状态多一维 先只搜spin
                {

                    visit1[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat, 0] = true;
                    if (node.spinlast && softdis == 0) { 
                        visit1[temp.minopos.x + 2, temp.minopos.y + 2, temp.stat, 1] = true;

                        mino fi = temp.clone();
                        fi.path = node;
                        fi.spinlast = node.spinlast;
                        allpos.Add(fi);
                    }
                }



                //source.setpos
            }

            return allpos;
        }
    }
}
