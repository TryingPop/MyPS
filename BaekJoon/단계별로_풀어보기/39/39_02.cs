using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 19
이름 : 배성훈
내용 : 트리의 독립집합
    문제번호 : 2213번

    dp를 어떻게 잡아야할지 감이 안왔다
    가장 원초적으로 해당 값을 넣을 때의 최대값과 안넣을 때의 최대값을 따로 뒀다
    해당 방법으로 dp를 설정하고, 이제 어떻게 값을 채워나갈지 고민했다

    앞의 문제와 연관 있을까 싶어 처음에는 쿼리로 해야하나 의문을 가졌다
    예제를 들어 해보니 꽤나 유효해 보였다
        
    그런데 어떻게 가장 밑에 노드로 갈지 고민했다
    처음에는 BFS에서 큐 대신 스택을 써서 밑에 노드를 접근할까 했는데,
    코드 작성과 읽기가 매우 복잡해질거 같아서 포기했다

    그래서 다른 방법을 고민했고,
    리프까지 쭉 내려오는 DFS 방법을 이용했다

    예를들어 다음과 같은 트리가 주어졌다고 하자
                                1
                    2           3           4
                5       6       7       8   9   10
                                       11       12  13

        1의 자식은 2, 3, 4
        2의 자식은 5, 6
        3의 자식은 7
        4의 자식은 8, 9, 10
        8의 자식 11
        10의 자식 12, 13
        인 상황에서

        1 -> 2 -> 5 순서로 가다가
        5 의 자식이 없으므로 5에 값을 채운다
            5를 포함한 경우 5에 가중치를 넣고
            5를 포함하지 않으면 0의 값을 넣었다
        그리고 5 탐색 종료

        5의 부모 2로 온 뒤에 다른 경로인 6으로 탐색을 한다
        6에도 자식이 없어 6의 값을 채운다
            6을 포함한 경우는 6에 가중치를 넣었다
            6을 포함하지 않는 경우는 0이다!
        그리고 6 탐색 종료

        다시 2로 올라오고 2의 모든 경로가 탐색이 완료되었으므로
        5, 6의 값을 비교하며 2의 값을 채웠다
            2를 포함하는 경우 5, 6의 포함하지 않은값들을 합쳤다
            2를 포함하지 않는 경우, 
                5의 포함하지 않은 경우와 5를 포함한 경우 중 큰 값을 넣고
                6의 포함하지 않은 경우와 6을 포함한 경우 중 큰 값을 더해줬다

                이는 리프(자식이 없는 노드)의 부모에서는 비교하는게 의미 없으나
                리프의 부모의 부모인 경우 유의미하기 때문이다
                    100
                     |
                    101
                     |
                    102
                의 관계이고 101의 가중치 1, 102의 가중치를 1만이라 하면
                100을 넣지 않는 경우의 최대 가중치는 102만 넣은 가중치인 경우다
            

        해당 방법으로 3, 4를 채우고 1까지 채우면
        1에 포함된 경우나 포함안된 경우에 최대값이 담기게된다

    경로도 저장하고 싶었으나 자식의 개수가 가변적이라 저장 대신 읽는 것을 택했다

                                1
                    2           3           4
                5       6       7       8   9   10
                                       11       12  13

        1의 자식은 2, 3, 4
        2의 자식은 5, 6
        3의 자식은 7
        4의 자식은 8, 9, 10
        8의 자식 11
        10의 자식 12, 13
        인 상황에서

        1에 최대값이 있으므로 먼저 비교를 한다
            1) 1이 포함안된 경우의 값이 1이 포함된 경우의 값보다 클 때,
                1이 포함 안되었으므로 자식들 2, 3, 4을 이제 비교해야한다
                    2가 포함된 경우와 포함 안된 경우중 큰 것을 택한다
                    3, 4도 마찬가지이다
            2) 반면 1이 포함된 경우의 값이 1이 포함안된 경우의 값보다 클 때,
                    2, 3, 4는 포함안된다
                    그래서 2, 3, 4각각의 자식들을 1)처럼 비교한다

        이렇게 포함된 것들을 BFS 방식으로 찾아갔다
*/

namespace BaekJoon._39
{
    internal class _39_02
    {

        static void Main2(string[] args)
        {

            // 입력 
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            // 노드 수
            int num = int.Parse(sr.ReadLine());

            // 노드들의 가중치
            int[] weights = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            // 노드 1번부터 시작
            // 간선 입력
            List<int>[] lines = new List<int>[num + 1];
            for (int i = 1; i <= num; i++)
            {

                lines[i] = new();
            }

            for (int i = 0; i < num - 1; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                // 양방향이다!
                lines[temp[0]].Add(temp[1]);
                lines[temp[1]].Add(temp[0]);
            }

            sr.Close();

            // 얘를 포함할 때 최대값, 포함 안할 때의 최대값 을 보관할 dp
            (int except, int contain)[] dpWeights = new (int except, int contain)[num + 1];
            // 방문 여부
            bool[] visit = new bool[num + 1];


            // 최대값 찾기
            DFS(lines, dpWeights, weights, num, visit);

            // 추적하기!
            bool[] result = new bool[num + 1];
            Tracking(lines, dpWeights, num, visit, result);

            // 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                int maxWeight = dpWeights[1].contain >= dpWeights[1].except ? dpWeights[1].contain : dpWeights[1].except;

                sw.WriteLine(maxWeight);

                for (int i = 1; i <= num; i++)
                {

                    if (result[i])
                    {

                        sw.Write(i);
                        sw.Write(' ');
                    }
                }
            }
        }

        static void DFS(List<int>[] _lines, (int except, int contain)[] _dpWeights, int[] _weights, int _size, bool[] _visit, int _start = 1)
        {

            // 중복 진입 방지
            _visit[_start] = true;
            // 포함될 때 가중치 증가
            _dpWeights[_start].contain += _weights[_start - 1];

            for (int i = 0; i < _lines[_start].Count; i++)
            {

                int next = _lines[_start][i];
                if (_visit[next]) continue;

                DFS(_lines, _dpWeights, _weights, _size, _visit, next);

                // 부모가 들어갔으므로 자식은 포함 안되야 한다!
                _dpWeights[_start].contain += _dpWeights[next].except;
                // 포함안될떄는 자식 중 큰 것들을 가중치를 합친다
                _dpWeights[_start].except += _dpWeights[next].contain > _dpWeights[next].except ? _dpWeights[next].contain : _dpWeights[next].except;
            }
        }

        static void Tracking(List<int>[] _lines, (int except, int contain)[] _dpWeights, int _size, bool[] _visit, bool[] _result)
        {

            _visit[1] = false;

            Queue<(int num, bool contain)> q = new();
            // 1이 들어간게 큰지 1이 안들어간게 큰지 확인
            bool chk = _dpWeights[1].contain >= _dpWeights[1].except;
            // 기록
            _result[1] = chk;
            q.Enqueue((1, chk));

            while(q.Count > 0)
            {

                var node = q.Dequeue();

                for (int i = 0; i < _lines[node.num].Count; i++)
                {

                    int next = _lines[node.num][i];
                    if (!_visit[next]) continue;
                    // 부모로 가는거 방지!
                    _visit[next] = false;

                    // 부모가 포함안되어야 자식을 판별 한다
                    bool contain = !node.contain && _dpWeights[next].contain >= _dpWeights[next].except;
                    _result[next] = contain;
                    q.Enqueue((next, contain));
                }
            }
        }
    }
}
