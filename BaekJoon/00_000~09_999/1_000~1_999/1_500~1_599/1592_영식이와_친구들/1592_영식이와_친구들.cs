using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 3
이름 : 배성훈
내용 : 영식이와 친구들
    문제번호 : 1592번

    구현, 시뮬레이션 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1745
    {

        static void Main1745(string[] args)
        {

            int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            int gcd = GetGCD(arr[0], arr[2]);
            int ret = (arr[1] - 1) * (arr[0]/ gcd);

            Console.Write(ret);

            int GetGCD(int _a, int _b)
            {

                while (_b > 0)
                {

                    int temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
            }
        }
    }
}
