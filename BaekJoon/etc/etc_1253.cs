using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 6
이름 : 배성훈
내용 : 어떤 우유의 배달목록 (Hard)
    문제번호 : 23836번

    현재 로직에 수정이 필요하다.!
*/

namespace BaekJoon.etc
{
    internal class etc_1253
    {

        static void Main1253(string[] args)
        {

            StreamReader sr;
            int S, E, n;
            List<int>[] edge;
            long[][] seg;
            (int idx, int head, int parent, int dep)[] chain;
            (int f, int t)[] stk;

            Solve();
            void Solve()
            {

                Input();

                SetHLD();

                GetRet();
            }

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                stk = new (int f, int t)[n];
                int m = ReadInt();

                while(m-- > 0)
                {

                    int op = ReadInt();

                    if (op == 1)
                    {

                        int u = ReadInt();
                        int v = ReadInt();

                        QueryUpdate(u, v);
                    }
                    else
                    {

                        int x = ReadInt();
                        sw.Write($"{GetVal(S, E, chain[x].idx)}\n");
                    }
                }

                sr.Close();

                void QueryUpdate(int _from, int _to)
                {

                    int f = _from;
                    int t = _to;
                    int s = -1;

                    int len = 0;
                    while (chain[f].dep < chain[t].dep)
                    {

                        stk[len++] = (chain[chain[t].head].idx, chain[t].idx);
                        t = chain[t].parent;
                    }

                    while (chain[f].dep > chain[t].dep)
                    {

                        Update(S, E, chain[chain[f].head].idx, chain[f].idx, s, -1);
                        s += chain[f].idx - chain[chain[f].head].idx + 1;
                        f = chain[f].parent;
                    }

                    while (chain[f].head != chain[t].head)
                    {

                        Update(S, E, chain[chain[f].head].idx, chain[f].idx, s, -1);
                        s += chain[f].idx - chain[chain[f].head].idx + 1;
                        f = chain[f].parent;

                        stk[len++] = (chain[chain[t].head].idx, chain[t].idx);
                        t = chain[t].parent;
                    }

                    if (chain[f].idx < chain[t].idx)
                    {

                        Update(S, E, chain[f].idx, chain[t].idx, s, 1);
                        s += chain[t].idx - chain[f].idx + 1;
                    }
                    else
                    {

                        Update(S, E, chain[t].idx, chain[f].idx, s, -1);
                        s += chain[f].idx - chain[t].idx + 1;
                    }

                    while (len-- > 0)
                    {

                        (int head, int tail) = stk[len];

                        Update(S, E, head, tail, s, 1);
                        s += tail - head + 1;
                    }
                }
            }

            void Update(int _s, int _e, int _chkS, int _chkE, long _val, int _op, int _idx = 0)
            {

                if (_chkE < _s || _e < _chkS) return;
                else if (_chkS <= _s && _e <= _chkE)
                {

                    if (_op == 1)
                    {

                        seg[_idx][0] += _val + _s - _chkS + 1;
                        seg[_idx][1]++;
                    }
                    else
                    {

                        seg[_idx][2] += _val + _chkE - _e + 1;
                        seg[_idx][3]++;
                    }

                    return;
                }

                int mid = (_s + _e) >> 1;
                Update(_s, mid, _chkS, _chkE, _val, _op, _idx * 2 + 1);
                Update(mid + 1, _e, _chkS, _chkE, _val, _op, _idx * 2 + 2);
            }

            long GetVal(int _s, int _e, int _chk, int _idx = 0)
            {

                if (_chk < _s || _e < _chk) return 0;

                long val = (seg[_idx][0] + seg[_idx][2]);
                if (_s == _e) return val;

                val += seg[_idx][1] * (_chk - _s) + seg[_idx][3] * (_e - _idx);

                int mid = (_s + _e) >> 1;
                return GetVal(_s, mid, _chk, _idx * 2 + 1) + GetVal(mid + 1, _e, _chk, _idx * 2 + 2) + val;
            }

            void SetHLD()
            {

                SetWeight();

                SetSeg();

                SetChain();

                void SetChain()
                {

                    chain = new (int idx, int head, int parent, int dep)[n + 1];
                    chain[1].head = 1;
                    int cnt = 1;
                    DFS();

                    void DFS(int _cur = 1, int _prev = 0, int _dep = 1)
                    {

                        chain[_cur].dep = _dep;
                        chain[_cur].idx = cnt++;

                        for (int i = 0; i < edge[_cur].Count; i++)
                        {

                            int next = edge[_cur][i];
                            if (next == _prev) continue;

                            if (i == 0)
                            {

                                chain[next].head = chain[_cur].head;
                                chain[next].parent = chain[_cur].parent;

                                DFS(next, _cur, _dep);
                            }
                            else
                            {

                                chain[next].head = next;
                                chain[next].parent = _cur;

                                DFS(next, _cur, _dep + 1);
                            }
                        }
                    }
                }

                void SetSeg()
                {

                    int log = (int)(Math.Ceiling(Math.Log2(n) + 1e-9)) + 1;
                    seg = new long[1 << log][];
                    for (int i = 0; i < seg.Length; i++)
                    {

                        seg[i] = new long[4];
                    }

                    S = 1;
                    E = n;
                }

                void SetWeight()
                {

                    int[] weight = new int[n + 1];
                    DFS();
                    int DFS(int _cur = 1, int _prev = 0)
                    {

                        ref int ret = ref weight[_cur];
                        ret = 1;

                        for (int i = 0; i < edge[_cur].Count; i++)
                        {

                            int next = edge[_cur][i];
                            if (next == _prev) continue;
                            ret += DFS(next, _cur);

                            if (weight[edge[_cur][0]] < weight[next] || edge[_cur][0] == _prev)
                            {

                                int temp = edge[_cur][0];
                                edge[_cur][0] = next;
                                edge[_cur][i] = temp;
                            }
                        }

                        return ret;
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                edge = new List<int>[n + 1];
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
