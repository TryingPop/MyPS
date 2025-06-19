using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 13
이름 : 배성훈
내용 : 버스 노선
    문제번호 : 12817번

    트리, dp 문제다.
    해당 간선을 지나는 버스의 갯수를 찾아 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1538
    {

        static void Main1538(string[] args)
        {

            int n;
            List<int>[] edge;
            int[] child;
            long[] ret;

            Input();

            SetArr();

            GetRet();

            void GetRet()
            {

                ret = new long[n + 1];
                DFS();

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 1; i <= n; i++)
                {

                    // 초기 n - 1대 추가
                    sw.Write($"{ret[i] + n - 1}\n");
                }

                void DFS(int _cur = 1, int _prev = 0)
                {

                    // 부모에서 자신을 지나는 것의 갯수
                    ret[_cur] = 1L * (n - child[_cur]) * child[_cur];

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (next == _prev) continue;
                        DFS(next, _cur);

                        // 자식에서 자신을 지나는 것의 갯수
                        ret[_cur] += 1L * child[next] * (n - child[next]);
                    }
                }
            }

            void SetArr()
            {

                child = new int[n + 1];
                DFS();
                int DFS(int _cur = 1, int _prev = 0)
                {

                    ref int ret = ref child[_cur];
                    if (ret > 0) return ret;
                    ret = 1;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (next == _prev) continue;

                        ret += DFS(next, _cur);
                    }
                    return ret;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                edge = new List<int>[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 1; i < n; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    edge[f].Add(t);
                    edge[t].Add(f);
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
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
// #include <bits/stdc++.h>
// #define fastio cin.tie(0)->sync_with_stdio(0)
using namespace std;

int main() {
	fastio;
	int n; cin >> n;
	vector e(n - 1, pair(0, 0));
	for (auto& [a, b] : e) cin >> a >> b;

	vector cnt(n + 2, 0);
	vector csr(2 * (n - 1), 0);
	for (auto [a, b] : e) cnt[a + 1]++, cnt[b + 1]++;
	for (int i = 1; i < cnt.size(); i++) cnt[i] += cnt[i - 1];
	for (auto [a, b] : e) csr[cnt[a]++] = b, csr[cnt[b]++] = a;

	vector sz(n + 1, 1), par(n + 1, 0);
	auto dfs = [&](const auto& self, int cur, int prv) -> void {
		for (int i = cnt[cur - 1]; i < cnt[cur]; i++) {
			int nxt = csr[i];
			if (nxt == prv) continue;
			par[nxt] = cur;
			self(self, nxt, cur);
			sz[cur] += sz[nxt];
		}
	};
	dfs(dfs, 1, 0);

	for (int i = 1; i <= n; i++) {
		auto res = 0LL, acc = 1LL;
		for (int j = cnt[i - 1]; j < cnt[i]; j++) {
			int x = csr[j];
			int s = x == par[i] ? n - sz[i] : sz[x];
			res += acc * s, acc += s;
		}
		cout << 2 * res << '\n';
	}
}
#elif other2
// #nullable disable

using System;
using System.Collections.Generic;
using System.IO;

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

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());

        var graph = new List<int>[n];
        for (var idx = 0; idx < n; idx++)
            graph[idx] = new List<int>();

        for (var idx = 0; idx < n - 1; idx++)
        {
            var span = sr.ReadLine().AsSpan();
            var spidx = span.IndexOf(' ');

            var x = Int32.Parse(span.Slice(0, spidx)) - 1;
            var y = Int32.Parse(span.Slice(spidx + 1)) - 1;

            graph[x].Add(y);
            graph[y].Add(x);
        }

        var parent = new int[n];
        var children = new List<int>[n];
        var vis = new bool[n];
        var q = new Queue<int>();
        q.Enqueue(0);

        for (var idx = 0; idx < n; idx++)
            children[idx] = new List<int>();

        vis[0] = true;
        while (q.TryDequeue(out var src))
        {
            vis[src] = true;
            foreach (var dst in graph[src])
            {
                if (!vis[dst])
                {
                    children[src].Add(dst);
                    parent[dst] = src;
                    vis[dst] = true;
                    q.Enqueue(dst);
                }
            }
        }

        var subtreeSize = new long[n];
        var ccount = new int[n];
        var zeroq = new Queue<int>();

        for (var idx = 0; idx < n; idx++)
        {
            ccount[idx] = children[idx].Count;
            if (ccount[idx] == 0)
                zeroq.Enqueue(idx);
        }

        while (zeroq.TryDequeue(out var pos))
        {
            var size = 1L;
            foreach (var c in children[pos])
                size += subtreeSize[c];

            subtreeSize[pos] = size;

            var p = parent[pos];
            ccount[p]--;
            if (ccount[p] == 0)
                zeroq.Enqueue(p);
        }

        for (var idx = 0; idx < n; idx++)
        {
            var rootSide = n - subtreeSize[idx];
            var childSide = subtreeSize[idx] - 1;

            var count = 2L * (n - 1);
            count += rootSide * childSide;

            foreach (var c in children[idx])
                count += (rootSide + childSide - subtreeSize[c]) * subtreeSize[c];

            sw.WriteLine(count);
        }
    }
}

#endif
}
