using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DiffieHellman
{
    public class DiffieHellman
    {
        public int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }
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

        public List<int> GetKeys(int q, int alpha, int xa, int xb)
        {
            // xa private key (a)
            // xb private key (b)

            List<int> keys = new List<int>();

            // Ya public key (a)
            // Yb public key (b)

            int Ya = power(alpha, xa, q);
            int Yb = power(alpha, xb, q);

            int k1 = power(Yb, xa, q);
            int k2 = power(Ya, xb, q);

            keys.Add(k1);
            keys.Add(k2);

            return keys;
        }
    }
}
