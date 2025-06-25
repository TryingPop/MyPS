using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 5
이름 : 배성훈
내용 : 두 단계 최단 경로 1
    문제번호 : 23793번

    다익스트라, 최단 경로 문제다
    x -> z로 가는데 y를 거치고 안거치고 가느냐 문제다
    
    아이디어는 다음과 같다
    y를 거치지 않는건 다익스트라 중 y를 방문하는 간선을 모두 무시한 뒤
    최단 경로를 찾으면 된다

    그리고 y를 거쳐가는 건 그리디 알고리즘으로
    x -> y로가는 최단경로와 y에서 z로 가는 최단경로의 합이 된다

    단순 합으로 제출해 x -> y, y -> z인 경로가 존재하지 않는 경우 
    -2가 나올 수 있다 해당 부분을 처리 안해 1번 틀렸다
    이후 반례 처리하니 이상없이 통과한다
*/

namespace BaekJoon.etc
{
    internal class etc_1028
    {

        static void Main1028(string[] args)
        {

            int INF = 2_000_000_000;

            StreamReader sr;

            int n, m;
            List<(int dst, int dis)>[] edge;
            PriorityQueue<(int dst, int dis), int> pq;
            bool[] visit;
            int[] dis;
            int x, y, z;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Dijkstra(x);
                int chk1 = dis[y];

                Dijkstra(y);
                int chk2 = dis[z];

                int ret1;
                if (chk1 == -1 || chk2 == -1) ret1 = -1;
                else ret1 = chk1 + chk2;

                Dijkstra(x, y);
                int ret2 = dis[z];

                Console.Write($"{ret1} {ret2}");
            }

            void Dijkstra(int _s, int _pop = -1)
            {

                Array.Fill(dis, -1);
                Array.Fill(visit, false);
                dis[_s] = 0;

                pq.Enqueue((_s, 0), 0);
                while (pq.Count > 0)
                {

                    var node = pq.Dequeue();
                    if (visit[node.dst]) continue;
                    visit[node.dst] = true;
                    dis[node.dst] = node.dis;

                    for (int i = 0; i < edge[node.dst].Count; i++)
                    {

                        int next = edge[node.dst][i].dst;
                        if (visit[next] || next == _pop) continue;
                        int nDis = node.dis + edge[node.dst][i].dis;

                        pq.Enqueue((next, nDis), nDis);
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                edge = new List<(int dst, int dis)>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();
                    int dis = ReadInt();

                    edge[f].Add((b, dis));
                }

                x = ReadInt();
                y = ReadInt();
                z = ReadInt();

                pq = new(n);
                visit = new bool[n + 1];
                dis = new int[n + 1];
                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
