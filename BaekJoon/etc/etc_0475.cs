using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 7
이름 : 배성훈
내용 : BFFs (small, Large)
    문제번호 : 14379번, 14380번

    dp 문제다
    처음에는 그냥 조합으로 개수를 선택하고 되는지 판별할까 했으나,
    100개의 케이스이고 2^10 * 100 * (연산...) 시간초과날거 같이 보여 해당 방법은 접근을 포기했다 (1초인줄 알았으나.. 5초였다;)

    실버라 가벼운 마인드로 접근했는데, 버거운 문제였다
    dp와 유니온 파인드 + 비둘기 집 + 그래프 이론으로 풀었다

    아이디어는 다음과 같다
    먼저 유니온 파인드로 그룹을 나눈다
    여기서 그룹은 간선이 존재하면 같은 그룹으로 취급한다
    그러면 그룹에 노드는 n개 존재하고 간선도 n개 존재한다
    비둘기집 원리로 해당 그룹엔 적어도 하나의 사이클(그래프 이론!으로 봐도 된다)이 보장된다!

    이제 그룹의 사이클의 길이가 3이상이면 다른 그룹을 이어붙일 수 없다
    해당 그룹의 원소로 만들 수 있는 최대 길이는 해당 사이클이 유일하다!

    반면 길이가 2라면 다른 해당 그룹에 다른 원소들을 이어붙일 수 있고,
    각 노드에 대해, 사이클로 가는 길은 끊고 DFS 탐색으로 최대한 많이 이어붙일 수 있는 경우를 찾았다
    그리고 사이클의 길이가 2인 그룹끼리는 이어 붙일 수 있어 해당 최장 길이를 따로 누적해갔다

    이후에 사이클의 최장 길이와 사이클의 길이가 2인 것들을 누적한것과 비교해 정답을 제출하니 
    각각 68ms, 76ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0475
    {

        static void Main475(string[] args)
        {

            int LEN = 1_000;
            int[] bffs = new int[LEN + 1];
            int[] dp = new int[LEN + 1];
            int[] group = new int[LEN + 1];
            int[] depths = new int[LEN + 1];
            bool[] chkCycle = new bool[LEN + 1];
            List<int>[] rev = new List<int>[LEN + 1];
            Stack<int> s = new(LEN);
            for (int i = 1; i <= LEN; i++)
            {

                rev[i] = new(LEN);
            }
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            StringBuilder sb = new(1_000 * 3);
            int test = ReadInt();
            for (int t = 1; t <= test; t++)
            {

                int n = ReadInt();
                
                // 초기 세팅
                for (int i = 1; i <= n; i++)
                {

                    dp[i] = -1;
                    group[i] = i;
                    rev[i].Clear();
                    depths[i] = 0;
                    chkCycle[i] = false;
                }

                for (int i = 1; i <= n; i++)
                {

                    // 그룹 나누기
                    int cur = ReadInt();
                    bffs[i] = cur;
                    rev[cur].Add(i);
                    cur = Find(cur);
                    int chk = Find(i);

                    if (chk < cur)
                    {

                        int temp = cur;
                        cur = chk;
                        chk = temp;
                    }

                    group[chk] = cur;
                }

                int ret = 0;
                int conn = 0;
                for (int i = 1; i <= n; i++)
                {

                    // 그룹별로 사이클 길이 판별
                    int g = Find(i);
                    if (chkCycle[g]) continue;
                    chkCycle[g] = true;

                    int curDepth = 1;
                    int cur = i;
                    // 사이클 존재가 보장되기에 일일히 이동하며 사이클 길이를 찾았다
                    while (depths[cur] == 0)
                    {

                        depths[cur] = curDepth++;
                        cur = bffs[cur];
                    }

                    int cycle = curDepth - depths[cur];
                    // 사이클 길이가 3이상인 경우
                    if (cycle > 2) ret = ret < cycle ? cycle : ret;
                    else
                    {

                        // 사이클의 길이가 2인 경우
                        // 각 노선을 끊고 DFS 탐색
                        int next = bffs[cur];
                        dp[next] = 0;
                        conn += DFS(cur);
                        dp[next] = -1;
                        dp[cur] = 0;
                        conn += DFS(bffs[cur]);
                    }
                }

                ret = ret < conn ? conn : ret;
                sb.Append($"Case #{t}: {ret}\n");
            }

            sr.Close();

            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            sw.Write(sb);
            sw.Close();

            int DFS(int _cur)
            {

                if (dp[_cur] != -1) return dp[_cur];
                dp[_cur] = 0;

                int ret = 0;
                for (int i = 0; i < rev[_cur].Count; i++)
                {

                    int calc = DFS(rev[_cur][i]);
                    ret = ret < calc ? calc : ret;
                }

                dp[_cur] = ret + 1;
                return dp[_cur];
            }

            int Find(int _chk)
            {

                while (group[_chk] != _chk)
                {

                    s.Push(_chk);
                    _chk = group[_chk];
                }

                while(s.Count > 0)
                {

                    group[s.Pop()] = _chk;
                }

                return _chk;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c!= ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other_Silver
// #include<cstdio>
// #include<algorithm>
using namespace std;
int tc,n,a[15],ans;
int per[12] = {0,1,2,3,4,5,6,7,8,9};

int solve() {
    int i,j,ret=0;
    for(i=2;i<=n;i++) {
        bool flag = true;
        for(j=1;j<i-1;j++) {
            if(a[per[j]]!=per[j-1] && a[per[j]]!=per[j+1]) {
                flag = false;
                break;
            }
        }
        if(a[per[0]]!=per[1] && a[per[0]]!=per[i-1]) flag = false;
        if(a[per[i-1]]!=per[0] && a[per[i-1]]!=per[i-2]) flag = false;
        if(flag) ret = max(ret,i);
    }
    return ret;
}

int main()
{
    int i,j,q;
    scanf("%d",&tc);
    for(q=1;q<=tc;q++) {
        scanf("%d",&n);
        for(i=1;i<=n;i++) {
            scanf("%d",&a[i]);
        }
        for(i=0;i<n;i++) per[i] = i+1;
        ans = 0;
        do {
            ans = max(solve(),ans);
        }while(next_permutation(per,per+n));
        printf("Case #%d: %d\n",q,ans);
    }
}
#elif other
// #include <stdio.h>
// #include <vector>
// #include <algorithm>
// #define MAX 1010

std::vector<int> in[MAX];
int f[MAX], n, check[MAX], ans;
std::vector<int> two;

int max(int a, int b) {
	return (a > b) ? a : b;
}

int go(int now) {
	int ret = 0;
	for (int chd : in[now]) {
		if (chd != f[now]) {
			ret = max(go(chd), ret);
		}
	}
	return ret + 1;
}
 
void dfs(int now) {
	check[now] = 1;
	if (check[f[now]] == 1) {
		int len, i;
		for (len = 0, i = now; check[i] != 2; i = f[i], len++) {
			check[i] = 2;
		}
		if (len == 2) {
			int t = go(now) + go(f[now]);
			two.push_back(t);
		}
		else {
			ans = max(ans, len);
		}
	}else if (check[f[now]]==0) dfs(f[now]);
	if (check[now] == 1) check[now] = 3;
}

int main(void) {
	int test, i, now;
	scanf("%d", &test);
	for (int T = 1; T <= test; T++) {
		scanf("%d", &n);
		for (i = 1; i <= n; i++) {
			scanf("%d", &f[i]);
			in[f[i]].push_back(i);
		}
		ans = 0;
		for (i = 1; i <= n; i++) {
			if (in[i].size() == 0) dfs(i);
		}
		for (i = 1; i <= n; i++) {
			if (check[i] == 0) {
				for (now = i; check[f[now]] != 1; now = f[now]) {
					check[now] = 1;
				}
				dfs(now);
			}
		}
		if (!two.empty()) {
			int s = 0;
			for (int e : two) s += e;
			ans = max(ans, s);
			two.clear();
		}
		printf("Case #%d: %d\n", T, ans);
		for (i = 1; i <= n; i++) {
			in[i].clear();
			check[i] = 0;
		}
	}
	return false;
}
#elif other2
// #include <stdio.h>
// #include <stdlib.h>
// #include <string.h>
// #include <assert.h>
// #include <math.h>
// #include <limits.h>
// #include <stack>
// #include <queue>
// #include <map>
// #include <set>
// #include <algorithm>
// #include <string>
// #include <functional>
// #include <vector>
// #include <numeric>
// #include <deque>
// #include <bitset>
// #include <iostream>
using namespace std;
typedef long long lint;
typedef long double llf;
typedef pair<int, int> pi;

int t;
int n, a[1005];
int indeg[1005];
vector<int> gph[1005];

bool vis[1005];

int dfs(int x){
	if(vis[x]) return 0;
	vis[x] = 1;
	return 1 + dfs(a[x]);
}

int dfs2(int x){
	int ret= 1;
	for(auto &j : gph[x]){
		if(indeg[j] == 0){
			ret = max(ret, dfs2(j) + 1);
		}
	}
	return ret;
}

int main(){
	cin >> t;
	for(int i=1; i<=t; i++){
		printf("Case #%d: ", i);
		memset(indeg, 0, sizeof(indeg));
		for(int i=0; i<=1000; i++) gph[i].clear();
		cin >> n;
		for(int i=1; i<=n; i++){
			cin >> a[i];
			indeg[a[i]]++;
			gph[a[i]].push_back(i);
		}
		queue<int> que;
		for(int i=1; i<=n; i++){
			if(indeg[i] == 0) que.push(i);
		}
		while(!que.empty()){
			int x = que.front();
			que.pop();
			indeg[a[x]]--;
			if(indeg[a[x]] == 0){
				que.push(a[x]);
			}
		}
		// calculate biggest cycle
		int ret = 0, ret2 = 0;
		for(int i=1; i<=n; i++){
			if(indeg[i]){
			memset(vis, 0, sizeof(vis));
				int t = dfs(i);
				ret = max(ret, t);
				if(t == 2){
					int u = i, v = a[i];
					ret2 += dfs2(u) + dfs2(v);
				}
			}
		}
		ret = max(ret, ret2/2);
		printf("%d\n",ret);
	}
}
#endif
}
