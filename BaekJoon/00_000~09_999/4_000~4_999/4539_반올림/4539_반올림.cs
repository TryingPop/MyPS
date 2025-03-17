using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 21
이름 : 배성훈
내용 : 반올림
    문제번호 : 4539번

    수학, 구현, 사칙연산 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1287
    {

        static void Main1287(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());
            int[] arr = new int[10];

            for (int i = 0; i < n; i++)
            {

                string input = sr.ReadLine();

                for (int j = 0; j < input.Length; j++)
                {

                    arr[j] = input[j] - '0';
                }

                for (int j = input.Length - 2; j >= 0; j--)
                {

                    if (arr[j + 1] > 4) arr[j]++;
                    arr[j + 1] = 0;
                }

                for (int j = 0; j < input.Length; j++)
                {

                    sw.Write(arr[j]);
                }

                sw.Write('\n');
            }
        }
    }
}
