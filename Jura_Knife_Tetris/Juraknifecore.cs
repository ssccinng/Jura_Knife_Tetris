using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{

    public class Juraknifecore
    {
        public int engine;


        public simpboard Board = new board(null, null, 0).tosimple();
        public mino_gene minorule;
        public int calcdepth = 0;
        public List<tree> nodequeue = new List<tree>();

        public tree boardtree;
        //public Queue<int> nextquenesour = new Queue<int>();
        //public Queue<int> nextquene = new Queue<int>();
        //public List<int> nextquene = new List<int>();
        public int[] nextqueue = new int[35];
        public int nextcnt = 0;

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
                var o = (b.score - a.score);
                var q = b.maxdepth - a.maxdepth;
                if (q != 0) return q;
                return (o * 1);
            });

            int aa = nodedep(boardtree);

            for (int i = 1; i < boardtree.treenode.Count; ++i)
            {
                freenode(boardtree.treenode[i]);
            }
            aa = nodedep(boardtree);
            boardtree = boardtree.treenode[0]; // 节点不存在的问题
            boardtree.father = null;
            aa = nodedep(boardtree);
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
            Board.column_height = Board.updatecol();

            Board.isb2b = b2b;
            Board.combo = combo;
            boardtree = new tree();
            boardtree.Board = Board.clone();
        }
        //public void nodeadd(tree node)
        //{

        //    if (node.depth == calcdepth) nodequeue.Add(node);

        //    //if (node.treenode.Count == 0 && !node.useless && node.pieceidx < nextquene.Count && !node.isextend) nodequeue.Add(node);
        //    //cnt += node.treenode.Count;
        //    foreach (tree chird in node.treenode)
        //    {
        //        nodeadd(chird);

        //    }
        //}

        public void nodeadd(tree node)
        {

            if (node.inplan && node.pieceidx < nextcnt ) nodequeue.Add(node);

            //if (node.treenode.Count == 0 && !node.useless && node.pieceidx < nextquene.Count && !node.isextend) nodequeue.Add(node);
            //cnt += node.treenode.Count;
            foreach (tree chird in node.treenode)
            {
                nodeadd(chird);

            }
        }

        public int nodedep(tree node)
        {
            int cnt = 0;
            if (!node.useless)
            {
                cnt = node.depth;
                //cnt += node.treenode.Count;
                foreach (tree chird in node.treenode)
                {
                    cnt = Math.Max(nodedep(chird), cnt);

                }
            }
            return cnt;
        }



        public int nodecnt(tree node)
        {
            int cnt = 0;
            if (node.depth == calcdepth) cnt += 1;
            //cnt += node.treenode.Count;
            foreach (tree chird in node.treenode)
            {
                cnt += nodecnt(chird);

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
            node.inplan = false;
            node = null;
        }

        public void add_next(int a)
        {
            //nextquene.Add(a);
            nextqueue[nextcnt++ % 30] = a;

        }

        public void start_bot()
        {

        }

        public void init()
        {
            boardtree = new tree();
            board F = new board(new mino_gene(), new TopGarbage(), 5);
            boardtree.Board = Board.clone();
            //for (int i = 0; i < 15; ++i) F.add_garbage(1);
            //boardtree.Board = F.tosimple();
            //boardtree.ad
            //nodequeue.Add(boardtree);

        }
        public void extend_node() // 前两层可能可以放宽要求 // 考虑给tspin单独一层循环
        {
            //
            bool flag = true;
            //while (calcdepth < nextquene.Count - 1) // 能够保持combo的要继续计算 无hold 会少计算一片
            while (true) // 能够保持combo的要继续计算 无hold 会少计算一片
                {
                
                nodequeue = new List<tree>();
                nodeadd(boardtree);
                if (nodequeue.Count == 0) break; // 异步时改为continue;
                calcdepth += 1;
                flag = false;
                List<tree> nextpiece = new List<tree>();
                
                int limit = 5;
                
                int qq = nodecnt(boardtree); // 会浪费时间吗
                limit = Math.Min(nodequeue.Count, 6);
                nodequeue.Sort((a, b) =>
                {
                    var o = (b.score - a.score);
                    var q = b.maxdepth - a.maxdepth;
                    if (q != 0) return q;
                    return (int)(o * 1); ;
                }
                    );
                // 全重置 haishinodequeue
                for (int j = 0, cnt = 0;j <  nodequeue.Count; ++j) // 剪枝思考 深度和分数的符合思考
                {
                    // cnt < Math.Max(nodequeue.Count / 20 + 1, limit) && 
                    //if (cnt > 10)
                    //{
                    //    //nodequeue[j].useless = true;
                    //    continue;
                    //}

                    

                    if (nextcnt <= nodequeue[j].pieceidx || !nodequeue[j].inplan)
                    {
                        continue;
                    }
                    nodequeue[j].inplan = false;
                    if (cnt > 20)
                    {
                        //nodequeue[j].inplan = false;
                        continue;
                    }
                    flag = true;
                    tree node = nodequeue[j];
                    if (node == null || node.useless)
                    {
                        nodequeue[j] = null;
                        continue;

                    }; // 等下打上无用标记

                    // 好节点后可跟2层无用节点

                    cnt++;
                    if (!node.isextend) node.findalladd(this);
                    //if (node.isdead) return;
                    node.isextend = true;
                    node.treenode.Sort((a, b) =>
                    {
                        var o = (int)(b.score - a.score);
                        var q = b.maxdepth - a.maxdepth;
                        if (q != 0) return q;
                        return (int)(o * 1); ;
                    });

                }
            }

        }

    }

}



