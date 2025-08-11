using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 11
이름 : 배성훈
내용 : 최대 사이클 1
    문제번호 : 2682번

    조합론, 순열 사이클 분할
    아이디어는 다음과 같다.
    길이 l인 경우를 만드는 방법은 (l - 1)!개 존재한다.
    그래서 정답은 우리가 찾는 값이 k라면 
    k개에서 i개를 선택한다. 여기서 1, k는 포함되어야 하므로 
    실질적으로 k - 2개에서 i - 2개를 선택하는 것과 같다.

    그러면 (i - 1)!개의 경우가 존재한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1816
    {

        static void Main1816(string[] args)
        {

            // 2682번 - 최대 사이클 1
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            long[][] dp, combi;
            long[] fac;
            int n, k;
            int t = ReadInt();

            SetArr();

            while (t-- > 0)
            {

                Input();

                sw.Write(GetRet());
                sw.Write('\n');
            }

            void SetArr()
            {

                int MAX_N = 20;
                dp = new long[MAX_N + 1][];
                combi = new long[MAX_N + 1][];
                fac = new long[MAX_N + 1];

                for (int i = 0; i <= MAX_N; i++)
                {

                    dp[i] = new long[i + 1];
                    combi[i] = new long[i + 1];
                }

                combi[0][0] = 1;
                fac[0] = 1;

                for (int i = 1; i <= MAX_N; i++)
                {

                    fac[i] = fac[i - 1] * i;

                    combi[i][0] = 1;
                    combi[i][i] = 1;

                    for (int j = 1; j < i; j++)
                    {

                        combi[i][j] = combi[i - 1][j - 1] + combi[i - 1][j];
                    }
                }
            }

            long GetRet()
            {

                ref long ret = ref dp[n][k];
                if (k == 1) ret = fac[n - 1];

                if (ret != 0)
                    return ret;

                for (int cnt = 2; cnt <= k; cnt++)
                {

                    ret += combi[k - 2][cnt - 2] * fac[cnt - 1] * fac[n - cnt];
                }

                return ret;
            }

            void Input()
            {

                n = ReadInt();
                k = ReadInt();
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
