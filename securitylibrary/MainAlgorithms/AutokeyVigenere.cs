

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        public string generate(string plain, string key)
        {
            int x = key.Length;
            int y = plain.Length;
            int z = y - x;
            for (int i = 0; i < z; i++)
            {
                key += plain[i];

            }
            return key;

        }


        private static int Mod(int a, int b)
        {
            return (a % b + b) % b;
        }
        public string Analyse(string plainText, string cipherText)
        {
            string key = "";
         
            cipherText = cipherText.ToLower();
            for (int m = 0; m < plainText.Length; m++)
            {

                // int keyIndex = (m - count) % key.Length;
                int kk = plainText[m] - 'a';
                char ch = (char)((Mod(((cipherText[m] - kk) - 'a'), 26)) + 'a');
                key += ch;
                string t = Encrypt(plainText, key);
                if (t.Equals(cipherText))
                    break;

            }
            return key;
        }

        public string Decrypt(string cipherText, string key)
        {
            string enc = "";
            int count = 0;
            //      key = generate(cipherText, key);
            int z = cipherText.Length - key.Length;
            for (int i = 0; i < key.Length; i++)
            {
                bool input = char.IsUpper(cipherText[i]);
                if (input == true)
                {

                    int keyIndex = (i - count) % key.Length;
                    int kk = char.ToUpper(key[keyIndex]) - 'A';
                    char ch = (char)((Mod(((cipherText[i] - kk) - 'A'), 26)) + 'A');
                    enc += ch;

                }

                if (input == false)
                {

                    int keyIndex = (i - count) % key.Length;
                    int kk = char.ToLower(key[keyIndex]) - 'a';
                    char ch = (char)((Mod(((cipherText[i] - kk) - 'a'), 26)) + 'a');
                    enc += ch;
                }
            }

            for (int j = 0; j < z; j++)
            {
                key += enc[j];
                bool input = char.IsUpper(cipherText[key.Length - 1]);
                if (input == true)
                {

                    int keyIndex = key.Length;
                    int kk = char.ToUpper(key[keyIndex - 1]) - 'A';
                    char ch = (char)((Mod(((cipherText[key.Length - 1] - kk) - 'A'), 26)) + 'A');
                    enc += ch;

                }

                if (input == false)
                {

                    int keyIndex = key.Length;
                    int kk = char.ToLower(key[keyIndex - 1]) - 'a';
                    char ch = (char)((Mod(((cipherText[key.Length - 1] - kk) - 'a'), 26)) + 'a');
                    enc += ch;
                }
            }

            return enc;
        }

        public string Encrypt(string plainText, string key)
        {
            string enc = "";
            int count = 0;
            key = generate(plainText, key);
            for (int i = 0; i < plainText.Length; i++)
            {

                bool input = char.IsUpper(plainText[i]);
                if (input == true)
                {

                    int keyIndex = (i - count) % key.Length;
                    int kk = char.ToUpper(key[keyIndex]) - 'A';
                    char ch = (char)((Mod(((plainText[i] + kk) - 'A'), 26)) + 'A');
                    enc += ch;

                }

                if (input == false)
                {

                    int keyIndex = (i - count) % key.Length;
                    int kk = char.ToLower(key[keyIndex]) - 'a';
                    char ch = (char)((Mod(((plainText[i] + kk) - 'a'), 26)) + 'a');
                    enc += ch;

                }

            }
            return enc;
        }
    }
}
