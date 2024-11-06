using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 29
이름 : 배성훈
내용 : 최대공약수와 최소공배수
    문제번호 : 2609번

    수학, 정수론, 유클리드 호제법 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0738
    {

        static void Main738(string[] args)
        {

            int[] n = Console.ReadLine().Split().Select(int.Parse).ToArray();

            int gcd = GetGCD(n[0], n[1]);
            int lcm = n[0] * n[1] / gcd;

            Console.Write($"{gcd}\n{lcm}");
            int GetGCD(int _a, int _b)
            {

                while(_b > 0)
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
