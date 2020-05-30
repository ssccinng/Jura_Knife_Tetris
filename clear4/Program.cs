using System;
using System.Threading;
using Jura_Knife_Tetris;

namespace clear4
{
    class Program
    {


        static void Main(string[] args)
        {
            int nextcnt = 6;
            byte[] movetag = { 65, 68, 75, 76, 83 };
            int[] last = Topfit.Get_MuliNext(nextcnt);
            int[] nownext = null;

            while (true)
            {
                bool firsthold = true;
                Thread.Sleep(100);
                nownext = Topfit.Get_MuliNext(nextcnt);
                foreach (int i in last)
                {
                    Console.Write("{0} ", i);
                }
                Console.WriteLine(" ");
                foreach (int i in nownext)
                {
                    Console.Write("{0} ", i);
                }
                Console.WriteLine(" "); Console.WriteLine(" ");

                if (check_gamestart(last, nownext))
                {
                    Console.WriteLine("start!");
                    Juraknifecore bot = new Juraknifecore();
                    foreach (int piece in nownext)
                    {
                        bot.add_next(piece);
                    }
                    bot.init();
                    bot.extend_node();

                    while (checkarrayequal(Topfit.Get_MuliNext(nextcnt), nownext))
                    {
                        Thread.Sleep(100);
                    }
                    nownext = Topfit.Get_MuliNext(nextcnt);
                    bot.add_next(nownext[nextcnt - 1]);
                    bot.extend_node();

                    while (true)
                    {
                        tree move = bot.requset_next_move();
                        Console.WriteLine("atk = {0}", move.attack);
                        Console.WriteLine("maxattack = {0}", move.maxattack);
                        Console.WriteLine("def = {0}", move.def);
                        Console.WriteLine("maxdef = {0}", move.maxdef);
                        Console.WriteLine("atkscore = {0}", move.atkscore);
                        if (move.ishold)
                        {
                            Topfit.keybd_event((byte)74, 0, 0, 0);
                            Thread.Sleep(25);
                            Topfit.keybd_event((byte)74, 0, 2, 0);
                            Thread.Sleep(17);

                            if (firsthold)
                            {
                                firsthold = false;
                                nownext = Topfit.Get_MuliNext(nextcnt);
                                bot.add_next(nownext[nextcnt - 1]);
                                bot.extend_node();
                            }
                        }
                        Console.WriteLine(move.finmino.name);
                        for (int i = 0; i < move.finmino.path.idx; ++i)
                        {
                            //Console.WriteLine(movetag[move.finmino.path.path[i]]);
                            if (move.finmino.path.path[i] != 4)
                            {
                                Topfit.keybd_event(movetag[move.finmino.path.path[i]], 0, 0, 0);
                                Thread.Sleep(25);
                                Topfit.keybd_event(movetag[move.finmino.path.path[i]], 0, 2, 0);
                                Thread.Sleep(17);
                            }
                            else
                            {
                                Topfit.keybd_event(movetag[move.finmino.path.path[i]], 0, 0, 0);
                                Thread.Sleep(400);
                                Topfit.keybd_event(movetag[move.finmino.path.path[i]], 0, 2, 0);
                                Thread.Sleep(17);
                            }
                        }
                        Topfit.keybd_event((byte)87, 0, 0, 0);
                        Thread.Sleep(25);
                        Topfit.keybd_event((byte)87, 0, 2, 0);
                        Thread.Sleep(200);
                        nownext = Topfit.Get_MuliNext(nextcnt);
                        bot.add_next(nownext[nextcnt - 1]);
                        bot.extend_node();
                        //Thread.Sleep(1000);

                    }

                }
                //Console.WriteLine(Topfit.hProcess);
                //aa.GetPidByProcessName("tetris");

                int[] a = Topfit.Get_MuliNext(nextcnt);


            }
        }

        //static bool checkgameend()
        //{

        //}

        static bool checkarrayequal(int[] next1, int[] next2)
        {
            for (int i = 0; i < next1.Length; ++i)
            {
                if (next1[i] != next2[i]) return false;
            }
            return true;
        }

        static bool check_gamestart(int[] next1, int[] next2)
        {
            bool flag = false;
            //for (int i = 0; i < next1.Length - 1; ++i)
            //{
            //    if (next2[i] != next1[i + 1])
            //    {
            //        flag = true;
            //    }
            //}
            //if (flag) return false;

            for (int i = 0; i < next1.Length; ++i)
            {
                if (next1[i] != next2[i])
                {
                    flag = true;
                }
            }
            if (flag) return true;
            return false;
        }
    }
}
