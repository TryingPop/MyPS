using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 17
이름 : 배성훈
내용 : 야바위 게임
    문제번호 : 23741번

    dp, BFS 탐색 문제다
    아이디어는 다음과 같다
    양방향 경로 이므로 만약 a에 i초에 도착했다면
    a에서 아무 경로나 잡고 왔다갔다를 반복하면 i + 2, i + 4, ... i + 6초에 언제든지 갈 수 있다
    그래서 i 번째에 해당 장소에 도착했다면 i + 2, i + 4번째에 도착여부는 항상 가능하다
    마찬가지로 i + 1번째에 a를 통해 경유한 장소 b들은 i + 3 에 방문 가능하기에
    홀수번째나 짝수번째에 해당 지점을 반복했다면 다음 홀수번째나 짝수번째에 항상 갈 수 있기에 해당 노드 탐색을 중지한다

    그래서 홀수턴, 짝수턴에 해당 노드의 방문 여부로 dp를 잡고
    턴을 진행하면서 홀짝 턴에 이미 방문한 곳이면 재탐색안하는 식으로 코드를 구현했다
    이에 결과는 결과 시간이 홀수면 홀수턴에 방문 가능한 노드들을 출력하며 되고
    결과 시간이 짝수면 짝수턴에 진입한 노드들을 출력하면 된다

    그리고 노드 탐색부분은 BFS 탐색을 했다
    dp?로 중간에 재진입을 끊기에 dp가 백트래킹 역할을 해줘서 DFS로도 쉽게 구현해도된다

    아래는 이러한 아이디어를 코드로 구현한 것이다
*/

namespace BaekJoon.etc
{
    internal class etc_0274
    {

        static void Main274(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int m = ReadInt(sr);

            int s = ReadInt(sr);
            int t = ReadInt(sr);

            List<int>[] lines = new List<int>[n + 1];
            for (int i = 1; i <= n; i++)
            {

                lines[i] = new();
            }

            for (int i = 0; i < m; i++)
            {

                int f = ReadInt(sr);
                int b = ReadInt(sr);

                lines[f].Add(b);
                lines[b].Add(f);
            }

            sr.Close();

            if (lines[s].Count == 0)
            {

                // 예제 중에 이동 못하는데 이동했다고 했으므로
                // -1을 출력
                Console.WriteLine(-1);
                return;
            }

            // 앞은 노드번호
            // 뒤는 1 : 홀수턴, 0 : 짝수턴을 의미
            bool[,] dp = new bool[n + 1, 2];

            // 처음 0초는 짝수턴
            dp[s, 0] = true;

            Queue<int> q = new(n);
            q.Enqueue(s);
            Queue<int> calc = new(n);
            for (int i = 0; i < t; i++)
            {

                // 현재 이동하는 턴이
                // 홀수턴 짝수턴 확인
                int nextTime = i % 2 == 0 ? 1 : 0;
                while(q.Count > 0)
                {

                    // 지난 방문 노드
                    var node = q.Dequeue();

                    for (int j = 0; j < lines[node].Count; j++)
                    {

                        // 다음 노드
                        int next = lines[node][j];
                        // 반복 진입인지 확인
                        if (dp[next, nextTime]) continue;
                        // 재진입 막기용
                        dp[next, nextTime] = true;
                        calc.Enqueue(next);
                    }
                }

                Queue<int> temp = q;
                q = calc;
                calc = temp;
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                int last = t % 2;

                for (int i = 1; i <= n; i++)
                {

                    if (!dp[i, last]) continue;
                    sw.Write(i);
                    sw.Write(' ');
                }
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
