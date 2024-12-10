using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 10
이름 : 배성훈
내용 : 수열과 쿼리 16
    문제번호 : 14428번

    세그먼트 트리 문제다.
    하나의 원소의 값 변경과, 구간에서 가장 작은 값을 찾아야 한다.
    그래서 세그먼트 트리로 자료구조를 잡았다.
    인덱스를 찾아야 하기에 인덱스와 값을 저장하는 2차원 세그먼트 트리로 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1171
    {

        static void Main1171(string[] args)
        {

            StreamReader sr;
            (int val, int idx) INF = (1_000_000_001, 100_001);
            int n;
            (int val, int idx)[] seg;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int len = ReadInt();

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                for (int i = 0; i < len; i++)
                {

                    int op = ReadInt();
                    int f = ReadInt();
                    int t = ReadInt();

                    if (op == 1)
                        Update(1, n, f, t);
                    else
                    {

                        var ret = GetVal(1, n, f, t);
                        sw.Write($"{ret.idx}\n");
                    }
                }

                sr.Close();
                sw.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput());
                n = ReadInt();
                int size = 1 << (2 + (int)(Math.Log2(n) - 1e-9));

                seg = new (int val, int idx)[size];
                for (int i = 1; i <= n; i++)
                {

                    Update(1, n, i, ReadInt());
                }
            }

            void Update(int _s, int _e, int _chk, int _val, int _idx = 0)
            {

                if (_s == _e)
                {

                    seg[_idx] = (_val, _s);
                    return;
                }

                int mid = (_s + _e) >> 1;
                if (mid < _chk) Update(mid + 1, _e, _chk, _val, _idx * 2 + 2);
                else Update(_s, mid, _chk, _val, _idx * 2 + 1);

                if (seg[_idx * 2 + 1].val <= seg[_idx * 2 + 2].val)
                    seg[_idx] = seg[_idx * 2 + 1];
                else seg[_idx] = seg[_idx * 2 + 2];
            }

            (int val, int idx) GetVal(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                if (_e < _chkS || _chkE < _s) return INF;
                else if (_chkS <= _s && _e <= _chkE) return seg[_idx];

                int mid = (_s + _e) >> 1;

                var l = GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1);
                var r = GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2);

                if (l.val <= r.val) return l;
                else return r;
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

                    while((c = sr.Read() ) != -1 && c != ' ' && c != '\n')
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
