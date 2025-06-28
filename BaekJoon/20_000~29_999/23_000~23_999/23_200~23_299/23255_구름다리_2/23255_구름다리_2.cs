using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 28
이름 : 배성훈
내용 : 구름다리 2
    문제번호 : 23255번

    그리디 문제다.
    가능한 가장 낮은 점수를 부여하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1737
    {

        static void Main1737(string[] args)
        {

            int n, m;
            List<int>[] edge;
            int[] ret;
            Input();

            GetRet();

            Output();

            void Output()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                for (int i = 1; i <= n; i++)
                {

                    sw.Write($"{ret[i]} ");
                }
            }

            void GetRet()
            {

                PriorityQueue<int, int> pq = new(m);
                ret = new int[n + 1];
                ret[1] = 1;

                for (int i = 2; i <= n; i++)
                {

                    int cur = 1;
                    for (int j = 0; j < edge[i].Count; j++)
                    {

                        int prev = edge[i][j];
                        pq.Enqueue(ret[prev], ret[prev]);
                    }

                    while (pq.Count > 0)
                    {

                        int node = pq.Dequeue();
                        if (cur == node) cur++;
                    }

                    ret[i] = cur;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                edge = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    if (t < f)
                    {

                        int temp = f;
                        f = t;
                        t = temp;
                    }

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
