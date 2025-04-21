using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 21
이름 : 배성훈
내용 : 병든 나이트
    문제번호 : 1783번

    구현, 그리디 알고리즘, 많은 조건 분기 문제다
    갈 수 있는 좌표들을 찾아 조건을 나눠 풀었다

    나이트는 오른쪽으로만 이동할 수 있다
    그래서 가로 길이를 기준으로 상황을 나눴다
    이동할 수 잇는 높이가 1, 2, 3이상 이렇게 3가지로 나눴다
*/

namespace BaekJoon.etc
{
    internal class etc_0313
    {

        static void Main313(string[] args)
        {

            int[] size = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int ret = 0;
            if (size[1] == 1 || size[0] == 1) ret = 1;
            else if (size[0] == 2)
            {

                int calc = (size[1] + 1) / 2;
                ret = calc < 5 ? calc : 4;
            }
            else
            {

                if (size[1] > 6) ret = size[1] - 2;
                else ret = size[1] < 5 ? size[1] : 4;
            }
            Console.WriteLine(ret);
        }
    }
}
