using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 27
이름 : 배성훈
내용 : 색칠 1
    문제번호 : 1117번

    수학, 구현 문제다.
    예제 종이접기를 통해 해당 자리에 몇 개의 종이가 겹쳤는지 계산해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1475
    {

        static void Main1475(string[] args)
        {

            long[] arr = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);

            // 2배수가 되는 오른쪽 끝의 좌표
            long r = arr[0] < arr[2] * 2 ? arr[0] - arr[2] : arr[2];
            // 기본적인 배수
            long mul = (arr[3] + 1);

            // 전체 종이 갯수
            long sum = arr[0] * arr[1];
            // 빼는 종이 갯수
            long sub = 0;

            if (arr[6] <= r)
                // 포함되는 영역이 모두 2배수임
                sub = (arr[6] - arr[4]) * (arr[7] - arr[5]) * mul * 2;
            else if (r <= arr[4])
                // 포함되는 영역이 모두 1배수임
                sub = (arr[6] - arr[4]) * (arr[7] - arr[5]) * mul;
            else
            {

                // 2배수와 1배수에 겹친 경우
                sub = (r - arr[4]) * (arr[7] - arr[5]) * mul * 2;
                sub += (arr[6] - r) * (arr[7] - arr[5]) * mul;
            }

            Console.Write(sum - sub);
        }
    }
}
