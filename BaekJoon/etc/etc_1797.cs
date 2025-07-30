using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 30
이름 : 배성훈
내용 : 효율적으로 과제하기
    문제번호 : 29818번

    dp, 비트마스킹 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1797
    {

        static void Main1797(string[] args)
        {

            int n;
            (int t, int d, int p)[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int[] dp = new int[1 << n];

                // 연산으로 찾을 수 있지만 시간이 오래 걸린다.
                // 이에 따로 배열에 저장
                int[] time = new int[1 << n];

                for (int curState = 0; curState < dp.Length; curState++)
                {


                    for (int i = 0; i < n; i++)
                    {

                        // 선택한 경우 중복 선택 막기
                        if ((curState & (1 << i)) != 0) continue;
                        int nextState = curState | (1 << i);
                        int nextTime = arr[i].t + time[curState];
                        int addScore = GetScore(i, nextTime);

                        time[nextState] = nextTime;
                        dp[nextState] = Math.Max(dp[nextState], dp[curState] + addScore);
                    }
                }

                int ret1 = 0, ret2 = 0;
                for (int i = 0; i < dp.Length; i++)
                {

                    if (ret1 < dp[i])
                    {

                        ret1 = dp[i];
                        ret2 = time[i];
                    }
                    else if (ret1 == dp[i]) ret2 = Math.Min(ret2, time[i]);
                }

                Console.Write($"{ret1} {ret2}");

                int GetScore(int _prob, int _time)
                {

                    if (arr[_prob].d + 24 < _time) return 0;
                    else if (arr[_prob].d < _time) return arr[_prob].p / 2;
                    else return arr[_prob].p;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new (int t, int d, int p)[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = (ReadInt(), ReadInt(), ReadInt());
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
}
