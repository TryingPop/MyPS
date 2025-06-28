using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 13
이름 : 배성훈
내용 : Sky Tax
    문제번호 : 13896번

    LCA 문제다.
    아이디어는 다음과 같다.
    만약 찾는 노드와 현재 수도의 lca가 찾는 노드와 다른 경우
    찾는 노드의 자식수가 정답이 된다.

    반면 같은 경우는 찾는 노드의 자식 중 루트로 가는 간선 사이에 있는 노드로 가는 경로를 끊고
    갈 수 있는 모든 노드가 정답이 된다.
    해당 부분에서 깊이 계산을 잘못해 2번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1404
    {

        static void Main1404(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n, q, r;
            List<int>[] edge;
            int[] child, depth;
            int[][] parent;

            Init();

            int t = ReadInt();
            for (int i = 1; i <= t; i++)
            {

                sw.Write($"Case #{i}:\n");
                Input();

                GetRet();
            }

            void GetRet()
            {

                while (q-- > 0)
                {

                    int op = ReadInt();

                    if (op == 0)
                        r = ReadInt();
                    else
                    {

                        int f = ReadInt();

                        int ret = GetChild(f);
                        sw.Write(ret);
                        sw.Write('\n');
                    }
                }

                int GetChild(int _f)
                {

                    if (_f == r) return n;
                    int lca = GetLCA(_f, r);

                    if (lca != _f) return child[_f];

                    int up = depth[r] - depth[lca] - 1;
                    int t = GoParent(r, up);

                    return n - child[t];
                }
            }

            int GetLCA(int _f, int _t)
            {

                if (depth[_t] < depth[_f])
                {

                    int temp = _f;
                    _f = _t;
                    _t = temp;
                }

                _t = GoParent(_t, depth[_t] - depth[_f]);

                if (_f == _t) return _f;

                for (int i = 16; i >= 0; i--)
                {

                    if (parent[_f][i] != parent[_t][i])
                    _f = parent[_f][i];
                    _t = parent[_t][i];
                }

                return parent[_f][0];
            }

            int GoParent(int _f, int _up)
            {

                for (int i = 17; i >= 0; i--)
                {

                    int up = 1 << i;
                    if (_up < up) continue;
                    _up -= up;
                    _f = parent[_f][i];
                }

                return _f;
            }

            void Init()
            {

                int N = 100_000;
                edge = new List<int>[N + 1];
                child = new int[N + 1];
                parent = new int[N + 1][];

                for (int i = 0; i <= N; i++)
                {

                    edge[i] = new();
                    parent[i] = new int[18];
                }

                depth = new int[N + 1];
            }

            void Input()
            {

                n = ReadInt();
                q = ReadInt();
                r = ReadInt();

                for (int i = 1; i <= n; i++)
                {

                    edge[i].Clear();
                    child[i] = 0;
                }

                for (int i = 1; i < n; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    edge[f].Add(t);
                    edge[t].Add(f);
                }

                DFS(r);

                for (int dep = 1; dep < 18; dep++)
                {

                    for (int node = 1; node <= n; node++)
                    {

                        int p = parent[node][dep - 1];
                        parent[node][dep] = parent[p][dep - 1];
                    }
                }

                int DFS(int _cur, int _prev = 0, int _depth = 1)
                {

                    child[_cur] = 1;
                    parent[_cur][0] = _prev;
                    depth[_cur] = _depth;
                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (next == _prev) continue;
                        child[_cur] += DFS(next, _cur, _depth + 1);
                    }

                    return child[_cur];
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
