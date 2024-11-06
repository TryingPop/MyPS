using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 26
이름 : 배성훈
내용 : 알고리즘 수업 - 너비 우선 탐색 1
    문제번호 : 24444번

    탐색 방법만 BFS로 다를 뿐 32_01과 같다
    32_02에서 썼던 list방법 그대로 이용!
    다만 앞에서는 Enumerable의 Select를 썼는데 여기서는 안쓴다
    Enumerable을 보니 System.Linq에서 가져온다


    BFS 탐색 방법과 DFS 탐색의 차이만 적어 놓는다
    다음과 같은 단방향 경로가 있다고 하자
    이미 간 곳은 다시 확인하지 않는다!

    1 <-> 2
    1 <-> 3
    2 <-> 5
    1 <-> 6

    그러면 1 에서 2, 3, 6 으로 갈 수 있고
    2에서는 5로 갈 수 있다

    DFS의 경우 
    갈 수 있는 최대 지점까지 간다
    1에서 시작하는 경우
    그래서 탐색 순서는 1 -> 2 -> 5를 탐색하고
    경로가 없는 경우 위로 올라온다 2, 5에는 새로운 경로가 없으므로 1까지 올라온다
    그리고 1 -> 3으로 오고 3에서 새로운 경로가 없어 다시 1로 올라온다
    마지막으로 1 -> 6 탐색을 하고 끝마친다
    
    반면 BFS의 경우
    해당 지점에서 갈 수 있는 곳을 모두 다 가볼때까지 더 깊게 안들어간다
    갈 수 있는 곳을 갈 때마다 그 지점에서 이동할 수 잇는 경로를 보관한다
    그리고 갈 수 잇는 곳을 다 가면 그제서야 먼저 보관한 곳에 이동하고
    갈 수 잇는 모든 곳을 탐색한다 이를 반복한다
    1에서 시작하는 경우
    1 -> 2, 을 가고 2 에서 갈 수 있는 경로를 저장한다 보관함에 2에서 새로 갈 수 있는 곳이 있다
    1 -> 3, 을 가고 3 에서 갈 수 있는 경로를 저장한다 보관함에 2에서 새로 갈 수 있는 곳과 3에서 새로 갈 수 있는 곳이 있다
    1 -> 6, 을 가고 6 에서 갈 수 있는 경로를 저장한다 보관함에 2에서 새로 갈 수 있는 곳과 3에서 새로 갈 수 있는 곳 그리고 6에서 새로 갈 수 있는 곳이 있다

    이제 1에서 갈 수 있는 곳을 다 갔으므로
    보관함에 2, 3, 6 에서 갈 수 있는 곳 중 먼저 들어온 곳으로 간다
    즉 2에서 갈 수 있는 곳을 탐색한다
    2 -> 1, 은 1 -> 2로 이미 간 곳이므로 탐색하지 않고 건너뛴다
    2 -> 6, 을 가고 6에서 갈 수 있는 경로를 저장한다 보관함에는 3, 6, 5에서 갈 수 있는 곳이 있다
    2에서 갈 수 있는 곳을 다 갔으므로 이제 3에서 갈 수 있는 곳을 가고
    3에서 갈 수 있는 곳이 없음을 알고 6으로, 5로 가며 탐색한다
    게임 길찾기에 자주 쓰이는 A* 알고리즘이 BFS에 해당하는 방법이다
    
    둘 다 완전 탐색을 하는 점에서 똑같지만 활용에 차이가 있다
    예를 들어 BFS로 길찾기를 했을 때 경로를 찾으면 그 경로가 최단 거리라고 확정 지을 수 있다
    반면 DFS로 길찾기를 하면 경로를 찾았다고 해도 그 경로가 최단 거리라고 확정 지을 수 없다

    그러나 BFS는 경로를 찾는데 시간이 많이 걸리고 
    DFS는 아무 경로나 하나 찾는데 걸리는 시간이 짧아 
    DFS 탐색에 백트래킹을 접목시켜 경로 찾는 순간 탈출하면 해당 목적지로 향하는 길을 빠르게 찾는다

    그리고 로직을 보면 탐색에 BFS는 자료구조 Queue를 DFS는 자료구조 Stack을 이용한다
*/

namespace BaekJoon._32
{
    internal class _32_03
    {

        static void Main3(string[] args)
        {

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            // 경로 입력
            List<int>[] root = new List<int>[info[0] + 1];
            for (int i = 0; i < info[1]; i++)
            {

                string[] temp = sr.ReadLine().Split(' ');

                int pos1 = int.Parse(temp[0]);
                int pos2 = int.Parse(temp[1]);

                root[pos1] = root[pos1] ?? new();
                root[pos2] = root[pos2] ?? new();

                root[pos1].Add(pos2);
                root[pos2].Add(pos1);
            }

            sr.Close();

            // 경로 오름차순 정렬
            for (int i = 1; i < root.Length; i++)
            {

                root[i]?.Sort();
            }

            // 방문 순서 - 결과 
            int[] visit = new int[info[0] + 1];

            // 탐색
            BFS(visit, root, info[2]);

            // 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 1; i < visit.Length; i++)
                {

                    sw.WriteLine(visit[i]);
                }
            }
        }

        // 너비 탐색
        static void BFS(int[] _visit, List<int>[] _root, int _start, int _order = 1)
        {

            // 먼저 시작점 체크
            // 앞에서 order는 재귀로 공유하는 값이라 주소값을 넘겼는데 여기서는 재귀 없이 해서 따로 안받아온다
            _visit[_start] = _order++;

            // 경로 보관용
            Queue<List<int>> queue = new Queue<List<int>>();

            // 경로가 있으면 넣는다
            if (_root[_start] != null) queue.Enqueue(_root[_start]);

            // 경로가 없을 때까지
            while(queue.Count > 0)
            {

                // 최상위 경로 꺼낸다
                List<int> node = queue.Dequeue();

                // 기록 안된거 기록하고 경로 넣기
                for (int i = 0; i < node.Count; i++)
                {

                    int next = node[i];
                    if (_visit[next] == 0) 
                    { 
                        
                        _visit[next] = _order++;
                        if (_root[next] != null) queue.Enqueue(_root[next]);
                    }
                }
            }
        }

    }
}
