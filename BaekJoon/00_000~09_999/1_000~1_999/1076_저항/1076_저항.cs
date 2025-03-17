using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 20
이름 : 배성훈
내용 : 저항
    문제번호 : 1076번

    구현 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1286
    {

        static void Main1286(string[] args)
        {

            Dictionary<string, int> sTv = new(10)
            {

                { "black", 0 },
                { "brown", 1 },
                { "red", 2 },
                { "orange", 3 },
                { "yellow", 4 },
                { "green", 5 },
                { "blue", 6 },
                { "violet", 7 },
                { "grey", 8 },
                { "white", 9 }
            };

            int[] vTm = new int[10];
            vTm[0] = 1;
            for (int i = 1; i < 10; i++)
            {

                vTm[i] = vTm[i - 1] * 10;
            }

            long num = sTv[Console.ReadLine()] * 10
                + sTv[Console.ReadLine()];

            num *= vTm[sTv[Console.ReadLine()]];
            Console.Write(num);
        }
    }
}
