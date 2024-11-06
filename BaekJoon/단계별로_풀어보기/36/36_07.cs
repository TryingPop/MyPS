using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 16
이름 : 배성훈
내용 : 트리
    문제번호 : 4803번

    문제 조건에서 트리는 사이클이 없는 연결요소라고 한다
    
    처음에는 문제 자체를 이해 못했다
        6 3         -- 점 6개, 간선 3개
        1 2         두점을 끝점으로 하는 간선들 
        2 3
        3 4
        0 0         -- 더 이상 입력 값 없음
    을 입력할 경우 왜 트리가 3개인지 이해를 못했다

    처음에는 [ 1, 2, 3, 4 ] 에 5, 6을 붙여 트리를 몇 개 만들 수 있는가로 이해했었다;
        
    5, 6을 1, 2, 3, 4 중 아무거나에 5, 6을 이어붙이는 경우 4 * 4 = 16
    그리고 5, 6에 간선이 있는 경우 4 * 2 = 8 (5 -> 1, 2, 3, 4에 가는 경우 혹은 6 -> 1, 2, 3, 4에 가는 경우)
    총 24개로 판단하고 왜 3개가 되는지 의문을 가졌다
        
    트리의 정의를 잘못 알고 있는지 
    혹은 트리 모양이 같으면 같은걸로 취급하는지 ( 1 - 2 - 3 - 4, 1 - 3 - 4 - 2를 같은 걸로 취급하는지)

    시간이 지나도 답이 안보이자 다른 사람 설명을 보고 잘못 이해한 것을 알게됐다

    문제에서 요구한 것은
        6 3         -- 점 6개, 간선 3개
        1 2         두점을 끝점으로 하는 간선들 
        2 3
        3 4
        0 0         -- 더 이상 입력 값 없음

    [ 1, 2, 3, 4 ] 는 사이클이 없으므로 트리가 된다
    그리고 [ 5 ], [ 6 ] 역시 사이클이 없으므로 하나의 트리이다!
    그래서 전체 트리는 3개이다

    그래서 트리 체크를 문제 조건에 맞춰 사이클이 있는지 여부로 BFS 탐색 했다
    문자열 입력을 trees 로 해야하는데 tress로 오타내서 2회 도전했지만 164ms로 통과!
    stringbuilder를 마지막에 사용하는 거 보다 그냥 바로 출력으로 내보내는게 좋을거 같아 바로 출력하니 12ms 단축되었다
    164ms -> 152ms
*/

namespace BaekJoon._36
{
    internal class _36_07
    {

        static void Main7(string[] args)
        {

            // 입력 조건
            const int MAX_VERTEX = 500;

            // 출력 조건에 맞는거 미리 변수로 선언
            const string ZERO = ": No trees.\n";
            const string ONE = ": There is one tree.\n";
            const string MORE_BEFORE = ": A forest of ";
            const string MORE_AFTER = " trees.\n";
            const string CASE = "Case ";

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            // StringBuilder sb = new StringBuilder();

            // 연산용 큐
            Queue<int> q = new Queue<int>(MAX_VERTEX);
            // 간선
            List<int>[] lines = new List<int>[MAX_VERTEX + 1];

            // 초기 세팅
            for (int i = 0; i < MAX_VERTEX; i++)
            {

                lines[i + 1] = new();
            }

            // 부모노드 저장용
            int[] parent = new int[MAX_VERTEX + 1];

            // 케이스 번호
            int cases = 0;
            while (true)
            {

                cases++;
                int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                // 탈출 조건!
                if (info[0] == info[1] && info[0] == 0) break;
    
                // 간선 입력
                for (int i = 0; i < info[1]; i++)
                {

                    int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                    // 사이클 체크!
                    // 중복되는 간선은 없다고 한다!
                    lines[temp[0]].Add(temp[1]);
                    lines[temp[1]].Add(temp[0]);
                }

                // 트리 개수 판정
                // 사이클이 없으면 트리! 추가
                int result = 0;
                for (int i = 1; i <= info[0]; i++)
                {

                    // 이미 들린 길이 아니고, 사이클이 없는 경우만 회수 추가!
                    if (parent[i] == 0) if (ChkCycle(lines, parent, q, i)) result++;
                }

                // 결과값 조건에 맞춰 출력
                sw.Write(CASE);
                sw.Write(cases);
                if (result == 0) sw.Write(ZERO);
                else if (result == 1) sw.Write(ONE);
                else
                {

                    sw.Write(MORE_BEFORE);
                    sw.Write(result);
                    sw.Write(MORE_AFTER);
                }

                /*
                sb.Append(CASE);
                sb.Append(cases);
                if (result == 0) sb.Append(ZERO);
                else if (result == 1) sb.Append(ONE);
                else
                {

                    sb.Append(MORE_BEFORE);
                    sb.Append(result.ToString());
                    sb.Append(MORE_AFTER);
                }
                */

                // 부모 배열과 간선 배열 초기화
                for (int i = 0; i < info[0]; i++)
                {

                    lines[i + 1].Clear();
                    parent[i + 1] = 0;
                }
            }

            // sw.Write(sb);
            sw.Close();
        }

        static bool ChkCycle(List<int>[] _lines, int[] _parent, Queue<int> _q, int _start)
        {

            // 사이클을 체크하기에 루트는 아무거나 시작해도된다
            _q.Enqueue(_start);
            _parent[_start] = -1;

            // 사이클이 없다고 먼저 선언
            bool result = true;

            while(_q.Count > 0)
            {

                var node = _q.Dequeue();

                for (int i = 0; i < _lines[node].Count; i++)
                {

                    int next = _lines[node][i];

                    // 부모로 가는 길인 경우!
                    if (next == _parent[node]) continue;

                    // 처음 가는 길인 경우
                    if (_parent[next] == 0)
                    {

                        _parent[next] = node;
                        _q.Enqueue(next);
                    }
                    else
                    {
                        
                        // 부모로 가는 점을 빼면 이미 들린 점이 나와서는 안된다!
                        // 이미 들린 점이 나왔다는 것은 사이클이 있다는 것과 같다!
                        // 그래서 사이클이 있다고만 갱신!
                        result = false;
                    }
                }
            }

            // 결과값 출력
            return result;
        }
    }
}
