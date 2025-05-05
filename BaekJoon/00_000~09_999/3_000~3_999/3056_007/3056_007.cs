using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 3
이름 : 배성훈
내용 : 007
    문제번호 : 3056번

    dp, 비트마스킹 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1609
    {

        static void Main3056(string[] args)
        {

            // 3056번
            double NOT_VISIT = -1.0;
            int n;
            double[][] dp;
            int[][] per;

            Input();

            SetDp();

            GetRet();

            void GetRet()
            {

                double ret = DFS() * 100;
                Console.Write($"{ret:0.##########}");

                double DFS(int _r = 0, int _state = 0)
                {

                    if (_r == n) return 1.0;
                    ref double ret = ref dp[_r][_state];
                    if (ret != NOT_VISIT) return ret;
                    ret = 0;

                    for (int i = 0; i < n; i++)
                    {

                        if ((_state & (1 << i)) != 0) continue;

                        double s = per[_r][i] / 100.0;

                        ret = Math.Max(ret, s * DFS(_r + 1, _state | (1 << i)));
                    }

                    return ret;
                }
            }

            void SetDp()
            {

                dp = new double[n + 1][];
                int len = 1 << n;
                for (int i = 0; i <= n; i++)
                {

                    dp[i] = new double[len];
                    Array.Fill(dp[i], NOT_VISIT);
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                per = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    per[i] = new int[n];
                    for (int j = 0; j < n; j++)
                    {

                        per[i][j] = ReadInt();
                    }
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
            }
        }
    }

#if other
// #include <cstdio>
// #include <algorithm>
// #include <cmath>

// #define MAX_N 24

const int INF = 0x3f2f1f0f;

int N;
int Orig[MAX_N][MAX_N];
int W[MAX_N][MAX_N];
int A[MAX_N], B[MAX_N], S[MAX_N], T[MAX_N], Mv[MAX_N], Mx[MAX_N];
// A and B are matching permutation; (i, A[i])
int Lx[MAX_N], Ly[MAX_N];


int hungarian() {
	for (int f=0; f<N;) {
		for (int k=1; k<=N && f<N; k++) if (A[k] == 0) {
			for (int i=1; i<=N; i++) {
				S[i] = T[i] = 0;
				Mv[i] = -INF;
			}
			int m = -1;
			for (int x=k; x; T[m]=1, x=B[m]) {
				S[x] = 1;
				m = -1;
				for (int j=1; j<=N; j++) {
					if (W[x][j]-Lx[x]-Ly[j] > Mv[j]) {
						Mv[j] = W[x][j]-Lx[x]-Ly[j];
						Mx[j] = x;
					}
				}
				for (int j=1; j<=N; j++)
					if (T[j] == 0 && (m == -1 || Mv[j] > Mv[m]))
						m = j;
				if (Mv[m] != 0) {
					int d = Mv[m];
					for (int i=1; i<=N; i++) {
						if (S[i]) Lx[i] += d;
						if (T[i]) Ly[i] -= d;
						if (!T[i]) Mv[i] -= d;
					}
				}
			}
			for (int t=m, x; x=Mx[t], m=t; B[A[x]=m]=x)
				t = A[x];
			f++;
		}
	}
	int ans = 0;
	for (int i=1; i<=N; i++)
		ans += Lx[i] + Ly[i];
	return ans;
}

int main() {
	scanf("%d", &N);
	for(int i=1;i<=N;i++)
        for(int j=1;j<=N;j++) {
            scanf("%d", &Orig[i][j]);
            if (Orig[i][j]==0) {
                W[i][j] = 0;
            } else {
                W[i][j] = 1e5 + 1e5 * log(Orig[i][j]);
            }
        }
	//printf("%d\n", hungarian());
    hungarian();
    long double ans = 1e2;
    for (int i=1; i<=N; i++) {
        ans = ans * Orig[i][A[i]] / 100;
    }
    printf("%.10Lf\n", ans);
}
#elif other2
// #include <cstdio>
// #include <vector>
// #include <cstring>
// #include <queue>
// #include <cmath>
using namespace std;
const int MAX_V = 42;
struct Edge {
	int v, f, c, rn;
	double ct;
	Edge() {}
	Edge(int v, int f, int c, double ct, int rn)
		: v(v), f(f), c(c), ct(ct), rn(rn) {}
	int rc() { return c - f; }
	void af(int amt, vector<Edge> adj[MAX_V]) {
		f += amt;
		adj[v][rn].f -= amt;
	}
};
vector<Edge> adj[MAX_V];
void addEdge(int u, int v, int c, double ct) {
	adj[u].push_back(Edge(v, 0, c, ct, (int)adj[v].size()));
	adj[v].push_back(Edge(u, 0, 0, -ct, (int)adj[u].size() - 1));
}
int main() {
	int n, i, j, d, S = MAX_V - 2, T = MAX_V - 1, bef[MAX_V];
	double dist[MAX_V], ans = 0;
	vector<Edge*> path(MAX_V);
	bool hasQ[MAX_V] = {};
	queue<int> q;
	scanf("%d", &n);
	for (i = 0; i<n; i++) {
		addEdge(S, i, 1, 0);
		addEdge(i + n, T, 1, 0);
		for (j = 0; j<n; j++) {
			scanf("%d", &d);
			addEdge(i, j + n, 1, -log(d) / log(10.));
		}
	}
	while (1) {
		memset(bef, -1, sizeof bef);
		fill(dist, dist + MAX_V, 1e9);
		fill(path.begin(), path.end(), nullptr);
		dist[S] = 0;
		q.push(S);
		while (!q.empty()) {
			int u = q.front(); q.pop();
			hasQ[u] = 0;
			for (Edge& e : adj[u]) if (e.rc() > 0 && dist[e.v] > dist[u] + e.ct) {
				dist[e.v] = dist[u] + e.ct;
				path[e.v] = &e;
				bef[e.v] = u;
				if (!hasQ[e.v]) {
					hasQ[e.v] = 1;
					q.push(e.v);
				}
			}
		}
		if (bef[T] == -1) break;
		for (i = T; i != S; i = bef[i]) {
			ans += path[i]->ct;
			path[i]->af(1, adj);
		}
	}
	printf("%.9lf", ans != 0 ? pow(10., -ans-2*(n-1)) : 0);
	return 0;
}
#elif other3
// #nullable disable

using System;
using System.IO;
using System.Linq;
using System.Numerics;

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());

        var cost = new double[n][];
        for (var idx = 0; idx < n; idx++)
            cost[idx] = sr.ReadLine().Split(' ').Select(str => Double.Parse(str) / 100).ToArray();

        // dp[handled last work, worked person mask] = min cost of work 0..(handled last work) inclusive, with using person in mask
        var dp = new double[n, 1 << n];

        for (var person = 0; person < n; person++)
            dp[0, 1 << person] = cost[0][person];

        for (var mask = 1; mask < (1 << n); mask++)
            for (var person = 0; person < n; person++)
            {
                var newmask = mask | (1 << person);
                if (mask == newmask)
                    continue;

                var nextWork = BitOperations.PopCount((uint)newmask) - 1;
                var newCost = dp[nextWork - 1, mask] * cost[nextWork][person];

                if (dp[nextWork, newmask] == 0)
                    dp[nextWork, newmask] = newCost;
                else
                    dp[nextWork, newmask] = Math.Max(dp[nextWork, newmask], newCost);
            }

        sw.WriteLine(100 * dp[n - 1, (1 << n) - 1]);
    }
}

