using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 13
이름 : 배성훈
내용 : 영일랜드
    문제번호 : 31871번

    백트래킹, 브루트포스 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1883
    {

        static void Main1883(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = ReadInt();
            int[][] dis = new int[n + 1][];
            for (int i = 0; i <= n; i++)
            {

                dis[i] = new int[n + 1];
                Array.Fill(dis[i], -1);
                dis[i][i] = 0;
            }

            int m = ReadInt();
            for (int i = 0; i < m; i++)
            {

                int f = ReadInt();
                int t = ReadInt();
                int d = ReadInt();
                dis[f][t] = Math.Max(dis[f][t], d);
            }

            int ret = -1;
            int escape = (1 << (n + 1)) - 1;

            DFS();

            Console.Write(ret);

            void DFS(int cur = 0, int sum = 0, int state = 1)
            {

                if (state == escape)
                {

                    if (dis[cur][0] != -1) ret = Math.Max(sum + dis[cur][0], ret);
                    return;
                }

                for (int next = 1; next <= n; next++)
                {

                    if ((state & (1 << next)) != 0
                        || dis[cur][next] == -1) continue;

                    int nState = state | (1 << next);
                    DFS(next, sum + dis[cur][next], nState);
                }
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
