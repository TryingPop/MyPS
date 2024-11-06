using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 24
이름 : 배성훈
내용 : 흔한 수열 문제
    문제번호 : 2787번

    이분 매칭 문제다
    로직은 이게 맞다고 확신하고 풀었는데, 6번 틀렸다;
    그래서 초기 3번은 해당 코드를 다시짜보고, 
    4번째는 그냥 호프크로프트 카프 알고리즘이 아닌 일반 이분 매칭을 짰다(해당 방법이 가장 빠르다;)
    그리고 숫자 읽는게 문제인가 싶어 입력 형식도 바꿔봤다('\t')
    그래도 틀려 다른 사람 풀이를 봤는데, 로직은 같고 하나씩 보다가 출력 형식이 잘못되었음을 알게되었다

    정확히 틀린 부분은 호프크로프트 카프 알고리즘에서 B[i]를 읽어 틀렸다
    B[i]에는 오른쪽 값에 매칭된 A의 노드들이 있다
    그런데 문제에서 요구하는 것은 A의 노드에 담긴 B의 노드를 읽는 것이었다;
    그래서 A의 값을 읽으면 된다

    반면 일반적인 이분 매칭 풀이 방법에 match에는 마찬가지로 호프의 B와 같다;
    그래서 역으로 담을게 필요하고 역으로 담아 읽어야한다;
*/

namespace BaekJoon.etc
{
    internal class etc_0722
    {

        static void Main722(string[] args)
        {

#if first
            int INF = 1_000_000;

            StreamReader sr;
            StreamWriter sw;

            int[] A, B;
            int[] lvl;
            int n;

            bool[,] disconn;
            int[] max;
            int[] min;
            bool[] visit;

            Queue<int> q;
            int ret;
            bool wrong = false;

            Solve();

            void Solve()
            {

                Input();

                if (wrong)
                {

                    Console.WriteLine(-1);
                    return;
                }

                Init();

                ret = 0;
                while (true)
                {

                    BFS();

                    int match = 0;
                    for (int i = 1; i <= n; i++)
                    {

                        if (!visit[i] && DFS(i)) match++;
                    }

                    if (match == 0) break;

                    ret += match;
                }

                Output();
            }

            void Output()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                if (ret != n) sw.Write(-1);
                else
                {

                    for (int i = 1; i <= n; i++)
                    {

                        sw.Write($"{A[i]} ");
                    }
                }

                sw.Close();
            }

            void Init()
            {

                A = new int[n + 1];
                B = new int[n + 1];
                lvl = new int[n + 1];
                visit = new bool[n + 1];
                q = new(n);
            }

            void BFS()
            {

                for (int i = 1; i <= n; i++)
                {

                    if (!visit[i])
                    {

                        lvl[i] = 0;
                        q.Enqueue(i);
                    }
                    else lvl[i] = INF;
                }

                while (q.Count > 0)
                {

                    int a = q.Dequeue();

                    for (int b = min[a]; b <= max[a]; b++)
                    {

                        if (disconn[a, b]) continue;

                        if (B[b] != 0 && lvl[B[b]] == INF)
                        {

                            lvl[B[b]] = lvl[a] + 1;
                            q.Enqueue(B[b]);
                        }
                    }
                }
            }

