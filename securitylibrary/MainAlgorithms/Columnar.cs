using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {
       
        public List<int> Analyse(string plainText, string cipherText)
        {
            int j = 0;
            int i = 0;
            int fixrow = 0;
            bool check = false;

            for (; j < plainText.Length; j++)
            {
                if (plainText[j] == Char.ToLower(cipherText[i]))
                {
                    int new_j = j + 1;
                    int c = i + 1;
                    int row = 1;

                    while (c < cipherText.Length && new_j < plainText.Length)
                    {
                        if (Char.ToLower(cipherText[c]) == plainText[new_j])
                        {
                            if (fixrow == 0)
                            {
                                fixrow = row;
                            }

                            if (row > fixrow)
                            {
                                check = false;
                                fixrow = 0;
                                break;
                            }
                            else if (row < fixrow)
                            {

                            }
                            else
                            {
                                check = true;
                                row = 0;
                                c++;
                            }
                        }
                        new_j++;
                        row++;
                    }
                }
                if (check)
                    break;
            }

            int column = fixrow;
            int row1;

            if (plainText.Length % column == 0)
                row1 = plainText.Length / column;
            else
                row1 = (plainText.Length / column) + 1;

            char[,] plain = new char[row1, column];
            int indexofplain = 0;

            for (int r = 0; r < row1; r++)
            {
                for (int c = 0; c < column; c++)
                {
                    plain[r, c] = plainText[indexofplain];
                    indexofplain++;

                    if (indexofplain == plainText.Length)
                        break;
                }
                if (indexofplain == plainText.Length)
                    break;
            }

            Dictionary<int, int> order = new Dictionary<int, int>();

            int indexofcipher = 0;
            int dic_index = 0;

            while (order.Count != column || indexofcipher < cipherText.Length)
            {
                for (int c = 0; c < column; c++)
                {
                    int index = indexofcipher;
                    int count = 0;

                    if (order.ContainsKey(c))
                        continue;

                    for (int r = 0; r < row1; r++)
                    {

                        if (indexofcipher < cipherText.Length && plain[r, c] == Char.ToLower(cipherText[indexofcipher]))
                        {
                            indexofcipher++;
                            count++;
                        }
                        else
                        {
                            if (plain[r, c] == '\0' && count == 3)
                            {
                                count++;
                            }
                            else
                            {
                                count = 0;
                                indexofcipher = index;
                                break;
                            }
                        }

                    }
                    if (count == row1)
                    {
                        order.Add(c, dic_index);
                        dic_index++;
                        break;
                    }
                }

            }
            List<int> key = new List<int>();
            for (int k = 0; k < order.Count; k++)
            {
                key.Add(order[k] + 1);
            }
            return key;

        }

        public string Decrypt(string cipherText, List<int> key)
        {
            int row = -1;
            int column = key.Count;

            if (cipherText.Length % key.Count == 0)
            {
                row = cipherText.Length / key.Count;
            }

            else
            {
                row = (cipherText.Length / key.Count) + 1;
            }

            char[,] plaintext = new char[row, column];
            int indexofcipher = 0;

            for (int c = 0; c < column; c++)
            {
                int j = 0;
                for (; j < key.Count; j++)
                {
                    if (key[j]-1 == c)
                        break;
                }
                for (int r = 0; r < row; r++)
                {
                    plaintext[r, j] = Char.ToLower(cipherText[indexofcipher]);
                    indexofcipher++;

                    if (indexofcipher == cipherText.Length)
                        break;
                }

                if (indexofcipher == cipherText.Length)
                    break;
            }

            String plain = "";
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < column; c++)
                {
                    if(plaintext[r,c]!='\0')
                         plain += plaintext[r, c];
                }
            }
            return plain;
        }

        public string Encrypt(string plainText, List<int> key)
        {
            int row = -1;
            int column=key.Count;

            if (plainText.Length % key.Count == 0)
            {
                row = plainText.Length / key.Count;
            }

            else
            {
                row = (plainText.Length / key.Count) + 1;
            }

            char[,] ciphertext = new char[row, column];
            int indexofplain = 0;

            Dictionary<int, int> order = new Dictionary<int, int>();

            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < column; c++)
                {
                    if(r==0)
                    {
                        order.Add(c, key[c]-1);
                    }
                    ciphertext[r, c] = Char.ToUpper(plainText[indexofplain]);
                    indexofplain++;

                    if (indexofplain == plainText.Length)
                        break;
                }

                if (indexofplain == plainText.Length)
                    break;
            }

            string cipher = "";
            int index = 0;
            while (index<column)
            { 
                for (int c = 0; c < order.Count; c++)
                {
                    if (order[c] == index)
                    {
                        index++;
                        for (int r = 0; r < row; r++)
                        {
                            if (ciphertext[r, c] != '\0')
                                cipher += ciphertext[r, c];
                        }
                        break;
                    } 
                }
            }
          

            return (cipher);
        }
    }
}
