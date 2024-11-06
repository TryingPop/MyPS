using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 20
이름 : 배성훈
내용 : 돌 게임 6
    문제번호 : 9660번

    규칙성 찾는 문제다
    dp로 표를 만들어 보고 규칙을 찾았다

    true가 연달아 4개 나오면 0인 경우와 같아지므로 true가 4개 나오는 최소 구간인
    7개로 끊었다 false, true, false, true, true, true, true

    그리고 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0068
    {

        static void Main68(string[] args)
        {

            long chk = long.Parse(Console.ReadLine());

            chk %= 7;
            bool[] ret = new bool[7] { false, true, false, true, true, true, true };

            if (ret[chk]) Console.WriteLine("SK");
            else Console.WriteLine("CY");
        }
    }
}
