using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 4
이름 : 배성훈
내용 : 스크루지 민호 2
    문제번호 : 12978번

    트리, dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1516
    {

        static void Main1516(string[] args)
        {

            int n;
            List<int>[] edge;

            Input();

            GetRet();

            void GetRet()
            {

                int[][] dp = new int[2][];
                for (int i = 0; i < dp.Length; i++)
                {

                    dp[i] = new int[n + 1];
                    Array.Fill(dp[i], -1);
                }

                DFS(1);

                Console.Write(Math.Min(dp[1][1], dp[0][1]));
                void DFS(int _cur)
                {

                    if (dp[0][_cur] != -1) return;
                    dp[0][_cur] = 0;
                    dp[1][_cur] = 1;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (dp[0][next] != -1) continue;
                        DFS(next);

                        dp[0][_cur] += dp[1][next];
                        dp[1][_cur] += Math.Min(dp[1][next], dp[0][next]);
                    }
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

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = c - '0';

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
// #define FASTIO ios_base::sync_with_stdio(false);cin.tie(0);cout.tie(0);

using namespace std;

vector<int> edges[100'001];

bool is_leaf(int node, int parent, int& answer){
    bool has_leaf_child = false;
    for(int child : edges[node]){
        if(child == parent) continue;

        has_leaf_child |= is_leaf(child, node, answer);
    }

    if(has_leaf_child) answer++;

    return !has_leaf_child;
}

int main(){
    FASTIO

    int n;
    cin >> n;

    for(int i = 0; i < n - 1; i++){
        int u, v;
        cin >> u >> v;

        edges[u].push_back(v);
        edges[v].push_back(u);
    }

    int answer = 0;
    is_leaf(1, -1, answer);

    cout << answer;

    return 0;
}
#endif
}
