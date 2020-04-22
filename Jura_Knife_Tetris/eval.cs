using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{

    class evalresult
    {
        public bool value;
        public bool attackper;
        public bool defper;
        public int score;
        public bool clearinst;

        public evalresult()
        {
            
        }

    }
    static class eval
    {
        public static evalresult evalnode(tree node)
        {
            return new evalresult(); // pass
        }
    }
}
