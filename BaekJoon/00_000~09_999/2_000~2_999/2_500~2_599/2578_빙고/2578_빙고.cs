using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 17
이름 : 배성훈
내용 : 빙고
    문제번호 : 2578번

    5 * 5 빙고게임에서 구현 문제
*/

namespace BaekJoon.etc
{
    internal class etc_0049
    {

        static void Main49(string[] args)
        {

            int ALLSIZE = 25;
            int SIZE = 5;
            int BINGO = 3;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            (int y, int x)[] nTop = new (int y, int x)[ALLSIZE + 1];
            int[][] board = new int[SIZE][];

            for (int i = 0; i < SIZE; i++)
            {

                board[i] = new int[SIZE];
                for (int j = 0; j < SIZE; j++)
                {

                    // 숫자 채워넣기
                    int n = ReadInt(sr);
                    board[i][j] = n;
                    nTop[n] = (i, j);
                }
            }

            // 이제 빙고 4개 입력
            // 5개부터 빙고가 나올 수 있다
            for (int i = 1; i < SIZE; i++)
            {

                int n = ReadInt(sr);
                var cur = nTop[n];

                // 보드 색칠
                board[cur.y][cur.x] = -1;
            }

            int turn = 0;           // 빙고 외치는 턴
            int cnt = 0;            // 현재 빙고 수

            // 0 ~ 4 : 행 빙고(가로)
            // 5 ~ 9 : 열 빙고(세로)
            // 10, 11 : 대각선 빙고 \, / 순서
            bool[] bingo = new bool[2 * SIZE + 2];
            for (int i = SIZE; i <= ALLSIZE; i++)
            {

                int n = ReadInt(sr);

                var cur = nTop[n];
                // 보드 색칠
                board[cur.y][cur.x] = -1;

                // 가로 방향 체크
                if (!bingo[cur.y])
                {

                    bool chk = true;
                    int fix = cur.y;
                    for (int j = 0; j < SIZE; j++)
                    {

                        if (board[fix][j] != -1)
                        {

                            chk = false;
                            break;
                        }
                    }

                    if (chk) 
                    { 
                        
                        bingo[fix] = true;
                        cnt++;
                    }
                }

                // 세로 방향 체크
                if (!bingo[cur.x + SIZE])
                {

                    bool chk = true;
                    int fix = cur.x;

                    for (int j = 0; j < SIZE; j++)
                    {

                        if (board[j][fix] != -1)
                        {

                            chk = false;
                            break;
                        }
                    }

                    if (chk)
                    {

                        bingo[fix + SIZE] = true;
                        cnt++;
                    }
                }

                // 대각선 체크
                // \ 방향
                if (cur.x == cur.y && !bingo[SIZE * 2])
                {

                    bool chk = true;
                    for (int j = 0; j < SIZE; j++)
                    {

                        if (board[j][j] != -1)
                        {

                            chk = false;
                            break;
                        }
                    }

                    if (chk)
                    {

                        bingo[SIZE * 2] = true;
                        cnt++;
                    }
                }

                // 대각선 체크
                // / 방향
                if (cur.x + cur.y == SIZE - 1 && !bingo[SIZE * 2 + 1])
                {

                    bool chk = true;
                    for (int j = 0; j < SIZE; j++)
                    {

                        if (board[j][SIZE - 1 - j] != -1)
                        {

                            chk = false;
                            break;
                        }
                    }

                    if (chk)
                    {

                        bingo[SIZE * 2 + 1] = true;
                        cnt++;
                    }
                }

                // 3개 이상이면 탈출
                if (cnt >= BINGO)
                {

                    turn = i;
                    break;
                }
            }

            sr.Close();

            Console.WriteLine(turn);
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;

            int c;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
