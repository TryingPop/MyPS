using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 29
이름 : 배성훈
내용 : 유진수
    문제번호 : 1356번

    사칙연산 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1490
    {

        static void Main1490(string[] args)
        {

            string temp = Console.ReadLine();

            int[] right = new int[temp.Length];
            right[temp.Length - 1] = temp[temp.Length - 1] - '0';
            for (int i = right.Length - 2; i >= 0; i--)
            {

                right[i] = right[i + 1] * (temp[i] - '0');
            }

            bool ret = false;
            int left = 1;
            for (int i = 0; i < temp.Length - 1; i++)
            {

                left *= temp[i] - '0';
                if (left != right[i + 1]) continue;
                ret = true;
                break;
            }

            Console.Write(ret ? "YES" : "NO");
        }
    }
}
