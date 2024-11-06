using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 4
이름 : 배성훈
내용 : 개구리
    문제번호 : 23797번
*/

namespace BaekJoon.etc
{
    internal class etc_0793
    {

        static void Main793(string[] args)
        {

            StreamReader sr;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 4);
                string str = sr.ReadLine();
                sr.Close();

                int ret = 0;
                int len1 = 0;
                int len2 = 0;

                for (int i = 0; i < str.Length; i++)
                {

                    if (str[i] == 'P')
                    {

                        len1++;
                        if (0 < len2) len2--;

                        if (ret < len1) ret = len1;
                    }
                    else
                    {

                        len2++;
                        if (0 < len1) len1--;

                        if (ret < len2) ret = len2;
                    }
                }

                Console.Write(ret);
            }
        }
    }

#if other
using System;
class Program
{
  static void Main(String[] args)
  {
    string str = Console.ReadLine();
    int k = 0;
    int p = 0;
    int mx = 0;
    int sz = str.Length;
    for (int i = 0; i < sz; i++)
    {
      if (str[i] == 'K')
      {
        if (k != 0) k--;
        p++;
      }
      else
      {
        if (p != 0) p--;
        k++;
      }
      mx = Math.Max(mx, k + p);
    }
    Console.WriteLine(mx);
  }
}
#elif other2
#endif
}
