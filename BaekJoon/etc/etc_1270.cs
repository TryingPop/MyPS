using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 13
이름 : 배성훈
내용 : Matrice
    문제번호 : 16703번
*/

namespace BaekJoon.etc
{
    internal class etc_1270
    {

        static void Main1270(string[] args)
        {

            int row, col;
            int[][] w, h;
            string[] board;

            Solve();
            void Solve()
            {

                Input();

                SetArr();


            }

            void GetRet()
            {

                // ㄴ형태의 삼각형 확인
                // 아래, 왼쪽 방향으로 값 저장
                for (int r = 0; r < row; r++)
                {

                    w[r + 1][col] = 1;
                    for (int c = col - 2; c >= 0; c--)
                    {

                        w[r + 1][c + 1] = 1;
                        if (board[r][c] == board[r][c + 1])
                            w[r + 1][c + 1] += w[r + 1][c + 2];
                    }
                }

                for (int c = 0; c < col; c++) 
                {

                    h[1][c] = 1;
                    for (int r = 1; r < row; r++)
                    {

                        h[r + 1][c + 1] = 1;
                        if (board[r][c] == board[r - 1][c])
                            h[r + 1][c + 1] += h[r][c + 1];
                    }
                }

                int ret = 0;

                for (int r = row - 1; r >= 0; r--)
                {

                    for (int c = 0; c < col; c++)
                    {


                    }
                }
            }

            void SetArr()
            {

                w = new int[row + 1][];
                h = new int[row + 1][];
                for (int r = 0; r <= row; r++)
                {

                    w[r] = new int[col + 1];
                    h[r] = new int[col + 1];
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string[] temp = sr.ReadLine().Split();
                row = int.Parse(temp[0]);
                col = int.Parse(temp[1]);

                board = new string[row];
                for (int r = 0; r < row; r++)
                {

                    board[r] = sr.ReadLine();
                }

                sr.Close();
            }
        }
    }
}
