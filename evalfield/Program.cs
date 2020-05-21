using System;

namespace evalfield
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            while (true)
            {
                Jura_Knife_Tetris.evalresult qq = new Jura_Knife_Tetris.evalresult();

                Jura_Knife_Tetris.simpboard Board = new Jura_Knife_Tetris.simpboard();
                Jura_Knife_Tetris.tree node = new Jura_Knife_Tetris.tree();
                node.Board = new Jura_Knife_Tetris.board(null, null, 0).tosimple();
                for (int i = 20; i >= 0; --i)
                {
                    string a = Console.ReadLine();

                    for (int q = 1; q < a.Length; q += 2)
                    {
                        if (a[q] == '[')
                        {
                            node.Board.field[i, (q - 1) / 2] = true;
                        }
                    }
                }
                node.Board.column_height = node.Board.updatecol();
                qq = Jura_Knife_Tetris.eval.evalfield(node);
                qq.print();
            }
           
        }
    }
}
