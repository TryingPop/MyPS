using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 12
이름 : 배성훈
내용 : 주식회사 승범이네
    문제번호 : 16404번

    세그먼트 트리, 오일러 경로 테크닉 문제다.
    HLD 알고리즘 처럼 풀면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1399
    {

        static void Main1399(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n, m;
            (int s, int e)[] chain;
            int[] seg;

            Input();

            SetSeg();

            GetRet();

            void GetRet()
            {

                while (m-- > 0)
                {

                    int op = ReadInt();

                    if (op == 1)
                    {

                        int idx = ReadInt();
                        int add = ReadInt();

                        Update(1, n, add, chain[idx].s, chain[idx].e);
                    }
                    else
                    {
                        int idx = ReadInt();
                        sw.Write(GetVal(1, n, chain[idx].s));
                        sw.Write('\n');
                    }
                }
            }

            void SetSeg()
            {

                int log = n == 1 ? 1 : (int)(Math.Log2(n) - 1e-9) + 2;
                seg = new int[1 << log];
            }

            int GetVal(int _S, int _E, int _chk, int _idx = 0)
            {

                if (_S == _E) return seg[_idx];

                int mid = (_S + _E) >> 1;
                if (_chk <= mid) return seg[_idx] + GetVal(_S, mid, _chk, _idx * 2 + 1);
                else return seg[_idx] + GetVal(mid + 1, _E, _chk, _idx * 2 + 2);
            }

            void Update(int _S, int _E, int _add, int _chkS, int _chkE, int _idx = 0)
            {

                if (_chkS <= _S && _E <= _chkE)
                {

                    seg[_idx] += _add;
                    return;
                }
                else if (_E < _chkS || _chkE < _S) return;

                int mid = (_S + _E) >> 1;
                Update(_S, mid, _add, _chkS, _chkE, _idx * 2 + 1);
                Update(mid + 1, _E, _add, _chkS, _chkE, _idx * 2 + 2);
            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();

                int[] parent = new int[n + 1];
                List<int>[] edge = new List<int>[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int t = 1; t <= n; t++)
                {

                    int f = ReadInt();
                    if (f == -1) continue;
                    edge[f].Add(t);
                }

                chain = new (int s, int e)[n + 1];
                int idx = 1;

                for (int i = 1; i <= n; i++)
                {

                    if (chain[i].s == 0) EulerDFS(i);
                }

                void EulerDFS(int _cur)
                {

                    chain[_cur].s = idx++;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        EulerDFS(edge[_cur][i]);
                    }

                    chain[_cur].e = idx - 1;
                }
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;

                    bool positive = c != '-';
                    ret = positive ? c - '0' : 0;

                    while((c = sr.Read()) != -1 && c!= ' ' && c != '\n')
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
