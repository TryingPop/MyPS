using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 16
이름 : 배성훈
내용 : 소수 부르기 게임
    문제번호 : 25632번

    소수를 부르는 문제인데
    겹치는 소수를 먼저 부르는게 유리하다!

    부를 수 있는 소수가 disjoint인 경우라하자
    이때, 많은 사람이 무조건 이긴다
    같은 경우 먼저 부른 사람이 진다!
*/

namespace BaekJoon.etc
{
    internal class etc_0046
    {

        static void Main46(string[] args)
        {

            int[] yt = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int[] yj = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int max = yt[1] < yj[1] ? yj[1] : yt[1];
            int min = yt[0] < yj[0] ? yt[0] : yj[0];

            int overlap = 0;        // 유진, 용태 둘 다 부를 수 있는 소수
            int ytPrime = 0;        // 용태만 부를 수 있는 소수
            int yjPrime = 0;        // 유진만 부를 수 있는 소수

            // 소수 판정
            for (int i = min; i <= max; i++)
            {

                bool prime = true;

                for (int j = 2; j < max; j++)
                {

                    if (j * j > i) break;

                    else if (i % j == 0)
                    {

                        prime = false;
                        break;
                    }
                }

                if (!prime) continue;

                bool chk1 = false, chk2 = false;

                if (yt[0] <= i && i <= yt[1]) chk1 = true;
                if (yj[0] <= i && i <= yj[1]) chk2 = true;

                if (chk1 && chk2) overlap++;
                else if (chk1) ytPrime++;
                else if (chk2) yjPrime++;
            }

            // overlap부터 비우는게 유리하다!
            // overlap을 다쓰는 순간
            // 먼저 외치는 사람이 같은 개수일 때 진다
            // 서로 다르면 많은 사람이 이긴다!
            overlap &= 1;
            if (ytPrime + overlap <= yjPrime) Console.WriteLine("yj");
            else Console.WriteLine("yt");
        }
    }
}
