using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 15
이름 : 배성훈
내용 : 방법을 출력하지 않는 숫자 맞추기
    문제번호 : 13392번

    우선 몇 가지 경우의 수로 시도해본 결과
    탐색은 위에서 아래로 그리디로 하면 된다

    만약 위에서 증가로 1번 감소로 1번 돌리는 건
    바로 아래서 증가로 1번 돌리는경우와 같기에 위에서 아래로 탐색한다면 
    그리디로 찾아도 이상없다!

    dp를 어떻게 잡아야할지 몰라서 검색을 했다
    그러니, dp를 인덱스를 i, j로 잡는데
        i : 현재 위에서 몇 번째
        j : 증가로 돌린 횟수

    그리고 값은 최소 횟수로 하면 된다고 했다
    그래서 dp의 기능을 해당 기능으로 잡고 풀었다
    dp접근 방법이 정해지니 풀이 과정이 보였고,

    풀이과정에서 현재층을 구하는데 이전 층의 정보만 필요해서
    따로 i를 안하고 이전층, 현재층을 나타내는 2개의 배열을 재활용하면서 했다

    아래는 설정된 dp대로 푼 코드이다
*/

namespace BaekJoon._48
{
    internal class _48_03
    {

        static void Main3(string[] args)
        {

            // 최대 경우의 수
            int MAX = 100_000;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            string cur = sr.ReadLine();
            string pw = sr.ReadLine();

            sr.Close();

            // idx : cur 값을 증가시킨다
            // 값은 해당 경우의 암호 찾는 최소값!
            int[] ret = new int[10] { MAX, MAX, MAX, MAX, MAX, MAX, MAX, MAX, MAX, MAX };
            int[] dp = new int[10] { MAX, MAX, MAX, MAX, MAX, MAX, MAX, MAX, MAX, MAX };

            {

                // 처음
                int find = pw[0] - cur[0];
                if (9 < find) find -= 10;
                else if (find < 0) find += 10;

                // 감소 연산으로 돌린 경우
                ret[0] = (10 - find) % 10;
                // 증가 연산으로 돌린 경우
                ret[find] = find;
            }

            for (int i = 1; i < len; i++)
            {

                int find = pw[i] - cur[i];
                if (find < 0) find += 10;
                else if (find > 9) find -= 10;

                for (int j = 0; j < 10; j++)
                {

                    // 해당 경우 없음
                    if (ret[j] == MAX) continue;

                    // cur값이 ret의 idx(j)만큼 증가한다
                    // 그래서 find - j이다
                    int right = find - j;
                    if (right < 0) right += 10;
                    else if (right > 9) right -= 10;

                    int left = 10 - right;
                    left = left == 10 ? 0 : left;

                    // 감소 시키는 연산부터 한다
                    int calc = ret[j] + left;
                    // 감소는 현재 idx그대로 유지한다!
                    dp[j] = calc < dp[j] ? calc : dp[j];

                    // 증가시키는 연산
                    // 다음 idx
                    int idx = (right + j) % 10;
                    calc = ret[j] + right;
                    dp[idx] = calc < dp[idx] ? calc : dp[idx];

                    // 재활용 용도
                    ret[j] = MAX;
                }

                // 스왑
                int[] temp = ret;
                ret = dp;
                dp = temp;
            }

            // 이제 최소값 찾기
            int min = MAX;
            for (int i = 0; i < 10; i++)
            {

                min = ret[i] < min ? ret[i] : min; 
            }

            Console.WriteLine(min);
        }
    }

#if other
using System.IO;
using System.Text;
using System;

class Programs
{
    static StreamReader sr = new StreamReader(Console.OpenStandardInput(), Encoding.Default);
    static StreamWriter sw = new StreamWriter(Console.OpenStandardOutput(), Encoding.Default);
    static int[] arr;
    static int[] answer;
    static int n;
    static int[,] dp = new int[10000, 10];
    static int DFS(int current, int leftRotation)//쌓이는 값 위에서 아래로 총 회전할 값이 쌓임
    {
        if (current == n)
        {
            return 0;
        }
        if (dp[current, leftRotation] != -1)
        {
            return dp[current, leftRotation];
        }
        dp[current, leftRotation] = 0;
        //현재 번호에서 타겟 번호까지 최소값을 구함
        //양쪽방향을 구해서 최소값을 리턴
        //우측방향이니까 왼쪽 회전
        int next = (arr[current] + leftRotation) % 10;
        int rotationLeft = (answer[current]-next+20)%10;
        ////좌측방향 우측회전
        int rotationRight =10-rotationLeft;
        //5~6으로 돈다면?해결
        int l = DFS(current + 1, (leftRotation + rotationLeft) % 10);
        int r = DFS(current + 1, leftRotation);
        return dp[current, leftRotation] = Math.Min(rotationLeft + l, r + rotationRight);
    }
    static void Main(String[] args)
    {
        n = int.Parse(sr.ReadLine());
        for (int i = 0; i < 10000; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                dp[i, j] = -1;
            }
        }
        arr = new int[n];
        answer = new int[n];
        string str = sr.ReadLine();
        for (int j = 0; j < n; j++)
        {
            arr[j] = Convert.ToInt32(str[j] - '0');
        }
        str = sr.ReadLine();
        for (int j = 0; j < n; j++)
        {
            answer[j] = Convert.ToInt32(str[j] - '0');
        }
        DFS(0, 0);
        sw.Write(dp[0, 0]);
        sw.Dispose();
    }
}
#endif
}
