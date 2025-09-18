using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 16
이름 : 배성훈
내용 : Rounding
    문제번호 : 17558번

    수학 문제다.
    반올림했을 때 해당 %가 되어야 한다.
    
    다음 2가지 반례를 보자.
    200개의 선택항목에 대해 모두 최소 최대가 50명이 투표한 경우
    200개 모두 1로 총합이 200이 될 수 있다.

    반면 예제를 보면 총 합이 100이 안되는 경우도 존재한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1887
    {

        static void Main1887(string[] args)
        {

            int n;
            string[] name;
            int[] per;

            Input();

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int[] min = new int[n];
                int[] max = new int[n];

                int minSum = 0;
                int maxSum = 0;

                for (int i = 0; i < n; i++)
                {

                    min[i] = Math.Max(per[i] * 100 - 50, 0);
                    max[i] = Math.Min(per[i] * 100 + 49, 10_000);

                    minSum += min[i];
                    maxSum += max[i];
                }

                if (10_000 < minSum || maxSum < 10_000)
                {

                    Console.Write("IMPOSSIBLE");
                    return;
                }

                maxSum = 0;
                for (int i = 0; i < n; i++)
                {

                    int curMin = minSum - min[i];
                    int chk = curMin + max[i] - 10_000;
                    if (chk < 0) chk = 0;
                    max[i] -= chk;

                    maxSum += max[i];
                }

                for (int i = 0; i < n; i++)
                {

                    int curMax = maxSum - max[i];
                    int chk = 10_000 - (curMax + min[i]);
                    if (chk < 0) chk = 0;
                    min[i] += chk;
                }

                for (int i = 0; i < n; i++)
                {

                    sw.Write(name[i]);
                    sw.Write(' ');
                    sw.Write($"{min[i] / 100.0:0.00} {max[i] / 100.0:0.00}\n");
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = int.Parse(sr.ReadLine());

                name = new string[n];
                per = new int[n];

                for (int i = 0; i < n; i++)
                {

                    string[] temp = sr.ReadLine().Split();
                    name[i] = temp[0];

                    per[i] = int.Parse(temp[1]);
                }
            }
        }
    }
}
