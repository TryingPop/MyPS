using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 4
이름 : 배성훈
내용 : 골목 대장 호석 - 효율성 2
    문제번호 : 20183번

    다익스트라, 매개 변수 탐색 문제다.
    다익스트라에서 다음 노드를 탐색할 때,
    이미 방문한 곳 체크를 안해 시간초과 났다.
    즉, 다익스트라를 비효율적으로 구현해 3번 틀렸다.

    아이디어는 단순하다.
    도로의 비용이 k 이하인 곳만 최소비용으로 가면서
    c 요금 이내에 도착할 수 있는지 확인하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1518
    {

        static void Main1518(string[] args)
        {

            int n, m, a, b;
            long c;
            List<(int dst, int cost)>[] edge;
            PriorityQueue<(int dst, long cost), long> pq;
            long[] dis;

            Input();

            GetRet();

            void GetRet()
            {

                long ret = BinarySearch();
                if (ret == 1_000_000_001) ret = -1;
                Console.Write(ret);
            }

            long BinarySearch()
            {

                long l = 1;
                long r = 1_000_000_000;

                while (l <= r)
                {

                    long mid = (l + r) >> 1;

                    if (Dijkstra(mid)) r = mid - 1;
                    else l = mid + 1;
                }

                return r + 1;
            }

            bool Dijkstra(long _inf)
            {

                Array.Fill(dis, -1L);

                dis[a] = 0L;
                pq.Clear();
                pq.Enqueue((a, 0), 0);

                while (pq.Count > 0)
                {

                    var node = pq.Dequeue();
                    if (node.cost > c) break;
                    else if (dis[node.dst] != node.cost) continue;

                    for (int i = 0; i < edge[node.dst].Count; i++)
                    {

                        var next = edge[node.dst][i];

                        if (_inf < next.cost) continue;
                        long nextCost = next.cost + node.cost;
                        if (dis[next.dst] == -1 || nextCost < dis[next.dst])
                        {

                            pq.Enqueue((next.dst, nextCost), nextCost);
                            dis[next.dst] = nextCost;
                        }
                    }
                }

                return dis[b] != -1 && dis[b] <= c;
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();
                a = ReadInt();
                b = ReadInt();
                c = ReadLong();

                edge = new List<(int dst, int cost)>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    int cost = ReadInt();

                    edge[f].Add((t, cost));
                    edge[t].Add((f, cost));
                }

                pq = new(m);
                dis = new long[n + 1];

                long ReadLong()
                {

                    long ret = 0L;

                    while (TryReadLong()) { }
                    return ret;

                    bool TryReadLong()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;

                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;

                        ret = c - '0';

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }
}
