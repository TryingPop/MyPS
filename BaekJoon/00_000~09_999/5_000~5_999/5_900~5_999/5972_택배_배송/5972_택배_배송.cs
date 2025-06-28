using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 15
이름 : 배성훈
내용 : 택배 배송
    문제번호 : 5972번

    다익스트라, 최단경로 문제다
    모두 소의 수가 음이아닌 정수이기에 다익스트라를 쓸 수 있다

    최단 경로이기에 최대값은 노드의 수 * 최대비용으로 했다
    이후에는 다익스트라로 결과를 구했다
*/

namespace BaekJoon.etc
{
    internal class etc_0239
    {

        static void Main239(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int m = ReadInt(sr);

            List<(int dst, int val)>[] lines = new List<(int dst, int val)>[n + 1];

            for (int i = 1; i <= n; i++)
            {

                lines[i] = new();
            }

            for (int i = 0; i < m; i++)
            {

                int f = ReadInt(sr);
                int b = ReadInt(sr);
                int val = ReadInt(sr);

                lines[f].Add((b, val));
                lines[b].Add((f, val));
            }

            sr.Close();

            int[] cost = new int[n + 1];
            bool[] visit = new bool[n + 1];

            Array.Fill(cost, 100_000_000);
            Dijkstra(lines, visit, cost);

            Console.WriteLine(cost[n]);
        }

        static void Dijkstra(List<(int dst, int val)>[] _lines, bool[] _visit, int[] _ret)
        {

            // 우선순위 큐를 이용한 다익스트라
            PriorityQueue<int, int> q = new();

            _ret[1] = 0;
            q.Enqueue(1, 0);

            while(q.Count > 0)
            {

                var node = q.Dequeue();
                if (_visit[node]) continue;
                _visit[node] = true;

                for (int i = 0; i < _lines[node].Count; i++)
                {

                    int next = _lines[node][i].dst;
                    if (_visit[next]) continue;

                    int chkVal = _ret[node] + _lines[node][i].val;
                    if (_ret[next] <= chkVal) continue;

                    q.Enqueue(next, chkVal);
                    _ret[next] = chkVal;
                }
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while ((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
