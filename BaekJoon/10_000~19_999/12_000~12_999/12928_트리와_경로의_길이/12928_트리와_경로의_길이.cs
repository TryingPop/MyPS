using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 17
이름 : 배성훈
내용 : 트리와 경로의 길이
    문제번호 : 12928번

    트리, 다이나믹 프로그래밍 문제다.
    트리를 루트와 루트의 자식을 2개를 놓는다.
    이후 루트에 자식을 i개씩 추가한다.
    그러면 i * (i + 1) / 2개씩 길이 2짜리가 추가됨을 확인할 수 있다.

    그러면 남은 설치 가능한 노드는 n - 2 - i개 이다.
    그리고 루트의 자식 중 아무에게나 n - 2 - i에서 위와 같이 j개를 추가한다.
    어느 자식에게 하던 길이 2인 간선은 동일하다.
    여기서 루트로 가는 길이 있어 따로 2개를 설치할 필요가 없다.
    해당 경우 추가되는 길이 2짜리 간선은 j * (j + 1) / 2개이다.

    그리고 루트의 자식의 자식에 추가하던, 루트의 자식에 추가하던 전체 경우는 같다.
    이렇게 남는 노드가 없게 계속해서 추가해간다.
    그래서 나오는 간선의 갯수는 가능한 경우다.
    반면 이외의 경우는 불가능하다.

    이렇게 찾아가는데 dp[n][k] = val를 val = 0은 미방문, val = 1은 가능, val = 2는 불가능으로 놓는다.
    그리고 dp[n][k] 는 남은 노드 n이고 현재 간선 k일 때의 가능여부 val로 dp를 놓아 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1413
    {

        static void Main1413(string[] args)
        {

            int n, s;
            int[][] dp;

            Input();

            GetRet();

            void GetRet()
            {

                int ret = DFS(n - 2, 0);
                if (ret == 1) Console.Write(1);
                else Console.Write(0);

                int DFS(int _n, int _k)
                {

                    if (_n == 0 && _k == s) return 1;
                    else if (_n <= 0 || _k > s) return 2;
                    else if (dp[_n][_k] > 0) return dp[_n][_k];

                    ref int ret = ref dp[_n][_k];
                    ret = 2;

                    for (int i = 1; i <= _n; i++)
                    {

                        int nk = _k + i * (i + 1) / 2;
                        if (nk > s) break;
                        ret = Math.Min(DFS(_n - i, nk), ret);
                    }

                    return ret;
                }
            }

            void Input()
            {

                string[] input = Console.ReadLine().Split();

                n = int.Parse(input[0]);
                s = int.Parse(input[1]);

                dp = new int[n + 1][];
                for (int i = 0; i <= n; i++)
                {

                    dp[i] = new int[s + 1];
                }
            }
        }
    }
}
