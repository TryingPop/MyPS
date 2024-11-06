using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 15
이름 : 배성훈
내용 : 미로에 갇힌 상근
    문제번호 : 5069번

    dp 문제다
    처음에는 규칙 찾으려고 해서, 6단계까지 수작업했다;
    알아낸건 매회 끝 부분의 수열이 조합값과 같다는거 말고는 없다...

    그리고 검색을 해서 다른 사람 힌트를 봤고
    탐색이 필요함을 알았다

    그래서 처음에는 BFS로 값이 담긴 곳만 탐색할까 하다가
    이중 포문도 시간이 얼마 안걸릴거 같아 이중포문으로 먼저 제출했는데,
    일단 속도 비교를 위해 결과값만 기록해서 제출먼저 해서 맞췄다
    그리고 이중 포문으로 제출했는데 시간차이가 없어, BFS 탐색은 안했다

    그리고 다른 사람의 풀이를 보니, 탐색 크기를 더 줄일 수 있어보인다
*/

namespace BaekJoon.etc
{
    internal class etc_0537
    {

        static void Main537(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()));

            int[] ret = new int[15];
            // int[] ret = { 1, 0, 6, 12, 90, 360, 2040, 10080, 54810, 290640, 1588356, 8676360, 47977776, 266378112, 1488801600 };
            Solve();

            sr.Close();
            sw.Close();

            void Solve()
            {

                BFS();
                int test = ReadInt();
                while(test-- > 0)
                {

                    int n = ReadInt();
                    sw.WriteLine(ret[n]);
                }
            }


            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }

            void BFS()
            {

                int[,] board1 = new int[60, 30];
                int[,] board2 = new int[60, 30];
                int midR = 30;
                int midC = 15;

                board1[midR, midC] = 1;
                int[] dirR = { 2, 1, -1, -2, -1, 1 };
                int[] dirC = { 0, 1, 1, 0, -1, -1 };
                board1[midR, midC] = 1;

                for (int t = 1; t <= 14; t++)
                {

                    for (int r = 0; r < 60; r++)
                    {

                        for (int c = 0; c < 30; c++)
                        {

                            int cur = board1[r, c];
                            if (cur == 0) continue;
                            board1[r, c] = 0;
                            for (int k = 0; k < 6; k++)
                            {

                                int nextR = r + dirR[k];
                                int nextC = c + dirC[k];

                                board2[nextR, nextC] += cur;
                            }
                        }
                    }

                    ret[t] = board2[midR, midC];
                    int[,] temp = board1;
                    board1 = board2;
                    board2 = temp;
                }
            }
        }
    }

#if other
// #include <stdio.h>

int N, D[14][15][15];
int dx[] = { -1, -1, 0, 0, 1, 1 };
int dy[] = { 0, 1, 1, -1, 0, -1 };
int Rec(int n, int x, int y) {
	if (x > 7 || x < -7 || y > 7 || y < -7) {
		return 0;
	}
	if(n >= N) {
		return x == 0 && y == 0;
	}

	int& ret = D[n][x + 7][y + 7];
	if (ret != -1)	return ret;

	ret = 0;
	for (int i = 0; i < 6; ++i) {
		ret += Rec(n + 1, x + dx[i], y + dy[i]);
	}
    return ret;
}

int main() {
	int TC;
	scanf("%d", &TC);
	for (int tc = 1; tc <= TC; ++tc) {
		scanf("%d", &N);

		int* d = (int*)D;
		for (int i = 0; i < sizeof(D) / sizeof(int); ++i)
			d[i] = -1;

		printf("%d\n", Rec(0, 0, 0));
	}
	return 0;
}
#elif other2
import sys,math,heapq,time
input=sys.stdin.readline
from collections import deque
LMI=lambda:list(map(int,input().split()))
LMS=lambda:list(map(str,input().split()))
MI=lambda:map(int,input().split())
I=lambda:int(input())
GI=lambda x:[ LMI() for _ in range(x) ]
GS=lambda x:[ LMS() for _ in range(x) ]
V=lambda x,y:[ [False]*y for _ in range(x) ]

"""
0 0 1 0 0
0 1 0 1 0
0 0 X 0 0
0 1 0 1 0
0 0 1 0 0

벌집을 2차원 그리디로 나타냈을때 위 그림처럼 X 에서 이동할수 있는칸은 값이 1 이다.

"""


def Honeycomb_Walk():

    dp[1][30-1][30-1] = 1
    dp[1][30+1][30-1] = 1
    dp[1][30-1][30+1] = 1
    dp[1][30+1][30+1] = 1
    dp[1][30+2][30] = 1
    dp[1][30-2][30] = 1

    for i in range(2,15):
        for j in range(60):
            for k in range(60):
                if dp[i-1][j][k] != 0:
                    for x, y in [(-1,-1) , (1,-1) , (-1,1) , (1,1) , (2,0) , (-2,0)]:

                        dp[i][j+x][k+y] += dp[i-1][j][k]

dp = [[[0] * 60 for _ in range(60)] for _ in range(15)]
Honeycomb_Walk()
for i in range(I()):
    print(dp[I()][30][30])
#elif other3
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.*;

public class Main { 
    public static BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
    public static int N;
    public static int[][][] dp = new int[15][30][30];
    public static final int[] dy = {1, -1, 0, 0, 1, -1};
    public static final int[] dx = {0, 0, 1, -1, 1, -1};
    
    public static void main(String[] args) throws IOException{
        int T = Integer.parseInt(br.readLine());
        for(int i = 0; i < 15; i++)
            for(int j = 0; j < 30; j++)
                Arrays.fill(dp[i][j], -1);
        while(T-- > 0){
            N = Integer.parseInt(br.readLine());
            System.out.println(solve(N, 15, 15));
        }
    }
    
    public static int solve(int here, int y, int x){
        if(here == 0) {
            if(y == 15 && x == 15) return 1;
            return 0;
        }
        if(dp[here][y][x] != -1) return dp[here][y][x];
        
        int ret = 0;
        
        for(int dir = 0; dir < 6; dir++){
            int ny = y + dy[dir];
            int nx = x + dx[dir];
            ret += solve(here-1, ny, nx);
        }
        
        return dp[here][y][x] = ret;
    }
}
#endif
}
