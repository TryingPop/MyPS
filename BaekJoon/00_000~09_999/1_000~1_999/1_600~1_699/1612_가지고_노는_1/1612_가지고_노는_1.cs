using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 12
이름 : 배성훈
내용 : 가지고 노는 1
    문제번호 : 1612번

    정수론 문제다
    왜 해가 보장되는지 아는게 더 중요한 문제같다
*/

namespace BaekJoon.etc
{
    internal class etc_0209
    {

        static void Main209(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            if (n % 2 == 0 || n % 5 == 0) 
            { 
                
                // 2, 5의 배수인 경우는 1로 이루어진 수의 배수가 될 수 없다
                // 쉽게 보면 끝자리가 매칭 안된다!
                // 합동 방정식으로 보면
                // 모든 정수 b에 대해 n * b != 1 (mod 10)
                Console.WriteLine(-1);
                return;
            }
            else
            {

                // 이제 2, 5가 아닌경우
                // 10과 n은 서로소이므로
                // 오일러 정리로
                // 10^phi(n) = 1(mod n)
                // 이말은
                // 10^0, 10^1, ..., 10^phi(n) - 1 까지의 합과, 
                // 10^phi(n), 10^(phi(n) + 1), ..., 10^(2 * phi(n) - 1) 까지의 합이 같다는 의미
                // 그리고 Z_n이 가환군이므로 n번 더하면 항상 0이 보장된다
                // 10과 n이 서로소면 n을 나누는 111....111이 항상 존재!
                int ret = 1;
                int chk = 1;
                while (chk % n != 0)
                {

                    chk *= 10;
                    chk += 1;
                    chk %= n;
                    ret++;
                }

                Console.WriteLine(ret);
            }

        }
    }
}
