using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 20
이름 : 배성훈
내용 : 이모티콘
    문제번호 : 14226번

    dp, bfs 문제다
    그냥 다익스트라 경로문제인줄 알아서 다익스트라로 접근했다
    다른 사람 풀이를 찾아보니 보드, 현재 노드를 함께 전달하는 bfs 탐색
    그리고 BFS 문제를 분석해서 N^2에 푸는 dp가 있고
    나처럼 다익스트라로 접근한 사람이 있었다
*/

namespace BaekJoon.etc
{
    internal class etc_0830
    {

        static void Main830(string[] args)
        {

#if first
            int INF = 100_000;

            int END;
            PriorityQueue<int, int> q;
            int[] dp;
            bool[] visit;

            int n;

            Solve();
            void Solve()
            {

                Init();
                BFS();

                // Console.Write(dp[n]);
                GetRet();
            }

            void GetRet()
            {

                for (int i = 0; i <= 1_000; i++)
                {

                    Console.Write($"{dp[i]},");
                    if (i != 0 && i % 100 == 0) Console.Write('\n');
                    else Console.Write(" ");
                }
            }

            void Init()
            {

                // n = int.Parse(Console.ReadLine());
                n = 1_000;
                END = 2 * n;
                visit = new bool[END];
                dp = new int[END];
                q = new(END * n);

                Array.Fill(dp, INF);
            }

            void BFS()
            {

                END = 2 * n;

                q.Enqueue(1, 0);
                dp[1] = 0;
                visit[0] = true;
                dp[0] = 1;

                while(q.Count > 0)
                {

                    int node = q.Dequeue();
                    if (visit[node]) continue;
                    visit[node] = true;

                    int turn = dp[node] + 1;
                    for (int next = node * 2; next < END; next += node)
                    {

                        turn++;
                        if (dp[next] < turn) continue;
                        dp[next] = turn;
                        q.Enqueue(next, turn);
                    }

                    turn = dp[node];
                    for (int next = node - 1; next > 0; next--)
                    {

                        turn++;
                        if (dp[next] < turn) continue;
                        dp[next] = turn;
                        q.Enqueue(next, turn);
                    }
                }
            }
#else

            Solve();
            void Solve()
            {

                int[] ret = new int[1_001] { 1, 0, 2, 3, 4, 5, 5, 7, 6, 6, 7, 8, 7, 10, 9, 8, 8, 9, 8, 10, 9, 10, 10, 10, 9, 10, 10, 9, 11, 11, 10, 11, 10, 11, 11, 11, 10, 13, 12, 12, 11, 13, 12, 13, 12, 11, 12, 12, 11, 13, 12, 12, 12, 12, 11, 13, 13, 13, 13, 13, 12, 14, 13, 13, 12, 14, 13, 14, 13, 13, 13, 13, 12, 15, 14, 13, 14, 14, 13, 14, 13, 12, 15, 15, 14, 14, 15, 14, 14, 14, 13, 15, 14, 14, 14, 14, 13, 16, 15, 14, 14,
                                            15, 14, 15, 14, 14, 14, 14, 13, 16, 15, 16, 15, 16, 15, 15, 15, 15, 15, 15, 14, 17, 16, 16, 15, 15, 15, 15, 14, 16, 15, 16, 15, 16, 15, 14, 15, 16, 15, 16, 15, 15, 15, 15, 14, 16, 17, 16, 16, 16, 15, 17, 16, 15, 16, 16, 15, 17, 16, 15, 15, 15, 14, 18, 17, 16, 17, 17, 16, 17, 16, 16, 17, 17, 16, 16, 16, 16, 16, 16, 15, 18, 17, 17, 16, 17, 16, 17, 16, 16, 16, 16, 15, 19, 18, 17, 17, 17, 16, 17, 16,
                                            17, 17, 17, 16, 18, 17, 16, 16, 17, 16, 17, 16, 16, 16, 16, 15, 18, 18, 18, 17, 18, 17, 18, 17, 16, 18, 18, 17, 18, 17, 17, 17, 17, 16, 17, 17, 17, 17, 17, 16, 17, 16, 15, 18, 18, 18, 18, 17, 18, 17, 18, 17, 18, 17, 17, 16, 19, 18, 18, 17, 17, 18, 18, 17, 17, 18, 17, 17, 17, 16, 18, 17, 18, 18, 18, 17, 19, 18, 17, 17, 18, 17, 18, 17, 17, 17, 17, 16, 19, 18, 19, 19, 19, 18, 18, 18, 17, 18, 18, 17,
                                            20, 19, 18, 18, 18, 17, 19, 18, 18, 18, 18, 17, 19, 18, 17, 18, 18, 17, 18, 17, 17, 17, 17, 16, 19, 20, 19, 19, 19, 18, 20, 19, 19, 19, 19, 18, 20, 19, 19, 18, 19, 18, 20, 19, 18, 19, 19, 18, 19, 18, 18, 18, 19, 18, 18, 18, 18, 18, 18, 17, 21, 20, 20, 19, 20, 19, 19, 18, 19, 19, 19, 18, 20, 19, 18, 18, 19, 18, 19, 18, 18, 18, 18, 17, 19, 20, 19, 20, 19, 18, 20, 19, 19, 19, 19, 18, 20, 19, 19, 18,
                                            19, 18, 19, 18, 17, 19, 19, 18, 21, 20, 19, 19, 19, 18, 19, 18, 19, 19, 19, 18, 20, 19, 18, 18, 19, 18, 19, 18, 18, 18, 18, 17, 21, 20, 19, 20, 21, 20, 20, 19, 19, 20, 20, 19, 19, 20, 19, 19, 19, 18, 21, 20, 20, 20, 20, 19, 20, 19, 18, 19, 20, 19, 20, 19, 19, 19, 19, 18, 20, 19, 20, 19, 20, 19, 19, 19, 18, 19, 19, 18, 20, 19, 18, 18, 18, 17, 21, 20, 21, 20, 21, 20, 21, 20, 19, 19, 20, 20, 20, 19,
                                            20, 20, 20, 19, 20, 20, 20, 19, 20, 19, 19, 18, 19, 21, 20, 20, 21, 20, 20, 19, 20, 19, 21, 20, 19, 20, 20, 19, 20, 19, 19, 20, 20, 19, 19, 19, 19, 19, 19, 18, 21, 20, 20, 19, 21, 20, 21, 20, 20, 20, 20, 19, 21, 21, 20, 20, 20, 19, 20, 19, 20, 20, 20, 19, 21, 20, 19, 19, 20, 19, 20, 19, 19, 19, 19, 18, 22, 21, 21, 20, 22, 21, 22, 21, 20, 21, 21, 20, 21, 20, 20, 20, 20, 19, 20, 20, 20, 20, 20, 19,
                                            22, 21, 20, 21, 21, 20, 21, 20, 20, 20, 20, 19, 22, 21, 21, 20, 21, 20, 21, 20, 19, 20, 20, 19, 20, 21, 20, 20, 20, 19, 21, 20, 20, 20, 20, 19, 21, 20, 19, 19, 20, 19, 20, 19, 19, 19, 19, 18, 21, 20, 21, 22, 22, 21, 21, 21, 21, 21, 21, 20, 23, 22, 21, 21, 21, 20, 22, 21, 21, 20, 21, 20, 21, 20, 19, 21, 22, 21, 21, 20, 21, 21, 21, 20, 21, 22, 21, 21, 21, 20, 22, 21, 20, 21, 21, 20, 22, 21, 20, 20,
                                            20, 19, 21, 20, 20, 21, 21, 20, 21, 20, 20, 20, 21, 20, 20, 20, 20, 20, 20, 19, 22, 21, 20, 21, 20, 19, 20, 19, 18, 22, 22, 21, 22, 21, 21, 20, 22, 21, 22, 21, 21, 21, 21, 20, 21, 22, 21, 21, 21, 20, 21, 20, 21, 21, 21, 20, 22, 21, 21, 20, 21, 20, 21, 20, 20, 20, 20, 19, 22, 21, 22, 22, 22, 21, 21, 22, 21, 21, 21, 20, 22, 21, 20, 21, 22, 21, 22, 21, 21, 21, 21, 20, 22, 21, 20, 21, 22, 21, 21, 20,
                                            20, 21, 21, 20, 20, 21, 20, 20, 20, 19, 22, 21, 21, 21, 21, 20, 23, 22, 21, 22, 22, 21, 22, 21, 21, 21, 21, 20, 22, 21, 21, 20, 22, 21, 22, 21, 20, 21, 21, 20, 23, 22, 21, 21, 21, 20, 21, 20, 21, 21, 21, 20, 22, 21, 20, 20, 21, 20, 21, 20, 20, 20, 20, 19, 22, 23, 22, 22, 22, 21, 23, 22, 22, 22, 21, 22, 23, 22, 22, 21, 22, 21, 23, 22, 21, 22, 22, 21, 22, 21, 20, 22, 22, 21, 21, 21, 21, 21, 21, 20,
                                            24, 23, 23, 22, 23, 22, 23, 22, 21, 22, 22, 21, 23, 22, 21, 21, 21, 20, 22, 21, 22, 22, 22, 21, 22, 22, 21, 21, 22, 21, 22, 21, 21, 21, 21, 20, 23, 22, 22, 21, 22, 21, 22, 21, 20, 22, 22, 21, 22, 21, 21, 21, 21, 20, 21, 21, 21, 21, 21, 20, 22, 21, 20, 21, 21, 20, 21, 20, 20, 20, 20, 19, 23, 23, 22, 22, 24, 23, 23, 22, 22, 23, 23, 22, 22, 23, 22, 22, 22, 21, 22, 21, 23, 22, 22, 22, 23, 22, 22, 21
                };
                int n = int.Parse(Console.ReadLine());

                Console.Write(ret[n]);
            }
#endif
        }
    }
#if other
using System;
using System.Collections.Generic;

namespace Baekjoon_14226
{
    class Program
    {
        static void Main()
        {
            int s = int.Parse(Console.ReadLine()), s2 = s * 2;
            bool[,] visit = new bool[s2, s];
            Queue<int[]> q = new Queue<int[]>();
            visit[1, 1] = true;
            q.Enqueue(new int[] { 1, 1, 1 });
            while (q.Count > 0)
            {
                int[] state = q.Dequeue();
                int now = state[0], copy = state[2], next = now + copy, t = state[1];
                if (now == s)
                {
                    Console.WriteLine(t);
                    return;
                }
                t++;
                if (next < s2 && !visit[next, copy])
                {
                    visit[next, copy] = true;
                    q.Enqueue(new int[] { next, t, copy });
                }
                if (now < s && !visit[now, now])
                {
                    visit[now, now] = true;
                    q.Enqueue(new int[] { now, t, now});
                }
                next = now - 1;
                if (next > 0 && !visit[next, copy])
                {
                    visit[next, copy] = true;
                    q.Enqueue(new int[] { next, t, copy });
                }
            }
        }
    }
}

#elif other2
using System;
using System.Collections.Generic;

namespace BaekJoon
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            var targetNum = numbers[0];

