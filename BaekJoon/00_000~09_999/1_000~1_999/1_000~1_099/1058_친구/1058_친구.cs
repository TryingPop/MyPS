using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 17
이름 : 배성훈
내용 : 친구
    문제번호 : 1058번

    브루트 포스, 플로이드 - 워셜 문제다
    처음에는 플로이드 워셜로 접근하려고 했다
    조건에 맞게 구현하고 싶어 삼중 포문으로 접근해봤다
    만들고 나서 보니, 플로이드 워셜과 아주 유사한 형태가 되었다;

    해당 for문에서
        for (int i; ;)
            for(int j; ;)
                for (int k; ;)
    
        가장 바깥쪽은 변수 i의 for문, 중앙 j의 for문, 가장 안쪽은 k의 for문 의미로 썼다

    여기서는 시작지점을 의미로 맨 바깥에 두었다
    그리고 거쳐 지나가는 곳을 중앙, 도착지점을 맨 안쪽에 두었다
    주석친 부분이다

    반면 플로이드 워셜은 거쳐 가는 곳을 가장 바깥쪽,
    시작지점을 중앙, 끝지점을 마지막에 두어 포문을 돌린다

    둘의 차이는 경험상 전자로 길이가 3 이상인 경우를 제출하면
    플로이드 워셜문제에서 모든 붙인 경우를 못찾았다
*/

namespace BaekJoon.etc
{
    internal class etc_0271
    {

        static void Main271(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = int.Parse(sr.ReadLine());

            int[,] board = new int[n + 1, n + 1];

            for (int i = 1; i <= n; i++)
            {

                for (int j = 1; j <= n; j++)
                {

                    int c = sr.Read();
                    board[i, j] = c == 'Y' ? 1 : 0;
                }

                sr.ReadLine();
            }

            sr.Close();

            // 현재 i
            for (int i = 1; i <= n; i++)
            {

                // 다음 j
                for (int j = 1; j <= n; j++)
                {

                    // 다린 건넌 친구 찾기
                    if (board[i, j] != 1) continue;

                    for (int k = 1; k <= n; k++)
                    {

                        // 중복 진입 방지
                        // if (i == k || board[i, k] == 1 || board[j, k] != 1) continue;
                        if (j == k || board[i, k] != 1 || board[j, k] == 1) continue;

                        // 2 - 친구
                        board[j, k] = 2;
                        board[k, j] = 2;
                    }
                }
            }

            // 개수 세기
            int ret = 0;
            for (int i = 1; i <= n; i++)
            {

                for (int j = 1; j <= n; j++)
                {

                    if (board[i, j] > 0) board[i, 0]++;
                }

                ret = ret < board[i, 0] ? board[i, 0] : ret;
            }

            Console.WriteLine(ret);
        }
    }
}
