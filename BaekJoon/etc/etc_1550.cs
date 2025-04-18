using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaekJoon.etc
{
    internal class etc_1550
    {

        static void Main1550(string[] args)
        {

            // 10327번
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int LEN = 44;
            long[] fibo;

            SetFibo();

            void SetFibo()
            {

                fibo = new long[LEN];

                fibo[0] = 1;
                fibo[1] = 1;

                for (int i = 2; i < LEN; i++)
                {

                    fibo[i] = fibo[i - 1] + fibo[i - 2];
                }
            }

            int t = ReadInt();

            while (t-- > 0)
            {

                int n = ReadInt();

                long ret1 = n + 1, ret2 = n + 1;

                for (int i = 1; i < fibo.Length; i++)
                {


                }

                sw.Write($"{ret1} {ret2}\n");
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != '\n' && c != ' ')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }
}
