using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            char[] key = new char[26];
   
            for (int i = 0; i < cipherText.Length; i++)
            {
                char currentletter = (Char)cipherText[i];
                if (!key.Contains(Char.ToLower(currentletter)))
                {
                    int c_index = plainText[i] - 97;
                    key[c_index] = Char.ToLower((Char)cipherText[i]);
                }
            }


            char delimiter = '.';
            for (int i = 0; i < 26; i++)
            {
                if (key[i] == '\0')
                {
                    key[i] += delimiter;
                    delimiter++;
                }
            }

            return new string(key);
        }

        public string Decrypt(string cipherText, string key)
        {
 
            char[] plainarr = new char[cipherText.Length];
            String plain = "";
            int letterindex = -1;

            for (int i = 0; i < cipherText.Length; i++)
            {
                letterindex = key.IndexOf(Char.ToLower(cipherText[i])) + 97;
                plainarr[i] = (char)letterindex;
            }

            for (int j = 0; j < plainarr.Length; j++)
            {
                plain += plainarr[j];
            }

            return plain;
   
        }

        public string Encrypt(string plainText, string key)
        {
            char[] cipherarr = new char[plainText.Length];
            String cipher = "";
            int letterindex = -1;

            for (int i = 0; i < cipherarr.Length; i++)
            {
                letterindex= plainText[i] - 97;
                cipherarr[i] = key[letterindex];
            }

            for(int j=0;j<cipherarr.Length;j++)
            {
                cipher += cipherarr[j];
            }
         
            return cipher;

        }

        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	8.04
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        public string AnalyseUsingCharFrequency(string cipher)
        {
            String letters = "ETAOINSRHLDCUMFPGWYBVKXJQZ".ToLower();
            Queue<char> morerepeated = new Queue<char>();

            for (int i = 0; i < letters.Length; i++)
            {
                morerepeated.Enqueue(letters[i]);
            }

            char[] ciphertext = cipher.ToArray();
            char[] plaintext = new char[cipher.Length];
            Dictionary<char, int> repetation = new Dictionary<char, int>();

            for (int i = 0; i < cipher.Length; i++)
            {

                if (repetation.ContainsKey((Char)cipher[i]))
                    continue;

                int count = 0;
                for (int j = 0; j < cipher.Length; j++)
                {
                    if (cipher[i] == cipher[j])
                    {
                        count++;
                    }
                }
                repetation.Add((Char)cipher[i], count);
            }



            while (plaintext.Contains('\0'))
            {
                int max = 0;
                char letter = ' ';
                for (int i = 0; i < repetation.Count; i++)
                {
                    if (repetation.ElementAt(i).Value > max)
                    {
                        max = repetation.ElementAt(i).Value;
                        letter = repetation.ElementAt(i).Key;
                    }
                }
                repetation.Remove(letter);

                char currentletter = morerepeated.Dequeue();
                for (int j = 0; j < cipher.Length; j++)
                {
                    if ((Char)cipher[j] == letter)
                    {
                        plaintext[j] = currentletter;
                    }
                }
            }

            return new string(plaintext);
        }
    }
    }
