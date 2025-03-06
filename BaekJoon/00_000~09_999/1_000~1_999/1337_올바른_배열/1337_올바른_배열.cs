using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 3
이름 : 배성훈
내용 : 올바른 배열
    문제번호 : 1337번

    구현 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1313
    {

        static void Main1313(string[] args)
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

                int ret = 4;
                for (int i = 0; i < n; i++)
                {

                    int cnt = 5;
                    int max = arr[i] + 4;
                    int e = Math.Min(n, i + 5);
                    for (int j = i; j < e; j++)
                    {

                        if (arr[j] <= max) cnt--;
                    }

                    ret = Math.Min(ret, cnt);
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = int.Parse(sr.ReadLine());
                }

                Array.Sort(arr);
            }
        }
    }
}
