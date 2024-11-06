using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 1
이름 : 배성훈
내용 : 이항 계수 2
    문제번호 : 11051번

    앞에 내용(27_05)이 안떠올라 상기하는 뜻으로 다시 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0141
    {

        static void Main141(string[] args)
        {

            int[] info = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            // 입력값이 1000이므로 브루트포스로 한다

            // A = n!
            // B = k!
            // C = (n - k)!
            int A = 1;
            int B = 1;
            int C = 1;

            if (info[0] - info[1] < info[1]) info[1] = info[0] - info[1];
            int idx = 2;
            for (; idx <= info[1]; idx++)
            {

                B *= idx;
                B %= 10_007;
            }

            C = B;
            for (; idx <= info[0] - info[1]; idx++)
            {

                C *= idx;
                C %= 10_007;
            }

            A = C;

            for (; idx <= info[0]; idx++)
            {

                A *= idx;
                A %= 10_007;
            }

            // 이건 p가 A, B, C보다 작아야 쓸 수 있다!
            // A / B * C = A * B^(p - 2) * C^(p - 2)
            B = Multiple(B, 10_007 - 2) % 10_007;
            C = Multiple(C, 10_007 - 2) % 10_007;

            int ret = (B * C) % 10_007;
            ret *= A;
            ret %= 10_007;

            Console.WriteLine(ret);
        }

        static int Multiple(int _n, int _pow)
        {

            // 같은 값을 제곱할 때 log 시간에 연산하는 방법!
            // 나중 타일 채우기에서 써야한다!
            int ret = 1;
            int calc = _n;
            while (_pow > 0)
            {

                if (_pow % 2 == 1)
                {

                    ret *= calc;
                    ret %= 10_007;
                    _pow--;
                }

                _pow /= 2;
                calc *= calc;
                calc %= 10_007;
            }

            return ret;
        }
    }
}
