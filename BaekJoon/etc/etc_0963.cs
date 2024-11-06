using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 12
이름 : 배성훈
내용 : 도로포장
    문제번호 : 1162번

    dp, 다익스트라 문제다
    거리가 음이아닌 정수이므로 다익스트라로 
    최단 경로를 찾을 수 있다

    그리고 해당 경로의 비용을 0으로 만드는 부분은
    0원을 a번 사용한 노드로 가는 식으로 생각했다
    a -> a + 1로 갈 수 는 있지만 
    a -> a -1로 가는 간선은 존재하지 않는다!

    이렇게 다익스트라를 이용해 풀고
    k번 사용한 것들을 조사하면서 
    최단 거리를 찾아 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0963
    {

        static void Main963(string[] args)
        {

            long INF = 1_000_000_000_000;
            StreamReader sr;

            PriorityQueue<(int n, int k), long> pq;
            long[][] dis;
            bool[][] visit;
            List<(int dst, int dis)>[] edge;

            int n, m, k;

            Solve();
            void Solve()
            {

                Input();

                Dijkstra();

                GetRet();
            }

            void GetRet()
            {

                long ret = INF;
                for (int i = 0; i <= k; i++)
                {

                    ret = Math.Min(ret, dis[n][i]);
                }

                Console.Write(ret);
            }

            void Dijkstra()
            {

                pq = new(m * k);
                pq.Enqueue((1, 0), 0);
                dis[1][0] = 0;

                while(pq.Count > 0)
                {

                    (int n, int k) node = pq.Dequeue();

                    if (visit[node.n][node.k]) continue;
                    visit[node.n][node.k] = true;

                    long curDis = dis[node.n][node.k];
                    for (int i = 0; i < edge[node.n].Count; i++)
                    {

                        int next = edge[node.n][i].dst;
                        if (visit[next][node.k]) continue;

                        if (node.k < k && curDis < dis[next][node.k + 1])
                        {

                            dis[next][node.k + 1] = curDis;
                            pq.Enqueue((next, node.k + 1), curDis);
                        }

                        long nDis = curDis + edge[node.n][i].dis;
                        if (dis[next][node.k] <= nDis) continue;

                        dis[next][node.k] = nDis;
                        pq.Enqueue((next, node.k), nDis);
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();
                k = ReadInt();

                edge = new List<(int dst, int dis)>[n + 1];
                dis = new long[n + 1][];
                visit = new bool[n + 1][];

                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                    dis[i] = new long[k + 1];
                    visit[i] = new bool[k + 1];
                    Array.Fill(dis[i], INF);
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();
                    int d = ReadInt();

                    edge[f].Add((b, d));
                    edge[b].Add((f, d));
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

#if other
using ProblemSolving.Templates.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace ProblemSolving.Templates.Utility {}
namespace System {}
namespace System.Collections.Generic {}
namespace System.IO {}
namespace System.Linq {}

#nullable disable

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        Solve(sr, sw);
    }

    public static void Solve(StreamReader sr, StreamWriter sw)
    {
        var (n, m, k) = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

        var graph = new List<(int dst, long cost)>[n];
        for (var idx = 0; idx < n; idx++)
            graph[idx] = new List<(int dst, long cost)>();

        for (var idx = 0; idx < m; idx++)
        {
            var (s, e, c) = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            s--;
            e--;

            graph[s].Add((e, c));
            graph[e].Add((s, c));
        }

        var pq = new PriorityQueue<(int pos, int fastmove), long>();
        var dist = new long[n, 1 + k];
        for (var idx = 0; idx < n; idx++)
            for (var jdx = 0; jdx <= k; jdx++)
                dist[idx, jdx] = Int64.MaxValue;

        pq.Enqueue((0, 0), 0);
        dist[0, 0] = 0;

        while (pq.TryDequeue(out var state, out var costSum))
        {
            var (pos, fastmove) = state;
            if (dist[pos, fastmove] != costSum)
                continue;

            if (pos == n - 1)
            {
                sw.WriteLine(costSum);
                return;
            }

            foreach (var (dst, cost) in graph[pos])
            {
                if (fastmove < k && costSum < dist[dst, 1 + fastmove])
                {
                    dist[dst, 1 + fastmove] = costSum;
                    pq.Enqueue((dst, 1 + fastmove), costSum);
                }
                if (cost + costSum < dist[dst, fastmove])
                {
                    dist[dst, fastmove] = cost + costSum;
                    pq.Enqueue((dst, fastmove), cost + costSum);
                }
            }
        }
    }
}

namespace ProblemSolving.Templates.Utility
{
    public static class DeconstructHelper
    {
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2) => (v1, v2) = (arr[0], arr[1]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3) => (v1, v2, v3) = (arr[0], arr[1], arr[2]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4) => (v1, v2, v3, v4) = (arr[0], arr[1], arr[2], arr[3]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5) => (v1, v2, v3, v4, v5) = (arr[0], arr[1], arr[2], arr[3], arr[4]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6) => (v1, v2, v3, v4, v5, v6) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6, out T v7) => (v1, v2, v3, v4, v5, v6, v7) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6, out T v7, out T v8) => (v1, v2, v3, v4, v5, v6, v7, v8) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6], arr[7]);
    }
}

#elif other2

#endif
}
