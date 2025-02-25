using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 19
이름 : 배성훈
내용 : Heximal
    문제번호 : 26312번

    수학, 이분 탐색 문제다.
    C#으로 하니 시간초과 뜬다.

    아마도 큰 수 연산에서 시간초과 걸리는거 같다.
    추후 FFT 를 다시 공부할 때 다시 풀어봐야 겠다.
    당장은 파이썬으로 풀었다.
    로직은 C# 코드와 같다.
*/

namespace BaekJoon.etc
{
    internal class etc_1204
    {

        static void Main1204(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            string str = sr.ReadLine();
            BigInteger num = BigInteger.Parse(str);

            sr.Close();
            Solve();
            void Solve()
            {

                int ret = BinarySearch();
                Console.Write(ret);
            }

            int BinarySearch()
            {

                int l = 1;
                int r = 1_000_000;

                while (l <= r)
                {

                    int mid = (l + r) >> 1;
                    if (num < GetPow(6, mid)) r = mid - 1;
                    else l = mid + 1;
                }

                return r + 1;
            }

            BigInteger GetPow(BigInteger _a, int _exp)
            {

                BigInteger ret = 1;
                while(_exp > 0)
                {

                    if ((_exp & 1) == 1) ret *= _a;
                    _a *= _a;
                    _exp >>= 1;
                }

                return ret;
            }
        }
    }

#if python
// 입력 받기
num = int(input())

// 거듭 제곱 함수
// ** 연산자 이용하면 오버플로우 뜬다
def GetPow(a, exp):
    ret = 1

    while (exp > 0):
        if (exp % 2 == 1):
            ret *= a

        a *= a
        exp //= 2

    return ret
        

// 이분 탐색
def BinarySearch():

    if (num == 0):
        return 1
    
    l = 0
    r = 1_000_000

    while (l <= r):
        mid = (l + r) // 2

        if (num < GetPow(6, mid)):
            r = mid - 1
        else:
            l = mid +1

    return r + 1

print(BinarySearch())
#elif other
// #nullable disable

using System;
using System.IO;
using System.Numerics;

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = BigInteger.Parse(sr.ReadLine());
        //var n = BigInteger.Parse(new String('1', 500000));

        if (n == 0)
        {
            sw.WriteLine(1);
            return;
        }

        if (n < 3000)
        {
            var count = 0;

            while (n > 0)
            {
                n /= 6;
                count++;
            }

            sw.WriteLine(count);
            return;
        }

        var countApprox = (int)(BigInteger.Log10(n) / Math.Log10(6)) - 2;
        n /= FastPow(6, countApprox);

        while (n > 0)
        {
            n /= 6;
            countApprox++;
        }

        sw.WriteLine(countApprox);
    }

    public static BigInteger FastPow(BigInteger a, BigInteger pow)
    {
        var rv = BigInteger.One;
        while (pow > 0)
        {
            if (pow % 2 == 1)
                rv *= a;

            pow /= 2;
            a *= a;
        }

        return rv;
    }
}
#endif
}
