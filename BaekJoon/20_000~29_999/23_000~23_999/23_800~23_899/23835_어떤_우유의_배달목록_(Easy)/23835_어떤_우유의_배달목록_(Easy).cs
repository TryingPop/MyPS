using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 5
이름 : 배성훈
내용 : 어떤 우유의 배달목록 (Easy)
    문제번호 : 23835번

    트리, dfs 문제다.
    먼저 HLD를 안쓴다면 어떻게 접근할까 생각해 푼 문제다.
    최대 쿼리가 1000개이고, 탐색은 많아야 노드의 최대 개수 1000개이므로,
    100만번 연산정도가 된다. 그래서 DFS로 탐색했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1252
    {

        static void Main1252(string[] args)
        {

            StreamReader sr;
            int n;
            List<int>[] edge;
            int[] milk;


            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                int end;
                int m = ReadInt();

                while (m-- > 0)
                {

                    int op = ReadInt();

                    if (op == 1)
                    {

                        int start = ReadInt();
                        end = ReadInt();

                        DFS(start, 0);
                    }
                    else
                    {

                        int idx = ReadInt();
                        sw.Write($"{milk[idx]}\n");
                    }
                }

                sr.Close();

                bool DFS(int _cur, int _prev, int _dep = 0)
                {

                    milk[_cur]+= _dep;
                    if (end == _cur) return true;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (next == _prev) continue;

                        if (DFS(next, _cur, _dep + 1)) return true;
                    }

                    milk[_cur] -= _dep;
                    return false;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                milk = new int[n + 1];
                edge = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 1; i < n; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    edge[f].Add(t);
                    edge[t].Add(f);
                }
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
