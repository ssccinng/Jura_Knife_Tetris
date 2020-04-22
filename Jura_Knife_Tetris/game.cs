using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{
    class game
    {
        public board Board;
        Stack<int> garbage_queue;
        Stack<int> attacking;
        rule gamerule;

        Juraknifecore bot;

        public int lock_piece_calc()
        {
            Board.piece.mino_lock(ref Board);

            bool isb2b = Board.isb2b;

            int row = Board.clear_full();
            int atk = 0;
            if (Board.isperfectclear) atk += 6;
            if (Board.piece.Tspin)
            {
                atk += gamerule.GetTspindmg(row);
                
            }
            else
            {
                atk += gamerule.Getcleardmg(row);
            }
            atk += gamerule.Getrendmg(Board.combo);

            if (isb2b)
                atk += gamerule.Getb2bdmg(row);
            if (Board.piece.mini && row == 1) atk -= 1;
            if (atk > 0)
                attacking.Push(atk);
            //if (Board.piece.isTspin())
            //Board.cl
            //int clear

            return atk;
        }

        void deal_garbage()
        {
            int garbage = 0;
            foreach(int i in garbage_queue)
            {
                garbage += i;
            }
            int atk = 0;
            foreach (int i in attacking)
            {
                atk += i;
            }

            if (atk > garbage)
            {
                garbage_queue.Clear();
                while (garbage > attacking.Peek())
                {
                    garbage -= attacking.Pop();
                    
                }
                if (garbage != 0)
                    attacking.Push(attacking.Pop() - garbage);


            }
            else
            {
                attacking.Clear();
                while (atk > garbage_queue.Peek())
                {
                    atk -= garbage_queue.Pop();

                }
                if (atk != 0)
                    garbage_queue.Push(garbage_queue.Pop() - atk);
            }

            if (Board.combo > 0) return;

            if (atk > 0) Board.add_garbage(atk);

        }


        public void Gamestart()
        {
            board F = new board(new mino_gene(), new TopGarbage(), 5);

            Board = F;
            attacking = new Stack<int>();
            garbage_queue = new Stack<int>();
            gamerule = new rule(new int[] { 0, 1, 1, 2, 1 },
            new int[] { 0, 2, 4, 6 }, new int[] {0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 4, 5, 5 }, new int[] { 0, 0, 1, 2, 4 });
            int atk = 0;
            while (!F.isdead)
            {
                F.Spawn_piece();
                //b.setpos(18, 3);
                //F.console_print(true, F.piece);
                //F.piece = b;
                
                while (!F.piece.locked)
                {
                    Console.Clear();

                    F.console_print(true, F.piece);
                    if (F.holdpiece != null)
                        F.holdpiece.console_print();
                    Console.Write(atk);
                    atk = 0;
                    if (F.piece.Tspin)
                    {
                        Console.WriteLine("tspin");
                        //if (F.piece.mini && row == 1)
                        //    Console.WriteLine("mini");
                    }
                    char a = Console.ReadKey().KeyChar;

                    switch (a)
                    {
                        case 'a':
                            F.piece.left_move(ref F);
                            break;
                        case 'd':
                            F.piece.right_move(ref F);
                            break;
                        case 's':
                            F.piece.soft_drop(ref F);
                            break;
                        case 'w':
                            //F.piece.mino_lock(ref F);
                            atk = lock_piece_calc();
                            //F.add_garbage(1);
                            break;
                        case 'l':
                            F.piece.right_rotation(ref F);

                            break;
                        case 'k':
                            F.piece.left_rotation(ref F);
                            break;
                        case 'j':
                            
                            F.use_hold();
                            break;
                        default:
                            break;
                    }
                    
                }
            }
        }

        void place_next_piece()
        {

        }


    }
}
