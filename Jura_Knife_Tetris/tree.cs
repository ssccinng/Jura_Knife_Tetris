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
        public int garbage = 0;
        public int garbageadd = 0;

        // 重判dead

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
        public int lock_piece_calc(ref simpboard Board)
        {
            rule gamerule = defaultop.defrule;
            Board.piece.mino_lock(ref Board);

            bool isb2b = Board.isb2b;

            int row = Board.clear_full();
            int atk = 0;
            if (Board.isperfectclear) atk += 6;
            if (Board.piece.Tspin)
            {
                atk += gamerule.GetTspindmg(row);

            }
            else
            {
                atk += gamerule.Getcleardmg(row);
            }
            atk += gamerule.Getrendmg(Board.combo);

            if (isb2b)
                atk += gamerule.Getb2bdmg(row);
            if (Board.piece.mini && row == 1) atk -= 1;
            // attack calu
            //if (Board.piece.isTspin())
            //Board.cl
            //int clear

            return atk;
        }


        public void findalladd(int nowpiece)
        {
            this.nowpiece = nowpiece;
            isextend = true;
            Board.piece = defaultop.demino.getmino(nowpiece);
            Board.piece.setpos(19, 3);
            List<mino> allpos = seacher.findallplace(Board);
            foreach (mino m in allpos)
            {
                tree chird = clone();
                chird.Board.piece = m;
                lock_piece_calc(ref chird.Board);
                chird.finmino = m;
                treenode.Add(chird);
            }

            if (holdpiece == -1)
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
                Board.piece = defaultop.demino.getmino(nowpiece);
                Board.piece.setpos(19, 3);
                List<mino> allpos1 = seacher.findallplace(Board);

                foreach (mino m in allpos1)
                {
                    tree chird = clone();
                    chird.Board.piece = m;
                    lock_piece_calc(ref chird.Board);
                    chird.finmino = m;
                    chird.ishold = true;
                    chird.holdpiece = temp; // oops
                    treenode.Add(chird);
                }
            }


        }


        public bool checkdead()
        {
            return false; // pass 
        }
    }
}
