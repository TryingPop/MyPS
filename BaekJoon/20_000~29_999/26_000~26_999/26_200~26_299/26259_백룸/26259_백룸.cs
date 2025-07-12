using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 21
이름 : 배성훈
내용 : 백룸
    문제번호 : 26259번

    dp문제다
    BFS 탐색을 통해 풀었다
    벽이 반대로, 뒤집혀서 들어오는 경우가 있어 한 번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0319
    {

        static void Main319(string[] args)
        {

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int row = ReadInt(sr);
            int col = ReadInt(sr);

            int[,] board = new int[row, col];
            int[,] move = new int[row, col];
            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    board[r, c] = ReadInt(sr);
                    // 가질 수 없는 최솟값이다
                    move[r, c] = -100_000_000;
                }
            }

            int[] wall = new int[4] { ReadInt(sr), ReadInt(sr), ReadInt(sr), ReadInt(sr) };

            if (wall[0] > wall[2])
            {

                int temp = wall[2];
                wall[2] = wall[0];
                wall[0] = temp;
            }
            else if (wall[1] > wall[3])
            {

                int temp = wall[1];
                wall[1] = wall[3];
                wall[3] = temp;
            }

            // 탐색 시작
            Queue<(int r, int c)> q = new(row * col);
            q.Enqueue((0, 0));
            int[] dirR = { 1, 0 };
            int[] dirC = { 0, 1 };
            move[0, 0] = board[0, 0];
            while(q.Count > 0)
            {

                var node = q.Dequeue();

                for (int i = 0; i < 2; i++)
                {

                    // 해당 방향에 벽이 잇는지 부터 판별
                    if (ChkInvalidDir(node.r, node.c, i, wall)) continue;
                    int nextR = node.r + dirR[i];
                    int nextC = node.c + dirC[i];

                    // 맵 안인지 판별
                    if (ChkInvalidPos(nextR, nextC, row, col)) continue;

                    // 현재 값
                    int val = move[node.r, node.c] + board[nextR, nextC];

                    // 가장 큰 값인지 확인
                    if (move[nextR, nextC] >= val) continue;
                    move[nextR, nextC] = val;

                    // 초기값인 경우만 넣는다
                    // 돌아서 올 수 없기에, 한번만 넣어도 된다
                    if (move[row - 1, col - 1] != -100_000_000) continue;
                    q.Enqueue((nextR, nextC));
                }
            }

            // 도착 여부 확인
            if (move[row - 1, col - 1] != -100_000_000)Console.WriteLine(move[row - 1, col - 1]);
            else Console.WriteLine("Entity");
        }

        static bool ChkInvalidDir(int _r, int _c, int _dir, int[] _wall)
        {

            // 지나갈 수 없는 길인지 판별
            if (_wall[0] == _wall[2])
            {

                if (_wall[1] == _wall[3]) return false;

                // 아래 벽이 있고 아래로 향하는 경우다
                if ((_wall[0] - 1 == _r && _dir == 0)
                    && _wall[1] <= _c && _c <= _wall[3] - 1) return true;
                else return false;
            }
            else
            {

                // 오른쪽에 벽이 있고, 오른쪽으로 향하는 경우다
                if ((_wall[1] - 1 == _c && _dir == 1) 
                    && _wall[0] <= _r && _r <= _wall[2] - 1) return true;
                else return false;
            }
        }

        static bool ChkInvalidPos(int _r, int _c, int _row, int _col)
        {

            // 맵 안인지 판별
            if (_r < 0 || _c < 0 || _r >= _row || _c >= _col) return true;
            return false;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            bool plus = true;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                else if (c == '-')
                {

                    plus = false;
                    continue;
                }
                ret = ret * 10 + c - '0';
            }

            return plus ? ret : -ret;
        }
    }


#if other

import java.io.*;
import java.util.Arrays;
import java.util.StringTokenizer;

/**
 * 문제이름 : 백룸
 * 링크 : https://www.acmicpc.net/problem/
 */

public class Main {
    static int N, M;
    static int[][] map;
    static int[][] dp;
    static int x1, y1, x2, y2;

    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        StringTokenizer st = new StringTokenizer(br.readLine());



        N = Integer.parseInt(st.nextToken());
        M = Integer.parseInt(st.nextToken());
        map = new int[N + 1][M + 1];

        for (int i = 1; i <= N; i++) {
            st = new StringTokenizer(br.readLine());
            for (int j = 1; j <= M; j++)
                map[i][j] = Integer.parseInt(st.nextToken());
        }
        st = new StringTokenizer(br.readLine());
        x1 = Integer.parseInt(st.nextToken());
        y1 = Integer.parseInt(st.nextToken());
        x2 = Integer.parseInt(st.nextToken());
        y2 = Integer.parseInt(st.nextToken());

        boolean isRow = x1 == x2;

        if (isPossible(isRow)) {
            x1++;
            x2++;
            y1++;
            y2++;
            dp = new int[N + 1][M + 1];
            for (int i = 0; i < N+1; i++) {
                Arrays.fill(dp[i],-100000000);
            }

            for (int i = 1; i <= N; i++) {
                for (int j = 1; j <= M; j++) {
                     if (i == 1 && j == 1) 
                        dp[i][j] = map[i][j];
                    else if (isRow && i == x1 && j >= y1 && j < y2) {   //가로벽이 존재하고 현재 탐색중인 좌표 위에 벽이 있으면
                        if (dp[i][j - 1] == 0)
                            continue;
                        dp[i][j] = dp[i][j - 1] + map[i][j];
                    } else if (!isRow && j == y1 && i >= x1 && i < x2) {   //세로벽이 존재하고 현재 탐색중인 좌표 왼쪽에 벽이 있으면
                        if (dp[i - 1][j] == 0)
                            continue;
                        dp[i][j] = dp[i - 1][j] + map[i][j];
                    } else {
                        dp[i][j] = Math.max(dp[i - 1][j], dp[i][j - 1]) + map[i][j];
                    }
                }
            }
            System.out.println(dp[N][M]);
        } else {
            System.out.println("Entity");
        }

    }

    public static boolean isPossible(boolean isRow) {
        if (isRow) {
            if (y1 > y2) {  //y1이 더 크면 스왑
                int temp = y1;
                y1 = y2;
                y2 = temp;
            }

            if ((y1 == 0 && y2 == M) && !(x1 == 0 || x1 == N)) //y가 M과 길이가 같고 x가 0 또는 N이 아니면
                return false;

        } else {
            if (x1 > x2) {  //x1이 더 크면 스왑
                int temp = x1;
                x1 = x2;
                x2 = temp;
            }

            if ((x1 == 0 && x2 == N) && !(y1 == 0 || y1 == M))  //x가 N과 길이가 같고 y가 0 또는 M이 아니면
                return false;
        }

        return true;
    }

}
#endif
}
