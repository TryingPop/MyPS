using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 7
이름 : 배성훈
내용 : 약속장소 정하기 (Large)
    문제번호 : 12442번

    다익스트라 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1687
    {

        static void Main1687(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t = ReadInt();
            int n, p, m;
            List<(int dst, int dis)>[] edge;
            int[][] dis;
            int[][] info;
            bool[] visit;
            PriorityQueue<int, int> pq;

            Init();

            for (int i = 1; i <= t; i++)
            {

                Input();

                sw.Write($"Case #{i}: ");
                sw.Write(GetRet());
                sw.Write('\n');
            }

            void Init()
            {

                int MAX_N = 10_000;
                int MAX_P = 100;
                int MAX_M = 1_000;
                int MAX_L = 150;

                edge = new List<(int dst, int dis)>[MAX_N + 1];

                for (int i = 1; i <= MAX_N; i++)
                {

                    edge[i] = new();
                }

                dis = new int[MAX_P][];
                info = new int[MAX_P][];
                for (int i = 0; i < MAX_P; i++)
                {

                    dis[i] = new int[MAX_N + 1];
                    info[i] = new int[2];
                }

                visit = new bool[MAX_N + 1];
                pq = new(MAX_M * MAX_L);
            }

            void Input()
            {

                n = ReadInt();
                p = ReadInt();
                m = ReadInt();

                for (int i = 1; i <= n; i++)
                {

                    edge[i].Clear();
                }

                for (int i = 0; i < p; i++)
                {

                    info[i][0] = ReadInt();
                    info[i][1] = ReadInt();
                }

                for (int i = 0; i < m; i++)
                {

                    int dis = ReadInt();
                    int l = ReadInt();

                    int cur = ReadInt();

                    for (int j = 1; j < l; j++)
                    {

                        int prev = cur;
                        cur = ReadInt();
                        edge[cur].Add((prev, dis));
                        edge[prev].Add((cur, dis));
                    }
                }
            }

            long GetRet()
            {

                long INF = 1_000_000_000_000_000;

                for (int i = 0; i < p; i++)
                {

                    Dijkstra(info[i][0], dis[i]);
                }

                long ret = INF;
                for (int i = 1; i <= n; i++)
                {

                    bool flag = false;
                    long time = 0;
                    for (int j = 0; j < p; j++)
                    {

                        if (dis[j][i] == -1)
                        {

                            flag = true;
                            break;
                        }

                        long chk = 1L * info[j][1] * dis[j][i];
                        time = Math.Max(time, chk);
                    }

                    if (flag) continue;
                    ret = Math.Min(ret, time);
                }

                if (ret == INF) ret = -1;
                return ret;

                void Dijkstra(int _s, int[] _dis)
                {

                    Array.Fill(_dis, -1, 1, n);
                    Array.Fill(visit, false, 1, n);
                    _dis[_s] = 0;

                    pq.Enqueue(_s, 0);
                    
                    while (pq.Count > 0)
                    {

                        int cur = pq.Dequeue();

                        if (visit[cur]) continue;
                        visit[cur] = true;

                        for (int i = 0; i < edge[cur].Count; i++)
                        {

                            int next = edge[cur][i].dst;
                            if (visit[next]) continue;

                            int nDis = _dis[cur] + edge[cur][i].dis;
                            if (_dis[next] == -1 || nDis < _dis[next])
                            {

                                pq.Enqueue(next, nDis);
                                _dis[next] = nDis;
                            }
                        }
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
}
