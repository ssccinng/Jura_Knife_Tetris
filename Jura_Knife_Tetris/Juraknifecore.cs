using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{

    class Juraknifecore
    {
        public int engine;


        public simpboard Board;
        public mino_gene minorule;


        tree boardtree;
        public Queue<int> nextquene;

        public int hold;

        public bool isdead
        {
            get
            {
                return Board.isdead;
            }
        }
        JK_Movec movereslut;

        public bool requset_next_move()
        {
            // movereslut
            if (boardtree.treenode.Count == 0)
                return false;
            boardtree = getbestnode();

            return true;

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


        public void add_next(int a)
        {
            nextquene.Enqueue(a);

        }

        public void start_bot()
        {

        }

        void init()
        {
            boardtree = new tree();


        }

        void extend_node(tree node)
        {

            if (!node.isextend) node.findalladd();

            foreach (tree Chird in node.treenode)
            {
                evalresult nodeval = eval.evalnode(node);
                if (nodeval.value)
                {
                    //nodeval 
                    Chird.findnextsol();
                    // 更新父节点分数
                }
            }
        }

    }


}
