using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 7
이름 : 배성훈
내용 : 결전의 금요일
    문제번호 : 25194번

    dp, 브루트포스 문제다.
    배낭 문제로 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1619
    {

        static void Main1619(string[] args)
        {

            int WEEK = 7;

            int n;
            int[] cnt;

            Input();

            GetRet();

            void GetRet()
            {

                for (int i = 1; i < WEEK; i++)
                {

                    if (cnt[i] >= WEEK)
                    {

                        Console.Write("YES");
                        return;
                    }
                }

                bool[] prev = new bool[WEEK], next = new bool[WEEK];
                prev[0] = true;

                for (int i = 1; i < WEEK; i++)
                {

                    int d = 0;
                    for (int j = 0; j < cnt[i]; j++)
                    {

                        d = (d + i) % WEEK;
                        
                        for (int k = 0; k < WEEK; k++)
                        {

                            if (!prev[k]) continue;
                            int chk = k + d;
                            if (chk >= WEEK) chk %= WEEK;

                            next[chk] = true;
                        }
                    }

                    for (int j = 0; j < WEEK; j++)
                    {

                        if (!next[j]) continue;
                        prev[j] = true;
                        next[j] = false;
                    }
                }

                Console.Write(prev[4] ? "YES" : "NO");
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                cnt = new int[WEEK];
                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt() % WEEK;
                    cnt[cur]++;
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