            var visited = new bool[2001, 2001];
            visited[1, 0] = true;
            visited[1, 1] = true;

            var queue = new Queue<(int, int, int)>();
            queue.Enqueue((1, 1, 1));   // 처음 무조건 클립보드에 복사를 해야함

            while (queue.Count > 0)
            {
                var (emoji, time, board) = queue.Dequeue();
                if (emoji < 0)
                    continue;

                if (emoji == targetNum)
                {
                    Console.WriteLine(time);
                    return;
                }

                if (!visited[emoji, emoji])
                {
                    visited[emoji, emoji] = true;
                    queue.Enqueue((emoji, time + 1, emoji));
                }

                if (emoji < targetNum && !visited[emoji + board, board])
                {
                    var case1 = emoji + board;
                    queue.Enqueue((case1, time + 1, board));
                    visited[case1, board] = true;
                }
                if (emoji > 1 && !visited[emoji - 1, board])
                {
                    var case2 = emoji - 1;
                    queue.Enqueue((case2, time + 1, board));
                    visited[case2, board] = true;
                }
            }
        }
    }
}
#elif other3
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prob14226
{
    internal class 이모티콘
    {
        static void Main(string[] args)
        {
            int s = int.Parse(Console.ReadLine());
            // BFS 활용
            // 방문했는지 표시를 해주는 변수 (이모티콘, 클립보드)
            bool[,] enter = new bool[1001, 1001];
            // que에 (이모티콘, 클립보드, 시간)을 담기위해 튜플형태로 담는다.
            Queue<(int, int, int)> que = new Queue<(int,int, int)>();
            // 스타트 국룰
            que.Enqueue((1, 0, 0));
            enter[1, 0] = true;

            while(que.Count > 0) 
            {
                var (emote, clipboard, time) = que.Dequeue();
                if(emote == s)
                {
                    Console.WriteLine(time);
                    return;
                }

                // 클립보드 저장
                if (!enter[emote, emote])
                {
                    enter[emote, emote] = true;
                    que.Enqueue((emote, emote, time + 1));
                }

                // 클립보드 붙여넣기
                // 조건 설명
                // 1. 클립보드에 값이 있어야한다.
                // 2. 클립보드를 붙여넣었을때 조건중 하나인 1000을 넘어서는 안된다.
                // 3. 붙여넣었을 때 이미 방문한 곳이 아니여야 한다.
                if(clipboard > 0 && emote + clipboard <= 1000 && !enter[emote + clipboard, clipboard])
                {
                    enter[emote + clipboard, clipboard] = true;
                    que.Enqueue((emote + clipboard, clipboard, time + 1));
                }

                // 이모티콘 삭제
                if(emote > 1 && !enter[emote - 1, clipboard])
                {
                    enter[emote - 1, clipboard] = true;
                    que.Enqueue((emote - 1, clipboard, time +1));
                }
            }
        }
    }
}

