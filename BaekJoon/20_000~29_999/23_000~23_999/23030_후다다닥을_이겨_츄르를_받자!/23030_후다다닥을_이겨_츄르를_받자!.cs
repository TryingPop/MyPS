using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 18
이름 : 배성훈
내용 : 후다다닥을 이겨 츄르를 받자!
    문제번호 : 23030번

    다익스트라 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1555
    {

        static void Main1555(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n;
            int[] add, dis;
            bool[] visit;
            List<(int dst, int time)>[] edge;
            PriorityQueue<int, int> pq;

            Input();

            SetEdge();

            GetRet();

            void GetRet()
            {

                pq = new(add[n + 1]);
                dis = new int[add[n + 1] + 1];
                visit = new bool[add[n + 1] + 1];

                int k = ReadInt();
                while(k-- > 0)
                {

                    int t = ReadInt();
                    int u1 = ReadInt();
                    int u2 = ReadInt();

                    int v1 = ReadInt();
                    int v2 = ReadInt();

                    int s = u2 + add[u1];
                    int e = v2 + add[v1];

                    int ret = Dijkstra(s, e, t);
                    sw.Write($"{ret}\n");
                }

                int Dijkstra(int _s, int _e, int _t)
                {

                    pq.Clear();
                    Array.Fill(dis, -1);
                    Array.Fill(visit, false);

                    dis[_s] = 0;
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

                            int addTime = edge[cur][i].time;
                            // 환승인 경우 환승에 걸리는 시간으로 변경
                            if (addTime == -1) addTime = _t;
                            int nextTime = dis[cur] + addTime;

                            if (dis[next] == -1 || nextTime < dis[next])
                            {

                                pq.Enqueue(next, nextTime);
                                dis[next] = nextTime;
                            }
                        }
                    }

                    return dis[_e];
                }
            }

            void SetEdge()
            {

                edge = new List<(int dst, int time)>[add[n + 1] + 1];

                for (int i = 1; i < edge.Length; i++)
                {

                    edge[i] = new();
                }

                int m = ReadInt();

                for (int i = 0; i < m; i++)
                {

                    int p1 = ReadInt();
                    int p2 = ReadInt();

                    int q1 = ReadInt();
                    int q2 = ReadInt();

                    int f = p2 + add[p1];
                    int t = q2 + add[q1];

                    edge[f].Add((t, -1));
                    edge[t].Add((f, -1));
                }

                for (int i = 1; i <= n; i++)
                {

                    for (int j = add[i] + 1; j < add[i + 1]; j++)
                    {

                        edge[j].Add((j + 1, 1));
                        edge[j + 1].Add((j, 1));
                    }
                }
            }

            void Input()
            {

                n = ReadInt();

                // 1번 1번역부터 .. n번 역을 1 부터 1씩 증가하며 만들거기에 추가 번호다
                add = new int[n + 2];
                for (int i = 1; i <= n; i++)
                {

                    add[i + 1] = add[i] + ReadInt();
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
