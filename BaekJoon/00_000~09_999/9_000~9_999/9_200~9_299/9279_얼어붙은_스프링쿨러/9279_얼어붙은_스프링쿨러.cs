using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 31
이름 : 배성훈
내용 : 얼어붙은 스프링쿨러
    문제번호 : 9279번

    트리에서의 dp 문제다.
    입력되는 그래프를 보면 트리이다.
    DFS로 탐색하며 찾아갔다.
*/

namespace BaekJoon.etc
{
    internal class etc_1503
    {

        static void Main1503(string[] args)
        {

            int INF = 2_000_000;
            int MAX = 1_000;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int[] dp = new int[MAX + 1];
            List<(int dst, int pow)>[] edge = new List<(int dst, int pow)>[MAX + 1];
            int n, c;

            Init();

            while (Input())
            {

                GetRet();
            }

            void GetRet()
            {

                int ret = DFS(c, -1, INF);
                sw.Write($"{ret}\n");

                int DFS(int _cur, int _prev, int _pow)
                {

                    ref int ret = ref dp[_cur];
                    if (ret != INF) return ret;

                    ret = 0;
                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i].dst;
                        if (next == _prev) continue;
                        ret += DFS(next, _cur, edge[_cur][i].pow);
                    }

                    if (ret == 0 || _pow < ret) ret = _pow;
                    return ret;
                }
            }

            bool Input()
            {

                string chk = sr.ReadLine();
                if (string.IsNullOrEmpty(chk)) return false;
                string[] temp = chk.Split();
                n = int.Parse(temp[0]);
                c = int.Parse(temp[1]);

                for (int i = 1; i <= n; i++)
                {

                    edge[i].Clear();
                    dp[i] = INF;
                }

                for (int i = 1; i < n; i++)
                {

                    temp = sr.ReadLine().Split();
                    int f = int.Parse(temp[0]);
                    int t = int.Parse(temp[1]);
                    int p = int.Parse(temp[2]);

                    edge[f].Add((t, p));
                    edge[t].Add((f, p));
                }

                return true;
            }

            void Init()
            {

                n = -1;
                c = -1;
                for (int i = 1; i <= MAX; i++)
                {

                    edge[i] = new();
                }
            }
        }
    }
}
