using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 29
이름 : 배성훈
내용 : 3에 깃든 힘
    문제번호 : 25622번

    그리디, 그래프 이론 문제다.
    처음에는 그리디로 깊이를 기록하고 가장 깊은 곳부터 시작해서 3개씩 묶는데,
    깊이 합이 크게 최대한 묶어주려고 시도했다.
    
    해당 방법은 매번 깊이를 확인해야 하므로 N^2으로 시간초과 날거 같았다.
    그래서 3개씩 묶이는 경우를 확인했다.

    임의의 노드를 루트를 삼으면 자식의 수에 따라 규칙이 있음을 확인했다.
    자식이 3의 배수이면 서브트리로 분할하면 된다.
    그리고 트리 형태로 주어지기에 3의 배수가 아닌 자식이 적어도 1개 존재한다.
    자식 중 3 * k + 1 이 2개 있거나 3 * k + 2가 1개 있는 이외의 경우면 3개로 분할 불가능하다.
    
    그리고 각 경우 3 * k + 1이 2개 있는 경우 루트와 해당 두 자식을 이어주면 된다.
    반면 3 * k + 2이 1개인 경우는 3 * k + 2의 노드로 가서
    3 * k + 1인 자식과 이어주면 된다.
    해당 자식이 없다면 불가능하다.

    이렇게 이어주면서 찾으니 이상없이 통과한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1225
    {

        static void Main1225(string[] args)
        {

            int n;
            List<int>[] edge;
            int[] child, parent;
            Queue<int> q;

            Solve();
            void Solve()
            {

                Input();

                SetChild();

                GetRet();
            }

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                (int a, int b, int c)[] subTree = new (int a, int b, int c)[n / 3];
                int len = 0;

                bool[] visit = new bool[n + 1];

                while (q.Count > 0)
                {

                    var node = q.Dequeue();

                    subTree[len].a = node;
                    visit[node] = true;
                    if (DFS(node)) len++;
                    else
                    {

                        len = 0;
                        break;
                    }
                }

                if (len != subTree.Length) sw.Write('U');
                else
                {

                    sw.Write($"S\n");
                    for (int i = 0; i < len; i++)
                    {

                        sw.Write($"{subTree[i].a} {subTree[i].b} {subTree[i].c}\n");
                    }
                }

                bool DFS(int _cur, int _depth = 1)
                {

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (next == parent[_cur] || visit[next] || child[next] % 3 == 0) continue;
                        visit[next] = true;
                        if (_depth == 1) subTree[len].b = next;
                        else if (_depth == 2) subTree[len].c = next;
                        else return false;

                        _depth++;
                        if (child[next] % 3 == 2) 
                            return DFS(next, _depth);

                        if (_depth == 3) return true;
                    }

                    return false;
                }
            }

            void SetChild()
            {

                child = new int[n + 1];
                parent = new int[n + 1];
                q = new Queue<int>(n / 3);

                DFS();

                int DFS(int _cur = 1, int _prev = 0)
                {

                    parent[_cur] = _prev;
                    ref int ret = ref child[_cur];
                    ret = 1;
                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (_prev == next) continue;
                        ret += DFS(next, _cur);
                    }

                    if (ret % 3 == 0) q.Enqueue(_cur);

                    return ret;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                edge = new List<int>[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 1; i < n; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    edge[f].Add(t);
                    edge[t].Add(f);
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
    }

#if other
// #include <stdio.h>
const int MAXN = 200005;
typedef long long lint;

int n;
int prv[696969], to[696969], las[696969], sz;

void addEdge(int u, int v){
	sz++; to[sz] = v; prv[sz] = las[u]; las[u] = sz;
}

int sub[696969], cnt;

void dfs(int x, int p){
	sub[x] = 1;
	for(int i = las[x]; i; i = prv[i]){
		if(to[i] != p){
			dfs(to[i], x);
			sub[x] += sub[to[i]];
		}
	}
	if(sub[x] % 3 == 0) cnt++;
}

int ans[155757][3], ptr[155757];

void add(int x, int v){
	ans[x][ptr[x]++] = v;
}

void trace(int x, int p, int grp){
	if(sub[x] % 3 == 0) grp = ++cnt;
	add(grp, x);
	for(int i = las[x]; i; i = prv[i]){
		if(to[i] != p){
			trace(to[i], x, grp);
		}
	}
}

int main(){
	int n;
	scanf("%d",&n);
	for(int i = 1; i < n; i++){
		int u, v; scanf("%d %d",&u,&v);
		addEdge(u, v);
		addEdge(v, u);
	}
	dfs(1, -1);
	if(cnt != n / 3){
		printf("U\n");
		return 0;
	}
	cnt = 0;
	trace(1, -1, 0);
	printf("S\n");
	for(int i = 1; i <= n / 3; i++){
		for(int j = 0; j < 3; j++){
			printf("%d ", ans[i][j]);
		}
		printf("\n");
	}
}
#elif other2
// #include <bits/stdc++.h>
// #define fi first
// #define se second
// #define eb emplace_back
// #define em emplace
// #define all(v) v.begin(), v.end()
// #define reset(x) memset(x, 0, sizeof(x))

using namespace std;
typedef long long ll;
typedef long double ld;
typedef complex <double> cpx;
typedef pair <int, int> pii;
typedef pair <ll, ll> pll;  

const int MAX = 353030;
const int INF = 1e9;
const ll LINF = 1e18;

struct TRI {
    int a, b, c;
    TRI(int x, int y, int z) {
        a = x, b = y, c = z;
    }
};

bool dp[MAX][3], flag; // 0: 분해 가능 / 1: 루트 위에 하나 필요 / 2: 루트만 남음
int cnt[MAX][3];
vector <int> g[MAX];
vector <TRI> ans;

inline void dfs(int x, int pa) {
    int k1 = 0, k2 = 0;
    for(int i : g[x]) {
        if(i == pa) continue;
        dfs(i, x);
        if(dp[i][0]) cnt[x][0]++;
        if(dp[i][1]) cnt[x][1]++;
        if(dp[i][2]) {
            cnt[x][2]++;
            if(k1 == 0) k1 = i;
            else k2 = i;
        }
    }
    if(cnt[x][1] == 0 && cnt[x][2] == 2) dp[x][0] = true, ans.eb(x, k1, k2);
    else if(cnt[x][1] == 1 && cnt[x][2] == 0) dp[x][0] = true;
    else if(cnt[x][1] == 0 && cnt[x][2] == 1) dp[x][1] = true, ans.eb(x, k1, pa);
    else if(cnt[x][1] == 0 && cnt[x][2] == 0) dp[x][2] = true;
    else flag = true;
}

int main() {
    ios::sync_with_stdio(false); cin.tie(nullptr);
    
    int n;
    cin >> n;
    for(int i = 1; i < n; i++) {
        int u, v;
        cin >> u >> v;
        g[u].eb(v), g[v].eb(u);
    }
    dfs(1, 0);
    
    if(flag) cout << "U";
    else {
        cout << "S\n";
        for(auto i : ans) {
            cout << i.a << ' ' << i.b << ' ' << i.c << '\n';
        }
    }
}
#endif
}
