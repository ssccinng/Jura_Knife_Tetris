using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{
    class tree
    {
        int score;
        public List<tree> treenode;
        public bool isextend;
        int nowpiece;
        int garbage;
        int attack;
        simpboard Board; // maybe simple
        int Tspinslot;
        public bool holdT;
        public bool holdI;
        bool ishold;
        public int bestnodeindex;
        int maxdef; // 最大缩减高度+垃圾行值


        public bool findnextsol()
        {
            return false; // pass
        }


        public bool findalladd()
        {
            isextend = true;



        }



    }
}
