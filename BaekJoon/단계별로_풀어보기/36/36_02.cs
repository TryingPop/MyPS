using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 13
이름 : 배성훈
내용 : 트리의 지름
    문제번호 : 1167번

    어떻게 풀지 감이 안왔다
    처음에는 이동할 수 잇는 길이 3개에서 값을 넣어 계산해야 하나 싶었다
    그래서 다른 사람의 푸는 아이디어를 빌려왔다;

    참고한 블로그의 풀이를 사용했다
        임의의 점 a에서 시작한다
        a와 가장 멀리 떨어진 점 b를 찾는다
        b에서 다시 멀리 떨어진 점 c를 찾는다
        그리고 b와 c의 거리를 재면 트리의 지름이 된다
    그래서 BFS 탐색을 두번 돌렸다
*/

namespace BaekJoon._36
{
    internal class _36_02
    {

        static void Main2(string[] args)
        {

#if first
            List<(int dst, int dis)>[] root;
            int[] dp;

            using (StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput())))
            {

                int len = int.Parse(sr.ReadLine());

                dp = new int[len + 1];
                root = new List<(int dst, int dis)>[len + 1];
                for (int i = 0; i < len; i++)
                {

                    root[i + 1] = new();
                }


                for (int i = 0; i < len; i++)
                {

                    int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                    int chk = 1;
                    while (temp[chk] != -1)
                    {

                        root[temp[0]].Add((temp[chk], temp[chk + 1]));
                        root[temp[chk]].Add((temp[0], temp[chk + 1]));

                        chk += 2;
                    }
                }
            }

            // 먼저 1에서 가장 멀리 있는 a점을 찾는다
            var calc = BFS(root, dp, 1);
            // 이후 a에서 가장 멀리 있는 점 b와의 거리를 찾는다
            var result = BFS(root, dp, calc.dst);

            // a와 b의 거리 출력
            Console.WriteLine(result.dis);
#else 

            // 빠른 사람의 풀이      
            List<Edge>[] edges;

            {
                var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
                var t = int.Parse(sr.ReadLine()!);
                edges = new List<Edge>[t + 1];
                while (t-- > 0)
                {
                    var input = Array.ConvertAll(sr.ReadLine()!.Split(), int.Parse);
                    var cur = input[0];
                    var list = new List<Edge>();
                    for (int i = 1; i < input.Length - 1; i += 2)
                        list.Add(new Edge { Dest = input[i], Dist = input[i + 1] });
                    edges[cur] = list;
                }
                sr.Close();
            }

            var visited = new bool[edges.Length];
            visited[1] = true;
            var result = int.MinValue;
            DFS(1, edges, visited, ref result);
            Console.Write(result);
#endif

        }

#if first
        static (int dst, int dis) BFS(List<(int dst, int dis)>[] _root, int[] _dp, int _start)
        {

            (int dst, int dis) result = new();
            bool[] visit = new bool[_dp.Length];

            Queue<int> q = new Queue<int>();

            q.Enqueue(_start);

            _dp[_start] = 0;
            visit[_start] = true;

            while (q.Count > 0)
            {

                int node = q.Dequeue();
                int curDis = _dp[node];

                var curRoot = _root[node];
                for (int i = 0; i < curRoot.Count; i++)
                {

                    int next = curRoot[i].dst;
                    if (visit[next]) continue;

                    visit[next] = true;
                    _dp[next] = curDis + curRoot[i].dis;

                    if (result.dis < _dp[next])
                    {

                        result.dis = _dp[next];
                        result.dst = next;
                    }

                    q.Enqueue(next);
                }
            }

            return result;
        }

#else

        static int DFS(int node, List<Edge>[] edges, bool[] visited, ref int result)
        {
            int max = 0, sec = int.MinValue;

            foreach ((int dest, int dist) in edges[node])
            {
                if (visited[dest]) continue;

                visited[dest] = true;
                var wholeDist = DFS(dest, edges, visited, ref result) + dist;
                if (max < wholeDist)
                {
                    sec = max;
                    max = wholeDist;
                }
                else if (sec < wholeDist)
                    sec = wholeDist;
            }

            var curResult = max + sec;
            if (result < curResult)
                result = curResult;

            return max;
        }

        struct Edge
        {
            public int Dest;
            public int Dist;

            public void Deconstruct(out int dest, out int dist)
                => (dest, dist) = (Dest, Dist);
        }
#endif
    }
}
