using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace SecurityLibrary
{
    public class PlayFair : ICryptographicTechnique<string, string>
    {
        /// <summary>
        /// The most common diagrams in english (sorted): TH, HE, AN, IN, ER, ON, RE, ED, ND, HA, AT, EN, ES, OF, NT, EA, TI, TO, IO, LE, IS, OU, AR, AS, DE, RT, VE
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public string Analyse(string plainText)
        {
            throw new NotImplementedException();
        }

        public string Analyse(string plainText, string cipherText)
        {
            throw new NotSupportedException();
        }

        public string Decrypt(string cipherText, string key)
        {
            key = key.ToLower();
            string sGrid = null;
            string sAlpha = "abcdefghiklmnopqrstuvwxyz";
            string sInput = cipherText.ToLower();
            string sOutput = "";
            key = key.Replace('j', 'i');
            for (int i = 0; i < key.Length; i++)
            {

                if ((sGrid == null) || (!sGrid.Contains(key[i])))
                {

                    sGrid += key[i];

                }

            }

            for (int i = 0; i < sAlpha.Length; i++)


            {

                if (!sGrid.Contains(sAlpha[i]))

                {

                    sGrid += sAlpha[i];

                }

            }

            int iTemp = 0;
            do
            {
                int iPosA = sGrid.IndexOf(sInput[iTemp]);
                int iPosB = sGrid.IndexOf(sInput[iTemp + 1]);
                int iRowA = iPosA / 5;
                int iColA = iPosA % 5;
                int iRowB = iPosB / 5;
                int iColB = iPosB % 5;

                if (iColA == iColB)
                {
                    iPosA -= 5;
                    iPosB -= 5;
                }
           
                else
                {
                    if (iRowA == iRowB)
                    {
                        if (iColA == 0)
                            iPosA += 4;
                        else
                        {
                            iPosA -= 1;

                        }
                        if (iColB == 0)
                        {
                            iPosB += 4;
                        }
                        else
                        {
                            iPosB -= 1;
                        }
                    }
                    else
                    { 
                        if (iRowA < iRowB)
                        {
                            iPosA -= iColA - iColB;
                            iPosB += iColA - iColB;
                        }
                        else
                        {
                            iPosA += iColB - iColA;
                            iPosB -= iColB - iColA;
                        }
                    }

                }


                if (iPosA > sGrid.Length)
                    iPosA = 0 + (iPosA - sGrid.Length);

                if (iPosB > sGrid.Length)
                    iPosB = 0 + (iPosB - sGrid.Length);

                if (iPosA < 0)
                    iPosA = sGrid.Length + iPosA;

                if (iPosB < 0)
                    iPosB = sGrid.Length + iPosB;
            
                sOutput += sGrid[iPosA].ToString() + sGrid[iPosB].ToString();
                iTemp += 2;
            } while (iTemp < sInput.Length);

            String newoutput = "";
            for(int j=0;j<sOutput.Length-1;j++)
            {
                if (sOutput[j] == 'x' && sOutput[j-1]==sOutput[j+1] && j%2!=0)
                    continue;
                newoutput += sOutput[j];
            }

            if (sOutput[sOutput.Length - 1] != 'x' && sOutput.Length-1 %2!=0)
                newoutput += sOutput[sOutput.Length - 1];

            newoutput = newoutput.Replace('j', 'i');

            return newoutput;
        }

        public string Encrypt(string plainText, string key)
        {

            string sEncryptedText = string.Empty;

            if ((key != "") && (plainText != ""))

            {

                key = key.ToLower();
                string sGrid = null;
                string sAlpha = "abcdefghiklmnopqrstuvwxyz";
                plainText = plainText.ToLower();
                string sOutput = "";
                Regex rgx = new Regex("[^a-z-]");
                key = rgx.Replace(key, "");
                key = key.Replace('j', 'i');
                for (int i = 0; i < key.Length; i++)
                {
                    if ((sGrid == null) || (!sGrid.Contains(key[i])))
                    {
                        sGrid += key[i];

                    }

                }

                for (int i = 0; i < sAlpha.Length; i++)
                {
                    if (!sGrid.Contains(sAlpha[i]))
                    {
                        sGrid += sAlpha[i];
                    }
                }

                plainText = rgx.Replace(plainText, "");
                plainText = plainText.Replace('j', 'i');
                for (int i = 0; i < plainText.Length; i += 2)
                {
                    if (((i + 1) < plainText.Length) && (plainText[i] == plainText[i + 1]))
                    {

                        plainText = plainText.Insert(i + 1, "x");
                    }
                }

                if ((plainText.Length % 2) > 0)
                {
                    plainText += "x";
                }

                int iTemp = 0;
                do
                {
                    int iPosA = sGrid.IndexOf(plainText[iTemp]);
                    int iPosB = sGrid.IndexOf(plainText[iTemp + 1]);
                    int iRowA = iPosA / 5;
                    int iColA = iPosA % 5;
                    int iRowB = iPosB / 5;
                    int iColB = iPosB % 5;

                    if (iColA == iColB)
                    {

                        iPosA += 5;
                        iPosB += 5;
                    }
                    else
                    {

                        if (iRowA == iRowB)
                        {
                            if (iColA == 4)
                            {
                                iPosA -= 4;
                            }
                            else
                            {
                                iPosA += 1;
                            }
                            
                            if (iColB == 4)
                            {

                                iPosB -= 4;

                            }
                     
                            else
                            {
                                iPosB += 1;
                            }
                        }
                        else            
                        {
                            if (iRowA < iRowB)
                            {
                                iPosA -= iColA - iColB;
                                iPosB += iColA - iColB;
                            }
                            else
                            {
                                iPosA += iColB - iColA;
                                iPosB -= iColB - iColA;
                            }
                        }

                    }

                    if (iPosA >= sGrid.Length)
                    {
                        iPosA = 0 + (iPosA - sGrid.Length);

                    }

                    if (iPosB >= sGrid.Length)
                    {
                        iPosB = 0 + (iPosB - sGrid.Length);
                    }

                    if (iPosA < 0)
                    {
                        iPosA = sGrid.Length + iPosA;
                    }
                    if (iPosB < 0)
                    {
                        iPosB = sGrid.Length + iPosB;
                    }

                    sOutput += sGrid[iPosA].ToString() + sGrid[iPosB].ToString();
                    iTemp += 2;
                } while (iTemp < plainText.Length);

                sEncryptedText = sOutput;
            }
            return sEncryptedText;
        }
    
      
    }
}

