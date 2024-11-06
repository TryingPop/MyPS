using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 13
이름 : 배성훈
내용 : Punching Power
    문제번호 : 14986번

    이분 매칭 문제다
    이분 매칭 단원 들어가기 전에, 복기할겸 먼저 풀었다
    아이디어는 다음과 같다
    뺄 기계를 찾았다
    거리가 1.3 이상 되게 해야하므로 이는 좌표 차가 1이하인 경우에 빼야한다!
    
    //////////
    간선을 거리 1차이로만 연결하고 이분 매칭 했다면
    이상 있는 것을 순차적으로 최대한 뺄 수 있는 만큼 뺀 경우와 같다
    //////////
    
    그래서 거리가 1이하이고, 합이 짝수에서 홀수로 연결시켰다
    (기기당 위치는 고유(유일)하므로 겹치는 경우는 없다!)
    그리고 이분 매칭 시켰다
    그러면 매칭된 것 중 적어도 1개는 빠져야한다
    여기서 적당히 선택하면, 제거할 기기를 최소가 되게 뺀 경우와 일치하다고 생각했고
    간선의 개수를 빼서 제출하니 이상없이 통과했다

    이를 찾아보니, 최소 버텍스 커버와 이분 매칭의 최대 매칭과 일치한다는 글을 찾을 수 있었다(쾨닉의 정리)
*/

namespace BaekJoon.etc
{
    internal class etc_0690
    {

        static void Main690(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n;
            (int x, int y)[] pos;
            List<int>[] line;

            int[] match;
            bool[] visit;

            Solve();

            void Solve()
            {

                Init();

                while (Input())
                {

                    LinkLine();

                    int ret = n;
                    for (int i = 0; i < n; i++)
                    {

                        Array.Fill(visit, false, 0, n);
                        if (DFS(i)) ret--;
                    }

                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            bool DFS(int _n)
            {

                for (int i = 0; i < line[_n].Count; i++)
                {

                    int next = line[_n][i];
                    if (visit[next]) continue;
                    visit[next] = true;

                    if (match[next] == -1 || DFS(match[next]))
                    {

                        match[next] = _n;
                        return true;
                    }
                }

                return false;
            }

            void LinkLine()
            {

                for (int i = 0; i < n - 1; i++)
                {

                    int x1 = pos[i].x;
                    int y1 = pos[i].y;
                    bool isI = ((x1 + y1) % 2) == 0;

                    for (int j = i + 1; j < n; j++)
                    {

                        int diff1 = x1 - pos[j].x;
                        diff1 = diff1 < 0 ? -diff1 : diff1;
                        if (diff1 > 1) continue;

                        int diff2 = y1 - pos[j].y;
                        diff2 = diff2 < 0 ? -diff2 : diff2;

                        if (diff2 + diff1 > 1) continue;
                        if (isI) line[i].Add(j);
                        else line[j].Add(i);
                    }
                }
            }

            bool Input()
            {

                n = ReadInt();
                if (n == -1) return false;

                for (int i = 0; i < n; i++)
                {

                    pos[i] = (ReadInt(), ReadInt());
                    line[i].Clear();
                    match[i] = -1;
                }

                return true;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                pos = new (int x, int y)[2_000];
                line = new List<int>[2_000];

                for (int i = 0; i < 2_000; i++)
                {

                    line[i] = new();
                }

                visit = new bool[2_000];
                match = new int[2_000];
            }

            int ReadInt()
            {

                int c = sr.Read();
                if (c == '\r') c = sr.Read();
                if (c == -1 || c == ' ' || c == '\n') return -1;
                int ret = c - '0';
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
// #include <iostream>
// #include <vector>
// #include <cstring>
// #include <map>
using namespace std;

// #define N 2001
typedef pair<int, int> p;
int A[N], B[N];
bool visit[N];
vector<int> adj[N];
map<p, int> m;
p arr[N];

bool dfs(int now){
	visit[now] = true;
	for (int next : adj[now]){
		if (!B[next]){
			A[now] = next, B[next] = now;
			return true;
		}
		else if (!visit[B[next]] && dfs(B[next])){
			A[now] = next, B[next] = now;
			return true;
		}
	}
	return false;
}
int main(void){
	cin.tie(0);
	ios::sync_with_stdio(0);

	while (1){
		int n, x, y; cin >> n; if (cin.eof()) break;
		for (int i = 1; i <= n; i++){
			cin >> x >> y;
			arr[i] = p(x, y);
			m[arr[i]] = i;
		}

		int dx[4] = { -1, 1, 0, 0 }, dy[4] = { 0, 0, -1, 1 };
		for (int i = 1; i <= n; i++){
			x = arr[i].first, y = arr[i].second;
			if ((x + y) % 2) continue;
			for (int k = 0; k < 4; k++){
				int nx = x + dx[k], ny = y + dy[k];
				if (m.find(p(nx, ny)) != m.end()){
					int j = m[p(nx, ny)];
					adj[i].push_back(j);
				}
			}
		}
		int answer = 0;
		for (int i = 1; i <= n; i++)
		if (!A[i]){
			memset(visit, 0, sizeof(visit));
			answer += dfs(i);
		}
		cout << n - answer << '\n';
		memset(A, 0, sizeof(A));
		memset(B, 0, sizeof(B));
		for (int i = 1; i <= n; i++) adj[i].clear();
		m.clear();
	}
	return 0;
}
#elif other2
from sys import setrecursionlimit as SRL
SRL(15000)
from collections import deque
def BFS(ssize, tsize, adj, pairu, pairv, dist):
    Q = deque()
    for u in range(1, ssize+1):
        if pairu[u] == 0: dist[u] = 0; Q.append(u)
        else: dist[u] = float('inf')
    dist[0] = float('inf')
    while len(Q) > 0:
        u = Q.popleft()
        if dist[u] >= dist[0]: continue
        for v in adj[u]:
            if dist[pairv[v]] == float('inf'):
                dist[pairv[v]] = dist[u] + 1
                Q.append(pairv[v])
    return dist[0] != float('inf')

def DFS(ssize, tsize, adj, pairu, pairv, dist, u):
    if u == 0: return True
    for v in adj[u]:
        if dist[pairv[v]] == dist[u] + 1 and DFS(ssize, tsize, adj, pairu, pairv, dist, pairv[v]):
            pairv[v] = u; pairu[u] = v; return True
    dist[u] = float('inf'); return False

def HopcroftKarp(ssize, tsize, adj):
    pairu = [0]*(ssize+1); pairv = [0]*(tsize+1); dist = [-1]*(ssize+1)
    match = 0
    while BFS(ssize, tsize, adj, pairu, pairv, dist):
        for u in range(1, ssize+1):
            if pairu[u] == 0: match+= DFS(ssize, tsize, adj, pairu, pairv, dist, u)
    return match

from sys import stdin
input = stdin.readline
while 1:
    try: n = int(input())
    except: break
    d1 = {}
    d2 = {}
    for i in range(n):
        x, y = map(int,input().split())
        if (x+y)%2 == 0: d1[(x,y)] = len(d1)+1
        else: d2[(x,y)] = len(d2)+1
    adj = [[] for i in range(len(d1)+1)]
    for x, y in d1:
        i = d1[(x,y)]
        for nx, ny in (x+1,y), (x-1,y), (x,y-1), (x,y+1):
            if (nx,ny) not in d2: continue
            adj[i].append(d2[(nx,ny)])
    print(n - HopcroftKarp(len(d1), len(d2), adj))
#endif
}
