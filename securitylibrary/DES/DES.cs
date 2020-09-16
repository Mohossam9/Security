using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DES
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class DES : CryptographicTechnique
    {



        int[] x1 =
         new int[] { 57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34,
            26, 18, 10, 2, 59, 51,43, 35, 27, 19, 11, 3, 60, 52, 44,
            36, 63, 55, 47, 39, 31, 23, 15, 
                    7, 62, 54, 46, 38, 30, 22, 14, 6,
                    61, 53, 45, 37, 29, 21, 13, 5, 28, 20, 12, 4 };
        int[] x2 =
        new int[] { 14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10, 23,
            19, 12, 4, 26, 8, 16, 7, 27, 20, 13, 2, 41, 52, 31,
            37, 47, 55, 30, 40, 51, 45, 33, 48,
                    44, 49, 39, 56, 34, 53, 46, 42,
                    50, 36, 29, 32 };
        int[] i1 =
        new int[] { 58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28,
            20, 12, 4, 62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 
            16, 8, 57, 49, 41,
                    33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3, 61, 53, 45, 37, 
                    29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7 };
        int[] i2 =
        new int[] { 40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47, 15, 55, 23, 63, 31, 38, 6, 46, 14, 54, 22, 
            62, 30, 37, 5, 45, 13, 53, 21, 61, 29, 36, 4, 44, 
                    12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27, 34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41,
                    9, 49, 17, 57, 25};
      
        List<string> listofkeys = new List<string>();


        public void me()
        {
            int a = 0;
            while (a < 10)
            {
                a = a + 2;
            }


        }
 


        public string fun1(String cipherText)
        {

            string ciipphheerr = "";
            listofkeys.Clear();
            if (cipherText[1] == 'x')
            {
                for (int i = 2; i < 18; i++)
                {
                    int indk;
                    if (Char.IsLetter(cipherText[i])) indk = (cipherText[i] - 'A') + 10;
                    else indk = cipherText[i] - '0';
                    string fin = "";
                    while (indk > 0)
                    {
                        if (indk % 2 == 1)
                        {
                            fin += '1';
                            me();
                        }
                        else if (indk % 2 != 1)
                        {
                            fin += '0';
                        }
                        indk /= 2;
                    }
                    while (fin.Length < 4)
                    {
                        fin = fin + '0';
                        me();
                    }
                    string fin2 = "" + fin[3] + fin[2] + fin[1] + fin[0];
                    ciipphheerr += fin2;
                }
            }
            else if (cipherText[1] != 'x')
            {
                ciipphheerr = cipherText;
            }

            return ciipphheerr;

        }

        public string fun2(string key)
        {


            string kcc = "";
            if (key[1] == 'x')
            {
                for (int i = 2; i < 18; i++)
                {
                    int index;
                    if (Char.IsLetter(key[i]))
                    {
                        index = (key[i] - 'A') + 10;
                    }
                    else if ( ! Char.IsLetter(key[i]))
                    {
                        index = key[i] - '0';
                    }
                    else
                        index = key[i] - '0';

                    decdes s = new decdes();
                    kcc += s.hd(index);
                    me();
                }
            }
            else if (key[1] != 'x')
            {
                kcc = key;
            }


            return kcc;
        }


        public string fun3(string KeyCC)
        {
            string cacaca = "";
            for (int i = 0; i < 28; i++)
            {
                cacaca += KeyCC[i];
            }

            return cacaca ; 
        }
      
          public string fun4(string KeyCC)
        {
            string da = "";
            for (int i = 28; i < 56; i++)
            {
                me();
                da += KeyCC[i];
            }

            return da ; 
        }


          public string leftright(string ch, char [] chararr)
          {


              string le = "", ri = "";
              string ppll = new string(chararr);
              for (int i = 0; i < 32; i++)
              {
                  ri += ppll[i];
              }
              for (int i = 32; i < 64; i++)
              {
                  le += ppll[i];
              }
              for (int i = 16; i >= 1; i--)
              {
                  me();
                  string Right2 = le;
                  decdes s = new decdes();
                  string res = s.rf(le, listofkeys[i - 1]);
                  string Left2 = "";
                  for (int uu = 0; uu < ri.Length; uu++)
                  {
                      if (ri[uu] == res[uu]) Left2 += '0';
                      else Left2 += '1';
                  }
                  le = Left2;
                  ri = Right2;
              }

              if (ch == "r")
              {
                  return ri;
              
              }
              else if (ch == "l")
              {
                  return le;
              
              
              }

              return " ";
          }


          public string plain(string cplain)
          {
              string ppbb = "";
              for (int i = 0; i < cplain.Length; i += 4)
              {
                  string H = "";
                  for (int j = i; j < i + 4; j++) H += cplain[j];
                  decdes s = new decdes();
                  int L = s.abdo(H);
                  char Q = L < 10 ? '0' : 'A';
                  if (L > 9) L -= 10;
                  while (L > 0)
                  {
                      L = L - 1;
                      Q++;
                  }
                  ppbb += Q;
              }
              cplain = ppbb;

              return cplain;
          }
        public override string Decrypt(string cipherText, string key)
        {
            string ciipphheerr = "";
            ciipphheerr = fun1(cipherText);
            string kc = "";
            kc = fun2(key);
            string KeyCC = "";
            int ii =0 ;
            me();
            while (ii != 56)
            {
                KeyCC += kc[x1[ii] - 1];
                ii++;
            }
          
            string ca = "", da = "", sskey = "";
            ca = fun3(KeyCC);
            da = fun4(KeyCC);
            int  kk = 1;
            while (kk != 17)
            {
                int iito=0;
                switch (kk)
                {
                    case 1: 
                        iito = 1;
                        break;

                    case 2:
                        iito = 1;
                        break;

                    case 9:
                        iito = 1;
                        break;


                    case 16:
                        iito = 1;
                        break;

                    default:
                        iito = 2;
                        break;
                }
                while (iito > 0)
                {
                    iito--;
                    string caa = "";
                    string daa = "";
                    int uu = 0;
                    int nn = 0;
                    while (nn < da.Length - 1)
                    {
                        daa += da[nn + 1];
                        nn++;
                    }
                    daa += da[0];
                    while (uu < ca.Length - 1)
                    {
                        caa += ca[uu + 1];
                        uu++;
                    }
                    caa += ca[0];

                    if (iito > -1)
                    {
                        ca = caa;
                        da = daa;      
                    }      
                }
                int jj = 0;
                while (jj < ca.Length)
                {
                    sskey += ca[jj];
                    jj++;
                }
                  int jo = 0;
                while ( jo < da.Length)
                {
                    sskey += da[jo];
                    jo++;
                }
                string refss = "";
                int u = 0;
                while (u < 48)
                {
                    refss += sskey[x2[u] - 1];
                    u++;
                }
                listofkeys.Add(refss);
                sskey = "";
                kk++;
            }
            char[] chararr = new char[64];
            for (int i = 0; i < 64; i++)
            {
                chararr[i2[i] - 1] = ciipphheerr[i];
            }
            string Left = "", Right = "";
            Left = leftright("l" , chararr);
            Right = leftright("r", chararr);
           
           string cplain = "";
           for (int i = 0; i < 32; i++)
           {
               cplain += Left[i];

           }
           for (int i = 0; i < 32; i++)
           {
               cplain += Right[i];
           }

            char[] z = new char[64];
            for (int i = 0; i < 64; i++)
            {
                z[i1[i] - 1] = cplain[i];
            }
            cplain = new string(z);
            cplain = plain(cplain);
            me();
            string cccccc = string.Concat("0x", cplain);
            return cccccc;
       
        }






        //ENCRYPTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT
        public override string Encrypt(string plainText, string key)
        
        {
            encdes e = new encdes();
          string  pl = e.Encryptplain(plainText);
               string KeyC = "";
               KeyC =e. keyenc(key) ;
               me();
              e. pp(KeyC);
              me();
           pl =    e. plainnnn(pl);
           string CIPHOOOOOOOOOR = string.Concat("0x", pl);
            return CIPHOOOOOOOOOR;
        }
    }



    class encdes
    {

        int[] x1 =
         new int[] { 57, 49, 41, 33, 25, 17, 9, 1, 58, 50, 42, 34,
            26, 18, 10, 2, 59, 51,43, 35, 27, 19, 11, 3, 60, 52, 44,
            36, 63, 55, 47, 39, 31, 23, 15, 
                    7, 62, 54, 46, 38, 30, 22, 14, 6,
                    61, 53, 45, 37, 29, 21, 13, 5, 28, 20, 12, 4 };
        int[] x2 =
        new int[] { 14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10, 23,
            19, 12, 4, 26, 8, 16, 7, 27, 20, 13, 2, 41, 52, 31,
            37, 47, 55, 30, 40, 51, 45, 33, 48,
                    44, 49, 39, 56, 34, 53, 46, 42,
                    50, 36, 29, 32 };
        int[] i1 =
        new int[] { 58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28,
            20, 12, 4, 62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 
            16, 8, 57, 49, 41,
                    33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3, 61, 53, 45, 37, 
                    29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7 };
        int[] i2 =
        new int[] { 40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47, 15, 55, 23, 63, 31, 38, 6, 46, 14, 54, 22, 
            62, 30, 37, 5, 45, 13, 53, 21, 61, 29, 36, 4, 44, 
                    12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27, 34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41,
                    9, 49, 17, 57, 25};
      
        List<string> listofkeys = new List<string>();
        public string Encryptplain(string plainText)
        {
            string Plain = "";
            listofkeys.Clear();
            if (plainText[1] == 'x')
            {
                for (int i = 2; i < 18; i++)
                {
                    int IN;
                    if (Char.IsLetter(plainText[i]))
                    {
                        IN = (plainText[i] - 'A') + 10;
                    }
                    else
                    {
                        IN = plainText[i] - '0';
                    }
                    string K = "";
                    while (IN > 0)
                    {
                        if (IN % 2 == 1)
                        {
                            K += '1';
                        }
                        else if (IN % 2 != 1)
                        {
                            K += '0';
                        }
                        IN /= 2;
                    }




                    while (K.Length < 4)
                    {
                        K =K+'0';
                    }
                    string fin2 = "" + K[3] + K[2] + K[1] + K[0];
                    Plain = Plain + fin2;
                }
            }
            else if (plainText[1] != 'x')
            {
                Plain = plainText;
            }

            return Plain;

        }

        public string keyenc(string key)
        {
            string kckckc = "";
            if (key[1] == 'x')
            {
                for (int i = 2; i < 18; i++)
                {
                    int index;
                    if (Char.IsLetter(key[i]))
                    {
                        index = (key[i] - 'A') + 10;
                    }
                    else if (!Char.IsLetter(key[i]))
                    {
                        index = key[i] - '0'; 
                    
                    }
                    else
                        index = key[i] - '0';
                    decdes s = new decdes();
                    kckckc += s.hd(index);
                }
            }
            else if (key[1] != 'x')
            {
                kckckc = key;
            }


            return kckckc;
        }

        public void pp(string KeyC)
        {
            string kcx = "";
            for (int i = 0; i < 56; i++)
            {
                kcx += KeyC[x1[i] - 1];
            }


            string ccss = "";
            string ddss = "";
            string df = "";
            for (int i = 0; i < 28; i++)
            {
                ccss =ccss + kcx[i];
            }
            for (int i = 28; i < 56; i++)
            {
                ddss = ddss + kcx[i];
            }
            for (int i = 1; i <= 16; i++)
            {
                int INN;
                switch (i ) 
                {
                    case 1 : 
                        INN = 1;
                        break ;
                    case  2 :
                        INN = 1;
                        break ;
                    case 9 :
                        INN = 1;
                        break ;
                    case  16 :
                        INN = 1;
                        break ;
                    default :
                        INN = 2;
                        break;
            }
                while (INN > 0)
                {
                    INN--;
                    string caak = "";
                    string daak = "";
                    for (int uu = 0; uu < ccss.Length - 1; uu++)
                    {
                        caak = caak + ccss[uu + 1];
                    }
                    caak = caak +ccss[0];
                    for (int uu = 0; uu < ddss.Length - 1; uu++)
                    {
                        daak += ddss[uu + 1];
                    }
                    daak =daak + ddss[0];
                    ccss = caak;
                    ddss = daak;
                }
                for (int j = 0; j < ccss.Length; j++)
                {

                    df += ccss[j];
                }
                for (int j = 0; j < ddss.Length; j++)
                {
                    df += ddss[j];
                }
                string keyyyyyyys = "";
                for (int u = 0; u < 48; u++)
                {
                    keyyyyyyys += df[x2[u] - 1];
                }
                listofkeys.Add(keyyyyyyys);
                df = "";
            }
        }

        public string plainnnn(string Plain)
        {


            string pb = "";
            for (int i = 0; i < 64; i++) pb += Plain[i1[i] - 1];
            Plain = pb;

            string l = "", r = "";
            for (int i = 32; i < 64; i++)
            {
                r = r + Plain[i];
            }
            for (int i = 0; i < 32; i++)
            {
                l = l + Plain[i];
            }
         
            for (int i = 1; i <= 16; i++)
            {
                string l2 = r;
                decdes s = new decdes();
                string res = s.rf(r, listofkeys[i - 1]);
                string r2 = "";
                for (int uu = 0; uu < l.Length; uu++)
                {
                    if (l[uu] == res[uu])
                    {
                        r2 += '0';
                    }
                    else
                    {
                        r2 += '1';
                    }
                }
                l = l2;
                r = r2;
            }
            pb = "";
            for (int i = 0; i < 32; i++)
            {
                pb = pb + r[i];
            }
            for (int i = 0; i < 32; i++)
            {
                pb = pb + l[i];
            }
            Plain = pb;

            pb = "";
            for (int i = 0; i < 64; i++) pb += Plain[i2[i] - 1];
            Plain = pb;
            pb = "";
            for (int i = 0; i < Plain.Length; i += 4)
            {
                string ff = "";
                for (int j = i; j < i + 4; j++) ff += Plain[j];
                decdes s = new decdes();
                int ii = s.abdo(ff);
                char A = ii < 10 ? '0' : 'A';
                if (ii > 9) ii -= 10;
                while (ii > 0)
                {
                    ii--;
                    A++;
                }
                pb += A;
            }
            Plain = pb;

            return Plain;

        }
    
    }




    //Helper class
    class decdes
    {
        int[] arr =
      new int[] { 32, 1, 2, 3, 4, 5, 4,
          5, 6, 7, 8, 9, 8, 9, 10, 11, 12, 13, 12, 13, 14, 15, 16, 17,
            16, 17, 18, 19, 20, 21, 20, 
            21, 22, 23, 24, 25, 24, 
                    25, 26, 27, 28,
                    29, 28, 29, 30, 31, 32, 1 };
        int[] barr =
        new int[] { 16, 7, 20, 21, 29,
            12, 28, 17, 1, 15,
            23, 26, 5, 18, 31, 10, 2, 8, 24,
            14, 32, 27, 3, 9,
            19, 13, 30, 6,
            22, 11, 4, 25};
        int[] barr2 =
        new int[] {14, 4, 13, 1, 2, 15, 11, 8,
                   3, 10, 6, 12, 5, 9, 0, 7,
                   0, 15, 7, 4, 14, 2, 13, 1,
                   10, 6, 12, 11, 9, 5, 3, 8,
                   4, 1, 14, 8, 13, 6, 2, 11,
                   15, 12, 9, 7, 3, 10, 5, 0, 
                   15, 12, 8, 2, 4, 9, 1, 7, 
                   5, 11, 3, 14, 10, 0, 6, 13,
                   15, 1, 8, 14, 6, 11, 3, 4,
                   9, 7, 2, 13, 12, 0, 5, 10,
                   3, 13, 4, 7, 15, 2, 8, 14,
                   12, 0, 1, 10, 6, 9, 11, 5,
                   0, 14, 7, 11, 10, 4, 13, 1,
                   5, 8, 12, 6, 9, 3, 2, 15, 13,
                   8, 10, 1, 3, 15, 4, 2,
                   11, 6, 7, 12, 0, 5, 14, 9,
                   10, 0, 9, 14, 6, 3, 15, 5, 
                   1, 13, 12, 7, 11, 4, 2, 8,
                   13, 7, 0, 9, 3, 4, 6, 10,
                   2, 8, 5, 14, 12, 11, 15, 1,
                   13, 6, 4, 9, 8, 15, 3, 0, 
                   11, 1, 2, 12, 5, 10, 14, 7,
                   1, 10, 13, 0, 6, 9, 8, 7,
                   4, 15, 14, 3, 11, 5, 2, 12,
                   7, 13, 14, 3, 0, 6, 9, 10,
                   1, 2, 8, 5, 11, 12, 4, 15,
                   13, 8, 11, 5, 6, 15, 0, 3,
                   4, 7, 2, 12, 1, 10, 14, 9,
                   10, 6, 9, 0, 12, 11, 7, 13,
                   15, 1, 3, 14, 5, 2, 8, 4, 3,
                   15, 0, 6, 10, 1, 13, 8,
                   9, 4, 5, 11, 12, 7, 2, 14,
                   2, 12, 4, 1, 7, 10, 11, 6,
                   8, 5, 3, 15, 13, 0, 14, 9,
                   14, 11, 2, 12, 4, 7, 13, 1,
                   5, 0, 15, 10, 3, 9, 8, 6, 4,
                   2, 1, 11, 10, 13, 7, 8,
                   15, 9, 12, 5, 6, 3, 0, 14,
                   11, 8, 12, 7, 1, 14, 2, 13,
                   6, 15, 0, 9, 10, 4, 5, 3,
                   12, 1, 10, 15, 9, 2, 6, 8,
                   0, 13, 3, 4, 14, 7, 5, 11,
                   10, 15, 4, 2, 7, 12, 9, 5,
                   6, 1, 13, 14, 0, 11, 3, 8,
                   9, 14, 15, 5, 2, 8, 12, 3,
                   7, 0, 4, 10, 1, 13, 11, 6,
                   4, 3, 2, 12, 9, 5, 15, 10,
                   11, 14, 1, 7, 6, 0, 8, 13,
                   4, 11, 2, 14, 15, 0, 8, 13,
                   3, 12, 9, 7, 5, 10, 6, 1,
                   13, 0, 11, 7, 4, 9, 1, 10, 
                   14, 3, 5, 12, 2, 15, 8, 6,
                   1, 4, 11, 13, 12, 3, 7, 14,
                   10, 15, 6, 8, 0, 5, 9, 2,
                   6, 11, 13, 8, 1, 4, 10, 7,
                   9, 5, 0, 15, 14, 2, 3, 12, 
                   13, 2, 8, 4, 6, 15, 11, 1,
                   10, 9, 3, 14, 5, 0, 12, 7,
                   1, 15, 13, 8, 10, 3, 7, 4,
                   12, 5, 6, 11, 0, 14, 9, 2,
                   7, 11, 4, 1, 9, 12, 14, 2,
                   0, 6, 10, 
                   13, 15, 3, 5, 8, 2, 1, 14, 7,
                   4, 10, 8, 13,
                   15, 12, 9, 0, 3, 5, 6, 11};

        public string hd(int num)
        {
            string result = "";
            while (num > 0)
            {


                if (num % 2 == 1)
                {
                    result += '1';
                }
                else if (num % 2 != 1)
                {
                    result += '0';
                }
                num /= 2;


            }
            while (result.Length < 4)
            {
                result = result +'0';
            }
            string RES = "" + result[3] + result[2] + result[1] + result[0];
            return RES;
        }



        public int abdo(string str)
        {
            int num = 0;
            int  b = 1;

            int count = str.Length - 1; 

            for (int i = count; i >= 0 ; i--)
            {
                if (str[i] == '1')
                {
                    num = num + b;
                }
                if (true)
                {
                    b = b * 2;
                }
            }
            return num;
        }



        public string rf(string texxxx, string keyu)
        {
            string y2 = "";
            string y1 = "";


            //111111111111111
            int ii = 0;
            while ( ii < 48)
            {
                y2 =y2 + texxxx[arr[ii] - 1];
                ii++;
            }
            
            texxxx = y2;

            //22222222222222222
            y1 = "";
            int jaj = 0;
            while ( jaj < texxxx.Length)
            {
                if (texxxx[jaj] == keyu[jaj]) 
                {
                    y1 += '0';
                    }
                else if (texxxx[jaj] != keyu[jaj])
                {
                    y1 += '1';
                }

                jaj++;
            }
            texxxx = y1;


            //33333333333333333
            y2 = "";
            for (int i = 0; i < 48; i += 6)
            {
                string v = "";
                for (int j = i; j < i + 6; j++)
                {
                    v = v+texxxx[j];
                }
                int bb = i / 6 * 64;

                string RREE = "" + v[0] + v[5];
                string CCEE = "" + v[1] + v[2] + v[3] + v[4];

                int row = abdo(RREE);
                int column = abdo(CCEE);

                int iii = bb + (row * 16) + column;

                y2 =y2 + hd(barr2[iii]);
            }
            texxxx = y2;



            //4444444444444444444
            y2 = "";
            int AA = 0;
            while (AA < 32)
            {
                y2 = y2 + texxxx[barr[AA] - 1];
                AA++;
            }
            texxxx = y2;
            return texxxx;
        }




            
    
    
    }
}
