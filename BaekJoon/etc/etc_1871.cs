using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 6
이름 : 배성훈
내용 : 불꽃놀이의 아름다움
    문제번호 : 31839번

    트리, dp 문제다.
    부모 노드 p에서 자식 노드 c로 스위치가 이동할 때 값의 변화를 살펴보면 다음과 같다.
    노드 a에서 점수를 S(a)라 하자.
    그리고 루트를 1로 잡은 트리에서 루트가 b인 서브 트리의 w의 총합을 W(b)라 하자.
    그러면 S(c) = S(p) - 2 * W(b) + W(1)이 됨을 알 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1871
    {

        static void Main1871(string[] args)
        {

            int n;
            List<int>[] edge;
            int[] w;
            long[] sum;

            Input();

            SetArr();

            GetRet();

            void GetRet()
            {

                long ret = GetFirst();

                DFS(ret);

                Console.Write(ret);

                void DFS(long val, int cur = 1, int prev = 0)
                {

                    for (int i = 0; i < edge[cur].Count; i++)
                    {

                        int next = edge[cur][i];
                        if (next == prev) continue;

                        long nVal = val + sum[1] - sum[next] - sum[next];
                        ret = Math.Max(ret, nVal);
                        DFS(nVal, next, cur);
                    }
                }

                long GetFirst(int cur = 1, int prev = 0, int dis = 0)
                {

                    long ret = 1L * dis * w[cur];
                    for (int i = 0; i < edge[cur].Count; i++)
                    {

                        int next = edge[cur][i];
                        if (next == prev) continue;
                        ret += GetFirst(next, cur, dis + 1);
                    }

                    return ret;
                }
            }

            void SetArr()
            {

                sum = new long[n + 1];

                DFS();
                long DFS(int cur = 1, int prev = 0)
                {

                    sum[cur] = w[cur];
                    for (int i = 0; i < edge[cur].Count; i++)
                    {

                        int next = edge[cur][i];
                        if (next == prev) continue;

                        sum[cur] += DFS(next, cur);
                    }

                    return sum[cur];
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

                    int a = ReadInt();
                    int b = ReadInt();

                    edge[a].Add(b);
                    edge[b].Add(a);
                }

                w = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    w[i] = ReadInt();
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
