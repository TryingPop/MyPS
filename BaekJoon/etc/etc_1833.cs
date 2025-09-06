using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 25
이름 : 배성훈
내용 : 나는 연어입니다.
    문제번호 : 31933번

    분리집합, 이분 탐색 문제다.
    최소 신장 트리와 이분 탐색을 이용해 풀었다.
    아이디어는 다음과 같다.
    
    각 간선에 대해 l을 기준으로 오름차순 정렬한다.
    그리고 각간선을 하나씩 사용을 시도한다.

    만약 사용한 간선 중 r의 값이 l미만인 경우 해당 간선은 사용할 수 없다.
    그래서 해당 간선을 뺀다. 이는 우선순위 큐에 저장하면 O(1)에 가깝게 해결이 가능하다.

    이제 모두 r의 값이 l이상인 경우에 한해 MST를 한다.
    여기서 1과 n이 연결될 때까지 가장 큰 r을 찾는다.
    여기서 l은 현재 간선의 l로 잡아도 된다.
    왜냐하면 연결 중 최소 l을 찾는 경우 이전 경우에서 확인이 되기 때문이다.

    이렇게 MST를 만들고, 1과 n이 이어져있는지 확인한다.
    이어진 경우 l, r의 값을 기록한다.

    이렇게 모든 간선을 이용해 l, r의 값이 존재하는 것을 모두 찾는다.
    이후 연어들을 크기로 나열한 뒤 포함되는 연어를 그리디로 찾아간다.
    해당 방법은 l, r구간을 r의 끝값으로 오름 차순으로 정렬하고, r이 같은 경우 l의 오름차순이되게 정렬한다.
    
    그리고 이전 탐색 값 x을 기록한다.
    r의 값이 x보다 작은 경우 해당 구간은 이미 조사를 마쳤다.
    그래서 넘어간다
    반면 r이 x보다 크거나 같은 경우 해당 구간을 조사하는데,
    현재 l의 값이 x와 비교해서 중복된 구간은 피한다.
    둘 중 큰 값을 비교하면 된다.
    여기에 포함된 연어를 이분탐색으로 찾아줬다.

    그리고 x를 r + 1의 값으로 둔다.
    이렇게 가능한 구간을 모두 비교하면 정답이 된다!
*/

namespace BaekJoon.etc
{
    internal class etc_1833
    {

