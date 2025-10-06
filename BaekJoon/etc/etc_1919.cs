using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 28
이름 : 배성훈
내용 : K-균등 문자열
    문제번호 : 15502번

    분리 집합 문제다.
    아이디어는 다음과 같다.
    l ~ r 구간안에 연속한 k개들에 대해 1의 개수가 같아야 한다.
    그래서 l + k + 1과 l의 값이 일치해야지만 가능하다.
    그래서 l과 l + k + 1은 같은 값을 갖는 그룹이 된다.
    이렇게 유니온 파인드를 진행해서 남은 서로 다른 칸의 개수를 g라하면
    정답은 2^g가 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1919
    {

        static void Main1919(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = ReadInt();
            int[] group = new int[n + 1];
            for (int i = 1; i <= n; i++)
            {

                group[i] = i;
            }

            int[] stk = new int[n];

            int g = n;
            int m = ReadInt();
            for (int i = 0; i < m; i++)
            {

                int l = ReadInt();
                int r = ReadInt();
                int k = ReadInt();

                for (int j = l; j + k <= r; j++)
                {

                    Union(j, j + k);
                }
            }

            long ret = GetPow(2, g);
            Console.Write(ret);

            long GetPow(long a, int exp)
            {

                long ret = 1;
                int MOD = 1_000_000_007;
                while (exp > 0)
                {

                    if ((exp & 1L) == 1L) ret = (ret * a) % MOD;

                    a = (a * a) % MOD;
                    exp >>= 1;
                }

                return ret;
            }

            void Union(int f, int t)
            {

                int gF = Find(f);
                int gT = Find(t);
                if (gF == gT) return;
                g--;
                int min = gF < gT ? gF : gT;
                int max = gF < gT ? gT : gF;

                group[max] = min;
            }

            int Find(int chk)
            {

                int len = 0;

                while (group[chk] != chk)
                {

                    stk[len++] = chk;
                    chk = group[chk];
                }

                while (len-- > 0)
                {

                    group[stk[len]] = chk;
                }

                return chk;
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
