using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 14
이름 : 배성훈
내용 : 밤 편지
    문제번호 : 23258번

    dp, 최단 경로 문제다.
    아이디어는 다음과 같다.
    2^c인 경우 해당 경로를 지나가지 못한다.
    이는 0 ≤ a < c에 대해 ∑2^a = 2^c - 1 < 2^c 이다.
    그래서 c이하인 점들만 지날 수 있다는 의미이다.

    그리고 플로이드 워셜 알고리즘은 mid를 1부터 n까지 연결하며 최단 경로를 찾는 문제이다.
    플로이드 워셜로 최단 경로를 찾는 과정을 모두 기로갛면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1187
    {

        static void Main1187(string[] args)
        {

            int INF = 1_000_000;
            StreamReader sr;

            int n, m;
            int[][][] fw;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                for (int i = 0; i < m; i++)
                {

                    int c = ReadInt();
                    int f = ReadInt();
                    int t = ReadInt();

                    int ret = fw[c - 1][f][t];
                    if (ret == INF) ret = -1;
                    sw.Write($"{ret}\n");
                }
            }
            
            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                fw = new int[n + 1][][];
                for (int i = 0; i <= n; i++)
                {

                    fw[i] = new int[n + 1][];
                    for (int j = 1; j <= n; j++)
                    {

                        fw[i][j] = new int[n + 1];
                        Array.Fill(fw[i][j], INF);
                    }
                }

                for (int i = 1; i <= n; i++)
                {

                    for (int j = 1; j <= n; j++)
                    {

                        int cur = ReadInt();
                        if (cur == 0) cur = INF;
                        fw[0][i][j] = cur;
                    }

                    fw[0][i][i] = 0;
                }

                for (int i = 1; i <= n; i++)
                {

                    for (int j = 1; j <= n; j++)
                    {

                        for (int k = 1; k <= n; k++)
                        {

                            fw[i][j][k] = Math.Min(fw[i - 1][j][k], fw[i - 1][j][i] + fw[i - 1][i][k]);
                        }
                    }
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
