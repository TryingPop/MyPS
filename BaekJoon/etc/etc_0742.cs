using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 30
이름 : 배성훈
내용 : 범죄 파티
    문제번호 : 13166번

    이분 탐색, 이분 매칭 문제다
    아이디어는 다음과 같다
    가장 비싼 고용 비용이 최소가 되게 하는 문제다
    그래서 일정 가격 이하에 한해서만 간선이 유효하다고 판별하고
    모두 이분 매칭 되는지 판별했다

    일정 가격은 이분 탐색으로 찾았다
    만약 가격 a에서 이분 매칭이 된다면 a보다 큰 경우에 한해서는 항상 이분 매칭이 가능하다!
    (가격이 늘었따고 간선이 비활성화 되지 않는다 오히려 활성화 되는 간선이 늘어난다)
    이렇게 만족하는 최솟값을 찾게 하니 이상없이 통과했다

    힌트를 보니 2-sat로도 가능하다고 되어져 있다
    난이도 기여를 보니, 한쪽을 대변해주면 다른 한쪽은 대변 못하는 식으로 2sat를 묶으면 된다고 한다
    그리고 해당 방법으로는 시간이 빡빡하다고 한다

    처음에 대변인 크기르 잘못 설정해 인덱스 에러 한 번 틀렸다
    그리고 여기 케이스에는 없지만 대변 못하는 경우
        3
        1 1 2 1
        1 1 2 1
        1 1 2 1

    인 경우도 고려하면 -1을 출력해줘야 하는데 안해줘서 (맞았지만) 1번 더 틀린다;
    총 2번 틀리고 380ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0742
    {

        static void Main742(string[] args)
        {

            StreamReader sr;

            int n;
            List<(int dst, int val)>[] line;

            int[] A, B, lvl, d;
            bool[] visit;

            Queue<int> q;
            int ret;

            Solve();

            void Solve()
            {

                Input();

                Init();

                GetRet();

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                line = new List<(int dst, int val)>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    line[i] = new(2);
                }

                for (int i = 1; i <= n; i++)
                {

                    for (int j = 0; j < 2; j++)
                    {

                        int dst = ReadInt();
                        int val = ReadInt();

                        line[i].Add((dst, val));
                    }
                }

                sr.Close();
            }

            void Init()
            {

                q = new(n);
                A = new int[n + 1];
                B = new int[2 * n + 1];

                lvl = new int[n + 1];
                visit = new bool[n + 1];
                d = new int[n + 1];

                lvl[0] = -1;
            }

            void GetRet()
            {

                ret = 0;

                int l = 0;
                int r = 1_000_000;
                while(l <= r)
                {

                    int mid = (l + r) / 2;
                    if (Match(mid)) r = mid - 1;
                    else l = mid + 1;
                }

                ret = r + 1;
                if (ret == 1_000_001) ret = -1;
            }

            bool Match(int _max)
            {

                int ret = 0;

                Array.Fill(A, 0);
                Array.Fill(B, 0);
                Array.Fill(visit, false);

                while (true)
                {

                    Array.Fill(d, 0);
                    BFS(_max);

                    int match = 0;

                    for (int i = 1; i <= n; i++)
                    {

                        if (!visit[i] && DFS(i, _max)) match++;
                    }

                    if (match == 0) break;
                    ret += match;
                }

                return ret == n;
            }

            bool DFS(int _a, int _max)
            {

                for (; d[_a] < line[_a].Count; d[_a]++)
                {

                    if (line[_a][d[_a]].val > _max) continue;
                    int b = line[_a][d[_a]].dst;

                    if (B[b] == 0 || lvl[B[b]] == lvl[_a] + 1 && DFS(B[b], _max))
                    {

                        visit[_a] = true;
                        A[_a] = b;
                        B[b] = _a;

                        return true;
                    }
                }
                return false;
            }

            void BFS(int _max)
            {

                for (int i = 1; i <= n; i++)
                {

                    if (visit[i]) lvl[i] = 0;
                    else
                    {

                        lvl[i] = 1;
                        q.Enqueue(i);
                    }
                }

                while(q.Count > 0)
                {

                    int a = q.Dequeue();

                    for (int i = 0; i < line[a].Count; i++)
                    {

                        if (line[a][i].val > _max) continue;
                        int b = line[a][i].dst;

                        if (lvl[B[b]] == 0)
                        {

                            lvl[B[b]] = lvl[a] + 1;
                            q.Enqueue(B[b]);
                        }
                    }
                }

                return;
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
// #include <iostream>
using namespace std;

int n;
pair<int, int> graph[200000][2];
int group[400001];
int group_type[400001];
int get_group(int x){
    if(group[x] == x)
        return x;
    return group[x] = get_group(group[x]);
}

int match(int threshold)
{
    for (int i = 1; i <= 2 * n; ++i) {
        group[i] = i;
        group_type[i] = 1;
    }

    for (int i = 1; i <= n; ++i) {
        if(graph[i][0].second > threshold)
            return 0;
        if(graph[i][1].second > threshold){
            int g1 = get_group(graph[i][0].first);
            if(!group_type[g1])
                return 0;
            group_type[g1] = 0;
        } else {
            int g1 = get_group(graph[i][0].first);
            int g2 = get_group(graph[i][1].first);
            if(!group_type[g1] && !group_type[g2])
                return 0;
            else {
                group[g1] = g2;
                group_type[g2] &= group_type[g1] && g1 != g2;
            }
        }
    }
    return 1;
}

int main(){
    ios::sync_with_stdio(false);
    cin.tie(0);
    cin >> n;

    for (int i = 1; i <= n; ++i) {
        int a, ka, b, kb;
        cin >> a >> ka >> b >> kb;
        if(ka > kb){
            swap(a, b);
            swap(ka, kb);
        }
        graph[i][0] = {a, ka};
        graph[i][1] = {b, kb};
    }

    int low = 0, high = 1000000;

    while (low < high){
        int mid = (low + high) / 2;
        if(match(mid))
            high = mid;
        else
            low = mid + 1;
    }

    cout << (low == 1000001 ? -1 : low);
}
#elif other2
/*
// #include <cstdio>
// #include <algorithm>
// #include <cstdlib>
// #include <cmath>
// #include <climits>
// #include <cstring>
// #include <string>
// #include <vector>
// #include <queue>
// #include <numeric>
// #include <functional>
// #include <set>
// #include <map>
// #include <unordered_map>
// #include <unordered_set>
// #include <memory>
// #include <thread>
// #include <tuple>
// #include <limits>
using namespace std;

struct PCSZ {
  // assumption: every root node points itself.
  struct node_t {
    int parent;
    int size;
    int aux;
    node_t() : size(1), aux(0) {}
  };
  vector<node_t> nodes;
  int negcnt;

  PCSZ(int size) : nodes(size), negcnt(0) {
    for (int i = 0; i < size; i++) {
      nodes[i].parent = i;
    }
  }

  // find root node
  int find(int index) {
    int root = index;
    while (root != nodes[root].parent) {
      root = nodes[root].parent;
    }
    while (root != nodes[index].parent) {
      int next = nodes[index].parent;
      nodes[index].parent = root;
      index = next;
    }
    return root;
  }

  // true if merge was successful
  bool merge(int first, int second) {
    int r1 = find(first);
    int r2 = find(second);
    if (r1 == r2) {
      return false;
    }
    if (nodes[r1].size < nodes[r2].size) {
      // careful, first and second not swapped
      swap(r1, r2);
    }
    if (nodes[r1].aux < 0) negcnt--;
    if (nodes[r2].aux < 0) negcnt--;
    nodes[r1].size += nodes[r2].size;
    nodes[r1].aux += nodes[r2].aux;
    nodes[r2].parent = r1;
    if (nodes[r1].aux < 0) negcnt++;
    return true;
  }

  int group_size(int index) {
    return nodes[find(index)].size;
  }
  int aux(int index) {
    return nodes[find(index)].aux;
  }
};

int main() {
  int n;
  scanf("%d", &n);
  PCSZ pcsz(3 * n);
  vector<tuple<int, int, int>> dat;
  for (int i = 0; i < n; i++) {
    int a, ka, b, kb;
    scanf("%d%d%d%d", &a, &ka, &b, &kb);
    a--, b--;
    dat.emplace_back(ka, i, a + n);
    dat.emplace_back(kb, i, b + n);
  }
  for (int i = 0; i < n; i++) {
    pcsz.nodes[i].aux = -1;
    pcsz.negcnt++;
  }
  for (int i = n; i < 3 * n; i++) {
    pcsz.nodes[i].aux = 1;
  }
  sort(dat.begin(), dat.end());
  for (auto d : dat) {
    int cost, a, b;
    tie(cost, a, b) = d;
    pcsz.merge(a, b);
    if (pcsz.negcnt <= 0) {
      printf("%d\n", cost);
      return 0;
    }
  }
  printf("-1\n");
  return 0;
}

#elif other3
// #include <bits/stdc++.h>
using namespace std;

// #define eb emplace_back

struct EDGE {
	int v, c1, c2;
	EDGE(int vv, int cc1, int cc2) : v(vv), c1(cc1), c2(cc2) {}
};

const int MAXN = 200010;

vector<EDGE> ed[MAXN];
bool vis[2 * MAXN];

int main() {
	ios::sync_with_stdio(0); cin.tie(0);
	int N;

	cin >> N;
	for(int i = 0; i < N; i++) {
		int a, b, c, d;
		cin >> a >> b >> c >> d;
		ed[a].eb(c, b, d);
		ed[c].eb(a, d, b);
	}

	int ans = 0;
	for(int i = 1; i <= 2 * N; i++) if(!vis[i] && ed[i].size() == 1) {
		int p = i;
		vector<int> c1, c2;
		while(true) {
			vis[p] = true;
			if(p != i && ed[p].size() == 1) break;
			for(auto a : ed[p]) if(!vis[a.v]) {
				c1.eb(a.c1);
				c2.eb(a.c2);
				p = a.v;
				break;
			}
		}
		/*
		printf("c1 : ");
		for(auto a : c1) cout << a << " ";
		printf("\nc2 : ");
		for(auto a : c2) cout << a << " ";
		puts("");
		*/
		for(int j = 1; j < c1.size(); j++) c1[j] = max(c1[j], c1[j - 1]);
		for(int j = (int) c2.size() - 2; j >= 0; j--) c2[j] = max(c2[j], c2[j + 1]);
		int mx = c2[0];
		c2.eb(0);
		for(int j = 0; j < c1.size(); j++) mx = min(mx, max(c1[j], c2[j + 1]));
		ans = max(ans, mx);
	}

	for(int i = 1; i <= 2 * N; i++) if(!vis[i] && !ed[i].empty()) {
		int p = i;
		vector<int> c1, c2;
		while(true) {
			vis[p] = true;
			if(vis[ed[p][0].v] && vis[ed[p][1].v]) break;
			for(auto a : ed[p]) if(!vis[a.v]) {
				c1.eb(a.c1);
				c2.eb(a.c2);
				p = a.v;
				break;
			}
		}
		c1.eb(ed[i][1].c2);
		c2.eb(ed[i][1].c1);
		/*
		printf("c1 : ");
		for(auto a : c1) cout << a << " ";
		printf("\nc2 : ");
		for(auto a : c2) cout << a << " ";
		puts("");
		*/
		int m1 = 0;
		for(auto a : c1) m1 = max(m1, a);
		int m2 = 0;
		for(auto a : c2) m2 = max(m2, a);
		ans = max(ans, min(m1, m2));
	}

	cout << ans << "\n";
	return 0;
}

#endif
}
