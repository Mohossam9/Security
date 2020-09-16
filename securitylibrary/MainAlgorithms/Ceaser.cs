using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {
        public string Encrypt(string plainText, int key)
        {
            string cipherText = string.Empty;

            int i = 0;
            while (i < plainText.Length)
            {
                if (!char.IsLetter(plainText[i]))
                {
                    continue;
                }

                char y = char.IsUpper(plainText[i]) ? 'A' : 'a';
                char z = (char)((plainText[i] + key) - y);
                char v = (char)((z % 26) + y);
                cipherText = cipherText + v;
                i++;
            }
            return cipherText;
        }

        public string Decrypt(string cipherText, int key)
        {
            int k = 26 - key;
            string plaintext = Encrypt(cipherText, k);
            return plaintext;
        }

        public int Analyse(string plainText, string cipherText)
        {
            char p = plainText[0];
            char c = cipherText[0];

            int key = (char.ToUpper(c)-65) - (Char.ToUpper(p)-65);

            if(key<0)
            {
                key += 26;
            }
            return key;
        }
    }
}