﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{
    class test
    {
        
        public void run()
        {
            while (true)
            {
                Juraknifecore bot = new Juraknifecore();
                bot.init();
                //bot.boardtree.Board.console_print(false);
                mino_gene mino_Gene = new mino_gene();
                Random rand = new Random();
                for (int i = 0; i < 3; ++i)
                {
                    //bot.add_next(rand.Next() % 7);
                    bot.add_next(mino_Gene.genebag7int());
                }
                //char a2 = Console.ReadKey().KeyChar;
                //bot.add_next(3);
                //bot.add_next(2);

                int t = 7;

                //int[] nextqq = { 3, 1, 6, 2, 0, 5, 2, 1, 4, 0, 1, 6, 5 };
                Queue<tree> ans = new Queue<tree>();
                while (true)
                //foreach (int q in nextqq)
                {
                    //bot.add_next(rand.Next() % 7);
                    bot.add_next(mino_Gene.genebag7int());
                    //bot.add_next(q);
                    bot.extend_node();
                    //if (bot.boardtree.treenode[0].res.score < -1000000)
                    {
                        Console.WriteLine("ressearch");
                        foreach (tree chird in bot.boardtree.treenode)

                        {
                            chird.Board.console_print(false);
                            Console.WriteLine("atk = {0}", chird.attack);
                            Console.WriteLine("maxattack = {0}", chird.maxattack);
                            Console.WriteLine("def = {0}", chird.def);
                            Console.WriteLine("maxdef = {0}", chird.maxdef);
                            Console.WriteLine("atkscore = {0}", chird.atkscore);
                            Console.WriteLine("battlescore = {0}", chird.battlescore);
                            Console.WriteLine("combo = {0}", chird.Board.combo);
                            Console.WriteLine("clearrow = {0}", chird.Board.clearrow);
                            Console.WriteLine("movetime = {0}", chird.finmino.path.movetime);
                            Console.WriteLine("name = {0}", chird.finmino.name);
                            Console.WriteLine("Tspin = {0}", chird.finmino.Tspin);
                            Console.WriteLine("isb2bclear = {0}", chird.Board.isb2bclear);

                            Console.WriteLine("nodebattlescore = {0}", bot.evalweight.evalbattle(chird));
                            Console.WriteLine(chird.score);
                            Console.WriteLine("maxdepth = {0}", chird.maxdepth);
                            //Console.WriteLine("scoreex = {0}", chird.scoreex);

                            //Console.WriteLine(chird.finmino.minopos.x);
                            //Console.WriteLine(chird.finmino.minopos.y);
                            //Console.WriteLine(chird.finmino.stat);
                            Console.WriteLine(chird.finmino.name);
                            //foreach (int a in chird.Board.column_height)
                            //{
                            //    Console.Write(a);
                            //    Console.Write(" ");
                            //}
                            //Console.WriteLine("");
                            chird.finmino.console_print();
                            if (chird.holdpiece != -1)
                            {
                                defaultop.demino.getmino(chird.holdpiece).console_print();
                            }
                            double kk = 0;
                            //eval.evalhole(chird, chird.Board.column_height, 0, ref kk);
                            //chird.res.print();
                            //char a1 = Console.ReadKey().KeyChar;
                        }
                    }
                        tree root = bot.requset_next_move();
                    if (root.isdead) { Console.WriteLine(root.pieceidx); ; break; };
                    //ans.Enqueue(root);
                    //if (ans.Count > 200) ans.Dequeue();
                    //eval.evalfield(root);
                    //Console.Clear();
                    //root.Board.console_print(true, root.finmino);
                    //if (root.pieceidx % 1 == 0 || root.score < -1000000)
                    {
                        root.Board.console_print(false);
                        Console.WriteLine("resroot");
                        Console.WriteLine("atk = {0}", root.attack);
                        Console.WriteLine("maxattack = {0}", root.maxattack);
                        Console.WriteLine("def = {0}", root.def);
                        Console.WriteLine("maxdef = {0}", root.maxdef);
                        Console.WriteLine("atkscore = {0}", root.atkscore);
                        Console.WriteLine("battlescore = {0}", root.battlescore);
                        Console.WriteLine("combo = {0}", root.Board.combo);
                        Console.WriteLine("clearrow = {0}", root.Board.clearrow);
                        Console.WriteLine("movetime = {0}", root.finmino.path.movetime);
                        Console.WriteLine("name = {0}", root.finmino.name);
                        Console.WriteLine("Tspin = {0}", root.finmino.Tspin);
                        Console.WriteLine("isb2bclear = {0}", root.Board.isb2bclear);
                        Console.WriteLine("nodebattlescore = {0}", bot.evalweight.evalbattle(root));
                        Console.WriteLine(root.score);
                        Console.WriteLine("maxdepth = {0}", root.maxdepth);
                        //Console.WriteLine("scoreex = {0}", root.scoreex);
                        //Console.WriteLine(bot.nodequeue.Count);
                        //Console.WriteLine(root.pieceidx);
                        //Console.WriteLine(root.Board.piece.name);
                        //root.res.print();
                        //foreach (int a in root.Board.column_height)
                        //{
                        //    Console.Write(a);
                        //    Console.Write(" ");
                        //}

                    }

                    //foreach (int a in root.Board.column_height)
                    //{
                    //    Console.Write(a);
                    //    Console.Write(" ");
                    //}
                    Console.WriteLine("");
                    //root.res.print();
                    Console.WriteLine("-----------------------------------------------");
                    root.finmino.console_print();
                    if (root.holdpiece != -1)
                    {
                        defaultop.demino.getmino(root.holdpiece).console_print();
                    }
                    // char a1 = Console.ReadKey().KeyChar;

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

                //foreach (tree root in ans)
                //{
                //    root.Board.console_print(false);
                //    Console.WriteLine("resroot");
                //    Console.WriteLine(root.score);
                //    Console.WriteLine(bot.nodequeue.Count);
                //    Console.WriteLine(root.pieceidx);
                //    Console.WriteLine(root.Board.piece.name);
                //    root.res.print();
                //}

            }
        }
    }
}
