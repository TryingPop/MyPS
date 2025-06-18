using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 11
이름 : 배성훈
내용 : 최솟값
    문제번호 : 10868번

    세그먼트 트리 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1695
    {

        static void Main1695(string[] args)
        {

            int INF = 1_000_000_001;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();
            int m = ReadInt();

            int log = n == 1 ? 1 : (int)(Math.Log2(n - 1)) + 2;
            int b = (1 << (log - 1));
            int[] seg = new int[b << 1];
            Array.Fill(seg, INF);

            for (int i = 0; i < n; i++)
            {

                seg[b | i] = ReadInt();
            }

            for (int p = b - 1; p >= 1; p--)
            {

                seg[p] = Math.Min(seg[p << 1], seg[(p << 1) | 1]);
            }

            for (int i = 0; i < m; i++)
            {

                int f = ReadInt();
                int t = ReadInt();

                sw.Write($"{GetVal(f, t)}\n");
            }

            int GetVal(int _l, int _r)
            {

                int lIdx = b | (_l - 1);
                int rIdx = b | (_r - 1);

                int ret = INF;
                while (lIdx <= rIdx)
                {

                    if ((lIdx & 1) == 1) ret = Math.Min(ret, seg[lIdx++]);
                    if ((rIdx & 1) == 0) ret = Math.Min(ret, seg[rIdx--]);

                    lIdx >>= 1;
                    rIdx >>= 1;
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
