using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 1
이름 : 배성훈
내용 : 치즈
    문제번호 : 2636번

    구현, 시뮬레이션 BFS 문제다
    아이디어는 다음과 같다
    두개의 큐를 둔다
    공기에서 시작해서, 공기와 인접한 치즈들을 녹일 예정인 큐에 넣는다
    그리고 방문 처리해서 다시 재진입 못하게 한다
    만약 치즈가 아닌 공기면 계속해서 탐색한다

    이렇게 녹일 예정인 치즈를 모두 찾으면 
    이는 공기와 인접한 치즈 덩어리들이다
    그리고, 모두 녹일 치즈 덩어리 수를 센다
    녹일 치즈 덩어리 수가 0이면, 종료한다

    0이 아닌 경우 탐색 큐와 녹일 예정 큐를 바꾸면된다
    이후 탐색 큐를 BFS 탐색한다
    이후 아직 진입못한 공기와 만난 경우 
    인접한 치즈들을 녹일 예정일 큐에 저장한다
    
    많아도 보드의 크기 x 4의 연산으로 모두 찾을 수 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0931
    {

        static void Main931(string[] args)
        {

            StreamReader sr;
            int row, col;
            int[][] board;
            bool[][] visit;
            Queue<(int r, int c)> q1, q2;
            int[] dirR, dirC;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int ret1 = 0;
                int ret2 = 0;
                q1 = new(row * col);
                q2 = new(row * col);

                q1.Enqueue((0, 0));
                visit[0][0] = true;
                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };

                while(q1.Count > 0)
                {

                    while(q1.Count > 0)
                    {

                        var node = q1.Dequeue();

                        for (int i = 0; i < 4; i++)
                        {

                            int nextR = node.r + dirR[i];
                            int nextC = node.c + dirC[i];

                            if (ChkInvalidPos(nextR, nextC) || visit[nextR][nextC]) continue;
                            visit[nextR][nextC] = true;

                            if (board[nextR][nextC] == 0) q1.Enqueue((nextR, nextC));
                            else q2.Enqueue((nextR, nextC));
                        }
                    }

                    int chk = q2.Count;
                    if (chk == 0) break;
                    ret1++;
                    ret2 = chk;

                    var temp = q1;
                    q1 = q2;
                    q2 = temp;
                }

                Console.Write($"{ret1}\n{ret2}");
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                return _r < 0 || _c < 0 || _r >= row || _c >= col;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                visit = new bool[row][];

                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    visit[r] = new bool[col];
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = ReadInt();
                    }
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;

                while((c = sr.Read()) != -1  && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// #include <stdio.h>

int main()
{
    int N, M, C=0, mC, ft=0, end=0, i, j, h=0;
    scanf("%d %d", &N, &M);
    int Board[N][M], Q[N*M][2], Vis[N][M];
    for(i=0; i<N; i++) for(j=0; j<M; j++){
        scanf("%d", &Board[i][j]);
        if(Board[i][j]==1) C++;
    }
    while(C){
        mC=C; end=0; ft=0; Q[0][0]=0; Q[0][1]=0;
        for(i=0; i<N; i++) for(j=0; j<M; j++) Vis[i][j]=0;
        while(ft<=end){
            if(Q[ft][1]<M-1 && Board[Q[ft][0]][Q[ft][1]+1]!=1 && !Vis[Q[ft][0]][Q[ft][1]+1]){
                Q[++end][0]=Q[ft][0]; Q[end][1]=Q[ft][1]+1;
                Board[Q[ft][0]][Q[ft][1]+1]=2; Vis[Q[ft][0]][Q[ft][1]+1]=1;
            }if(Q[ft][1]>0 && Board[Q[ft][0]][Q[ft][1]-1]!=1 && !Vis[Q[ft][0]][Q[ft][1]-1]){
                Q[++end][0]=Q[ft][0]; Q[end][1]=Q[ft][1]-1;
                Board[Q[ft][0]][Q[ft][1]-1]=2; Vis[Q[ft][0]][Q[ft][1]-1]=1;
            }if(Q[ft][0]<N-1 && Board[Q[ft][0]+1][Q[ft][1]]!=1 && !Vis[Q[ft][0]+1][Q[ft][1]]){
                Q[++end][0]=Q[ft][0]+1; Q[end][1]=Q[ft][1];
                Board[Q[ft][0]+1][Q[ft][1]]=2; Vis[Q[ft][0]+1][Q[ft][1]]=1;
            }if(Q[ft][0]>0 && Board[Q[ft][0]-1][Q[ft][1]]!=1 && !Vis[Q[ft][0]-1][Q[ft][1]]){
                Q[++end][0]=Q[ft][0]-1; Q[end][1]=Q[ft][1];
                Board[Q[ft][0]-1][Q[ft][1]]=2; Vis[Q[ft][0]-1][Q[ft][1]]=1;
            }ft++;
        }
        for(i=1; i<N-1; i++) for(j=1; j<M-1; j++){
            if(Board[i][j]==1 && (Board[i+1][j]==2||Board[i-1][j]==2||Board[i][j+1]==2||Board[i][j-1]==2)){
                C--; Board[i][j]=0;
            }
        }
        h++;
    }
    printf("%d\n%d\n", h, mC);
    return 0;
}
#endif
}
