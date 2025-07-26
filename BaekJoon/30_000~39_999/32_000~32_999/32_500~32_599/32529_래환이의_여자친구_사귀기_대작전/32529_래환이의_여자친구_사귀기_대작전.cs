using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 20
이름 : 배성훈
내용 : 래환이의 여자친구 사귀기 대작전
    문제번호 : 32529번

    수학, 구현 문제다.
    누적합을 이용해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1207
    {

        static void Main1207(string[] args)
        {

            int n, m;
            int[] sum;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                for (int i = n - 1; i >= 0; i--)
                {

                    sum[i] += sum[i + 1];
                }
                int l = 0;
                int r = n;

                while (l <= r)
                {

                    int mid = (l + r) >> 1;
                    if (m <= sum[mid]) l = mid + 1;
                    else r = mid - 1;
                }

                Console.Write(l - 1);
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                sum = new int[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    sum[i] = ReadInt();
                }

                sr.Close();

                int ReadInt()
                {

                    int c, ret = 0;
                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return ret;
                }
            }
        }
    }
}
