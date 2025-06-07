using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 9
이름 : 배성훈
내용 : 너 봄에는 캡사이신이 맛있단다
    문제번호 : 15824번

    수학, 정렬, 조합론 문제다.
    각 수가 큰 수로 사용되는 경우의 수는 내림차순 정렬되었을 때, i번째라하면 2^i - 1번이다.
    작은 수로 사용되는 경우의 수는 오름차순 정렬 했을 때, i번째라하면 2^i - 1번 사용된다.
    그러면 각 값을 누적해가면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1170
    {

        static void Main1170(string[] args)
        {

            int MOD = 1_000_000_007;

            int n;
            int[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Array.Sort(arr);

                long ret = 0;
                int e = n - 1;
                int mul = 0;
                for (int s = 0; s < n; s++, e--)
                {

                    long add = (1L * mul * (arr[s] - arr[e])) % MOD;
                    if (add < 0) add += MOD;
                    ret = (ret + add) % MOD;
                    mul = (mul * 2 + 1) % MOD;
                }

                Console.Write(ret);
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();

                int ReadInt()
                {

                    int ret = 0;
                    while (TryReadInt()) { }
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
}
