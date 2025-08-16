using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 14
이름 : 배성훈
내용 : 아침은 고구마야 (Normal)
    문제번호 : 20426번

    dp, 트리 문제다.
    고구마가 분할되는 경우는 없다.
    그래서 고구마를 하나의 점으로 생각하면 dp로 풀린다.
*/

namespace BaekJoon.etc
{
    internal class etc_1823
    {

        static void Main1823(string[] args)
        {

            long INF = 1_000_000_000_000_000_000;
            int n, m;
            List<(int dst, int dis)>[] edge;
            long[] dp, min;
            bool[] isPotato, isContain, visit;
            int[] stk;
            Input();

            GetRet();

            void GetRet()
            {

                dp = new long[n + 1];
                min = new long[n + 1];
                min[1] = INF;

                isContain = new bool[n + 1];
                isPotato = new bool[n + 1];
                visit = new bool[n + 1];

                stk = new int[n + 1];
                int len = 0;

                DFS(1, 0);

                Console.Write(dp[1]);

                void DFS(int _cur, int _prev)
                {

                    long chk = 0;
                    visit[_cur] = true;
                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i].dst;
                        if (next == _prev) continue;

                        if (isContain[next])
                        {

                            int chkLen = len;
                            while (stk[--chkLen] != next)
                            {

                                isPotato[stk[chkLen]] = true;
                            }

                            long s = (len - chkLen);
                            s *= s;
                            dp[next] = s;
                        }
                        else if (!visit[next])
                        {

                            isContain[next] = true;
                            stk[len++] = next;
                            min[next] = edge[_cur][i].dis;

                            DFS(next, _cur);
                            isContain[next] = false;
                            len--;
                            chk += min[next];
                            dp[_cur] += dp[next];
                        }
                    }

                    if (isPotato[_cur]) min[_cur] = INF;
                    else if (chk == 0) chk = INF;

                    if (min[_cur] < chk) dp[_cur] = 0;
                    else min[_cur] = chk;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                edge = new List<(int dst, int dis)>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    int d = ReadInt();

                    edge[f].Add((t, d));
                    edge[t].Add((f, d));
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
                        if (c == '\n' || c == ' ' || c == '\t') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n' && c != '\t')
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