        // 내일 최적화 코드 다시 작성해보기!
        static void Main1833(string[] args)
        {

            int n, m, k;
            (int f, int t, int l, int r)[] edge;
            int[] fish;

            Input();

            GetRet();

            void GetRet()
            {

                Array.Sort(fish);
                PriorityQueue<int, int> pq1 = new(m), pq2 = new(m), pq3 = new(m);
                HashSet<int> use = new(m);

                List<(int s, int e)> possible = new(m);

                for (int i = 0; i < m; i++)
                {

                    pq1.Enqueue(i, edge[i].l);
                }

                int[] group = new int[n + 1];
                int[] stk = new int[n];
                for (int i = 1; i <= n; i++)
                {

                    group[i] = i;
                }

                while (pq1.Count > 0)
                {

                    int curIdx = pq1.Dequeue();

                    PopUse(edge[curIdx].l);
                    use.Add(curIdx);

                    int sup = MST();

                    if (Find(1) == Find(n))
                    {

                        int inf = FindInf(edge[curIdx].l);
                        sup = FindSup(sup);
                        if (inf == k || sup == -1 || sup < inf) continue;
                        possible.Add((inf, sup));
                    }
                }

                CntRet();

                int MST()
                {

                    // 크루스칼
                    pq3.Clear();

                    foreach (int item in use)
                    {

                        pq3.Enqueue(item, -edge[item].r);
                    }

                    InitGroup();

                    int ret = 2_000_000_005;
                    while (pq3.Count > 0)
                    {

                        int idx = pq3.Dequeue();

                        Union(edge[idx].f, edge[idx].t);
                        ret = Math.Min(ret, edge[idx].r);
                        if (Find(1) == Find(n)) break;
                    }

                    return ret;
                }

                void CntRet()
                {

                    // 합집합 원소 개수 찾기 그리디
                    possible.Sort((x, y) =>
                    {

                        int ret = x.e.CompareTo(y.e);
                        if (ret == 0) ret = x.s.CompareTo(y.s);
                        return ret;
                    });

                    int chkIdx = 0;
                    int ret = 0;

                    for (int i = 0; i < possible.Count; i++)
                    {

                        if (possible[i].e < chkIdx) continue;

                        int s = Math.Max(possible[i].s, chkIdx);
                        chkIdx = possible[i].e + 1;
                        ret += possible[i].e - s + 1;
                    }

                    Console.Write(ret);
                }

                void PopUse(int _l)
                {

                    while (pq2.Count > 0 && edge[pq2.Peek()].r < _l)
                    {

                        use.Remove(pq2.Peek());
                        pq2.Dequeue();
                    }
                }

                void InitGroup()
                {

                    // 초기화
                    for (int i = 1; i <= n; i++)
                    {

                        group[i] = i;
                    }
                }

                int Find(int _chk)
                {

                    // 파인드
                    int len = 0;

                    while (group[_chk] != _chk)
                    {

                        stk[len++] = _chk;
                        _chk = group[_chk];
                    }

                    while (len-- > 0)
                    {

                        group[stk[len]] = _chk;
                    }

                    return _chk;
                }

                void Union(int _f, int _t)
                {

                    // 유니온
                    int f = Find(_f);
                    int t = Find(_t);
                    if (f == t) return;

                    int min = f < t ? f : t;
                    int max = f < t ? t : f;

                    group[max] = min;
                }

                int FindSup(int _val)
                {

                    int l = 0;
                    int r = k - 1;

                    while (l <= r)
                    {

                        int mid = (l + r) >> 1;
                        if (fish[mid] <= _val) l = mid + 1;
                        else r = mid - 1;
                    }

                    return r;
                }

                int FindInf(int _val)
                {

                    int l = 0;
                    int r = k - 1;

                    while (l <= r)
                    {

                        int mid = (l + r) >> 1;
                        if (fish[mid] < _val) l = mid + 1;
                        else r = mid - 1;
                    }

                    return l;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                edge = new (int f, int t, int l, int r)[m];
                for (int i = 0; i < m; i++)
                {

                    edge[i] = (ReadInt(), ReadInt(), ReadInt(), ReadInt());
                }

                k = ReadInt();
                fish = new int[k];
                for (int i = 0; i < k; i++)
                {

                    fish[i] = ReadInt();
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
// #include <bits/stdc++.h>

using namespace std;
typedef long long ll;

int main() {
    ios::sync_with_stdio(false);
    cin.tie(NULL);

    int n, m;
    cin >> n >> m;
    vector<vector<tuple<int, int, int>>> v(n+1);
    for (int i=0; i<m; i++) {
        int a, b, l, r;
        cin >> a >> b >> l >> r;
        v[a].push_back({l, r, b});
        v[b].push_back({l, r, a});
    }

    vector<set<pair<int, int>>> vs(n+1);
    queue<tuple<int, int, int>> q;
    q.push({0, 1e9, 1});
    while (!q.empty()) {
        auto [l, r, node] = q.front();
        q.pop();
        auto it = vs[node].upper_bound({r, 1e9+1});
        bool flag = true;
        if (it == vs[node].begin()) vs[node].insert({l, r});
        else {
            while (it != vs[node].begin()) {
                it--;
                if (it->second < l) break;
                if (it->first <= l && it->second >= r) {
                    flag = false;
                    break;
                }
                r = max(r, it->second);
                l = min(l, it->first);
                it = vs[node].erase(it);
            }
            if (flag)   vs[node].insert({l, r});
            else    continue;
        }

        for (auto [nl, nr, nnode] : v[node]) {
            nl = max(nl, l);
            nr = min(nr, r);
            if (nl > nr)    continue;
            q.push({nl, nr, nnode});
        }
    }

    int k;
    cin >> k;

    int ans = 0;
    for (int i=0; i<k; i++) {
        int x;  cin >> x;
        auto it = vs[n].lower_bound({x+1, -1});
        if (it == vs[n].begin())    continue;
        it--;
        if (it->second < x) continue;
        ans++;
    }
    cout << ans;
}
#elif other2
// #include <iostream>
// #include <vector>
// #include <cstring>
// #include <algorithm>
using namespace std;

// #define N 5001
vector<int> v = { 0 };
int parent[N], arr[N][4];
bool check[10002];
int find(int n){
	if (parent[n] < 0) return n;
	return parent[n] = find(parent[n]);
}
void merge(int u, int v){
	u = find(u), v = find(v);
	if (u == v) return;
	if (parent[u] > parent[v]) swap(u, v);
	parent[u] += parent[v];
	parent[v] = u;
}
int main(void){
	cin.tie(0);
	ios::sync_with_stdio(0);

	int n, m; cin >> n >> m;
	for (int i = 0; i < m; i++)
	for (int j = 0; j < 4; j++){
		cin >> arr[i][j];
		v.push_back(arr[i][2]);
		v.push_back(arr[i][3] + 1);
	}
	sort(v.begin(), v.end());
	v.erase(unique(v.begin(), v.end()), v.end());
	v.push_back(1100000000);

	for (int i = 0; i < m; i++){
		arr[i][2] = lower_bound(v.begin(), v.end(), arr[i][2]) - v.begin();
		arr[i][3] = lower_bound(v.begin(), v.end(), arr[i][3] + 1) - v.begin();
	}
	
	for (int i = 1; i < (int)v.size() - 1; i++){
		int idx = i;
		memset(parent, -1, sizeof(parent));
		for (int j = 0; j < m; j++)
		if (arr[j][2] <= idx && idx < arr[j][3])
			merge(arr[j][0], arr[j][1]);
		check[i] = (find(1) == find(n));
	}

	int q, x, answer = 0; cin >> q;
	while (q--){
		cin >> x;
		int idx = upper_bound(v.begin(), v.end(), x) - v.begin() - 1;
		answer += (1 <= idx && idx < (int)v.size() - 1 && check[idx]);
	}
	cout << answer;
	return 0;
}
#endif
}
