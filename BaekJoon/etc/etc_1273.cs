using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. -
이름 : 배성훈
내용 : 언더프라임
    문제번호 : 1124번
*/

namespace BaekJoon.etc
{
    internal class etc_1273
    {

        static void Main1273(string[] args)
        {

            bool[] isPrime = new bool[100_001], visit = new bool[100_001];
            Array.Fill(isPrime, true);
            isPrime[1] = false;
            isPrime[0] = false;

            int cnt = 0;
            int[] div = new int[100_001];
            for (int i = 2; i <= 100_000; i++)
            {

                if (!isPrime[i]) continue;
                visit[i] = true;
                div[i] = 1;

                cnt++;
                for (int j = i + i; j <= 100_000; j += i)
                {

                    isPrime[j] = false;
                }
            }

            for (int i = 2; i <= 100_000; i++)
            {


            }
        }
    }
}
