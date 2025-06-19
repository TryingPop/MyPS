using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 6
이름 : 배성훈
내용 : 모범생 포닉스
    문제번호 : 28097번

    수학, 사칙연산 문제다.
    폰으로 풀어서 Solve함수가 아니다.
*/

namespace BaekJoon.etc
{
    internal class etc_1095
    {

        static void Main1095(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = int.Parse(sr.ReadLine());
            int[] arr = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            sr.Close();

            int sum = n * 8 - 8;
            for (int i = 0; i < n; i++)
            {

                sum += arr[i];
            }

            Console.Write($"{sum / 24} {sum % 24}");
        }
    }
}
