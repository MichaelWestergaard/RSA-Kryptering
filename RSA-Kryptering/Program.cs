using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace RSA_Kryptering
{
    class Program
    {
        static int p;
        static int q;
        static int n;
        static int e;
        static int phi;
        static int d;

        public static string convertedText;
        public static string convertedTextBackwards;
        public static string decryptedText;
        public static string cipherText;
        public static List<int> cipherChars = new List<int>();
        public static List<BigInteger> cipherBlocks = new List<BigInteger>();
        public static List<string> convertedTextBlocks = new List<string>();

        public static char[] chars = "?????????? abcdefghijklmnopqrstuvwxyzæøåABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅ".ToArray();

        static void Main(string[] args)
        {
            Console.Write("Skriv en tekst der skal krypteres: ");
            string clearText = Console.ReadLine();

            createKeys();

            encrypt(clearText);

            decrypt();

            Console.WriteLine("Offentlig Nøgle = (" + n + ", " + e + ")");
            Console.WriteLine("Hemmelig Nøgle = " + d);
            Console.WriteLine("Konverteret Tekst = " + convertedText);
            Console.WriteLine("Ciffer Tekst = " + cipherText);
            Console.WriteLine("Dekrypteret Tekst = " + decryptedText);
            Console.ReadLine();

            Console.ReadLine();
        }

        static void createKeys()
        {
            //Først sættes de to primtal p og q.
            p = 23;
            q = 31;

            //Dernæst bestemmes n
            n = p * q;

            //Herefter beregnes φ(n) = (p-1)(q-1). - φ(n) = phi
            phi = (p - 1) * (q - 1);

            //Så vælges variablen e, hvor 0 < e < φ(n) og (e, φ(n)) = 1
            e = 7;

            //Beregn d. - e * d ≡ 1 (mod φ(n))
            int RES = 0;

            for (d = 1; ; d++)
            {
                RES = (d * e) % phi;
                if (RES == 1)
                {
                    break;
                }
            }
        }

        static void encrypt(string clearText)
        {

            //Konverter teksten til tal
            convertToInt(clearText);

            //Sæt ind i blokke
            for (int i = 0; i < convertedText.Length / 2; i++)
            {
                convertedTextBlocks.Add(convertedText.Substring(i * 2, 2));
            }

            foreach (var item in convertedTextBlocks)
            {
                BigInteger cipherBlock = BigInteger.Pow(BigInteger.Parse(item), e) % n;
                cipherText += cipherBlock;
                cipherBlocks.Add(cipherBlock);
            }

        }

        static void decrypt()
        {
            foreach (var item in cipherBlocks)
            {
                convertedTextBackwards += BigInteger.Pow(item, d) % n;
            }
            convertToChar(convertedTextBackwards);
        }

        static void convertToInt(string clearText)
        {
            char[] text = clearText.ToArray();
            for (int i = 0; i < text.Length; i++)
            {
                int index = Array.IndexOf(chars, text[i]) + 1;
                if (index < 10)
                {
                    convertedText += "0" + index;
                }
                else
                {
                    convertedText += index;
                }
            }
        }

        static void convertToChar(string cipherText)
        {
            for (int i = 0; i < cipherText.Length / 2; i++)
            {
                int block = Int32.Parse(cipherText.Substring(i * 2, 2));
                cipherChars.Add(block);
            }

            foreach (var item in cipherChars)
            {
                decryptedText += chars[item - 1];
            }

        }


    }
}