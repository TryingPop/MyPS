using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 20
이름 : 배성훈
내용 : 제리와 톰 2
    문제번호 : 17504번

    수학, 구현 문제다.
    분수 기호 정의대로 구현하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1945
    {

        static void Main1945(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();

            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
            }

            long up = 1, down = arr[n - 1];
            for (int i = n - 2; i >= 0; i--)
            {

                up = up + down * arr[i];

                long gcd = GetGCD(up, down);
                up /= gcd;
                down /= gcd;

                long temp = up;
                up = down;
                down = temp;
            }

            up = down - up;

            Console.Write($"{up} {down}");

            long GetGCD(long a, long b)
            {

                while (b > 0)
                {

                    long temp = a % b;
                    a = b;
                    b = temp;
                }

                return a;
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }
}
