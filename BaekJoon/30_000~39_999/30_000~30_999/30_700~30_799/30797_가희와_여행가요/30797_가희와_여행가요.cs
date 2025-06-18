using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 9
이름 : 배성훈
내용 : 가희와 여행가요
    문제번호 : 30797번

    MST 문제다
    범위, 정렬 문제로 3번 틀렸다
    기존에는 priorityqueue를 이용해 크루스칼로 MST를 구현했으나
    여기서는 그냥 정렬만 해도 충분할거 같아 배열을 정렬해 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_1040
    {

        static void Main1040(string[] args)
        {

            StreamReader sr;
            (int f, int t, int cost, int time)[] edge;
            int n, q;
            int[] stack, group;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                for (int i = 1; i <= n; i++)
                {

                    group[i] = i;
                }

                int g = 1;
                int ret1 = 0;
                long ret2 = 0;
                for (int i = 0; i < q; i++)
                {

                    int f = Find(edge[i].f);
                    int t = Find(edge[i].t);

                    if (f == t) continue;
                    ret1 = Math.Max(ret1, edge[i].time);
                    ret2 += edge[i].cost;

                    g++;
                    group[f] = t;

                    if (g == n) break;
                }

                if (g != n)
                    Console.Write(-1);
                else
                    Console.Write($"{ret1} {ret2}");
            }

            int Find(int _chk)
            {

                int len = 0;
                while (group[_chk] != _chk)
                {

                    stack[len++] = _chk;
                    _chk = group[_chk];
                }

                while (len > 0)
                {

                    group[stack[--len]] = _chk;
                }

                return _chk;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                q = ReadInt();

                stack = new int[n];
                group = new int[n + 1];

                edge = new (int f, int t, int cost, int time)[q];
                for (int i = 0; i < q; i++)
                {

                    edge[i] = (ReadInt(), ReadInt(), ReadInt(), ReadInt());
                }

                Array.Sort(edge, (x, y) =>
                {

                    int ret = x.cost.CompareTo(y.cost);
                    if (ret == 0) return x.time.CompareTo(y.time);
                    return ret;
                });

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