            bool DFS(int _a)
            {

                for (int b = min[_a]; b <= max[_a]; b++)
                {

                    if (disconn[_a, b]) continue;

                    if (B[b] == 0 || lvl[B[b]] == lvl[_a] + 1 && DFS(B[b]))
                    {

                        visit[_a] = true;
                        A[_a] = b;
                        B[b] = _a;
                        return true;
                    }
                }

                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                int len = ReadInt();

                max = new int[n + 1];
                min = new int[n + 1];

                disconn = new bool[n + 1, n + 1];
                min[0] = -1;
                max[0] = -1;
                for (int i = 1; i <= n; i++)
                {

                    min[i] = 1;
                    max[i] = n;
                }

                while (len-- > 0)
                {

                    int op = ReadInt();
                    int x = ReadInt();
                    int y = ReadInt();
                    int v = ReadInt();

                    for (int i = 1; i <= n; i++)
                    {

                        if (i < x || y < i) disconn[i, v] = true;
                        else if (op == 1) max[i] = Math.Min(max[i], v);
                        else min[i] = Math.Max(min[i], v);
                    }
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n' && c != '\t')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }

#else

            StreamReader sr;
            StreamWriter sw;

            int n;

            bool[,] disconn;
            int[] max;
            int[] min;

            int[] match;
            bool[] visit;

            int ret;
            int[] arr;

            Solve();

            void Solve()
            {

                Input();

                ret = 0;
                for (int i = 1; i <= n; i++)
                {

                    Array.Fill(visit, false);
                    if (DFS(i)) ret++;
                }

                Output();
            }

            bool DFS(int _n)
            {

                for (int i = min[_n]; i <= max[_n]; i++)
                {

                    if (disconn[_n, i] || visit[i]) continue;
                    visit[i] = true;

                    if (match[i] == 0 || DFS(match[i]))
                    {

                        match[i] = _n;
                        return true;
                    }
                }
                return false;
            }

            void Output()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                if (ret != n) sw.Write(-1);
                else
                {

                    arr = new int[n + 1];
                    for (int i = 1; i <= n; i++)
                    {

                        arr[match[i]] = i;
                    }

                    for (int i = 1; i <= n; i++)
                    {

                        sw.Write($"{arr[i]} ");
                    }
                }

                sw.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                int len = ReadInt();

                max = new int[n + 1];
                min = new int[n + 1];

                match = new int[n + 1];
                visit = new bool[n + 1];

                disconn = new bool[n + 1, n + 1];
                for (int i = 1; i <= n; i++)
                {

                    min[i] = 1;
                    max[i] = n;
                }

                while (len-- > 0)
                {

                    int op = ReadInt();
                    int x = ReadInt();
                    int y = ReadInt();
                    int v = ReadInt();

                    for (int j = 1; j <= n; j++)
                    {

                        if (j < x || y < j) disconn[j, v] = true;
                        else if (op == 1) max[j] = Math.Min(max[j], v);
                        else min[j] = Math.Max(min[j], v);
                    }
                }

                sr.Close();
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
#endif
        }
    }

#if other
// #include <bits/stdc++.h>
// #include <sys/stat.h>
// #include <sys/mman.h>
using namespace std;

constexpr int INF = 1e9 + 7;

struct HopcroftKarp { //Hopcroft-Karp algorithm, O(Esqrt(V)).
	vector<vector<int>> adj;
	vector<int> par, lv, work, check; int sz;
	HopcroftKarp(int n) : adj(n), par(n, -1), lv(n), work(n), check(n), sz(n) {}
	void add_edge(int a, int b) { adj[a].push_back(b); }
	void BFS() {
		queue<int> Q;
		for (int i = 0; i < sz; i++) {
			if (!check[i]) lv[i] = 0, Q.push(i);
			else lv[i] = INF;
		}
		while (!Q.empty()) {
			auto cur = Q.front(); Q.pop();
			for (auto nxt : adj[cur]) {
				if (par[nxt] != -1 && lv[par[nxt]] == INF) {
					lv[par[nxt]] = lv[cur] + 1;
					Q.push(par[nxt]);
				}
			}
		}
	}
	bool DFS(int cur) {
		for (int& i = work[cur]; i < adj[cur].size(); i++) {
			int nxt = adj[cur][i];
			if (par[nxt] == -1 || lv[par[nxt]] == lv[cur] + 1 && DFS(par[nxt])) {
				check[cur] = 1, par[nxt] = cur;
				return 1;
			}
		}
		return 0;
	}
	int Match() {
		int ret = 0;
		for (int fl = 0; ; fl = 0) {
			fill(work.begin(), work.end(), 0); BFS();
			for (int i = 0; i < sz; i++) if (!check[i] && DFS(i)) fl++;
			if (!fl) break;
			ret += fl;
		}
		return ret;
	}
};

bitset<201> check[201];

int main() {
    struct stat st; fstat(0, &st);
	char *p = (char*)mmap(0, st.st_size, PROT_READ, MAP_SHARED, 0, 0);
	auto ReadInt = [&]() {
		int ret = 0;
		for (char c = *p++; c & 16; ret = 10 * ret + (c & 15), c = *p++);
		return ret;
	};
    
	int n = ReadInt(), m = ReadInt();
	vector<int> mx(n + 1, n), mn(n + 1, 1);
	while (m--) {
        int t = ReadInt(), a = ReadInt(), b = ReadInt(), c = ReadInt();
		for (int i = a; i <= b; i++) {
			if (t & 1) mx[i] = min(mx[i], c);
			else mn[i] = max(mn[i], c);
		}
		for (int i = 1; i < a; i++) check[i][c] = 1;
		for (int i = b + 1; i <= n; i++) check[i][c] = 1;
	}

	HopcroftKarp flow(2 * n + 1);
	for (int i = 1; i <= n; i++)
		for (int j = mn[i]; j <= mx[i]; j++)
			if (!check[i][j]) flow.add_edge(i, n + j);

	if (flow.Match() == n) {
		vector<int> ans(n + 1);
		for (int i = 1; i <= n; i++) ans[flow.par[n + i]] = i;
		for (int i = 1; i <= n; i++) cout << ans[i] << ' ';
		cout << '\n';
	}
	else cout << -1 << '\n';
}
#endif
}
