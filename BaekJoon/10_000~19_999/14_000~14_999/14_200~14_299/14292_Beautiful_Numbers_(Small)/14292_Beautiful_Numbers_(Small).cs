using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 28
이름 : 배성훈
내용 : Beautiful Numbers(Small)
    문제번호 : 14292번

    수학, 브루트포스 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1650
    {

        static void Main1650(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int[] cnt;

            Init();

            int t = int.Parse(sr.ReadLine());
            for (int c = 1; c <= t; c++)
            {

                sw.Write($"Case #{c}: {cnt[int.Parse(sr.ReadLine())]}\n");
            }

            void Init()
            {

                int MAX = 1_000;
                cnt = new int[MAX + 1];
                for (int i = 3; i <= MAX; i++)
                {

                    cnt[i] = i - 1;
                }

                for (int i = 2; i <= MAX; i++)
                {

                    int cur = 1;
                    for (int j = i; j <= MAX; j *= i)
                    {

                        cur += j;
                        if (cur <= MAX) cnt[cur] = Math.Min(i, cnt[cur]);
                    }
                }
            }
        }
    }
}
