using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 28
이름 : 배성훈
내용 : 오타맨 고창영
    문제번호 : 2711번

    구현, 문자열 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1298
    {

        static void Main1298(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            int n = int.Parse(sr.ReadLine());

            for (int i = 0; i < n; i++)
            {

                string[] temp = sr.ReadLine().Split();
                int pop = int.Parse(temp[0]) - 1;

                for (int j = 0; j < temp[1].Length; j++)
                {

                    if (j == pop) continue;
                    sw.Write(temp[1][j]);
                }

                sw.Write('\n');
            }
        }
    }
}
