using System;
using System.Collections.Generic;
using System.Text;
using Jura_Knife_Tetris;

namespace optimizer
{
    class battle
    {
        game p1, p2 ;
        weights w1,  w2;
        public battle()
        {
            w1 = new weights();
            w2 = new weights();
        }
        public battle(weights w1, weights w2)
        {
            this.w1 = w1;
            this.w2 = w2;
        }

        public int start()
        {
            p1 = new game(); p2 = new game();
            p1.bot_init(w1);
            p2.bot_init(w2);
            while (!p1.isdead && !p2.isdead)
            {
                p1.bot_run();
                p2.bot_run();
                foreach (int g in p2.attacking)
                {
                    p1.garbage_queue.Push(g);
                }
                foreach (int g in p1.attacking)
                {
                    p2.garbage_queue.Push(g);
                }
                //p1.garbage_queue = p2.attacking;
                //p2.garbage_queue = p1.attacking;
                p1.deal_garbage();
                p2.deal_garbage();
                //print();
            }
            if (p1.isdead)
            {
                return 2;
            }
            else
            {
                return 1;
            }

        }
        //public void start()
        //{
        //    p1 = new game(); p2 = new game();
        //    p1.bot_init();
        //    p2.bot_init();
        //    while (!p1.isdead && !p2.isdead)
        //    {
        //        p1.bot_run();
        //        p2.bot_run();
        //        foreach (int g in p2.attacking)
        //        {
        //            p1.garbage_queue.Push(g);
        //        }
        //        foreach (int g in p1.attacking)
        //        {
        //            p2.garbage_queue.Push(g);
        //        }
        //        //p1.garbage_queue = p2.attacking;
        //        //p2.garbage_queue = p1.attacking;
        //        p1.deal_garbage();
        //        p2.deal_garbage();
        //        print();
        //    }


        //}

        public void print()
        {
            Console.Clear();
            Console.WriteLine("\n+--------------------+                    +--------------------+");
            for (int j = 22; j >= 0; --j)
            {
                Console.Write("|");
                for (int i = 0; i < 10; ++i)
                {
                    
                        if (p1.Board.field[j, i] != 0)
                        {
                            Console.Write("[]");
                        }
                        else
                        {
                            Console.Write(" +");
                        }
                    
                }
                Console.Write("|                    |");
                for (int i = 0; i < 10; ++i)
                {
                    if (p2.Board.field[j, i] != 0)
                    {
                        Console.Write("[]");
                    }
                    else
                    {
                        Console.Write(" +");
                    }
                }
                Console.Write("|\n");
            }
            Console.WriteLine("\n+--------------------+                    +--------------------+");
        }

    }
}
