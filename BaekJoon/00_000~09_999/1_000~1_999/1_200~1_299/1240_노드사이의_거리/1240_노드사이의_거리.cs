using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 27
이름 : 배성훈
내용 : 노드사이의 거리
    문제번호 : 1240번

    트리, 그래프 탐색 문제다.
    두 노드 사이의 거리는 LCA를 이용해 구하는 것이
    매 탐색에서 log N으로 좋다.

    여기서는 노드 수와 쿼리 수가 적기에 일일히 경로탐색을 하며 찾아갔다.
*/

namespace BaekJoon.etc
{
    internal class etc_1480
    {

        static void Main1480(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt(), m = ReadInt();
            List<(int dst, int dis)>[] edge = new List<(int dst, int dis)>[n + 1];
            for (int i = 1; i <= n; i++)
            {

                edge[i] = new();
            }

            for (int i = 1; i < n; i++)
            {

                int f = ReadInt();
                int t = ReadInt();
                int dis = ReadInt();

                edge[f].Add((t, dis));
                edge[t].Add((f, dis));
            }

            while (m-- > 0)
            {

                int ret = DFS(ReadInt(), ReadInt());
                sw.Write(ret);
                sw.Write('\n');
            }

            int DFS(int _cur, int _goal, int _prev = -1)
            {

                if (_cur == _goal) return 0;

                int ret = -1;
                for (int i = 0; i < edge[_cur].Count; i++)
                {

                    int next = edge[_cur][i].dst;
                    if (next == _prev) continue;
                    int chk = DFS(next, _goal, _cur);

                    if (chk == -1) continue;
                    ret = chk + edge[_cur][i].dis;
                    break;
                }

                return ret;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c= sr.Read()) != -1 && c != ' ' && c!= '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
