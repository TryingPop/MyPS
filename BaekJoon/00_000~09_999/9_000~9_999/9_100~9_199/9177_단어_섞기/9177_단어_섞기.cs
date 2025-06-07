using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 12
이름 : 배성훈
내용 : 단어 섞기
    문제번호 : 9177번

    dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1179
    {

        static void Main1179(string[] args)
        {

            int LEN = 200;
            string HEAD = "Data set ";
            string TAIL = ": ";
            string YES = "yes\n";
            string NO = "no\n";

            StreamReader sr;
            StreamWriter sw;

            string f, s, t;
            int n;
            int[][] dp;

            Solve();
            void Solve()
            {

                Init();

                int t = int.Parse(sr.ReadLine());
                for (int i = 1; i <= t; i++)
                {

                    Input();

                    int ret = GetRet();

                    sw.Write($"{HEAD}{i}{TAIL}");
                    if (ret == 1) sw.Write(YES);
                    else sw.Write(NO);
                }

                sr.Close();
                sw.Close();
            }

            int GetRet()
            {

                return DFS();
                int DFS(int _f = 0, int _s = 0, int _idx = 0)
                {

                    ref int ret = ref dp[_f][_s];
                    // 이미 탐색한 경로
                    if (ret != -1) return ret;
                    // 목표지점 도달
                    else if (_idx == t.Length) return 1;
                    
                    // 가능한 경로 탐색
                    ret = 0;
                    if (_f < f.Length && f[_f] == t[_idx]) ret = Math.Max(ret, DFS(_f + 1, _s, _idx + 1));
                    if (_s < s.Length && s[_s] == t[_idx]) ret = Math.Max(ret, DFS(_f, _s + 1, _idx + 1));

                    return ret;
                }
            }

            void Input()
            {

                string[] temp = sr.ReadLine().Split();
                f = temp[0];
                s = temp[1];
                t = temp[2];

                for (int i = 0; i <= f.Length; i++)
                {

                    for (int j = 0; j <= s.Length; j++)
                    {

                        dp[i][j] = -1;
                    }
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                dp = new int[LEN + 1][];
                for (int i = 0; i <= LEN; i++)
                {

                    dp[i] = new int[LEN + 1];
                }
            }
        }
    }
}
