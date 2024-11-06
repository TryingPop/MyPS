using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 7
이름 : 배성훈
내용 : BABBA
    문제번호 : 9625번

    dp 문제다
    규칙대로 한칸씩 진전했다
    시뮬레이션 돌려보니, 피보나치와 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0681
    {

        static void Main681(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            int[] arr = new int[2];
            arr[0] = 1;

            for (int i = 0; i < n; i++)
            {

                int temp1 = arr[1];
                int temp2 = arr[0] + arr[1];
                arr[0] = temp1;
                arr[1] = temp2;
            }

            Console.Write($"{arr[0]} {arr[1]}");
        }
    }
}
