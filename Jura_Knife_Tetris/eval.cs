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
        public int[] height = { -120, -500, -2000 };
        public int[] clear = new int[4]; // 1 2 3 4
        public int[] tspin = new int[4]; // mini 1 2 3
        public int wide = 100;
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
        public int downstack =2000;
        public int deephole = 1500;
        public int deephole2 = 1000;
        public int deephole3 = 100;
        public int deltcol = 200;
        public int safecost = 3000;


    }
    static class eval
    {

        static weights W = new weights();
        public static evalresult evalnode(tree node)
        {
            return new evalresult(); // pass
            // 评判场地 以及其他的各种状态
        }

        // 安全距离增加扣分

        public static int evalhole(tree node, int[] colhight, int h, ref int score)
        {
            if (h >= 27) return 0; // 或直接对堵洞判断
            bool canclear = true;
            int holecnt = 0;
            
            
            int downcnt = evalhole(node, colhight, h + 1, ref score);
            int nextsafedis = downcnt;
            int safedis = downcnt; // 该行的安全堆叠层数基数 即上一次层的挖开数 + 1
            for (int i = 0; i < 10; ++i)
            {
                if (!node.Board.field[h, i]) // colh
                {

                    if (colhight[i] >= h)
                    {
                        canclear = false; // 最好检测一下是否封闭
                        if (node.Board.field[h + 1, i])
                        {
                            nextsafedis = Math.Max(nextsafedis, downcnt + 1);
                            nextsafedis = Math.Max(nextsafedis, colhight[i] - h); // 这个safedis需不需要下传 不依托与上层传递时 挖开这层的最少消行数
                            
                        }
                        else
                        {
                            // 与上一个洞连接 理应传递上一层洞的挖开数
                            nextsafedis = Math.Max(nextsafedis, downcnt);
                        }
                        holecnt++;
                    }

                    //if (colhight[i] == h + 1) // 这东西有啥用
                    //{
                    //    // 露天
                    //    holecnt++;
                    //}
                    //else if (colhight[i] >= h)
                    //{
                    //    canclear = false;
                    //    holecnt++;
                    //    // 依托于顶部
                    //}
                }
            }
            // 空格数目
            // 如果顶上也是洞 再减
            
            

            

            if (canclear)
            {
                nextsafedis = 0;
                safedis = 0;
            }

            for (int i = 0; i < 10; ++i)
            {
                if (!node.Board.field[h, i]) // colh 检查！！ 检查算法
                {
                    if (colhight[i] >= h)
                    {
                        score -= W.safecost * safedis;
                        if (colhight[i] - h > (int)(1.5 * (safedis - h)))
                        {
                            score -= W.downstack * (colhight[i] - h - (int)(1.5 * (safedis - h)));
                        }
                    }
                }
            }
            return nextsafedis;

        }



        //private static int evaldeephole(int width)
        //{
        //    for (int i = width - 1; i < 10; ++i)
        //    {

        //    }
        //}


        public static int evalfield(tree node)
        {
            // height
            // hole
            // 洞的优先

            // 出现长洞扣分 (
            // 2宽长洞扣分
            // 堵洞 长列的平衡

            //minhigh 位置
            //// 每列给权重
            int score = 0;
            //if (node.Board.clearrow == 4) score += 10000; // 维持一深洞加分
            //else if (node.Board.clearrow > 0)
            //{
            //    score -= 2000 * node.Board.clearrow;
            //}
            int[] colhight = node.Board.updatecol();
            int height = Math.Max(Math.Max(colhight[3], colhight[4]), Math.Max(colhight[5], colhight[6]));


            if (height > 5)
            {
                score += height * W.height[0];

                if (height > 10)
                {
                    score += height * W.height[1] * 3;
                }
                if (height > 15)
                {
                    score += height * W.height[2] * 10;
                }
            } // 需要细化

            

            
            int minhigh = 41;
            int flag = 1;
            int notrule = 0;
            int idx = 0;
            for (int i = 0; i < colhight.Length; ++i)
            {
                if (minhigh > colhight[i])
                {
                    idx = i;
                    minhigh = colhight[i];
                }

            }

            int deepholecnt = 0;

            int hightower = 0;  // 高塔扣分

            //for (int i = 2; i < 9; ++i)
            //{

            //        if ((colhight[i - 1] - colhight[i - 2] >= 4) && colhight[i] - colhight[i + 1] >= 4)
            //        {

            //             score -= Math.Min((colhight[i - 1] - colhight[i]), colhight[i + 1] - colhight[i]) * W.deephole2;

            //        }
            //} // 2宽塔

            //for (int i = 1; i < 9; ++i)
            //{

            //    if (colhight[i] - colhight[i + 1] >= 2 && colhight[i] - colhight[i - 1] >= 2)
            //    {

            //        score -= (colhight[i + 1] - colhight[i]) * W.deephole;

            //    }
            //} // 2宽塔
            //for (int i = 2; i < 10; ++i)
            //{

            //    if (i == 2)
            //    {

            //        if (colhight[i + 1] - colhight[i] >= 4 && colhight[i + 1] - colhight[i - 1] >= 4 && colhight[i + 1] - colhight[i - 2] >= 4)
            //        {

            //            deepholecnt++;

            //            if (deepholecnt == 1 && (colhight[i] == minhigh || colhight[i - 1] == minhigh))
            //            {

            //            }
            //            else
            //            {
            //                score -= (colhight[i + 1] - colhight[i]) * W.deephole3;
            //            }
            //        }
            //    }
            //    else if (i == 9)
            //    {
            //        if (colhight[i - 2] - colhight[i - 1] >= 4)
            //        {

            //            deepholecnt++;

            //            if (deepholecnt == 1 && (colhight[i] == minhigh || colhight[i - 1] == minhigh))
            //            {

            //            }
            //            else
            //            {
            //                score -= (colhight[i - 2] - colhight[i - 1]) * W.deephole2;
            //            }
            //        }

            //    }
            //    else
            //    {
            //        if ((colhight[i - 2] - colhight[i - 1] >= 4) && colhight[i + 1] - colhight[i] >= 4)
            //        {

            //            deepholecnt++;
            //            if (deepholecnt == 1 && (colhight[i] == minhigh || colhight[i - 1] == minhigh))
            //            {

            //            }
            //            else
            //            {
            //                score -= Math.Min((colhight[i - 2] - colhight[i - 1]), colhight[i + 1] - colhight[i]) * W.deephole2;
            //            }

            //        }
            //    }
            //} // 3宽洞判定

            bool cleartag = false;
            for (int i = 0; i < 10; ++i)
            {
                if (i == 0)
                {
                    if (colhight[i + 1] - colhight[i] >= 2)
                    {

                        deepholecnt++;
                        score -= (colhight[i + 1] - colhight[i]) * W.deephole;
                        if (deepholecnt == 1 && colhight[i] == minhigh)
                        {
                            cleartag = true;
                        }

                    }
                }
                else if (i == 9)
                {
                    if (colhight[i - 1] - colhight[i] >= 2)
                    {

                        deepholecnt++;
                        score -= (colhight[i - 1] - colhight[i]) * W.deephole;
                        if (deepholecnt == 1 && colhight[i] == minhigh)
                        {
                            cleartag = true;
                        }
                    }

                }
                else
                {
                    if ((colhight[i - 1] - colhight[i] >= 1) && colhight[i + 1] - colhight[i] >= 2)
                    {
                        score -= Math.Min((colhight[i - 1] - colhight[i]), colhight[i + 1] - colhight[i]) * W.deephole;
                        deepholecnt++;
                        if (deepholecnt == 1 && colhight[i] == minhigh)
                        {
                            cleartag = true;
                        }

                    }
                }
            }  // hold或next有i才可出第二个 1宽洞判定
            //deepholecnt = 0;

            int tempsc = score;
            for (int i = 1; i < 10; ++i)  // 单列识别两次bug
            {
                if (i == 1)
                {
                    if (colhight[i + 1] - colhight[i] >= 3)
                    {

                        deepholecnt++;
                        score -= (colhight[i + 1] - colhight[i]) * W.deephole2;
                        if (deepholecnt == 1 && (colhight[i] == minhigh || colhight[i - 1] == minhigh))
                        {
                            cleartag = true;
                        }

                    }
                }
                else if (i == 9)
                {
                    if (colhight[i - 2] - colhight[i - 1] >= 3)
                    {

                        deepholecnt++;
                        score -= (colhight[i - 2] - colhight[i - 1]) * W.deephole2;
                        if (deepholecnt == 1 && (colhight[i] == minhigh || colhight[i - 1] == minhigh))
                        {
                            cleartag = true;
                        }
                    }

                }
                else
                {
                    if ((colhight[i - 2] - colhight[i - 1] >= 3) && colhight[i + 1] - colhight[i] >= 3)
                    {

                        deepholecnt++;
                        score -= Math.Min((colhight[i - 2] - colhight[i - 1]), colhight[i + 1] - colhight[i]) * W.deephole2;
                        if (deepholecnt == 1 && (colhight[i] == minhigh || colhight[i - 1] == minhigh))
                        {
                            cleartag = true;
                        }
                    }
                }
            } // 2宽洞判定

            
            

            if (cleartag && deepholecnt == 1) score = tempsc;


            int lefs =idx - 1, rigs= idx + 1;
            int lefhigh = minhigh, righigh = minhigh;
            while (lefs >= 0 || rigs < colhight.Length) // 考虑不合规重置minhigh
            {

                if (lefs >= 0)
                {
                    if (colhight[lefs] >= lefhigh)
                    {
                        if (colhight[lefs] - lefhigh <= 2)
                            score += W.wide;
                        else
                        {
                            score -= (colhight[lefs] - lefhigh - 2) * W.deltcol;
                        }
                        lefhigh = colhight[lefs];
                    }
                    else
                    {
                        score -= (lefhigh - colhight[lefs]) * W.wide;
                    }
                    lefs--;


                }

                if (rigs < colhight.Length)
                {
                    if (colhight[rigs] >= righigh)
                    {
                        if (colhight[rigs] - righigh <= 2)  // 不能超过2个
                            score += W.wide;
                        else
                        {
                            score -= (colhight[rigs] - righigh - 2) * W.deltcol;
                        }
                        righigh = colhight[rigs];
                    }
                    else
                    {
                        score -= (righigh - colhight[rigs]) * W.wide;
                    }
                    rigs++;
                }

            }


            //for (int i = 0; i < colhight.Length; ++i) //
            //{
            //    if (minhigh * flag >= colhight[i] * flag + flag)
            //    {
            //        score += W.wide;
            //        //if (flag == -1)
            //        //{
            //        //    score += W.wide;
            //        //}
            //        if (minhigh * flag == colhight[i] * flag + 1)
            //        {
            //            if (notrule < 3)
            //            {
            //                notrule += 1;
            //                minhigh = colhight[i];
            //            }
            //            else
            //            {
            //                score -= W.wide;
            //            }
            //        }
            //        else
            //        {
            //            minhigh = colhight[i];
            //        }
            //    }
            //    else
            //    {
            //        if (flag == 1) flag = -1;
            //        else
            //        {
            //            score += W.wide * (minhigh * flag - colhight[i] * flag); 
            //        }
            //    }

            //}  // 凹形地形加分 同时也注重了平衡性 场地平横要更注重 反着来一遍

            for (int i = 0; i< colhight.Length; ++i)
            {

            }

            // 底层的洞依托于上一层的洞  的最大挖掘

            evalhole(node, colhight, 0, ref score);

            return score;
        }


    }
}