#elif other4
const int INF = 1234567890;
int N = int.Parse(Console.ReadLine()!);

double[] dp = new double[1 << N];
for (int i = 0; i < 1 << N; i++)
    dp[i] = INF;

int[][] q = new int[N][];
for (int i = 0; i < N; i++)
    q[i] = Array.ConvertAll(Console.ReadLine()!.Split(), int.Parse);

Console.WriteLine(Solve(0, 0) * 100);
double Solve(int bits, int num)
{
    if (bits == (1 << N) - 1) return 1;
    if (dp[bits] != INF) return dp[bits];

    dp[bits] = 0;
    for (int i = 0; i < N; i++)
    {
        if ((bits & (1 << i)) != 0) continue;
        int nextBit = bits | (1 << i);
        dp[bits] = Math.Max(dp[bits], (Solve(nextBit, num + 1) * q[num][i]) / 100);
    }

    return dp[bits];
}
#elif other5
using System;

public class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        double[,] array = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            int[] row = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            for (int j = 0; j < n; j++)
            {
                array[i, j] = row[j] / 100d;
            }
        }
        int m = 1 << n;
        double[] dp = new double[m];
        for (int i = 0; i < n; i++)
        {
            dp[1 << i] = array[0, i];
        }
        for (int j = 1; j < m; j++)
        {
            int i = Count(j);
            for (int k = 0; k < n; k++)
            {
                if ((j & 1 << k) != 0)
                    continue;
                dp[j | 1 << k] = Math.Max(dp[j | 1 << k], dp[j] * array[i, k]);
            }
        }
        Console.Write(dp[m - 1] * 100);
    }
    static int Count(int n)
    {
        int result = 0;
        for (int i = 0; i < 20; i++)
        {
            if ((n & 1 << i) != 0)
                result++;
        }
        return result;
    }
}
#endif
}
