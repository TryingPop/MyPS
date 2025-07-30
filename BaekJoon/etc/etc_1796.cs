using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 29
이름 : 배성훈
내용 : 중량제한
    문제번호 : 1939번

    분리집합, 다익스트라 문제다.
    mst를 이용해 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1796
    {

        static void Main1796(string[] args)
        {

            int n, m;
            PriorityQueue<(int a, int b, int c), int> pq;
            int s, e;

            Input();

            GetRet();

            void GetRet()
            {

                int INF = 1_000_000_001;
                int[] g = new int[n + 1];
                int[] ret = new int[n + 1];
                int[] stk = new int[n];

                for (int i = 1; i <= n; i++)
                {

                    g[i] = i;
                    ret[i] = INF;
                }

                while (pq.Count > 0)
                {

                    var node = pq.Dequeue();

                    int a = Find(node.a);
                    int b = Find(node.b);

                    if (a == b) continue;

                    // Union
                    int min = a < b ? a : b;
                    int max = a < b ? b : a;

                    g[max] = min;
                    ret[min] = Math.Min(ret[min], node.c);

                    if (Find(s) == Find(e)) break;
                }

                Console.Write(ret[Find(e)]);


                int Find(int _chk)
                {

                    int len = 0;

                    while (_chk != g[_chk])
                    {

                        stk[len++] = _chk;
                        _chk = g[_chk];
                    }

                    while (len-- > 0)
                    {

                        g[stk[len]] = _chk;
                    }

                    return _chk;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                pq = new(m);

                for (int i = 0; i < m; i++)
                {

                    int a = ReadInt();
                    int b = ReadInt();
                    int c = ReadInt();

                    pq.Enqueue((a, b, c), -c);
                }

                s = ReadInt();
                e = ReadInt();

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
