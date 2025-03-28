using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 27
이름 : 배성훈
내용 : 이상한 곱셈
    문제번호 : 1225번

    수학, 문자열 문제다.
    조건대로 구현하면 최악의 경우 1만 x 1만 = 1억번 연산을 한다.
    그런데 각 숫자의 갯수를 이용하면 3만번 이내의 연산으로 해결가능하다.
*/

namespace BaekJoon.etc
{
    internal class etc_1476
    {

        static void Main1476(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            string[] temp = sr.ReadLine().Split();
            int[] cnt1 = new int[10], cnt2 = new int[10];
            for (int i = 0; i < temp[0].Length; i++)
            {

                // 각 숫자 갯수 세기
                cnt1[temp[0][i] - '0']++;
            }

            for (int i = 0; i < temp[1].Length; i++)
            {

                // 각 숫자 갯수 세기
                cnt2[temp[1][i] - '0']++;
            }

            long ret = 0;

            for (int i = 1; i < 10; i++)
            {

                for (int j = 1; j < 10; j++)
                {

                    // i x j의 갯수를 찾는다.
                    long add = i * j * cnt1[i];
                    // int 여기서 범위 벗어날 수 있으므로 long 범위로 잡았다.
                    // 9 가 1만개씩 있는 문자열을 곱할 때 나온다.
                    // 9 x 9 x 1만 x 1만 = 81억 > 22억 > int.MaxValue
                    add *= cnt2[j];

                    ret += add;
                }
            }
            Console.Write(ret);
        }
    }
}
