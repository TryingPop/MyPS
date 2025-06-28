using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 5
이름 : 배성훈
내용 : 거의 최단 경로
    문제번호 : 5719번

    다익스트라 문제다
    초기화를 안해서 1번, 메모리 초과로 2번 틀렸다
    간선 제거하는 q의 최적화 문젠가 싶어 수정해도 터졌다
    이후 거리가 같은 것들을 끊으니 이상없이 통과한다
    
    아이디어는 다음과 같다
    다익스트라를 하면서 최단 경로들을 모두 찾고 기록한다
    그리고 해당 경로에 있는 간선들을 모두 제거하고
    다시 다익스트라로 경로를 탐색해 결론을 도출하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_1031
    {

        static void Main1031(string[] args)
        {

            int N = 500;
            int M = 10_000;
            int INF = 100_000_000;

            StreamReader sr;
            StreamWriter sw;

            PriorityQueue<(int dst, int dis, int idx), int> pq;
            Queue<int> q;
            List<int>[] edge;
            (int src, int dst, int dis, bool active)[] info;
            int n, m, s, e;
            List<int>[] prev;
            int[] dis;

            Solve();
            void Solve()
            {

                Init();

                while (Input())
                {

                    Dijkstra();
                    RemoveEdge();
                    Dijkstra();

                    int ret = dis[e];
                    if (ret == INF) sw.Write("-1\n");
                    else sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            void RemoveEdge()
            {

                q.Enqueue(e);
                while (q.Count > 0)
                {

                    var node = q.Dequeue();

                    for (int i = 0; i < prev[node].Count; i++)
                    {

                        int idx = prev[node][i];
                        if (idx == -1) continue;
                        int next = info[idx].src;
                        if (prev[next].Count > 0) q.Enqueue(next);
                        info[idx].active = false;
                    }

                    prev[node].Clear();
                }
            }

            void Dijkstra()
            {

                Array.Fill(dis, INF);
                pq.Enqueue((s, 0, -1), 0);
                dis[s] = 0;

                while (pq.Count > 0)
                {

                    var node = pq.Dequeue();
                    if (dis[node.dst] < node.dis) continue;

                    for (int i = 0; i < edge[node.dst].Count; i++)
                    {

                        int idx = edge[node.dst][i];
                        int next = info[idx].dst;
                        int nDis = info[idx].dis + node.dis;

                        if (!info[idx].active || dis[next] < nDis) continue;
                        else if (dis[next] == nDis)
                            prev[next].Add(idx);
                        else
                        {

                            prev[next].Clear();
                            prev[next].Add(idx);
                            dis[next] = nDis;
                            pq.Enqueue((next, nDis, idx), nDis);
                        }
                    }
                }
            }

            bool Input()
            {

                n = ReadInt();
                m = ReadInt();

                if (n == 0 && m == 0) return false;

                s = ReadInt();
                e = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    edge[i].Clear();
                    prev[i].Clear();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    int dis = ReadInt();

                    info[i] = (f, t, dis, true);
                    edge[f].Add(i);
                }

                return true;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                dis = new int[N];
                q = new(N);
                pq = new(M);

                edge = new List<int>[N];
                prev = new List<int>[N];


                s = 0;
                e = 0;

                for (int i = 0; i < N; i++)
                {

                    edge[i] = new();
                    prev[i] = new();
                }

                info = new (int src, int dst, int dis, bool active)[M];
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
using System.Text;

StringBuilder sb = new StringBuilder();
const int INF = 1234567890;
while (true)
{
    var N = Array.ConvertAll(Console.ReadLine()!.Split(), int.Parse);
    int nodeCnt = N[0];
    int linkCnt = N[1];
    if (nodeCnt == 0) break;

    var M = Array.ConvertAll(Console.ReadLine()!.Split(), int.Parse);
    int start = M[0];
    int end = M[1];

    List<(int, int)>[] nodes = new List<(int, int)>[nodeCnt];
    List<int>[] front = new List<int>[nodeCnt];
    int[][] dis = new int[nodeCnt][];
    bool[] isVisited = new bool[nodeCnt];
    bool[][] isBanedLink = new bool[nodeCnt][]; // a -> b로 가는 경로가 금지되었나?

    for (int i = 0; i < nodeCnt; i++)
    {
        nodes[i] = new List<(int, int)>();
        front[i] = new List<int>();
        dis[i] = new int[nodeCnt];
        isBanedLink[i] = new bool[nodeCnt];
    }

    SetDisArr();

    for (int i = 0; i < linkCnt; i++)
    {
        var link = Array.ConvertAll(Console.ReadLine()!.Split(), int.Parse);
        nodes[link[0]].Add((link[1], link[2]));
    }

    PriorityQueue<int, int> que = new PriorityQueue<int, int>();
    que.Enqueue(start, 0);
    DIJ();
    Array.Clear(isVisited, 0, nodeCnt);
    BanLink(end);
    SetDisArr();
    Array.Clear(isVisited, 0, nodeCnt);
    que.Enqueue(start, 0);
    DIJ();

    sb.AppendLine(dis[start][end] != INF ? dis[start][end].ToString() : "-1");

    void BanLink(int cur)
    {
        isVisited[cur] = true;
        foreach (int f in front[cur])
        {
            isBanedLink[f][cur] = true;
            if (!isVisited[f])
                BanLink(f);
        }
    }

    void DIJ()
    {
        while (que.Count > 0)
        {
            int cur = que.Dequeue();
            if (isVisited[cur]) continue; 
            isVisited[cur] = true;

            foreach (var (next, wei) in nodes[cur])
            {
                if (isBanedLink[cur][next]) continue;

                int oldDis = dis[start][next];
                int newDis = dis[start][cur] + wei;

                if (oldDis >= newDis)
                {
                    if (oldDis > newDis)
                    {
                        dis[start][next] = newDis;
                        front[next].Clear();
                        if (!isVisited[next])
                            que.Enqueue(next, newDis);
                    } 

                    front[next].Add(cur);
                }
            }
        }
    }

    void SetDisArr()
    {
        for (int i = 0; i < nodeCnt; i++)
        {
            for (int j = 0; j < nodeCnt; j++)
                dis[i][j] = INF;

            dis[i][i] = 0;
        }
    }
}

Console.WriteLine(sb);
#elif other2
// #include <cstdio>
// #include <algorithm>

namespace fio
{
    constexpr size_t BUF_SIZ = 0x8'0000, SPARE = 20;
  char buf_i[BUF_SIZ], * p_in = buf_i, * p_max = p_in;

    inline char getCh(void)
    {
        if (p_in == p_max)
        {
            p_max = buf_i + fread(buf_i, 1, BUF_SIZ, stdin);
            p_in = buf_i;
        }
        return (p_in != p_max) ? *(p_in++) : -1;
    }

    template<typename T>
    inline T get(void)
    {
        int t, sign = 1;
        T r = 0;
        do
        {
            t = getCh();
        } while (t != '-' && (t < '0' || t > '9'));
        if (t == '-') sign = -1, t = getCh();
        while (true)
        {
            r = r * 10 + t - '0'; t = getCh();
            if (t < '0' || t > '9') break;
        }
        return r * sign;
    }

    char buf_o[BUF_SIZ + SPARE], * p_out = buf_o;

    template<typename T>
  void prt(T n, char delim = ' ')
    {
        if (n < 0) n = -n, *p_out++ = '-';
        char* ptr = p_out;
        do { *ptr++ = n % 10 + '0'; n /= 10; } while (n);
        std::reverse(p_out, ptr);
        *ptr++ = delim;
        if (ptr < buf_o + BUF_SIZ)
        {
            p_out = ptr;
        }
        else
        {
            fwrite(buf_o, ptr - buf_o, 1, stdout); p_out = buf_o;
        }
    }

    inline void prtn(void)
    {
        *p_out++ = '\n';
        if (p_out >= buf_o + BUF_SIZ)
        { fwrite(buf_o, p_out - buf_o, 1, stdout); p_out = buf_o; }
    }

    void flush(void)
    {
        fwrite(buf_o, p_out - buf_o, 1, stdout);
        p_out = buf_o;
    }
};  // namespace fio
///////////////////////////

///////////////////////////
// #include <vector>
// #include <utility>
// #include <queue>

using std::vector, std::pair, std::priority_queue;

constexpr int MAXN = 500;
constexpr int INF = 987'654'321;

typedef pair<int, int> p_ii;

int N, M, S, D;
vector<p_ii> edges[MAXN];
bool visited[MAXN];

int dists[MAXN];
vector<int> parent[MAXN];

bool readData(void)
{
    N = fio::get<int>();
    M = fio::get<int>();
    if (N == 0 && M == 0) return false;

    for (int n = 0; n < N; n++) { edges[n].clear(); }

    S = fio::get<int>();
    D = fio::get<int>();

    int u, v, d;
    for (int m = 0; m < M; m++)
    {
        u = fio::get<int>();
        v = fio::get<int>();
        d = fio::get<int>();
        edges[u].emplace_back(v, d);
    }

    return true;
}

void findShortest(void)
{
    for (int n = 0; n < N; n++)
    {
        dists[n] = INF;
        visited[n] = false;
        parent[n].clear();
    }

    priority_queue<p_ii> que;

    dists[S] = 0;
    que.emplace(-0, S);

    while (!que.empty())
    {
        auto[dist, node] = que.top();
        que.pop();
        dist = -dist;

        if (visited[node]) continue;
        visited[node] = true;

        for (auto[next, d] : edges[node]) {
    if (dists[next] > dists[node] + d)
    {
        dists[next] = dists[node] + d;
        que.emplace(-dists[next], next);

        parent[next].clear();
        parent[next].push_back(node);
    }
    else if (dists[next] == dists[node] + d)
    {
        parent[next].push_back(node);
    }
}
  }
}

bool modDist2Inf(vector<p_ii>& edge, int n)
{
    for (auto & [v, d] : edge) {
    if (v == n)
    {
        if (d == INF) return false;
        d = INF;
        break;
    }
}
return true;
}

void deleteShortestPath(void)
{
    vector<int> stk;

    stk.push_back(D);

    while (!stk.empty())
    {
        int n = stk.back();
        stk.pop_back();

        for (int p : parent[n]) {
    if (modDist2Inf(edges[p], n)) stk.push_back(p);
}
  }
}

int main(void)
{
    ioInit();

    while (readData())
    {
        findShortest();
        deleteShortestPath();
        findShortest();

        if (dists[D] == INF) dists[D] = -1;
        printf("%d\n", dists[D]);
    }

    return 0;
}

#endif
}
