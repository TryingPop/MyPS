using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 11. 7
이름 : 배성훈
내용 : 마작에서 가장 어려운 것
    문제번호 : 33049번

    수학, 브루트포스 문제다.
    그리디로 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1965
    {

        static void Main1965(string[] args)
        {

            // 33049 - 마작에서 가장 어려운 것
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int ret1 = arr[0] / 3;
            int ret2 = arr[1] / 4;
            arr[0] %= 3;
            arr[1] %= 4;

            if (arr[0] > 0)
            {

                arr[2] -= 3 - arr[0];
                ret1++;
            }

            if (arr[1] > 0)
            {

                arr[2] -= 4 - arr[1];
                ret2++;
            }

            if (arr[2] == 0)
                Console.Write($"{ret1} {ret2}");
            else if (arr[2] == 3)
                Console.Write($"{ret1 + 1} {ret2}");
            else if (arr[2] == 4)
                Console.Write($"{ret1} {ret2 + 1}");
            else if (6 <= arr[2])
            {

                if (9 < arr[2])
                {

                    int add = (arr[2] - 6) / 4;
                    ret2 += add;
                    arr[2] -= add * 4;
                }

                if (arr[2] == 6) ret1 += 2;
                else if (arr[2] == 7)
                {

                    ret1++;
                    ret2++;
                }
                else if (arr[2] == 8) ret2 += 2;
                else if (arr[2] == 9) ret1 += 3;

                Console.Write($"{ret1} {ret2}");
            }
            else Console.Write(-1);
        }
    }
}
