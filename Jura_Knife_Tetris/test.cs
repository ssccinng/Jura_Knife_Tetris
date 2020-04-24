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
            bot.add_next(3);
            bot.init();
            bot.extend_node(bot.boardtree);

            foreach (tree a in bot.boardtree.treenode)
            {
                a.Board.console_print(false);
                char a1 = Console.ReadKey().KeyChar;
            }


        }
    }
}
