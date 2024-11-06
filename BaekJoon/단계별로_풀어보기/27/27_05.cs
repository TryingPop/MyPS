using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 10
이름 : 배성훈
내용 : 이항 계수 3
    문제번호 : 11401

    Factorial을 재귀함수로 하니 메모리 사용량이 20배 이상 들어가고
    시간도 3배 이상 느려졌다

    다른 빠른 풀이를 보니 유클리드 알고리즘? 을 이용해서 푸는거 같다
    https://www.acmicpc.net/source/59803925
*/

namespace BaekJoon._27
{
    internal class _27_05
    {

        static void Main5(string[] args)
        {

            int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            // 1_000_000_007을 해당 루트보다 큰 33000까지 나눠본 결과 인수 분해가 안되었다
            // 에라토스테네스의 체 정리에 의해 그래서 1_000_000_007 은 소수!

            // 페르마 소정리를 이용해서
            // n!/(k! (n - k)!) == n! * k! ^ p-2 * (n - k)! ^ p - 2 (mod p)
            // p : 소수, n, k, n - k 는 모두 p보다 작아서 위 등식이 성립!

            // 그리고 앞 문제를 이용한다
            const int mod = 1_000_000_007;

            if (inputs[1] > inputs[0] / 2) inputs[1] = inputs[0] - inputs[1];

            long A = 1, B = 1, C = 1;

            int idx = 1;
            for (; idx <= inputs[1]; idx++)
            {

                B *= idx;
                B %= mod;
            }

            A = B;
            C = B;
            for (; idx <= inputs[0]- inputs[1]; idx++)
            {

                C *= idx;
                C %= mod;
            }

            A = C;
            for (;idx <= inputs[0]; idx++)
            {

                A *= idx;
                A %= mod;
            }

            B = Multiple(B, mod - 2, mod);
            C = Multiple(C, mod - 2, mod);

            long result = B * C % mod;
            result = result * A % mod;
            Console.WriteLine(result);
        }
        


        public static long Multiple(long _n, int _pow, int _mod)
        {

            long result = 1;

            while (_pow != 0)
            {

                if (_pow % 2 == 1)
                {

                    result *= _n;
                    result %= _mod;

                    _pow--;
                }

                _pow /= 2;
                _n = (_n * _n) % _mod;
            }

            return result;
        }
    }
}
