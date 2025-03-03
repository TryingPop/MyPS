using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 2
이름 : 배성훈
내용 : 중간계 전쟁
    문제번호 : 4435번

    사칙연산 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1371
    {

        static void Main1371(string[] args)
        {

            string HEAD = "Battle ";
            string MID = ": ";
            string RET1 = "Good triumphs over Evil\n";
            string RET2 = "Evil eradicates all trace of Good\n";
            string RET3 = "No victor on this battle field\n";

            int[] G = { 1, 2, 3, 3, 4, 10 };
            int[] S = { 1, 2, 2, 2, 3, 5, 10 };

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());

            for (int b = 1; b <= n; b++)
            {

                sw.Write(HEAD);
                sw.Write(b);
                sw.Write(MID);

                int g = 0;
                int s = 0;
                string[] input = sr.ReadLine().Split();
                for (int j = 0; j < 6; j++)
                {

                    g += G[j] * int.Parse(input[j]);
                }

                input = sr.ReadLine().Split();
                for (int j = 0; j < 7; j++)
                {

                    s += S[j] * int.Parse(input[j]);
                }

                if (s < g) sw.Write(RET1);
                else if (g < s) sw.Write(RET2);
                else sw.Write(RET3);
            }
        }
    }
}
