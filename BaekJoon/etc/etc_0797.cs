using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 6
이름 : 배성훈
내용 : 최소비용 구하기
    문제번호 : 1916번

    다익스트라 문제다
    조건대로 구현했다
*/
namespace BaekJoon.etc
{
    internal class etc_0797
    {

        static void Main797(string[] args)
        {

            int MAX = 100_000_000;
            StreamReader sr;
            PriorityQueue<int, int> q;

            List<(int dst, int cost)>[] line;
            int n;
            int s, e;
            int ret;

            Solve();
            void Solve()
            {

                Input();

                Dijstra();

                Console.Write(ret);
            }

            void Dijstra()
            {

                int[] dis = new int[n + 1];
                bool[] visit = new bool[n + 1];

                Array.Fill(dis, MAX);

                q = new(n);
                dis[s] = 0;
                q.Enqueue(s, 0);
                while(q.Count > 0)
                {

                    int node = q.Dequeue();
                    if (visit[node]) continue;
                    visit[node] = true;

                    int cur = dis[node];

                    for (int i = 0; i < line[node].Count; i++)
                    {

                        int next = line[node][i].dst;
                        if (visit[next]) continue;

                        int nextCost = line[node][i].cost + cur;

                        if (dis[next] <= nextCost) continue;

                        dis[next] = nextCost;
                        q.Enqueue(next, nextCost);
                    }
                }

                ret = dis[e];
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                int len = ReadInt();

                line = new List<(int dst, int cost)>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    line[i] = new();
                }

                for (int i = 0; i < len; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    int c = ReadInt();

                    line[f].Add((t, c));
                }

                s = ReadInt();
                e = ReadInt();

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

#if other
using System;
using System.Collections.Generic;
using System.IO;

int n, s, d;
List<ValueTuple<int, int>>[] edges;
using (var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput())))
{
    n = ScanInt();
    var m = ScanInt();
    edges = new List<ValueTuple<int, int>>[n + 1];
    for (int i = 0; i < m; i++)
    {
        int start = ScanInt(), dest = ScanInt(), cost = ScanInt();
        edges[start] ??= new();
        edges[start].Add((dest, cost));
    }
    s = ScanInt();
    d = ScanInt();

    int ScanInt()
    {
        int c, ret = 0;
        while ((c = sr.Read()) != '\n' && c != ' ' && c != -1)
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            ret = 10 * ret + c - '0';
        }
        return ret;
    }
}

var dist = new int[n + 1];
for (int i = 1; i < dist.Length; i++)
    dist[i] = int.MaxValue;

var visited = new bool[n + 1];
var heap = new PriorityQueue<int, int>();
heap.Enqueue(s, 0);
dist[s] = 0;
while (heap.TryDequeue(out int dest, out int cost))
{
    if (visited[dest])
        continue;
    visited[dest] = true;
    var curEdges = edges[dest];
    if (curEdges == null)
        continue;
    foreach ((var nextDest, var nextCost) in curEdges)
    {
        if (visited[nextDest])
            continue;
        var newCost = nextCost + cost;
        if (dist[nextDest] > newCost)
        {
            dist[nextDest] = newCost;
            heap.Enqueue(nextDest, newCost);
        }
    }
}
Console.Write(dist[d]);
#endif
}
