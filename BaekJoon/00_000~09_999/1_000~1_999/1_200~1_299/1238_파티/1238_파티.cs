using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 18
이름 : 배성훈
내용 : 파티
    문제번호 : 1238번

    풀이 아이디어는 다음과 같다
    우선 음의 간선이 없고, 중복되는 간선이 없어 다익스트라로 썼다!
    먼저 파티 열리는 장소에서 다른 지점으로 가는 다익스트라를 구한다
    그러면 파티가 끝나고 파티장에서 집으로 가는 최단 거리를 찾을 수 있다
    그리고 사람들이 파티장으로 가는 최단거리는
    역방향으로 경로들을 저장해서 파티장에서 다익스트라를 한다
    그러면 파티장으로 오는 최단경로들이 저장되어져 있다!

    다른사람 풀이를 보니, 다익스트라 로직을 다시 짜는게 좋아보인다;
    여태까지 한 방법은 더 많은 메모리를 쓰는거처럼 보인다

    해당 경우 N을 사용해서 거리 값을 비교하며 넣어주는 방식을 취했는데,
    다른사람 풀이보니, 거리 값을 구지 저장할 필요가 없어 보인다

    해당 방법으로 다시 작성해보니 메모리는 확실히 줄었고 속도도 조금 빨라졌는데, 속도 부분은 모르겠다;
    앞으로는 해당 방법으로 다익스트라를 써야겠다

    >>> https://great-park.tistory.com/133
    에 시간 복잡도가 나와있다
*/

namespace BaekJoon.etc
{
    internal class etc_0057
    {

        static void Main57(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int l = ReadInt(sr);
            // 목적지
            int x = ReadInt(sr);
            List<(int dst, int dis)>[] lines = new List<(int dst, int dis)>[n + 1];
            List<(int dst, int dis)>[] reverseLines = new List<(int dst, int dis)>[n + 1];

            for (int i = 1; i <= n; i++)
            {

                lines[i] = new();
                reverseLines[i] = new();
            }

            for (int i = 0; i < l; i++)
            {

                int from = ReadInt(sr);
                int to = ReadInt(sr);
                int dis = ReadInt(sr);

                // 시작점과 끝점이 정해지면 해당 단방향 간선은 유일!
                lines[from].Add((to, dis));
                reverseLines[to].Add((from, dis));
            }
            sr.Close();

            // 다익스트라
            int[] goHome = new int[n + 1];
            Array.Fill(goHome, 10_000_000);
            int[] goParty = new int[n + 1];
            Array.Fill(goParty, 10_000_000);


#if first
            PriorityQueue<(int dst, int dis), int> q = new();
            q.Enqueue((x, 0), 0);
            goHome[x] = 0;

            while(q.Count > 0)
            {

                var node = q.Dequeue();

                int cur = node.dst;
                int curDis = node.dis;
                if (node.dis > goHome[cur]) continue;

                for (int i = 0; i < lines[cur].Count; i++)
                {

                    var next= lines[cur][i];
                    int nextDis = next.dis + curDis;

                    if (goHome[next.dst] <= nextDis) continue;


                    q.Enqueue((next.dst, nextDis), nextDis);
                    goHome[next.dst] = nextDis;
                }
            }

            q.Enqueue((x, 0), 0);
            goParty[x] = 0;

            while (q.Count > 0)
            {

                var node = q.Dequeue();

                int cur = node.dst;
                int curDis = node.dis;

                if (node.dis > goParty[cur]) continue;

                for (int i = 0; i < reverseLines[cur].Count; i++)
                {

                    var next = reverseLines[cur][i];
                    int nextDis = next.dis + curDis;

                    if (goParty[next.dst] <= nextDis) continue;

                    q.Enqueue((next.dst, nextDis), nextDis);
                    goParty[next.dst] = nextDis;
                }
            }
#else
            PriorityQueue<int, int> q = new PriorityQueue<int, int>();

            goHome[x] = 0;
            bool[] visit = new bool[n + 1];
            q.Enqueue(x, 0);

            while(q.Count > 0)
            {

                int node = q.Dequeue();

                if (visit[node]) continue;

                visit[node] = true;

                for (int i = 0; i < lines[node].Count; i++)
                {

                    int next = lines[node][i].dst;

                    if (visit[next]) continue;

                    int nextDis = goHome[node] + lines[node][i].dis;
                    if (goHome[next] <= nextDis) continue;
                    goHome[next] = nextDis;
                    q.Enqueue(next, nextDis);
                }
            }

            goParty[x] = 0;

            q.Enqueue(x, 0);
            while (q.Count > 0)
            {

                int node = q.Dequeue();

                if (!visit[node]) continue;

                visit[node] = false;

                for (int i = 0; i < reverseLines[node].Count; i++)
                {

                    int next = reverseLines[node][i].dst;

                    if (!visit[next]) continue;

                    int nextDis = goParty[node] + reverseLines[node][i].dis;
                    if (nextDis >= goParty[next]) continue;
                    goParty[next] = nextDis;
                    q.Enqueue(next, nextDis);
                }
            }
#endif

            int max = 0;
            for (int i = 1; i <= n; i++)
            {

                int chk = goHome[i] + goParty[i];
                if (max < chk) max = chk;
            }

            Console.WriteLine(max);
        }

        

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0, c;

            while ((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
int n = ScanInt(), m = ScanInt(), x = ScanInt();
var edges = new List<Edge>[n + 1];
var reversedEdges = new List<Edge>[n + 1];
for (int i = 1; i <= n; i++)
{
    edges[i] = new();
    reversedEdges[i] = new();
}
for (int i = 0; i < m; i++)
{
    int from = ScanInt(), to = ScanInt(), cost = ScanInt();
    edges[from].Add(new(to, cost));
    reversedEdges[to].Add(new(from, cost));
}

var heap = new PriorityQueue<int, int>();
var costs = new int[n + 1];
var visit = new bool[n + 1];
Dijkstra(reversedEdges);
Dijkstra(edges);
Console.Write(costs.Max());

void Dijkstra(List<Edge>[] edges)
{
    heap.Enqueue(x, 0);
    while (heap.TryDequeue(out var dest, out var cost))
    {
        if (visit[dest])
            continue;
        visit[dest] = true;
        costs[dest] += cost;
        foreach (var e in edges[dest])
        {
            if (!visit[e.Dest])
                heap.Enqueue(e.Dest, cost + e.Cost);
        }
    }
    Array.Fill(visit, false);
}

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
        ret = 10 * ret + (c - '0');
    }
    return ret;
}

struct Edge
{
    public int Dest;
    public int Cost;

    public Edge(int dest, int cost)
    {
        Dest = dest;
        Cost = cost;
    }
}
#endif
}
