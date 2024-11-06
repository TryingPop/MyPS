using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 5
이름 : 배성훈
내용 : 소수의 연속합
    문제번호 : 1644번

    소수 판정과 연속한 부분수열 합 비교하는게 주된 풀이같다

    매번 소수를 에라토스 테네스 체이론으로 판별하며 하기에는 안좋은거 같아서
    먼저 입력값 이하의 모든 소수들을 찾아냈다
    입력 값의 범위가 400만(4mb) 이하이기에 과감하게 bool 배열을 썼다
    파악 방법은 고등학교에도 실린 소수 판정법이다
        1) i = 2부터 시작한다
        2) i를 제외한 나머지 i의 배수들은 해당 범위에서 모두 소수가 아니라고 판정한다
        3) 그리고 지워지지 않은 i보다 큰 지워지지 않은 수 중에서 가장 작은 수를 i로 놓고 2)과정을 실행한다
            여기서는 3이된다
        여기서 400만까지 하면 되나, 
        에라토스테네스의 체 정리에 의해 400만 이하의 모든 합성 수는 400만의 제곱근(2,000) 보다 작거나 같은 수들로 모두 소인수 분해 가능하므로
        입력값의 제곱근(소수 부분은 버림) 까지만 했다

    다음으로 연속한 부분수열의 합은 투 포인터 알고리즘을 이용했다
    앞에서 bool 배열에 소수인지 아닌지 저장되어 있으므로
    시작과 끝을 모두 2에서 시작하게 했다
*/

namespace BaekJoon._34
{
    internal class _34_04
    {

        static void Main4(string[] args)
        {
            
            // 입력
            int MAX = int.Parse(Console.ReadLine());

            // 소수 판정
            bool[] notPrimes = new bool[MAX + 1];
            int len = (int)Math.Sqrt(MAX) + 1;

            for (int i = 2; i < len; i++)
            {

                if (notPrimes[i]) continue;

                bool chk = false;
                for (int j = 2; j <= MAX; j++)
                {


                    if (i * j > MAX) break;

                    if (notPrimes[i * j]) continue;

                    notPrimes[i * j] = true;
                }
            }

            // 합 판별
            int start = 2;
            int end = 2;

            int result = 0;
            int calc = start;

            while (true)
            {

                if (calc >= MAX)
                {

                    if (calc == MAX)
                    {

                        result++;
                    }

                    calc -= start;
                    start++;
                    while(start <= MAX && notPrimes[start])
                    {

                        start++;
                    }

                    if (start > MAX) break;
                }
                else
                {

                    end++;

                    while (end <= MAX && notPrimes[end])
                    {

                        end++;
                    }

                    if (end > MAX) break;
                    calc += end;
                }
            }

            // 출력
            Console.WriteLine(result);
        }
    }
}
