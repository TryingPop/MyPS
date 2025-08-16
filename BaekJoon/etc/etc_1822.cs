using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 14
이름 : 배성훈
내용 : 아침은 고구마야 (Easy)
    문제번호 : 20425번

    그래프 이론 문제다.
    스택을 이용해 사이클을 찾았다.
    다른 사람의 풀이를 보니, 스택없이 그냥 깊이만으로도 사이클을 찾을 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1822
    {

        static void Main1822(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = ReadInt();
            int m = ReadInt();

            List<int>[] edge = new List<int>[n + 1];
            for (int i = 1; i <= n; i++)
            {

                edge[i] = new();
            }

            for (int i = 0; i < m; i++)
            {

                int f = ReadInt();
                int t = ReadInt();
                ReadInt();

                edge[f].Add(t);
                edge[t].Add(f);
            }

            long ret = 0;

            int[] stk = new int[n];
            int len = 0;
            bool[] visit = new bool[n + 1];
            bool[] contains = new bool[n + 1];

            DFS(1, 0);

            Console.Write(ret);
            void DFS(int _cur, int _prev)
            {

                for (int i = 0; i < edge[_cur].Count; i++)
                {

                    int next = edge[_cur][i];
                    if (next == _prev) continue;

                    if (visit[next])
                    {

                        if (!contains[next]) continue;
                        int chkLen = len;

                        long cnt = 1;
                        while (stk[--chkLen] != next)
                        {

                            cnt++;
                        }

                        // 해당 지역에 사이클 발견
                        ret += cnt * cnt;
                    }
                    else
                    {

                        visit[next] = true;
                        contains[next] = true;
                        stk[len++] = next;
                        DFS(next, _cur);

                        contains[next] = false;
                        len--;
                    }
                }
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

#if other
// #define _CRT_SECURE_NO_WARNINGS

// #include <iostream>
// #include <vector>

using namespace std;
typedef long long ll;

int n, m, depth[1001010];
vector<int> graph[1001010];
ll ans;

void dfs(int cur, int prv) {
    for (int nxt : graph[cur]) {
        if (prv != nxt) {
            if (depth[nxt] == 0) {
                depth[nxt] = depth[cur] + 1;
                dfs(nxt, cur);
            }
            else if (depth[cur] > depth[nxt]) {
                int sz = depth[cur] - depth[nxt] + 1;
                ans += 1LL * sz * sz;
            }
        }
    }
}

void input() {
    cin >> n >> m;
    for (int i = 0; i < m; i++) {
        int a, b, c;
        cin >> a >> b >> c;
        graph[a].push_back(b);
        graph[b].push_back(a);
    }
}

void pro() {
    dfs(1, 0);
    cout << ans << endl;
}

int main() {
    
    cin.tie(NULL);
    ios_base::sync_with_stdio(false);

    //freopen("input.txt", "r", stdin);
    
    input();
    pro();
    return 0;
}
#endif
}
