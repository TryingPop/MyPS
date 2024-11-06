using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 4
이름 : 배성훈
내용 : 피타고라스 기댓값
    문제번호 : 11070번

    수학, 구현 문제다
    부동소수점 오차만 주의하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0790
    {

        static void Main790(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;
            int[] s;
            int[] a;
            int n, m;
            long min, max;

            Solve();

            void Solve()
            {

                Init();
                int test = ReadInt();

                while (test-- > 0)
                {

                    Input();

                    GetRet();

                    sw.Write($"{max}\n");
                    sw.Write($"{min}\n");
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                min = 1_000;
                max = 0;
                for (int i = 0; i < n; i++)
                {

                    long cur = GetScore(i);
                    min = cur < min ? cur : min;
                    max = max < cur ? cur : max;
                }
            }

            long GetScore(int _idx)
            {

                long curS = s[_idx];
                long curA = a[_idx];
                if (curS == 0L && curA == 0L) return 0L;
                curS = curS * curS;
                curA *= curA;

                long ret = (10000 * curS) / (curA + curS);
                ret /= 10;
                return ret;
            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();
                for (int i = 0; i < n; i++)
                {

                    s[i] = 0;
                    a[i] = 0;
                }

                for (int i = 0; i < m; i++)
                {

                    int t1 = ReadInt() - 1;
                    int t2 = ReadInt() - 1;

                    int p = ReadInt();
                    int q = ReadInt();

                    s[t1] += p;
                    s[t2] += q;

                    a[t1] += q;
                    a[t2] += p;
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                s = new int[1_000];
                a = new int[1_000];
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
