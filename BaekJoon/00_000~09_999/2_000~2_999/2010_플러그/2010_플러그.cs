using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 13
이름 : 배성훈
내용 : 플러그
    문제번호 : 2010번

    사칙연산 문제다.
    최대한 멀티탭에 이어붙이면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1333
    {

        static void Main1333(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());

            int ret = 1;

            for (int i = 0; i < n; i++)
            {

                ret += int.Parse(sr.ReadLine()) - 1;
            }

            Console.Write(ret);
        }
    }
}
