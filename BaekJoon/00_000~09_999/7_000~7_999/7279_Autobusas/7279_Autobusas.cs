using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 8
이름 : 배성훈
내용 : Autobusas
    문제번호 : 7279번

    구현 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1263
    {

        static void Main1263(string[] args)
        {

            Solve();
            void Solve()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                int n, k;
                int a, b;

                Input();
                n = a;
                k = b;
                int cur = 0;
                int ret = 0;

                for (int i = 0; i < n; i++)
                {

                    Input();
                    cur += a;
                    cur -= b;

                    ret = Math.Max(ret, cur - k);
                }

                Console.Write(ret);

                void Input()
                {

                    string[] input = sr.ReadLine().Split();
                    a = int.Parse(input[0]);
                    b = int.Parse(input[1]);
                }
            }
        }
    }
}
