using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 19
이름 : 배성훈
내용 : 줄 세우기
    문제번호 : 1681번

    구현, 브루트포스 문제다.
    해당 수열을 만들어 풀었다.
    아니라면 문자열로 변환한 뒤 해당 문자가 있으면 넘기는식으로 구하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1284
    {

        static void Main1284(string[] args)
        {

            int n, pop, length;
            int[] arr;

            Solve();
            void Solve()
            {

                Input();

                SetArr();

                Console.Write(arr[n]);
            }

            void SetArr()
            {

                int s = 1;
                int e = 9;
                int len = 1;
                for (int i = 1; i < 10; i++)
                {

                    if (i == pop) continue;
                    arr[len++] = i;
                }

                while (len <= n)
                {

                    e = len;

                    for (int i = s; i < e; i++)
                    {

                        for (int j = 0; j < 10; j++)
                        {

                            if (j == pop) continue;
                            arr[len++] = arr[i] * 10 + j;
                            if (len > n) return;
                        }
                    }

                    s = e;
                }
            }

            void Input()
            {

                string[] input = Console.ReadLine().Split();
                n = int.Parse(input[0]);
                pop = int.Parse(input[1]);
                length = Math.Max(10, n);
                arr = new int[length + 1];
            }
        }
    }
}
