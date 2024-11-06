using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 2
이름 : 배성훈
내용 : 제곱근
    문제번호 : 13706번

    수학, 이분탐색 문제다
    처음 확인했을 때에는 브론즈였는데, 지금 보니 실버로 올라갔다
*/

namespace BaekJoon.etc
{
    internal class etc_0143
    {

        static void Main143(string[] args)
        {

            BigInteger find = BigInteger.Parse(Console.ReadLine());

            BigInteger l = 0, r = find;
            while(l <= r)
            {

                BigInteger mid = (l + r) / 2;


                if (mid * mid <= find) l = mid + 1;
                else r = mid - 1;
            }

            Console.Write(l - 1);
        }
    }

#if other
using System.Numerics;

class Program
{
    static BigInteger SqrtByBinarySearch(BigInteger A, int N)
    {
        BigInteger start = BigInteger.Pow(10, (N - 1) / 2);
        BigInteger end = BigInteger.Pow(10, (N - 1) / 2 + 1);

        while (start <= end)
        {
            BigInteger mid = (start + end) / 2;
            if (mid * mid <= A) start = mid + 1;
            else end = mid - 1;
        }
        return end;
    }
    static void Main(string[] args)
    {
        string stringA = Console.ReadLine();
        BigInteger A = BigInteger.Parse(stringA);

        Console.WriteLine(SqrtByBinarySearch(A, stringA.Length));
    }
}

#endif
}
