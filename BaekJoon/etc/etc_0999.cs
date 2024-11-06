using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 26
이름 : 배성훈
내용 : 패스
    문제번호 : 25559번

    수학, 애드 혹, 해 구성하기 문제다
    1 ~ 10까지 진행해본 결과 홀수는 불가능하다 추론했다
    그리고 짝수를 만드는데 집중했다

    처음에 그냥 N, N - 1, ... 1하면 되지 않을까 제출하니 3번 틀렸다
    이후 조금 고민해보니 앞뒤로 한칸씩 진행하면 어떨까 했고
    실제로 1 ~ N까지 사용해 해당 방법을 제출했다

    풀고나서 고민하니 홀수는 불가능하다
    N을 빼고 이동하는 총 합을 계산해보면 (N - 1) x N / 2 인데
    N은 홀수이므로 (N - 1) / 2가 정수다 해당 수는 즉 N의 배수이다
    이는 모두 이동하면 시작 위치 1로 오게된다

    어느 경우던 이동할 수 있는 곳 중에서 N개를 사용해야 하는데,
    이는 두 번 방문하는 것과 같다
    그래서 N개를 방문할 수 없다
*/

namespace BaekJoon.etc
{
    internal class etc_0999
    {

        static void Main999(string[] args)
        {

            Test();
            void Test()
            {

                int N = 1_000_000;
                int[] arr = new int[N + 1];
                

                // DFS();

                for (int i = 2; i < 15; i += 2)
                {

                    Chk(i);
                }

                void Chk(int _N)
                {

                    int cur = 1;
                    Array.Fill(arr, 0);
                    for (int i = _N; i >= 1; i--)
                    {

                        int next = (cur + i);
                        if (next > _N) next -= _N;
                        arr[next]++;
                        cur = next;
                    }

                    for (int i = 1; i <= _N; i++)
                    {

                        if (arr[i] == 1) continue;

                        Console.Write($"{_N} : Impossible!\n");
                        break;
                    }
                }

                bool[] use = new bool[N + 1];
                int[] order = new int[N];
                void DFS(int _depth = 0, int _cur = 0)
                {

                    if (_depth == N)
                    {

                        for (int i = 0; i < N; i++)
                        {

                            if (arr[i] != 1) return;
                        }

                        for (int i = 0; i < N; i++)
                        {

                            Console.Write($"{order[i]} ");
                        }
                        Console.Write("\n존재함!\n");
                        return;
                    }

                    for (int i = 1; i <= N; i++)
                    {

                        if (use[i]) continue;
                        use[i] = true;
                        int next = (_cur + i) % N;
                        arr[next]++;
                        order[_depth] = i;
                        DFS(_depth + 1, next);
                        arr[next]--;
                        use[i] = false;
                    }
                }
            }


            // Solve();
            void Solve()
            {

                int n = int.Parse(Console.ReadLine());

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                if ((n & 1) == 0)
                {

                    int len = n >> 1;
                    for (int i = 0; i < n; i += 2)
                    {

                        sw.Write($"{n - i} ");
                        sw.Write($"{i + 1} ");
                    }
                }
                else if (n == 1) sw.Write(1);
                else sw.Write(-1);

                sw.Close();
            }

        }
    }
}
