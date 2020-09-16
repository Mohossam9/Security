using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RepeatingkeyVigenere : ICryptographicTechnique<string, string>
    {


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

            for (int i = 0; i < cipherText.Length; i++)
            {
                bool input = char.IsUpper(cipherText[i]);
                if (input == true)
                {

                    int Index = i % key.Length;
                    int kk = char.ToUpper(key[Index]) - 'A';
                    char ch = (char)((Mod(((cipherText[i] - kk) - 'A'), 26)) + 'A');
                    enc += ch;

                }

                if (input == false)
                {

                    int Index = i % key.Length;

                    int kk = char.ToLower(key[Index]) - 'a';

                    char ch = (char)((Mod(((cipherText[i] - kk) - 'a'), 26)) + 'a');
                    enc += ch;

                }

            }
            return enc;

        }
        private static int Mod(int a, int b)
        {
            return (a % b + b) % b;
        }

        public string Encrypt(string plainText, string key)
        {
            string enc = "";

            for (int i = 0; i < plainText.Length; i++)
            {
                bool input = char.IsUpper(plainText[i]);
                if (input == true)
                {

                    int Index = i % key.Length;
                    int kk = char.ToUpper(key[Index]) - 'A';
                    char ch = (char)((Mod(((plainText[i] + kk) - 'A'), 26)) + 'A');
                    enc += ch;

                }

                if (input == false)
                {
                    int Index = i % key.Length;
                    int kk = char.ToLower(key[Index]) - 'a';
                    char ch = (char)((Mod(((plainText[i] + kk) - 'a'), 26)) + 'a');
                    enc += ch;
                }

            }
            return enc;

        }
    }
}