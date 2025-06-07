using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 30
이름 : 배성훈
내용 : 매수
    문제번호 : 9368번

    dp, 비트마스킹 문제다.
    dp[i][j] = val를
    i명 매수해야 하고, j를 매수 시도한 상태라 할 때 확률 val를 담게 설정하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1600
    {

        static void Main1600(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t, n, c, m;
            (int money, int per)[] human;

            double[][] dp;

            Init();

            while (t-- > 0)
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                sw.Write($"{DFS(c, 0, m):0.#########}\n");

                double DFS(int _r, int _state, int _money)
                {

                    if (_r == 0) return 1.0;
                    ref double ret = ref dp[_r][_state];

                    if (ret >= 0) return ret;
                    ret = 0;

                    for (int i = 0; i < n; i++)
                    {

                        if (ChkState(i) && _money >= human[i].money)
                        {

                            double per = human[i].per / 100.0;
                            double success = per * DFS(_r - 1, _state | (1 << i), _money - human[i].money);
                            double fail = (1 - per) * DFS(_r, _state | (1 << i), _money - human[i].money);

                            ret = Math.Max(ret, success + fail);
                        }
                    }

                    return ret;

                    bool ChkState(int _i)
                    {

                        return (_state & (1 << _i)) == 0;
                    }
                }
            }

            void Init()
            {

                int N = 16;

                dp = new double[N + 1][];
                for (int i = 0; i <= N; i++)
                {

                    dp[i] = new double[1 << N];
                }

                human = new (int money, int per)[N];

                t = ReadInt();
            }

            void Input()
            {

                n = ReadInt();
                c = ReadInt();
                m = ReadInt();

                for (int i = 0; i <= c; i++)
                {

                    Array.Fill(dp[i], -1.0, 0, 1 << n);
                }

                for (int i = 0; i < n; i++)
                {

                    int money = ReadInt();
                    int per = ReadInt();

                    human[i] = (money, per);
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
