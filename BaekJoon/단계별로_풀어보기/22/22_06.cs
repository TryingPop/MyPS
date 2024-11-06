using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 분수합
    문제번호 : 1735번
*/

namespace BaekJoon._22
{
    internal class _22_06
    {

        static void Main6(string[] args)
        {

            int[] A = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int[] B = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            // 분수 합 연산
            int up = (A[0] * B[1]) + (B[0] * A[1]);
            int down = (A[1]) * B[1];

            // 기약분수
            int calc = GCD(up, down);
            up /= calc;
            down /= calc;

            Console.WriteLine($"{up} {down}");
        }

        // 최대 공약수
        static int GCD(int x, int y)
        {

            int r = 1;

            while (true)
            {

                r = x % y;

                if (r == 0)
                {
                    r = y;
                    break;
                }

                x = y;
                y = r;
            }

            return r;
        }
    }
}
