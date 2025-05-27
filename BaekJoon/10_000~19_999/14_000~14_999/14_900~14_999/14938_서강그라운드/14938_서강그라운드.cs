using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 3
이름 : 배성훈
내용 : 서강그라운드
    문제번호 : 14938번

    플로이드 워셜 문제다.
    각 노드에서 다른 노드로 가는 최단 경로를 찾아야 한다.
    노드의 수가 100으로 작고 모든 최단 경로를 찾아야 하므로 플로이드 워셜을 선택했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1143
    {

        static void Main1143(string[] args)
        {

            int INF = 10_000;
            int[][] fw;
            int n, m;
            int[] item;
            
            Solve();
            void Solve()
            {

                Input();

                FloydWarshall();

                GetRet();
            }

            void GetRet()
            {

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    int cur = 0;
                    for (int j = 0; j < n; j++)
                    {

                        if (fw[i][j] > m) continue;
                        cur += item[j];
                    }

                    ret = Math.Max(ret, cur);
                }

                Console.Write(ret);
            }

            void FloydWarshall()
            {

                for (int mid = 0; mid < n; mid++)
                {

                    for (int start = 0; start < n; start++)
                    {

                        if (fw[start][mid] == INF) continue;
                        for (int end = 0; end < n; end++)
                        {

                            if (fw[mid][end] == INF) continue;
                            fw[start][end] = Math.Min(fw[start][end], fw[start][mid] + fw[mid][end]);
                        }
                    }
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();
                item = new int[n];
                fw = new int[n][];

                for (int i = 0; i < n; i++)
                {

                    fw[i] = new int[n];
                    for (int j = 0; j < n; j++)
                    {

                        if (i == j) continue;
                        fw[i][j] = INF;
                    }
                }

                int len = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    item[i] = ReadInt();
                }

                for (int i = 0; i < len; i++)
                {

                    int f = ReadInt() - 1;
                    int b = ReadInt() - 1;
                    int dis = ReadInt();

                    fw[f][b] = Math.Min(dis, fw[f][b]);
                    fw[b][f] = Math.Min(dis, fw[b][f]);
                }

                sr.Close();

                int ReadInt()
                {

                    int c, ret = 0;
                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return ret;
                }
            }
        }
    }
}
