using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 4
이름 : 배성훈
내용 : Dwarves
    문제번호 : 13147번

    위상정렬 문제다
    아이디어가 떠오르지 않아 힌트를 봤다
    그러니 대소 관계니 위상 정렬으로 나타내면 됨을 인지했고
    해당 방법으로 풀어 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_1024
    {

        static void Main1024(string[] args)
        {

            int MAX = 10_000;

            string NO = "impossible";
            string YES = "possible";
            string LEFT = ">";
            string RIGHT = "<";

            StreamReader sr;
            Dictionary<string, int> sTi;
            List<int>[] edge;
            int[] degree;

            int n, len;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Queue<int> q = new(len);
                int cnt = 0;

                for (int i = 0; i < len; i++)
                {

                    if (degree[i] != 0) continue;
                    q.Enqueue(i);
                    cnt++;
                }

                while(q.Count > 0)
                {

                    int node = q.Dequeue();
                        
                    for (int i = 0; i < edge[node].Count; i++)
                    {

                        int next = edge[node][i];
                        degree[next]--;

                        if (degree[next] == 0)
                        {

                            q.Enqueue(next);
                            cnt++;
                        }
                    }
                }

                Console.Write(cnt == len ? YES : NO);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                len = 0;

                sTi = new(MAX);
                edge = new List<int>[MAX];
                degree = new int[MAX];
                for (int i = 0; i < MAX; i++)
                {

                    edge[i] = new();
                }

                for (int i = 0; i < n; i++)
                {

                    string[] temp = sr.ReadLine().Split();

                    int idx1 = GetIdx(temp[0]);
                    int idx2 = GetIdx(temp[2]);

                    if (temp[1] == LEFT)
                    {

                        edge[idx1].Add(idx2);
                        degree[idx2]++;
                    }
                    else 
                    {

                        edge[idx2].Add(idx1); 
                        degree[idx1]++;
                    }
                }
            }

            int GetIdx(string _name)
            {

                if (!sTi.ContainsKey(_name)) sTi[_name] = len++;
                return sTi[_name];
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
// # include <iostream>
// # include <cstdlib>
// # include <vector>
// # include <cfloat>
// # include <algorithm>
// # include <cmath>
// # include <memory.h>
// # include <unordered_map>
// # include <queue>
// # include <cassert>
// # include <map>
// #define fast_io() cin.tie(NULL), cout.tie(NULL), ios_base::sync_with_stdio(false)
using namespace std;
typedef pair<int, int> pii;
typedef long long ll;
typedef pair<ll, ll> pll;
typedef tuple<int, int, int> tiii;
typedef tuple<ll, ll, ll> tlll;
typedef unsigned long long ull;

int n, cnt, in[101010];
unordered_map<string, int> id;
vector<int> ed[101010];

int main(){
    fast_io();
// # ifndef ONLINE_JUDGE
    freopen("input.txt", "r", stdin);
    freopen("output.txt", "w", stdout);
// #endif

    cin >> n;
    for(int i=0; i<n; i++){
        string u, v, op;
    cin >> u >> op >> v;

        if(!id.count(u)){
            id[u]=cnt++;
        }
if (!id.count(v))
{
    id[v] = cnt++;
}

if (op == "<") swap(u, v);

ed[id[u]].push_back(id[v]);
        in[id[v]]++;
    }

    queue<int> q;
cnt = 0;

for (int i = 0; i < id.size(); i++)
{
    if (in[i]) continue;
q.push(i);
cnt++;
    }

    while (q.size())
{
    int pos = q.front(); q.pop();
    for (auto & nxt:ed[pos])
    {
            in[nxt] --;
        if (in[nxt]== 0){
        cnt++;
        q.push(nxt);
    }
}
    }

    if (cnt == id.size())
{
    cout << "possible\n";
}
else
{
    cout << "impossible\n";
}

return 0;
}
#endif
}
