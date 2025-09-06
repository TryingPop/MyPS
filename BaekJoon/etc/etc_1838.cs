using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 26
이름 : 배성훈
내용 : 표 정렬
    문제번호 : 14204번

    애드 혹 문제다.
    행 교환이나 열 교환만 가능하다.
    그래서 마지막에 행을 우선순위로 출력할 때 오름 차순 수열이 될러면
    행으로 봤을 때 행의 값들 -1은 열의 길이로 나눴을 때 모두 나눈 몫이 같아야 한다.
    그리고 열로 보면 열의 값들 -1은 열의 길이로 나눴을 대 모두 나눈 나머지가 같아야 한다.

    예를 들어
        2 3 4
        5 6 7
        1 8 9
    를 행을 우선순위로 출력하는 것은
    2 3 4 -> 5 6 7 -> 1 8 9 를 모아 놓은
    2 3 4 5 6 7 1 8 9 수열을 뜻한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1838
    {

        static void Main1838(string[] args)
        {

            int row, col;
            int[][] board;

            Input();

            GetRet();

            void GetRet()
            {

                int Y = 1;
                int N = 0;

                for (int r = 0; r < row; r++)
                {

                    int chk = board[r][0] / col;
                    for (int c = 1; c < col; c++)
                    {

                        if (chk != board[r][c] / col)
                        {

                            Console.Write(N);
                            return;
                        }
                    }
                }

                for (int c = 0; c < col; c++)
                {

                    int chk = board[0][c] % col;
                    for (int r = 1; r < row; r++)
                    {

                        if (chk != board[r][c] % col)
                        {

                            Console.Write(N);
                            return;
                        }
                    }
                }

                Console.Write(Y);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = ReadInt() - 1;
                    }
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }

        }
    }
}
