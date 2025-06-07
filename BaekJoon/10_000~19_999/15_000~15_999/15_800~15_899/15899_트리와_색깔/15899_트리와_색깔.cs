using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 17
이름 : 배성훈
내용 : 트리와 색깔
    문제번호 : 15899번

    오프라인 쿼리, 오일러 경로 테크닉, 세그먼트 트리 문제다.
    아이디어는 다음과 같다.
    서브트리에서 색상이 C 이하인 노드의 수를 찾아야 한다.
    오프라인 쿼리를 이용하면 색칠하는 쿼리와 찾는 쿼리로 나눠서 색상 번호로 정렬한 뒤 쿼리를 진행하면
    색칠된 노드의 갯수만 세어주면 된다.

    그리고 서브트리에서 범위를 찾는 것은 오일러 경로 테크닉을 이용해 인덱스를 재부여 했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1412
    {

        static void Main1412(string[] args)
        {

            int n, m, c;
            List<int>[] edge;
            (int v, int c)[] queries;
            (int s, int e)[] chain;
            int[] seg;

            Input();

            SetSeg();

            SetChain();

            GetRet();

            void GetRet()
            {

                Array.Sort(queries, (x, y) => 
                {

                    int ret = x.c.CompareTo(y.c);
                    if (ret == 0) ret = x.v.CompareTo(y.v);
                    return ret;
                });

                int S = 1, E = n;

                int ret = 0;
                int MOD = 1_000_000_007;
                for (int i = 0; i < queries.Length; i++)
                {

                    if (queries[i].v < 0)
                    {

                        
                        int idx = chain[-queries[i].v].s;
                        int color = queries[i].c;

                        Update(S, E, idx, color);
                    }
                    else
                    {

                        int chkS = chain[queries[i].v].s;
                        int chkE = chain[queries[i].v].e;

                        ret = (ret + GetVal(S, E, chkS, chkE)) % MOD;
                    }
                }

                Console.Write(ret);

                int GetVal(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
                {

                    if (_e < _chkS || _chkE < _s) return 0;
                    else if (_chkS <= _s && _e <= _chkE) return seg[_idx];

                    int mid = (_s + _e) >> 1;
                    return GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1) +
                        GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2);
                }

                void Update(int _s, int _e, int _chk, int _val, int _idx = 0)
                {

                    if (_s == _e)
                    {

                        seg[_idx] = 1;
                        return;
                    }

                    int mid = (_s + _e) >> 1;
                    if (_chk <= mid) Update(_s, mid, _chk, _val, _idx * 2 + 1);
                    else Update(mid + 1, _e, _chk, _val, _idx * 2 + 2);

                    seg[_idx] = seg[_idx * 2 + 1] + seg[_idx * 2 + 2];
                }
            }

            void SetChain()
            {

                chain = new (int s, int e)[n + 1];

                int idx = 1;

                DFS(1);
                void DFS(int _cur, int _prev = 0)
                {

                    chain[_cur].s = idx++;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (next == _prev) continue;

                        DFS(next, _cur);
                    }

                    chain[_cur].e = idx - 1;
                }
            }

            void SetSeg()
            {

                int log = n == 1 ? 1 : (int)(Math.Log2(n - 1) + 1e-9) + 2;
                seg = new int[1 << log];
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();
                c = ReadInt();

                queries = new (int v, int c)[m + n];
                for (int i = 0; i < n; i++)
                {

                    queries[i] = (-i - 1, ReadInt());
                }

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

                for (int i = 0; i < m; i++)
                {

                    queries[i + n] = (ReadInt(), ReadInt());
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
