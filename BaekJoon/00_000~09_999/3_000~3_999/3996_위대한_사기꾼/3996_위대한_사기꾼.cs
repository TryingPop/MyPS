using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 21
이름 : 배성훈
내용 : 위대한 사기꾼
    문제번호 : 3996번

    수학 문제다
    입력 숫자보다 작으면서 짝수번째 인덱스에 들어갈 수 있는 전체 경우의 수를 찾는 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0316
    {

        static void Main316(string[] args)
        {

            long[] input = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            long p = 1;
            int expMax = 0;
            while(p <= input[0])
            {

                long calc = input[0] / p;
                if (calc < input[1]) break;

                p *= input[1];
                expMax++;
            }

            long[] max = new long[expMax + 1];
            long[] exp = new long[expMax + 1];
            {

                long calc = input[0];
                for (int i = 0; i <= expMax; i++)
                {

                    max[i] = calc / p;

                    calc %= p;
                    p /= input[1];
                }
                exp[0] = 1;

                for (int i = 1; i <= expMax; i++)
                {

                    exp[i] = exp[i - 1] * input[1];
                }
            }

            long ret = 0;
            for (int i = 0; i <= expMax; i++)
            {

                if ((expMax - i) % 2 == 0)
                {

                    // 홀수번째 값이 모두 비었다면
                    // 자기보다 '작은' 짝수항을 모두 세어준다 - 자기자신 제외다
                    int calc = (expMax - i) / 2;
                    ret += max[i] * exp[calc];
                    // 마지막은 자기자신을 포함해서 1개 카운트!
                    if (i == expMax) ret++;
                }
                else if (max[i] > 0)
                {

                    // 홀수 항에 값이 있다면,
                    // p : input[1] - 진법 절댓값
                    // 이하 짝수번째 값을 k라 하면 p^k
                    int calc = (expMax + 1 - i)/ 2;
                    ret += exp[calc];
                    break;
                }
            }

            Console.WriteLine(ret);
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public static class Program
{
    private static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var nk = sr.ReadLine().Split(' ').Select(Int64.Parse).ToArray();
        var n = nk[0];
        var k = (int)nk[1];
        //sw.WriteLine(CountSlow(k, n));

        var digits = new List<int>();
        while (n != 0)
        {
            digits.Add((int)(n % k));
            n /= k;
        }

        digits.Reverse();
        sw.WriteLine(Count(k, digits));
    }

    public static long CountSlow(int k, long n)
    {
        var count = 0;

        for (var idx = 0; idx <= n; idx++)
        {
            var copy = idx;
            var digit = 0;
            while (copy > 0)
            {
                if (digit % 2 == 1 && copy % k != 0)
                    break;

                digit++;
                copy /= k;
            }

            if (copy == 0)
                count++;
        }

        return count;
    }
    public static long Count(int k, List<int> revdigits)
    {
        if (revdigits.Count == 0)
            return 0;
        if (revdigits.Count == 1)
            return 1 + revdigits[0];

        if (revdigits.Count % 2 == 0)
        {
            return Pow(k, revdigits.Count / 2);
        }
        else
        {
            if (revdigits[1] == 0)
                return revdigits[0] * Pow(k, revdigits.Count / 2) + Count(k, revdigits.Skip(2).ToList());
            else
                return revdigits[0] * Pow(k, revdigits.Count / 2) + Count(k, revdigits.Skip(1).ToList());
        }
    }

    private static long Pow(int k, int v)
    {
        var rv = 1L;
        for (var idx = 0; idx < v; idx++)
            rv *= k;

        return rv;
    }
}
#endif
}
