using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSSL;
using System.IO;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                System.Console.WriteLine("Please enter a numeric argument.");
                Environment.Exit(0);
            }
            Console.WriteLine("Encrypt = 0 | Decrypt = 1: ");
            int mode = int.Parse(Console.ReadLine());
            Console.WriteLine("key: "+args[0]);
            string s = args[0];
            if(s.Length>17 || s.Length==0)
            {
                Console.WriteLine("Too looooooooooooong! (or short)");
                Environment.Exit(0);
            }
            byte[] key = System.Text.Encoding.ASCII.GetBytes(s);
            Console.WriteLine("vector: "+args[1]);
            s = args[1];
            if (s.Length > 17 || s.Length == 0)
            {
                Console.WriteLine("Too looooooooooooong! (or short)");
                Environment.Exit(0);
            }
            byte[] vec = System.Text.Encoding.ASCII.GetBytes(s);
            Console.Write("path: ");
            s = @Console.ReadLine();
            OpenSSL.Crypto.CipherContext ctx = new OpenSSL.Crypto.CipherContext(OpenSSL.Crypto.Cipher.AES_128_CTR);
            String line;
            using (StreamReader sr = new StreamReader(s))
            {
                line = sr.ReadToEnd();
                //Console.WriteLine(line);
            }
            string answer = "mode error";
            if (mode == 1)
            {
                byte[] msg = Convert.FromBase64String(line);
                byte[] dec = ctx.Decrypt(msg, key, vec);
                answer = new string(System.Text.Encoding.ASCII.GetChars(dec));
                //Console.WriteLine(kak);
            }
            else if (mode == 0)
            {
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(line);
                byte[] enc = ctx.Encrypt(msg, key, vec);
                answer = Convert.ToBase64String(enc);
            }
            Console.WriteLine(answer);
            Console.ReadKey();

        }
        
        
    }
}
