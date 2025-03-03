using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 3
이름 : 배성훈
내용 : 투에-모스 문자열
    문제번호 : 18222번

    분할 정복, 재귀 문제다.

    아이디어는 다음과 같다.
    조건 2를 분석하면 2^i 보다작은 k에 대해,
    2^i + k는 k번째를 0은 1로 1은 0으로 바꿔서 만들어진다.
    그리고 뒤집는 것은 순서를 바꿔도(교환법칙) 이상없다.

    이에 끝에서부터 뒤집기 작업을 진행해 0인 경우 0을 출력하게 하면 찾을 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1373
    {

        static void Main1373(string[] args)
        {

            long n = long.Parse(Console.ReadLine()) - 1;

            int ret = 0;
            for (int i = 62; i >= 0; i--)
            {

                if ((n & (1L << i)) == 0L) continue;
                ret = 1 - ret;
            }

            Console.Write(ret);
        }
    }
}
