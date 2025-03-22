using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 18
이름 : 배성훈
내용 : 24
    문제번호 : 1408번

    사칙연산 문제다.
    시간 계산하는 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1416
    {

        static void Main1416(string[] args)
        {

            int[] f = Array.ConvertAll(Console.ReadLine().Split(':'), int.Parse);
            int[] t = Array.ConvertAll(Console.ReadLine().Split(':'), int.Parse);

            Sub(2, 60);
            Sub(1, 60);
            Sub(0, 24);

            Console.Write($"{t[0]:00}:{t[1]:00}:{t[2]:00}");

            void Sub(int _idx, int _up)
            {

                t[_idx] -= f[_idx];
                if (t[_idx] < 0) 
                { 
                    
                    t[_idx] += _up;
                    if (_idx > 0) t[_idx - 1]--;
                }
            }
        }
    }
}
