using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 31
이름 : 배성훈
내용 : 조합 0의 개수
    문제번호 : 2004번

    수학, 정수론 문제다
    조합에서 소인수 2의 개수와 5의 개수 중 작은 것을 반환하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0404
    {

        static void Main404(string[] args)
        {

            int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            long get2 = CountN(input[0], 2) - CountN(input[1], 2) - CountN(input[0] - input[1], 2);
            long get5 = CountN(input[0], 5) - CountN(input[1], 5) - CountN(input[0] - input[1], 5);

            // 5 C 1 = 5같이 5가 많은 경우도 있다
            long ret = get5 < get2 ? get5 : get2;

            Console.WriteLine(ret);

            // 팩토리얼을 인수분해할 때, n의 소인수 개수 찾기
            long CountN(int _factorial, int _n)
            {

                int chk = _factorial;
                long ret = 0;
                while(chk > 0)
                {

                    chk /= _n;
                    ret += chk;
                }

                return ret;
            }
        }
    }
}
