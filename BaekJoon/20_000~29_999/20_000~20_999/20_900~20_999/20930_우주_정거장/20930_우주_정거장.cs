using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 24
이름 : 배성훈
내용 : 우주 정거장
    문제번호 : 20930번

    분리 집합, 스위핑 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1728
    {

        static void Main1728(string[] args)
        {

            string Y = "1\n";
            string N = "0\n";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n, q;
            int[] group, stk;

            Input();

            while (q-- > 0)
            {

                int f = Find(ReadInt());
                int t = Find(ReadInt());

                sw.Write(f == t ? Y : N);
            }

            void Input()
            {

                n = ReadInt();
                q = ReadInt();

                (int s, int e, int idx)[] x = new (int s, int e, int idx)[n], y = new (int r, int c, int idx)[n];
                group = new int[n + 1];
                stk = new int[n];

                for (int i = 0, j = 1; i < n; i++, j++)
                {

                    int sX = ReadInt();
                    int sY = ReadInt();
                    int eX = ReadInt();
                    int eY = ReadInt();

                    if (eX < sX)
                    {

                        int temp = sX;
                        sX = eX;
                        eX = temp;
                    }

                    if (eY < sY)
                    {

                        int temp = sY;
                        sY = eY;
                        eY = temp;
                    }

                    x[i] = (sX, eX, j);
                    y[i] = (sY, eY, j);

                    group[j] = j;
                }

                Sweeping(x);
                Sweeping(y);

                void Sweeping((int s, int e, int idx)[] _arr)
                {

                    Array.Sort(_arr, (a, b) => a.s.CompareTo(b.s));

                    int max = _arr[0].e;
                    int curIdx = _arr[0].idx;
                    for (int i = 1; i < n; i++)
                    {

                        if (max < _arr[i].s)
                        {

                            max = _arr[i].e;
                            curIdx = _arr[i].idx;
                        }
                        else
                        {

                            max = Math.Max(max, _arr[i].e);
                            Union(curIdx, _arr[i].idx);
                        }
                    }
                }

                void Union(int _f, int _t)
                {

                    int f = Find(_f);
                    int t = Find(_t);

                    if (t < f)
                    {

                        int temp = f;
                        f = t;
                        t = temp;
                    }

                    group[t] = f;
                }
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
                    bool positive = c != '-';
                    ret = positive ? c - '0' : 0;

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n') 
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
