using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 12
이름 : 배성훈
내용 : 수
    문제번호 : 22943번

    조건 2를 잘못해서 여러번 틀렸다;
    아이디어는 간단하다

    조건 1의 경우
    홀수는 2 + ?의 형태만 가능하다!
    그래서 2를 뺀게 소수인지 판정했다

    다음으로 짝수의 경우는 골드바흐의 추측을 보고 하나의 가정을 세웠다
    10만 이하의 숫자에 한해서 2, 4, 6을 제외하면 서로 다른 두 소수의 합으로 표현 가능하다
    실제로 에라토스 테네스 체이론을 써서 확인해본 결과 참이었다!
    전처리어 Lemma 부분 돌려보면 안되는 경우가 3임(2, 4, 6)을 알 수 있다

    그리고 조건 2의 경우 이제 두 소수의 곱인지만 확인했다
    만약 a가 2이상인 수 j에 대해 나눠떨어질 때,
    a / j 가 소수인지 확인하면 된다
    j는 +1 씩 확장해오므로 처음 발견에선 소수가 보장된다!

    여러 번 틀렸으나 256ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0207
    {

        static void Main207(string[] args)
        {

            int[] info = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            bool[] visit = new bool[10];

            int ret = 0;
            List<int> arr = new((int)Math.Pow(10, info[0]));

            bool[] primes = new bool[100_000];
            for (int i = 2; i < primes.Length; i++)
            {

                primes[i] = true;
                for (int j = 2; j < i; j++)
                {

                    if (j * j > i) break;
                    if (i % j != 0) continue;

                    primes[i] = false;
                    break;
                }
            }

#if Lemma
            int lemma = 0;
            for (int i = 2; i < 100_000; i += 2)
            {

                int half = i / 2;

                bool chk = true;
                for (int j = 2; j < half; j++)
                {

                    int calc1 = i - j;
                    int calc2 = j;

                    if (primes[calc1] && primes[calc2]) 
                    { 
                        
                        chk = false;
                        break;
                    }
                }

                if (chk) lemma += 1;
            }

            Console.WriteLine(lemma);
#endif
            DFS(visit, 0, info[0], 0, arr);
            foreach (int item in arr)
            {

                if ((item & 1) == 1)
                {

                    int chk = item - 2;
                    if (chk < 2 || !primes[chk]) continue;
                }
                else if (item < 8) continue;

                int calc = item;

                // 여기를 잘못짜서 여러번 틀렸다
                while (calc % info[1] == 0)
                {

                    calc /= info[1];
                }

                // 두 소수의 곱으로 표현되는지 확인
                for (int j = 2; j < calc; j++)
                {

                    if (j * j > calc) break;
                    if (calc % j != 0) continue;

                    if (primes[calc / j]) ret++;
                    break;
                }

            }

            Console.WriteLine(ret);
        }
        

        static void DFS(bool[] _visit, int _depth, int _max, int _curVal, List<int> _arr)
        {

            if (_depth == _max)
            {

                _arr.Add(_curVal);
            }
            
            int s = _depth == 0 ? 1 : 0;
            for (int i = s; i < 10; i++)
            {

                if (_visit[i]) continue;
                _visit[i] = true;

                DFS(_visit, _depth + 1, _max, _curVal * 10 + i, _arr);
                _visit[i] = false;
            }
        }
    }
}
