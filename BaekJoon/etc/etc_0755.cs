using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 7
이름 : 배성훈
내용 : 웜홀
    문제번호 : 1865번

    벨만 포드 문제다
    음의 사이클이 존재하는지 확인하는 문제다
    
    영역별로 음의 사이클이 존재하는지 확인한다
    적어도 하나 존재하면 과거로 갈 수 있다

    이렇게 제출하니 300ms 쯤에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0755
    {

        static void Main755(string[] args)
        {

            int INF = 100_000_000;
            string YES = "YES\n";
            string NO = "NO\n";

            StreamReader sr;
            StreamWriter sw;

            List<(int dst, int dis)>[] line;
            int n, m, w;
            int[] dis;

            Solve();
            void Solve()
            {

                Init();

                int test = ReadInt();

                while(test-- > 0)
                {

                    Input();

                    Array.Fill(dis, INF, 1, n);

                    bool ret = false;
                    for (int i = 1; i <= n; i++)
                    {

                        if (dis[i] < INF || BellmanFord(i)) continue;
                        ret = true;
                        break;
                    }

                    sw.Write(ret ? YES : NO);
                }

                sr.Close();
                sw.Close();
            }

            bool BellmanFord(int _s)
            {

                
                dis[_s] = 0;
                for (int cycle = 1; cycle <= n; cycle++)
                {

                    for (int i = 1; i <= n; i++)
                    {

                        // 아직 방문 X면 컷!
                        if (dis[i] == INF) continue;

                        int cur = dis[i];
                        for (int j = 0; j < line[i].Count; j++)
                        {

                            int next = line[i][j].dst;
                            int nDis = line[i][j].dis + cur;

                            if (dis[next] > nDis)
                            {

                                dis[next] = nDis;

                                // n번째에도 간선의 정보 갱신이 있다는 것은
                                // 음의 사이클이 있다
                                if (cycle == n) return false;
                            }
                        }
                    }
                }

                return true;
            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();
                w = ReadInt();

                for (int i = 1; i <= n; i++)
                {

                    line[i].Clear();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();
                    int dis = ReadInt();

                    line[f].Add((b, dis));
                    line[b].Add((f, dis));
                }

                for (int i = 0; i < w; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();
                    int dis = ReadInt();

                    line[f].Add((b, -dis));
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                line = new List<(int dst, int dis)>[501];
                for (int i = 1; i <= 500; i++)
                {

                    line[i] = new();
                }

                dis = new int[501];
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
//Question No: 1865
//Title: 웜홀
//Tier: Gold III
namespace Joy
{
    class Q1865
    {
        static List<(int, int)>[] adj;
        static int INF = 1000000007;

        static void CheckTimeShift(int N, int src)
        {
            int[] upper = new int[N + 1];
            for (int i = 1; i <= N; i++)
            {
                upper[i] = INF;
            }

            bool updated = false;
            upper[src] = 0;

            for (int iter = 0; iter < N; iter++)
            {
                updated = false;

                for (int here = 1; here <= N; here++)
                {
                    foreach ((int there, int cost) in adj[here])
                    {
                        if (upper[there] > upper[here] + cost)
                        {
                            upper[there] = upper[here] + cost;
                            updated = true;
                        }
                    }
                }

                if (!updated)
                {
                    Console.WriteLine("NO");
                    return;
                }
            }

            if (updated)
            {
                Console.WriteLine("YES");
            }
        }

        static void Main()
        {
            int TC = int.Parse(Console.ReadLine());

            for (int k = 0; k < TC; k++)
            {
                int[] values = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                int N = values[0];
                int M = values[1];
                int W = values[2];

                adj = new List<(int, int)>[N + 1];
                for (int i = 1; i <= N; i++)
                {
                    adj[i] = new List<(int, int)>();
                }

                for (int i = 0; i < M; i++)
                {
                    values = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                    int start = values[0];
                    int end = values[1];
                    int time = values[2];
                    adj[start].Add((end, time));
                    adj[end].Add((start, time));
                }

                for (int i = 0; i < W; i++)
                {
                    values = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                    int start = values[0];
                    int end = values[1];
                    int time = values[2];
                    adj[start].Add((end, -time));
                }

                CheckTimeShift(N, 1);
            }
        }
    }

}
#elif other2
var reader = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

int T = int.Parse(reader.ReadLine());

while (T-- > 0)
{
    var NMW = reader.ReadLine().Split();
    int N = int.Parse(NMW[0]);
    int M = int.Parse(NMW[1]);
    int W = int.Parse(NMW[2]);

    var wormhole = new Wormhole(N, M, W);

    for (int i = 0; i < M; i++)
        wormhole.AddEdge(reader.ReadLine(), false);

    for (int i = 0; i < W; i++)
        wormhole.AddEdge(reader.ReadLine(), true);

    bool result = false;
    var dist = new int[N + 1];
    result = wormhole.BellmanFord(1);

    Console.WriteLine(result ? "YES" : "NO");
}

class Wormhole
{
    private int vertices;
    
    private (int u, int v, int w)[] _edges;

    private int count;

    private static readonly int INF = (int)1e9;

    public Wormhole(int verticies, int roads, int holes)
    {
        this.vertices = verticies;
        this._edges = new (int, int, int)[roads * 2 + holes];
        this.count = 0;
    }

    public void AddEdge(string line, bool isWormhole)
    {
        var sp = line.Split();
        int u = int.Parse(sp[0]);
        int v = int.Parse(sp[1]);
        int w = int.Parse(sp[2]);

        AddEdge(u, v, w, isWormhole);
    }

    public void AddEdge(int u, int v, int weight, bool isWormhole)
    {
        if (isWormhole == true)
        {
            this._edges[count++] = (u, v, -weight);
        }
        else // Undirected
        {
            this._edges[count++] = (u, v, weight);
            this._edges[count++] = (v, u, weight);
        }
    }

    public bool BellmanFord(int start)
    {
        var distance = new int[vertices + 1];
        Array.Fill(distance, INF);
        distance[start] = 0;

        for (int i = 1; i <= vertices; i++)
        {
            foreach (var e in _edges)
            {
                if (distance[e.v] > distance[e.u] + e.w)
                {
                    distance[e.v] = distance[e.u] + e.w;

                    if (i == vertices)
                        return true;
                }
            }
        }

        return false;
    }
}
#endif
}
