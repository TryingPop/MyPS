using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 3
이름 : 배성훈
내용 : 아이돌
    문제번호 : 3648번

    처음에는 주어진 투표가 의심하는 상황이 안나올 수 있는지 확인하려고 했다
    즉, 찬성, 반대 모든 원소가 2 - SAT를 이루는지 확인
    그리고 2 - SAT에 이상이 없다면 이제 1이 참이 되는 경우인지 확인하려고 했다
    여기서 타잔으로 SCC를 만들고 우선순위 큐로 직접 위상정렬을 하려고 했다
    그리고 1이 참인지 판별하고, 또한 -1과 같이 있는 원소들이 모두 참인지 판별할려고 했다(-1을 1로 바꿔야하기에)
    그런데, 풀이가 복잡해져 해당 요구가 맞는지 의문을 품게 되었고 다른 방법이 있지 않을까 고민했다

    그래서 고민해보니 1, 1을 외치는 사람을 추가하면 해당 상근이가 합격하면서, 이 경우 심사위원이 의문을 안가지는지 확인하는 문제로 바뀐다
    그리고 찾으려는 상황과 일치한다 그래서 간선을 입력받고 마지막에 -1 -> 1로 가는 간선을 추가했고 심사위원이 의문을 가지는지 확인하기만 하니 통과했다

    이를 표현한 코드는 아래와 같다
*/

namespace BaekJoon._45
{
    internal class _45_07
    {

        static void Main7(string[] args)
        {

            // 최대 참가자 수
            int MAX = 1_000;

            // 출력용
            string yes = "yes";
            string no = "no";

            // 정방향 간선과 역방향 간선
            List<int>[] lines = new List<int>[2 * MAX + 1];
            List<int>[] reverseLines = new List<int>[2 * MAX + 1];

            // 코사라주 용
            Stack<int> s = new Stack<int>(MAX * 2);
            
            // DFS 탐색용
            bool[] visit = new bool[2 * MAX + 1];

            // 그룹 번호
            int[] groups = new int[2 * MAX + 1];

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            while (true)
            {

                string chk = sr.ReadLine();

                if (chk == null || chk == string.Empty) break;

                // 노드 수, 2 - CNF 식의 수
                int[] info = Array.ConvertAll(chk.Split(' '), int.Parse);

                // 간선 초기화 
                for (int i = 1; i < 2 * info[0] + 1; i++)
                {

                    if (lines[i] == null) lines[i] = new();
                    else lines[i].Clear();

                    if (reverseLines[i] == null) reverseLines[i] = new();
                    else reverseLines[i].Clear();
                }

                // 2 - CNF 식을 입력받고 간선으로 변환
                // 전체가 3일 때,
                // 음수는
                // -1 == 4, -2 == 5, -3 == 6
                // 이 되게 설정
                // 그리고 x or y 는 ~ x -> y, ~y -> x인 간선을 생성한다 여기서 ~ 는 not을 의미한다
                // or은 논리 곱
                for (int i = 0; i < info[1]; i++)
                {

                    int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                    if (temp[0] < 0) temp[0] = -temp[0] + info[0];
                    if (temp[1] < 0) temp[1] = -temp[1] + info[0];

                    int notF = Not(temp[0], info[0]);
                    int notB = Not(temp[1], info[0]);

                    // 정방향 간선
                    lines[notF].Add(temp[1]);
                    lines[notB].Add(temp[0]);

                    // 역방향 간선
                    reverseLines[temp[0]].Add(notB);
                    reverseLines[temp[1]].Add(notF);
                }

                // (1 V 1) 추가
                // 1번을 합격시키는 심사 위원을 추가했다
                lines[Not(1, info[0])].Add(1);
                reverseLines[1].Add(Not(1, info[0]));

                // (x, x), (-x, x) 를 외치는 사람이 없기에 아래 코드나 함께 넣어도 풀이에는 상관없다
                // lines[1].Add(Not(1, info[0]));
                // reverseLines[Not(1, info[0])].Add(1);
                
                // 코사라주 알고리즘으로 SCC를 만든다
                int max = info[0] * 2 + 1;
                for (int i = 1; i < max; i++)
                {

                    if (visit[i]) continue;

                    BeforeDFS(lines, visit, i, s);
                }

                int groupId = 0;

                while (s.Count > 0)
                {

                    var node = s.Pop();

                    if (!visit[node]) continue;

                    groupId++;
                    AfterDFS(reverseLines, visit, node, groups, ref groupId);
                }


                bool failed = false;
                // 심사위원 중 (x, x), (-x, x)를 제출하는 이가 없고 있다해도 무시하면 된다
                // 혹여나 1, -1이 이어져 있다면 다른 원소에서 건너온 것이기 때문에
                // 다른쪽에서도 걸리게 되어 1은 따로 확인안했다
                for (int i = 2; i <= info[0]; i++)
                {

                    if (groups[i] == groups[Not(i, info[0])])
                    {

                        failed = true;
                        break;
                    }
                }

                // 결과 출력
                if (failed) sw.WriteLine(no);
                else sw.WriteLine(yes);
            }

            sw.Close();
            sr.Close();
        }

        static int Not(int _n, int _add)
        {

            if (_n > _add) _n -= _add;
            else _n += _add;

            return _n;
        }

        static void BeforeDFS(List<int>[] _lines, bool[] _visit, int _cur, Stack<int> _calc)
        {

            _visit[_cur] = true;

            for (int i = 0; i < _lines[_cur].Count; i++)
            {

                int next = _lines[_cur][i];

                if (_visit[next]) continue;

                BeforeDFS(_lines, _visit, next, _calc);
            }

            _calc.Push(_cur);
        }

        static void AfterDFS(List<int>[] _reverseLines, bool[] _visit, int _cur, int[] _groups, ref int _groupId)
        {

            _visit[_cur] = false;
            _groups[_cur] = _groupId;

            for (int i = 0; i < _reverseLines[_cur].Count; i++)
            {

                int next = _reverseLines[_cur][i];

                if (!_visit[next]) continue;

                AfterDFS(_reverseLines, _visit, next, _groups, ref _groupId);
            }
        }
    }
}
