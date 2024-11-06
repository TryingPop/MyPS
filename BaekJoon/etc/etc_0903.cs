using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 23
이름 : 배성훈
내용 : Highways
    문제번호 : 7439번

    최소 스패닝 트리 문제다
    MST 문제다

    크루스칼 알고리즘을 이용해 최소 신장 트리를 찾았다
    기존에 주어진 고속도로를 연결된 그룹의 개수로 잘못 오인해 3번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0903
    {

        static void Main903(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;
            int n;
            int[] stack, group;
            PriorityQueue<(int f, int b), int> pq;
            (int x, int y)[] pos;
            int conn;

            Solve();
            void Solve()
            {

                Input();

                MST();
            }

            void MST()
            {

                pq = new((n * (n - 1)) / 2);

                FillPQ();
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                while(conn < n - 1)
                {

                    (int f, int b) node = pq.Dequeue();
                    int f = Find(node.f);
                    int b = Find(node.b);

                    if (f == b) continue;

                    sw.Write($"{node.f} {node.b}\n");
                    Union(f, b);
                }

                sw.Close();
            }

            void FillPQ()
            {

                for (int i = 1; i <= n; i++)
                {

                    int f = Find(i);
                    for (int j = i + 1; j <= n; j++)
                    {

                        int b = Find(j);
                        if (f == b) continue;
                        pq.Enqueue((i, j), GetDis(i, j));
                    }
                }
            }

            int GetDis(int _i, int _j)
            {

                int x = (pos[_i].x - pos[_j].x);
                int y = (pos[_i].y - pos[_j].y);

                return x * x + y * y;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                stack = new int[n];
                group = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    group[i] = i;
                }
                pos = new (int x, int y)[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    pos[i] = (ReadInt(), ReadInt());
                }

                int m = ReadInt();
                conn = 0;
                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    f = Find(f);
                    b = Find(b);

                    if (f == b) continue;
                    Union(f, b);
                }
            }

            void Union(int _g1, int _g2)
            {

                if (_g1 < _g2)
                {

                    int temp = _g1;
                    _g1 = _g2;
                    _g2 = temp;
                }

                group[_g1] = _g2;
                conn++;
            }

            int Find(int _chk)
            {

                int len = 0;
                while(_chk != group[_chk])
                {

                    stack[len++] = _chk;
                    _chk = group[_chk];
                }

                while(len-- > 0)
                {

                    group[stack[len]] = _chk;
                }

                return _chk;
            }

            bool TryRead(out int _ret)
            {

                int c = sr.Read();
                _ret = 0;
                if (c == '\r') c = sr.Read();
                if (c == ' ' || c == '\n' || c == '\t') return true;
                else if (c == -1) return false;
                bool positive = c != '-';

                if (positive) _ret = c - '0';

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    _ret = _ret * 10 + c - '0';
                }

                if (!positive) _ret = -_ret;
                return false;
            }

            int ReadInt()
            {

                int ret;
                while (TryRead(out ret)) { }

                return ret;
            }
        }
    }
}
