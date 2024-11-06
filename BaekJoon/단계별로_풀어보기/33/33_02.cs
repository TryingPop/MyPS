using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 1
이름 : 배성훈
내용 : 특정한 최단 경로
    문제번호 : 1504번

    풀이 아이디어는 가중치(거리)가 양수이기에 다익스트라 3번 해서 값을 구했다
    1 -> N으로 가는 루트 중 v와 w 둘 다 거쳐서 가는 경로는
    1 -> v -> w -> N
    1 -> w -> v -> N
    뿐이므로

    1에서 시작하는 다익스트라,
    v에서 시작하는 다익스트라,
    w에서 시작하는 다익스트라
    이렇게 최단 경로 3개를 구했다

    양방향이므로 실상 2개만 해도 충분하다
    1 -> v나 v -> 1이나 거리가 같고 마찬가지로 1 -> w나 w -> 1의 거리가 같기 때문에
    v에서 시작하는 다익스트라,
    w에서 시작하는 다익스트라
    해당 2개의 경우만 해도된다
    
    아래는 입력 길 부분만 수정하면 단방향의 경우도 해결가능한 코드이다

    그리고 앞에서는 우선순위 큐 역할을 하는 자료구조를 만들어서 해결했는데
    여기서는 단순 오름차순 연산이기에 System.Collections.Genenric 에서 제공하는 PriorityQueue 클래스를 이용했다
*/

namespace BaekJoon._33
{
    internal class _33_02
    {

        static void Main2(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            List<int[]>[] roots = new List<int[]>[info[0] + 1];

            for (int i = 0; i < info[1]; i++)
            {

                string[] temp = sr.ReadLine().Split(' ');

                int pos1 = int.Parse(temp[0]);
                int pos2 = int.Parse(temp[1]);

                int dis = int.Parse(temp[2]);

                roots[pos1] = roots[pos1] ?? new();
                roots[pos2] = roots[pos2] ?? new();

                roots[pos1].Add(new int[2] { pos2, dis });
                roots[pos2].Add(new int[2] { pos1, dis });
            }

            int[] target = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            sr.Close();

            // 탐색
            Dijkstra(roots, info, 1, target, out (int dis, bool v, bool w)[] result1);
            Dijkstra(roots, info, target[0], target, out (int dis, bool v, bool w)[] result2);
            Dijkstra(roots, info, target[1], target, out (int dis, bool v, bool w)[] result3);

            // 결과 값 계산
            int result;
            if (result1[info[0]].v && result1[info[0]].w) result = result1[info[0]].dis;
            else
            {

                int comp1, comp2;

                /// start -> target[0] -> target[1] -> end 순서
                if (result1[target[1]].v && result2[info[0]].w) comp1 = result1[target[1]].dis + result3[info[0]].dis
                                                                        < result1[target[0]].dis + result2[info[0]].dis ?

                                                                        result1[target[1]].dis + result3[info[0]].dis :
                                                                        result1[target[0]].dis + result2[info[0]].dis;
                // start -> target[1]인데 taregt[0]을 거친 경우 : start -> target[1] -> end
                else if (result1[target[1]].v) comp1 = result1[target[1]].dis + result3[info[0]].dis;
                // target[0] -> end에서 target[1]을 거친 경우 : start -> target[0] -> end
                else if (result2[info[0]].w) comp1 = result1[target[0]].dis + result2[info[0]].dis;
                // 이외의 경우  : start -> target[0] -> target[1] -> end
                else comp1 = result1[target[0]].dis + result2[target[1]].dis + result3[info[0]].dis;

                /// start -> target[1] -> target[0] -> end 순서
                if (result1[target[0]].w && result3[info[0]].v) comp2 = result1[target[0]].dis + result2[info[0]].dis
                                                                        < result1[target[1]].dis + result3[info[0]].dis ?

                                                                        result1[target[0]].dis + result2[info[0]].dis :
                                                                        result1[target[1]].dis + result3[info[0]].dis;
                // start -> target[0]인데 target[1]을 거친 경우 : start -> target[0] -> end
                else if (result1[target[0]].w) comp2 = result1[target[0]].dis + result2[info[0]].dis;
                // target[1] -> end에서 target[0]을 거친 경우 : start -> target[1] -> end
                else if (result3[info[0]].v) comp2 = result1[target[1]].dis + result3[info[0]].dis;
                else comp2 = result1[target[1]].dis + result3[target[0]].dis + result2[info[0]].dis;

                result = comp1 < comp2 ? comp1 : comp2;
                if (result >= 10_000_000) result = -1;
            }

            // 출력
            Console.WriteLine(result);
        }

        // 다익스트라
        static void Dijkstra(List<int[]>[] _roots, int[] _info, int _start, int[] _target, out (int dis, bool v, bool w)[] _result)
        {

            _result = new (int dis, bool v, bool w)[_info[0] + 1];

            // 양방향 간선이고 최대 점의 개수 800개, 최대 가중치 1000 이므로
            // 두 점 사이의 최단 거리는 항상 모든 점을 최대 가중치로 지나는 값보다 작으므로
            // 3 * 800 * 1000 = 2,400,000 이상의 가중치는 나올 수 없다
            // 그래서 넉넉하게 최대값 1,000만으로 잡았다
            Array.Fill(_result, (10_000_000, false, false));

            // 다익스트라 알고리즘 시작
            PriorityQueue<(int dst, int dis, bool v, bool w), int> q = new PriorityQueue<(int dst, int dis, bool v, bool w), int>();

            q.Enqueue((_start, 0, _start == _target[0], _start == _target[1]), 0);
            _result[_start].dis = 0;
            _result[_start].v = _start == _target[0];
            _result[_start].w = _start == _target[1];

            // 찾아서 기록
            while(q.Count > 0)
            {

                (int dst, int dis, bool v, bool w) node = q.Dequeue();

                if (_result[node.dst].dis < node.dis) continue;
                if (_roots[node.dst] == null) continue;

                for (int i = 0; i < _roots[node.dst].Count; i++)
                {

                    int dst = _roots[node.dst][i][0];
                    int dis = _roots[node.dst][i][1] + node.dis;
                    bool v = node.v ? true : dst == _target[0];
                    bool w = node.w ? true : dst == _target[1];
                    if (_result[dst].dis <= dis) continue;

                    _result[dst].dis = dis;
                    _result[dst].v = v;
                    _result[dst].w = w;
                    q.Enqueue((dst, dis, v, w), dis);
                }
            }
        }
    }
}
