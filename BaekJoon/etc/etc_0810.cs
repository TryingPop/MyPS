using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 13
이름 : 배성훈
내용 : 경주
    문제번호 : 5820번

    센트로이드 분할 문제다
    트리의 모양을 층이 log N이 되게 변형하고

    중심에서 시작해 탐색을 시작한다
    그러면 계층은 많아야 logN이되고

    매 계층의 모든 원소를 탐색하면 N log N 이 된다
    아직은 감이 잘 안온다
    몇 문제 더 풀어봐야겠다
*/

namespace BaekJoon.etc
{
    internal class etc_0810
    {

        static void Main810(string[] args)
        {

            int INF = 10_000_000;
            StreamReader sr;
            int n, k, ret = INF;

            int[] size;
            bool[] visit;

            int[] min;
            List<int> tree;

            List<(int dst, int dis)>[] line;

            Solve();
            void Solve()
            {

                Input();

                ret = DNC(0);

                ret = ret == INF ? -1 : ret;
                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                k = ReadInt();

                visit = new bool[n + 1];
                size = new int[n + 1];
                min = new int[k + 1];

                Array.Fill(min, INF);
                tree = new(n);


                line = new List<(int dst, int dis)>[n];
                for (int i = 0; i < n; i++)
                {

                    line[i] = new();
                }
                
                for (int i = 0; i < n - 1; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    int dis = ReadInt();

                    line[f].Add((b, dis));
                    line[b].Add((f, dis));
                }

                sr.Close();
            }

            int GetSize(int _cur, int _before = -1)
            {

                // 방문한 점을 제외한 자식 찾기
                // O(N)
                size[_cur] = 1;

                for (int i = 0; i < line[_cur].Count; i++)
                {

                    int next = line[_cur][i].dst;
                    if (_before == next || visit[next]) continue;

                    size[_cur] += GetSize(next, _cur);
                }

                return size[_cur];
            }

            int GetCent(int _cur, int _cap, int _before = -1)
            {

                // 중심을 찾는다
                // 자식들의 크기가 모두 n / 2를 만족할 때 중심이라 한다
                // 즉 자신이 n / 2 > 보다 크면 중심이 안된다
                // O (log N)이 예상
                for (int i = 0; i < line[_cur].Count; i++)
                {

                    int next = line[_cur][i].dst;
                    if (next == _before || size[next] * 2 <= _cap || visit[next]) continue;
                    return GetCent(next, _cap, _cur);
                }

                return _cur;
            }

            int DFS1(int _cur, int _before, int _dis, int _depth)
            {

                // 거리가 k이하면서, 깊이가 낮은걸 저장
                int ret = INF;

                // 거리 초과하면 해당 경로는 탐색 종료
                if (_dis > k) return ret;

                // dp로 k까지 가는데 필요한 최소 노드 개수 확인
                ret = Math.Min(ret, min[k - _dis] + _depth);

                for (int i = 0; i < line[_cur].Count; i++)
                {

                    int next = line[_cur][i].dst;

                    if (_before == next || visit[next]) continue;
                    ret = Math.Min(ret, DFS1(next, _cur, _dis + line[_cur][i].dis, _depth + 1));
                }

                return ret;
            }

            void DFS2(int _cur, int _before, int _dis, int _depth)
            {

                if (_dis > k) return;

                min[_dis] = Math.Min(min[_dis], _depth);
                tree.Add(_dis);

                for (int i = 0; i < line[_cur].Count; i++)
                {

                    int next = line[_cur][i].dst;

                    if (_before == next || visit[next]) continue;

                    DFS2(next, _cur, _dis + line[_cur][i].dis, _depth + 1);
                }
            }
            
            int DNC(int _cur)
            {

                int size = GetSize(_cur);

                int cent = GetCent(_cur, size);

                visit[cent] = true;

                int ret = INF;

                for (int i = 0; i < tree.Count; i++)
                {

                    min[tree[i]] = INF;
                }

                tree.Clear();

                min[0] = 0;

                // 해당 중심에서 해당하는 것만 갱신
                for (int i = 0; i < line[cent].Count; i++)
                {

                    int next = line[cent][i].dst;

                    if (visit[next]) continue;

                    ret = Math.Min(ret, DFS1(next, cent, line[cent][i].dis, 1));

                    DFS2(next, cent, line[cent][i].dis, 1);
                }

                for (int i = 0; i < line[cent].Count; i++)
                {

                    int next = line[cent][i].dst;

                    if (visit[next]) continue;

                    ret = Math.Min(ret, DNC(next));
                }

                return ret;
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
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayDeque;
import java.util.StringTokenizer;

public class Main {
	private static final int INF = Integer.MAX_VALUE >> 1;
	
	private static final class Node {
		int idx;
		int weight;
		Node next;
		
		Node(int idx, int weight, Node next) {
			this.idx = idx;
			this.weight = weight;
			this.next = next;
		}
	}
	
	private static int k;
	private static int[] subSize;
	private static int[] minDepth;
	private static int[] subDepth;
	private static boolean[] visited;
	private static Node[] adj;
	private static ArrayDeque<Integer> copy;
	private static ArrayDeque<Integer> updated;
	
	private static int getSize(int curr, int parent) {
		Node child;
		
		subSize[curr] = 1;
		for (child = adj[curr]; child != null; child = child.next) {
			if (visited[child.idx] || child.idx == parent) {
				continue;
			}
			subSize[curr] += getSize(child.idx, curr);
		}
		return subSize[curr];
	}
	
	private static int getCentroid(int curr, int parent, int threshold) {
		Node child;
		
		for (child = adj[curr]; child != null; child = child.next) {
			if (visited[child.idx] || child.idx == parent) {
				continue;
			}
			if (subSize[child.idx] > threshold) {
				return getCentroid(child.idx, curr, threshold);
			}
		}
		return curr;
	}
	
	private static int dfs(int curr, int parent, int dist, int depth) {
		int ret;
		Node child;
		
		if (dist > k) {
			return INF;
		}
		ret = INF;
		if (depth < subDepth[dist]) {
			subDepth[dist] = depth;
			copy.addLast(dist);
			updated.addLast(dist);
		}
		ret = Math.min(ret, minDepth[k - dist] + depth);
		for (child = adj[curr]; child != null; child = child.next) {
			if (visited[child.idx] || child.idx == parent) {
				continue;
			}
			ret = Math.min(ret, dfs(child.idx, curr, dist + child.weight, depth + 1));
		}
		return ret;
	}
	
	private static int dnc(int curr) {
		int ret;
		int dist;
		int centroid;
		Node child;
		
		centroid = getCentroid(curr, -1, getSize(curr, -1) >> 1);
		visited[centroid] = true;
		while (!updated.isEmpty()) {
			minDepth[updated.pollFirst()] = INF;
		}
		while (!copy.isEmpty()) {
			subDepth[copy.pollFirst()] = INF;
		}
		ret = INF;
		for (child = adj[centroid]; child != null; child = child.next) {
			if (visited[child.idx]) {
				continue;
			}
			while (!copy.isEmpty()) {
				dist = copy.pollFirst();
				if (subDepth[dist] < minDepth[dist]) {
					minDepth[dist] = subDepth[dist];
					updated.addLast(dist);
				}
				subDepth[dist] = INF;
			}
			ret = Math.min(ret, dfs(child.idx, centroid, child.weight, 1));
		}
		for (child = adj[centroid]; child != null; child = child.next) {
			if (visited[child.idx]) {
				continue;
			}
			ret = Math.min(ret, dnc(child.idx));
		}
		return ret;
	}
	
	public static void main(String[] args) throws IOException {
		int n;
		int u;
		int v;
		int l;
		int i;
		int ans;
		BufferedReader br;
		StringTokenizer st;
		
		br = new BufferedReader(new InputStreamReader(System.in));
		st = new StringTokenizer(br.readLine());
		n = Integer.parseInt(st.nextToken());
		k = Integer.parseInt(st.nextToken());
		adj = new Node[n];
		for (i = 1; i < n; i++) {
			st = new StringTokenizer(br.readLine());
			u = Integer.parseInt(st.nextToken());
			v = Integer.parseInt(st.nextToken());
			l = Integer.parseInt(st.nextToken());
			adj[u] = new Node(v, l, adj[u]);
			adj[v] = new Node(u, l, adj[v]);
		}
		subSize = new int[n];
		visited = new boolean[n];
		minDepth = new int[k + 1];
		subDepth = new int[k + 1];
		for (i = 1; i <= k; i++) {
			minDepth[i] = INF;
			subDepth[i] = INF;
		}
		copy = new ArrayDeque<>(n);
		updated = new ArrayDeque<>(n);
		ans = dnc(0);
		System.out.print(ans == INF ? "-1" : ans);
	}
}

#elif other2
from random import randrange
import sys
input = sys.stdin.readline

N, K = map(int, input().split())

connect = [{} for _ in range(N)]
child = [[] for _ in range(N)]
for _ in range(N - 1):
    H0, H1, L = map(int, input().split())
    connect[H0][H1] = L
    connect[H1][H0] = L
    child[H0].append(H1)
    child[H1].append(H0)

visited = [False] * N
size = [0] * N


def get_size(u):
    stack = [(u, -1, -1)]
    while stack:
        u, p, i = stack.pop()
        if i < 0:
            size[u] = 1
        else:
            v = child[u][i]
            size[u] += size[v]
        i += 1
        while i < len(child[u]):
            v = child[u][i]
            if v == p or visited[v]:
                i += 1
                continue
            stack.append((u, p, i))
            stack.append((v, u, -1))
            break


def centroid(u, s):
    stack = [(u, -1, -1)]
    while stack:
        u, p, i = stack.pop()
        if i >= 0:
            v = child[u][i]
            size[u] += size[v]
        i += 1
        while i < len(child[u]):
            v = child[u][i]
            if v == p or visited[v] or not size[v] << 1 > s:
                i += 1
                continue
            stack.append((u, p, i))
            stack.append((v, u, -1))
            break
        else:
            return u


def dfs(u, par):
    ret = []
    stack = [(u, par, 0, 1, -1)]
    while stack:
        u, p, dist, depth, i = stack.pop()
        if dist > K:
            continue
        if i < 0:
            ret.append((dist + connect[p][u], depth))
        i += 1
        while i < len(child[u]):
            v = child[u][i]
            if v == p or visited[v]:
                i += 1
                continue
            stack.append((u, p, dist, depth, i))
            stack.append((v, u, dist + connect[p][u], depth + 1, -1))
            break
    return ret


def dnc(u):
    get_size(u)
    c = centroid(u, size[u])
    ret = 1 << 63
    dist_map = {}
    for v in connect[c]:
        if not visited[v]:
            root_dist = dfs(v, c)
            for d, depth in root_dist:
                if d == K:
                    ret = min(depth, ret)
                if K - d in dist_map:
                    ret = min(dist_map[K - d] + depth, ret)
            for d, depth in root_dist:
                if d in dist_map:
                    dist_map[d] = min(depth, dist_map[d])
                else:
                    dist_map[d] = depth
            del root_dist
    del dist_map
    visited[c] = True
    for v in connect[c]:
        if not visited[v]:
            ret = min(ret, dnc(v))
    return ret


ans = dnc(randrange(0, N))
print(ans if ans < 1 << 63 else -1)

#elif other3
// #include <bits/stdc++.h>
// #pragma GCC optimize("O3")
const char nl = '\n';
using namespace std;
using ll = long long;
using ld = long double;
const int N = 2e5+1;

int n, k;

vector<pair<int, int>> adj[N];

struct dat {
  set<pair<int, int>> s;
  int doff;
  int eoff;
};

int ans = N;
void merge(dat& a, dat& b) {
  if (a.s.size() < b.s.size()) swap(a, b);
  for (auto [p, q] : b.s) {
    auto it = a.s.lower_bound(pair(k - p - b.doff - a.doff, -N));
    if (it != end(a.s) && it->first + p + a.doff + b.doff == k) {
      ans = min(ans, q + b.eoff + it->second + a.eoff);
    }
  }
  for (auto [p, q] : b.s) {
    if (p + b.doff <= k) a.s.emplace(p + b.doff - a.doff, q + b.eoff - a.eoff);
  }
  while (!a.s.empty() && rbegin(a.s)->first + a.doff > k) a.s.erase(--a.s.end());
  b.s.clear();
}

dat dfs(int u, int p=-1) {
  dat ret;
  ret.doff = 0;
  ret.eoff = 0;
  ret.s.emplace(0, 0);
  for (auto [v, w] : adj[u]) {
    if (v == p) continue;
    auto dv = dfs(v, u);
    dv.doff += w;
    dv.eoff++;
    merge(ret, dv);
  }
  return ret;
}

char get() {
  static char buf[1000000], *S = buf, *T = buf;
  if (S == T) {
    T = (S = buf) + fread(buf, 1, 1000000, stdin);
    if (S == T) return EOF;
  }
  return *S++;
}
void read(int& x) {
  static char c; x = 0;
  for (c = get(); c < '0'; c = get());
  for (; c >= '0'; c = get()) x = x * 10 + c - '0';
}

int main() {
  cin.tie(0)->sync_with_stdio(0);
  read(n); read(k);
  for (int i = 0; i < n-1; i++) {
    int u, v, w; read(u); read(v); read(w);
    adj[u].emplace_back(v, w);
    adj[v].emplace_back(u, w);
  }
  dfs(0);
  cout << (ans == N ? -1 : ans) << nl;
}

#endif
}
