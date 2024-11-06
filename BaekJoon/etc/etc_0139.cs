using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 1
이름 : 배성훈
내용 : 골목 대장 호석 - 기능성
    문제번호 : 20168번

    길을 끊어가며
    다익스트라를 계속 돌렸다;

    그런데, 다른사람 풀이를 보니 더 좋은 방법이 있는거 같다
    긿을 끊는다고 ashamed에 시작지점, 끝점, 가격을 넣었는데
    그냥 ashamed는 cost만 받고, 다익스트라에서 가격 제한을 거는게 좋아보인다!

    시간은 조금 느려졌으나, 메모리는 조금 줄었다;

    이것보다 더 짧은 방법이 존재한다;
    이분탐색으로 제한을 걸면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0139
    {

        static void Main139(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int m = ReadInt(sr);

            List<(int dst, int cost)>[] lines = new List<(int dst, int cost)>[n + 1];
            for (int i = 1; i <= n; i++)
            {

                lines[i] = new();
            }

            int from = ReadInt(sr);
            int to = ReadInt(sr);

            int cost = ReadInt(sr);

            for (int i = 0; i < m; i++)
            {

                int f = ReadInt(sr);
                int b = ReadInt(sr);

                int c = ReadInt(sr);

                lines[f].Add((b, c));
                lines[b].Add((f, c));
            }

            sr.Close();

            int[] dis = new int[n + 1];
#if first
            (int from, int to, int cost)[] ashamed = new (int from, int to, int cost)[n + 1];
#else
            int[] ashamed = new int[n + 1];
#endif
            bool[] visit = new bool[n + 1];

            PriorityQueue<int, int> q = new(100);
            int ret = 1_001;
            while (true)
            {

#if first
                Dijkstra(lines, from, visit, dis, ashamed, q);

                if (cost >= dis[to])
                {


                    if (ret > ashamed[to].cost) ret = ashamed[to].cost;
                    int f = ashamed[to].from;
                    int b = ashamed[to].to;
                    int c = ashamed[to].cost;

                    lines[f].Remove((b, c));
                    lines[b].Remove((f, c));
                    continue;
                }
#else

                // ret으로 이분탐색을 하면 된다!;
                Dijkstra(lines, from, visit, dis, ashamed, ret, q);

                if (cost >= dis[to])
                {

                    if (ret > ashamed[to]) ret = ashamed[to];
                    continue;
                }
                
#endif
                break;
            }
            ret = ret == 1_001 ? -1 : ret;
            Console.WriteLine(ret);
        }


#if first
        static void Dijkstra(List<(int dst, int cost)>[] _lines, int _start, bool[] _visit, int[] _dis, (int from, int to, int cost)[] _ashamed, PriorityQueue<int, int> _q)
        {

            Array.Fill(_dis, 100_000);
            Array.Fill(_ashamed, (0, 0, 0));
            Array.Fill(_visit, false);
            _q.Enqueue(_start, 0);
            _dis[_start] = 0;
            while(_q.Count > 0)
            {

                int curDst = _q.Dequeue();
                if (_visit[curDst]) continue;
                _visit[curDst] = true;

                for (int i = 0; i < _lines[curDst].Count; i++)
                {

                    int next = _lines[curDst][i].dst;
                    if (_visit[next]) continue;

                    int nextDis = _lines[curDst][i].cost + _dis[curDst];
                    if (nextDis < _dis[next])
                    {

                        _q.Enqueue(next, nextDis);
                        _dis[next] = nextDis;
                        (int from, int to, int cost) nextAshamed = _ashamed[curDst];
                        nextAshamed = nextAshamed.cost > _lines[curDst][i].cost ? nextAshamed : (curDst, next, _lines[curDst][i].cost);

                        if (_ashamed[next].cost < nextAshamed.cost) _ashamed[next] = nextAshamed;
                    }
                }
            }
        }
#else
        static void Dijkstra(List<(int dst, int cost)>[] _lines, int _start, bool[] _visit, int[] _dis, int[] _ashamed, int _upper, PriorityQueue<int, int> _q)
        {

            Array.Fill(_dis, 100_000);
            Array.Fill(_ashamed, 0);
            Array.Fill(_visit, false);
            _q.Enqueue(_start, 0);
            _dis[_start] = 0;
            while (_q.Count > 0)
            {

                int curDst = _q.Dequeue();
                if (_visit[curDst]) continue;
                _visit[curDst] = true;

                for (int i = 0; i < _lines[curDst].Count; i++)
                {

                    int next = _lines[curDst][i].dst;
                    if (_visit[next] || _lines[curDst][i].cost >= _upper) continue;

                    int nextDis = _lines[curDst][i].cost + _dis[curDst];
                    if (nextDis < _dis[next])
                    {

                        _q.Enqueue(next, nextDis);
                        _dis[next] = nextDis;
                        int nextAshamed = _ashamed[curDst];
                        nextAshamed = nextAshamed > _lines[curDst][i].cost ? nextAshamed : _lines[curDst][i].cost;

                        if (_ashamed[next] < nextAshamed) _ashamed[next] = nextAshamed;
                    }
                }
            }
        }

#endif
        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
