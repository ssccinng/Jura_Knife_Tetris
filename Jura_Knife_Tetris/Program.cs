using System;

namespace Jura_Knife_Tetris
{
    class Program
    {
        static void Main(string[] args)
        {

            game defaultgame = new game();
            defaultgame.Gamestart();

            //test t = new test();
            //t.run();




            //Random rand = new Random();
            ////Console.WriteLine("Hello World!");
            //board F = new board(new mino_gene(), new TopGarbage(), 5);
            //for (int i = 0; i < 100; ++i)
            //{
            //    //mino_gene minorule = new mino_gene();
            //    //mino b = minorule.getmino(rand.Next() % 7);
            //    //mino[] bt = minorule.bag7mino();
            //    //mino[] bt = { minorule.get_mino_wu(), minorule.get_mino_han()};
            //    //foreach (mino b in bt)
            //    //{

            //    //mino b = minorule.get_mino_wu();

            //    F.Spawn_piece();
            //    //b.setpos(18, 3);
            //    F.console_print(true, F.piece);
            //    //F.piece = b;

            //    while (!F.piece.locked)
            //    {
            //        char a = Console.ReadKey().KeyChar;

            //        switch (a)
            //        {
            //            case 'a':
            //                F.piece.left_move(ref F);
            //                break;
            //            case 'd':
            //                F.piece.right_move(ref F);
            //                break;
            //            case 's':
            //                F.piece.soft_drop(ref F);
            //                break;
            //            case 'w':
            //                F.piece.mino_lock(ref F);
            //                F.add_garbage(1);
            //                break;
            //            case 'l':
            //                F.piece.right_rotation(ref F);

            //                break;
            //            case 'k':
            //                F.piece.left_rotation(ref F);
            //                break;
            //            default:
            //                break;
            //        }
            //        Console.Clear();
            //        F.console_print(true, F.piece);
            //        int row = F.clear_full();
            //        if (F.piece.Tspin)
            //        {
            //            Console.WriteLine("tspin");
            //            if (F.piece.mini && row == 1)
            //                Console.WriteLine("mini");
            //        }

            //    }

            //}
        }

    }

}
