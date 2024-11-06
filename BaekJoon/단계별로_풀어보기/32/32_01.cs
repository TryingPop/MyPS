using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 25
이름 : 배성훈
내용 : 알고리즘 수업 - 깊이 우선 탐색 1
    문제번호 : 24479번

    DFS 알고리즘 문제
    
    처음에 bool로 특정 좌표에서 해당 좌표로 갈 수 있는지 저장하려고 했다

    예를들어 위치가 5개이고 1, 4 양방향 간선이 존재하면
    root 정보를
    bool[][] root = new bool[5 + 1][];
    root[1] = new bool[5 + 1];
    root[4] = new bool[5 + 1];

    root[1][4] = true;
    root[4][1] = true;
    와 같은 방식을 사용하려고 했다
    실제로 했고 메모리 초과 떴다

    조건을 확인해보니 위치가 최대 100,000개 였다
    
    이 방법을 이용하면 최대일 경우 bool을 100,000 * 100,000개 할당해야 한다 
    그리고 계산해보니 bool 할당에만 1GB를 가뿐히 넘겼다
    
    200,000 만개의 간선이 존재하고
    입력값이 100,000이니 
    매번 길 전부를 탐색하면 
    최대를 기준으로 20만 * 10만 = 200억이므로 시간 초과이다

    메모리를 써서 시간 복잡도를 O(N * log N)로 해결하려고 했다
    
    그래서 배열을 항이 2개인 배열을 받아오되
    입력 배열을 좌우 대칭 이동 한 배열을 새로 만들고
    기존 입력 배열과 새로만든 배열을 간선 집합에 저장했다
    
    그리고 간선 집합을 0번 인덱스를 기준으로 오름차순 정렬하고,
    그 다음에 1번 인덱스를 기준으로 오름차순 정렬했다
    여기서 N logN 이다( OrderBy의 시간 복잡도 - https://stackoverflow.com/questions/1832684/c-sharp-sort-and-orderby-comparison )
    (퀵 정렬을 변형했다고 한다)
    
    root에는 정렬되어만 있을뿐 인덱스 시작 번호는 알 수 없다
    여기서 바로 탐색할 경우 자원 낭비만 했을 뿐이다

    그래서 rootInfo 배열을 새로 만들어 
    root를 직접 탐색하며
    각 항에 대해 시작 인덱스와 개수를 담는 배열 정보를 넣었다
    
    DFS에서는 root와 rootInfo를 넣고 해당 인덱스에서 해당 개수만 탐색하게 했다
    그래서 DFS에서는 많아야 N 복잡도를 갖는다

    다른 사람 풀이를 확인해 보니 간선 집합을 리스트 배열로 해서 풀었다
    인덱스가 시작 지점 값은 연결된 지점들을 모은 리스트이다
    연결된 지점을 넣었다

    예를 들어 1, 4를 입력 받으면
    
    root = new List<int>()[N + 1];

    if (root[1] == null) root[1] = new List<int>();
    if (root[4] == null) root[4] = new List<int>();

    root[1].Add(4);
    root[4].Add(1);

    그리고 입력이 끝나면 각 원소에대해 존재하는 리스트를 정렬했다
    결과만 놓고 보니 이 방법이 내가 썼던 방법보다 속도가 2배 가까이 빠르고 메모리 사용량도 적다!

    PriorityQueue 자료구조를 이용해 가장 빠르게 푼 사람이 있었다
    (아마 힙 문제에서 본 자료구조인거 같다 값을 넣으면 정렬해서 큰 값을 맨 앞으로 올려주는 자료구조)
    비교해보니 나의 방법은 2배 이상의 메모리를 사용했고, 시간도 3배 가까이 늦었다
*/

namespace BaekJoon._32
{
    internal class _32_01
    {

        static void Main1(string[] args)
        {

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int[][] roots = new int[info[1] * 2][];

            for (int i = 0; i < info[1]; i++)
            {

                // 길 0 -> 1로 가는 걸로 해석한다
                int[] temp = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
                // 조건에서 양방향이므로 1 -> 0으로 갈 수 있음을 의미해야한다
                int[] reverse = new int[2] { temp[1], temp[0] };

                roots[i] = temp;
                roots[i + info[1]] = reverse;
            }

            sr.Close();

#if Wrong            
            // 메모리 초과 난다!
            // 다른 방법으로 간선들의 집합을 만들어야한다
            bool[][] roots = new bool[info[0] + 1][];

            for (int i = 1; i <= info[0]; i++)
            {

                roots[i] = new bool[info[0] + 1];
            }

            for (int i = 0; i < info[1]; i++)
            {

                int[] temp = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

                roots[temp[0]][temp[1]] = true;
                roots[temp[1]][temp[0]] = true;
            }
#endif

            // 0번 인덱스 오름차순으로 먼저 정렬하고 앞 조건을 지키면서 1번 인덱스 오름차순으로 정렬
            roots = roots.OrderBy(x => x[0]).ThenBy(x => x[1]).ToArray();

            // 정보 기록용
            // 각 위치에 따른 root의 시작 인덱스와 간선의 개수를 담을 생각
            int[][] rootInfo = new int[info[0] + 1][];
            
            // Array.Fill()메서드를 처음에 썼는데 해당 주소로 덮어서 의도한 결과가 안나온다
            for (int i = 1; i < info[0] + 1; i++)
            {

                // 0번 인덱스는 root에서 i위치가 시작하는 인덱스, 1번 인덱스는 간선의 개수
                // -1은 없음을 의미한다
                rootInfo[i] = new int[2] { -1, 0 };
            }

            // 간선 탐색
            for (int i = 0; i < roots.Length; i++)
            {

                // 간선 정보를 받아온다 그러면 시작 위치는 [0]에 있다
                int tempIdx = roots[i][0];
                // 정렬 했으므로 -1일 때 담는 값이 처음 시작 인덱스이다
                if (rootInfo[tempIdx][0] == -1) rootInfo[tempIdx][0] = i;
                // 간선의 개수 1개 증가 시킨다
                rootInfo[tempIdx][1]++;
            }

            // 방문 순서 기록용
            int[] visit = new int[info[0] + 1];

            // 순서
            int order = 1;

            // 스택이나 클래스를 이용해 DFS 탐색이 가능하나 여기서는 재귀함수를 이용해 DFS 탐색했다
            DFS(visit, roots, rootInfo, info[2], ref order);

#if Wrong
            DFS(visit, roots, info[2], ref order);
#endif

            // 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 1; i < visit.Length; i++)
                {

                    sw.WriteLine(visit[i]);
                }
            }
        }

        static void DFS(int[] _visit, int[][] _roots, int[][] _rootInfo, int _start, ref int _order)
        {

            _visit[_start] = _order++;

            int start = _rootInfo[_start][0];
            int end = _rootInfo[_start][1] + start;

            for (int i = start; i < end; i++)
            {

                int next = _roots[i][1];
                if (_visit[next] == 0) DFS(_visit, _roots, _rootInfo, next, ref _order);
            }
        }

#if Wrong
        static void DFS(int[] _visit, bool[][] _roots, int _start, ref int _order)
        {

            _visit[_start] = _order++;
            

            for (int i = 0; i < _visit.Length; i++)
            {

                if (_roots[_start][i] 
                    && _visit[i] == 0) DFS(_visit, _roots, i, ref _order);
            }
        }
#endif

    }
}
