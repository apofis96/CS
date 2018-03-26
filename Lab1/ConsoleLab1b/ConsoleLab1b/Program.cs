using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLab1b
{
    class Program
    {
        static void Main(string[] args)
        {
            // string alef = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
            //Dictionary<BitArray, char> base64 = new Dictionary<BitArray, char>();
            // BinaryReader binReader = new BinaryReader(File.Open(@"D:\ForCS\ConsoleLab1b\1.txt", FileMode.Open));
            //StreamReader sr = File.OpenText(@"D:\ForCS\ConsoleLab1\1.txt");
            /* for (; ; )
             {
                 Console.Write(binReader.ReadByte()+ " ");
             }*/
            /*byte[] rrr = new byte[3];
            rrr = binReader.ReadBytes(3);
            //foreach (byte b in rrr)
            //{
            //    Console.Write(b + " ");
            //}
            Console.WriteLine(rrr[0] +" "+ rrr[1] +" "+ rrr[2]);
            //BitArray ba = new BitArray(rrr);
            //foreach (bool b in ba)
            //{
            //    Console.Write(b);
            //}
            int t = 0;
            t |= rrr[0];
            t <<= 8;
            t |= rrr[1];
            t <<= 8;
            t |= rrr[2];
            int mask = 16515072;
            int k = t & mask;
            k >>= 18;
            Console.WriteLine(k);
            binReader.Close();*/
            bool flag = true;
            while (flag)
            {
                flag = false;
                Console.Write("Путь к исходному файлу: ");
                string strIn = @Console.ReadLine();
                Console.Write("Путь к закодированому файлу: ");
                string strOut = @Console.ReadLine();
                ConvertToBase64(strIn, strOut);
            }
            

        }
        static void ConvertToBase64(string path, string writePath)
        {
            if (File.Exists(writePath)) { File.Delete(writePath); }
            using (File.Create(writePath)) ;
            BinaryReader binReader = new BinaryReader(File.Open(path, FileMode.Open));
            int[] iii = new int[4];
            int k = -1;
            int t = 0;
            int l = 0;
            int mask = 16515072;
            int shake = 18;
            while (binReader.BaseStream.Position != binReader.BaseStream.Length)
            {
                //Console.WriteLine(l++);
                t = 0;
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        t |= binReader.ReadByte();
                        t <<= 8;

                    }
                    catch (Exception)
                    {
                        k = i;
                        while(i<3)
                        {
                            t <<= 8;
                            i++;
                        }
                        //Console.WriteLine("Exc");
                        
                    }
                   
                }
                t >>= 8;
                //Console.WriteLine(t);
                mask = 16515072;
                shake = 18;
               // Console.WriteLine(t & mask);
                for(int i = 0; i<4; i++)
                {

                    iii[i] = (t & mask) >> shake;
                    mask >>= 6;
                    shake -= 6;
                   // Console.WriteLine(iii[i] + " " + i);
                }
                if (k != -1)
                {
                    k++;
                    while (k < 4)
                    {
                        iii[k] = 64;
                        k++;
                    }
                }

                WriteFile(writePath, iii);

            }
            binReader.Close();
        }

        static void WriteFile(string path, int[] iii)
        {
            StreamWriter text = new StreamWriter(File.Open(path, FileMode.Append), Encoding.ASCII);
            string alef = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
            text.Write(alef.ElementAt(iii[0]));
            //Console.WriteLine(iii[0]);
            text.Write(alef.ElementAt(iii[1]));
            text.Write(alef.ElementAt(iii[2]));
            text.Write(alef.ElementAt(iii[3]));
            text.Close();

        }
    }
}
