using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string alphabet = "абвгґдеєжзиіїйклмнопрстуфхцчшщьюя ";
            Dictionary<char, int> chars = new Dictionary<char, int>();
            Console.Write("Путь к файлу: ");
            string str = @Console.ReadLine();

            StreamReader sr = File.OpenText(str);

            foreach(char c in alphabet)
            {
                chars.Add(c, 0);
            }

            double amountOfChars = CountChars(chars, sr);
            Dictionary<char, double> charFrequency;
            charFrequency = CountFrequency(chars, amountOfChars, alphabet);
            charFrequency.OrderBy(i => i.Value);

            Console.WriteLine("Частоты появления символов в тексте:");
            foreach (char c in charFrequency.Keys)
            {

                if (c != '\n')
                {
                    Console.Write(c);
                    Console.WriteLine(": {0:f10}", charFrequency[c]);
                }
                else
                {
                    Console.WriteLine("Переносов строки: " + charFrequency[c]);
                }

            }

            Console.WriteLine("Частоты появления символов в тексте:");
            string frequentChars = "";
            foreach (char c in from pair in charFrequency
                                           orderby pair.Value descending
                                           select pair.Key)
            {

                if (c != '\n')
                {
                    Console.Write(c);
                    Console.WriteLine(": {0:f10}",charFrequency[c]);
                    frequentChars += c;
                }
                else
                {
                    Console.WriteLine("Переносов строки: " + charFrequency[c]);
                }

            }
            Console.WriteLine(frequentChars);
            double entrophy = CountEntrophy(charFrequency, alphabet);
            Console.WriteLine("Энтропия: " + entrophy);
            Console.WriteLine("Количество информации: " + entrophy * amountOfChars + " b (" + (entrophy * amountOfChars) / 8 + " B)");
            Console.WriteLine("Длинна файла: " + sr.BaseStream.Length + " B");

            Console.ReadKey();
        }
        static int CountChars(Dictionary<char, int> chars, StreamReader sr)
        {
            int counter = 0;
            string str;
            while (!sr.EndOfStream)
            {
                str = sr.ReadLine().ToLower();
                foreach (char c in str)
                {
                    try
                    {
                        chars[c]++;
                    }
                    catch
                    {
                        
                    }
                }
                counter += str.Length;
            }
            return counter;
        }
        static Dictionary<char, double> CountFrequency(Dictionary<char, int> chars, double amountOfChars, string alphabet)
        {
            Dictionary<char, double> frequency = new Dictionary<char, double>();
            foreach (char c in alphabet)
            {
                frequency.Add(c, chars[c] / amountOfChars);
            }

            return frequency;
        }
        static double CountEntrophy(Dictionary<char, double> chars, string alef)
        {
            double entrophy = 0.0;
            foreach (char c in alef)
            {
                if (chars[c] != 0)
                {
                    entrophy -= chars[c] * Math.Log(chars[c], 2);
                }
            }
            return entrophy;
        }
    }
}
