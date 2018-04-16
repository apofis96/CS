using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleLab2b
{
    class Program
    {
        static long quo = 0b0000000000000000000000000000000011111111111111111111111111111111;
        static long rem = 0b0111111111111111111111111111111100000000000000000000000000000000;
        static void Main(string[] args)
        {
            uint dividend, divisor;
            Console.Write("dividend =");
            dividend = Convert.ToUInt32(Console.ReadLine());
            Console.Write("divisor =");
            divisor = Convert.ToUInt32(Console.ReadLine());
            long ii = Division(dividend, divisor);
            int i = (int)(ii & quo);
            int k = (int)(ii >> 32);
            Console.WriteLine("quo = " + i);
            Console.WriteLine("rem = " + k);

        }
        static long Division(uint dvd, uint dvr)
        {
            long reg = 0;
            //Console.WriteLine(dvd + " dvd");
            reg = dvd;
            Console.WriteLine("Initial reg: " + (Convert.ToString((long)(reg), 2)).PadLeft(64, '0'));
            //Console.WriteLine(reg + " reg");
            for (int i = 0; i < 32; i++)
            {
                Console.WriteLine("Step " + (i+1));
                //Console.WriteLine(reg + " reg");
                reg <<= 1;
             //   Console.WriteLine(reg + " reg");
             //   Console.WriteLine(Convert.ToString((uint)(reg>>32), 2));
             //   Console.WriteLine(Convert.ToString((uint)(reg & quo), 2));
                int remm = 0;
                remm = (int)((reg & rem) >>32);
                //Console.WriteLine(remm + " remm");
                long temp = (remm - dvr);
                Console.WriteLine("Difference: " + temp);
                if (temp >= 0)
                {
                    Console.WriteLine("Diff >=0 !!!");
                    reg &= quo;
                    reg |= (temp << 32);
                    reg++;
                }
                Console.WriteLine("reg: "+ (Convert.ToString((long)(reg), 2)).PadLeft(64, '0'));
                Console.WriteLine("rem: "+ (Convert.ToString((uint)(reg >> 32), 2)).PadLeft(32, '0'));
                Console.WriteLine("quo: "+ (Convert.ToString((uint)(reg & quo), 2)).PadLeft(32, '0'));
                Console.WriteLine("====================");

            }
            return reg;
        }
    }
}
