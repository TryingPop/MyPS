using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 1. 4
이름 : 배성훈
내용 : 운동
    문제번호 : 1956번

    플로이드 워셜을 이용한 사이클 탐색 문제이다
    처음에 문제를 이해 못해서 무엇을 찾는건지 몰랐다
    그래서 검색을 하게 되었고, 
    a에서 출발하면 a에 돌아오는 길을 사이클로 표현했다
    여기서 a는 임의의 점!이다
        예를들어 점 1, 2, 3이 있고, 1 -> 2, 2 -> 3, 3 -> 1인 루트가 있을 때 사이클
            1 -> 2 -> 3 -> 1 을 사이클로 볼 수 있다
            또한 2 -> 3 -> 2인 루트가 존재한다면 해당 루트도 사이클로 볼 수있다
            2 -> 2도 사이클로 볼 수 있으나, 경로가 있을 때만 인정되어야 한다!

    만약 사이클이 존재하면 존재하는 사이클 길이의 최소값을
    없으면 -1을 출력해야한다

    문제 이해에 시간이 걸렸을 뿐 풀이는 쉽게 나왔다

    플로이드 워셜 알고리즘의 강점이 모든 지점에서 특정 지점으로 향하는 최단 경로를 표에 기록하는 것이고,
    플로이드 워셜로 나온 표로 사이클 존재를 읽을 수 있는지 문제였다

    플로이드 워셜 알고리즘을 돌아 만들어진 표 dis라 하자
    사이클 1 -> 2 -> 3 -> 4 -> 1 이 존재한다고 가정하자
    그러면 1 -> 2 -> 1 사이클이 존재한다고 할 수 있다!
        1 -> 2는 당연히 존재하고
        2 -> 3 -> 4 -> 1이 2 -> 1로 향하는 경로 중 하나이기 때문이다!
    그래서 사이클 1 -> 2 -> 3 -> 4 -> 1의 최단경로는 dis[1][2] + dis[2][1]값보다 크거나 같다
    비슷하게 사이클 1 -> 3 -> 1, 사이클 1 -> 4 -> 1의 존재성이 보장되고
        사이클 1 -> 2 -> 3 -> 4 -> 1의 최단 경로는 dis[1][3] + dis[3][1], dis[1][4] + dis[4][1] 값보다 크거나 같다!

    즉 사이클 1 -> 2 -> 3 -> 4 -> 1이 존재한다면, 사이클들의 최소값을 찾기 위해서는 사이클 1 -> 2 -> 3 -> 4 -> 1이 아닌,
    사이클 1 -> 2 -> 1, 1 -> 3 -> 1, 1 -> 4 -> 1을 비교해야한다

    이러한 방법으로 접근해서 사이클 탐색 코드가 이중 포문으로 나왔다

    전체 시간 복잡도는 플로이드 워셜 알고리즘을 채우는데, O(N^3)연산,
    그리고 사이클 최소값 찾는 연산 O(N^2)연산 즉, O(N^3)연산으로 해결 가능하다
*/

namespace BaekJoon._33
{
    internal class _33_07
    {

        static void Main7(string[] args)
        {

            const int MAX = 5_000_000;

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int[][] dis = new int[info[0] + 1][];

            for (int i = 1; i < info[0] + 1; i++)
            {

                dis[i] = new int[info[0] + 1];
                for (int j = 1; j < info[0] + 1; j++)
                {

                    if (i == j) dis[i][j] = 0;
                    else dis[i][j] = MAX;
                }
            }

            int result = MAX;

            for (int i = 0; i < info[1]; i++)
            {

                int[] temp = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();


                if (dis[temp[0]][temp[1]] > temp[2]) dis[temp[0]][temp[1]] = temp[2];

                // 자기자신으로 향하는 사이클은 여기서 바로 확인!
                if (temp[0] == temp[1] && result > temp[2]) result = temp[2]; 
            }

            sr.Close();

            // 플로이드 워셜 탐색
            for (int mid = 1; mid <= info[0]; mid++)
            {

                for (int start = 1; start <= info[0]; start++)
                {

                    for (int end = 1; end <= info[0]; end++)
                    {

                        int cur = dis[start][end];

                        if (dis[start][mid] == MAX || dis[mid][end] == MAX) continue;

                        int comp = dis[start][mid] + dis[mid][end];

                        if (cur <= comp) continue;
                            
                        dis[start][end] = comp;
                    }
                }
            }

            // 사이클 탐색
            for (int i = 1; i <= info[0]; i++)
            {

                for (int j = 1; j <= info[0]; j++)
                {

                    // 같은 경우는 앞에서 탐색 완료
                    if (i == j) continue;

                    int g = dis[i][j];
                    int b = dis[j][i];

                    if (g == MAX || b == MAX) continue;

                    if (result > g + b) result = g + b;
                }
            }

            // 출력
            if (result != MAX) Console.WriteLine(result);
            else Console.WriteLine(-1);
        }
    }
}
