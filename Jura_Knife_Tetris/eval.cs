using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{

    public class evalresult
    {
        public bool value;
        public bool attackper; // 攻击许可
        public bool defper; // 防御许可
        public int score = 0; // 评定分数
        public bool clearinst;// 是否能立即消除

        public int deephole = 0;
        public int wide = 0;
        public int Continuity = 0;
        public int hole = 0;
        public int height = 0;
        public int minidx = 0;
        public int minhigh = 0;


        public void print()
        {
            Console.WriteLine("score = {0}", score);
            Console.WriteLine("height = {0}", height);
            Console.WriteLine("wide = {0}", wide);
            Console.WriteLine("Continuity = {0}", Continuity);
            Console.WriteLine("hole = {0}", hole);
            Console.WriteLine("deephole = {0}", deephole);
            Console.WriteLine("minidx = {0}", minidx);
            Console.WriteLine("minhigh = {0}", minhigh);
        }
        //public evalresult()
        //{

        //}

    }


    class weights
    {
        public int[] height = { -200, -500, -2000, -3000 };
        public int[] clear = { 0, -700, -300, -400, 1700 }; // 1 2 3 4 // combo时也许不一样
        public int[] tspin = { 0, 1100, 2800, 200 }; // mini 1 2 3
        public int wide = -300;
        public int b2b;
        public int b2b_clear;
        public int wastedT = -50000;
        public int[] tslot = new int[4]; // mini 1 2 3
        public int movetime = -3; // 操作数
        public int tslotnum; // t坑数目jla
        public int holdT = 400;
        public int holdI = 200;
        public int perfectclear = 9999;
        public int bus = -30;
        public int bus_sq = -100;
        public int fewcombo;
        public int lotcombo; // maybe combo table
        public int maxdef; // 最高防御垃圾行
        public int attack; // 攻击
        public int downstack = -1000;
        public int deephole = -1000;
        public int deephole2 = 400;
        public int deephole3 = 100;
        public int deltcol = -300;
        public int safecost = -1500;
        public int parity = -000;
        public int dephigh = -1000;
        public int linefull = -0;
        public int[] col_minhigh = { -20, -30, -40, 30, 50, 30, 30, -40, -30, -20 };


    }
    static class eval // 平整 无洞 易挖 奇偶性
    {

        static weights W = new weights();



        public static int evalparity(tree node) // 表面奇偶性
        {
            int parity = 0;
            for (int i = 0; i < node.Board.column_height.Length; ++i)
            {

                if ((node.Board.column_height[i] & 1) == 1)
                {
                    if ((i & 1) == 1)
                    {
                        parity += 1;
                    }
                    else
                    {
                        parity -= 1;
                    }
                }
            }
            return parity;
        }

        public static evalresult evalfield(tree node)
        {
            int score = 0;
            evalresult evalresult = new evalresult();
            int[] colh = new int[node.Board.column_height.Length + 2];
            for (int i = 0; i < node.Board.column_height.Length; ++i)
            {
                colh[i + 1] = node.Board.column_height[i];
            }

            colh[0] = colh[colh.Length - 1] = 60;
            int height = Math.Max(Math.Max(colh[4], colh[5]), Math.Max(colh[6], colh[7]));
            // 高度评分

            evalresult.height = height * W.height[height / 7];
            score += height * W.height[height / 7];

            // 场地最低点
            int minhigh = 40;

            for (int i = 1; i < colh.Length - 1; ++i)
            {
                minhigh = Math.Min(minhigh, colh[i]);
            }


            int minidx = 1; // 场地最低点
            int mindel = 0;

            for (int i = 1; i < colh.Length - 1; ++i)
            {
                if (minhigh == colh[i] && Math.Min(colh[i - 1], colh[i + 1]) - colh[i] >= mindel)
                {
                    minidx = i;
                    mindel = Math.Min(colh[i - 1], colh[i + 1]) - colh[i];
                }
            }
            evalresult.minidx = minidx;
            evalresult.minhigh = colh[minidx];
            score += W.dephigh * colh[minidx];
            score += W.col_minhigh[minidx - 1] * Math.Min(mindel, 5);
            if (mindel > 7)
            {
                score -= Math.Abs(W.col_minhigh[minidx - 1]) * mindel;
            }
            // 长洞（？
            // 深洞本身高度扣分 过深扣分

            Queue<int> deepholequeue = new Queue<int>();
            for (int i = 1; i < colh.Length - 1; ++i)
            {
                int left = colh[i - 1] - colh[i];
                int right = colh[i + 1] - colh[i];

                if (left >= 2 && right >= 2) // 都大于5制裁
                {
                    //if (i != minidx)
                    {
                        //score += W.deephole * Math.Min(left, right);
                        //evalresult.deephole += W.deephole * Math.Min(left, right); // duojinzhicai
                        deepholequeue.Enqueue(Math.Min(left, right) /**+ Math.Max(left, right) / 2.0**/);
                    }
                }

            }
            foreach (int a in deepholequeue)
            {
                //score += W.deephole * a; // 加入深洞底部厚度
                score += W.deephole * a * 1;
                evalresult.deephole += W.deephole * a * 1; // duojinzhicai
            }
            deepholequeue.Clear();

            // 凹形判定


            int lefidx = minidx - 1, ritidx = minidx + 1;
            int lefhig = colh[lefidx], rithig = colh[ritidx];



            while (lefidx > 1)
            {
                if (colh[lefidx - 1] >= lefhig) // 大于太多也扣分（？
                {
                    lefhig = colh[lefidx - 1];
                }
                else
                {
                    evalresult.wide += W.wide * (lefhig - colh[lefidx - 1]);
                    score += W.wide * (lefhig - colh[lefidx - 1]);
                }
                lefidx -= 1;
            }

            while (ritidx < colh.Length - 1)
            {
                if (colh[ritidx + 1] >= rithig)
                {
                    rithig = colh[ritidx + 1];
                }
                else
                {
                    evalresult.wide += W.wide * (rithig - colh[ritidx + 1]);
                    score += W.wide * (rithig - colh[ritidx + 1]);
                }
                ritidx += 1;
            }

            // 连续性判定 

            for (int i = 2; i < colh.Length - 1; ++i)
            {

                int del;

                del = colh[i] - colh[i - 1];
                //right = colh[i + 1] - colh[i];
                //if (left >= 3 && right >= 3)

                score += Math.Abs(del) * W.deltcol;
                evalresult.Continuity += Math.Abs(del) * W.deltcol;

            } // 连续性



            double avg = 0;
            for (int i = 1; i < colh.Length - 1; ++i)
            {
                avg += colh[i];
            } // 平均值

            // 是否减去最低点
            //avg = (avg - minhigh) / (node.Board.column_height.Length - 1);
            avg /= node.Board.column_height.Length;
            // 方差 标准差
            double bus_sq = 0, bus = 0;
            for (int i = 1; i < colh.Length - 1; ++i)
            {
                bus_sq += Math.Pow(colh[i] - avg, 2);
            }
            //bus_sq -= Math.Pow(minhigh - avg, 2);
            //bus = Math.Sqrt(bus_sq);
            // 方差 标准差

            //score += bus * W.bus + bus_sq * W.bus_sq;
            //score += (int)(bus * W.bus);
            //

            // 奇偶性

            //int parity = evalparity(node);
            //score += parity * W.parity;

            evalresult.hole = evalhole(node);
            //evalhole(node, node.Board.column_height, 0, ref evalresult.hole);
            score += evalresult.hole;
            evalresult.score = score;




            return evalresult;
        } //场地评分

        //public static double evalhole(tree node)
        //{
        //    double score = 0;
        //    int roof = 0;
        //    foreach (int a in node.Board.column_height)
        //    {
        //        roof = Math.Max(a, roof);
        //    }
        //    int digrow = 0; // 上层被挖开的所需层数
        //    double[] DIG = new double[node.Board.column_height.Length];

        //    int[] fulldig = new int[40];

        //    for (int row = roof - 1; row >= 0; --row)
        //    {
        //        int nextdig = 0;// 该层被挖开的所需层数
        //        for (int cell = 0; cell < node.Board.column_height.Length; ++cell)
        //        {
        //            if (!node.Board.field[row, cell])
        //            {
        //                fulldig[row]++;
        //                //if (row + 1 < node.Board.column_height[cell])
        //                if (node.Board.field[row + 1, cell])
        //                {
        //                    int temp = Math.Max(digrow, fulldig[row + 1] - fulldig[node.Board.column_height[cell]]);
        //                    nextdig = Math.Max(temp, nextdig);
        //                    score += temp * W.downstack;
        //                }
        //                else
        //                {
        //                    if (DIG[cell] < 1000000) 
        //                        DIG[cell] *= 5;
        //                    score += DIG[cell] * W.downstack;

        //                }
        //            }
        //        }
        //        fulldig[row] += fulldig[row + 1];
        //        for (int cell = 0; cell < node.Board.column_height.Length; ++cell)
        //        {
        //            if (node.Board.field[row + 1, cell])
        //            {
        //                DIG[cell] = nextdig;
        //            }

        //        }

        //           digrow = nextdig;


        //    }


        //    return score;
        //} // 空洞（可挖性评分;

        public static evalresult evalnode(tree node)
        {
            return new evalresult(); // pass
            // 评判场地 以及其他的各种状态
        }

        // 安全距离增加扣分

        //public static int evalTmino()
        //{

        //}



        public static int evalhole(tree node)
        {
            int[] colhight = node.Board.column_height;
            int roof = 0;
            int score = 0;
            for (int i = 0; i < colhight.Length; ++i)
            {
                roof = Math.Max(colhight[i], roof);
            }
            int nextsafedis = 0;
            int safedis = 0; // 该行的安全堆叠层数基数 即上一次层的挖开数 + 1
            int[] fulldig = new int[40];
            for (int row = roof - 1; row >= 0; --row)
            {
                bool canclear = true;
                safedis = nextsafedis;
                int downcnt = safedis;
                for (int i = 0; i < 10; ++i)
                {
                    if (!node.Board.field[row, i]) // colh
                    {
                        fulldig[row]++;
                        if (colhight[i] >= row + 1)
                        {
                            canclear = false; // 最好检测一下是否封闭
                            if (node.Board.field[row + 1, i])
                            {
                                int temp = Math.Max(downcnt + 1, colhight[i] - row - 1);
                                //score += W.safecost * temp;
                                score += W.linefull * (fulldig[row + 1] - fulldig[row + 1 + safedis]);
                                nextsafedis = Math.Max(nextsafedis, downcnt + 1);
                                nextsafedis = Math.Max(nextsafedis, colhight[i] - row - 1); // 这个safedis需不需要下传 不依托与上层传递时 挖开这层的最少消行数
                                                                                          // 安全距离失误？
                            }
                            else
                            {
                                // 与上一个洞连接 理应传递上一层洞的挖开数
                                //score += W.safecost * safedis;
                                nextsafedis = Math.Max(nextsafedis, downcnt);
                            }
                            
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
                fulldig[row]+= fulldig[row + 1] ;
                if (canclear)
                {
                    nextsafedis = 0;
                    safedis = 0;
                }

                for (int i = 0; i < 10; ++i) //洞的层数 需要增加
                {
                    if (!node.Board.field[row, i]) // colh 检查！！ 检查算法
                    {
                        if (colhight[i] >= row + 1)
                        {
                            score += W.safecost * safedis /**  Math.Max(nextsafedis - safedis, 0)*/;
                            if (colhight[i] - row - 1 > (int)(1.3 * (safedis - row - 1)))
                            {
                                score += W.downstack * (colhight[i] - row - 1 - (int)(1.3 * (safedis - row - 1)));
                            }
                        }
                    }
                }

            }
            return score;
        }


        public static int evalhole(tree node, int[] colhight, int h, ref int score) // 造洞的分析 边缘空洞
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

                    if (colhight[i] >= h + 1)
                    {
                        canclear = false; // 最好检测一下是否封闭
                        if (node.Board.field[h + 1, i])
                        {
                            nextsafedis = Math.Max(nextsafedis, downcnt + 1);
                            nextsafedis = Math.Max(nextsafedis, colhight[i] - h - 1); // 这个safedis需不需要下传 不依托与上层传递时 挖开这层的最少消行数
                            // 安全距离失误？
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

            for (int i = 0; i < 10; ++i) //洞的层数 需要增加
            {
                if (!node.Board.field[h, i]) // colh 检查！！ 检查算法
                {
                    if (colhight[i] >= h + 1)
                    {
                        score += W.safecost * safedis /**  Math.Max(nextsafedis - safedis, 0)*/;
                        if (colhight[i] - h - 1 > (int)(1.3 * (safedis - h - 1)))
                        {
                            score += W.downstack * (colhight[i] - h - 1 - (int)(1.3 * (safedis - h - 1)));
                        }
                    }
                }
            }
            return nextsafedis;

        }
        // 洞判定部分可能更好


        //private static int evaldeephole(int width)
        //{
        //    for (int i = width - 1; i < 10; ++i)
        //    {

        //    }
        //}


        public static int evalbattle(tree node)
        {
            int score = 0;
            score += W.movetime * node.Board.piece.path.idx;

            if (node.holdT)
                score += W.holdT;
            if (node.holdI)
                score += W.holdI;
            if (node.Board.piece.Tspin && node.Board.piece.name == "T")
            {
                if (node.Board.piece.mini)
                {
                    score += W.tspin[0];
                }
                else
                {
                    score += W.tspin[node.Board.clearrow];
                }

            }
            else
            {
                score += W.clear[node.Board.clearrow];

            }
            if (node.Board.piece.name == "T" && (!node.Board.piece.Tspin || node.Board.clearrow == 0))
            {
                score += W.wastedT;
            }
            //score
            return score;
        }

        //public static int evalfield(tree node)
        //{
        //    // height
        //    // hole
        //    // 洞的优先

        //    // 出现长洞扣分 (
        //    // 2宽长洞扣分
        //    // 堵洞 长列的平衡

        //    //minhigh 位置
        //    //// 每列给权重
        //    int score = 0;




        //    int[] colhight = node.Board.updatecol();
        //    int height = Math.Max(Math.Max(colhight[3], colhight[4]), Math.Max(colhight[5], colhight[6]));


        //    if (height > 5)
        //    {
        //        score += height * W.height[0];

        //        if (height > 10)
        //        {
        //            score += height * W.height[1] * 3;
        //        }
        //        if (height > 15)
        //        {
        //            score += height * W.height[2] * 10;
        //        }
        //    } // 需要细化




        //    int minhigh = 41;
        //    int flag = 1;
        //    int notrule = 0;
        //    int idx = 0;
        //    for (int i = 0; i < colhight.Length; ++i)
        //    {
        //        if (minhigh > colhight[i])
        //        {
        //            idx = i;
        //            minhigh = colhight[i];
        //        }

        //    } // 两minhigh 以及minhigh位置

        //    int deepholecnt = 0;

        //    int hightower = 0;  // 高塔扣分

        //    int tempsc = score;

        //    bool cleartag = false;
        //    for (int i = 0; i < 10; ++i)
        //    {
        //        if (i == 0)
        //        {
        //            if (colhight[i + 1] - colhight[i] >= 2)
        //            {

        //                deepholecnt++;
        //                score -=  (colhight[i + 1] - colhight[i]) * W.deephole;
        //                if (deepholecnt == 1 && colhight[i] == minhigh)
        //                {
        //                    cleartag = true;
        //                }

        //            }
        //        }
        //        else if (i == 9)
        //        {
        //            if (colhight[i - 1] - colhight[i] >= 2)
        //            {

        //                deepholecnt++;
        //                score -= (colhight[i - 1] - colhight[i]) * W.deephole;
        //                if (deepholecnt == 1 && colhight[i] == minhigh)
        //                {
        //                    cleartag = true;
        //                }
        //            }

        //        }
        //        else
        //        {
        //            if ((colhight[i - 1] - colhight[i] >= 1) && colhight[i + 1] - colhight[i] >= 2)
        //            {
        //                score -= Math.Min((colhight[i - 1] - colhight[i]), colhight[i + 1] - colhight[i]) * W.deephole;
        //                deepholecnt++;
        //                if (deepholecnt == 1 && colhight[i] == minhigh)
        //                {
        //                    cleartag = true;
        //                }

        //            }
        //        }
        //    }  // hold或next有i才可出第二个 1宽洞判定
        //    //deepholecnt = 0;


        //    for (int i = 1; i < 10; ++i)  // 单列识别两次bug
        //    {
        //        if (i == 1)
        //        {
        //            if (colhight[i + 1] - colhight[i] >= 3)
        //            {

        //                deepholecnt++;
        //                score -= (colhight[i + 1] - colhight[i]) * W.deephole2;
        //                if (deepholecnt == 1 && (colhight[i] == minhigh || colhight[i - 1] == minhigh))
        //                {
        //                    cleartag = true;
        //                }

        //            }
        //        }
        //        else if (i == 9)
        //        {
        //            if (colhight[i - 2] - colhight[i - 1] >= 3)
        //            {

        //                deepholecnt++;
        //                score -= (colhight[i - 2] - colhight[i - 1]) * W.deephole2;
        //                if (deepholecnt == 1 && (colhight[i] == minhigh || colhight[i - 1] == minhigh))
        //                {
        //                    cleartag = true;
        //                }
        //            }

        //        }
        //        else
        //        {
        //            if ((colhight[i - 2] - colhight[i - 1] >= 3) && colhight[i + 1] - colhight[i] >= 3)
        //            {

        //                deepholecnt++;
        //                score -= Math.Min((colhight[i - 2] - colhight[i - 1]), colhight[i + 1] - colhight[i]) * W.deephole2;
        //                if (deepholecnt == 1 && (colhight[i] == minhigh || colhight[i - 1] == minhigh))
        //                {
        //                    cleartag = true;
        //                }
        //            }
        //        }
        //    } // 2宽洞判定




        //    if (cleartag && deepholecnt == 1) score = tempsc;


        //    int lefs =idx - 1, rigs= idx + 1;
        //    int lefhigh = minhigh, righigh = minhigh;
        //    while (lefs >= 0 || rigs < colhight.Length) // 考虑不合规重置minhigh
        //    {

        //        if (lefs >= 0)
        //        {
        //            if (colhight[lefs] >= lefhigh)
        //            {
        //                if (idx - 1 == lefs)
        //                {
        //                    score += Math.Min(5, (colhight[lefs] - lefhigh)) * W.col_minhigh[idx]; // 可能用不一样的参数

        //                }
        //                else
        //                if (colhight[lefs] - lefhigh <= 2)
        //                    score += W.wide;
        //                else
        //                {
        //                    score -= (colhight[lefs] - lefhigh - 2) * W.deltcol;
        //                }
        //                lefhigh = colhight[lefs];
        //            }
        //            else
        //            {
        //                score -= (lefhigh - colhight[lefs]) * W.wide;
        //            }
        //            lefs--;


        //        }

        //        if (rigs < colhight.Length)
        //        {
        //            if (colhight[rigs] >= righigh)
        //            {
        //                if (idx + 1 == rigs)
        //                {
        //                    score += Math.Min(5, (colhight[rigs] - righigh)) * W.col_minhigh[idx]; // 可能用不一样的参数

        //                }
        //                else
        //                if (colhight[rigs] - righigh <= 2)  // 不能超过2个
        //                    score += W.wide;
        //                else
        //                {
        //                    score -= (colhight[rigs] - righigh - 2) * W.deltcol;
        //                }
        //                righigh = colhight[rigs];
        //            }
        //            else
        //            {
        //                score -= (righigh - colhight[rigs]) * W.wide;
        //            }
        //            rigs++;
        //        }

        //    }


        //    //for (int i = 0; i < colhight.Length; ++i) //
        //    //{
        //    //    if (minhigh * flag >= colhight[i] * flag + flag)
        //    //    {
        //    //        score += W.wide;
        //    //        //if (flag == -1)
        //    //        //{
        //    //        //    score += W.wide;
        //    //        //}
        //    //        if (minhigh * flag == colhight[i] * flag + 1)
        //    //        {
        //    //            if (notrule < 3)
        //    //            {
        //    //                notrule += 1;
        //    //                minhigh = colhight[i];
        //    //            }
        //    //            else
        //    //            {
        //    //                score -= W.wide;
        //    //            }
        //    //        }
        //    //        else
        //    //        {
        //    //            minhigh = colhight[i];
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        if (flag == 1) flag = -1;
        //    //        else
        //    //        {
        //    //            score += W.wide * (minhigh * flag - colhight[i] * flag); 
        //    //        }
        //    //    }

        //    //}  // 凹形地形加分 同时也注重了平衡性 场地平横要更注重 反着来一遍

        //    for (int i = 0; i< colhight.Length; ++i)
        //    {

        //    }

        //    // 底层的洞依托于上一层的洞  的最大挖掘

        //    evalhole(node, colhight, 0, ref score);

        //    return score;
        //}


    }
}
