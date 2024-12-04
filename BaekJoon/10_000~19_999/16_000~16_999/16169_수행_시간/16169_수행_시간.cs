using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 4
이름 : 배성훈
내용 : 수행 시간
    문제번호 : 16169번

    위상 정렬 문제다.
    그래프를 보면 위상정렬된 그래프가 된다.
    그런데 정렬하면 순차적으로 해도 되어 정렬해서 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1148
    {

        static void Main1148(string[] args)
        {

            int n;
            (int lvl, int time, int idx)[] coms;
            List<(int dst, int dis)>[] edge;

            Solve();
            void Solve()
            {

                Input();

                SetEdge();

                GetRet();
            }

            void GetRet()
            {

                int[] ret = new int[n];

                for (int i = 0; i < n; i++)
                {

                    ret[i] += coms[i].time;
                    for (int j = 0; j < edge[i].Count; j++)
                    {

                        int next = edge[i][j].dst;
                        int nTime = edge[i][j].dis + ret[i];
                        ret[next] = Math.Max(ret[next], nTime);
                    }
                }

                int max = 0;
                for (int i = 0; i < n; i++)
                {

                    max = Math.Max(ret[i], max);
                }

                Console.Write(max);
            }

            void SetEdge()
            {

                edge = new List<(int dst, int dis)>[n];

                for (int i = 0; i < n; i++) 
                {

                    edge[i] = new();
                }

                Array.Sort(coms, (x, y) =>
                {

                    int ret = x.lvl.CompareTo(y.lvl);
                    if (ret == 0) x.time.CompareTo(y.time);
                    return ret;
                });

                for (int i = 0; i < n; i++)
                {

                    int next = coms[i].lvl + 1;
                    for (int j = i + 1; j < n; j++)
                    {

                        if (coms[j].lvl == next)
                        {

                            int dis = (coms[i].idx - coms[j].idx);
                            dis *= dis;
                            edge[i].Add((j, dis));
                        }
                    }
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                coms = new (int lvl, int time, int idx)[n];

                for (int i = 0; i < n; i++)
                {

                    coms[i] = (ReadInt(), ReadInt(), i);
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
}
