using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 30
이름 : 배성훈
내용 : Strongly Connected Component
    문제번호 : 2150번

    타잔 알고리즘을 써서 했다
    해당 사이트 예시 보고 코드로 만들어봤다
    https://jason9319.tistory.com/98

    주된 아이디어는 다음과 같다
    DFS 탐색을하며 루트에대해 자신으로 이어져 있는지 판별한다
    판별은 진입한 노드에 고유 id를 순서대로 부여하고 스택에 해당 노드를 담는다
    이미 고유 id가 있다면 새롭게 부여하지 않는다

    그리고 해당 노드에서 갈 수 있는 고유 id가 낮은 값을 반환하면서 반환 id가 자신의 id랑 같은지 확인한다
    모든 루트를 탐색하고 탐색 결과가 자기 자신이랑 같다면 그 루트에 있는 노드들은 하나의 그룹이된다
    이는 자기자신으로 돌아오는 사이클이 있고 가장 큰 사이클이 된다 
    그리고 해당 그룹의 노드들을 바로 그룹화하고 스택에서 꺼낸다!

    만약 다른 노드 N에서 이미 만들어진 그룹 A와 간선이 이어져 있는 경우 그 간선을 무시한다
    왜냐하면 A 그룹이 만들어지면서 A 그룹에 해당하는 노드들이 갈 수 있는 길을 모두 들렸다
    이는 현재 노드 N에서 A 그룹으로 가는 길은 있어도 A 그룹에서 노드 N으로 가는 길이 없다는 말이다!
    그래서 노드 N은 A 그룹에 포함되는 사이클이 될 수 없다
    이러한 경우는 만약 루트를 돌면서 자기보다 id 수치가 높은 값을 반환하게 되는 경우이다
    (역은 성립하지 않는다! 반환하는 id가 수치가 높다고 SCC 그룹이 존재하는 건 아니다! -> 같은 SCC 그룹에 있을 수도 있다, 45_03에 반례가 있다!)
    그리고 해당 노드들은 그룹화하면서 이미 스택에서 먼저 빠져나갔다!

    자기보다 id가 낮은 수치를 반환하면, 더 큰 사이클로 확장 가능하다는 의미이다

    이렇게 그룹을 만들어가면 SCC가 완성된다!
    코드는 아래와 같다

    코사라주 알고리즘도 있는데 이는 45_02 문제에서 적용해볼 예정이다!
*/

namespace BaekJoon._45
{
    internal class _45_01
    {

        static void Main1(string[] args)
        {

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            // 노드 수, 간선 수
            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            List<int>[] lines = new List<int>[info[0] + 1];
            for (int i = 1; i <= info[0]; i++)
            {

                lines[i] = new();
            }

            // 간선 입력
            for (int i = 0; i < info[1]; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                lines[temp[0]].Add(temp[1]);
            }

            sr.Close();

            // 타잔 알고리즘?
            int[] connected = new int[info[0] + 1];     // 연결 그룹
            int[] id = new int[info[0] + 1];            // 아이디
            Stack<int> s = new Stack<int>();            // 그룹짓기 용도
            int curId = 1;                              // 시작 아이디
            int groups = 0;                             // 그룹 개수

            // 1번에서 시작해서 못지나가는 노드가 있을수도 있어서 for문 돌렸다
            for (int i = 1; i <= info[0]; i++)
            {

                DFS(lines, i, connected, id, s, ref curId, ref groups);
            }

            // 조건에 맞게 출력
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            // 그룹 개수
            sw.Write(groups);
            sw.Write('\n');

            for (int i = 1; i <= groups; i++)
            {

                // 그룹 내에 최소값이 낮은 순서대로 먼저 출력하고
                // 해당 그룹을 출력할 때는 오름차순
                // 그래서 그룹 번호 먼저 확인!
                int next = -1;
                for (int j = 0; j < info[0]; j++)
                {

                    if (next == -1 && connected[j + 1] != 0)
                    {

                        next = connected[j + 1];
                        // 이미 확인한 노드로 변경
                        connected[j + 1] = 0;
                        sw.Write(j + 1);
                        sw.Write(' ');
                    }
                    else if (next == connected[j + 1])
                    {

                        connected[j + 1] = 0;
                        sw.Write(j + 1);
                        sw.Write(' ');
                    }
                }

                sw.Write("-1\n");
            }

            sw.Close();
        }

        static int DFS(List<int>[] _lines, int _cur, int[] _connected, int[] _id, Stack<int> _calc, ref int _curId, ref int _groups)
        {

            // for문으로 들어오기에 아이디가 부여됐다면, 이미 지나온 길이므로 탈출
            if (_id[_cur] != 0) return -1;

            // 아이디 부여, 중복방문 방지용!
            _id[_cur] = _curId++;
            
            // 현재 노드 넣기
            _calc.Push(_cur);

            // 사이클 판별용
            int result = _cur;

            for (int i = 0; i < _lines[_cur].Count; i++)
            {

                // 다음 노드 번호
                int next = _lines[_cur][i];
                if (_id[next] != 0)
                {

                    // 이미 방문한 곳이고 방문한 곳이 아직 그룹이 없다면
                    // 작은 값을 받아온다
                    if (_connected[next] == 0 && _id[next] < _id[result]) result = next;
                    // 만약 이미 방문한 노드가 그룹이 있다면, 그 그룹에서 해당 노드로 오는 길이 없기에 끊어버린다!
                    continue;
                }

                // 첫 방문 하는 노드 값 낮은 인덱스를 가져온다
                int chk = DFS(_lines, next, _connected, _id, _calc, ref _curId, ref _groups);
                
                // id가 작다면, 더 작은 아이디로 간다!
                // _id[chk]는 그룹과 이어져 있는 것이면
                // 자기보다 아이디가 높은 경우이다 _id[chk] > _id[_cur] >= _id[_result]
                if (_id[chk] < _id[result]) result = chk;
            }

            // 여러 노드를 거쳐서 자기자신 그대로라면 == 스텍에 있는 원소는 사이클 발생!
            if (result == _cur)
            {

                // 전체 그룹 수 + 1, 결과용!
                _groups++;

                // 바로 그룹화된 노드들 제거
                while (_calc.Count > 0)
                {

                    // 해당 노드가 나올때까지 넣은게 DFS 탐색하며 발견한 그룹들이다!
                    int next = _calc.Pop();
                    _connected[next] = _cur;

                    if (next == _cur) break;
                }
            }

            return result;
        }
    }
}
