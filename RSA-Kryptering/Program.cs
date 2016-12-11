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

        static void encrypt(string secretText)
        {
            //Først sættes de to primtal p og q.
            p = 37;
            q = 89;

            //Dernæst bestemmes n
            n = p * q;

            //Herefter beregnes φ(n) = (p-1)(q-1). - φ(n) = phi
            phi = (p - 1)*(q - 1);

            //Så vælges variablen e, hvor 0 < e < φ(n) og (e, φ(n)) = 1
            e = 25;

            //Beregn d. - e * d ≡ 1 (mod φ(n))
            d = modInv(e,phi);

            //Konverter teksten til tal
            convertToInt(secretText);

            //Separer ind i blokke mindre end n
            for (int i = 0; i < convertedText.Length-1; i++)
            {
                convertedTextBlocks[i] = convertedText.Substring(i*3,3);
            }
            convertedTextBlocks[convertedText.Length - 1] = convertedText.Substring(3);

            for (int i = 0; i< convertedTextBlocks.Length; i++)
            {
                Console.WriteLine(convertedTextBlocks[i]);
            }
        }

        public static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int tmp = b;
                b = a % b;
                a = tmp;
            }

            return a;
        }

        public static int modInv(int a, int n)
        {
            int t = 0, nt = 1, r = n, nr = a % n;

            if(n < 0)
            {
                n = -n;
            }

            if (a < 0)
            {
                a = n - (-a % n);
            }

            while (nr != 0)
            {
                int quot = (r/nr) | 0;
                int temp = nt;
                nt = t - quot * nt;
                t = temp;
                temp = nr;
                nr = r - quot * nr;
                r = temp;

            }
            if (r > 1)
            {
                return -1;
            }
            if (t < 0)
            {
                t += n;
            }

            return t;
        }

        static void convertToInt(string secretText)
        {
            char[] text = secretText.ToArray();
            char[] chars = "abcdefghijklmnopqrstuvwxyzæøåABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅ".ToArray();
            for (int i = 0; i < text.Length; i++)
            {
                int index =  Array.IndexOf(chars, text[i]) + 1;
                if (index < 10)
                {
                    convertedText += "0" + index;
                }
                else {
                    convertedText += index;
                }
            }
        }
                
    }
}
