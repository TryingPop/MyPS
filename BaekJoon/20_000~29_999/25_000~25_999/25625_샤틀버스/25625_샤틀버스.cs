using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 4
이름 : 배성훈
내용 : 샤틀버스
    문제번호 : 25625번

    수학, 사칙연산 문제다.
    가장 짧은 시간을 찾아야 한다.
    x, y가 주어지는데, 
    x는 이동시간의 절반 y는 맞은편에 도착한 시간이 된다.
    그래서 (y + x) % 2x가 정답이 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1145
    {

        static void Main1145(string[] args)
        {

            int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            input[1] += input[0];
            input[0] <<= 1;
            Console.Write(input[1] % input[0]);
        }
    }
}
