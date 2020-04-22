using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{
    static class seacher
    {
        static public List<pos> findallplace(simpboard Board)
        {
            bool[,,] visit = new bool[40, 10, 4];
            Queue<mino_stat> minoque = new Queue<mino_stat>();
            minoque.Enqueue(new mino_stat(source.minopos, source.stat));
            mino_gene minogen = new mino_gene();
            mino temp = minogen.getmino(source.name);
            while (minoque.Count != 0)
            {
                mino_stat node = minoque.Dequeue();

                visit[node.minoopos.x, node.minoopos.y, node.stat] = true;
                if (node.minoopos.x == target.minopos.x &&
                    node.minoopos.y == target.minopos.y &&
                    node.stat == target.stat)
                {
                    return true;
                }

                temp.setpos(node.minoopos);
                temp.setstat(node.stat);
                if (temp.left_rotation(ref field) != -1)
                {
                    if (!visit[temp.minopos.x, temp.minopos.y, temp.stat])
                    {
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 2));
                    }
                }

                temp.setpos(node.minoopos);
                temp.setstat(node.stat);
                if (temp.right_rotation(ref field) != -1)
                {
                    if (!visit[temp.minopos.x, temp.minopos.y, temp.stat])
                    {
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 3));
                    }
                }

                temp.setpos(node.minoopos);
                temp.setstat(node.stat);

                if (temp.left_move(ref field))
                {
                    if (!visit[temp.minopos.x, temp.minopos.y, temp.stat])
                    {
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 0));
                    }
                }
                temp.setpos(node.minoopos);
                temp.setstat(node.stat);
                if (temp.right_move(ref field))
                {
                    if (!visit[temp.minopos.x, temp.minopos.y, temp.stat])
                    {
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 1));
                    }
                }

                temp.setpos(node.minoopos);
                temp.setstat(node.stat);
                if (temp.soft_drop_floor(ref field) != 0)
                {
                    if (!visit[temp.minopos.x, temp.minopos.y, temp.stat])
                    {
                        minoque.Enqueue(new mino_stat(temp.minopos, temp.stat, node.idx, node.path, 4));
                    }
                }


                //source.setpos
            }
        }

    }
}
