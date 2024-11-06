using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 27
이름 : 배성훈
내용 : 북쪽나라의 도로
    문제번호 : 1595번

    입력에 대한 정보가 모호하다
    그래서 segfalut, index error로 여러 번 틀렸다
    그리고 도시가 1개인 경우도 생각해야한다!(100% 케이스;)

    주된 아이디어는 다익스트라를 2번 해서 풀었다
    가장 긴 두 도시간의 거리는 트리의 지름이 된다
    아무 점에서 가장 먼 곳을 찾고 먼곳에서 가장 먼 거리를 찾으면 결과가된다
    다익스트라는 저번 etc_0057의 방법을 이용했다
*/

namespace BaekJoon.etc
{
    internal class etc_0104
    {

        static void Main104(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            List<(int dst, int dis)>[] roots = new List<(int dst, int dis)>[10_001];
            for(int i = 0; i <= 10_000; i++)
            {

                roots[i] = new();
            }

            int s = 0;
            while(true)
            {

                string str = sr.ReadLine();
                if (str == null || str == string.Empty) break;
                string[] temp = str.Split(' ');
                int from = int.Parse(temp[0]);
                s = from;


                int to = int.Parse(temp[1]);
                int dis = int.Parse(temp[2]);

                roots[from].Add((to, dis));
                roots[to].Add((from, dis));
            }

            sr.Close();
            // 마을이 1개인 경우
            if (s == 0)
            {

                Console.WriteLine(0);
                return;
            }

            int[] board = new int[10_001];
            bool[] visit = new bool[10_001];

            for (int i = 0; i < board.Length; i++)
            {

                board[i] = -1;
            }

            PriorityQueue<int, int> q = new PriorityQueue<int, int>();

            int max = Dijkstra(roots, s, visit, board, q);

            for (int i = 0; i < board.Length; i++)
            {

                if (board[i] == max) s = i;
                board[i] = -1;
            }

            int ret = Dijkstra(roots, s, visit, board, q);

            Console.WriteLine(ret);
        }

        static int Dijkstra(List<(int dst, int dis)>[] _roots, int _start, bool[] _visit, int[] _board, PriorityQueue<int, int> _q)
        {

            int ret = -1;
            _board[_start] = 0;
            bool chk = _visit[_start];

            _q.Enqueue(_start, 0);

            while(_q.Count > 0)
            {

                var node = _q.Dequeue();

                if (_visit[node] != chk) continue;
                _visit[node] = !chk;

                for (int i = 0; i < _roots[node].Count; i++)
                {

                    int next = _roots[node][i].dst;
                    if (_visit[next] != chk) continue;

                    int nextDis = _board[node] + _roots[node][i].dis;
                    if (_board[next] != -1 && _board[next] < nextDis) continue;

                    _board[next] = nextDis;
                    _q.Enqueue(next, nextDis);

                    if (ret < nextDis) ret = nextDis;
                }
            }

            return ret;
        }
    }
}
