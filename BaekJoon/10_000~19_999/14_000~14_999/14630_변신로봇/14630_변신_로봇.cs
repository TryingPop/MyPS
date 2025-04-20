using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 20
이름 : 배성훈
내용 : 변신로봇
    문제번호 : 14630번

    다익스트라 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1560
    {

        static void Main1560(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();

            string[] arr = new string[n + 1];
            for (int i = 1; i <= n; i++)
            {

                arr[i] = sr.ReadLine();
            }

            int s = ReadInt();
            int e = ReadInt();

            int[][] dis;
            SetDis();

            Console.Write(Dijkstra());

            void SetDis()
            {

                dis = new int[n + 1][];
                for (int i = 1; i <= n; i++)
                {

                    dis[i] = new int[n + 1];
                }

                for (int i = 1; i <= n; i++)
                {

                    for (int j = i + 1; j <= n; j++)
                    {

                        int cur = GetDis(i, j);
                        dis[i][j] = cur;
                        dis[j][i] = cur;
                    }
                }

                int GetDis(int _i, int _j)
                {

                    int len = arr[_i].Length;
                    int ret = 0;
                    for (int i = 0; i < len; i++)
                    {

                        int x = arr[_i][i] - arr[_j][i];
                        ret += x * x;
                    }

                    return ret;
                }
            }

            int Dijkstra()
            {

                PriorityQueue<int, int> pq = new();
                int[] ret = new int[n + 1];
                bool[] visit = new bool[n + 1];
                Array.Fill(ret, -1);
                ret[s] = 0;

                pq.Enqueue(s, 0);

                while (pq.Count > 0)
                {

                    int cur = pq.Dequeue();
                    if (visit[cur]) continue;
                    visit[cur] = true;

                    for (int i = 1; i <= n; i++)
                    {

                        int nDis = ret[cur] + dis[cur][i];
                        if (ret[i] == -1 || nDis < ret[i])
                        {

                            pq.Enqueue(i, nDis);
                            ret[i] = nDis;
                        }
                    }
                }

                return ret[e];
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
