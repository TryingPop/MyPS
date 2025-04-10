using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 10
이름 : 배성훈
내용 : 풍성한 트리
    문제번호 : 32934번

    트리 문제다.
    문제에서 말하는 차수는 자식의 수가 아닌 이어진 간선의 수이다.
    조건으로 차수가 1인 것은 리프와 일치한다.
    그리고 리프들과 거리가 같은게 트리의 외심으로 유일하게 존재함을 알 수 있다.
    그래서 정답은 존재한다면 유일하다.

    먼저 차수가 1 또는 3으로만 이루어졌는지 확인한다.
    여기서 차수 3인 것을 아무거나 하나 루트로 잡는다.
    루트가 없거나 차수가 1, 3 이외의 것이 존재하면 불가능하다고 판별하고 탈출한다.

    이후 앞에서 찾은 루트로 트리의 부모 관계를 설정한다.
    부모관계 설정과 동시에 리프를 모두 찾는다.

    이후 루트와 가장 멀리 떨어진 거리와 가장 가까운 거리를 찾고 해당 노드를 기록한다.
    이제 해당 길이는 트리의 지름이 된다.
    트리의 지름에서 중심이 루트의 후보가 된다.(트리의 외심문제)
    마지막으로 트리의 지름의 중심이 루트가 되는지 확인해 제출했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1534
    {


        static void Main1534(string[] args)
        {

#if !FAST
            int n, lenOne;
            List<int>[] edge;
            int[] degree, one;
            int[][] parent;

            Input();

            GetRet();

            void GetRet()
            {

                int root = 0;
                one = new int[n];
                lenOne = 0;

                ChkRoot();

                if (root == 0)
                {

                    Console.Write(-1);
                    return;
                }

                int[] dep = new int[n + 1];
                int log = n == 1 ? 1 : (int)(Math.Log2(n - 1) + 1e-9) + 1;

                SetParent();

                int minDis = 0;
                int maxDis = 0;
                int minCnt = 0;
                int maxCnt = 0;

                int minIdx = 0;
                int maxIdx = 0;

                SetMinMaxDis();

                int chk;
                int half = (minDis + maxDis) / 2;

                chk = UpParent(maxIdx, half);

                for (int i = 0; i < lenOne; i++)
                {

                    int chkDis = GetDis(one[i], chk);
                    if (chkDis == half) continue;
                    Console.Write(-1);
                    return;
                }

                Console.Write($"1\n{chk}");

                void ChkRoot()
                {

                    for (int i = 1; i <= n; i++)
                    {

                        if (degree[i] == 1)
                        {

                            one[lenOne++] = i;
                            continue;
                        }
                        else if (degree[i] == 3) root = i;
                        else
                        {

                            root = 0;
                            break;
                        }
                    }
                }

                void SetMinMaxDis()
                {

                    for (int i = 0; i < lenOne; i++)
                    {

                        int curDis = GetDis(one[i], root);
                        if (minDis == 0 || curDis < minDis)
                        {

                            minDis = curDis;
                            minCnt = 1;
                            minIdx = one[i];
                        }
                        else if (curDis == minDis) minCnt++;

                        if (maxDis == 0 || maxDis < curDis)
                        {

                            maxDis = curDis;
                            maxCnt = 1;
                            maxIdx = one[i];
                        }
                        else if (curDis == maxDis) maxCnt++;
                    }
                }

                void SetParent()
                {

                    parent = new int[log + 1][];
                    for (int i = 0; i <= log; i++)
                    {

                        parent[i] = new int[n + 1];
                    }

                    DFS(root);

                    for (int i = 1; i <= log; i++)
                    {

                        for (int j = 1; j <= n; j++)
                        {

                            int p = parent[i - 1][j];
                            parent[i][j] = parent[i - 1][p];
                        }
                    }
                }

                int GetDis(int _f, int _t)
                {

                    int lca = GetLCA(_f, _t);
                    return Math.Abs(dep[_f] - dep[lca]) + Math.Abs(dep[_t] - dep[lca]);
                }

                int UpParent(int _cur, int _up)
                {

                    for (int i = log; i >= 0; i--)
                    {

                        int u = 1 << i;
                        if (_up < u) continue;
                        _up -= u;
                        _cur = parent[i][_cur];

                        if (_up == 0) break;
                    }

                    return _cur;
                }

                int GetLCA(int _f, int _t)
                {

                    if (dep[_f] < dep[_t])
                    {

                        int temp = _f;
                        _f = _t;
                        _t = temp;
                    }

                    if (dep[_t] < dep[_f])
                        _f = UpParent(_f, dep[_f] - dep[_t]);

                    for (int i = log; i >= 0; i--)
                    {

                        if (parent[i][_f] == parent[i][_t]) continue;
                        _f = parent[i][_f];
                        _t = parent[i][_t];
                    }

                    if (_f != _t) _f = parent[0][_f];
                    return _f;
                }

                void DFS(int _cur, int _prev = 0, int _dep = 1)
                {

                    dep[_cur] = _dep;
                    parent[0][_cur] = _prev;
                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (next == _prev) continue;
                        DFS(next, _cur, _dep + 1);
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                degree = new int[n + 1];
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

                    degree[f]++;
                    degree[t]++;
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
#elif SLOW

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            
            int n = ReadInt();
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

            int[] ret = new int[n + 1];
            int len = 0;
            int dis = -1;

            for (int i = 1; i <= n; i++)
            {

                if (edge[i].Count == 1 || edge[i].Count == 3) continue;
                Console.Write(-1);
                return;
            }

            for (int i = 1; i <= n; i++)
            {

                if (edge[i].Count == 1) continue;
                dis = -1;
                if (DFS(i, 0, 0)) ret[len++] = i;
            }

            if (len == 0) Console.Write(-1);
            else
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                sw.Write($"{len}\n");
                for (int i = 0; i < len; i++)
                {

                    sw.Write($"{ret[i]} ");
                }
            }

            bool DFS(int _cur, int _prev, int _dis)
            {

                for (int i = 0; i < edge[_cur].Count; i++)
                {

                    int next = edge[_cur][i];
                    if (next == _prev) continue;

                    if (DFS(next, _cur, _dis + 1)) continue;
                    return false;
                }

                if (edge[_cur].Count == 1)
                {

                    if (dis == -1) dis = _dis;
                    else if (dis == _dis) return true;
                    else return false;
                }

                return true;
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
#endif

        }
    }
}
