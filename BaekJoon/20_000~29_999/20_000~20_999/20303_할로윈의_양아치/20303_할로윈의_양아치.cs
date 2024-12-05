using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 5
이름 : 배성훈
내용 : 할로윈의 양아치
    문제번호 : 20303번

    배낭 문제, 분리 집합 문제다.
    1명을 먹으면 친구들도 먹고 양방향 간선이므로 
    사이클로 그룹을 만들어야 한다.
    
    이후 그룹에 대해 k = 3_000 이하이고 그룹은 n = 30_000이하이므로 
    배낭 dp로 접근해 최대 먹을 수 있는 캔디를 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1159
    {

        static void Main1159(string[] args)
        {

            int n, m, k;
            int[] candy;
            List<int>[] edge;
            List<(int cnt, int candy)> cnt;
            Solve();
            void Solve()
            {

                Input();

                SetGroup();

                GetRet();
            }

            void GetRet()
            {

                int[] dp = new int[k];
                Array.Fill(dp, -1);
                dp[0] = 0;
                for (int i = 0; i < cnt.Count; i++)
                {

                    for (int j = k - 1; j >= 0; j--)
                    {

                        int idx = j + cnt[i].cnt;
                        if (dp[j] == -1 || idx >= k) continue;
                        dp[idx] = Math.Max(dp[idx], dp[j] + cnt[i].candy);
                    }
                }

                int ret = 0;
                for (int i = 0; i < k; i++)
                {

                    ret = Math.Max(ret, dp[i]);
                }
                Console.Write(ret);
            }

            void SetGroup()
            {

                int[] group = new int[n];
                int g = 0;

                cnt = new(n);
                for (int i = 0; i < n; i++)
                {

                    if (group[i] != 0) continue;
                    group[i] = ++g;
                    var chk = DFS(i);
                    if (chk.cnt >= k) continue;
                    cnt.Add(chk);
                }

                (int cnt, int candy) DFS(int _n)
                {

                    (int cnt, int candy) ret = (1, candy[_n]);
                    for (int i = 0; i < edge[_n].Count; i++)
                    {

                        int next = edge[_n][i];
                        if (group[next] != 0) continue;
                        group[next] = g;
                        var add = DFS(next);
                        ret.cnt += add.cnt;
                        ret.candy += add.candy;
                    }

                    return ret;
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();
                k = ReadInt();

                candy = new int[n];
                edge = new List<int>[n];
                for (int i = 0; i < n; i++)
                {

                    edge[i] = new();
                    candy[i] = ReadInt();
                }
                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt() - 1;
                    int b = ReadInt() - 1;

                    edge[f].Add(b);
                    edge[b].Add(f);
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

#if other
// #include <iostream>

using namespace std;

int N, M, K, C[30001], D[30001];
short P[30001], S[30001];

int find(int x) {
	return P[x] ? find(P[x]) : x;
}

void merge(int x, int y) {
	x = find(x);
	y = find(y);
	if (x == y) return;
	if (S[x] < S[y]) swap(x, y);
	S[x] += S[y];
	C[x] += C[y];
	P[y] = x;
}

int main() {
	cin.tie(nullptr);
	ios::sync_with_stdio(false);
	cin >> N >> M >> K;
	for (int i = 1; i <= N; ++i) {
		cin >> C[i];
		S[i] = 1;
	}
	for (int i = 0; i < M; ++i) {
		int a, b;
		cin >> a >> b;
		merge(a, b);
	}
	for (int i = 1; i <= N; ++i)
		if (i == find(i))
			for (int j = K; j >= S[i]; --j)
				D[j] = max(D[j], D[j-S[i]]+C[i]);
	cout << D[K-1];
}
#elif other2
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static int[] p;
        public static void Main(string[] args)
        {
            StreamReader input = new StreamReader(
                new BufferedStream(Console.OpenStandardInput()));
            StreamWriter output = new StreamWriter(
                new BufferedStream(Console.OpenStandardOutput()));
            StringBuilder sb = new StringBuilder();
            int[] arr = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
            int n = arr[0]; int m = arr[1]; int k = arr[2];
            int[] candy = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
            p = new int[n + 1];
            for (int i = 1; i <= n; i++)
                p[i] = i;
            for(int i = 0; i < m; i++)
            {
                int[] temp = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
                int a = temp[0]; int b = temp[1];
                union(a, b);
            }
            Dictionary<int, (int, int)> d = new();
            for(int i = 1; i <= n; i++)
            {
                int parent = find(i);
                if (d.ContainsKey(parent))
                {
                    d[parent] = (d[parent].Item1 + candy[i - 1], d[parent].Item2 + 1);
                }
                else
                    d.Add(parent, (candy[i - 1], 1));
            }
            int[] dp = new int[k + 1];
            foreach((int g, (int c,int ch)) in d)
            {
                for(int i = k; i >= ch; i--)
                {
                    dp[i] = Math.Max(dp[i], dp[i - ch] + c);
                }
            }

            output.Write(dp[k - 1]);

            input.Close();
            output.Close();
        }
        static int find(int x)
        {
            if (x == p[x]) return x;
            return p[x] = find(p[x]);
        }
        static void union(int x,int y)
        {
            int px = find(x);
            int py = find(y);
            if (px < py)
                p[py] = px;
            else
                p[px] = py;
        }
    }
}
#endif
}
