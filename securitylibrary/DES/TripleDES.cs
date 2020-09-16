using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DES
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class TripleDES : ICryptographicTechnique<string, List<string>>
    {
        public string Decrypt(string cipherText, List<string> key)
        {
          //  throw new NotImplementedException();
            DES d = new DES();
            string one = d.Decrypt(cipherText, key[0]);
            string two = d.Encrypt(one, key[1]);
            string three = d.Decrypt(two, key[0]);


            return three;
        }

        public string Encrypt(string plainText, List<string> key)
        {
            //throw new NotImplementedException();
            DES d = new DES();
            string one =  d.Encrypt(plainText, key[0]);
            string two = d.Decrypt(one, key[1]);
            string three = d.Encrypt(two, key[0]);

            return three;
        }

        public List<string> Analyse(string plainText,string cipherText)
        {
            throw new NotSupportedException();
        }

    }
}
