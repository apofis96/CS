using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Part_2
{
    class Program
    {
        static int x = 2;
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Dictionary<string, int> xGrams = new Dictionary<string, int>();
            Console.Write("Путь к файлу: ");
            string str = @Console.ReadLine();

            StreamReader sr = File.OpenText(str);
            

            double amountOfChars = CountGrams(xGrams, PrepareString(str).Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries));
            Dictionary<string, double> x_GramsFrequency;
            x_GramsFrequency = CountFrequency(xGrams, amountOfChars);
            x_GramsFrequency.OrderBy(i => i.Value);

            Console.WriteLine("Частоты появления {0}-грамм в тексте:", x);
            string frequentx_Grams = "";
            int amountCounter = 0;
            foreach (string c in from pair in x_GramsFrequency
                               orderby pair.Value descending
                               select pair.Key)
            {
                Console.WriteLine("{0} : {1:f10}", c, x_GramsFrequency[c]);
                if (amountCounter < 30)
                {
                    frequentx_Grams += c + " ";
                    amountCounter++;
                }

            }
            Console.WriteLine(frequentx_Grams);

            Console.ReadKey();
        }
        static int CountGrams(Dictionary<string, int> x_Grams, string[] text)
        {
            int counter = 0;
            foreach(string word in text)
            {
                if (word.Length > x - 1)
                {
                    for (int i = 0; i < word.Length - (x - 1); i++)
                    {
                        counter++;
                        try
                        {
                            x_Grams[word.Substring(i, x)]++;
                        }
                        catch
                        {
                            x_Grams.Add(word.Substring(i, x), 1);
                        }
                    }
                }
            }
            return counter;
        }
        static Dictionary<string, double> CountFrequency(Dictionary<string, int> x_Grams, double amount)
        {
            Dictionary<string, double> frequency = new Dictionary<string, double>();
            foreach (string x_Gram in from x_Gram in x_Grams.Keys select x_Gram)
            {
                frequency.Add(x_Gram, x_Grams[x_Gram] / amount);
            }

            return frequency;
        }

        static string PrepareString(string address)
        {
            StreamReader sr = File.OpenText(address);
            string answ = "";
            string alphabet = "АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯабвгґдеєжзиіїйклмнопрстуфхцчшщьюя ";
            while (!sr.EndOfStream)
            {
                answ += sr.ReadLine();
            }
            answ = new string((
                    from c in answ
                    where alphabet.IndexOf(c) != -1
                    select c).ToArray()).ToLower();
            Console.WriteLine(answ);
            return answ;
        }
    }
}
