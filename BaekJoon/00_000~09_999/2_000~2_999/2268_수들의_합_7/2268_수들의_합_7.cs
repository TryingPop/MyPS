using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 9
이름 : 배성훈
내용 : 수들의 합 7
    문제번호 : 2268번

    세그먼트 트리 문제다.
    구간 누적합을 찾아야 한다.

    팬윅 트리로 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1530
    {

        static void Main1530(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();
            int m = ReadInt();
            long[] seg = new long[n + 1];

            for (int i = 0; i < m; i++)
            {

                int op = ReadInt();
                int f = ReadInt();
                int t = ReadInt();

                if (op == 0)
                {

                    if (t < f)
                    {

                        int temp = f;
                        f = t;
                        t = temp;
                    }

                    long ret = GetVal(t) - GetVal(f - 1);
                    sw.Write($"{ret}\n");
                }
                else
                {

                    long cur = GetVal(f) - GetVal(f - 1);
                    Update(f, t - cur);
                }
            }

            void Update(int _chk, long _val)
            {

                for (; _chk <= n; _chk += _chk & -_chk)
                {

                    seg[_chk] += _val;
                }
            }

            long GetVal(int _chk)
            {

                long ret = 0;
                for (; _chk > 0; _chk -= _chk & -_chk)
                {

                    ret += seg[_chk];
                }

                return ret;
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

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
