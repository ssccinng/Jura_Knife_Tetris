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
            for (int i = 0; i < 0; ++i)
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
                //if (bot.boardtree.treenode[0].res.score < -30000)
                //foreach (tree chird in bot.boardtree.treenode)
                //{
                //    chird.Board.console_print(false);
                //    Console.WriteLine(chird.score);
                //    //Console.WriteLine(chird.finmino.minopos.x);
                //    //Console.WriteLine(chird.finmino.minopos.y);
                //    //Console.WriteLine(chird.finmino.stat);
                //    Console.WriteLine(chird.finmino.name);
                //    foreach (int a in chird.Board.column_height)
                //    {
                //        Console.Write(a);
                //        Console.Write(" ");
                //    }
                //    Console.WriteLine("");
                //    double kk = 0;
                //    //eval.evalhole(chird, chird.Board.column_height, 0, ref kk);
                //    chird.res.print();
                //    //char a1 = Console.ReadKey().KeyChar;
                //}
                tree root = bot.requset_next_move();
                

                //eval.evalfield(root);
                //Console.Clear();
                //root.Board.console_print(true, root.finmino);
                //root.Board.console_print(false);
                //Console.WriteLine("resroot");
                //Console.WriteLine(root.score);
                //Console.WriteLine(bot.nodequeue.Count);
                Console.WriteLine(root.pieceidx);
                //Console.WriteLine(root.Board.piece.name);
                //foreach(int a in root.Board.column_height)
                //{
                //    Console.Write(a);
                //    Console.Write(" ");
                //}
                //Console.WriteLine("");
                //root.res.print();
                //Console.WriteLine("-----------------------------------------------");
                //root.finmino.console_print();
                //if ( root.holdpiece != -1)
                //{
                //    defaultop.demino.getmino(root.holdpiece).console_print();
                //}
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
