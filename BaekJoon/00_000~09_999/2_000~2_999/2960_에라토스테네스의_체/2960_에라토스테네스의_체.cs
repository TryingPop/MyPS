using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 11
이름 : 배성훈
내용 : 에라토스테네스의 체
    문제번호 : 2960번

    에라토스테네스의 체 구현 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1535
    {

        static void Main1535(string[] args)
        {

            string[] input = Console.ReadLine().Split();
            bool[] arr = new bool[int.Parse(input[0]) + 1];
            int cnt = int.Parse(input[1]);

            for (int i = 2; i < arr.Length && 0 < cnt; i++)
            {

                if (arr[i]) continue;

                for (int j = i; j < arr.Length; j += i)
                {

                    if (arr[j]) continue;
                    arr[j] = true;
                    cnt--;
                    if (cnt == 0)
                    {

                        Console.Write(j);
                        break;
                    }
                }
            }
        }
    }
}
