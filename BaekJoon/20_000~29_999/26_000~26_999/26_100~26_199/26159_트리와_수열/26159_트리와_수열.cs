using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 12
이름 : 배성훈
내용 : 트리와 수열
    문제번호 : 26159번

    그리디, 정렬, 트리, dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1762
    {

        static void Main1762(string[] args)
        {

            int n;
            List<(int dst, int idx)>[] edge;
            int[] dis;

            Input();

            GetRet();

            void GetRet()
            {

                int[] child = new int[n + 1];

                SetChild();

                long ret = 0;
                long MOD = 1_000_000_007;

                long[] use = new long[n - 1];
                DFS();

                Array.Sort(dis);
                Array.Sort(use);

                for (int i = 0, j = n - 2; i < use.Length; i++, j--)
                {

                    ret = (ret + ((use[i] % MOD) * dis[j]) % MOD) % MOD;
                }

                Console.Write(ret);

                void DFS(int _cur = 1, int _prev = 0)
                {

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i].dst;
                        if (next == _prev) continue;

                        long cnt = child[next];
                        cnt *= n - child[next];

                        use[edge[_cur][i].idx] = cnt;

                        DFS(next, _cur);
                    }
                }

                int SetChild(int _cur = 1, int _prev = 0)
                {

                    ref int ret = ref child[_cur];
                    if (ret != 0) return ret;
                    ret = 1;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i].dst;
                        if (next == _prev) continue;

                        ret += SetChild(next, _cur);
                    }

                    return ret;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                edge = new List<(int dst, int idx)>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 0; i < n - 1; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    edge[f].Add((t, i));
                    edge[t].Add((f, i));
                }

                dis = new int[n - 1];
                for (int i = 0; i < dis.Length; i++)
                {

                    dis[i] = ReadInt();
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
