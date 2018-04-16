using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace ConsoleLab2c
{
    class Program
    {
        static uint mantisa = 0b00000000011111111111111111111111;
        static uint exp = 0b01111111100000000000000000000000;
        static uint sign = 0b10000000000000000000000000000000;
        
        static void Main(string[] args)
        {
            float f1=9.75F;
            float f2= -0.05625F;
            Console.WriteLine("Input: ");
            Console.Write("f1= ");
           // f1 = Convert.ToSingle(Console.ReadLine());
            Console.Write("f2= ");
            // f2 = Convert.ToSingle(Console.ReadLine());
            Console.WriteLine("\n============================");
            float f3 = Add_floats(f1, f2);
            

            //Console.WriteLine("Answer: "+f3);


        }
        static void Check_numbers(ref float f1, ref float f2)
        {
            if (Math.Abs(f1) < Math.Abs(f2))
            {
                float temp;
                temp = f1;
                f1 = f2;
                f2 = temp;
            }
        }

        static unsafe uint FloatToUInt32Bits(float f)
        {
            return *((uint*)&f);
        }

        static unsafe float UInt32BitsToFloat(uint u)
        {
            return *((float*)&u);
        }





        static unsafe uint Add_floats(float f1, float f2)
        {
            Check_numbers(ref f1, ref f2);

            /*We assume that f1 has the larger absolute value of the 2 numbers. 
            Absolute value of of f1 should be greater than absolute value of f2,
            else swap the values such that ABS(f1) is greater than ABS(f2). 
            ABS(f1) > ABS(f2). */

            uint b_fpn1 = FloatToUInt32Bits(f1);
            uint b_fpn2 = FloatToUInt32Bits(f2);

            Console.WriteLine("f1= " + f1 + ":\n" + Convert.ToString(b_fpn1, 2).PadLeft(32, '0'));
            Console.WriteLine("f2= " + f2 + ":\n" + Convert.ToString(b_fpn2, 2).PadLeft(32, '0'));

            bool sign1 = (b_fpn1 >> 31) == 1 , sign2 = (b_fpn2 >> 31) == 1 ;
            uint exp1, exp2, exp3;
            uint mant1, mant2, mant3;

            exp1 = (b_fpn1 & exp) >> 23;
            exp2 = (b_fpn2 & exp) >> 23;

            mant1 = b_fpn1 & mantisa;
            mant2 = b_fpn2 & mantisa;
            Console.WriteLine("_____________________________");
            Console.WriteLine("Float-1\nSIGN: " + sign1 + "\nEXPONENT: " + exp1 + "\nBITS:\n" + Convert.ToString(exp1, 2).PadLeft(32, '0') + "\nMANTISSA: " + mant1 + "\nBITS:\n" + Convert.ToString(mant1, 2).PadLeft(32, '0') + "\n");
            Console.WriteLine("Float-2\nSIGN: " + sign2 + "\nEXPONENT: " + exp2 + "\nBITS:\n" + Convert.ToString(exp2, 2).PadLeft(32, '0') + "\nMANTISSA: " + mant2 + "\nBITS:\n" + Convert.ToString(mant2, 2).PadLeft(32, '0') + "\n");
            

            /*f1 and f2 can only be added if the exponents are the same i.e exp1=exp2.
            Initial value of the exponent should be the larger of the 2 numbers,
            since we know exponent of f1 will be bigger,
            hence Initial exponent result exp3 = exp1. */
            exp3 = exp1;

            //Calculate the exponent's difference i.e. exp_diff = (exp1-exp2). 
            uint exp_diff;
            exp_diff = exp1 - exp2;

            Console.WriteLine("exp_diff: " + exp_diff);
            /*Left shift the decimal point of mantissa (f2) by the exponent difference. 
            Now the exponents of both f1 and f2 are same.*/
            exp2 = exp1;
            mant2 >>= 1;
            mant2 |= 0b00000000010000000000000000000000;
            mant2 >>= (int)exp_diff;
            mant1 >>= 1;
            mant1 |= 0b00000000010000000000000000000000;
            Console.WriteLine("MANT after exp1==exp2:");
            Console.WriteLine("MANT1:\n" + Convert.ToString(mant1, 2));
            Console.WriteLine("MANT2:\n" + Convert.ToString(mant2, 2));

            /*Compute the sum / difference of the mantissas depending on the sign bit sign1 and sign2.
            If signs of f1 and f2 are equal(sign1 == sign2) then add the mantissas
            If signs of f1 and f2 are not equal(f1 != f2) then subtract the mantissas*/
            if (sign1 == sign2)
            {
                Console.WriteLine("Add mant");
                mant3 = mant1 + mant2;
            }
            else
            {
                Console.WriteLine("Sub mant");
                mant3 = mant1 - mant2;
            }
            Console.WriteLine("MANT3:\n" + Convert.ToString(mant3, 2));
            Console.WriteLine("EXP3 " + exp3);
            int e = /*(uint)Math.Abs(exp3 > 127 ? */(int)exp3 - 127;//: 127-exp3) ;
            //Console.WriteLine("E" + e);
            //e = Math.Abs(e);
            //bitset<MANTISSA> integer;
            Console.WriteLine("Normalize (if needed)");
            uint answer = 0;
            if (sign1) answer |= sign;
            //Console.WriteLine("rrrrrrrrrrrrr");
            while ((mant3 & exp) != 0)
            {
                mant3 >>= 1;
                exp3++;
                //Console.WriteLine("rrrrrrrrrrrrr");
            }
            while ((mant3 & exp) == 0)
            {
                //Console.WriteLine("lllllllllllllllllll");
                mant3 <<= 1;
                exp3--;
            }
            answer |= exp3+1 << 23;
            answer |= (mant3 & mantisa);
            Console.WriteLine("ANSWER: " + Convert.ToString(answer, 2).PadLeft(32, '0'));
            Console.WriteLine("DEC: " + UInt32BitsToFloat(answer));
            return answer;
        }



    }
}
