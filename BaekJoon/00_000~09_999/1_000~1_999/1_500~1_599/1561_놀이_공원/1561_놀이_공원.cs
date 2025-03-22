using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 30
이름 : 배성훈
내용 : 놀이공원
    문제번호 : 1561번

    이분 탐색 문제다.
    아이디어는 다음과 같다. n명이 다 타는 먼저 시간을 찾는다.
    그리고 몇 번째에 n명이 다 타는지 확인한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1138
    {

        static void Main1138(string[] args)
        {

            int n, m;
            int[] timer;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                if (n < m)
                {

                    Console.Write(n);
                    return;
                }

                // n명이 다 타는데 걸리는 시간 찾기
                long time = BinarySearch();
                long idx = 0;

                // 해당 시간에 타는 놀이기구 확인
                int[] rets = new int[m];
                int len = 0;
                for (int i = 0; i < m; i++)
                {

                    idx += 1 + time / timer[i];
                    if (time % timer[i] == 0) rets[len++] = i;
                }

                // 이제 어느 놀이기구에서 n명이 타는지 확인
                idx = len + n - idx - 1;

                Console.Write(rets[idx] + 1);
            }

            long BinarySearch()
            {

                long l = 0;
                long r = 60_000_000_000;
                while (l <= r)
                {

                    long mid = (l + r) >> 1;
                    if (ChkCnt(mid) < n) l = mid + 1;
                    else r = mid - 1;
                }

                return l;
            }

            long ChkCnt(long _t)
            {

                long ret = 0;
                for (int i = 0; i < m; i++)
                {

                    ret += 1 + _t / timer[i];
                }

                return ret;
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                timer = new int[m];
                for (int i = 0; i < m; i++)
                {

                    timer[i] = ReadInt();
                }

                sr.Close();

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
}
