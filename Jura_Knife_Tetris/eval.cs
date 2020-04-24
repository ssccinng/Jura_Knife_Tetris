using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{

    class evalresult
    {
        public bool value;
        public bool attackper; // 攻击许可
        public bool defper; // 防御许可
        public int score; // 评定分数
        public bool clearinst;// 是否能立即消除

        public evalresult()
        {
            
        }

    }


    class weights {
        public int height = - 20;
        public int[] clear = new int[4]; // 1 2 3 4
        public int[] tspin = new int[4]; // mini 1 2 3
        public int wide = 30;
        public int b2b;
        public int b2b_clear;
        public int wastedT;
        public int[] tslot = new int[4]; // mini 1 2 3
        public int movetime; // 操作数
        public int tslotnum; // t坑数目
        public int holdT;
        public int holdI;
        public int fewcombo;
        public int lotcombo; // maybe combo table
        public int maxdef; // 最高防御垃圾行
        public int attack; // 攻击




    }
    static class eval
    {

        static weights W = new weights();
        public static evalresult evalnode(tree node)
        {
            return new evalresult(); // pass
            // 评判场地 以及其他的各种状态
        }
        public static int evalfield(tree node)
        {
            // height
            // hole
            // 洞的优先
            int score = 0;

            int[] colhight = node.Board.updatecol();
            int height = Math.Max(Math.Max(colhight[3], colhight[4]), Math.Max(colhight[5], colhight[6]));
            score += height * W.height;
            int minhigh = 41;
            int flag = 1;
            int notrule = 0;
            for (int i = 0; i < colhight.Length; ++i)
            {
                if (minhigh * flag >= colhight[i] * flag + flag)
                {

                    score += W.wide;
                    //if (flag == -1)
                    //{
                    //    score += W.wide;
                    //}
                    if (minhigh * flag > colhight[i] * flag)
                    {
                        if (notrule < 3)
                        {
                            notrule += 1;
                            minhigh = colhight[i];
                        }
                        else
                        {
                            score -= W.wide;
                        }
                    }
                    else
                    {
                        minhigh = colhight[i];
                    }
                }
                else
                {
                    if (flag == 1) flag = -1;
                    else
                    {
                        score += W.wide * (minhigh * flag - colhight[i] * flag); ;
                    }
                }

            }  // 凹形地形加分 同时也注重了平衡性 场地平横要更注重



            return score;
        }


    }
}
