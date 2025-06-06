using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 29
이름 : 배성훈
내용 : 제곱 ㄴㄴ 수
    문제번호 : 1016번

    수학, 에라토스테네스의 체 이론 문제다
    아이디어는 다음과 같다

    min, max 범위가 100만 이므로, 100만 범위안에 n 제곱수들을 지워나간다
    max가 커봐야 1_000_001_000_000 이고,
    (1_000_001)^2 = 1_000_002_000_001 > 1_000_001_000_000 이므로
    100만 제곱수까지만 확인하면 된다
    그리고 지워지지 않은 것들의 개수를 세어, 결과로 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0386
    {

        static void Main386(string[] args)
        {

            long[] info = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
            long len = info[1] - info[0] + 1;
            bool[] arr = new bool[len];

            for (long i = 2; i <= 1_000_000; i++)
            {

                long div = i * i;
                if (info[1] < div) break;

                long s = info[0] % div;
                s = (div - s) % div;
                for (long j = s; j < len; j += div)
                {

                    arr[j] = true;
                }
            }

            int ret = 0;
            for(int i = 0; i <len; i++)
            {

                if (arr[i]) continue;
                ret++;
            }

            Console.WriteLine(ret);
        }
    }
#if other
using System;

long min, max;
string[] input = Console.ReadLine().Split();
min = long.Parse(input[0]);
max = long.Parse(input[1]);

bool[] isNotSquareFree = new bool[max - min + 1];

for (long i = 2; i * i <= max; i++)
{
    long square = i * i;
    long start = ((min - 1) / square + 1) * square;
    for (long j = start; j <= max; j += square)
    {
        isNotSquareFree[j - min] = true;
    }
}

int count = 0;
for (long i = min; i <= max; i++)
{
    if (!isNotSquareFree[i - min])
    {
        count++;
    }
}

Console.WriteLine(count);
#elif other2
using System;

class Joy
{
    static void Main()
    {
        string[] input = Console.ReadLine().Split();
        long min = long.Parse(input[0]);
        long max = long.Parse(input[1]);

        int count = CountNonSquareNumbers(min, max);
        Console.WriteLine(count);
    }

    static int CountNonSquareNumbers(long min, long max)
    {
        bool[] isSquare = new bool[max - min + 1];
        long sqrtMax = (long)Math.Sqrt(max);

        for (long i = 2; i <= sqrtMax; i++)
        {
            long square = i * i;
            long start = min % square == 0 ? min : min + (square - min % square);
            
            for (long j = start; j <= max; j += square)
            {
                isSquare[j - min] = true;
            }
        }

        int count = 0;
        for (int i = 0; i <= max - min; i++)
        {
            if (!isSquare[i])
            {
                count++;
            }
        }

        return count;
    }
}

#endif
}
