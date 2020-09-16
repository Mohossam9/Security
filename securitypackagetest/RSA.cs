using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityLibrary.AES;

namespace SecurityLibrary.RSA
{
    public class RSA
    {
        public int power(int Base, int power, int modular)
        {
            int result = 1;

            if (modular == 1) return 0;
            for (int i = 0; i < power; i++)
            {
                result = (result * Base) % modular;
            }
            return result;
        }
        public int Encrypt(int p, int q, int M, int e)
        {
            // C = M ^ e mod n
            int n = p * q;
            return power(M, e, n);
        }

        public int Decrypt(int p, int q, int C, int e)
        {
            // d = e ^ -1 mod TotientN
            // M = C ^ d mod n
            int n = p * q;
            ExtendedEuclid obj = new ExtendedEuclid();
            int TotientN = (p - 1) * (q - 1);
            int d = obj.GetMultiplicativeInverse(e, TotientN);
            return power(C, d, n);
        }
    }
}
