using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 4
이름 : 배성훈
내용 : 뉴스 전하기
    문제번호 : 1135번

    트리, 정렬, 그리디 문제다.
    우선 시간이 많이 걸리는것에 먼저 전화를 거는 것이 좋다.
    왜냐하면 A > B라할 때, A에 전화를 먼저하고, B에 전화를 하면 A >= B + 1초이므로
    A가 끝나면 B는 이미 끝나있을 수 있기 때문이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1514
    {

        static void Main1514(string[] args)
        {

            int n;
            List<int>[] edge;
            Input();

            GetRet();

            void GetRet()
            {

                // 가장 빠르게 받는 시간
                int[] dp = new int[n];
                int ret = DFS();

                Console.Write(ret);
                int DFS(int _cur = 0, int _time = 0)
                {

                    ref int ret = ref dp[_cur];
                    ret = _time;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        // 동시간에 전화를 건다고 가정
                        DFS(edge[_cur][i], _time + 1);
                    }

                    // 가장 오래 걸리는 시간부터 먼저 전화를 건다.
                    edge[_cur].Sort((x, y) => dp[y].CompareTo(dp[x]));
                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        // 전화하는데 걸리는 시간 갱신
                        int chk = dp[edge[_cur][i]] + i;
                        ret = Math.Max(ret, chk);
                    }

                    return ret;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());

                edge = new List<int>[n];
                for (int i = 0; i < n; i++)
                {

                    edge[i] = new();
                }

                ReadInt();

                for (int i = 1; i < n; i++)
                {

                    edge[ReadInt()].Add(i);
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
                        if (c == ' ' || c == '\n') return true;

                        bool positive = c != '-';
                        ret = positive ? c - '0' : 0;

                        while((c = sr.Read()) != -1 && c != ' ' && c!= '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        ret = positive ? ret : -ret;
                        return false;
                    }
                }
            }
        }
    }
}
