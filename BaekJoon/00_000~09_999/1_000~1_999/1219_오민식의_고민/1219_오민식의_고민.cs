using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 18
이름 : 배성훈
내용 : 오민식의 고민
    문제번호 : 1219번

    벨만 포드 문제다.
    범위를 잘못 산정해 엄청나게 틀렸다;
    한번 사이클을 돌 때 최대 획득 금액은 5000만 골드다.
    그리고 노드의 2배를 돌리므로 50억까지 누적될 수 있다.
    여기서, int 자료형으로하면 오버플로우가 나고 51%에서 계속해서 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_1199
    {

        static void Main1199(string[] args)
        {

            long NOT_VISIT = -2_000_000_000_000_000;
            long GEE = 1_000_000_000_000_000;
            int n, s, e, m;
            (int from, int to, long cost)[] edge;
            long[] gold;
            long[] cost;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                cost = new long[n];
                Array.Fill(cost, NOT_VISIT);
                cost[s] = gold[s];
                int len = 1 + n << 1;
                for (int i = 0; i < len; i++)
                {

                    for (int j = 0; j < m; j++)
                    {

                        int f = edge[j].from;
                        if (cost[f] == NOT_VISIT) continue;
                        int t = edge[j].to;
                        long chk = cost[f] - edge[j].cost + gold[t];
                        
                        if (cost[f] == GEE) cost[t] = GEE;
                        else if (chk <= cost[t]) continue;
                        else
                        {

                            if (i >= n) cost[t] = GEE;
                            else cost[t] = chk;
                        }
                    }
                }

                if (cost[e] == NOT_VISIT) Console.Write("gg");
                else if (cost[e] == GEE) Console.Write("Gee");
                else Console.Write(cost[e]);
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                s = ReadInt();
                e = ReadInt();
                m = ReadInt();

                edge = new (int from, int to, long cost)[m];
                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    int dis = ReadInt();

                    edge[i] = (f, t, dis);
                }

                gold = new long[n];
                for (int i = 0; i < n; i++)
                {

                    gold[i] = ReadInt();
                }

                sr.Close();

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
int[] inputs;
int N, M, start, end;
const int MIN = -123456789;
List<((int, int), int)> links = new List<((int, int), int)>();
List<((int, int), int)> temps = new List<((int, int), int)>();
List<int>[] nodes;
long[] dist;
int[] profit;
bool[] isMinusCycle;
bool[] isVisited;

inputs = Array.ConvertAll(Console.ReadLine()!.Split(), int.Parse);
N = inputs[0];
start = inputs[1];
end = inputs[2];
M = inputs[3];

nodes = new List<int>[N];
dist = new long[N];

for (int i = 0; i < N; i++)
{
    nodes[i] = new List<int>();
    dist[i] = MIN;
}

int a, b, c;
for (int i = 0; i < M; i++)
{
    inputs = Array.ConvertAll(Console.ReadLine()!.Split(), int.Parse);
    a = inputs[0]; b = inputs[1]; c = inputs[2];
    nodes[a].Add(b);
    links.Add(((a, b), -c));
}

profit = Array.ConvertAll(Console.ReadLine()!.Split(), int.Parse);

isMinusCycle = new bool[N];
isVisited = new bool[N];
Queue<int> que = new Queue<int>();
int cur, next, cost;
dist[start] = profit[start];
BF();
if (BFS())
{
    Console.WriteLine("Gee");
    return;
}

Console.WriteLine(dist[end] == MIN ? "gg" : dist[end]);

bool BFS()
{
    bool ret = default;
    while (que.Count != 0)
    {
        int cur = que.Dequeue();
        if (isVisited[cur]) continue;
        isVisited[cur] = true;
        if (cur == end)
        {
            ret = true;
            break;
        }

        foreach (int next in nodes[cur])
            que.Enqueue(next);
    }
    return ret;
}

void BF()
{
    for (int i = 0; i < N; i++)
    {
        for (int j = 0; j < links.Count; j++)
        {
            cur = links[j].Item1.Item1;
            next = links[j].Item1.Item2;
            cost = links[j].Item2;
            if (dist[cur] != MIN && dist[next] < dist[cur] + cost + profit[next])
            {
                dist[next] = dist[cur] + cost + profit[next];
                if (i == N - 1)
                    que.Enqueue(next);
            }
        }
    }
}

#endif
}
