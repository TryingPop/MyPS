using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 12
이름 : 배성훈
내용 : 플로이드 2
    문제번호 : 11780번

    플로이드 워셜 문제
    플로이드 워셜 구현 자체는 시간이 얼마 안걸렸다

    다만, 최단 경로 역추적에서 시간을 많이 소모했다

    극단적으로 경로가 2 -> 4 -> 5 -> 1 -> 3만 있을 경우
    2 -> 3인 경우를 보면, 
    3 에는 1이, 1에는 5가, 5에는 4가, 4에는 2가 담기길 원한다
    즉 
        dp[2][3].before = 1, 
        dp[2][1].before = 5,
        dp[2][5].before = 4,
        dp[2][4].before = 2,
    을 원한다!
        
    그런데 mid에서 그냥 dp[start][end].before = mid 코드를 이용했는데
    플로이드 워셜에 의해 
            5 -> 1 -> 3,    dp[5][3].before에 1이 담긴다
            2 -> 4 -> 5,    dp[2][5].before에 4가 담긴다
            2 -> 5 -> 1,    dp[2][1].before에 5가 담긴다
            2 -> 5 -> 3,    dp[2][3].before에 5가 담긴다
            4 -> 5 -> 1,    dp[4][1].before에 5가 담긴다
            4 -> 5 -> 3,    dp[4][3].before에 5가 담긴다
    순서로 before에 값을 넣는다

    dp[2][3].before = 5 != 1 이므로 원하는 경우가 아니다
    실제로 dp[2][3].before = 5를 얻으면 dp[5][3].before을 확인해주는 코드를 넣어줘야한다
    dp[5][3].before에 값이 있는 경우면 dp[dp[5][3].before][3]을 확인하는 코드도 필요하고
    읽는 코드가 점점 복잡해진다

    그래서 넣을 때 dp[start][end].before = dp[mid][end].before를 넣어줬다
    이러한 방법은 순차적으로 쌓여가기에 가장 뒤에 있는 원소를 넣을 수 있다

    그리고, 경로가 없을 경우 0을 출력하는 부분에서 2번이나 틀렸다;
    다음 반례를 보고 수정했다
        4
        4
        1 2 1
        1 3 1
        1 4 1
        2 2 1
*/

namespace BaekJoon._35
{
    internal class _35_08
    {

        static void Main8(string[] args)
        {

            // 100 * 100_000
            const int MAX = 10_000_000;

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int size = int.Parse(sr.ReadLine());

            (int dis, int before)[][] dp = new (int dis, int before)[size + 1][];
            for (int i = 1; i <= size; i++)
            {

                dp[i] = new (int dis, int before)[size + 1];

                for (int j = 1; j <= size; j++)
                {

                    if (i == j) dp[i][j] = (0, 0);
                    else dp[i][j] = (MAX, 0);
                }
            }

            int len = int.Parse(sr.ReadLine());

            for (int i = 0; i < len; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                if (dp[temp[0]][temp[1]].dis < temp[2]) continue;
                // 시작 지점을 before에 넣어준다
                dp[temp[0]][temp[1]] = (temp[2], temp[0]);
            }

            sr.Close();

            // 플로이드 워셜 탐색
            for (int mid = 1; mid <= size; mid++)
            {

                for (int start = 1; start <= size; start++)
                {

                    // dis를 MAX 비교해도 되나 큰 수보다는 작은수로 비교
                    if (dp[start][mid].before == 0) continue;

                    for (int end = 1; end <= size; end++)
                    {

                        // 딱히 필요하지 않다! <<< dp[start][end].dis <= chk에서 걸러지기 때문
                        // if (dp[mid][end].before == 0) continue;

                        int chk = dp[start][mid].dis + dp[mid][end].dis;

                        if (dp[start][end].dis <= chk) continue;

                        dp[start][end].dis = chk;
                        // 뒤에서 부터 읽기에 쓰는 방법
                        dp[start][end].before = dp[mid][end].before;

                        // 처음 오답
                        // dp[start][end].before = mid;
                    }
                }
            }

            // 출력
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            // 플로이드 워셜 표
            for (int start = 1; start <= size; start++)
            {

                for (int end = 1; end <= size; end++)
                {

                    // 문제 조건 맞춰 출력해줘야한다
                    int chk = dp[start][end].dis;
                    if (chk == MAX) chk = 0;
                    sw.Write(chk);
                    sw.Write(' ');
                }

                sw.Write('\n');
            }


            // 경로 역추적
            Stack<int> result = new Stack<int>();
            for (int start = 1; start <= size; start++)
            {

                for (int end = 1; end <= size; end++)
                {

                    int chk = dp[start][end].before;

                    if (chk == 0)
                    {

                        // 경로가 없는 경우
                        sw.Write("0\n");
                        continue;
                    }

                    // 끝 부분은 end로 넣어준다
                    result.Push(end);

                    // 이전 경로 넣어주기
                    // 시작지점도 여기에 포함된다
                    while (chk != 0)
                    {

                        result.Push(chk);
                        chk = dp[start][chk].before;
                    }

                    sw.Write(result.Count);

                    // 역으로 읽어서 stack으로 출력
                    while(result.Count > 0)
                    {

                        sw.Write(' ');
                        sw.Write(result.Pop());
                    }

                    sw.Write('\n');
                }
            }


            sw.Close();
        }
    }
}
