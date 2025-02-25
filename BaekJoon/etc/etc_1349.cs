using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 19
이름 : 배성훈
내용 : 저울
    문제번호 : 10159번

    위상정렬, 그래프 이론 문제다.
    대소 관계로 간선을 이으면 위상정렬하는 문제로 바뀐다.
    작은쪽 간선과 큰쪽 간선으로 방문 탐색을 해주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1349
    {

        static void Main1349(string[] args)
        {

            int n, m;
            List<int>[] edge1, edge2;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                bool[] visit = new bool[n + 1];
                int[] ret = new int[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    visit[i] = true;
                    DFS(i, edge1);
                    for (int j = 1; j <= n; j++)
                    {

                        if (visit[j]) 
                        {

                            ret[i]++; 
                            visit[j] = false;
                        }
                    }

                    visit[i] = true;
                    DFS(i, edge2);
                    for (int j = 1; j <= n; j++)
                    {

                        if (visit[j])
                        {

                            ret[i]++;
                            visit[j] = false;
                        }
                    }

                    ret[i]--;
                }

                void DFS(int _cur, List<int>[] _edge)
                {

                    for (int i = 0; i < _edge[_cur].Count; i++)
                    {

                        int next = _edge[_cur][i];
                        if (visit[next]) continue;
                        visit[next] = true;
                        DFS(next, _edge);
                    }
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 1; i <= n; i++)
                {

                    sw.WriteLine(n - ret[i]);
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                edge1 = new List<int>[n + 1];
                edge2 = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge1[i] = new();
                    edge2[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    edge1[f].Add(t);
                    edge2[t].Add(f);
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == ' ' || c == '\n') return true;

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
}
