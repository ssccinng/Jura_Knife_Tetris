using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{
    class tree
    {

        // 加入一个指向父节点的指针
        public int score = 0;
        // 如果能保持连击 分数就使用叶子节点最高的评价
        // 如果不行 需要根据场地高度判断适不适合

        public List<tree> treenode = new List<tree>();
        public bool isextend = false;
        public int nowpiece = -1;
        public int holdpiece = -1;
        public bool useless = false;

        // 重判dead

        public int garbage = 0;
        public int garbageadd = 0;
        public int clearrow = 0;
        public bool holdT
        {
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

        public int tspintype;

        public int tslotnum;

        public int maxren; 
        // 分为能立刻ren和后续才能ren
        // tspin后的场地分数
        // 是否有holdt 无holdt最多允许一个tslot存在

        public bool wastedT
        {
            get
            {
                return !Board.piece.Tspin && finmino.name == "T";
            }
        }
        public int attack = 0;
        public int maxattack = 0;
        public int maxdef = 0; // 最大缩减高度+垃圾行值
        public simpboard Board; // maybe simple
        // 内有b2b combo (b2bclear考虑

        //public bool clearing = false;


        public mino finmino = null;
        
        public int Tspinslot = 0;
        
        public bool ishold = false;
        public int bestnodeindex; // 估计不用
        


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
        public Tuple<int, int> lock_piece_calc(ref simpboard Board)
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

            return new Tuple<int, int>(atk, row);
        }

        //public int searchtslot() // 去找可能的t坑
        //{

        //}


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
                chird.score = eval.evalfield(chird);
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
                // 先对相对有用的节点更新 
                foreach (mino m in allpos1)
                {
                    tree chird = clone();
                    chird.Board.piece = m;
                    lock_piece_calc(ref chird.Board);
                    chird.finmino = m;
                    chird.score = eval.evalfield(chird);
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
