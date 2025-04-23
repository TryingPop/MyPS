using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 23
이름 : 배성훈
내용 : 동까뚱뽭 게임
    문제번호 : 32645번

    트리, dp, 게임 이론 문제다.
    아이디어는 다음과 같다.

    각 노드는 진입하면 이기는 경우 true, 지는 경우 false로 놓을 수 있다.
    이후 리프부터 확인하면서 어느 경우 true인지 false인지 확인했다.
    true는 리프노드거나 자식들이 모두 false인 경우다.
    반면 false는 자식 중 ture가 있는 경우다.

    이렇게 규칙을 찾고 true일 때 누가 이기는지 확인해 제출했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1567
    {

        static void Main1567(string[] args)
        {

            int n;
            List<int>[] edge;
            bool[] ret;

            Input();

            GetRet();

            Output();

            void Output()
            {

                string T = "uppercut\n";
                string F = "donggggas\n";
                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0; i < n; i++)
                {

                    sw.Write(ret[i] ? T : F);
                }
            }

            void GetRet()
            {

                ret = new bool[n];

                DFS();

                bool DFS(int _cur = 0, int _prev = -1)
                {

                    ret[_cur] = true;
                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (next == _prev) continue;

                        if (DFS(next, _cur)) ret[_cur] = false;
                    }

                    return ret[_cur];
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                edge = new List<int>[n];

                for (int i = 0; i < n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 1; i < n; i++)
                {

                    int f = ReadInt() - 1;
                    int t = ReadInt() - 1;

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

                        while(( c= sr.Read()) != -1 && c != ' ' && c != '\n')
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
