using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 28
이름 : 배성훈
내용 : LCA
    문제번호 : 11437번

    44_01이 자꾸 런타임 Format에러 떠서 로직이 잘못된거가 확인하려고 푼 문제이다

    주된 로직은 다음과 같다

    각 노드들의 부모를 찾는다
    여기서는 루트가 1로 고정되어져 있다

    그래서 간선을 입력받은 뒤에 BFS 탐색으로 각 노드들의 부모를 parent 배열에 기록했다

    그다음에 문제를 입력받으면 해당 노드의 부모들을 Stack에 저장하며 루트까지 나아간다
    그러면 Stack에는 루트에서 해당 노드로 가는 경로가 저장되어져 있다
    그러면 두 노드의 Stack에 대해 동시에 하나씩 꺼내면서 값이 같은지 비교해간다
    값이 바뀌는 순간 이전의 값이 LCA가 된다는 논리이다

    만약 깊은 곳에 있는 같은 노드인 경우, 해당 방법으로 찾는데 엄청난 시간이 소모된다
    그래도 해당 문제를 아슬아슬하게 통과했으니 아이디어가 잘못된거 같지는 않다
    제한은 3초인데 2.2초 걸렸다

    찾아보니, 이진 탐색 아이디어 쓴다! 이는 44_01에서 한다!
*/

namespace BaekJoon._44
{
    internal class _44_02
    {

        static void Main2(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int node = int.Parse(sr.ReadLine());

            int[] parents = new int[node + 1];
            List<int>[] lines = new List<int>[node + 1];

            for (int i = 1; i <= node; i++)
            {

                lines[i] = new();
            }

            for (int i = 1; i < node; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                lines[temp[0]].Add(temp[1]);
                lines[temp[1]].Add(temp[0]);
            }

            // 부모 찾기
            Queue<int> q = new Queue<int>();
            q.Enqueue(1);

            while(q.Count > 0)
            {

                var cur = q.Dequeue();

                for (int i = 0; i < lines[cur].Count; i++)
                {

                    int next = lines[cur][i];
                    if (parents[cur] == next) continue;

                    parents[next] = cur;
                    q.Enqueue(next);
                }
            }

            int find = int.Parse(sr.ReadLine());
            Stack<int> calc1 = new();
            Stack<int> calc2 = new();

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            for (int i = 0; i < find; i++)
            {

                int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                while (info[0] != 0)
                {

                    calc1.Push(info[0]);
                    info[0] = parents[info[0]];
                }

                while (info[1] != 0)
                {

                    calc2.Push(info[1]);
                    info[1] = parents[info[1]];
                }

                int result = 1;
                int chk1 = calc1.Pop();
                int chk2 = calc2.Pop();

                while(chk1 == chk2)
                {

                    result = chk1;
                    if (calc1.Count == 0 || calc2.Count == 0) break;

                    chk1 = calc1.Pop();
                    chk2 = calc2.Pop();
                }

                calc1.Clear();
                calc2.Clear();
                sw.Write(result);
                sw.Write('\n');
            }
            sw.Close();
            sr.Close();

        }
    }

#if other1
    var sr = new StreamReader(Console.OpenStandardInput());
    var sw = new StreamWriter(Console.OpenStandardOutput());

    var N = int.Parse(sr.ReadLine());    // 노드의 개수
    var log = (int)Math.Log(N, 2) + 1;   // 노드의 높이
    var parent = new int[N + 1, log];       // 부모 노드를 저장할 배열
    var graph = new List<int>[N + 1]; // 그래프를 저장할 배열
    var depth = new int[N + 1];       // 노드의 깊이를 저장할 배열
    for (var i = 0; i <= N; i++)
    {
        graph[i] = new List<int>();
    }

    for (int i = 0; i < N - 1; i++)
    {
        var input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        var a = input[0];
        var b = input[1];
        graph[a].Add(b);
        graph[b].Add(a);
    }

    var visited = new bool[N + 1];

    void DFS(int current, int currentDepth)
    {
        visited[current] = true;
        depth[current] = currentDepth;
        foreach (var next in graph[current])
        {
            if (visited[next])
            {
                continue;
            }

            parent[next, 0] = current;             // next의 부모 노드는 current
            DFS(next, currentDepth + 1);
        }
    }

    void InitParent()
    {
        DFS(1, 0);                   // 1번 노드를 루트 노드로 설정한다.
        for (var i = 1; i < log; i++)           // 2^i번째 부모 노드를 찾는다.
        {
            for (var j = 1; j <= N; j++)        // 모든 노드를 순회한다.
            {
                parent[j, i] = parent[parent[j, i - 1], i - 1];
            }
        }
    }

    InitParent();

    var M = int.Parse(sr.ReadLine());    // 공통 조상을 알고 싶은 쌍의 개수
    for (var i = 0; i < M; i++)
    {
        var input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
        var a = input[0];
        var b = input[1];

        if (a == b)
        {
            sw.WriteLine(a);
            continue;
        }
    
        var aDepth = depth[a];
        var bDepth = depth[b];
    
        if(aDepth != bDepth)
        {
            if (aDepth > bDepth)
            {
                var temp = a;
                a = b;
                b = temp;
            }

            for (var j = log - 1; j >= 0; j--)
            {
                var diff = depth[b] - depth[a];
                if (diff < (1 << j)) continue;
                b = parent[b, j];
            }
        }
    
        if (a == b)
        {
            sw.WriteLine(a);
            continue;
        }

        for (var j = log - 1; j >= 0; j--)
        {
            if (parent[a, j] == parent[b, j]) continue;
            a = parent[a, j];
            b = parent[b, j];
        }

        sw.WriteLine(parent[a, 0]);
    }

    sr.Close();
    sw.Close();
#elif other2
        using System.Text;

        public class Program
        {
            private static void Main(string[] args)
            {
                using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
                var nodeCount = ScanInt(sr);
                var link = new List<int>[nodeCount + 1];
                for (int i = 1; i <= nodeCount; i++)
                {
                    link[i] = new();
                }
                for (int i = 0; i < nodeCount - 1; i++)
                {
                    int a = ScanInt(sr), b = ScanInt(sr);
                    link[a].Add(b);
                    link[b].Add(a);
                }

                var depth = new int[nodeCount + 1];
                var parent = new int[nodeCount + 1];
                parent[1] = 1;
                DP(1, 0);
                var lcaCount = ScanInt(sr);
                var sb = new StringBuilder();
                for (int i = 0; i < lcaCount; i++)
                {
                    int a = ScanInt(sr), b = ScanInt(sr);
                    sb.Append(LCA(a, b)).Append('\n');
                }
                Console.Write(sb);

                void DP(int node, int level)
                {
                    depth[node] = level;
                    foreach (var next in link[node])
                    {
                        if (parent[next] == 0)
                        {
                            parent[next] = node;
                            DP(next, level + 1);
                        }
                    }
                }

                int LCA(int a, int b)
                {
                    while (depth[a] > depth[b])
                    {
                        a = parent[a];
                    }
                    while (depth[b] > depth[a])
                    {
                        b = parent[b];
                    }
                    while (a != b)
                    {
                        a = parent[a];
                        b = parent[b];
                    }
                    return a;
                }

                static int ScanInt(StreamReader sr)
                {
                    int c, n = 0;
                    while (!((c = sr.Read()) is ' ' or '\n' or -1))
                    {
                        if (c == '\r')
                        {
                            sr.Read();
                            break;
                        }
                        n = 10 * n + c - '0';
                    }
                    return n;
                }
            }
        }
#endif
}
