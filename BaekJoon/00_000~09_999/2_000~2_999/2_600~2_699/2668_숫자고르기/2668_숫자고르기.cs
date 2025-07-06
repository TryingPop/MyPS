using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 6
이름 : 배성훈
내용 : 숫자고르기
    문제번호 : 2668번

    사이클을 찾는 그래프 이론, 그래프 탐색 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1749
    {

        static void Main1749(string[] args)
        {

            int n;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                bool[] visit = new bool[n + 1];
                bool[] ret = new bool[n + 1];
                int[] stk = new int[n];

                int cnt = 0;
                for (int i = 1; i <= n; i++)
                {

                    if (visit[i]) continue;
                    FindCycle(i);
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                sw.Write($"{cnt}\n");
                for (int i = 1; i <= n; i++)
                {

                    if (!ret[i]) continue;
                    sw.Write($"{i}\n");
                }

                void FindCycle(int _s)
                {

                    int e = _s;

                    int len = 0;
                    while (!visit[e])
                    {

                        visit[e] = true;
                        stk[len++] = e;
                        e = arr[e];
                    }

                    int chk = -1;
                    for (int i = 0; i < len; i++)
                    {

                        if (e != stk[i]) continue;
                        chk = i;
                        break;
                    }

                    if (chk == -1) return;

                    for (int i = chk; i < len; i++)
                    {

                        cnt++;
                        ret[stk[i]] = true;
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                arr = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
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
