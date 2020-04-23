﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Jura_Knife_Tetris
{
    class board
    {
        public int[,] field = new int[40, 10];

        public int[] column_height = new int[10];

        public bool isb2b = false;
        public bool isdead = false;
        public int combo = 0;
        public mino holdpiece = null;
        public mino piece = null;
        public mino_gene Minorule;
        public Queue<mino> Next_queue = new Queue<mino>();
        public int next_queue_size;
        public Garbagegene garbagerule;
        public int garbage_cnt;

        public board(mino_gene Minorule, Garbagegene garbagerule, int next_queue_size)
        {
            this.Minorule = Minorule;
            this.next_queue_size = next_queue_size;
            this.garbagerule = garbagerule;
            for (int i = 0; i < next_queue_size; ++i)
            {
                gene_next_piece();
            }
        }

        public void reset(int[,]field, bool isb2b, int combo)
        {
            this.field = field;
            this.isb2b = isb2b;
            this.combo = combo;
        }

        private bool check_mino_ok(int x, int y)
        {
            for (int i = 0; i < piece.height; ++i)
            {
                for (int j = 0; j < piece.weight; ++j)
                {
                    if (checkfield(i + x, j + y)
                        && piece.minofield[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool check_mino_ok(pos p)
        {
            for (int i = 0; i < piece.height; ++i)
            {
                for (int j = 0; j < piece.weight; ++j)
                {
                    if (checkfield(i + p.x, j + p.y)
                        && piece.minofield[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void Spawn_piece()
        {
            piece = Next_queue.Dequeue();
            piece.setpos(18, 3);
            bool isok = check_mino_ok(piece.minopos);
            isdead = !isok;
            gene_next_piece();
        }

        public bool isperfectclear
        {
            get
            {
                foreach (int i in field)
                {
                    if (i != 0) return false;
                }
                return true;
            }
        }

        //public Queue<int> garbage_sent;

        private void updatecol()
        {
            for (int i = 0; i < 10; ++i)
            {
                for (int h = 39; h > 0; --h)
                {
                    if (field[h, i] != 0)
                    {
                        column_height[i] = h;
                    } 
                }
            }
        }

        public int clear_full()
        {
            bool[] clearflag = new bool[40];
            int cntclear = 40;
            for (int i = 0; i < 40; ++i)
            {
                clearflag[i] = true;
                for (int j = 0; j < 10; ++j)
                {
                    if (field[i, j] == 0)
                    {
                        clearflag[i] = false;
                        cntclear--;
                        break;
                    }
                }
            }
            int index2 = 0;
            if (cntclear > 0)
            {
                if (cntclear == 4 || piece.Tspin)
                {
                    isb2b = true;
                }
                else
                {
                    isb2b = false;
                }

                combo += 1;
            }
            else
            {
                combo = 0;
            }

            for (int i = 0; i < 40; ++i)
            {
                while (index2 <40 && clearflag[index2]) index2++;
                copy_line(index2, i);
                index2++;
            }
            
            return cntclear;
        }
        public void clear_row(int row)
        {
            for (int i = 0; i < 10; ++i)
            {
                field[row, i] = 0;
            }
        }
        public void clear_col(int col)
        {
            for (int i = 0; i < 40; ++i)
            {
                field[i, col] = 0;
            }
        }

        public void copy_line(int source, int target)
        {
            if (target >= 40 || target < 0) return;
            if (source >= 40 || source < 0)
            {
                clear_row(target);
            }
            else
            {
                for (int i = 0; i < 10; ++i)
                {
                    field[target, i] = field[source, i];
                }
            }
        }
        public void all_clear()
        {
            for (int i = 0; i < 40; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    field[i, j] = 0;
                }
            }
        }
        public void console_print(bool printmino = true, mino m = null)
        {
            Console.WriteLine("\n+--------------------+");
            if (printmino && !m.locked)
            {
                for (int i = 0; i < m.height; ++i)
                {
                    for (int j = 0; j < m.weight; ++j)
                    {
                        if (m.minofield[i, j] != 0)
                            field[i + m.minopos.x, j + m.minopos.y] = 1;
                    }
                }
            }
            for (int i = 20; i >= 0; --i)
            {
                Console.Write("|");
                for (int j = 0; j < 10; ++j)
                {
                    if (field[i, j] != 0)
                    {
                        Console.Write("[]");
                    }
                    else
                    {
                        Console.Write(" +");
                    }
                }
                Console.WriteLine("|");
            }
            if (printmino && !m.locked)
            {
                for (int i = 0; i < m.height; ++i)
                {
                    for (int j = 0; j < m.weight; ++j)
                    {
                        if (m.minofield[i, j] != 0)
                            field[i + m.minopos.x, j + m.minopos.y] = 0;
                    }
                }
            }

            Console.WriteLine("+--------------------+\n");
        }
        public bool checkfield(int x, int y)
        {
            if ((x < 40 && x >= 0) && (y < 10 && y >= 0))
            {
                return field[x, y] != 0;
            }
            return true;
        }
        public bool checkfield(pos p)
        {
            if ((p.x < 40 && p.x >= 0) && (p.y < 10 && p.y >= 0))
            {
                return field[p.x, p.y] != 0;
            }
            return true;
        }
        public void gene_next_piece()
        {
            Next_queue.Enqueue(Minorule.genebag7mino());
        }
        public bool use_hold()
        {
            piece.reset();
            if (holdpiece == null)
            {
                holdpiece = piece;
                Spawn_piece();
                gene_next_piece();
            }
            else
            {
                mino temp = holdpiece;
                holdpiece = piece;
                piece = temp;
            }

            return true;
        }

        public bool add_garbage(Stack<int> garbage_queue)
        {
            int[,] garbage = garbagerule.Gene(garbage_queue);
            int addgarbage_cnt = garbage.GetLength(0);
            if (addgarbage_cnt == 0) return false;
            if (addgarbage_cnt >= 20)
            {
                isdead = true;
            } 
            for (int i = 39; i >= addgarbage_cnt; --i)
            {
                copy_line(i - addgarbage_cnt, i);
            }
            for (int i = 0; i < addgarbage_cnt; ++i)
            {
                for(int j = 0; j < 10; ++j)
                {
                    field[i, j] = garbage[i, j];
                }
            }
            return true;
        }

        public bool add_garbage(int addgarbage_cnt)
        {
            int[,] garbage = garbagerule.Gene(addgarbage_cnt);
            
            if (addgarbage_cnt == 0) return false;
            addgarbage_cnt = garbage.GetLength(0);
            if (addgarbage_cnt >= 20)
            {
                isdead = true;
            }
            for (int i = 39; i >= addgarbage_cnt; --i)
            {
                copy_line(i - addgarbage_cnt, i);
            }
            for (int i = 0; i < addgarbage_cnt; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    field[i, j] = garbage[i, j];
                }
            }
            return true;
        }


        public simpboard tosimple()
        {
            simpboard sBoard = new simpboard();
            sBoard.isb2b = isb2b;
            sBoard.combo = combo;
            sBoard.isb2b = isb2b;
            sBoard.isb2b = isb2b;

            for (int i = 0; i < 40; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    sBoard.field[i, j] = (field[i, j] != 0);
                }
            }
            return sBoard;

        }
    }


    class simpboard
    {
        public bool[,] field = new bool[40, 10];
        public bool isb2b = false;
        public bool isdead = false;
        public int combo = 0;
        public mino piece = null;
        public int garbage_cnt = 0;
        public bool isperfectclear
        {
            get
            {
                foreach (bool i in field)
                {
                    if (i) return false;
                }
                return true;
            }
        }


        public bool checkfield(int x, int y)
        {
            if ((x < 40 && x >= 0) && (y < 10 && y >= 0))
            {
                return field[x, y] != false;
            }
            return true;
        }
        public bool checkfield(pos p)
        {
            if ((p.x < 40 && p.x >= 0) && (p.y < 10 && p.y >= 0))
            {
                return field[p.x, p.y] != false;
            }
            return true;
        }
        private bool check_mino_ok(pos p)
        {
            for (int i = 0; i < piece.height; ++i)
            {
                for (int j = 0; j < piece.weight; ++j)
                {
                    if (checkfield(i + p.x, j + p.y)
                        && piece.minofield[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool check_mino_ok(int x, int y)
        {
            for (int i = 0; i < piece.height; ++i)
            {
                for (int j = 0; j < piece.weight; ++j)
                {
                    if (checkfield(i + x, j + y)
                        && piece.minofield[i, j] != 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public int clear_full()
        {
            bool[] clearflag = new bool[40];
            int cntclear = 40;
            for (int i = 0; i < 40; ++i)
            {
                clearflag[i] = true;
                for (int j = 0; j < 10; ++j)
                {
                    if (field[i, j] == false)
                    {
                        clearflag[i] = false;
                        cntclear--;
                        break;
                    }
                }
            }
            int index2 = 0;
            if (cntclear > 0)
            {
                if (cntclear == 4 || piece.Tspin)
                {
                    isb2b = true;
                }
                else
                {
                    isb2b = false;
                }

                combo += 1;
            }
            else
            {
                combo = 0;
            }

            for (int i = 0; i < 40; ++i)
            {
                while (index2 < 40 && clearflag[index2]) index2++;
                copy_line(index2, i);
                index2++;
            }

            return cntclear;
        }
        public void clear_row(int row)
        {
            for (int i = 0; i < 10; ++i)
            {
                field[row, i] = false;
            }
        }
        public void clear_col(int col)
        {
            for (int i = 0; i < 40; ++i)
            {
                field[i, col] = false;
            }
        }

        public void copy_line(int source, int target)
        {
            if (target >= 40 || target < 0) return;
            if (source >= 40 || source < 0)
            {
                clear_row(target);
            }
            else
            {
                for (int i = 0; i < 10; ++i)
                {
                    field[target, i] = field[source, i];
                }
            }
        }
        public void all_clear()
        {
            for (int i = 0; i < 40; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    field[i, j] = false;
                }
            }
        }


    }
}