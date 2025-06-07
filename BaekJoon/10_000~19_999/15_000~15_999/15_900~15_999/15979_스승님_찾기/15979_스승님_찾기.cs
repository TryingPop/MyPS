using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 24
이름 : 배성훈
내용 : 스승님 찾기
    문제번호 : 15979번

    문제를 해석하는 문제다
    -10억 ~ 10억 범위이기에 int로 받았다

    그리고, 경우의 수를 나눠서 찾았다
    0, 0에 스승이 있는 경우 이동할 필요가 없다

    ?, 0이나 0, ?인 경우
    ?가 -1, 1인 경우면 1번만 순간이동 하면 된다ㅣ
    이외 경우는 +-1, 0 이나 0, +-1에 가려지므로
    ?, 1 이나 1, ?로 이동하고 ?, 0이나 0, ?로 이동하면 2번에 된다

    그리고 이외의 0으로 가는 점이 없는 경우
    n, m 으로 간다고 가정하자
    n, m이 서로소라면 보이는 지점이므로 1번에 이동 가능하다
    서로소가 아니라면, n - 1, 1로 이동하고 n, m으로 이동하면 2회로 된다

    이를 코드로 구현한 것이 아래 코드다
*/

namespace BaekJoon.etc
{
    internal class etc_0087
    {

        static void Main87(string[] args)
        {

            int[] info = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            if (info[0] == info[1] && info[0] == 0)
            {

                Console.WriteLine(0);
            }
            else if (info[0] == 0)
            {

                if (info[1] == 1 || info[1] == -1) Console.WriteLine(1);
                else Console.WriteLine(2);
            }
            else if (info[1] == 0)
            {

                if (info[0] == 1 || info[0] == -1) Console.WriteLine(1);
                else Console.WriteLine(2);
            }
            else
            {

                int chk1 = info[0] < 0 ? -info[0] : info[0];
                int chk2 = info[1] < 0 ? -info[1] : info[1];

                int min = chk1 < chk2 ? chk1 : chk2;
                int max = chk1 < chk2 ? chk2 : chk1;

                if (Euclid(max, min) == 1) Console.WriteLine(1);
                else Console.WriteLine(2);
            }
        }

        static int Euclid(int max, int min)
        {

            int ret;
            int r = max % min;

            if (r == 0) ret = min;
            else ret = Euclid(min, r);

            return ret;
        }
    }
}
