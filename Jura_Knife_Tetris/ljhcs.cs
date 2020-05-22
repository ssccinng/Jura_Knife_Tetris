using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{
    class ljhcs
    {
        game a;
        public void run()
        {
            a = new game();
            //a.init();
            a.bot_init();
            for (int i = 0; i < 100; ++i)
            {
                a.bot_run();
                if (i % 10 == 0)
                    a.garbage_queue.Push(1);
                a.deal_garbage();
                a.Board.console_print(false);
                //a.bot.boardtree.Board.console_print(false);
                char a1 = Console.ReadKey().KeyChar;
            }
        }
    }
}
