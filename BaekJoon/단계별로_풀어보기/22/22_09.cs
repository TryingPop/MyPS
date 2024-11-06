using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 골드바흐 파티션
    문제번호 : 17103번

    1 ~ ??? 까지 소수들을 찾아 기록할 때에는 각각의 수를 소수 판정하는게 아닌 소수의 정의를 이용하는게 가장 빠르다
*/

namespace BaekJoon._22
{
    internal class _22_09
    {

        static void Main9(string[] args)
        {
#if false
            int len = int.Parse(Console.ReadLine());
            // int[] nums = new int[1000001];
            bool[] primes = new bool[1000001];
            // nums[3] = 1;
            primes[3] = true;
            int max = 3;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < len; i++)
            {

                int input = int.Parse(Console.ReadLine());

                if (input <= 6)
                {

                    sb.AppendLine("1");
                    continue;
                }


                // 입력값보다 작은 소수 찾기
                bool chk;
                for (int j = max + 2; j < input; j += 2)
                {

                    chk = true;
                    for (int k = 3; k < (int)Math.Sqrt(j) + 1; k += 2)
                    {

                        if (j % k == 0)
                        {

                            chk = false;
                        }
                    }

                    if (chk)
                    {

                        max = j;
                        // nums[j] = 1;
                        primes[j] = true;
                    }
                }

                // 골드바흐 파티션 수 판정
                int count = 0;
                for (int j = 3; j <= input / 2; j += 2)
                {

                    if (primes[j] && primes[input - j])
                    {

                        count++;
                    }
                }

                sb.AppendLine(count.ToString());
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.Write(sb);
            }
#elif false

            int len = int.Parse(Console.ReadLine());
            HashSet<int> prime = new HashSet<int>() { 3 };
            int max = 3;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < len; i++)
            {

                int input = int.Parse(Console.ReadLine());

                if (input <= 6)
                {

                    sb.AppendLine("1");
                    continue;
                }


                // 입력값보다 작은 소수 찾기
                bool chk;
                int bound;
                for (int j = max + 2; j < input; j += 2)
                {

                    // 소수 판정
                    chk = true;
                    bound = (int)Math.Sqrt(j) + 1;
                    for (int k = 3; k < bound; k += 2)
                    {

                        if (j % k == 0)
                        {

                            chk = false;
                        }
                    }

                    if (chk)
                    {

                        max = j;
                        prime.Add(j);
                    }
                }

                // 골드바흐 파티션 수 판정
                int count = 0;
                for (int j = 3; j <= input / 2; j += 2)
                {

                    if (prime.Contains(j) && prime.Contains(input - j))
                    {

                        count++;
                    }
                }

                sb.AppendLine(count.ToString());
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.Write(sb);
            }
#else

            int len = int.Parse(Console.ReadLine());
            bool[] notPrime = new bool[1000001];

            // 소수를 찾는 방법
            // 소수들을 기록할 때 사용하면 좋다
            for (int i = 2; i <= 1000000; i++)
            {

                if (!notPrime[i])
                {

                    for (int j = i + i; j <= 1000000; j += i)
                    {

                        notPrime[j] = true;
                    }
                }
            }

            // 0, 1은 아래의 로직에 의하면 없어도 무방하다
            notPrime[0] = true;
            notPrime[1] = true;

            for (int i = 0; i < len; i++)
            {

                int input = int.Parse(Console.ReadLine());

                int count = 0;
                for (int j = 2; j < (input / 2) + 1; j++)
                {


                    if (notPrime[j] || notPrime[input - j])
                    {

                        continue;
                    }

                    count++;
                }

                Console.WriteLine(count);
            }

#endif
        }
    }
}
