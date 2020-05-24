using optimizer;
using System;
using Jura_Knife_Tetris;
using System.Collections.Generic;

namespace training
{

    class Item
    {
        public weights Weights;
        public int win = 0;
        public Item(weights Weights)
        {
            this.Weights = Weights;
        }
        public static Boolean operator >(Item a, Item b)
        {
            return a.win < b.win;
        }
        public static Boolean operator <(Item a, Item b)
        {
            return a.win > b.win;
        }

    }
    class Program
    {


        const int battlecount = 6;
        static void Main(string[] args)
        {
            List<Item> env = new List<Item>();
            while (env.Count < 48) env.Add(null);
            Random rnd = new Random();
            env[0] = (new Item(new weights()));
            env[1] = (new Item(Genetic.generate()));
            for (int i = 2; i < 48; ++i)
            {
                env[i] = new Item(Genetic.crossover(env[rnd.Next(0, 1)].Weights, env[rnd.Next(0, 1)].Weights));
            }

            for (int gen = 0; gen < 500; ++gen)
            {
                int plim = 24;

                
                //optimizer.battle a = new optimizer.battle();
                foreach (Item i in env)
                {
                    foreach (Item j in env)
                    {
                        if (i == j) continue;
                        for (int cnt = 0; cnt < battlecount; ++cnt)
                        {
                            optimizer.battle a = new optimizer.battle(i.Weights, j.Weights);
                            // 多线程加入
                            int res = a.start();
                            if (res == 1)
                            {
                                i.win += 1;
                            }
                            else
                            {
                                j.win += 1;
                            }
                        }
                    }
                }


                env.Sort();
                while (plim < 48)
                {
                    env[plim++] = new Item(Genetic.crossover(env[rnd.Next(0, 1)].Weights, env[rnd.Next(0, 1)].Weights));
                }
            }
        }
    }
}
