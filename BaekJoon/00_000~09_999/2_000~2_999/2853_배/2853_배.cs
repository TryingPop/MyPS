using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 18
이름 : 배성훈
내용 : 배
    문제번호 : 2853번

    수학, 그리디 문제다.
    그리디로 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1289
    {

        static void Main1289(string[] args)
        {

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

                bool[] visit = new bool[n];

                int ret = 0;
                for (int i = 1; i < n; i++)
                {

                    if (visit[i]) continue;
                    ret++;

                    int jump = arr[i];
                    for (int j = i; j < n; j++)
                    {

                        if (arr[j] % jump == 0) visit[j] = true;
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new int[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt() - 1;
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
}
