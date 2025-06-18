using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 18
이름 : 배성훈
내용 : 해킹
    문제번호 : 10282번

    다익스트라 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1712
    {

        static void Main1712(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int NOT_VISIT = -1;
            int n, d, c;
            List<(int dst, int time)>[] edge;
            bool[] visit;
            int[] time;
            PriorityQueue<int, int> pq;

            Init();

            int t = ReadInt();
            while (t-- > 0)
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                pq.Clear();

                time[c] = 0;
                pq.Enqueue(c, time[c]);

                while (pq.Count > 0)
                {

                    var node = pq.Dequeue();

                    if (visit[node]) continue;
                    visit[node] = true;

                    for (int i = 0; i < edge[node].Count; i++)
                    {

                        int next = edge[node][i].dst;
                        if (visit[next]) continue;

                        int nTime = time[node] + edge[node][i].time;
                        if (time[next] == NOT_VISIT || nTime < time[next])
                        {

                            time[next] = nTime;
                            pq.Enqueue(next, time[next]);
                        }
                    }
                }

                int ret1 = 0;
                int ret2 = 0;
                for (int i = 1; i <= n; i++)
                {

                    if (visit[i])
                    {

                        ret1++;
                        ret2 = Math.Max(time[i], ret2);
                    }
                }

                sw.Write($"{ret1} {ret2}\n");
            }

            void Init()
            {

                int MAX = 10_000;
                edge = new List<(int dst, int time)>[MAX + 1];
                for (int i = 1; i <= MAX; i++)
                {

                    edge[i] = new();
                }

                visit = new bool[MAX + 1];
                time = new int[MAX + 1];
                pq = new(100_000 + MAX);
            }

            void Input()
            {

                n = ReadInt();
                d = ReadInt();
                c = ReadInt();

                for (int i = 1; i <= n; i++)
                {

                    edge[i].Clear();
                    visit[i] = false;
                    time[i] = NOT_VISIT;
                }

                for (int i = 0; i < d; i++)
                {

                    int dst = ReadInt();
                    int src = ReadInt();
                    int t = ReadInt();

                    edge[src].Add((dst, t));
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
