using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 4
이름 : 배성훈
내용 : Brexit
    문제번호 : 13463번

    BFS 문제다.
    아이디어는 다음과 같다.
    이어진 절반 이상이 탈퇴하면 자신도 탈퇴한다.
    그래서 이어진 간선과 끊어진 간선을 세고
    간선이 끊어지면 절반 이상 끊어졌는지 확인해 다음 탐색에 넣는다.
    이렇게 탐색하면 많아야 각각의 노드를 1번 탐색 혹은 간선을 2번 탐색한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1149
    {

        static void Main1149(string[] args)
        {

            int c, p, x, l;
            List<int>[] edge;
            int[] conn, leave;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Queue<int> q = new(c);
                conn[l] = -1;
                q.Enqueue(l);

                while (q.Count > 0)
                {

                    int node = q.Dequeue();

                    for (int i = 0; i < edge[node].Count; i++)
                    {

                        int next = edge[node][i];
                        if (conn[next] == -1) continue;
                        leave[next]++;

                        if (leave[next] * 2 >= conn[next])
                        {

                            conn[next] = -1;
                            q.Enqueue(next);
                        }
                    }

                }

                Console.Write(conn[x] == -1 ? "leave" : "stay");
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                c = ReadInt();
                p = ReadInt();
                x = ReadInt();
                l = ReadInt();

                edge = new List<int>[c + 1];
                for (int i = 1; i <= c; i++)
                {

                    edge[i] = new();
                }

                conn = new int[c + 1];
                leave = new int[c + 1];

                for (int i = 0; i < p; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    edge[f].Add(b);
                    edge[b].Add(f);

                    conn[f]++;
                    conn[b]++;
                }

                sr.Close();

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
}
