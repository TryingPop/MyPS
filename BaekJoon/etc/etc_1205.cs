using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 19
이름 : 배성훈
내용 : 단절선
    문제번호 : 11400번

    단절선 문제다.
    단절선은 해당 간선을 제거했을 때 그래프가 2개로 분할되는 경우 해당 간선을 단절선이라 한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1205
    {

        static void Main1205(string[] args)
        {

            int n, m;
            List<int>[] edge;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int order = 1;
                int[] visit = new int[n + 1];
                List<(int f, int t)> retEdge = new(m);
                for (int i = 1; i <= n; i++)
                {

                    if (visit[i] > 0) continue;
                    DFS(0, i);
                }

                retEdge.Sort();
                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                sw.Write($"{retEdge.Count}\n");
                for (int i = 0; i < retEdge.Count; i++)
                {

                    sw.Write($"{retEdge[i].f} {retEdge[i].t}\n");
                }
                
                int DFS(int _prev, int _cur)
                {

                    int ret = order++;
                    visit[_cur] = ret;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (_prev == next) continue;

                        if (visit[next] != 0)
                        {

                            ret = Math.Min(ret, visit[next]);
                            continue;
                        }

                        int chk = DFS(_cur, next);
                        if (chk > visit[_cur]) retEdge.Add((Math.Min(_cur, next), Math.Max(_cur, next)));
                        ret = Math.Min(ret, chk);
                    }

                    return ret;
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                edge = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    edge[f].Add(t);
                    edge[t].Add(f);
                }

                sr.Close();

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == ' ' || c == '\n') return true;

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

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var ve = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var (v, e) = (ve[0], ve[1]);

        var graph = new List<int>[v];
        for (var idx = 0; idx < v; idx++)
            graph[idx] = new List<int>();

        for (var idx = 0; idx < e; idx++)
        {
            var l = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            l[0]--;
            l[1]--;

            graph[l[0]].Add(l[1]);
            graph[l[1]].Add(l[0]);
        }

        var dfstree = new List<int>[v];
        var visited = new bool[v];

        for (var idx = 0; idx < v; idx++)
            dfstree[idx] = new List<int>();

        var roots = new List<int>();

        for (var idx = 0; idx < v; idx++)
        {
            if (visited[idx])
                continue;

            roots.Add(idx);
            BuildDFSTree(graph, visited, dfstree, idx);
        }

        var depth = new int[v];
        var q = new Queue<(int pos, int depth)>();

        foreach (var root in roots)
            q.Enqueue((root, 0));

        while (q.TryDequeue(out var state))
        {
            var (pos, d) = state;
            depth[pos] = d;

            foreach (var child in dfstree[pos])
                q.Enqueue((child, 1 + d));
        }

        var minReachableDepth = new int[v];
        Array.Clear(visited);

        foreach (var root in roots)
            BuildMinReachableDepth(graph, dfstree, depth, visited, minReachableDepth, root, -1);

        var arti = new List<(int a, int b)>();
        for (var parent = 0; parent < v; parent++)
            foreach (var child in dfstree[parent])
                if (minReachableDepth[child] == depth[child])
                    arti.Add((1 + Math.Min(parent, child), 1 + Math.Max(parent, child)));

        sw.WriteLine(arti.Count);
        foreach (var (a, b) in arti.OrderBy(p => p.a).ThenBy(p => p.b))
            sw.WriteLine($"{a} {b}");
    }

    private static void BuildDFSTree(List<int>[] graph, bool[] visited, List<int>[] dfstree, int pos)
    {
        visited[pos] = true;

        foreach (var child in graph[pos])
            if (!visited[child])
            {
                dfstree[pos].Add(child);
                BuildDFSTree(graph, visited, dfstree, child);
            }
    }
    private static int BuildMinReachableDepth(List<int>[] graph, List<int>[] dfstree, int[] depth, bool[] visited, int[] minReachableDepth, int pos, int parent)
    {
        if (visited[pos])
            return minReachableDepth[pos];

        var min = depth[pos];

        foreach (var conn in graph[pos])
            if (conn != parent)
                min = Math.Min(min, depth[conn]);

        foreach (var child in dfstree[pos])
            min = Math.Min(min, BuildMinReachableDepth(graph, dfstree, depth, visited, minReachableDepth, child, pos));

        visited[pos] = true;
        minReachableDepth[pos] = min;
        return min;
    }
}
#elif other2
// #include <bits/stdc++.h>
// #include <sys/stat.h>
// #include <sys/mman.h>
int n, m, temp, idx[100001];
vector<int> adj[100001];
vector<pair<int, int>> ans;

int dfs(int cur, int prev) {
	idx[cur] = ++temp;
	int ret = idx[cur];
	for (auto nxt : adj[cur]) {
		if (nxt == prev) continue;
		if (idx[nxt]) ret = min(ret, idx[nxt]);
		else {
			int mn = dfs(nxt, cur);
			if (mn > idx[cur]) ans.push_back({ min(cur, nxt), max(cur, nxt) });
			ret = min(ret, mn);
		}
	}
	return ret;
}

int main() {
	fastio;
	cin >> n >> m;
	while (m--) {
		int a, b; cin >> a >> b;
		adj[a].push_back(b);
		adj[b].push_back(a);
	}
	for (int i = 1; i <= n; i++) if (idx[i] == 0) dfs(i, 0);
	sort(ans.begin(), ans.end());
	cout << ans.size() << '\n';
	for (const auto& [a, b] : ans) cout << a << ' ' << b << '\n';
}
#endif
}
