using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{
    class tree
    {
        public int score = 0;
        public List<tree> treenode = new List<tree>();
        public bool isextend = false;
        public int nowpiece = -1;
        public int holdpiece = -1;
        public int garbage = -1;
        public int garbageadd;

        //public bool clearing = false;

        public int attack = 0;
        public mino finmino = null;
        public simpboard Board; // maybe simple
        public int Tspinslot = 0;
        public bool holdT {
            get
            {
                return holdpiece == 2;
            }
        }
        public bool holdI
        {
            get
            {
                return holdpiece == 0;
            }
        }
        public bool ishold = false;
        public int bestnodeindex;
        public int maxdef; // 最大缩减高度+垃圾行值


        public bool findnextsol()
        {
            return false; // pass
        }

        public tree clone()
        {
            tree cp = new tree();
            cp.Board = Board.clone();
            cp.garbage = garbage; // 可能有抵消
            cp.attack = attack;
            cp.holdpiece = holdpiece;
            // attack 可能继承
            return cp;
        }


        public void findalladd(int nowpiece)
        {
            this.nowpiece = nowpiece;
            isextend = true;
            List<mino> allpos = seacher.findallplace(Board);
            foreach (mino m in allpos)
            {
                tree chird = clone();
                chird.finmino = m;
                treenode.Add(chird);
            }

            if (holdpiece != -1)
            {
                tree chird = clone();
                chird.holdpiece = nowpiece;
                chird.ishold = true;
                treenode.Add(chird);
            }
            else
            {
                int temp = nowpiece;
                nowpiece = holdpiece;
                List<mino> allpos1 = seacher.findallplace(Board);

                foreach (mino m in allpos)
                {
                    tree chird = clone();
                    chird.finmino = m;
                    chird.ishold = true;
                    chird.holdpiece = temp; // oops
                    treenode.Add(chird);
                }
            }


        }



    }
}
