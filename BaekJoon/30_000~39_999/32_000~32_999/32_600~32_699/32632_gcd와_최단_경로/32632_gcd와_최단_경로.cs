using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 28
이름 : 배성훈
내용 : gcd와 최단 경로
    문제번호 : 32632번

    유클리드 호제법, 애드 혹 문제다.
    아이디어는 다음과 같다.
    1을 제외한 모든 수는 1과 거리가 1이다.
    그래서 모든 수의 최단 경로는 존재하고 커봐야 2다.
    그리고 서로 다른 두 수의 최단 경로가 1인 경우는 서로 소인 경우뿐이다.
    서로 소가 아닌 경우는 무조건 2가 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1585
    {

        static void Main1585(string[] args)
        {

#if GCD

            string[] temp = Console.ReadLine().Split();
            int k = int.Parse(temp[0]), n = int.Parse(temp[1]);

            int ret = 0;

            for (int i = 1; i <= n; i++)
            {

                if (i == k) continue;
                int gcd = GetGCD(k, i);
                if (gcd == 1 || gcd == 2) ret++;
            }

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
#else
            string[] temp = Console.ReadLine().Split();
            int k = int.Parse(temp[0]), n = int.Parse(temp[1]);


            int[] arr = new int[n + 1];
            for (int i = 1; i <= k; i++)
            {

                if (k % i != 0) continue;

                for (int j = i; j <= n; j += i)
                {

                    arr[j] = i;
                }
            }

            int ret = 0;
            for (int i = 1; i <= n; i++)
            {

                if (i == k) continue;
                if (arr[i] == 1 || arr[i] == 2) ret++;
            }

            Console.Write(ret);
#endif
        }
    }
}
