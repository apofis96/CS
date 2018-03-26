using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLab1
{
    class Program
    {
        static void Main(string[] args)
        {   // Ё для переноса строки. Ы для необработанных. Э сумма 
            bool flag = true;
            while (flag)
            {
                flag = false;
                string alef = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZzАаБбВвГгҐґДдЕеЄєЖжЗзИиІіЇїЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЬьЮюЯя0123456789 '\\\"!@#№$%^&*()_+-=/,.<>?[]{}~ЁЫ";
                Dictionary<char, double> chars = new Dictionary<char, double>();
                Console.Write("Путь к файлу: ");
                string str = @Console.ReadLine();

                StreamReader sr = File.OpenText(str);
                
                /* while (!sr.EndOfStream)
                 {
                     str = sr.ReadLine();
                     foreach (char c in str)
                     {
                         Console.Write(c + " ");
                     }
                 }*/
                FillDictionary(chars, alef);
                EvalChar(chars, sr);
                EvalFric(chars, alef);
                Console.WriteLine("Частоты появления символов в тексте:");
                foreach (char c in chars.Keys)
                {
                    
                    if (c != 'Ё' && c != 'Ы' && c != 'Э')
                    {
                        
                        Console.Write(c);
                        Console.WriteLine(": " + chars[c]);
                    }
                    else if (c == 'Ё')
                    {
                        Console.WriteLine("Переносов строки: " + chars[c]);
                    }
                    else if (c == 'Ы')
                    {
                        Console.WriteLine("Символов вне алфавита: " + chars[c]);
                    }


                }
                /*EvalFric(chars, alef);
                foreach (char c in chars.Keys)
                {
                    Console.Write(c);
                    Console.WriteLine(" " + chars[c]);

                }*/
                double en = EvalEntropi(chars, alef);
                Console.WriteLine("Энтропия: "+ en);
                Console.WriteLine("Количество информации: " + en * chars['Э'] + "bit ("+ (en * chars['Э']) / 8 + "byte)" );
                Console.WriteLine("Длинна файла: "+ sr.BaseStream.Length+"byte");
                Console.WriteLine("===================================================");
                Console.WriteLine("Повторить? (y/n)");
                if (Console.ReadLine() == "y")
                {
                    flag = true;
                }
            }
        }
        static void FillDictionary(Dictionary<char, double> chars, string alef)
        {
            foreach (char c in alef)
            {
                chars.Add(c, 0);
            }
            chars.Add('Э', 0);

        }
        static void EvalChar(Dictionary<char, double> chars, StreamReader sr)
        {
            string str;
            while (!sr.EndOfStream)
            {
                str = sr.ReadLine();
                foreach (char c in str)
                {
                    try
                    {
                        chars[c]++;
                    }
                    catch (Exception)
                    {
                        //Console.Write(c);
                        chars['Ы']++;
                    }
                    finally { chars['Э']++; }
                }
                chars['Ё']++;
                chars['Э']++;
            }

        }
        static void EvalFric(Dictionary<char, double> chars, string alef)
        {
            foreach (char c in alef)
            {
                chars[c] = chars[c] / chars['Э'];
            }
        }
        static double EvalEntropi(Dictionary<char, double> chars, string alef)
        {
            double entropi = 0.0;
            foreach(char c in alef)
            {
                if (chars[c] != 0)
                {
                    entropi -= chars[c] * Math.Log(chars[c], 2);
                }
                //Console.WriteLine(entropi);
            }
            return entropi;
        }
    }
}
