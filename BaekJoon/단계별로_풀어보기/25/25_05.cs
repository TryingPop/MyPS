using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 23
이름 : 배성훈
내용 : N-Queen
    문제번호 : 9663번
*/

namespace BaekJoon._25
{
    internal class _25_05
    {

        static void Main5(string[] args)
        {

            int input = int.Parse(Console.ReadLine());
            int result = 0;

#if first
            bool[,] board = new bool[input, input];
#elif seconds
            int[,] board = new int[input, input];
#elif third

            int[] xPos = new int[input];

            Back(xPos, ref result);
#elif true

#endif
            bool[] xPos = new bool[input];          // col 위치 저장
            bool[] lDia = new bool[2 * input];      // 대각선의 성질에 의해 x + y로 분할된다!
            bool[] rDia = new bool[2 * input];      // 마찬가지로 x - y로 분할된다!

            Back(xPos, lDia, rDia, ref result);

#if first || seconds

            Back(board, ref result);
#endif

            Console.WriteLine(result);
        }

#if first
        // 시간 초과 방법
        // 1 ~ 14까지 결과를 백준에 넣은 결과 성공

        // 매 위치마다 놓을 수 있는지 검사
        // 반환값이 false : 못 놓는다, true : 놓을 수 있다
        static bool ChkBoard(bool[,] board, int row, int col)
        {

            // 가로, 세로 검사
            for (int i = 0; i < board.GetLength(0); i++)
            {
                
                // 가로 검사
                if (board[i, col])
                {

                    return false;
                }

                // 세로 검사
                if (board[row, i])
                {

                    return false;
                }
            }

            // 역슬러시 방향의 대각선 검사
            // 역슬러시로 반으로 쪼갰을 때 위쪽 삼각형 검사
            if (row >= col)
            {

                for (int i = row - col; i < board.GetLength(0); i++)
                {

                    if (board[i, col - row + i])
                    {

                        return false;
                    }
                }
            }
            // 역슬래쉬 아래쪽 삼각형
            else
            {

                for (int i = col - row; i < board.GetLength(0); i++)
                {

                    if (board[row  - col + i, i])
                    {

                        return false;
                    }
                }
            }

            // 슬레쉬 (/) 방향으로 대각선 검사
            // 쪼갰을 때 위쪽 삼각형 검사
            if (row + col <= board.GetLength(0) - 1)
            {

                for (int i = row +col; i >= 0; i--)
                {

                    if (board[i, row + col - i])
                    {

                        return false;
                    }
                }
            }
            // 아래쪽 삼각형 검사
            else
            {

                for (int i = (row + col) - board.GetLength(0) + 1; i <= board.GetLength(0) -1; i++)
                {

                    if (board[i, row + col - i])
                    {

                        return false;
                    }
                } 
            }

            return true;
        }

        // step과 col에 값을 주면 원하는 결과가 안나온다
        static void Back(bool[,] board, ref int result, int step = 0, int col = 0)
        {
            
            // 안전하게 놓은게 N개다 (N회 돈 경우)
            if (step >= board.GetLength(0))
            {

                result++;
                return;
            }

            // 가로
            for (int i = 0; i < board.GetLength(0); i++)
            {

                // 세로
                for (int j = col; j < board.GetLength(1); j++)
                {

                    // 놓을 수 있는 위치인가?
                    if (ChkBoard(board, i, j))
                    {
                        
                        // 퀸 놓는다
                        board[i, j] = true;

                        // 다음으로
                        Back(board, ref result, step + 1, j + 1);

                        // 해당 경우의 수를 모두 탐색했으므로 false를 놓고 탈출
                        board[i, j] = false;
                    }
                }
            }
        }
#elif seconds
        // 아슬아슬하게 세이프
        // 처음보다는 쪼끔 나아졌지만 여전히 안좋은 방법
        // 아직까지는 판을 기준으로 기록했다

