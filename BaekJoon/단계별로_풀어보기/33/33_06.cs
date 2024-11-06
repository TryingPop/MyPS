using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 3
이름 : 배성훈
내용 : 플로이드
    문제번호 : 11404번

    Floyd Warshall 알고리즘 문제이다
    해당 지점을 경유해 가는 경우 최단 경로일 수 있어서 직행 경로와 비교한다
    비교해서 더 빠른 경로로 갱신한다
    만약 직행 노선이 없다면 가질 수 있는 모든 값들보다 크게 잡는다

    주의할 껀 for문에 쓰는 인덱스의 순서이다
    처음에는 시작, 끝, 중간순서로 했는데
    이 경우 제대로 탐색안되는 경우가 나온다!

    꼭 중간, 시작 - 끝 순서로 for문을 작성해야한다!

    삼중 포문이므로 시간 복잡도는 O(N^3)이다
    그리고 모든 지점에서 가는 최단 경로가 저장하므로 메모리 N^2의 메모리도 사용한다
*/

namespace BaekJoon._33
{
    internal class _33_06
    {

        static void Main6(string[] args)
        {

            // 가질 수 잇는 최대 경우의 수
            // 도시 수  * 최대 비용!
            const int MAX = 20_000_000;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            // 도시의 수
            int posNum = int.Parse(sr.ReadLine());

            int lineNum = int.Parse(sr.ReadLine());

            int[][] pos = new int[posNum + 1][];
            for (int i = 1; i <= posNum; i++)
            {

                pos[i] = new int[posNum + 1];
                for (int j = 1; j <= posNum; j++)
                {

                    if (i == j) pos[i][j] = 0;
                    else pos[i][j] = MAX;
                }
            }

            for (int i = 0; i < lineNum; i++)
            {

                int[] temp = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

                int cur = pos[temp[0]][temp[1]];
                if (cur > temp[2])
                {

                    pos[temp[0]][temp[1]] = temp[2];
                }
            }

            sr.Close();

            // 해당 좌표를 경유해 가는 경우
            for (int mid = 1; mid <= posNum; mid++)
            {

                // 시작 인덱스
                for (int start = 1; start <= posNum; start++)
                {

                    // 끝 인덱스
                    for (int end = 1; end <= posNum; end++)
                    {

                        int cur = pos[start][end];
                        if (pos[start][mid] == MAX || pos[mid][end] == MAX) continue;
                        int comp = pos[start][mid] + pos[mid][end];

                        if (cur > comp) pos[start][end] = comp;
                    }
                }
            }


            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            for (int i = 1; i <= posNum; i++)
            {

                for (int j = 1; j <= posNum; j++)
                {

                    if (pos[i][j] == MAX) sw.Write('0');
                    else sw.Write(pos[i][j]);
                    sw.Write(' ');
                }
                sw.Write('\n');
            }

            sw.Close();
        }
    }
}
