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
            bot.boardtree.Board.console_print(false);
            mino_gene mino_Gene = new mino_gene();
            Random rand = new Random();
            for (int i = 0; i < 2; ++i)
            {
                //bot.add_next(rand.Next() % 7);
                bot.add_next(mino_Gene.genebag7int());
            }
            //char a2 = Console.ReadKey().KeyChar;
            //bot.add_next(3);
            //bot.add_next(2);

            int t = 7;

            //int[] nextqq = { 3, 1, 6, 2, 0, 5, 2, 1, 4, 0, 1, 6, 5 };

            while (true)
            //foreach (int q in nextqq)
            {
                //bot.add_next(rand.Next() % 7);
                bot.add_next(mino_Gene.genebag7int());
                //bot.add_next(q);
                bot.extend_node();

                //foreach (tree chird in bot.boardtree.treenode)
                //{
                //    chird.Board.console_print(true, chird.finmino);
                //    Console.WriteLine(chird.score);
                //    //Console.WriteLine(chird.finmino.minopos.x);
                //    //Console.WriteLine(chird.finmino.minopos.y);
                //    //Console.WriteLine(chird.finmino.stat);
                //    Console.WriteLine(chird.finmino.name);
                //    //char a1 = Console.ReadKey().KeyChar;
                //}
                tree root = bot.requset_next_move();
                

                eval.evalfield(root);
                Console.Clear();
                //root.Board.console_print(true, root.finmino);
                root.Board.console_print(false);
                Console.WriteLine(root.score);
                Console.WriteLine(bot.nodequeue.Count) ;
                Console.WriteLine(root.pieceidx);
                Console.WriteLine(root.Board.piece.name);
                Console.WriteLine("-----------------------------------------------");
                root.finmino.console_print();
                if ( root.holdpiece != -1)
                {
                    defaultop.demino.getmino(root.holdpiece).console_print();
                }
                //char a1 = Console.ReadKey().KeyChar;

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
