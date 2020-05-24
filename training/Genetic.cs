using System;
using System.Collections.Generic;
using System.Text;
using Jura_Knife_Tetris;

namespace optimizer
{
    class Genetic
    {

        public static weights generate()
        {
            weights W = new weights();
            Random rnd = new Random();
            W.height = (new int[]{ rnd.Next(-9999,9999), rnd.Next(-9999, 9999), rnd.Next(-9999, 9999),
                rnd.Next(-9999, 9999), rnd.Next(-9999, 9999) });
            W.clear = new int[] { 0, rnd.Next(-9999, 9999), rnd.Next(-9999, 9999), rnd.Next(-9999, 9999),
            rnd.Next(-9999, 9999) }; // 1 2 3 4 // combo时也许不一样
            W.tspin = new int[] { rnd.Next(-9999, 9999), rnd.Next(-9999, 9999), rnd.Next(-9999, 9999), rnd.Next(-9999, 9999) }; // mini 1 2 3
            W.wide = rnd.Next(-9999, 9999);
            W.b2b = rnd.Next(-9999, 9999);
            W.b2b_clear = rnd.Next(-9999, 9999);
            W.wastedT = rnd.Next(-9999, 9999);
            W.tslot = new int[4]; // mini 1 2 3
            W.movetime = -3; // 操作数
                             //W. tslotnum; // t坑数目jla
            W.holdT = rnd.Next(-9999, 9999);
            W.holdI = rnd.Next(-9999, 9999);
            W.perfectclear = rnd.Next(-9999, 9999);
            W.bus = rnd.Next(-9999, 9999);
            W.bus_sq = rnd.Next(-9999, 9999);
            W.fewcombo = rnd.Next(-9999, 9999);
            W.lotcombo = rnd.Next(-9999, 9999); // maybe combo table
            W.maxdef = rnd.Next(-9999, 9999); // 最高防御垃圾行
            W.attack = rnd.Next(-9999, 9999); // 攻击
            W.downstack = rnd.Next(-9999, 9999);
            W.deephole = rnd.Next(-9999, 9999);
            W.deephole2 = rnd.Next(-9999, 9999);
            W.deephole3 = rnd.Next(-9999, 9999);
            W.deltcol = rnd.Next(-9999, 9999);
            W.safecost = rnd.Next(-9999, 9999);
            W.parity = rnd.Next(-9999, 9999);
            W.dephigh = rnd.Next(-9999, 9999);
            W.linefull = rnd.Next(-9999, 9999); // -5
            W.col_minhigh = new int[] { rnd.Next(-9999, 9999), rnd.Next(-9999, 9999),
            rnd.Next(-9999, 9999), rnd.Next(-9999, 9999), rnd.Next(-9999, 9999),
            rnd.Next(-9999, 9999), rnd.Next(-9999, 9999), rnd.Next(-9999, 9999),
            rnd.Next(-9999, 9999), rnd.Next(-9999, 9999) };
            return W;
        }

        public static weights crossover(weights p1, weights p2)
        {
            Random rnd = new Random();
            weights chird = new weights();
            chird.height = (new int[]{ 
                crossover_gene(p1.height[0], p2.height[0]),
                crossover_gene(p1.height[1], p2.height[1]),
                crossover_gene(p1.height[2], p2.height[2]),
                crossover_gene(p1.height[3], p2.height[3]), 
                crossover_gene(p1.height[4], p2.height[4]) });
            chird.clear = new int[] { 0, 
                crossover_gene(p1.clear[1], p2.clear[1]), 
                crossover_gene(p1.clear[2], p2.clear[2]), 
                crossover_gene(p1.clear[3], p2.clear[3]),
                crossover_gene(p1.clear[4], p2.clear[4]) }; // 1 2 3 4 // combo时也许不一样
            chird.tspin = new int[] {
                crossover_gene(p1.tspin[0], p2.tspin[0]),
                crossover_gene(p1.tspin[1], p2.tspin[1]),
                crossover_gene(p1.tspin[2], p2.tspin[2]),
                crossover_gene(p1.tspin[3], p2.tspin[3])
                 }; // mini 1 2 3
            chird.wide = crossover_gene(p1.wide, p2.wide);
            chird.b2b = crossover_gene(p1.b2b, p2.b2b);
            chird.b2b_clear = crossover_gene(p1.b2b_clear, p2.b2b_clear);
            chird.wastedT = crossover_gene(p1.wastedT, p2.wastedT);
            chird.tslot = new int[4]; // mini 1 2 3
            chird.movetime = -3; // 操作数
                             //chird. tslotnum; // t坑数目jla
            chird.holdT = crossover_gene(p1.holdT, p2.holdT);
            chird.holdI = crossover_gene(p1.holdI, p2.holdI);
            chird.perfectclear = crossover_gene(p1.perfectclear, p2.perfectclear);
            chird.bus = crossover_gene(p1.bus, p2.bus);
            chird.bus_sq = crossover_gene(p1.bus_sq, p2.bus_sq);
            chird.fewcombo = crossover_gene(p1.fewcombo, p2.fewcombo);
            chird.lotcombo = crossover_gene(p1.lotcombo, p2.lotcombo); // maybe combo table
            chird.maxdef = crossover_gene(p1.maxdef, p2.maxdef); // 最高防御垃圾行
            chird.attack = crossover_gene(p1.attack, p2.attack); // 攻击
            chird.downstack = crossover_gene(p1.downstack, p2.downstack);
            chird.deephole = crossover_gene(p1.deephole, p2.deephole);
            chird.deephole2 = crossover_gene(p1.deephole2, p2.deephole2);
            chird.deephole3 = crossover_gene(p1.deephole3, p2.deephole3);
            chird.deltcol = crossover_gene(p1.deltcol, p2.deltcol);
            chird.safecost = crossover_gene(p1.safecost, p2.safecost);
            chird.parity = crossover_gene(p1.parity, p2.parity);
            chird.dephigh = crossover_gene(p1.dephigh, p2.dephigh);
            chird.linefull = crossover_gene(p1.linefull, p2.linefull); // -5
            chird.col_minhigh = new int[] { 
                crossover_gene(p1.col_minhigh[0], p2.col_minhigh[0]), 
                crossover_gene(p1.col_minhigh[1], p2.col_minhigh[1]), 
                crossover_gene(p1.col_minhigh[2], p2.col_minhigh[2]),
                crossover_gene(p1.col_minhigh[3], p2.col_minhigh[3]),
                crossover_gene(p1.col_minhigh[4], p2.col_minhigh[4]),
                crossover_gene(p1.col_minhigh[5], p2.col_minhigh[5]),
                crossover_gene(p1.col_minhigh[6], p2.col_minhigh[6]),
                crossover_gene(p1.col_minhigh[7], p2.col_minhigh[7]),
                crossover_gene(p1.col_minhigh[8], p2.col_minhigh[8]),
                crossover_gene(p1.col_minhigh[9], p2.col_minhigh[9]),};
            return chird;
        }

        public static int crossover_gene(int p1,int p2)
        {
            Random r = new Random();
            int rand = r.Next(0, 100);
            int res = 0;
            if (rand < 41) res = p1;
            else if (rand < 82) res = p2;
            else if (rand < 98) res = (p1 + p2) / 2;
            if (res > 9999) return 9999;
            else if (res < -9999) return -9999;
            else return res;
        }
    }
}
