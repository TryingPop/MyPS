using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 7
이름 : 배성훈
내용 : 1로 만들기 2
    문제번호 : 12852번

    동적계획법과 최단거리 역추적 단원이다

    아이디어가 안떠올라서
    일단은 BFS로 dp를 만들고, 이후에 추적하는 연산을 했다
    여기서 dp는 횟수를 기록한 것일뿐, 경로를 저장하지 않았다
    다른 사람들 풀이에 비하면 비효율적이다

    그래서 다른 사람의 풀이를 보니 dp에 횟수를 저장하는게 아닌,
    dp를 해당 시행에서 경로를 저장하는 용도로 사용했다 즉, dp에는 이전 수를 저장했다
    문제가 단일 문제이므로 해당 풀이가 더 좋아보인다
*/

namespace BaekJoon._35
{
    internal class _35_01
    {

        static void Main1(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            int[] dp = new int[n + 1];
            dp[n] = -1;
#if first
            // Dp 채우기
            Queue<int> q = new Queue<int>();
            q.Enqueue(1);
            while (q.Count > 0)
            {

                int node = q.Dequeue();

                int cur = dp[node];

                for (int i = 0; i < 3; i++)
                {

                    int next = Next(node, i);
                    if (next > n) continue;

                    if (dp[next] != 0) continue;
                    dp[next] = cur + 1;
                    q.Enqueue(next);
                }

                if (dp[n] != 0) q.Clear();
            }

            StringBuilder sb = new StringBuilder();

            int result = n;

            sb.AppendLine(dp[result].ToString());

            // 역 추적
            while (result != 1)
            {

                sb.Append(result);
                sb.Append(' ');

                if (dp[result] == dp[result - 1] + 1) result -= 1;
                else if (result % 3 == 0 && dp[result] == dp[result / 3] + 1) result /= 3;
                else result /= 2;
            }

            sb.Append(1);

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            sw.Write(sb);
            sw.Close();
#else

            int step = 0;

            // BFS 탐색 - 여기서 dp는 이전 결과물이 담겨있다
            Queue<(int n, int step)> q = new Queue<(int n, int step)>();
            q.Enqueue((n, 0));

            while(q.Count > 0)
            {

                (int n, int step) node = q.Dequeue();

                if (node.n == 1)
                {

                    step = node.step;
                    q.Clear();
                    break;
                }

                if (node.n % 3 == 0 && dp[node.n / 3] == 0)
                {

                    dp[node.n / 3] = node.n;
                    q.Enqueue((node.n / 3, node.step + 1));
                }

                if (node.n % 2 == 0 && dp[node.n / 2] == 0)
                {

                    dp[node.n / 2] = node.n;
                    q.Enqueue((node.n / 2, node.step + 1));
                }

                if (dp[node.n - 1] == 0)
                {

                    dp[node.n - 1] = node.n;
                    q.Enqueue((node.n - 1, node.step + 1));
                }
            }

            // 정답 기록
            Stack<int> result = new Stack<int>(step);
            int next = 1;
            while(next != -1)
            {

                result.Push(next);
                next = dp[next];
            }

            // 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(step);

                for (int i = 0; i < step + 1; i++)
                {

                    sw.Write(result.Pop());
                    sw.Write(' ');
                }
            }
#endif
        }

        static int Next(int _n, int _idx)
        {

            switch (_idx)
            {

                case 0:
                    return _n * 3;

                case 1:
                    return _n * 2;

                default:
                    return _n + 1;
            }
        }

    }
}
