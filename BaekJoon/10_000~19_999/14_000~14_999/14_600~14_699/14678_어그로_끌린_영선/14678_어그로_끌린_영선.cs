using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 28
이름 : 배성훈
내용 : 어그로 끌린 영선
    문제번호 : 14678번

    트리에서의 dp, 그래프 이론 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1791
    {

        static void Main1791(string[] args)
        {

            int n;
            List<int>[] edge;

            Input();

            GetRet();

            void GetRet()
            {

                int ret;
                if (n < 3)
                {

                    ret = n & 1;
                    Console.Write(ret);
                    return;
                }

                int[][] dp = new int[2][];

                for (int i = 0; i < 2; i++)
                {

                    dp[i] = new int[n + 1];
                }

                int root = 0;
                for (int i = 1; i <= n; i++)
                {

                    if (edge[i].Count == 1) continue;
                    root = i;
                    break;
                }

                DFS(root);

                bool flag = true;
                for (int i = 0; i < edge[root].Count; i++)
                {

                    int next = edge[root][i];
                    flag &= edge[next].Count == 1;
                }

                if (flag) dp[1][root]--;
                ret = Math.Max(dp[1][root], dp[0][root]);
                Console.Write(ret);

                void DFS(int _cur, int _prev = 0)
                {

                    dp[0][_cur] = edge[_cur].Count == 1 ? 1 : 0;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (next == _prev) continue;
                        DFS(next, _cur);
                        dp[0][_cur] += dp[1][next];
                        dp[1][_cur] += dp[0][next];
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

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
}
