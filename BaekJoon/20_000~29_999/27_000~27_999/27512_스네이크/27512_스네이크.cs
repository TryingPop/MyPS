using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 21
이름 : 배성훈
내용 : 스네이크
    문제번호 : 27512번

    수학, 애드 혹 문제다.
    스네이크를 최대한 채우는 방법은 머리와 꼬리가 인접하는 것인데,
    다른 문제를 풀면서 넓이가 홀수면 - 1, 짝수면 해당 넓이 값이 최대임을 알았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1211
    {

        static void Main1211(string[] args)
        {

            int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            int ret = input[0] * input[1];
            if ((ret & 1) == 1) ret--;
            Console.Write(ret);
        }
    }
}
