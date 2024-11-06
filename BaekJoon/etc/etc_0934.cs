using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 2
이름 : 배성훈
내용 : 음악프로그램
    문제번호 : 2623번

    위상 정렬 문제다
    사이클 판별을 잘못해서 2번 틀렸다
    길이가 n이하일 때 판별해야하는데, 0이하로 판별해서 틀렸다
    해당 부분 수정하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0934
    {

        static void Main934(string[] args)
        {

            StreamReader sr;

            int n, m;
            List<int>[] edge;
            int[] degree;
            int[] ret;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Queue<int> q = new(n);
                ret = new int[n];
                int len = 0;

                for (int i = 1; i <= n; i++)
                {

                    if (degree[i] > 0) continue;
                    q.Enqueue(i);
                }

                while(q.Count > 0)
                {

                    var node = q.Dequeue();
                    ret[len++] = node;

                    for (int i = 0; i < edge[node].Count; i++)
                    {

                        int next = edge[node][i];
                        degree[next]--;
                        if (degree[next] > 0) continue;

                        q.Enqueue(next);
                    }
                }

                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    if (len < n) sw.Write('0');
                    else
                    {

                        for (int i = 0; i < len; i++)
                        {

                            sw.Write(ret[i]);
                            sw.Write('\n');
                        }
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                edge = new List<int>[n + 1];
                degree = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int len = ReadInt();
                    int f = ReadInt();
                    for (int j = 1; j < len; j++)
                    {

                        int b = ReadInt();
                        edge[f].Add(b);
                        degree[b]++;
                        f = b;
                    }
                }

                sr.Close();
            }

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
