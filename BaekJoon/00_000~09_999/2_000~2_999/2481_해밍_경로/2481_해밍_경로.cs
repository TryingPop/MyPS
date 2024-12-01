using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 1
이름 : 배성훈
내용 : 해밍 경로
    문제번호 : 2481번

    다익스트라 문제다.
    이동 가능한 장소의 거리가 1이므로 BFS로 봐도 무방하다.
    비트가 1개 다른건 비트 연산자 ^ 로 찾을 수 있다.
    
    두 원소에 ^연산을 해서 1 << j 인 0 <= j < m 인 j가 존재하면
    간선이 존재한다. 이 연산을 변환해 원소 a에 a ^ (1 << j) = b인 원소 b가
    존재하면 b와 간선이 존재한다고 볼 수 있다.

    그래서 해시로 b들을 저장해 원소 b가 존재하는지 확인해 간선을 이었다.
    이후 이어진 간선으로 다익스트라를 통해 이전 노드와 거리를 저장했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1139
    {

        static void Main1139(string[] args)
        {

            StreamReader sr;
            int n, m;
            int[] arr, prev, dis;
            Dictionary<int, int> dic;
            List<int>[] edge;
            Solve();
            void Solve()
            {

                Input();

                SetEdge();

                Dijkstra();

                Query();
            }

            void Query()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int q = ReadInt();
                int[] stk = new int[n];
                int len = 0;

                for (int i = 0; i < q; i++)
                {

                    int dst = ReadInt() - 1;

                    if (dis[dst] == -1)
                    {

                        sw.Write("-1\n");
                        continue;
                    }

                    len = 0;
                    while (dst != -1)
                    {

                        stk[len++] = dst + 1;
                        dst = prev[dst];
                    }

                    while (len-- > 0)
                    {

                        sw.Write($"{stk[len]} ");
                    }

                    sw.Write('\n');
                }

                sw.Close();
                sr.Close();
            }

            void SetEdge()
            {

                // 간선 연결
                edge = new List<int>[n];
                for (int i = 0; i < n; i++)
                {

                    edge[i] = new();
                }

                foreach (var item in dic)
                {

                    for (int i = 0; i < m; i++)
                    {

                        int cur = item.Key ^ (1 << i);
                        if (dic.ContainsKey(cur)) edge[item.Value].Add(dic[cur]);
                    }
                }
            }

            void Dijkstra()
            {

                // 거리가 1이므로 그냥 큐 쓴다.
                // 거리 찾기
                Queue<int> q = new(n);

                prev = new int[n];
                dis = new int[n];
                for (int i = 0; i < n; i++)
                {

                    prev[i] = -1;
                    dis[i] = -1;
                }

                q.Enqueue(0);
                dis[0] = 0;
                while(q.Count > 0)
                {

                    int node = q.Dequeue();

                    for (int i = 0; i < edge[node].Count; i++)
                    {

                        int next = edge[node][i];
                        if (dis[next] != -1) continue;
                        prev[next] = node;
                        dis[next] = dis[node] + 1;
                        q.Enqueue(next);
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                arr = new int[n];
                dic = new(n);
                for (int i = 0; i < n; i++)
                {

                    int num = ReadBinary();
                    arr[i] = num;
                    dic[num] = i;
                }
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

            int ReadBinary()
            {

                int ret = 0;
                for (int i = 0; i < m; i++)
                {

                    int cur = sr.Read() - '0';
                    ret |= cur << i;
                }

                if (sr.Read() == '\r') sr.Read();
                return ret;
            }
        }
    }

#if other
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

// #nullable disable

public static class Program
{
    private static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var nk = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var n = nk[0];
        var k = nk[1];

        var forward = new ulong[n];
        var reverse = new Dictionary<ulong, int>();
        for (var idx = 0; idx < n; idx++)
        {
            var c = sr.ReadLine().Select((ch, idx) => (ulong)(ch - '0') << idx).Aggregate((l, r) => l | r);

            forward[idx] = c;
            reverse.Add(c, idx);
        }

        var visited = new bool[n];
        var prevArr = new int?[n];

        var q = new Queue<(int prev, int curr)>();
        q.Enqueue((0, 0));

        while (q.TryDequeue(out var s))
        {
            var (prev, curr) = s;

            if (visited[curr])
                continue;

            visited[curr] = true;
            prevArr[curr] = prev;

            for (var idx = 0; idx < 32; idx++)
            {
                var next = (1UL << idx) ^ forward[curr];
                if (!reverse.TryGetValue(next, out var nextIdx))
                    continue;

                if (visited[nextIdx])
                    continue;

                q.Enqueue((curr, nextIdx));
            }
        }

        var path = new List<int>();
        var m = Int32.Parse(sr.ReadLine());
        while (m-- > 0)
        {
            var src = 0;
            var dst = Int32.Parse(sr.ReadLine()) - 1;

            var exists = true;

            path.Clear();
            do
            {
                path.Add(dst);
                if (!prevArr[dst].HasValue)
                {
                    exists = false;
                    break;
                }

                dst = prevArr[dst].Value;
            } while (src != dst);

            path.Reverse();

            if (exists)
            {
                sw.Write("1 ");
                foreach (var v in path)
                    sw.Write($"{1 + v} ");

                sw.WriteLine();
            }
            else
            {
                sw.WriteLine(-1);
            }
        }
    }
}
#endif
}
