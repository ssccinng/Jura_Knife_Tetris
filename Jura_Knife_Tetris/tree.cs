using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{
    public class tree//: IDisposable
    {

        public bool isdead = false;
        public int score = 0;
        // 如果能保持连击 分数就使用叶子节点最高的评价
        // 如果不行 需要根据场地高度判断适不适合

        public List<tree> treenode = new List<tree>();
        public bool isextend = false;
        public int nowpiece = -1;
        public int holdpiece = -1;
        public bool useless = false;
        public tree father; // 指向父亲节点
        // 重判dead
        public int keepcombobestchird;
        public int bestchird;
        public int garbage = 0;
        public int garbageadd = 0;
        public int clearrow = 0;
        public int pieceidx = 0;
        public int afttspinscore = 0;
        public evalresult res;

        public bool inplan = true;
        // 预测块会到来的顺序

        //是否要在子节点分数高时才能到这里

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
        public int maxdepth = 0;

        public mino finmino = null;
        public int depth = 0;
        public int Tspinslot = 0;
        
        public bool ishold = false;
        public int bestnodeindex; // 估计不用
        


        public void updatefather()
        {
            if (father != null)
            {
                father.score = Math.Max(father.score, score);
                father.maxdepth = Math.Max(father.maxdepth, maxdepth);
                father.updatefather();
            }
        }

        //public bool findnextsol()
        //{
        //    for (; ; ) 
        //    yield return false; // pass
        //}

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



        public void findalladd(Juraknifecore bot)
        {
            if (pieceidx >= bot.nextcnt)
                return;
            this.nowpiece = bot.nextqueue[pieceidx % 30];
            isextend = true;
            inplan = false;
            Board.piece = defaultop.demino.getmino(nowpiece);
            Board.piece.setpos(19, 3);
            List<mino> allpos = seacher.findallplace(Board);
            int chirdidx = pieceidx + 1;
            if (allpos == null) { tree chird = clone(); chird.isdead = true; chird.pieceidx = chirdidx; chird.inplan = false; chird.isextend = true; treenode.Add(chird); return; };
            foreach (mino m in allpos)
            {
                tree chird = clone();
                chird.Board.piece = m;
                lock_piece_calc(ref chird.Board);
                chird.finmino = m;
                chird.father = this;
                chird.ishold = false;
                chird.holdpiece = holdpiece;
                chird.pieceidx = chirdidx;
                chird.depth = depth + 1;
                chird.maxdepth = chird.depth;
                chird.inplan = true;
                chird.res = bot.evalweight.evalfield(chird);
                chird.score = (int)chird.res.score;
                chird.score += bot.evalweight.evalbattle(chird);
                if (chird.holdT)
                {
                    tree Tchird1 = chird.clone();
                    Tchird1.Board.piece = defaultop.demino.getmino(2);
                    Tchird1.Board.piece.setpos(19, 3);
                    List<mino> Alltslot = seacher.findallplace(Tchird1.Board); // 修改

                    //List<mino> Alltslot = search_tspin.findalltslot(chird.Board);
                    tree bestT;
                    int minscore = chird.score;
                    
                    foreach (mino t in Alltslot)
                    {
                        tree Tchird = chird.clone();
                        Tchird.Board.piece = t;
                        Tuple<int, int> res = lock_piece_calc(ref Tchird.Board);
                        Tchird.score = bot.evalweight.evalfield(Tchird).score;
                        Tchird.score += bot.evalweight.evalbattle(Tchird); // 是否要battle也加上

                        if (Tchird.score > minscore && Tchird.Board.piece.Tspin)
                        {
                            minscore = Tchird.score;
                            bestT = Tchird;
                        }
                    }
                    chird.score = minscore;

                }

                // 回传父节点

                chird.updatefather(); // check update
                treenode.Add(chird);
            }

            if (holdpiece == -1)
            {

                int holdidx = pieceidx + 1, nextnext = pieceidx + 2;

                if (holdidx < bot.nextcnt)
                {


                    Board.piece = defaultop.demino.getmino(bot.nextqueue[holdidx % 30]);
                    Board.piece.setpos(19, 3);
                    List<mino> allpos2 = seacher.findallplace(Board);
                    foreach (mino m in allpos2)
                    {
                        tree chird = clone();
                        chird.Board.piece = m;
                        lock_piece_calc(ref chird.Board);
                        chird.finmino = m;

                        chird.ishold = true;
                        chird.holdpiece = nowpiece;
                        chird.father = this;
                        chird.pieceidx = nextnext;
                        chird.depth = depth + 1;
                        chird.maxdepth = chird.depth;
                        chird.inplan = true;
                        chird.res = bot.evalweight.evalfield(chird);
                        chird.score = (int)chird.res.score;
                        chird.score += bot.evalweight.evalbattle(chird);
                        if (chird.holdT)
                        {
                            tree Tchird1 = chird.clone();
                            Tchird1.Board.piece = defaultop.demino.getmino(2);
                            Tchird1.Board.piece.setpos(19, 3);
                            List<mino> Alltslot = seacher.findallplace(Tchird1.Board);
                            //List<mino> Alltslot = search_tspin.findalltslot(chird.Board);
                            tree bestT;
                            int minscore = chird.score;
                            foreach (mino t in Alltslot)
                            {
                                tree Tchird = chird.clone();
                                Tchird.Board.piece = t;
                                Tuple<int, int> res = lock_piece_calc(ref Tchird.Board);
                                Tchird.score = bot.evalweight.evalfield(Tchird).score;
                                Tchird.score += bot.evalweight.evalbattle(Tchird); // 是否要battle也加上

                                if (Tchird.score > minscore && Tchird.Board.piece.Tspin)
                                {
                                    minscore = Tchird.score;
                                    bestT = Tchird;
                                }
                            }
                            chird.score = minscore;

                        }
                        chird.updatefather();
                        // 回传父节点
                        treenode.Add(chird);
                    }
                    // 回传父节点
                }


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

                    chird.ishold = true;
                    chird.holdpiece = temp; // oops
                    chird.pieceidx = chirdidx;
                    chird.father = this;
                    chird.depth = depth + 1;
                    chird.maxdepth = chird.depth;
                    chird.inplan = true;
                    chird.res = bot.evalweight.evalfield(chird);
                    chird.score = (int)chird.res.score;
                    chird.score += bot.evalweight.evalbattle(chird);
                    if (chird.holdT)
                    {
                        tree Tchird1 = chird.clone();
                        Tchird1.Board.piece = defaultop.demino.getmino(2);
                        Tchird1.Board.piece.setpos(19, 3);
                        List<mino> Alltslot = seacher.findallplace(Tchird1.Board);
                        //List<mino> Alltslot = search_tspin.findalltslot(chird.Board);
                        tree bestT;
                        int minscore = chird.score;
                        foreach (mino t in Alltslot)
                        {
                            tree Tchird = chird.clone();
                            Tchird.Board.piece = t;
                            Tuple<int, int> res = lock_piece_calc(ref Tchird.Board);
                            Tchird.score = bot.evalweight.evalfield(Tchird).score;
                            Tchird.score += bot.evalweight.evalbattle(Tchird); // 是否要battle也加上

                            if (Tchird.score > minscore && Tchird.Board.piece.Tspin) // 可以优化计算顺序
                            {
                                minscore = Tchird.score;
                                bestT = Tchird;
                            }
                        }
                        chird.score = minscore;

                    }
                    chird.updatefather();
                    // 回传父节点
                    treenode.Add(chird);
                }
            }


        }

        public void console_print()
        {

        }
        public bool checkdead()
        {
            return false; // pass 
        }
    }
}
