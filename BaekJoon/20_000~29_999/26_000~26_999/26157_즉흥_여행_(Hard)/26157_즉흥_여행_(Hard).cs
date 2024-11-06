using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 30
이름 : 배성훈
내용 : 즉흥 여행 (Hard)
    문제번호 : 26157번

    강한 연결 요소 위상 정렬 문제다
    타잔 알고리즘을 쓰면 위상정렬의 역순으로 그룹 번호가 붙어진다
    타잔으로 SCC를 찾았다

    문제 조건이 세계 일주이므로 SCC 그룹들은 
    일직선인 경우만 세계 일주가 가능하다

    각 그룹이 일직선 형태로 이뤄져 있는지 확인하는데
    위상정렬 역순이 되었으므로 모든 간선을 조사해
    바로 이전 그룹으로 갈 수 있는지 확인한다

    이렇게 제출하니 이상없이 통과했다

    다른 사람의 풀이를 보니 SCC 구하면서 바로 정답을 도출하는데
    아직 타잔 알고리즘에 이해도가 낮아 나눠서 구했다

    해당 사이트를 참고하면 좋을거 같다
    https://m.blog.naver.com/PostView.nhn?blogId=kks227&logNo=220802519976&referrerCode=0&searchKeyword=scc
*/

namespace BaekJoon.etc
{
    internal class etc_1009
    {

        static void Main1009(string[] args)
        {

            StreamReader sr;
            int n, m;
            List<int>[] edge;

            int[] id, group, s;
            int curId;
            int groups;
            int sLen;

            Solve();
            void Solve()
            {

                Input();

                SCC();

                GetRet();

                Output();
            }

            void Output()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                sw.Write($"{sLen}\n");
                for (int i = 0; i < sLen; i++)
                {

                    sw.Write($"{s[i]} ");
                }
                sw.Close();
            }

            void GetRet()
            {

                Array.Fill(id, 0);
                for (int i = 1; i <= n; i++)
                {

                    int f = group[i];
                    for (int j = 0; j < edge[i].Count; j++)
                    {

                        int next = edge[i][j];
                        int b = group[next];
                        if (f == b) continue;
                        id[f] = Math.Max(b, id[f]);
                    }
                }

                bool flag = true;
                for (int i = 1; i <= groups; i++)
                {

                    if (id[i] == i - 1) continue;
                    flag = false;
                    break;
                }

                if (flag)
                {

                    sLen = 0;
                    for (int i = 1; i <= n; i++)
                    {

                        if (group[i] == groups)
                            s[sLen++] = i;
                    }
                }
                else sLen = 0;
            }

            void SCC()
            {

                group = new int[n + 1];
                id = new int[n + 1];
                s = new int[n + 1];
                curId = 1;
                groups = 0;
                sLen = 0;

                for (int i = 1; i <= n; i++)
                {

                    DFS(i);
                }

                int DFS(int _cur)
                {

                    if (id[_cur] != 0) return -1;
                    id[_cur] = curId++;

                    s[sLen++] = _cur;
                    int ret = _cur;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];

                        if (id[next] != 0)
                        {

                            if (group[next] == 0 && id[next] < id[ret]) ret = next;
                            continue;
                        }

                        int chk = DFS(next);

                        if (id[chk] < id[ret]) ret = chk;
                    }

                    if (ret == _cur)
                    {

                        groups++;

                        while (sLen > 0)
                        {

                            int next = s[--sLen];
                            group[next] = groups;
                            if (next == _cur) break;
                        }
                    }

                    return ret;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                edge = new List<int>[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    edge[f].Add(b);
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
        }
    }

#if other
// #include <iostream>
// #include <vector>
// #include <stack>
// #include <set>

using namespace std;

int n;
vector<int> graph[200001];
int vid;
int visited[200001];

stack<int> hist;
int scc[200001];

int sid;
int finished[200001];

int sink;

int dfs(int current){
    int size = graph[current].size();
    int m = visited[current] = ++vid;
    hist.push(current);

    for (int i = 0; i < size; ++i) {
        int next = graph[current][i];
        if(!visited[next])
            m = min(m, dfs(next));
        else if(!finished[next])
            m = min(m, visited[next]);
    }

    if(m == visited[current]){
        int prev = -1;
        ++sid;
        while (prev != current){
            prev = hist.top();
            hist.pop();
            finished[prev] = sid;
            for(int next: graph[prev]){
                if(finished[next] && finished[next] != sid)
                    scc[sid] = max(scc[sid], finished[next]);
            }
        }
    }
    return m;
}

int main(){
    ios::sync_with_stdio(false);
    cin.tie(0);
    int m;
    cin >> n >> m;
    while (m--){
        int u, v;
        cin >> u >> v;
        graph[u].push_back(v);
    }
    for (int i = 1; i <= n; ++i) {
        if(!finished[i])
            dfs(i);
    }

    int current = sid;
    while (current){
        if(scc[current] != current - 1){
            cout << 0;
            return 0;
        }
        current = scc[current];
    }

    vector<int> ans;
    for (int i = 1; i <= n; ++i) {
        if(finished[i] == sid)
            ans.push_back(i);
    }

    cout << ans.size() << '\n';
    for(int v: ans){
        cout << v << ' ';
    }
}
#elif other2
// #include<bits/stdc++.h>
using namespace std;

// #define fast ios::sync_with_stdio(0); cin.tie(0);
// #define pre(a) cout<<fixed; cout.precision(a);
// #define fi first
// #define se second
// #define em emplace
// #define eb emplace_back
// #define mp make_pair
// #define all(v) (v).begin(), (v).end()

typedef long long ll;
typedef pair<int,int> pii;
typedef pair<ll,ll> pll;
const ll INF = 1e18;
const int inf = 1e9;

int n, m, cnt, Cnt;
vector<int> g[200010];
int ord[200010];
int scc[200010];
vector<int> st;

int in[200010];

int dfs(int x) {
    int ret = ord[x] = ++cnt;
    st.eb(x);

    for(auto i : g[x]) {
        if(scc[i]) continue;
        
        if(ord[i]) {
            ret = min(ret, ord[i]);
        }
        else {
            ret = min(ret, dfs(i));
        }
    }


    if(ret == ord[x]) {
        Cnt++;
        while(st.size()) {
            int tmp = st.back();
            st.pop_back();
            scc[tmp] = Cnt;
            if(tmp == x) break;
        }
    }

    return ret;
}

int main() {
    fast;

    cin >> n >> m;

    for(int i=1; i<=m; i++) {
        int u, v;
        cin >> u >> v;
        g[u].eb(v);
    }

    for(int i=1; i<=n; i++) {
        if(!scc[i]) dfs(i); 
    }

    for(int i=1; i<=n; i++) {
        for(auto j : g[i]) {
            if(scc[j] == scc[i]-1) in[scc[j]]++;
        }
    }

    for(int i=1; i<Cnt; i++) {
        if(!in[i]) {
            cout << 0;
            return 0;
        }
    }

    vector<int> ans;
    for(int i=1; i<=n; i++) if(scc[i] == Cnt) ans.eb(i);

    cout << ans.size() << "\n";
    for(auto i : ans) cout << i << " ";
}
#endif
}