#elif other4
// #include <stdio.h>
// #include <stdlib.h>

int main(void)
{
    int s, tem;
    scanf("%d", &s);
    int *arr = malloc(sizeof(int)*(s+1));
    for (int i = 1; i <= s ; i++) arr[i] = i;
    for (int i = 2 ; i <= s ; i++)
    {
        arr[i] += 1;
        for (int j = i ; j <= s ; j++)
        {
            tem = arr[i];
            if (j % i == 0)
                tem += (j / i - 1);
            else
                tem += (j / i + i - j % i);
            if (tem < arr[j])
                arr[j] = tem;

        }
    }
    printf("%d", arr[s]-1);
    free(arr);
}
#elif other5
// #include <stdio.h>
// #include <math.h>
// #include <string.h>

const int MAX = 1002;
int dp[MAX];

int main() {
    int S, j, flag = 0;

    scanf("%d", &S);
    memset(dp, 10000, sizeof(int) * MAX);
    
    dp[0] = 0;
    dp[1] = 0;

    for(int i = 1 ; i < MAX-1 ; i++) {
        for(int j = 1 ; j <= i ; j++) {
            if(!(i % j)) {
                dp[i] = dp[i] > dp[j] + i/j ? dp[j] + i/j : dp[i];
                dp[i-1] = dp[i-1] > (dp[i]+1) ? (dp[i]+1) : dp[i-1];
            }
        }
    }


    for(int i = MAX-2 ; i > 1 ; i--) {
        if(dp[i] > (dp[i+1] + 1)) {
            dp[i] = dp[i+1] + 1;
        }
    }
    printf("%d\n", dp[S]);
}
#endif
}
