using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 8
이름 : 배성훈
내용 : 암호제작
    문제번호 : 1837번

    수학, 브루트포스 문제다
    조건대로 구현해 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_1036
    {

        static void Main1036(string[] args)
        {

            string YES = "GOOD";
            string NO = "BAD ";

            string p;
            int k;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                if (k > 2 && Div(2))
                {

                    Console.Write($"{NO}{2}");
                    return;
                }

                for (int i = 3; i < k; i += 2)
                {

                    if (Div(i))
                    {

                        Console.Write($"{NO}{i}");
                        return;
                    }
                }

                Console.Write(YES);
            }

            bool Div(int _n)
            {

                int r = 0;
                for (int i = 0; i < p.Length; i++)
                {

                    r = (r * 10 + p[i] - '0') % _n;
                }

                return r == 0;
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                p = temp[0];
                k = int.Parse(temp[1]);
            }
        }
    }

#if other
using System;
using System.Numerics;

class cw
{
    public static int Main(string[] args)
    {
        string[] num = Console.ReadLine().Split();
        BigInteger num1 = BigInteger.Parse(num[0]);
        int num2 = int.Parse(num[1]);

        int[] temp = new int[num2 + 1];

        for (int i = 2; i < num2; i++)
        {
            temp[i] = i;
        }

        for (int i = 2; i <= num2; i++)
        {   
            if (temp[i] == 0) continue;
            for (int j = i + i; j <= num2; j += i)
            {
                temp[j] = 0;
            }
        }

        for(int i = 2; i <= num2;i++)
        {
            if (temp[i] == 0) continue;
            if (num1 % temp[i] == 0)
            {
                Console.WriteLine("BAD " + temp[i]);
                return 0;
            }
        }

        Console.WriteLine("GOOD");
        return 0;

    }
}
#endif
}
