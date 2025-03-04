using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 12
이름 : 배성훈
내용 : 장기
    문제번호 : 1846번

    해 구성하기 문제다.
    우선 3개일 때는 모든 경우를 고려해보면 불가능함을 알 수 있다.
    이후 4개 이상인 경우 홀수와 짝수 경우로 나눠서 풀었다.

    5이상의 홀수 일 때는 맨 윗줄을 중앙 mid에 놓는다.
    그리고 두번째줄부터 중앙 줄 mid까지 처음부터 순차적으로 하나씩 놓는다.
    mid + 1줄은 n번째에 놓는다.
    이후에 mid + 2줄은 mid + 1부터 놓아가면 이상없이 모두 놓을 수 있다.

    이제 짝수인 경우 첫번째 줄과 마지막 줄을 n / 2 = mid라하면
    첫 번째 줄은 mid, mid + 1줄은 마지막에 놓는다.
    그러면 이제 2번째줄부터 mid 줄까지 1번주터 채워간다.
    다음으로 mid + 2번줄부터 n번줄까지 mid + 2, ..., n - 1까지 채워가면 된다.
    이렇게 줄을 채우면 정답이 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1332
    {

        static void Main1332(string[] args)
        {

            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = int.Parse(Console.ReadLine());


            if (n == 3)
            {

                sw.Write(-1);
                return;
            }

            if ((n & 1) == 1)
            {

                int mid = (n + 1) >> 1;
                sw.WriteLine(mid);
                for (int i = 1; i < mid; i++)
                {

                    sw.WriteLine(i);
                }

                sw.WriteLine(n);

                for (int i = mid + 1; i < n; i++)
                {

                    sw.WriteLine(i);
                }
            }
            else
            {

                int mid = n >> 1;
                sw.WriteLine(mid);
                for (int i = 1; i < mid; i++)
                {

                    sw.WriteLine(i);
                }

                sw.WriteLine(n);

                for (int i = mid + 1; i < n; i++)
                {

                    sw.WriteLine(i);
                }
            }
        }
    }
}
