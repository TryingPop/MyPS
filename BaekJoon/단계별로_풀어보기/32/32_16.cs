using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 30
이름 : 배성훈
내용 : 이분 그래프
    문제번호 : 1707번

    모든 꼭짓점을 빨강과 파랑으로 색칠하되, 
    모든 변이 빨강과 파랑 꼭짓점을 포함하도록 색칠할 수 있는 그래프!

    핵심을 잘못 파악해서 6% 에서 바로 틀렸다
    처음에는 인접한 간선들을 제공하는 줄 알았다
    예를 들어 1 - 3, 1 - 5, 2 - 4, 3 - 5 있으면
    1 - 3, 3 - 5, 1 - 5
    2 - 4 이 순서로 주는 줄 알았다
    그래서 전부 인접해있으므로 고민 없이 처음 색 설정해서 색상 칠하고
    새로운게 나오면 떨어져 있는거니 다시 1색상을 칠하는 행동을 했다
    당연히 6% 에서 바로 오답이 떴고 순서가 랜덤일 수 있겠다고 생각했다
    
    그래서 앞에서 했던 리스트 배열을 이용해 선로들을 표현하고 ( 찾아보니 무방향 그래프 )
    간선의 두 점은 서로 다르다는 조건이 있어서 처음 아무 항이나 찾아 색을 넣고 인접한 항들에 색을 비교하면 탐색했다
    그래서 많아야 N(정점의 개수) + 2 * M(간선의 개수) 탐색이 된다 (간선 양방향으로 즉, 2배로 넣었다)
    중간에 간선을 뺄 수 있지만 List에서 매번 특정 원소를 pop하는거보다는 한 번 더 찾는 연산을 하는게 적어보여서 두 번 연산했다

    위키 백과의 이분 그래프 정의 링크이다
    https://ko.wikipedia.org/wiki/%EC%9D%B4%EB%B6%84_%EA%B7%B8%EB%9E%98%ED%94%84
    
    다른 사람의 풀이를 보니 인접행렬? N * N 으로 하면 메모리 초과 뜬다고 한다
    N이 20,000개이므로 행렬 크기만 고려해도 당연해 보인다 
    2만 * 2만 * 4바이트 = 16억바이트 = 800 * 2백만바이트 > 800 * 메가바이트
*/

namespace BaekJoon._32
{
    internal class _32_16
    {

        static void Main16(string[] args)
        {


            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int len = int.Parse(sr.ReadLine());

            List<int>[] lines = new List<int>[20_001];
            Queue<int> group = new Queue<int>(200_001);
            int[] chk = new int[20_001];

            for (int i = 0; i < len; i++)
            {

                int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                if (i != 0)
                {

                    // 배열 초기화
                    for (int j = 1; j <= info[0]; j++)
                    {

                        chk[j] = 0;
                        lines[j]?.Clear();
                    }

                    group.Clear();
                }

                // 선의 넣기
                for (int j = 0; j < info[1]; j++)
                {

                    int[] temp = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

                    lines[temp[0]] = lines[temp[0]] ?? new();
                    lines[temp[1]] = lines[temp[1]] ?? new();

                    lines[temp[0]].Add(temp[1]);
                    lines[temp[1]].Add(temp[0]);
                }


                bool result = true;


                // 탐색
                for(int j = 1; j <= info[0]; j++)
                {

                    if (lines[j]?.Count > 0)
                    {

                        if (BFS(lines, chk, group, j))
                        {

                            result = false;
                            break;
                        }
                    }
                }

                if (result) sw.WriteLine("YES");
                else sw.WriteLine("NO");
            }

            sr.Close();
            sw.Close();
        }

        static bool BFS(List<int>[] _lines, int[] _color, Queue<int> _group, int _idx)
        {

            _group.Clear();

            _group.Enqueue(_idx);
            _color[_idx] = 1;

            while (_group.Count > 0)
            {

                int idx = _group.Dequeue();

                for (int i = 0; i < _lines[idx].Count; i++)
                {

                    int next = _lines[idx][i];
                    _group.Enqueue(next);

                    // 현재꺼와 다음께 색이 같은 경우
                    if (_color[next] == _color[idx])
                    {

                        // 이분 그래프가 안된다
                        return true;
                    }
                    else if (_color[next] == 0)
                    {

                        _color[next] = -_color[idx];
                    }
                }

                // 인접한게 모두 색이 없다!
                _lines[idx].Clear();
            }

            return false;
        }
    }
}
