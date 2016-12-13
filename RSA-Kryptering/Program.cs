using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA_Kryptering
{
    class Program
    {
        static Random rand = new Random();

        static int p;
        static int q;
        static int n;
        static int e;
        static int phi;
        static int d;

        public static string convertedText;
        public static string[] convertedTextBlocks = new string[] { };

        static void Main(string[] args)
        {
            Console.Write("Skriv en tekst der skal krypteres: ");
            string secretText = Console.ReadLine();

            encrypt(secretText);

            Console.WriteLine("N = " + n);
            Console.WriteLine("Phi = " + phi);
            Console.WriteLine("E = " + e);
            Console.WriteLine("d = " + d);
            Console.WriteLine("convertedText = " + convertedText);
            Console.ReadLine();
        }

                        
    }
}
