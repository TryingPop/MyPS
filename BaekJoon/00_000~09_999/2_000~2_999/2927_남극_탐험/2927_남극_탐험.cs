using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 6
이름 : 배성훈
내용 : 남극 탐험
    문제번호 : 2927번

    HLD, 느리게 갱신되는 세그먼트 트리, 오프라인 쿼리, 분리집합 문제다.
    먼저 쿼리들을 모두 입력받는다.
    이후 유니온 파인드 알고리즘을 이용해 간선 잇는 부분을 처리한다.
    여기서 간선이 연결 가능한지, 불가능한지와 이동할 수 있는지를 판별한다.

    이후 간선이 모두 놓이면 여러 개의 트리가 만들어진다.
    각각의 트리에 대해 HLD 분할을 진행한다.

    여기서 자식 노드를 카운팅하면서 간선에 가중치가 높은 것을 0번으로 옮기는데,
    해당 과정을 안하면 시간이 오래 걸린다!
    이걸 캐치 못해 국제 메시 기구랑 해당 문제 시간초과 많이 나왔다.
    그리고 쿼리를 진행하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1256
    {

        static void Main1256(string[] args)
        {

            int NOT_VISIT = -1;
            int yes = -2;
            int no = -3;
            int impo = -4;
            int EMPTY = -5;

            int EXCURSION = 1_783_666_096;
            int BRIDGE = 5_722_803;
            int PENGUINS = 699_825_387;

            string YES = "yes\n";
            string NO = "no\n";
            string IMPO = "impossible\n";

            int n, m, bias;
            int[] penguins;
            int[] ret;

            List<int>[] edge;
            List<(int f, int t, int idx)> query;
            (int idx, int head, int parent, int dep)[] chain;
            int[] seg;

            Solve();
            void Solve()
            {

                Input();

                SetChild();

                SetSeg();

                SetChain();

                GetRet();
            }

            void Update(int _chk, int _val)
            {

                int idx = bias | _chk;
                seg[idx] = _val;

                for (; idx > 1; idx >>= 1)
                {

                    seg[idx >> 1] = seg[idx] + seg[idx ^ 1];
                }
            }

            int GetVal(int _l, int _r)
            {

                int ret = 0;

                _l |= bias;
                _r |= bias;

                while (_l <= _r)
                {

                    if ((_l & 1) == 1)
                    {

                        ret += seg[_l];
                        _l++;
                    }

                    if (((~_r) & 1) == 1)
                    {

                        ret += seg[_r];
                        _r--;
                    }

                    _l >>= 1;
                    _r >>= 1;
                }

                return ret;
            }

            void GetRet()
            {

                for (int i = 0; i < query.Count; i++)
                {

                    int f = query[i].f;
                    if (f < 0)
                    {

                        f = -f;
                        penguins[f] = query[i].t;
                        Update(chain[f].idx, penguins[f]);
                        ret[query[i].idx] = EMPTY;
                    }
                    else
                    {

                        int t = query[i].t;
                        int num = 0;

                        if (chain[f].dep > chain[t].dep)
                        {

                            int temp = f;
                            f = t;
                            t = temp;
                        }

                        while (chain[f].dep < chain[t].dep)
                        {

                            num += GetVal(chain[chain[t].head].idx, chain[t].idx);
                            t = chain[t].parent;
                        }

                        while (chain[f].head != chain[t].head)
                        {

                            num += GetVal(chain[chain[f].head].idx, chain[f].idx);
                            num += GetVal(chain[chain[t].head].idx, chain[t].idx);

                            f = chain[f].parent;
                            t = chain[t].parent;
                        }

                        if (chain[f].idx > chain[t].idx)
                        {

                            int temp = f;
                            f = t;
                            t = temp;
                        }

                        num += GetVal(chain[f].idx, chain[t].idx);

                        ret[query[i].idx] = num;
                    }
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0; i < m; i++)
                {

                    if (ret[i] == EMPTY) continue;
                    if (ret[i] == yes) sw.Write(YES);
                    else if (ret[i] == no) sw.Write(NO);
                    else if (ret[i] == impo) sw.Write(IMPO);
                    else sw.Write($"{ret[i]}\n");
                }
            }

            void SetChain()
            {

                chain = new (int idx, int head, int parent, int dep)[n + 1];
                int cnt = 1;

                for (int i = 1; i <= n; i++)
                {

                    if (chain[i].head > 0) continue;
                    chain[i].head = i;
                    DFS(i);
                }

                for (int i = bias - 1; i > 0; i--)
                {

                    seg[i] = seg[i << 1] + seg[(i << 1) | 1];
                }

                void DFS(int _cur, int _prev = 0, int _dep = 1)
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

                    seg[bias | chain[_cur].idx] = penguins[_cur];
                }
            }

            void SetSeg()
            {

                int log = (int)Math.Ceiling(Math.Log2(n) + 1e-9);
                bias = 1 << log;
                seg = new int[1 << (log + 1)];
            }

            void SetChild()
            {

                int[] child = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    if (child[i] > 0) continue;
                    DFS(i, 0);
                }

                int DFS(int _cur, int _prev = 0)
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

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                penguins = new int[n + 1];
                edge = new List<int>[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    penguins[i] = ReadInt();
                    edge[i] = new();
                }

                m = ReadInt();
                ret = new int[m];
                Array.Fill(ret, NOT_VISIT);
                query = new(m);

                int[] group = new int[n + 1], stk = new int[n];
                for (int i = 1; i <= n; i++)
                {

                    group[i] = i;
                }

                for (int i = 0; i < m; i++)
                {

                    int op = ReadInt();
                    int f = ReadInt();
                    int t = ReadInt();

                    if (op == BRIDGE)
                    {

                        int gF = Find(f);
                        int gT = Find(t);

                        if (gF == gT)
                            ret[i] = no;
                        else
                        {

                            Union(gF, gT);
                            ret[i] = yes;

                            edge[f].Add(t);
                            edge[t].Add(f);
                        }
                    }
                    else if (op == EXCURSION)
                    {

                        int gF = Find(f);
                        int gT = Find(t);

                        if (gF != gT)
                        {

                            ret[i] = impo;
                            continue;
                        }

                        query.Add((f, t, i));
                    }
                    else
                        query.Add((-f, t, i));
                }

                void Union(int _f, int _t)
                {

                    if (_f > _t)
                    {

                        int temp = _f;
                        _f = _t;
                        _t = temp;
                    }

                    group[_t] = _f;
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
}
