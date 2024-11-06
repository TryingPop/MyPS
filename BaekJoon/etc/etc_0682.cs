using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 8
이름 : 배성훈
내용 : 그대, 그머가 되어
    문제번호 : 14496번

    BFS 문제다
    간선이 양방향 간선이다
    단방향으로 해서 한 번 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_0682
    {

        static void Main682(string[] args)
        {

            StreamReader sr;
            int s, e;
            int n, m;
            List<int>[] line;

            Solve();

            void Solve()
            {

                Input();
                int ret = BFS();
                Console.WriteLine(ret);
            }

            int BFS()
            {

                int[] arr = new int[n + 1];
                Array.Fill(arr, -1);
                bool[] visit = new bool[n + 1];

                Queue<int> q = new(n + 1);
                q.Enqueue(s);
                visit[s] = true;
                arr[s] = 0;
                while(q.Count > 0)
                {

                    int node = q.Dequeue();

                    for (int i = 0; i < line[node].Count; i++)
                    {

                        int next = line[node][i];
                        if (visit[next]) continue;
                        visit[next] = true;

                        q.Enqueue(next);
                        arr[next] = arr[node] + 1;
                    }
                }

                return arr[e];
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                s = ReadInt();
                e = ReadInt();

                n = ReadInt();
                m = ReadInt();
                line = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    line[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    line[f].Add(b);
                    line[b].Add(f);
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c= sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
