using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 3
이름 : 배성훈
내용 : 합리적인 이동경로
    문제번호 : 2176번

    다익스트라, dp 문제다
    dp 확인에서 무턱대고 집어 넣다가 메모리 초과로 1번 틀렸고,
    다익스트라를 잘못 구현해 1번 틀렸다

    아이디어는 다음과 같다
    우선 t에 대해 다익스트라를 한다
    양방향 간선이므로 최단 거리를 찾을 수 있다

    그리고 s에서 시작해 t와 거리가 가까워지는 경우
    현재지점의 경우의 수를 다음 지역의 경우의 수에 누적한다

    이렇게 도착지까지 누적해가면 되는데,
    이동 경로를 보고 거리가 짧아지는 순으로 확인하며 누적해갔다
    이러니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0939
    {

        static void Main939(string[] args)
        {

            int INF = 10_000_001;
            StreamReader sr;

            int n, m;
            List<(int dst, int dis)>[] edge;
            int s, t;

            PriorityQueue<int, int> pq;
            int[] dis;
            bool[] visit;

            int[] ret;

            Solve();
            void Solve()
            {

                Input();

                Dijkstra();

                GetRet();
            }

            void GetRet()
            {

                ret = new int[n + 1];

                // visit이 방문 여부가 아닌
                // 우선순위 큐에 있는지로 역할이 바뀐다!
                Array.Fill(visit, false);

                pq.Enqueue(s, -dis[s]);
                ret[s] = 1;
                visit[s] = true;

                while (pq.Count > 0)
                {

                    int node = pq.Dequeue();
                    visit[node] = false;

                    for (int i = 0; i < edge[node].Count; i++)
                    {

                        int next = edge[node][i].dst;
                        if (dis[node] <= dis[next]) continue;
                        ret[next] += ret[node];

                        if (visit[next]) continue;
                        visit[next] = true;
                        pq.Enqueue(next, -dis[next]);
                    }
                }

                Console.Write(ret[t]);
            }

            void Dijkstra()
            {

                dis = new int[n + 1];
                Array.Fill(dis, INF);
                dis[t] = 0;

                visit = new bool[n + 1];

                pq = new(n);
                pq.Enqueue(t, 0);

                while (pq.Count > 0)
                {

                    int node = pq.Dequeue();
                    if (visit[node]) continue;
                    visit[node] = true;

                    for (int i = 0; i < edge[node].Count; i++)
                    {

                        int next = edge[node][i].dst;
                        if (visit[next]) continue;

                        int chk = dis[node] + edge[node][i].dis;
                        if (dis[next] <= chk) continue;
                        dis[next] = dis[node] + edge[node][i].dis;
                        pq.Enqueue(next, dis[next]);
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                edge = new List<(int dst, int dis)>[n + 1];
                dis = new int[n + 1];

                s = 1;
                t = 2;

                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
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
// #include <cstdio>
// #include <cstring>
// #include <vector>
// #include <queue>
// #define N 1010

using namespace std;

struct st
{
	int t, w;

	bool operator<(const st &hs) const
	{
		return w > hs.w;
	}
};

int n, m, d[N], p[N];
vector<st> e[N];
priority_queue<st> pq;

int f(int x)
{
	if(x == 0)
		return 1;

	if(p[x] == -1)
	{
		int sum = 0;
		for(st &to : e[x])
			if(d[x] < d[to.t])
				sum += f(to.t);
		p[x] = sum;
	}
	return p[x];
}

int main()
{
	scanf("%d %d", &n, &m);
	for(int i=0; i<m; ++i)
	{
		int a, b, c;
		scanf("%d %d %d", &a, &b, &c);
		a--, b--;
		e[a].push_back({b, c});
		e[b].push_back({a, c});
	}
	
	memset(d, 0x7f, sizeof(d));
	memset(p, -1, sizeof(p));
	d[1] = 0;
	pq.push({1, 0});
	while(!pq.empty())
	{
		int t = pq.top().t;
		int w = pq.top().w;
		pq.pop();
		if(d[t] < w)
			continue;

		for(st &to : e[t])
			if(d[to.t] > w+to.w)
			{
				d[to.t] = w+to.w;
				pq.push({to.t, d[to.t]});
			}
	}

	printf("%d", f(1));
	return 0;
}

#endif
}
