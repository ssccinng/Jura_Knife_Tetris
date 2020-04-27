using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{

    class Juraknifecore
    {
        public int engine;


        public simpboard Board = new board(null, null, 0).tosimple();
        public mino_gene minorule;

        List<tree> nodequeue = new List<tree>();

        public tree boardtree;
        //public Queue<int> nextquenesour = new Queue<int>();
        //public Queue<int> nextquene = new Queue<int>();
        public List<int> nextquene = new List<int>();
        public int hold;

        public bool isdead
        {
            get
            {
                return Board.isdead;
            }
        }
        JK_Movec movereslut;

        public tree requset_next_move()
        {
            // movereslut
            //if (boardtree.treenode.Count == 0)
            //    return false;
            boardtree.treenode.Sort((a, b) =>
            {
                var o = b.score - a.score;
                return o;
            });

            int aa = nodecnt(boardtree);

            for (int i = 1; i < boardtree.treenode.Count; ++i)
            {
                freenode(boardtree.treenode[i]);
            }

            boardtree = boardtree.treenode[0]; // 节点不存在的问题
            boardtree.father = null;
            aa = nodecnt(boardtree);
            System.GC.Collect();
            eval.evalfield(boardtree);
            // 重置nodequeue
            return boardtree;

        }

        public JK_Movec poll_next_move()
        {
            return movereslut;
        }

        public tree getbestnode()
        {

            // 计算路径
            return boardtree.treenode[boardtree.bestnodeindex];
        }


        public void reset_stat(board a)
        {
            Board = a.tosimple();
        }

        public void reset_stat(bool[,] field, bool b2b, int combo)
        {
            Board.field = field;
            Board.isb2b = b2b;
            Board.combo = combo;
            boardtree = new tree();
        }

        public int nodecnt(tree node)
        {
            int cnt = 0;
            foreach (tree chird in node.treenode)
            {
                cnt += nodecnt(chird) + 1;

            }
            return cnt;
        }

        public void freenode(tree node)
        {
            foreach (tree chird in node.treenode)
            {
                freenode(chird);
            }
            node.useless = true;
            node = null;
        }

        public void add_next(int a)
        {
            nextquene.Add(a);

        }

        public void start_bot()
        {

        }

        public void init()
        {
            boardtree = new tree();
            board F = new board(new mino_gene(), new TopGarbage(), 5);
            boardtree.Board = Board.clone();
            //for (int i = 0; i < 10; ++i) F.add_garbage(1);
            //boardtree.Board = F.tosimple();
            //boardtree.ad
            nodequeue.Add(boardtree);

        }
        public void extend_node() // 前两层可能可以放宽要求
        {
            while (nextquene.Count > nodequeue[0].pieceidx) // 能够保持combo的要继续计算
            {

                List<tree> nextpiece = new List<tree>();
                nodequeue.Sort((a, b) =>
                {
                    var o = b.score - a.score;
                    return o;
                }
                    );
                int limit = 5;
                limit = Math.Min(nodequeue.Count, 6);

                for (int j = 0, cnt = 0; cnt < Math.Max(nodequeue.Count / 20 + 1, limit) && j < nodequeue.Count; ++j) // 剪枝思考
                {
                    tree node = nodequeue[j];
                    if (node == null || node.useless)
                    {
                        nodequeue[j] = null;
                        continue;

                    }; // 等下打上无用标记

                    // 好节点后可跟2层无用节点

                    cnt++;
                    if (!node.isextend) node.findalladd(this);
                    node.isextend = true;
                    node.treenode.Sort((a, b) =>
                    {
                        var o = b.score - a.score;
                        return o;
                    });

                    for (int i = 0; i < node.treenode.Count; ++i) // 剪枝有待商议
                    {
                        nextpiece.Add(node.treenode[i]);
                    }
                }
                nodequeue = nextpiece;
                //if (nextquene.Count != 0) // 之后改成广搜
                //{
                //    nextquene.Dequeue();
                //    for (int i = 0; i < node.treenode.Count / 10; ++i)
                //    {
                //        extend_node(node.treenode[i]);
                //    }
                //}
            }

            //foreach (tree Chird in node.treenode)
            //{

            //    //evalresult nodeval = eval.evalnode(node); // sort value
            //    if (Chird.score)
            //    {
            //        //nodeval 
            //        Chird.findalladd();
            //        // 更新父节点分数
            //    }
            //}
        }
        //public void extend_node(tree node)
        //{


        //    if (!node.isextend) node.findalladd(nextquene.Peek());
        //    node.treenode.Sort((a, b) =>
        //    {
        //        var o = a.score - b.score;
        //        return o;
        //    });
        //    if (nextquene.Count != 0) // 之后改成广搜
        //    {
        //        nextquene.Dequeue();
        //        for (int i = 0; i < node.treenode.Count / 10; ++i)
        //        {
        //            extend_node(node.treenode[i]);
        //        }
        //    }

        //foreach (tree Chird in node.treenode)
        //{

        //    //evalresult nodeval = eval.evalnode(node); // sort value
        //    if (Chird.score)
        //    {
        //        //nodeval 
        //        Chird.findalladd();
        //        // 更新父节点分数
        //    }
        //}
    }

}



