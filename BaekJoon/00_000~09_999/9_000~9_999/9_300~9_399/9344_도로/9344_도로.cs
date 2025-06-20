using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 20
이름 : 배성훈
내용 : 도로
    문제번호 : 9344번

    MST 문제다.
    최소 비용으로 우선순위 큐를 설정하되
    비용이 같은 경우 p - q 간선을 앞서게 하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1716
    {

        static void Main1716(string[] args)
        {

            string Y = "YES\n";
            string N = "NO\n";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n, m, p, q;
            PriorityQueue<(int u, int v, int w), int> pq;
            int[] group, stk;

            Init();

            int t = ReadInt();
            while (t-- > 0)
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                bool ret = false;
                while (pq.Count > 0)
                {

                    var node = pq.Dequeue();

                    Union(node.u, node.v);
                }

                sw.Write(ret ? Y : N);

                void Union(int _f, int _t)
                {

                    if (_t < _f)
                    {

                        int temp = _f;
                        _f = _t;
                        _t = temp;
                    }

                    int f = Find(_f);
                    int t = Find(_t);

                    if (f == t) return;

                    if (_f == p && _t == q)
                        ret = true;

                    if (t < f)
                    {

                        int temp = f;
                        f = t;
                        t = temp;
                    }

                    group[t] = f;
                }

                int Find(int _chk)
                {

                    int len = 0;
                    while (group[_chk] != _chk)
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

            void Init()
            {

                int MAX_M = 20_000;
                int MAX_N = 10_000;

                p = 0;
                q = 0;

                Comparer<(int u, int v, int w)>.Create((x, y) =>
                {

                    int ret = x.w.CompareTo(y.w);
                    if (ret == 0)
                    {

                        if (x.u == p && x.v == q) ret = -1;
                        else if (y.u == p && y.v == q) ret = 1;
                    }

                    return ret;
                });

                pq = new(MAX_M);
                group = new int[MAX_N + 1];
                stk = new int[MAX_N];
            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();
                p = ReadInt();
                q = ReadInt();

                if (q < p)
                {

                    int temp = p;
                    p = q;
                    q = temp;
                }

                for (int i = 1; i <= n; i++)
                {

                    group[i] = i;
                }

                for (int i = 0; i < m; i++)
                {

                    int u = ReadInt();
                    int v = ReadInt();
                    if (v < u)
                    {

                        int temp = u;
                        u = v;
                        v = temp;
                    }
                    int w = ReadInt();

                    pq.Enqueue((u, v, w), w);
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
