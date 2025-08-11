using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 11
이름 : 배성훈
내용 : 카드 게임
    문제번호 : 11062번
*/

namespace BaekJoon.etc
{
    internal class etc_1767
    {

        static void Main1767(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int NOT_VISIT = -1;
            int[][] dp;
            int n;
            int[] arr;

            SetArr();
            int t = ReadInt();

            while (t-- > 0)
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                sw.Write(DFS(1, n));
                sw.Write('\n');
                int DFS(int _s, int _e, int _add = 1)
                {

                    ref int ret = ref dp[_s][_e];
                    if (_s > _e) ret = 0;
                    if (ret != NOT_VISIT) return ret;

                    if (_add == 1)
                    {

                        // 근우 턴
                        ret = Math.Max(arr[_s] + DFS(_s + 1, _e, 0), arr[_e] + DFS(_s, _e - 1, 0));
                    }
                    else
                    {

                        // 명우 턴
                        ret = Math.Min(DFS(_s + 1, _e, 1), DFS(_s, _e - 1, 1));
                    }

                    return ret;
                }
            }

            void SetArr()
            {

                int MAX_N = 1_000;
                arr = new int[MAX_N + 2];
                dp = new int[MAX_N + 2][];
                for (int i = 0; i < dp.Length; i++)
                {

                    dp[i] = new int[MAX_N + 2];
                }
            }

            void Input()
            {

                n = ReadInt();
                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
                    Array.Fill(dp[i], NOT_VISIT, 0, n + 2);
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
