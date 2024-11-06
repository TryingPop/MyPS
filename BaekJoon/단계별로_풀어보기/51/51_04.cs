using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 23
이름 : 배성훈
내용 : 도시 왕복하기 2
    문제번호 : 2316번

    최대 유량 문제다
    정점을 한 번만 지나야하므로, 강제로 n -> n 으로 가는 용량이 1인 간선과 정점을 분할해서 풀었다;
    
    만약 visit으로 막는다면
        8 9
        1 3
        3 4
        4 2
        2 6
        6 5
        5 3
        1 7
        7 8
        8 4

    해당 경우 2가 나와야하는데 
        1 -> 3 -> 5 -> 6 -> 2
        1 -> 7 -> 8 -> 4 -> 2

    1개만 나와 틀림을 알 수 있다
*/

namespace BaekJoon._51
{
    internal class _51_04
    {

        static void Main4(string[] args)
        {

            int INF = 1_000_000;
            StreamReader sr;

            int n, m;
            int[,] c, f;
            int[] d;

            List<int>[] line;
            int ret = 0;

            int source, sink;

            // 방문을 막아버리면 91%쯤에서 틀린다
            bool[] visit;

            Solve();
            void Solve()
            {

                Input();

                MaxFlow();

                Console.WriteLine(ret);
            }
#if !Wrong
            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                source = 1;
                sink = 4;

                c = new int[2 * n + 1, 2 * n + 1];
                f = new int[2 * n + 1, 2 * n + 1];
                d = new int[2 * n + 1];
                line = new List<int>[2 * n + 1];

                for (int i = 1; i <= 2 * n; i++)
                {

                    line[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int from = ReadInt();
                    int to = ReadInt();

                    line[2 * from].Add(2 * to - 1);
                    line[2 * to - 1].Add(2 * from);

                    line[2 * to].Add(2 * from - 1);
                    line[2 * from - 1].Add(2 * to);

                    c[2 * from, 2 * to - 1] = 1;
                    c[2 * to, 2 * from - 1] = 1;
                }

                c[1, 2] = INF;
                c[3, 4] = INF;

                line[1].Add(2);
                line[2].Add(1);
                line[3].Add(4);
                line[4].Add(3);

                for (int i = 3; i <= n; i++)
                {

                    line[2 * i - 1].Add(2 * i);
                    line[2 * i].Add(2 * i - 1);

                    c[2 * i - 1, 2 * i] = 1;
                }
            }

            void MaxFlow()
            {

                Queue<int> q = new(2 * n);

                while (true)
                {

                    Array.Fill(d, -1);
                    q.Enqueue(source);

                    while(q.Count > 0)
                    {

                        int cur = q.Dequeue();

                        for (int i = 0; i < line[cur].Count; i++)
                        {

                            int next = line[cur][i];

                            if (c[cur, next] - f[cur, next] > 0 && d[next] == -1)
                            {

                                q.Enqueue(next);
                                d[next] = cur;
                                if (next == sink) break;
                            }
                        }
                    }

                    if (d[sink] == -1) break;
                    int flow = INF;

                    for (int i = sink; i != source; i = d[i])
                    {

                        flow = Math.Min(flow, c[d[i], i] - f[d[i], i]);
                    }

                    for (int i = sink; i != source; i = d[i])
                    {

                        f[d[i], i] += flow;
                        f[i, d[i]] -= flow;
                    }

                    ret += flow;
                }
            }
#else

            // 해당 정점을 막아버려서 역으로 흐르는 것을 막아버린다
            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                c = new int[n + 1, n + 1];
                f = new int[n + 1, n + 1];
                d = new int[n + 1];

                line = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    line[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int from = ReadInt();
                    int to = ReadInt();

                    line[from].Add(to);
                    line[to].Add(from);

                    c[from, to] = 1;
                    c[to, from] = 1;
                }

                sr.Close();
            }

