using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaekJoon.etc
{
    internal class etc_1767
    {

        static void Main1767(string[] args)
        {

            // 11062번 현재 DP 방법이 제대로 작동 X
            // 내일 다시 알아보자!
            int MAX = 1_000;
            int NOT_VISIT = -1;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int[][][] dp = new int[MAX + 1][][];
            int[] cards = new int[MAX + 1];

            for (int i = 0; i <= MAX; i++)
            {

                dp[i] = new int[MAX + 1][];
                for (int j = 0; j <= MAX; j++)
                {

                    dp[i][j] = new int[2];
                }
            }

            int t = ReadInt();

            while (t-- > 0)
            {

                int n = ReadInt();

                Input();


                sw.Write('\n');

                void Input()
                {

                    for (int i = 1; i <= n; i++)
                    {

                        cards[i] = ReadInt();
                        for (int j = 1; j <= n; j++)
                        {

                            Array.Fill(dp[i][j], NOT_VISIT);
                        }
                    }
                }

                int DFS(int _f, int _t, int _add)
                {

                    ref int ret = ref dp[_f][_t][_add];
                    if (ret != NOT_VISIT) return ret;
                    else if (_f == _t) return ret = _add == 1 ? cards[_f] : 0;

                    int f = DFS(_f + 1, _t, _add ^ 1);
                    int t = DFS(_f, _t - 1, _add ^ 1);

                    if (f < t)
                    {

                        ret = _add == 1 ? cards[_f] : 0;
                        ret += f;
                    }
                    else
                    {

                        ret = _add == 1 ? cards[_t] : 0;
                        ret += t;
                    }

                    return ret;
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
