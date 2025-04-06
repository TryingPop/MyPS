using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 6
이름 : 배성훈
내용 : 발전소 설치
    문제번호 : 1277번

    다익스트라 문제다.
    음수 입력을 처리 안해 한 번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1523
    {

        static void Main1523(string[] args)
        {

            int n;
            double[][] dis;
            (int x, int y)[] pos;
            double m;

            Input();

            GetRet();

            void GetRet()
            {

                PriorityQueue<int, double> pq = new(n);
                double[] minDis = new double[n + 1];
                bool[] visit = new bool[n + 1];

                Array.Fill(minDis, -1.0);
                minDis[1] = 0.0;
                pq.Enqueue(1, 0.0);

                while (pq.Count > 0)
                {

                    int cur = pq.Dequeue();
                    // 방문 여부 확인
                    if (visit[cur]) continue;
                    visit[cur] = true;

                    for (int next = 1; next <= n; next++)
                    {

                        // 방문한 곳은 재방문 X
                        if (visit[next] || dis[cur][next] == -1.0) continue;

                        double nextDis = dis[cur][next] + minDis[cur];
                        if (minDis[next] == -1.0 || nextDis + 1e-9 < minDis[next])
                        {

                            pq.Enqueue(next, nextDis);
                            minDis[next] = nextDis;
                        }
                    }
                }

                long ret = (long)(minDis[n] * 1000);
                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                int w = ReadInt();

                m = double.Parse(sr.ReadLine());

                pos = new (int x, int y)[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    pos[i] = (ReadInt(), ReadInt());
                }

                dis = new double[n + 1][];
                for (int i = 1; i <= n; i++)
                {

                    dis[i] = new double[n + 1];
                }

                for (int i = 1; i <= n; i++)
                {

                    for (int j = i + 1; j <= n; j++)
                    {

                        double d = GetDis(i, j);
                        if (d > m) d = -1.0;
                        dis[i][j] = d;
                        dis[j][i] = d;
                    }

                    dis[i][i] = -1.0;
                }

                for (int i = 0; i < w; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    dis[f][t] = 0.0;
                    dis[t][f] = 0.0;
                }

                double GetDis(int _i, int _j)
                {

                    double x = pos[_i].x - pos[_j].x;
                    double y = pos[_i].y - pos[_j].y;

                    return Math.Sqrt(x * x + y * y);
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
                        bool positive = c != '-';
                        ret = positive ? c - '0' : 0;

                        while((c =sr.Read()) != -1 && c != ' ' &&c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        ret = positive ? ret : -ret;
                        return false;
                    }
                }
            }
        }
    }
}
