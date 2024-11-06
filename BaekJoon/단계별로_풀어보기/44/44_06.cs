using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 29
이름 : 배성훈
내용 : 트리와 쿼리 2
    문제번호 : 13511번

    테스트 케이스는 다 맞는데, 1%에서 틀린다
    일단 코드가 너무 길 뿐더러, 함수로 반복되는 부분 짤라내야겠다!

    로직은 몇 번을 둘러봐도 이상없었다
    그래서 문제를 다시 읽어보니, 출력 형태의 문제임을 알았다

    노드 한 번에 이동하는 최대 입력 값이 100만이고, 최대 노드 이동은 10만이다
    100 만 * 10만 = 1000억 수치까지 갈 수 있다!
*/

namespace BaekJoon._44
{
    internal class _44_06
    {

        static void Main6(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            // 노드 수 입력
            int len = int.Parse(sr.ReadLine());

            // 간선 입력
            List<(int dst, int dis)>[] lines = new List<(int dst, int dis)>[len + 1];

            for (int i = 1; i <= len; i++)
            {

                lines[i] = new();
            }

            for (int i = 1; i < len; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                lines[temp[0]].Add((temp[1], temp[2]));
                lines[temp[1]].Add((temp[0], temp[2]));
            }

            // 이제 부모 관계 만들기!
            int log = (int)Math.Log2(len) + 1;
            (int parent, long dis)[,] dp = new (int parent, long dis)[len + 1, log];
            int[] depth = new int[len + 1];

            Queue<int> q = new();
            q.Enqueue(1);
            while(q.Count > 0)
            {

                int node = q.Dequeue();
                int curDepth = depth[node];
                for (int i = 0; i < lines[node].Count; i++)
                {

                    int next = lines[node][i].dst;
                    
                    if (dp[node, 0].parent == next) continue;

                    dp[next, 0] = (node, lines[node][i].dis);
                    depth[next] = curDepth + 1;

                    for (int s = 1; s < log; s++)
                    {

                        // 2^(n - 1) -> 2^n이동부분!
                        var chk = dp[dp[next, s - 1].parent, s - 1];
                        // 부모가 없는 경우
                        if (chk.parent == 0) break;
                        // 0 -> 2^(n - 1) 이동
                        var calc = dp[next, s - 1];
                        // 누적 거리이므로 ! 
                        dp[next, s] = (chk.parent, chk.dis + calc.dis);
                    }

                    q.Enqueue(next);
                }
            }

            // 출력
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            // 케이스 수
            int test = int.Parse(sr.ReadLine());

            for (int t = 0; t < test; t++)
            {

                // 문제
                int[] find = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);


                if (find[0] == 1)
                {

                    // 노드 이동간 최단 경로의 전체 거리 찾기
                    // 1_000_000 * 100_000 > int.MaxValue
                    long result = First(dp, depth, find[1], find[2]);
                    sw.Write(result);
                }
                else 
                { 
                    
                    // 노드 번호이므로 int
                    int result = Second(dp, depth, find[1], find[2], find[3]);
                    sw.Write(result);
                }

                sw.Write('\n');
            }

            sr.Close();
            sw.Close();
        }

        static long First((int parent, long dis)[,] _dp, int[] _depth, int _find1, int _find2)
        {

            long result = 0;
            // 깊이 맞추기 및 이동간 거리 연산
            while (_depth[_find1] != _depth[_find2])
            {

                int diff = _depth[_find1] - _depth[_find2];
                if (diff > 0)
                {

                    int up = -1;
                    int calc = 1;
                    while(calc <= diff)
                    {

                        calc *= 2;
                        up++;
                    }

                    var chk = _dp[_find1, up];
                    _find1 = chk.parent;
                    result += chk.dis;
                }
                else
                {

                    diff = -diff;
                    int up = -1;
                    int calc = 1;
                    while (calc <= diff)
                    {

                        calc *= 2;
                        up++;
                    }

                    var chk = _dp[_find2, up];
                    _find2 = chk.parent;
                    result += chk.dis;
                }
            }

            // 이제 LCA 찾아가기
            while(_find1 != _find2)
            {

                int up = 0;
                for(int i = 0; i < _dp.GetLength(1); i++)
                {

                    if (_dp[_find1, i].parent == _dp[_find2, i].parent) break;
                    up = i;
                }

                var chk1 = _dp[_find1, up];
                var chk2 = _dp[_find2, up];

                result += chk1.dis + chk2.dis;
                _find1 = chk1.parent;
                _find2 = chk2.parent;
            }

            return result;
        }

        static int Second((int parent, long dis)[,] _dp, int[] _depth, int _start, int _end, int _find)
        {

            int result;
            // 깊이 맞추는데 이동한 깊이 즉 간선의 수
            int diffLvl = _depth[_start] - _depth[_end];

            int depStart = _start;
            int depEnd = _end;

            // 깊이 맞추기
            if (diffLvl > 0)
            {

                depStart = Move(_dp, _start, diffLvl);
            }
            
            else if (diffLvl < 0)
            {

                depEnd = Move(_dp, _end, -diffLvl);
            }

            int findStart = depStart;
            int findEnd = depEnd;
            // lca 찾기
            while(findStart != findEnd)
            {

                int up = 0;
                for (int i = 0; i < _dp.GetLength(1); i++)
                {

                    if (_dp[findStart, i].parent == _dp[findEnd, i].parent) break;
                    up = i;
                }

                findStart = _dp[findStart, up].parent;
                findEnd = _dp[findEnd, up].parent;
            }

            // 같은 깊이에서 lca로 가는데 이동한 깊이 즉 간선의 수
            int lcaLvl = _depth[depStart] - _depth[findStart];

            if (diffLvl >= 0)
            {

                // start가 depth연산했다
                int chk = _find - 1 - diffLvl - lcaLvl;
                if (chk == 0) result = findStart;
                else if (chk > 0)
                {

                    // end를 이동해야한다!
                    chk = lcaLvl - chk;
                    result = Move(_dp, _end, chk);
                }
                else
                {

                    // start 이동!
                    chk = _find - 1;
                    result = Move(_dp, _start, chk);
                }
            }
            else
            {

                // end가 depth연산했다 start는 lcaLvl만큼만 이동한다
                // 이동한 간선이 start 연산보다 많은지 확인
                int chk = _find - 1 - lcaLvl;
                // start 노드가 LCA만큼만 이동한 경우
                if (chk == 0) result = findStart;
                // 찾아야할 간선의 이동이 start노드의 최대 이동보다 작다
                else if (chk < 0)
                {

                    // start 이동!
                    chk = _find - 1;
                    result = Move(_dp, _start, chk);
                }
                // 찾아야할 간선의 이동이 start 노드의 최대 이동보다 많다
                else
                {

                    // end이동
                    chk = -diffLvl + lcaLvl - chk;
                    result = Move(_dp, _end, chk);
                }
            }

            return result;
        }

        static int Move((int parent, long dis)[,] _dp, int _node, int _move)
        {

            
            for (int i = 0; i <= _dp.GetLength(1); i++)
            {

                if ((_move & (1 << i)) != 0) _node = _dp[_node, i].parent;
            }

            return _node;
        }
    }
}
