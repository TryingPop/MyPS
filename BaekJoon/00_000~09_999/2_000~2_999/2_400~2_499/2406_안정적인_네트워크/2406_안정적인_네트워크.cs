using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 9
이름 : 배성훈
내용 : 안정적인 네트워크
    문제번호 : 2406번

    MST 문제다.
    1번이 고장날 때 MST가 되는 최소 비용을 찾아야 한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1689
    {

        static void Main1689(string[] args)
        {

            int n, m, x, k;
            PriorityQueue<(int s, int e, int dis), int> pq;
            (int f, int t)[] ret;

            Input();

            GetRet();

            Output();

            void Output()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                sw.Write($"{x} {k}\n");
                for (int i = 0; i < k; i++)
                {

                    sw.Write($"{ret[i].f} {ret[i].t}\n");
                }
            }

            void GetRet()
            {

                int[] group = new int[n + 1];
                int[] stk = new int[n];
                for (int i = 1; i <= n; i++)
                {

                    group[i] = i;
                }

                ret = new (int f, int t)[n - 1];

                x = 0;
                k = 0;
                while (pq.Count > 0)
                {

                    var node = pq.Dequeue();
                    int f = Find(node.s);
                    int t = Find(node.e);

                    if (f == t) continue;

                    Union(f, t);

                    if (node.dis == 0) continue;
                    x += node.dis;
                    ret[k++] = (node.s, node.e);
                }

                void Union(int _f, int _t)
                {

                    int min = _f < _t ? _f : _t;
                    int max = _f < _t ? _t : _f;

                    group[max] = min;
                }

                int Find(int _chk)
                {

                    int len = 0;

                    while (_chk != group[_chk])
                    {

                        stk[len++] = _chk;
                        _chk = group[_chk];
                    }

                    while (len-- > 0)
                    {

                        group[stk[len]] = _chk;
                    }

                    return _chk;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                pq = new(n * n + m);
                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    if (f == 1 || t == 1) continue;
                    pq.Enqueue((f, t, 0), 0);
                }

                for (int i = 1; i <= n; i++)
                {

                    for (int j = 1; j <= n; j++)
                    {

                        int dis = ReadInt();
                        if (j <= i || i == 1) continue;


                        pq.Enqueue((i, j, dis), dis);
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
}
