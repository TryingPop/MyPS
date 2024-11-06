using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 17
이름 : 배성훈
내용 : 알고리즘 수업 - 깊이 우선 탐색 5
    문제번호 : 24483번

    DFS, 정렬 문제다
    depth * time을 누적합 한 값을 찾아야한다
    di를 노드번호로 읽어 한 번 틀렸고,
    두 번째로 시작값을 잘못 설정해 한 번 더 틀렸다

    이를 수정하니 이상없이 통과했다
    그리고 정렬 부분은 우선순위 큐를 이용해 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0820
    {

        static void Main820(string[] args)
        {

            StreamReader sr;
            int n, m;
            int s;

            PriorityQueue<int, int>[] line;

            long time = 1;
            bool[] visit;

            Solve();
            void Solve()
            {

                Input();

                long ret = DFS(s);

                Console.Write(ret);
            }

            long DFS(int _cur, int _depth = 0)
            {

                if (visit[_cur]) return 0L;
                visit[_cur] = true;

                long ret = _depth * time++;

                while (line[_cur].Count > 0)
                {

                    int next = line[_cur].Dequeue();
                    if (visit[next]) continue;

                    ret += DFS(next, _depth + 1);
                }

                return ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();
                s = ReadInt();

                visit = new bool[n + 1];
                line = new PriorityQueue<int, int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    line[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    line[f].Enqueue(b, b);
                    line[b].Enqueue(f, f);
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;

                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
