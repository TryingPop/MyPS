using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 27
이름 : 배성훈
내용 : 이진수 덧셈
    문제번호 : 1252번

    사칙연산 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1484
    {

        static void Main1484(string[] args)
        {

            string[] temp = Console.ReadLine().Split();
            int[] arr = new int[82];

            for (int i = temp[0].Length - 1, j = 0; i >= 0; i--, j++)
            {

                arr[j] = temp[0][i] - '0';
            }

            for (int i = temp[1].Length - 1, j = 0; i >= 0; i--, j++)
            {

                arr[j] += temp[1][i] - '0';
            }

            for (int i = 0; i < arr.Length - 1; i++)
            {

                while (arr[i] > 1)
                {

                    arr[i] -= 2;
                    arr[i + 1]++;
                }
            }

            int e = 0;
            for (int i = 0; i < arr.Length; i++)
            {

                if (arr[i] == 1) e = i;
            }

            for (int i = e; i >= 0; i--)
            {

                Console.Write(arr[i]);
            }
        }
    }
}
