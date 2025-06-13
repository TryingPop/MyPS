using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 6
이름 : 배성훈
내용 : 국제 메시 기구
    문제번호 : 17429번

    HLD, lazy 세그먼트 트리, 오일러 경로 문제다.
    HLD에서 chain 설정하는데 오일러 경로 테크닉만 추가한 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1255
    {

        static void Main1255(string[] args)
        {

            StreamReader sr;

            int n, m;
            List<int>[] edge;
            (int val, bool lazy, int add, int mul)[] seg;
            // idx : hld로 새로 붙은 idx
            // head : 머리, parent : 부모, dep : hld의 깊이, subEnd : 오일러 테크닉으로 찾은 서브 트리의 끝
            (int idx, int head, int parent, int dep, int subEnd)[] chain;

            Solve();
            void Solve()
            {

                Input();

                SetChild();

                SetSeg();

                SetChain();

                GetRet();
            }

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                while (m-- > 0)
                {

                    int op = ReadInt();
                    int x = ReadInt();
                    int y, v;
                    switch (op)
                    {

                        case 1:
                            v = ReadInt();

                            UpdateAdd(1, n, chain[x].idx, chain[x].subEnd, v);
                            break;

                        case 2:
                            y = ReadInt();
                            v = ReadInt();

                            QueryAdd(x, y, v);
                            break;

                        case 3:
                            v = ReadInt();

                            UpdateMul(1, n, chain[x].idx, chain[x].subEnd, v);
                            break;

                        case 4:
                            y = ReadInt();
                            v = ReadInt();

                            QueryMul(x, y, v);
                            break;

                        case 5:
                            sw.Write($"{(uint)GetVal(1, n, chain[x].idx, chain[x].subEnd)}\n");
                            break;

                        case 6:
                            y = ReadInt();

                            sw.Write($"{Query(x, y)}\n");
                            break;
                    }
                }

                sr.Close();

                void QueryAdd(int _x, int _y, int _v)
                {

                    int f = _x;
                    int t = _y;

                    if (chain[f].dep > chain[t].dep)
                    {

                        int temp = f;
                        f = t;
                        t = temp;
                    }

                    while (chain[f].dep < chain[t].dep)
                    {

                        UpdateAdd(1, n, chain[chain[t].head].idx, chain[t].idx, _v);
                        t = chain[t].parent;
                    }

                    while (chain[f].head != chain[t].head)
                    {

                        UpdateAdd(1, n, chain[chain[f].head].idx, chain[f].idx, _v);
                        UpdateAdd(1, n, chain[chain[t].head].idx, chain[t].idx, _v);

                        f = chain[f].parent;
                        t = chain[t].parent;
                    }

                    if (chain[f].idx > chain[t].idx)
                    {

                        int temp = f;
                        f = t;
                        t = temp;
                    }

                    UpdateAdd(1, n, chain[f].idx, chain[t].idx, _v);
                }

                void QueryMul(int _x, int _y, int _v)
                {

                    int f = _x;
                    int t = _y;

                    if (chain[f].dep > chain[t].dep)
                    {

                        int temp = f;
                        f = t;
                        t = temp;
                    }

                    while (chain[f].dep < chain[t].dep)
                    {

                        UpdateMul(1, n, chain[chain[t].head].idx, chain[t].idx, _v);
                        t = chain[t].parent;
                    }

                    while (chain[f].head != chain[t].head)
                    {

                        UpdateMul(1, n, chain[chain[f].head].idx, chain[f].idx, _v);
                        UpdateMul(1, n, chain[chain[t].head].idx, chain[t].idx, _v);

                        f = chain[f].parent;
                        t = chain[t].parent;
                    }

                    if (chain[f].idx > chain[t].idx)
                    {

                        int temp = f;
                        f = t;
                        t = temp;
                    }

                    UpdateMul(1, n, chain[f].idx, chain[t].idx, _v);
                }

                uint Query(int _x, int _y)
                {

                    int f = _x;
                    int t = _y;

                    if (chain[f].dep > chain[t].dep)
                    {

                        int temp = f;
                        f = t;
                        t = temp;
                    }

                    int ret = 0;
                    while (chain[f].dep < chain[t].dep)
                    {

                        ret += GetVal(1, n, chain[chain[t].head].idx, chain[t].idx);
                        t = chain[t].parent;
                    }

                    while (chain[f].head != chain[t].head)
                    {

                        ret += GetVal(1, n, chain[chain[f].head].idx, chain[f].idx);
                        ret += GetVal(1, n, chain[chain[t].head].idx, chain[t].idx);

                        f = chain[f].parent;
                        t = chain[t].parent;
                    }

                    if (chain[f].idx > chain[t].idx)
                    {

                        int temp = f;
                        f = t;
                        t = temp;
                    }

                    ret += GetVal(1, n, chain[f].idx, chain[t].idx);
                    return (uint)ret;
                }
            }

            int GetVal(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                LazyUpdate();

                if (_e < _chkS || _chkE < _s) return 0;
                else if (_chkS <= _s && _e <= _chkE)
                    return seg[_idx].val;

                int mid = (_s + _e) >> 1;
                return GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1)
                    + GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2);

                void LazyUpdate()
                {

                    if (!seg[_idx].lazy) return;
                    seg[_idx].lazy = false;

                    int mul = seg[_idx].mul;
                    int add = seg[_idx].add;

                    seg[_idx].mul = 1;
                    seg[_idx].add = 0;

                    seg[_idx].val = (seg[_idx].val * mul) + add * (_e - _s + 1);

                    if (_s == _e) return;
                    int l = _idx * 2 + 1;
                    int r = l + 1;
                    seg[l].mul *= mul;
                    seg[l].add = (seg[l].add * mul) + add;
                    seg[l].lazy = true;

                    seg[r].mul *= mul;
                    seg[r].add = (seg[r].add * mul) + add;
                    seg[r].lazy = true;
                }
            }

            void UpdateAdd(int _s, int _e, int _chkS, int _chkE, int _add, int _idx = 0)
            {

                LazyUpdate();

                if (_e < _chkS || _chkE < _s) return;
                else if (_chkS <= _s && _e <= _chkE)
                {

                    seg[_idx].lazy = true;
                    seg[_idx].add += _add;
                    LazyUpdate();
                    return;
                }

                int mid = (_s + _e) >> 1;
                UpdateAdd(_s, mid, _chkS, _chkE, _add, _idx * 2 + 1);
                UpdateAdd(mid + 1, _e, _chkS, _chkE, _add, _idx * 2 + 2);

                seg[_idx].val = seg[_idx * 2 + 1].val + seg[_idx * 2 + 2].val;

                void LazyUpdate()
                {

                    if (!seg[_idx].lazy) return;
                    seg[_idx].lazy = false;

                    int mul = seg[_idx].mul;
                    int add = seg[_idx].add;

                    seg[_idx].mul = 1;
                    seg[_idx].add = 0;

                    seg[_idx].val = (seg[_idx].val * mul) + add * (_e - _s + 1);

                    if (_s == _e) return;
                    int l = _idx * 2 + 1;
                    int r = l + 1;
                    seg[l].mul *= mul;
                    seg[l].add = (seg[l].add * mul) + add;
                    seg[l].lazy = true;

                    seg[r].mul *= mul;
                    seg[r].add = (seg[r].add * mul) + add;
                    seg[r].lazy = true;
                }
            }

            void UpdateMul(int _s, int _e, int _chkS, int _chkE, int _mul, int _idx = 0)
            {

                LazyUpdate();

                if (_e < _chkS || _chkE < _s) return;
                else if (_chkS <= _s && _e <= _chkE)
                {

                    seg[_idx].lazy = true;
                    seg[_idx].mul *= _mul;
                    seg[_idx].add *= _mul;
                    LazyUpdate();
                    return;
                }

                int mid = (_s + _e) >> 1;
                UpdateMul(_s, mid, _chkS, _chkE, _mul, _idx * 2 + 1);
                UpdateMul(mid + 1, _e, _chkS, _chkE, _mul, _idx * 2 + 2);

                seg[_idx].val = seg[_idx * 2 + 1].val + seg[_idx * 2 + 2].val;

                void LazyUpdate()
                {

                    if (!seg[_idx].lazy) return;
                    seg[_idx].lazy = false;

                    int mul = seg[_idx].mul;
                    int add = seg[_idx].add;

                    seg[_idx].mul = 1;
                    seg[_idx].add = 0;

                    seg[_idx].val = (seg[_idx].val * mul) + add * (_e - _s + 1);

                    if (_s == _e) return;
                    int l = _idx * 2 + 1;
                    int r = l + 1;
                    seg[l].mul *= mul;
                    seg[l].add = (seg[l].add * mul) + add;
                    seg[l].lazy = true;

                    seg[r].mul *= mul;
                    seg[r].add = (seg[r].add * mul) + add;
                    seg[r].lazy = true;
                }
            }

            void SetChain()
            {

                chain = new (int idx, int head, int parent, int dep, int subEnd)[n + 1];
                int cnt = 1;
                chain[1].head = 1;

                DFS();

                int DFS(int _cur = 1, int _prev = 0, int _dep = 1)
                {

                    chain[_cur].idx = cnt;
                    chain[_cur].dep = _dep;

                    int ret = cnt++;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (next == _prev) continue;

                        if (i == 0)
                        {

                            chain[next].parent = chain[_cur].parent;
                            chain[next].head = chain[_cur].head;

                            ret = Math.Max(ret, DFS(next, _cur, _dep));
                        }
                        else
                        {

                            chain[next].parent = _cur;
                            chain[next].head = next;

                            ret = Math.Max(ret, DFS(next, _cur, _dep + 1));
                        }
                    }

                    // 여기서 오일러 경로 테크닉 적용
                    chain[_cur].subEnd = ret;
                    return ret;
                }
            }

            void SetSeg()
            {

                int log = (int)(Math.Ceiling(Math.Log2(n) + 1e-9)) + 1;
                seg = new (int val, bool lazy, int add, int mul)[1 << log];
                (int val, bool lazy, int add, int mul) init = (0, false, 0, 1);
                Array.Fill(seg, init);
            }

            void SetChild()
            {

                int[] child = new int[n + 1];
                DFS();

                int DFS(int _cur = 1, int _prev = 0)
                {

                    ref int ret = ref child[_cur];
                    ret = 1;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];

                        if (next == _prev) continue;

                        ret += DFS(next, _cur);

                        if (child[edge[_cur][0]] < child[next] || edge[_cur][0] == _prev)
                        {

                            int temp = edge[_cur][0];
                            edge[_cur][0] = next;
                            edge[_cur][i] = temp;
                        }
                    }

                    return ret;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();
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
                    if (c == ' ' || c == '\n') return true;

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
