using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    /// <summary>
    /// The List<int> is row based. Which means that the key is given in row based manner.
    /// </summary>
    public class HillCipher : ICryptographicTechnique<List<int>, List<int>>
    {
        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {

            Dictionary<string, int> keydic = new Dictionary<string, int>();
            bool first_3 = false,sec_3= false;

            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    if ((i * plainText[0] + j * plainText[1]) % 26 == cipherText[0] && !first_3)
                    {
                        if ((i * plainText[2] + j * plainText[3]) % 26 == cipherText[2])
                        {
                            keydic.Add("first", i);
                            keydic.Add("second", j);
                            first_3 = true;
                            break;
                        }
                    }

                    if ((i * plainText[0] + j * plainText[1]) % 26 == cipherText[1] && !sec_3)
                        if ((i * plainText[2] + j * plainText[3]) % 26 == cipherText[3])
                        {
                            {
                                keydic.Add("third", i);
                                keydic.Add("fourth", j);
                                sec_3 = true;
                                break;
                            }
                        }
                }
            }

            List<int> key = new List<int>();
            key.Add(keydic["first"]);
            key.Add(keydic["second"]);
            key.Add(keydic["third"]);
            key.Add(keydic["fourth"]);

            return key;
        }


        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {

            int keyrow = -1;
            int keycolumn = -1;
            int ciphercolumn;
            int[,] plainindices;
            int[,] cipherindices;

            int determinant_value;
            int b = 0;



            if (key.Count() == 4)
            {
                keycolumn = keyrow = 2;

                ciphercolumn = cipherText.Count / 2;

                cipherindices = new int[2, ciphercolumn];

                plainindices = new int[2, ciphercolumn];

                if (cipherText.Count() % 2 != 0)
                    cipherText.Add(23);


            }
            else
            {
                keycolumn = keyrow = 3;

                ciphercolumn = cipherText.Count / 3;

                cipherindices = new int[3, ciphercolumn];

                plainindices = new int[3, ciphercolumn];

                if (cipherText.Count() % 3 != 0)
                    cipherText.Add(23);

            }


            int[,] keyarr = new int[keyrow, keycolumn];
            int keyindex = 0;

            int index = 0;
            for (int i = 0; i < ciphercolumn; i++) // b7ot list el cipher fe matrix
            {
                for (int j = 0; j < keyrow; j++)
                {
                    cipherindices[j, i] = cipherText[index];
                    index++;
                }

            }

            int r = 0;

            while (r < keyrow)   // b7ot list el key fe matrix
            {
                for (int c = 0; c < keycolumn; c++)
                {
                    keyarr[r, c] = key[keyindex];
                    keyindex++;
                }
                r++;
            }

            if (r == 2)  // determinant 2x2 matrix
            {
                determinant_value = ((keyarr[0, 0] * keyarr[1, 1]) - (keyarr[1, 0] * keyarr[0, 1]));
            }

            else   // determinant 3x3 matrix
            {
                determinant_value = (keyarr[0, 0] * ((keyarr[1, 1] * keyarr[2, 2]) - (keyarr[1, 2] * keyarr[2, 1])))
                                  - (keyarr[0, 1] * ((keyarr[1, 0] * keyarr[2, 2]) - (keyarr[1, 2] * keyarr[2, 0])))
                                  + (keyarr[0, 2] * ((keyarr[1, 0] * keyarr[2, 1]) - (keyarr[1, 1] * keyarr[2, 0])));

            }

            if (determinant_value < 0)  // el determinant_value lw nigative bfdl azwad 3leh 26 l7d lma ywsal l awl rkm positave b3d el 0
            {
                while (determinant_value < 0)
                {
                    determinant_value += 26;
                }
            }
            else                         // lw positave b3ml mod 26 3ade
                determinant_value = determinant_value % 26;



            for (int i = 0; i < 26; i++)   // b7sb el b (rkm ben 0:25) lma adrbo fe determinant_value mod 26 == 1
            {
                if (determinant_value * i % 26 == 1)
                {
                    b = i;
                    break;
                }
            }

            List<int> rule_output = new List<int>(); // h7ot feha natg el rule 3lshan b3d kda a7oto x el key matrix
            int rule = 0;

            if (keyrow == 2)
            {
               
                rule = b * (1) * (keyarr[1, 1]);
                rule_output.Add(rule);
                rule = b * (-1) * (keyarr[1, 0]);
                rule_output.Add(rule);
                rule = b * (-1) * (keyarr[0, 1]);
                rule_output.Add(rule);
                rule = b * (1) * (keyarr[0, 0]);
                rule_output.Add(rule);
            }
            else
            {
                rule = b * (1) * ((keyarr[1, 1] * keyarr[2, 2]) - (keyarr[1, 2] * keyarr[2, 1]));
                rule_output.Add(rule);
                rule = b * (-1) * ((keyarr[1, 0] * keyarr[2, 2]) - (keyarr[1, 2] * keyarr[2, 0]));
                rule_output.Add(rule);
                rule = b * (1) * ((keyarr[1, 0] * keyarr[2, 1]) - (keyarr[1, 1] * keyarr[2, 0]));
                rule_output.Add(rule);
                rule = b * (-1) * ((keyarr[0, 1] * keyarr[2, 2]) - (keyarr[0, 2] * keyarr[2, 1]));
                rule_output.Add(rule);
                rule = b * (1) * ((keyarr[0, 0] * keyarr[2, 2]) - (keyarr[0, 2] * keyarr[2, 0]));
                rule_output.Add(rule);
                rule = b * (-1) * ((keyarr[0, 0] * keyarr[2, 1]) - (keyarr[0, 1] * keyarr[2, 0]));
                rule_output.Add(rule);
                rule = b * (1) * ((keyarr[0, 1] * keyarr[1, 2]) - (keyarr[0, 2] * keyarr[1, 1]));
                rule_output.Add(rule);
                rule = b * (-1) * ((keyarr[0, 0] * keyarr[1, 2]) - (keyarr[0, 2] * keyarr[1, 0]));
                rule_output.Add(rule);
                rule = b * (1) * ((keyarr[0, 0] * keyarr[1, 1]) - (keyarr[0, 1] * keyarr[1, 0]));
                rule_output.Add(rule);
            }

            int s = 0;
            while (s < rule_output.Count())
            {
                if (rule_output[s] < 0)
                {
                    while (rule_output[s] < 0)
                    {
                        rule_output[s] += 26;

                    }
                }
                else
                    rule_output[s] = rule_output[s] % 26;
                s++;
            }


            keyindex = 0;
            r = 0;
            while (r < keyrow)   // b7ot list el rule_output fe matrix el key 
            {
                for (int c = 0; c < keycolumn; c++)
                {
                    keyarr[r, c] = rule_output[keyindex];
                    keyindex++;
                }
                r++;
            }


            int[,] keyarr2 = new int[keyrow, keycolumn];

            for (int i = 0; i < keycolumn; i++)     // transpose ll key matrix
            {
                for (int j = 0; j < keycolumn; j++)
                {
                    keyarr2[j, i] = keyarr[i, j];
                }
            }


            int sum = 0;
            int row = keyrow;
            int column = ciphercolumn;

            for (int i = 0; i < row; i++) // bdrab el cipher matrix fe el key matrix w el natg byt7t fe plain matrix
            {
                for (int j = 0; j < column; j++)
                {
                    int multi = 0;
                    while (multi < row)
                    {
                        sum += cipherindices[multi, j] * keyarr2[i, multi];
                        multi++;
                    }
                    plainindices[i, j] = sum;
                    sum = 0;
                }
            }



            List<int> plain = new List<int>();  // b7ot el plain matrix fe list
            for (int i = 0; i < ciphercolumn; i++)
            {
                for (int j = 0; j < keyrow; j++)
                {
                    int currentindex = plainindices[j, i] % 26;
                    plain.Add(currentindex);
                }
            }

            return plain;

        }


        public List<int> Encrypt(List<int> plainText, List<int> key)
        {

            int keyrow = -1;
            int keycolumn = -1;
            int ciphercolumn;
            int[,] plainindices;
            int[,] cipherindices;

            if (key.Count() == 4)
            {
                keycolumn = keyrow = 2;

                ciphercolumn = plainText.Count / 2;

                plainindices = new int[2, ciphercolumn];

                cipherindices = new int[2, ciphercolumn];

                if (plainText.Count() % 2 != 0)
                    plainText.Add(23);


            }
            else
            {
                keycolumn = keyrow = 3;

                ciphercolumn = plainText.Count / 3;

                plainindices = new int[3, ciphercolumn];

                cipherindices = new int[3, ciphercolumn];

                if (plainText.Count() % 3 != 0)
                    plainText.Add(23);

            }
            int[,] keyarr = new int[keyrow, keycolumn];
            int keyindex = 0;

            int r = 0;

            while (r < keyrow)
            {
                for (int c = 0; c < keycolumn; c++)
                {
                    keyarr[r, c] = key[keyindex];
                    keyindex++;
                }
                r++;
            }

            int index = 0;
            for (int i = 0; i < ciphercolumn; i++)
            {
                for (int j = 0; j < keyrow; j++)
                {
                    plainindices[j, i] = plainText[index];
                    index++;
                }

            }

            int sum = 0;
            int row = keyrow;
            int column = ciphercolumn;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    int multi = 0;
                    while (multi < row)
                    {
                        sum += plainindices[multi, j] * keyarr[i, multi];
                        multi++;
                    }
                    cipherindices[i, j] = sum;
                    sum = 0;
                }
            }

            List<int> cipher = new List<int>();
            for (int i = 0; i < ciphercolumn; i++)
            {
                for (int j = 0; j < keyrow; j++)
                {
                    int currentindex = cipherindices[j, i] % 26;
                    cipher.Add(currentindex);
                }
            }

            return cipher;
        }


        public List<int> Analyse3By3Key(List<int> plainText, List<int> cipherText)
        {
            Dictionary<string, int> keydic = new Dictionary<string, int>();

            bool sec_3 = false, f_3 = false, third_3 = false;
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    for (int k = 0; k < 25; k++)
                    {
                        if ((i * plainText[0] + j * plainText[1] + k * plainText[2]) % 26 == cipherText[0] && !f_3)
                        {
                            if ((i * plainText[3] + j * plainText[4] + k * plainText[5]) % 26 == cipherText[3])
                            {
                                if ((i * plainText[6] + j * plainText[7] + k * plainText[8]) % 26 == cipherText[6])
                                {
                                    keydic.Add("first", i);
                                    keydic.Add("second", j);
                                    keydic.Add("third", k);
                                    f_3 = true;
                                    break;
                                }
                            }

                        }

                        if ((i * plainText[0] + j * plainText[1] + k * plainText[2]) % 26 == cipherText[1] && !sec_3)
                            if ((i * plainText[3] + j * plainText[4] + k * plainText[5]) % 26 == cipherText[4])
                            {
                                if ((i * plainText[6] + j * plainText[7] + k * plainText[8]) % 26 == cipherText[7])
                                {
                                    keydic.Add("fourth", i);
                                    keydic.Add("fifth", j);
                                    keydic.Add("sixth", k);
                                    sec_3 = true;
                                    break;
                                }

                            }

                        if ((i * plainText[0] + j * plainText[1] + k * plainText[2]) % 26 == cipherText[2] && !third_3)
                            if ((i * plainText[3] + j * plainText[4] + k * plainText[5]) % 26 == cipherText[5])
                            {
                                if ((i * plainText[6] + j * plainText[7] + k * plainText[8]) % 26 == cipherText[8])
                                {
                                    keydic.Add("seventh", i);
                                    keydic.Add("eight", j);
                                    keydic.Add("ninth", k);
                                    third_3 = true;
                                    break;
                                }

                            } 
                    }
                }
  
            }

            List<int> key = new List<int>();
            key.Add(keydic["first"]);
            key.Add(keydic["second"]);
            key.Add(keydic["third"]);
            key.Add(keydic["fourth"]);
            key.Add(keydic["fifth"]);
            key.Add(keydic["sixth"]);
            key.Add(keydic["seventh"]);
            key.Add(keydic["eight"]);
            key.Add(keydic["ninth"]);



            return key;

        }
    }
}
