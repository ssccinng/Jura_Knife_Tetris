using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{
    class test
    {
        Juraknifecore bot = new Juraknifecore();
        public void run()
        {

            bot.init();

            Random rand = new Random();
            for (int i = 0; i < 2; ++i)
            {
                bot.add_next(rand.Next() % 7);
            }

            //bot.add_next(3);
            //bot.add_next(2);

            int t = 7;
            while (true)
            {
                bot.add_next(rand.Next() % 7);
                //bot.add_next(t % 7);
                bot.extend_node();
                

                tree root = bot.requset_next_move();


                eval.evalfield(root);
                root.Board.console_print(false);
                Console.WriteLine(root.score);
                Console.WriteLine(root.Board.piece.name);
                Console.WriteLine("-----------------------------------------------");
                //foreach (tree chird in root.treenode)
                //{
                //    chird.Board.console_print(false);
                //    Console.WriteLine(chird.score);
                //    //Console.WriteLine(chird.finmino.minopos.x);
                //    //Console.WriteLine(chird.finmino.minopos.y);
                //    //Console.WriteLine(chird.finmino.stat);
                //    //Console.WriteLine(chird.finmino.name);
                //    //char a1 = Console.ReadKey().KeyChar;
                //}

                //char a2 = Console.ReadKey().KeyChar;
                //foreach (tree a in bot.boardtree.treenode)
                //{
                //    //a.Board.console_print(false);
                //    //char a1 = Console.ReadKey().KeyChar;
                //}

                //bot.add_next(2);
                //foreach (tree a in bot.boardtree.treenode)
                //{
                //    bot.extend_node(a);
                //    foreach (tree a1 in a.treenode)
                //    {
                //        a1.Board.console_print(false);
                //        char a2 = Console.ReadKey().KeyChar;
                //        //char a1 = Console.ReadKey().KeyChar;
                //    }

                //}
            }
        }
    }
}
