using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLab2a
{
    class Program
    {
        //Так как множим 8бит, то произведение должно быть 17бит... Но у нас С#, поэттому для наглядности обрезаем вывод до 17бит
        public static int bit8 = 0b00000000000000000000000011111111, bit17 = 0b00000000000000011111111111111111;

        static sbyte mpd, mpr;
        public static int Pdt, Ampd, Smpd;
        
        static void Main(string[] args)
        {
            int rrr = Multiplication();
            Console.WriteLine("Результат множення: " + rrr );
            Print_bits(rrr, bit17, 17);
        }

        static void Add(ref int reg, int A)
        {
            Console.WriteLine("ADD Product");
            reg += A;
            Print_bits(reg, bit17, 17);
        }

        static void Sub(ref int reg, int S)
        {
            Console.WriteLine("SUB Product");
            reg += S;
            Print_bits(reg, bit17, 17);
        }

        static void Shift(ref int reg)
        {
            Console.WriteLine("Shift Product");
            reg >>= 1;
            Print_bits(reg, bit17, 17);
            
        }

        static int Multiplication()
        {

            Console.WriteLine("░░░▒▒▒▓▓▓╣ АЛГОРИТМ БУТА ╠▓▓▓▒▒▒░░░\n");
            Console.WriteLine("mpd та mpr є 8бiтними числами зi знаком (дiапазон +/-127)\n");
            Console.Write("mpd = ");
            mpd = Convert.ToSByte(Console.ReadLine());
            Console.Write("В бiтах: ");
            Print_bits(mpd, bit8, 8);

            Console.Write("mpr = ");
            mpr = Convert.ToSByte(Console.ReadLine());
            Console.Write("В бiтах: ");
            Print_bits(mpr, bit8, 8);


            Ampd = 0;
            Ampd |= mpd << 9;
            Console.WriteLine("Перетворене mpd для ADD");
            Print_bits(Ampd, bit17, 17);


            Smpd = 0;
            Smpd = (~mpd + 1) << 9;
            Console.WriteLine("Перетворене mpd для SUB");
            Print_bits(Smpd, bit17, 17);


            Pdt = 0;
            Pdt |= mpr << 1;
            Pdt &= 0b00000000000000000000000111111111;
  
            Console.WriteLine("Product + mpr + 0");
            Print_bits(Pdt, bit17,17);

            Console.WriteLine("\n============\nРобота Бута:\n");

            for (int i = 0; i < 8; i++)
            {

                switch (Pdt & 3)
                {
                    case 0 | 3:
                        break;
                    case 1:
                        Add(ref Pdt, Ampd);
                        break;
                    case 2:
                        Sub(ref Pdt, Smpd);
                        break;
                }
                Shift(ref Pdt);
            }

            Shift(ref Pdt);
            return Pdt;
        }



        static void Print_bits(int reg, int bit, int pad)
        {
            Console.WriteLine(Convert.ToString(reg & bit, 2).PadLeft(pad, '0'));
            Console.WriteLine("__________");
        }

    }
}
