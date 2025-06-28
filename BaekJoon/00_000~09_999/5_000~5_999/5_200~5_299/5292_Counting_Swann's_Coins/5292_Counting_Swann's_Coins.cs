using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 13
이름 : 배성훈
내용 : Counting Swann's Coins
    문제번호 : 5292번

    구현 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1108
    {

        static void Main1108(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            string DEAD = "Dead\n";
            string MAN = "Man\n";
            string DEADMAN = "DeadMan\n";

            using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
            {

                for (int i = 1; i <= n; i++)
                {

                    if (i % 3 == 0 && i % 5 == 0) sw.Write(DEADMAN);
                    else if (i % 3 == 0) sw.Write(DEAD);
                    else if (i % 5 == 0) sw.Write(MAN);
                    else sw.Write($"{i} ");
                }
            }
        }
    }
}
