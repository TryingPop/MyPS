using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 3
이름 : 배성훈
내용 : 대한민국
    문제번호 : 3770번

    세그먼트 트리 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1510
    {

        static void Main1510(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n, m, q;
            int bias;
            int[] seg;
            (int f, int t)[] queries;

            Init();
            void Init()
            {

                seg = new int[(1 << 11) + 1];
                bias = 1 << 10;
                queries = new (int f, int t)[400_000];
            }

            int t = ReadInt();
            Comparer<(int f, int t)> comp = Comparer<(int f, int t)>.Create((x, y) =>
            {

                int ret = y.f.CompareTo(x.f);
                if (ret == 0) ret = y.t.CompareTo(x.t);
                return ret;
            });

            for (int i = 1; i <= t; i++)
            {

                sw.Write($"Test case {i}: ");
                n = ReadInt();
                m = ReadInt();
                q = ReadInt();
                

                for (int j = 0; j < q; j++)
                {

                    queries[j] = (ReadInt(), ReadInt());
                }

                Array.Sort(queries, 0, q, comp);

                long ret = 0;
                for (int j = 0; j < q; j++)
                {

                    Update(queries[j].t);
                    ret += GetVal(queries[j].t - 1);
                }

                Array.Fill(seg, 0);
                sw.Write(ret);
                sw.Write('\n');
            }

            void Update(int _chk)
            {

                int idx = bias | _chk;
                seg[idx]++;

                while (idx > 1)
                {

                    idx >>= 1;
                    seg[idx]++;
                }
            }

            int GetVal(int _chk)
            {

                int l = bias;
                int r = bias | _chk;

                int ret = 0;
                while (l < r)
                {

                    if ((r & 1) == 0) ret += seg[r--];
                    l >>= 1;
                    r >>= 1;
                }

                if (l == r) ret += seg[l];
                return ret;
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt());
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while((c =sr.Read()) != -1 && c != ' ' && c != '\n')
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
