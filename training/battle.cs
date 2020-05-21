using System;
using System.Collections.Generic;
using System.Text;
using Jura_Knife_Tetris;

namespace optimizer
{
    class battle
    {
        
        public void start()
        {
            game p1 = new game(), p2 = new game();
            p1.bot_init();
            p2.bot_init();
            while (!p1.isdead && !p2.isdead)
            {
                p1.bot_run();
                p2.bot_run();

                p1.garbage_queue = p2.attacking;
                p2.garbage_queue = p1.attacking;
                p1.deal_garbage();
                p2.deal_garbage();
            }


        }

        public void print()
        {
            for (int j = 22; j >= 0; --j)
            {
                for (int i = 0; i < 10; ++i)
                {

                }

                for (int i = 0; i < 10; ++i)
                {

                }
            }
            
        }

    }
}
