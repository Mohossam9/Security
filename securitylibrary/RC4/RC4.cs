using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RC4
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class RC4 : CryptographicTechnique
    {
        public override string Decrypt(string cipherText, string key)
        {
            //Check for Hexadecimal vslue
            bool hexa = key.Contains("0x");
            if (hexa)
            {
                key = key.Remove(0, 2);
                cipherText = cipherText.Remove(0, 2);
            }
            //Intialize for both S&&T
            int[] S = new int[256];
            int[] T = new int[256];

            //Load Values for each S&&T
            int i = 0, j = 0;
            for (; i < 256; i++)
            {
                S[i] = i;
                if (hexa)
                    T[i] = int.Parse("" + key[(i * 2) % key.Length] + key[((i * 2) + 1) % key.Length], System.Globalization.NumberStyles.HexNumber);
                else
                    T[i] = (int)key[i % key.Length];
            }
            //Permutation for S
            i = 0; j = 0;
            for (; i < 256; i++)
            {
                j = (j + S[i] + T[i]) % 256;
                int tmp = S[i];
                S[i] = S[j];
                S[j] = tmp;
            }

            //Generate for Keys based on CT Bytes
            i = 0; j = 0;
            string Plain = "";

            for (int charci = 0; charci < cipherText.Length; charci++)
            {
                char Messagei = cipherText[charci];
                int Messagef = Messagei;
                if (hexa)
                {
                    Messagef = int.Parse("" + cipherText[charci] + cipherText[charci + 1],
                        System.Globalization.NumberStyles.HexNumber);
                    charci++;
                }
                i = (i + 1) % 256;
                j = (j + S[i]) % 256;
                int tmp = S[i];
                S[i] = S[j];
                S[j] = tmp;
                int generator = (S[i] + S[j]) % 256;
                if (hexa)
                    Plain += (Messagef ^ S[generator]).ToString("X");
                else
                    Plain += (char)(Messagef ^ S[generator]);
            }
            if (hexa)
            {
                Plain = "0x" + Plain;
            }

            return Plain;
        }

        public override string Encrypt(string plainText, string key)
        {
            //Check for Hexadecimal vslue
            bool hexa = key.Contains("0x");
            if (hexa)
            {
                key = key.Remove(0, 2);
                plainText = plainText.Remove(0, 2);
            }
            // Intialie for S and T
            int[] S = new int[256];
            int[] T = new int[256];
            // Load Values for S && T
            int i = 0, j = 0;
            for (; i < 256; i++)
            {
                S[i] = i;
                if (hexa)
                    T[i] = int.Parse("" + key[(i * 2) % key.Length] + key[((i * 2) + 1) % key.Length], System.Globalization.NumberStyles.HexNumber);
                else
                    T[i] = (int)key[i % key.Length];
            }

            //Permutation for S
            i = 0; j = 0;
            for (; i < 256; i++)
            {
                j = (j + S[i] + T[i]) % 256;
                int tmp = S[i];
                S[i] = S[j];
                S[j] = tmp;
            }

            //Generate for Keys based on PT Bytes
            i = 0; j = 0;
            string Cipher = "";
            for (int charp = 0; charp < plainText.Length; charp++)
            {
                char Messagei = plainText[charp];
                int Messagef = Messagei;
                if (hexa)
                {
                    Messagef = int.Parse("" + plainText[charp] + plainText[charp + 1],
                        System.Globalization.NumberStyles.HexNumber);
                    charp++;
                }
                i = (i + 1) % 256;
                j = (j + S[i]) % 256;
                int tmp = S[i];
                S[i] = S[j];
                S[j] = tmp;
                int generator = (S[i] + S[j]) % 256;
                if (hexa)
                    Cipher += (Messagef ^ S[generator]).ToString("X");
                else
                    Cipher += (char)(Messagef ^ S[generator]);
            }
            if (hexa)
            {
                Cipher = "0x" + Cipher;
            }

            return Cipher;
        }
    }
}
