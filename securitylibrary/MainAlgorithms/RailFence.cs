using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RailFence : ICryptographicTechnique<string, int>
    {
        public int Analyse(string plainText, string cipherText)
        {
            int key = 0;
            int j = 1;
            int i = 1;
            bool check = false;

            for (; j < plainText.Length; j++)
            {
                key = 0;

                if (plainText[j] == Char.ToLower(cipherText[i]))
                {
                    key = j ;
                    int new_j = j + key;
                    if (new_j >= plainText.Length)
                        new_j = 1;
                   
                    int c = i+1;
                    
                    while (c < cipherText.Length && new_j < plainText.Length)
                    {
                        if (Char.ToLower(cipherText[c]) == plainText[new_j])
                        {
                            c++;
                            new_j += key;
                            check = true;

                        }
                        else
                        {
                            check = false;
                            break;
                        }
                       
                    }

                    if (check)
                        break;

                }
            }

            return key;
        }

        public string Decrypt(string cipherText, int key)
        {
            int row = key;
            int column;

            if (cipherText.Length % key == 0)
            {
                column = cipherText.Length / key;
            }

            else
            {
                column = (cipherText.Length / key) + 1;
            }

            char[,] plaintext = new char[row, column];
            int indexofcipher = 0;
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < column; c++)
                {
                    plaintext[r, c] = Char.ToLower(cipherText[indexofcipher]);
                    indexofcipher++;

                    if (indexofcipher == cipherText.Length)
                        break;
                }

                if (indexofcipher == cipherText.Length)
                    break;
            }

            string cipher = "";

            for(int c=0;c<column;c++)
            {
                for(int r=0;r<row;r++)
                {
                    if(plaintext[r,c]!='\0')
                    cipher += plaintext[r, c];
                }
            }

            return cipher;
        }

        public string Encrypt(string plainText, int key)
        {
           
            int row = key;
            int column;

            if(plainText.Length%key==0)
            {
                column = plainText.Length / key;
            }

            else
            {
                column = (plainText.Length / key) + 1;
            }

            char[,] ciphertext = new char [row, column];
            int indexofplain = 0;

            for (int c = 0; c < column; c++)
            {
                for (int r = 0; r < row; r++)
                {
                    ciphertext[r, c] = Char.ToUpper(plainText[indexofplain]);
                    indexofplain++;

                    if (indexofplain == plainText.Length)
                        break;
                }

                if (indexofplain == plainText.Length)
                    break;
            }

            string cipher = "";
            for(int r=0;r<row;r++)
            {
                for(int c=0;c<column;c++)
                {
                    if (ciphertext[r, c] == '\0')
                        continue;

                    cipher += ciphertext[r, c];
                }
            }


            return (cipher);

        }
    }
}
