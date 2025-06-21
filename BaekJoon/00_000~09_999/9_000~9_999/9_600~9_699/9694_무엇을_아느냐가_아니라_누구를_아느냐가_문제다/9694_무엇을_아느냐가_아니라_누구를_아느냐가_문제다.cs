using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 21
이름 : 배성훈
내용 : 무엇을 아느냐가 아니라 누구를 아느냐가 문제다.
    문제번호 : 9694번

    다익스트라, 역추적 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1720
    {

        static void Main1720(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n, m, len;
            List<(int dst, int dis)>[] edge;
            int[] dis, back, stk;
            bool[] visit;
            PriorityQueue<int, int> pq;

            Init();

            int t = ReadInt();

            for (int i = 1; i <= t; i++)
            {

                Input();

                GetRet();

                Output(i);
            }

            void Output(int _t)
            {

                sw.Write($"Case #{_t}: ");

                if (dis[0] == -1) sw.Write(-1);
                else
                {

                    // for (int i = 0; i < len; i++)
                    for (int i = 0; i != -1; i = back[i])
                    {

                        // sw.Write($"{stk[i]} ");
                        sw.Write($"{i} ");
                    }
                }
                sw.Write('\n');
            }

            void GetRet()
            {

                Dijkstra();

                // Trace();

                void Dijkstra()
                {

                    dis[m - 1] = 0;
                    pq.Enqueue(m - 1, 0);

                    while (pq.Count > 0)
                    {

                        int node = pq.Dequeue();
                        if (visit[node]) continue;
                        visit[node] = true;
                        int curDis = dis[node];
                        for (int i = 0; i < edge[node].Count; i++)
                        {

                            int next = edge[node][i].dst;
                            if (visit[next]) continue;

                            int nDis = edge[node][i].dis + curDis;
                            if (dis[next] == -1 || nDis< dis[next])
                            {

                                pq.Enqueue(next, nDis);
                                dis[next] = nDis;
                                back[next] = node;
                            }
                        }
                    }
                }

                void Trace()
                {

                    len = 0;
                    for (int i = 0; i != -1; i = back[i])
                    {

                        stk[len++] = i;
                    }
                }
            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();

                for (int i = 0; i < m; i++)
                {

                    edge[i].Clear();
                    dis[i] = -1;
                    back[i] = -1;
                    visit[i] = false;
                }

                for (int i = 0; i < n; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    int type = ReadInt();

                    edge[f].Add((t, type));
                    edge[t].Add((f, type));
                }
            }

            void Init()
            {

                int MAX = 20;
                edge = new List<(int dst, int dis)>[MAX];
                for (int i = 0; i < MAX; i++)
                {

                    edge[i] = new(MAX);
                }

                dis = new int[MAX];
                back = new int[MAX];
                visit = new bool[MAX];
                stk = new int[MAX];

                pq = new(MAX);
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
                    if (c < '0' || c > '9') return true;
                    ret = c - '0';

                    while ((c = sr.Read()) <= '9' && '0' <= c)
                    {

                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }
}
