using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 13
이름 : 배성훈
내용 : Ignition
    문제번호 : 13141번

    플로이드 워셜 문제다.
    노드와 간선을 모두 태워야 한다.
    불은 특정 노드에서 시작해 모든 간선들을 1초당 1씩 이동하면 탄다.
    edge[i] = (f, t, dis)를 f노드와 t노드가 이어진 간선이고
    간선의 거리가 dis라 하자.

    그러면 불이 타는 과정을 보면 시작 노드에서 f의 최단 거리와 
    시작 노드에서 t와의 최단 거리를 확인한다.
    두 시간 거리 차이가 최단 경로의 정의로 dis보다 크거나 같을 것이다.

    만약 같은 경우 해당 간선은 f와 t로 이동하면서 모두 타기에 가장 긴 거리가 된다.

    반면 dis가 최단 경로 차이보다 크다면 해당 경로는 차이만큼 불에 탔다.
    그리고 양쪽으로 불타 오면서 1초에 거리 2씩 불에 탈것이다.
    그래서 해당 간선이 타는데 걸리는 시간은 최대 시간 + 남은 간선의 길이 / 2가 된다.

    먼저 모든 노드에 대해 다른 노드로 가는 최단 경로를 찾아야 한다.
    노드의 갯수가 200이므로 플로이드 워셜로 찾았다.
    이후 각 노드에서 시작해 모든 간선들을 태우는데 최소시간을 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1185
    {

        static void Main1185(string[] args)
        {

            int INF = 200_000;
            int n, m;
            int[][] fw;
            (int f, int t, int dis)[] edge;

            Solve();
            void Solve()
            {

                Input();

                FW();

                GetRet();
            }

            void Input() 
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                fw = new int[n + 1][];
                for (int i = 1; i <= n; i++)
                {

                    fw[i] = new int[n + 1];
                    Array.Fill(fw[i], INF);
                    fw[i][i] = 0;
                }

                edge = new (int f, int t, int dis)[m];
                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    int dis = ReadInt() * 2;

                    edge[i] = (f, t, dis);
                    fw[f][t] = Math.Min(fw[f][t], dis);
                    fw[t][f] = Math.Min(fw[t][f], dis);
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

            void FW()
            {

                for (int mid = 1; mid <= n; mid++)
                {

                    for (int start = 1; start <= n; start++)
                    {

                        if (fw[start][mid] == INF) continue;

                        for (int end = 1; end <= n; end++)
                        {

                            if (fw[mid][end] == INF) continue;
                            ref int conn = ref fw[start][end];
                            conn = Math.Min(conn, fw[start][mid] + fw[mid][end]);
                        }
                    }
                }
            }

            void GetRet()
            {

                int ret = INF;
                for (int start = 1; start <= n; start++)
                {

                    int time = 0;
                    for (int i = 0; i < m; i++)
                    {

                        int min, max;
                        if (fw[start][edge[i].f] <= fw[start][edge[i].t])
                        {

                            min = fw[start][edge[i].f];
                            max = fw[start][edge[i].t];
                        }
                        else
                        {

                            min = fw[start][edge[i].t];
                            max = fw[start][edge[i].f];
                        }

                        int dis = edge[i].dis - (max - min);
                        if (dis < 0) time = Math.Max(time, max);
                        else time = Math.Max(time, max + (dis >> 1));
                    }

                    ret = Math.Min(ret, time);
                }

                int f = ret >> 1;
                int b = (ret & 1) == 1 ? 5 : 0;

                Console.Write($"{f}.{b}");
            }
        }
    }
}
