using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 13
이름 : 배성훈
내용 : 트리 재구성하기
    문제번호 : 30832번

    DFS, 해 구성하기 문제다.
    아이디어는 다음과 같다.
    A의 그래프의 1을 제외한 모든 노드를 1을 부모로 되게 만든다.
    이는 N미만의 횟수로 해결 가능하다.

    이제 1이 아닌 노드에 대해 모두 1이 부모로 만들면
    B의 리프에 해당하는 노드부터 부로를 형태로 변형한다.
    이역시 N미만의 과정으로 해결된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1539
    {

        static void Main1539(string[] args)
        {

            int n;
            List<int>[] edgeA, edgeB;

            Input();
#if !_

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                (int a, int b, int c)[] ret = new (int a, int b, int c)[n << 1];
                int len = 0;

                DFS1();

                DFS2();

                sw.Write($"{len}\n");
                for (int i = 0; i < len; i++)
                {

                    sw.Write($"{ret[i].a} {ret[i].b} {ret[i].c}\n");
                }

                void DFS2(int _cur = 1, int _prev = 0)
                {

                    for (int i = 0; i < edgeB[_cur].Count; i++)
                    {

                        int next = edgeB[_cur][i];
                        if (next == _prev) continue;

                        DFS2(next, _cur);

                        if (_prev != 0)
                            ret[len++] = (next, 1, _cur);
                    }
                }

                void DFS1(int _cur = 1, int _prev = 0)
                {

                    for (int i = 0; i < edgeA[_cur].Count; i++)
                    {

                        int next = edgeA[_cur][i];
                        if (next == _prev) continue;

                        if (_prev != 0)
                            ret[len++] = (next, _cur, 1);

                        DFS1(next, _cur);
                    }
                }
            }
#else

            int[] depA, depB;
            int[] parentA, parentB;

            SetArr();

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int len = 0;
                (int a, int b, int c)[] ret = new (int a, int b, int c)[n << 1];

                for (int dep = 2; dep < n; dep++)
                {

                    for (int node = 1; node <= n; node++)
                    {

                        if (depA[node] != dep) continue;
                        ret[len++] = (node, parentA[node], 1);
                    }
                }

                for (int dep = n; dep > 1; dep--)
                {

                    for (int node = 1; node <= n; node++)
                    {

                        if (depB[node] != dep) continue;
                        ret[len++] = (node, 1, parentB[node]);
                    }
                }

                sw.Write($"{len}\n");

                for (int i = 0; i < len; i++)
                {

                    sw.Write($"{ret[i].a} {ret[i].b} {ret[i].c}\n");
                }
            }

            void SetArr()
            {

                depA = new int[n + 1];
                depB = new int[n + 1];
                parentA = new int[n + 1];
                parentB = new int[n + 1];

                DFS(edgeA, depA, parentA);
                DFS(edgeB, depB, parentB);

                void DFS(List<int>[] _edge, int[] _dep, int[] _parent, int _cur = 1, int _prev = 0, int _depth = 0)
                {

                    _dep[_cur] = _depth;
                    _parent[_cur] = _prev;

                    for (int i = 0; i < _edge[_cur].Count; i++)
                    {

                        int next = _edge[_cur][i];
                        if (next == _prev) continue;

                        DFS(_edge, _dep, _parent,next, _cur, _depth + 1);
                    }
                }
            }
#endif

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                edgeA = new List<int>[n + 1];
                edgeB = new List<int>[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    edgeA[i] = new();
                    edgeB[i] = new();
                }

                for (int i = 1; i < n; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    edgeA[f].Add(t);
                    edgeA[t].Add(f);
                }

                for (int i = 1; i < n; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    edgeB[f].Add(t);
                    edgeB[t].Add(f);
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
                        while ((c= sr.Read()) != -1 && c != ' ' && c != '\n')
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
