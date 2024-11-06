using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 17
이름 : 배성훈
내용 : 인하니카 공화국
    문제번호 : 12784번

    트리, 트리에서의 dp, dfs 문제다
    조건에 섬을 연결하는 다리를 최소한의 개수로 만들어 
    모든 섬 간의 왕래가 가능하도록 만들었다는 조건으로부터 트리다!


    그래서 1에서 특정노드를 방문하는 길은 유일하다!
    이에 1에서 해당 노드를 방문할 때, 
    최소 폭파 거리를 dp에 저장해 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0823
    {

        static void Main823(string[] args)
        {

            int INF = 100_000;

            StreamReader sr;
            StreamWriter sw;

            List<(int dst, int dis)>[] line;
            int n;
            int[] dp;
            int ret;

            Solve();
            void Solve()
            {

                Init();

                int test = ReadInt();

                while(test-- > 0)
                {

                    Input();

                    ret = DFS(1, -1, INF);

                    Output();
                }

                sr.Close();
                sw.Close();
            }

            void Output()
            {

                for (int i = 1; i <= n; i++)
                {

                    line[i].Clear();
                }
                if (ret == INF) ret = 0;
                sw.Write($"{ret}\n");
            }

            int DFS(int _cur, int _before, int _dis)
            {

                int ret = dp[_cur];
                if (ret != INF) return ret;
                ret = 0;
                bool flag = true;

                for (int i = 0; i < line[_cur].Count; i++)
                {

                    int next = line[_cur][i].dst;
                    if (next == _before) continue;

                    if (flag) flag = false;
                    int add = DFS(next, _cur, line[_cur][i].dis);
                    ret += add;
                }

                if (flag) ret = _dis;
                else ret = Math.Min(ret, _dis);
                return dp[_cur] = ret;
            }

            void Input()
            {

                n = ReadInt();
                int m = ReadInt();

                for (int i = 1; i <= n; i++)
                {

                    dp[i] = INF;
                }

                while(m-- > 0)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    int dis = ReadInt();

                    line[f].Add((b, dis));
                    line[b].Add((f, dis));
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int MAX = 1_000;
                dp = new int[MAX + 1];
                line = new List<(int dst, int dis)>[MAX + 1];
                for (int i = 1; i <= MAX; i++)
                {

                    line[i] = new();
                }
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
// #include <bits/stdc++.h>
// #include <ratio>
// #define endl '\n'

using namespace std;

int n, m;
vector<pair<int, int>> g[1000];
bool visited[1000];

int Solve(int v, int pval)
{
	visited[v] = true;
	int sum = 0;
	for (auto i:g[v])
	{
		if (!visited[i.first])
			sum += Solve(i.first, i.second);
		if (sum > pval)
			break;
	}
	if (!sum)
		return pval;
	return min(sum, pval);
}

int main()
{
	cin.sync_with_stdio(false), cin.tie(nullptr);

	//random_device rd;
	//mt19937_64 rng(rd());
	//uniform_int_distribution<int> dn(1, 10000); // n의 범위
	//uniform_int_distribution<int> darr(0, 999); // arr의 범위
	////n = dn(rng);
	////arr[i]=darr(rng);

	int t;
	cin >> t;
	while (t--)
	{
		memset(visited, 0, sizeof(visited));
		for (int i = 0; i < 1000; i++)
			g[i].clear();
		cin >> n >> m;
		for (int i = 0; i < m; i++)
		{
			int a, b, d;
			cin >> a >> b >> d;
			--a, --b;
			g[a].push_back({ b, d });
			g[b].push_back({ a, d });
		}
		if (n == 1)
			cout << 0 << endl;
		else
			cout << Solve(0, 987654321) << endl;
	};

	return 0;
}
#endif
}
