using optimizer;
using System;
using Jura_Knife_Tetris;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace training
{

    class Item : IComparable<Item>
    {
        public weights Weights;
        public int win = 0;
        public Item(weights Weights)
        {
            this.Weights = Weights;
        }
        public  int CompareTo (Item other)
        {
            return other.win - win;
        }
    }
    class Program
    {
 

        const int battlecount = 6;
        static void Main(string[] args)
        {
            List<Item> env = new List<Item>();
            while (env.Count < 24) env.Add(null);
            Random rnd = new Random();
            env[0] = (new Item(new weights()));
            env[1] = (new Item(Genetic.generate()));
            for (int i = 2; i < 24; ++i)
            {
                env[i] = new Item(Genetic.crossover(env[rnd.Next(0, 2)].Weights, env[rnd.Next(0, 2)].Weights));
            }

            for (int gen = 0; gen < 500; ++gen)
            {
                int plim = 12;
                foreach (Item i in env)
                {
                    i.win = 0;
                }
                for (int i = plim; i < 24; ++i)
                {
                    env[i] = new Item(Genetic.crossover(env[rnd.Next(0, plim)].Weights, env[rnd.Next(0, plim)].Weights));
                }
                //env[1].win = 1;
                env.Sort();

                //optimizer.battle a = new optimizer.battle();
                int cnt1 = 0;
                foreach (Item i in env)
                {
                    foreach (Item j in env)
                    {
                        if (i == j) continue;
                        for (int cnt = 0; cnt < battlecount; ++cnt)
                        {
                            optimizer.battle a = new optimizer.battle(i.Weights, j.Weights);
                            // 多线程加入
                            //Thread t = new Thread(a.start);
                            int res = a.start();
                            if (res == 1)
                            {
                                i.win += 1;
                            }
                            else
                            {
                                j.win += 1;
                            }
                            cnt1++;
                            Console.WriteLine(cnt1);
                        }
                    }
                }


                env.Sort();

                FileStream fs = new FileStream("best.txt", FileMode.Append);
                byte[] myByte = System.Text.Encoding.UTF8.GetBytes("gen" + gen);
                fs.Write(myByte, 0, myByte.Length);
                myByte = System.Text.Encoding.UTF8.GetBytes(env[0].Weights.ToString());
                fs.Write(myByte, 0, myByte.Length);


                FileStream fs1 = new FileStream("pop.txt", FileMode.Append);
                byte[] myByte1 = System.Text.Encoding.UTF8.GetBytes("gen" + gen);
                fs.Write(myByte1, 0, myByte.Length);
                for (int i = 1; i <= 10; ++i) { 
                myByte = System.Text.Encoding.UTF8.GetBytes(env[0].Weights.ToString());
                fs.Write(myByte1, 0, myByte.Length);
                }



            }
        }
    }
}
