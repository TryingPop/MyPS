using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 28
이름 : 배성훈
내용 : 2024년에는 혼자가 아니길
    문제번호 : 31529번

    수학, 기하학, 피타고라스 정리 문제다
    MatKor을 원점으로 하고 원점에서 각 점에 대한 거리를
    A = a, B = b, C = c, D = d라 하자
    ^을 제곱연산을 줄여 쓴 거라 생각하자
    그러면 x = a^ + b^ + c^ + d^
    y = (a + b)^ + (c + d)^

    그러면 MN은 (a - ((a - b)/ 2))^ + (c - ((c - d) / 2))^
    이고 이는 (2 * x - y) / 4.0과 같다

    그런데 x > y는 실수 범위에서 될 수 없고
    마찬가지로 2 * x - y < 0도 실수 범위에서는 불가능하다

    이렇게 풀어 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0642
    {

        static void Main642(string[] args)
        {

            int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int ret = input[0] * 2 - input[1];
            if (ret < 0 || input[0] > input[1]) ret = -1;
            else ret = (ret  * 2024) / 4;

            Console.WriteLine(ret);
        }
    }
}