        // 판의 값이 0인 경우 놓을 수 있는 의미로 바뀐다
        // 반면 1인 경우는 다른 퀸에게 죽는 자리
        // 음수는 현재 로직상 못나온다!
        // change가 false인 경우 해제, true면 설정
        static void ChangeBoard(int[,] board, int row, int col, bool change)
        {

            // 가로 세로에 퀸 죽는 자리 값 추가
            for (int i = 0; i < board.GetLength(0); i++)
            {

                // 가로
                board[i, col] += change ? 1 : -1;
                // 세로
                board[row, i] += change ? 1: -1;
            }

            // 역슬래쉬 방향의 대각선 판정
            // 위쪽
            if (row >= col)
            {

                for (int i = row - col; i < board.GetLength(0); i++)
                {

                    board[i, col - row + i] += change ? 1 : -1;
                }
            }
            // 아래쪽
            else
            {

                for (int i = col - row; i < board.GetLength(0); i++)
                {

                    board[row - col + i, i] += change ? 1 : -1;
                }
            }

            // 슬래쉬 방향 대각선판정 
            // 위쪽
            if (row + col <= board.GetLength(0) - 1)
            {

                for (int i = row + col; i >= 0; i--)
                {

                    board[i, row + col - i] += change ? 1 : -1;
                }
            }
            // 아래쪽
            else
            {

                for (int i = (row + col) - board.GetLength(0) + 1; i <= board.GetLength(0) - 1; i++)
                {

                    board[i, row + col - i] += change ? 1 : -1;
                }
            }
        }


        static void Back(int[,] board, ref int result, int step = 0)
        {
            
            // 탈출
            if (step >= board.GetLength(0))
            {

                // 안전하게 N개를 놓았다
                result++;
                return;
            }

            // 퀸 놓기
            // step을 세로 값으로 활용해서 가로만 검색
            // 가로가 모두 죽기에 가로만 검색해서 나오는 결과나 가로 세로 모두 검색해서 나오는 결과나 같다
            for (int i = 0; i < board.GetLength(0); i++)
            {

                // 놓을 수 있는지 판별
                if (board[i, step] == 0)
                {
                    
                    // 놓았다
                    ChangeBoard(board, i, step, true);

                    // 해당 경우의 수 검색
                    Back(board, ref result, step + 1);

                    // 경우의 수를 모두 검색했으므로 탈출
                    ChangeBoard(board, i, step, false);
                }
            }
        }

#elif third
        // 말의 좌표를 저장해 놓을 수 있는지 판정
        // 대각선을 일일히 판정하는게 아닌 계산 한 번으로 끝난다
        // 앞보다 절반 정도로 빨라졌다
        static void Back(int[] xPos, ref int result, int step = 0)
        {

            if (step >= xPos.Length)
            {

                result++;
                return;
            }

            // 가로는 이미 step으로 판정 된다
            for (int i = 0; i < xPos.Length; i++)
            {

                bool chk = false;
                
                for (int j = 0; j < step; j++)
                {

                    // 세로 검사, 슬래쉬(/) 방향의 대각선 판정, 역슬래쉬 방향의 대각선 판정
                    if (xPos[j] == i || (j - step) == (xPos[j] - i) || (j - step) == -(xPos[j] - i))
                    {

                        chk = true;
                        break;
                    }
                }

                if (chk) continue;

                xPos[step] = i;
                Back(xPos, ref result, step + 1);
            }
        }
#elif true
        // 이젠 대각선을 좀 더 보기 좋게 바꿔보자!
        // 절반 더 빨라졌다!
        static void Back(bool[] xPos, bool[] lDia, bool[] rDia, ref int result, int step = 0)
        {

            if (step >= xPos.Length)
            {

                result++;
                return;
            }

            for (int i = 0; i < xPos.Length; i++)
            {

                if (!xPos[i] && !lDia[step + i] && !rDia[xPos.Length + i - step])
                {

                    xPos[i] = true;
                    lDia[step + i] = true;
                    rDia[xPos.Length + i - step] = true;

                    Back(xPos, lDia, rDia, ref result, step + 1);

                    xPos[i] = false;
                    lDia[step + i] = false;
                    rDia[xPos.Length + i - step] = false;
                }
            }
        }

#endif
    }
}
