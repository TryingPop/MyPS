using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 29
이름 : 배성훈
내용 : 횡단보도
    문제번호 : 24042번

    다익스트라 문제다.
    주어진 횡단보도에 최소로 도달 할 수 있는 시간을 만드는 함수를 찾고 다익스트라 돌리면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1588
    {

        static void Main1588(string[] args)
        {

            int n, m;
            List<(int dst, int m)>[] edge;

            Input();

            GetRet();

            void GetRet()
            {

                long[] dis = new long[n + 1];
                bool[] visit = new bool[n + 1];

                Array.Fill(dis, -1);
                dis[1] = 0;

                PriorityQueue<int, long> pq = new(n);
                pq.Enqueue(1, 0);

                while (pq.Count > 0)
                {

                    int node = pq.Dequeue();
                    if (visit[node]) continue;
                    visit[node] = true;

                    for (int i = 0; i < edge[node].Count; i++)
                    {

                        int next = edge[node][i].dst;
                        int nM = edge[node][i].m;

                        if (visit[next]) continue;
                        long nextDis = GetNext(dis[node], nM);

                        if (dis[next] == -1 || nextDis < dis[next])
                        {

                            dis[next] = nextDis;
                            pq.Enqueue(next, nextDis);
                        }
                    }
                }

                Console.Write(dis[n]);

                long GetNext(long _cur, int _i)
                {

                    long k = (_cur - 1 - _i);
                    if (k < 0) k = 0;
                    else k = 1 + k / m;
                    return k * m + 1 + _i;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                edge = new List<(int dst, int m)>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    edge[f].Add((t, i));
                    edge[t].Add((f, i));
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
                        if (c == '\n' || c == -1) return true;
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
}
