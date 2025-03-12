using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 12
이름 : 배성훈
내용 : 성대나라의 물탱크
    문제번호 : 18227번

    세그먼트 트리 문제다.
    A에 물이 차면 해당 노드의 조상들에 물이 차는 횟수가 1씩 누적된다.
    오일러 경로 테크닉을 쓰면 자식들의 노드 번호를 알 수 있다.
    물이 찼다는 것은 자신의 번호에 하되, 얼마만큼 물이 찼는지 확인하는 것은
    자식 범위에 있는 물찬 횟수를 누적하는 것과 같다.
*/

namespace BaekJoon.etc
{
    internal class etc_1400
    {

        static void Main1400(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n, c;
            int[] seg;
            (int s, int e, int dep)[] chain;

            Input();

            SetSeg();

            GetRet();

            void GetRet()
            {

                int q = ReadInt();

                while (q-- > 0)
                {

                    int op = ReadInt();
                    int idx = ReadInt();

                    if (op == 1)
                        Update(1, n, chain[idx].s);
                    else
                    {

                        long visit = GetVal(1, n, chain[idx].s, chain[idx].e);
                        sw.Write(chain[idx].dep * visit);
                        sw.Write('\n');
                    }
                }

                void Update(int _S, int _E, int _chk, int _idx = 0)
                {

                    if (_S == _E) 
                    { 
                        
                        seg[_idx]++;
                        return;
                    }

                    int mid = (_S + _E) >> 1;
                    if (_chk <= mid) Update(_S, mid, _chk, _idx * 2 + 1);
                    else Update(mid + 1, _E, _chk, _idx * 2 + 2);

                    seg[_idx] = seg[_idx * 2 + 1] + seg[_idx * 2 + 2];
                }

                long GetVal(int _S, int _E, int _chkS, int _chkE, int _idx = 0)
                {

                    if (_chkS <= _S && _E <= _chkE) return seg[_idx];
                    else if (_chkE < _S || _E < _chkS) return 0L;

                    int mid = (_S + _E) >> 1;

                    return GetVal(_S, mid, _chkS, _chkE, _idx * 2 + 1) 
                        + GetVal(mid + 1, _E, _chkS, _chkE, _idx * 2 + 2);
                }
            }

            void SetSeg()
            {

                int log = n == 1 ? 1 : (int)(Math.Log2(n - 1) + 1e-9) + 2;
                seg = new int[1 << log];
            }

            void Input()
            {

                n = ReadInt();
                c = ReadInt();

                List<int>[] edge = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 1; i < n; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    edge[f].Add(t);
                    edge[t].Add(f);
                }

                chain = new (int s, int e, int dep)[n + 1];
                int idx = 1;

                EulerDFS(c, -1);

                void EulerDFS(int _cur, int _prev, int _depth = 1)
                {

                    chain[_cur].s = idx++;
                    chain[_cur].dep = _depth;

                    for(int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (next == _prev) continue;
                        EulerDFS(next, _cur, _depth + 1);
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

                    ret = c - '0';

                    while((c = sr.Read()) != -1 &&  c != '\n' && c != ' ')
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
