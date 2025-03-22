using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 17
이름 : 배성훈
내용 : 정점들의 거리
    문제번호 : 1761번

    최소 공통 조상 문제다.
    정점들의 최소 거리는 최소 공통조상으로 이동하는 것이다.
    그래서 희소 배열을 이용해 log N에 공통 조상으로 이동하게 했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1196
    {


        static void Main1196(string[] args)
        {

            StreamReader sr;
            int n, m;
            List<(int dst, int dis)>[] edge;
            int[][] parent, dis;
            int[] depth;
            Solve();
            void Solve()
            {

                Input();

                SetArr();

                GetRet();
            }

            void SetArr()
            {

                m = 2 + (int)Math.Log2(n);
                depth = new int[n];
                parent = new int[m][];
                dis = new int[m][];

                for (int i = 0; i < m; i++)
                {

                    parent[i] = new int[n];
                    Array.Fill(parent[i], -1);
                    dis[i] = new int[n];
                }

                DFS(0, -1);

                for (int i = 1; i < m; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        int next = parent[i - 1][j];
                        if (next == -1 || parent[i - 1][j] == -1) continue;
                        parent[i][j] = parent[i - 1][next];
                        dis[i][j] = dis[i - 1][j] + dis[i - 1][next];
                    }
                }

                void DFS(int _cur, int _parent)
                {

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i].dst;
                        if (next == _parent) continue;

                        depth[next] = depth[_cur] + 1;
                        parent[0][next] = _cur;
                        dis[0][next] = edge[_cur][i].dis;
                        DFS(next, _cur);
                    }
                }
            }

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                int q = ReadInt();

                while(q-- > 0)
                {

                    sw.Write($"{Query(ReadInt() - 1, ReadInt() - 1)}\n");
                }

                int Query(int _f, int _t)
                {

                    if (depth[_t] < depth[_f])
                    {

                        int temp = _f;
                        _f = _t;
                        _t = temp;
                    }

                    int ret = 0;
                    int sub = depth[_t] - depth[_f];

                    // 깊이 맞추기
                    for (int i = m - 1; i >= 0; i--)
                    {

                        int chk = 1 << i;
                        if (chk <= sub)
                        {

                            sub -= chk;
                            ret += dis[i][_t];
                            _t = parent[i][_t];
                        }

                        if (sub == 0) break;
                    }

                    if (_f == _t) return ret;

                    // 공통 조상 찾기
                    for (int i = m - 1; i >= 0; i--)
                    {

                        // 부모 존재 X
                        if (parent[i][_f] == -1
                            || parent[i][_f] == parent[i][_t]) continue;

                        ret += dis[i][_f] + dis[i][_t];
                        _f = parent[i][_f];
                        _t = parent[i][_t];
                    }

                    ret += dis[0][_f] + dis[0][_t];

                    return ret;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                edge = new List<(int dst, int dis)>[n];
                for (int i = 0; i < n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 1; i < n; i++)
                {

                    int f = ReadInt() - 1;
                    int t = ReadInt() - 1;
                    int dis = ReadInt();

                    edge[f].Add((t, dis));
                    edge[t].Add((f, dis));
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != '\n' && c != ' ')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// #include <queue>
// #include <vector>
// #include <cstdio>
// #define INF 1000000000

using namespace std;

typedef struct Point{
    int v, w;
    Point(){}
    Point(int i, int j){
        v = i;
        w = j;
    }
}Point;

char buf[1 << 17];

inline char read() {
    static int idx = 1 << 17;
    if (idx == 1 << 17) {
        fread(buf, 1, 1 << 17, stdin);
        idx = 0;
    }
    return buf[idx++];
}
inline int readInt() {
    int sum = 0;
    bool flg = 1;
    char now = read();

    while (now == 10 || now == 32) now = read();
    if (now == '-') flg = 0, now = read();
    while (now >= 48 && now <= 57) {
        sum = sum * 10 + now - 48;
        now = read();
    }

    return flg ? sum : -sum;
}

vector< vector< Point > > ll;
int parent[17][40020], level[40020], d[40020];
int n, m, v, w, u;



int LCA(int chd, int par){
    int diff = level[chd] - level[par];
    for(int i = 16; i >= 0; i--){
        if((1<<i) <= diff){
            chd = parent[i][chd];
            diff -= (1<<i);
        }
    }
    if(chd == par) return chd;
    for(int i = 15; i >= 0; i--){
        if(parent[i][chd] != parent[i][par]){
            chd = parent[i][chd];
            par = parent[i][par];
        } 
    } 
    return parent[0][chd];
}

void build_tree(){
    queue<int> q;
    level[1] = 1, d[1] = 0, parent[0][1] = 1;
    q.push(1);
    while(!q.empty()){
        u = q.front(); q.pop();
        for(int i = 0; i < ll[u].size(); i++){
            v = ll[u][i].v;
            if(level[v] == 0){
                level[v] = level[u] + 1;
                parent[0][v] = u;
                d[v] = d[u] + ll[u][i].w;
                q.push(v);
            }
        }
    }
}
void find_parent(){
    for(int i = 1; i < 17; i++){
        for(int j = 1; j < n + 1; j++)parent[i][j] = parent[i-1][parent[i-1][j]];
    }
}

int main(){
    n = readInt();
    // scanf("%d", &n);
    
    ll = vector< vector< Point > >(n + 1);
    for(int i = 1; i < n; i++){
        v = readInt(), u = readInt(), w = readInt();
        // scanf("%d %d %d", &v, &u, &w);
        ll[v].push_back(Point(u, w));
        ll[u].push_back(Point(v, w));
    }
    build_tree();
    find_parent();

    m = readInt();
    // scanf("%d", &m);
    while(m--){
        v = readInt(), u = readInt();
        // scanf("%d %d", &v, &u);
        printf("%d\n", d[v] + d[u] - d[level[v] > level[u] ? LCA(v, u) : LCA(u, v)]*2);       
    }
}
#endif
}
