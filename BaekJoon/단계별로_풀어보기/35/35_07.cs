using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 11
이름 : 배성훈
내용 : 최소비용 구하기 2
    문제번호 : 11779번

    다익스트라 알고리즘을 써서 시작 지점에서 다른 목적지로 향하는 최소 비용을 구했다
    그리고 기존의 dp 에 이전 주소만 추가해 이전 주소를 읽어 역추적했다
*/

namespace BaekJoon._35
{
    internal class _35_07
    {

        static void Main7(string[] args)
        {

            // 1_000 * 100_000 = 100_000_000
            const int MAX = 100_000_000;

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int cityNum = int.Parse(sr.ReadLine());


            List<(int dst, int dis)>[] roots = new List<(int dst, int dis)>[cityNum + 1];

            for (int i = 0; i < cityNum; i++)
            {

                roots[i + 1] = new List<(int dst, int dis)>();
            }

            int len = int.Parse(sr.ReadLine());

            for (int i = 0; i < len; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                roots[temp[0]].Add((temp[1], temp[2]));
            }

            int[] target = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
            sr.Close();

            (int dis, int before)[] dp = new (int dis, int before)[cityNum + 1];

            Array.Fill(dp, (MAX, -1));

            Dijkstra(roots, dp, target[0]);

            Stack<int> result = new Stack<int>();

            int chk = target[1];
            while(chk != -1)
            {

                result.Push(chk);
                chk = dp[chk].before;
            }


            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(dp[target[1]].dis);

                int cnt = result.Count;
                sw.WriteLine(cnt);

                for (int i = 0; i < cnt; i++)
                {

                    sw.Write(result.Pop());
                    sw.Write(' ');
                }
            }
        }

        static void Dijkstra(List<(int dst, int dis)>[] _root, (int dis, int before)[] _dp, int _start)
        {

            PriorityQueue<(int start, int dis), int> q = new PriorityQueue<(int start, int dis), int>();

            // 현재 거리를 담는다!
            q.Enqueue((_start, 0), 0);

            _dp[_start] = (0, -1);

            while(q.Count > 0)
            {

                var node = q.Dequeue();

                var curRoot = _root[node.start];
                int curDis = node.dis;

                // 최단 거리가 이미 기록되었다!
                if (_dp[node.start].dis < curDis) continue;

                for (int i = 0; i < curRoot.Count; i++)
                {

                    int dst = curRoot[i].dst;
                    int dis = curRoot[i].dis + curDis;

                    if (_dp[dst].dis <= dis) continue;

                    _dp[dst].dis = dis;
                    _dp[dst].before = node.start;

                    q.Enqueue((dst, dis), dis);
                }
            }
        }
    }
}
