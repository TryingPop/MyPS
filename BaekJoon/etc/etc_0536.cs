using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 15
이름 : 배성훈
내용 : 경로 게임
    문제번호 : 12887번

    BFS 문제다
    visit 여부로 결과 체크하다가 두 번 틀렸다
    (장애물 만나도 재 방문을 방지하기위해 true 했었다;)

    이후 0으로 바꾸니 이상없이 통과했다

    아이디어는 다음과 같다
    왼쪽 끝에서 오른쪽 끝으로 이동할 때, 
    최단 경로에 포함된 타일의 수를 찾는다!

    그리고 초기 장애물의 수를 센 뒤에 전체 도로 - 초기 장애물 수 - 최단 경로 타일의 수
    결과가 장애물 설치하는 최대 개수가 된다

    최단 경로는 BFS 탐색으로 찾아갔다
*/

namespace BaekJoon.etc
{
    internal class etc_0536
    {

        static void Main536(string[] args)
        {

            int n;
            string u;
            string d;

            Solve();

            void Solve()
            {

                n = int.Parse(Console.ReadLine());
                u = Console.ReadLine();
                d = Console.ReadLine();

                int min = BFS();

                int obj = 0;
                for (int i = 0; i < n; i++)
                {

                    if (u[i] == '#') obj++;
                    if (d[i] == '#') obj++;
                }

                int ret = 2 * n - obj - min;
                if (min == 10_000) ret = -1;
                Console.WriteLine(ret);
            }

            int BFS()
            {

                int[,] board = new int[2, n];
                bool[,] visit = new bool[2, n];

                Queue<(int r, int c)> q = new(n * 2);

                board[0, 0] = 1;
                board[1, 0] = 1;
                if (u[0] != '#') q.Enqueue((0, 0));
                if (d[0] != '#') q.Enqueue((1, 0));
                int[] dirR = { 0, 1 };
                int[] dirC = { 1, 0 };

                visit[0, 0] = true;
                visit[1, 0] = true;

                while (q.Count > 0)
                {

                    var node = q.Dequeue();

                    for (int i = 0; i < 2; i++)
                    {

                        int nextR = (node.r + dirR[i]) % 2;
                        int nextC = node.c + dirC[i];

                        if (nextC >= n || visit[nextR, nextC]) continue;
                        visit[nextR, nextC] = true;
                        bool obstacle = false;
                        if (nextR == 0) obstacle = u[nextC] == '#';
                        else obstacle = d[nextC] == '#';

                        if (obstacle) continue;

                        q.Enqueue((nextR, nextC));
                        board[nextR, nextC] = board[node.r, node.c] + 1;
                    }
                }

                int ret = 10_000;
                if (board[0, n - 1] != 0) ret = board[0, n - 1];
                if (board[1, n - 1] != 0) ret = ret <= board[1, n - 1] ? ret : board[1, n - 1];
                return ret;
            }
        }
    }

#if other
import java.io.BufferedReader;
import java.io.InputStreamReader;

public class Main {
    static int[] dx = {1, 0, 0};
    static int[] dy = {0, 1, -1};
    static int M;
    static int result;
    static char[][] road;
    static boolean finish;
    public static void main(String[] args) throws Exception {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        M = Integer.parseInt(br.readLine());
        result = M*2;
        road = new char[2][];
        int white = 0;
        for(int i = 0; i < 2; i++) {
            road[i] = br.readLine().toCharArray();
            for(int j = 0; j < M; j++) {
                if(road[i][j] == '.') {
                    white++;
                }
            }
        }

        for(int i = 0; i < 2; i++) {
            finish = false;
            if(road[i][0] == '.') go(i, 0, 1);
        }
        System.out.print(white-result);
    }// end of main

    static void go(int y, int x, int cnt) {
        if(finish) return;
        if(x >= M-1) {
            result = cnt < result? cnt : result;
            finish = true;
            return;
        }
        for(int i = 0; i < 3; i++) {
            int ty = y + dy[i];
            if(ty >= 0 && ty < 2) {
                int tx = x + dx[i];
                if(road[ty][tx] == '.') go(ty, tx, cnt+1);
            }
        }
    }
}//end of class

#endif
}
