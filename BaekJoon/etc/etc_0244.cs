using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 16
이름 : 배성훈
내용 : 게임
    문제번호 : 1072번

    수학 문제다
    99%를 캐치 못해 여러번 틀렸다

    직접 손연산으로 x의 부등식을 찾았다
    // 내림 연산
    c := 100 * info[1] / info[0];
    
    // 소숫점 살려야한다
    d = (c + 1) / 100;

    그러면 찾으려는 x는
        x >= (info[0] * d - info[1]) / (1 - d)
    임을 알 수 있다

    아래는 이분탐색으로 다른 사람이 푼 풀이다
*/

namespace BaekJoon.etc
{
    internal class etc_0244
    {

        static void Main224(string[] args)
        {

            long[] info = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            long c = (info[1] * 100) / info[0];
            if (c == 100 || c == 99)
            {

                Console.WriteLine(-1);
                return;
            }

            decimal d = (c + 1) / (decimal)100.0;

            decimal calc = (info[0] * d - info[1]) / (1 - d);

            long ret = (long)Math.Ceiling(calc);
            Console.WriteLine(ret);
        }
    }

#if other
    static long BinarySearch(long X, long Y)
    {
        // 현재까지의 승률(소수점 버림)
        int CurrentRate = (int)((100 * Y) / X);
        // 승률이 100%이면 더 올라갈 수 없고, 99%면 아무리 승리해도 100%는 도달할 수 없다.
        if (CurrentRate >= 99)
            return -1;

        // 이분탐색을 위한 시작점, 끝점 설정
        long start = 1;
        long end = X;

        while (start <= end)
        {
            long mid = (start + end) / 2;
            if ((int)((100 * (Y + mid)) / (X + mid)) <= CurrentRate) start = mid + 1;
            else end = mid - 1;
        }
        return start;
    }
    static void Main(string[] args)
    {
        string[] stringXY = Console.ReadLine().Split(" ");
        long X = long.Parse(stringXY[0]);
        long Y = long.Parse(stringXY[1]);

        Console.WriteLine(BinarySearch(X, Y));
    }
#endif
}
