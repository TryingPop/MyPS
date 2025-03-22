using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 1
이름 : 배성훈
내용 : 트리 색칠하기
    문제번호 : 1693번

    dp, 트리 문제다.
    처음에는 4색으로 평면 그래프는 모두 칠할 수 있기에
    4색만 칠하면 되지 않을까 했다.
    그렇게 제출하니 틀렸다.

    이후에 찾아보니 log N으로 찾으면 된다고 한다.
    n개의 색상을 사용하는 경우 답이 나오는 최소 크기를 T(n)이라하면
    T(1) = 1이고, T(i) >= T(i - 1) + T(i - 2) + ... + T(1) + 1이 성립한다.
    이를 귀납적으로 계속해가면 T(n) >= 2^(n - 1)임을 알 수 있다.
    그래서 log2 n개의 색상을 이용하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1306
    {

        static void Main1306(string[] args)
        {

            int n;
            List<int>[] edge;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int COLOR = 18;
                int INF = 1_000_000_000;
                int[][] dp = new int[n + 1][];

                for (int i = 1; i <= n; i++)
                {

                    dp[i] = new int[COLOR];
                    Array.Fill(dp[i], -1);
                }

                int ret = INF;
                for (int i = 1; i < 5; i++)
                {

                    ret = Math.Min(ret, DFS(1, 0, i));
                }

                Console.Write(ret);

                int DFS(int _cur, int _prev, int _color)
                {

                    ref int ret = ref dp[_cur][_color];
                    if (ret != -1) return ret;
                    ret = _color;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (next == _prev) continue;

                        int min = INF;
                        for (int j = 1; j < COLOR; j++)
                        {

                            if (_color == j) continue;
                            min = Math.Min(min, DFS(next, _cur, j));
                        }

                        if (min == INF) min = 0;
                        ret += min;
                    }

                    return ret;
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
