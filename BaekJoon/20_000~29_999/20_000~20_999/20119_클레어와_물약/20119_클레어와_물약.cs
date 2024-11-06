using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 4
이름 : 배성훈
내용 : 클레어와 물약
    문제번호 : 20119번

    위상 정렬 문제다
    처음에 물약에 대해 위상정렬하면 되지 않을까 고민했고
    해당 물약에 대한 다른 레시피가 존재하는 경우
    쓸 수 없는 방법이었다
    결국 한참을 고민했고, 다음과 같은 방법으로 풀었다
    레시피에 대해 위상정렬해 풀었다

    가리키는 노드들이 모두 있으면 위상을 줄이고
    0이되면 탐색에 넣었다
*/

namespace BaekJoon.etc
{
    internal class etc_1023
    {

        static void Main1023(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            List<int>[] edge;       // 레시피 조합 간선
            int n, m;               // 
            int[] arr, degree;      // 레시피
            bool[] ret;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Queue<int> q = new(n);

                int len = ReadInt();
                int cnt = 0;
                for (int i = 0; i < len; i++)
                {

                    int y = ReadInt();
                    q.Enqueue(y);
                    ret[y] = true;
                    cnt++;
                }

                while (q.Count > 0)
                {

                    int node = q.Dequeue();
                    for (int i = 0; i < edge[node].Count; i++)
                    {

                        int next = edge[node][i];
                        degree[next]--;

                        if (degree[next] == 0 && !ret[arr[next]])
                        {

                            q.Enqueue(arr[next]);
                            cnt++;
                            ret[arr[next]] = true;
                        }
                    }
                }

                sw.Write($"{cnt}\n");
                for (int i = 1; i <= n; i++)
                {

                    if (ret[i]) sw.Write($"{i} ");
                }

                sw.Close();
                sr.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                arr = new int[m];
                degree = new int[m];

                edge = new List<int>[n + 1];
                ret = new bool[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int k = ReadInt();

                    for (int j = 0; j < k; j++)
                    {

                        int x = ReadInt();
                        edge[x].Add(i);
                    }

                    int r = ReadInt();
                    arr[i] = r;
                    degree[i] = k;
                }
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
        }
    }

#if other
// #include<iostream>
// #include<algorithm>
// #include<vector>
// #include<map>
// #include<cmath>
// #include<cstring>
// #include<set>
// #include<queue>
// #include<deque>
// #include<stack>
// #include<string>
// include<new>
// #include<stdio.h>
// #define ll long long int
// #define pii pair<int,int>
// #define inf 1000000000
// #define tii tuple<int,int,int>
using namespace std;
int n, m, in[200010], out[200010], total;
vector<int> v[200010];
queue<int> q;
bool ans[200010];
int main() {
	ios_base::sync_with_stdio(false);
	cin.tie(NULL); cout.tie(NULL);

	cin >> n >> m;
	for (int i = 1; i <= m; i++) {
		int a, b;
		cin >> a;
		in[i] = a;
		for (int j = 1; j <= a; j++) {
			int c;
			cin >> c;
			v[c].push_back(i);
		}
		cin >> b;
		out[i] = b;
	}
	int k;
	cin >> k;
	for (int i = 1; i <= k; i++) {
		int a;
		cin >> a; 
		q.push(a);
		ans[a] = true;
		total++;
	}
	while (!q.empty()) {
		int x = q.front();
		q.pop();
		for (int j = 0; j < v[x].size(); j++) {
			int y = v[x][j];
			in[y]--;
			if (in[y] == 0 && ans[out[y]] != true) {
				ans[out[y]] = true;
				total++;
				q.push(out[y]);
			}
		}
	}
	cout << total << "\n";;
	for (int i = 1; i <= n; i++) {
		if (ans[i] == true) {
			cout << i << " ";;
		}
	}

	return 0;
}
#elif other2
// #include<bits/stdc++.h>
using namespace std;
using ll = long long;
using pll = pair<ll, ll>;

int n, m;
bool able[202020];
vector<int> nxt_recipe[202020];
int recipe[202020], deg[202020];
queue<int> q;

int main(){
	cin.tie(0); cout.tie(0); ios::sync_with_stdio(false);
	cin >> n >> m;
	for(int i = 1; i <= m; i++){
		int k; cin >> k; deg[i] = k;
		while(k--){
			int x; cin >> x;
			nxt_recipe[x].push_back(i);
		}
		cin >> recipe[i];
	}
	int L; cin >> L; while(L--){
		int k; cin >> k; able[k] = 1;
		for(auto i : nxt_recipe[k]){
			deg[i]--;
			if(!deg[i]) q.push(i);
		}
	}
	while(q.size()){
		int now = q.front(); q.pop();
		if(able[recipe[now]]) continue;
		able[recipe[now]] = 1;
		for(auto i : nxt_recipe[recipe[now]]){
			deg[i]--;
			if(!deg[i]) q.push(i);
		}
	}
	int ans = 0;
	for(int i = 1; i <= n; i++) if(able[i]) ans++;
	cout << ans << '\n';
	for(int i = 1; i <= n; i++) if(able[i]) cout << i << ' ';
}
#endif
}