            void MaxFlow()
            {

                Queue<int> q = new(n);
                visit = new bool[n + 1];
                source = 1;
                sink = 2;
                while (true)
                {

                    Array.Fill(d, -1);
                    q.Enqueue(source);

                    while(q.Count > 0)
                    {

                        int cur = q.Dequeue();
                        for (int i = 0; i < line[cur].Count; i++)
                        {

                            int next = line[cur][i];
                            if (visit[next]) continue;

                            if (c[cur, next] - f[cur, next] > 0 && d[next] == -1)
                            {

                                q.Enqueue(next);
                                d[next] = cur;
                                if (next == sink) break;
                            }
                        }
                    }

                    if (d[sink] == -1) break;
                    int flow = INF;

                    for (int i = sink; i != source; i = d[i])
                    {

                        flow = Math.Min(flow, c[d[i], i] - f[d[i], i]);
                    }

                    for (int i = sink; i != source; i = d[i])
                    {

                        f[d[i], i] += flow;
                        f[i, d[i]] -= flow;

                        visit[i] = true;
                    }

                    visit[sink] = false;
                    ret += flow;
                }
            }
#endif
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
        }
    }

#if other
// #include <stdio.h>
// #include <algorithm>
// #include <vector>
// #include <queue>
// #include <stack>
// #include <set>
// #include <string.h>
// #include <string>
// #include <iostream>
// #define ll long long
// #define limit 210000000

using namespace std;

int realparent[401];

vector<int> road[401];

int N, P;

int main() {
	scanf("%d %d", &N, &P);
	for (int i = 0; i < P; i++) {
		int a, b;
		scanf("%d %d", &a, &b);
		road[a].push_back(b);
		road[b].push_back(a);
	}
	int ans = 0;
	while (1) {
		vector<int> parent(N + 1, 0);
		queue<int> q;
		parent[1] = 1;
		q.push(1);
		while (!q.empty() && !parent[2]) {
			int t = q.front();
			q.pop();
			/*for (int next : road[t]) {
				if (next == 1) continue;
				if (realparent[next]) {
					if (next == parent[t]) continue;
					if (parent[next]) {
						if (realparent[parent[next]] == next) continue;
						else {
							parent[next] = t;
							q.push(next);
						}
					}
					else {
						if (realparent[next] == 1) continue;
						parent[next] = t;
						parent[realparent[next]] = next;
						q.push(realparent[next]);
					}
				}
				else {
					if (parent[next]) continue;
					else {
						parent[next] = t;
						q.push(next);
					}
				}
			}*/
			if (realparent[t]) {
				int now;
				int before = t;

				for (now = realparent[t]; now != 1 && !parent[now]; now = realparent[now]) {
					for (int nextnext : road[now]) {
						if (nextnext == realparent[now] || parent[nextnext]) continue;
						parent[nextnext] = now;
						q.push(nextnext);
					}
					parent[now] = before;
					before = now;
				}
				if (now != 1) {
					for (int nextnext : road[now]) {
						if (parent[nextnext]) continue;
						parent[nextnext] = now;
						q.push(nextnext);
					}
					parent[now] = before;
				}
			}
			else {
				for (int next : road[t]) {
					if (!parent[next]) {
						parent[next] = t;
						q.push(next);
					}
				}
			}
		}
		if (!parent[2]) break;
		for (int now = parent[2]; now != 1; now = parent[now]) {
			if (realparent[parent[now]] == now) realparent[parent[now]] = 0;
			else realparent[now] = parent[now];
		}
		ans++;
	}
	/*for (int s : road[2]) {
		for (int now = s; s != 1; s = realparent[s]) {
			printf("%d -> ", s);
		}
		printf("1\n");
	}*/
	printf("%d", ans);
}
/*
17 18
1 3
3 4
4 5
5 6
6 7
2 17
16 17
15 16
14 15
4 14
6 13
12 13
11 12
10 11
9 10
8 9
1 8
2 7
*/
#elif other2

#endif
}
