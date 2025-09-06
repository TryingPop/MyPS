using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 2
이름 : 배성훈
내용 : k개 트리 노드에서 사과를 최대로 수확하기
    문제번호 : 25691번

    비트마스킹, 브루트포스, 트리 문제다.
    n <= 17이므로 2^n x n < 230만으로 유효하다.
    그래서 브루트포스로 모든 경우를 찾아서 확인해서 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1857
    {

        static void Main1857(string[] args)
        {

            // 25691 - k개 트리 노드에서 사과를 최대로 수확하기
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            int k = ReadInt();
            int[] parent = new int[n];

            Array.Fill(parent, -1);

            for (int i = 1; i < n; i++)
            {

                int p = ReadInt();
                int c = ReadInt();

                parent[c] = p;
            }

            int apple = 0;

            for (int i = 0; i < n; i++)
            {

                int cur = ReadInt();
                apple |= cur << i;
            }

            int root = -1;
            for (int i = 0; i < n; i++)
            {

                if (parent[i] != -1) continue;
                root = i;
                break;
            }


            int[] dp = new int[1 << n];
            Array.Fill(dp, -1);
            int ret = 0;

            DFS(1, 1 << root, (apple & (1 << root)) == 0 ? 0 : 1);
            Console.Write(ret);

            void DFS(int _dep, int _state, int _cnt)
            {

                if (dp[_state] != -1) return;
                dp[_state] = _cnt;

                if (_dep == k) ret = Math.Max(dp[_state], ret);
                else
                {

                    for (int i = 0; i < n; i++)
                    {

                        if ((_state & (1 << i)) != 0
                            || (((1 << parent[i]) & _state) == 0)) continue;

                        int nState = _state | (1 << i);
                        int add = ((1 << i) & apple) == 0 ? 0 : 1;
                        DFS(_dep + 1, nState, _cnt + add);
                    }
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
