using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 9
이름 : 배성훈
내용 : 백도어
    문제번호 : 17396

    다익스트라를 쓰는 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0169
    {

        static void Main169(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 16);

            int n = ReadInt(sr);
            int m = ReadInt(sr);

            bool[] wards = new bool[n];

            for (int i = 0; i < n; i++)
            {

                if (ReadInt(sr) == 1) wards[i] = true;
            }

            List<(int dst, int dis)>[] roots = new List<(int dst, int dis)>[n];
            for (int i = 0; i < n; i++)
            {

                roots[i] = new();
            }

            for (int i = 0; i < m; i++)
            {

                int f = ReadInt(sr);
                int t = ReadInt(sr);
                int dis = ReadInt(sr);

                roots[f].Add((t, dis));
                roots[t].Add((f, dis));
            }

            bool[] visit = new bool[n];
            long[] ret = new long[n];

            Array.Fill(ret, 10_000_000_000);

            Dijkstra(roots, wards, visit, 0, ret);

            if (ret[n - 1] == 10_000_000_000) Console.WriteLine(-1);
            else Console.WriteLine(ret[n - 1]);
        }

        static void Dijkstra(List<(int dst, int dis)>[] _roots, bool[] _ward, bool[] _visit, int _start, long[] _ret)
        {

            PriorityQueue<int, long> q = new(_visit.Length);

            q.Enqueue(_start, 0);
            _ret[_start] = 0;
            while (q.Count > 0)
            {

                var node = q.Dequeue();
                if (_visit[node]) continue;
                _visit[node] = true;

                for (int i = 0; i < _roots[node].Count; i++)
                {

                    int next = _roots[node][i].dst;
                    if (_visit[next]) continue;
                    // 적의 시야가 있는 곳이면 넥서스가 아닌 이상 못지나간다
                    if (next != _visit.Length - 1 && _ward[next]) continue;

                    long nextDis = _ret[node] + _roots[node][i].dis;
                    if (_ret[next] > nextDis)
                    {

                        _ret[next] = nextDis;
                        q.Enqueue(next, nextDis);
                    }
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
