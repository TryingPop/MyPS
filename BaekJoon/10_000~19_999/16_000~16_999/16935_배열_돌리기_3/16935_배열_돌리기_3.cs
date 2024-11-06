using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 29
이름 : 배성훈
내용 : 배열 돌리기 3
    문제번호 : 16935번

    조건대로 함수로 만들어서 구현했다
    한번씩 실행한다 해당 결과가 끝난 행렬식이 존재할거 같다

    다만, 배열이라도 함수안에서 swap을 해도 함수 밖에 나오면
    swap이 되지 않는다!

    유니티에서 Transform을 메서드로 전달할 때 Swap하는 경우 
    ref를 넣어야 하는 이유와 같다!
*/

namespace BaekJoon.etc
{
    internal class etc_0103
    {

        static void Main103(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int row = ReadInt(sr);
            int col = ReadInt(sr);

            int[,] board = new int[row, col];
            int[,] rotBoard = new int[col, row];
            int len = ReadInt(sr);

            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    board[r, c] = ReadInt(sr);
                }
            }

            for (int i = 0; i < len; i++)
            {

                int calc = ReadInt(sr);

                switch (calc)
                {

                    case 1:
                        Calc1(board);
                        break;

                    case 2:
                        Calc2(board);
                        break;

                    case 3:
                        Calc3(ref board, ref rotBoard);
                        break;

                    case 4:
                        Calc4(ref board, ref rotBoard);
                        break;

                    case 5:
                        Calc5(board);
                        break;

                    case 6:
                        Calc6(board);
                        break;

                    default:
                        break;
                }
            }

            sr.Close();

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                int chkRow = board.GetLength(0);
                int chkCol = board.GetLength(1);

                for (int r = 0; r < chkRow; r++)
                {

                    for (int c = 0; c < chkCol; c++)
                    {

                        sw.Write(board[r, c]);
                        sw.Write(' ');
                    }

                    sw.Write('\n');
                }
            }


        }

        static void Calc1(int[,] _board)
        {

            int row = _board.GetLength(0);
            int col = _board.GetLength(1);

            int halfRow =  row / 2;
            for (int r = 0; r < halfRow; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    int temp = _board[r, c];
                    _board[r, c] = _board[row - 1 - r, c];
                    _board[row - 1 - r, c] = temp;
                }
            }
        }

        static void Calc2(int[,] _board)
        {

            int row = _board.GetLength(0);
            int col = _board.GetLength(1);

            int halfCol = col / 2;

            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < halfCol; c++)
                {

                    int temp = _board[r, c];
                    _board[r, c] = _board[r, col - 1 - c];
                    _board[r, col - 1 - c] = temp;
                }
            }
        }

        static void Calc3(ref int[,] _board, ref int[,] _rotBoard)
        {

            int row = _board.GetLength(0);
            int col = _board.GetLength(1);

            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    _rotBoard[c, row - 1 - r] = _board[r, c];
                }
            }

            var temp = _board;
            _board = _rotBoard;
            _rotBoard = temp;
        }

        static void Calc4(ref int[,] _board, ref int[,] _rotBoard)
        {

            int row = _board.GetLength(0);
            int col = _board.GetLength(1);

            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    _rotBoard[col - 1 - c, r] = _board[r, c];
                }
            }

            var temp = _board;
            _board = _rotBoard;
            _rotBoard = temp;
        }

        static void Calc5(int[,] _board)
        {

            int halfRow = _board.GetLength(0) / 2;
            int halfCol = _board.GetLength(1) / 2;

            for (int r = 0; r < halfRow; r++)
            {

                for (int c = 0; c < halfCol; c++)
                {

                    int temp = _board[r, c];
                    _board[r, c] = _board[r + halfRow, c];
                    _board[r + halfRow, c] = _board[r + halfRow, c + halfCol];
                    _board[r + halfRow, c + halfCol] = _board[r, c + halfCol];
                    _board[r, c + halfCol] = temp;
                }
            }
        }

        static void Calc6(int[,] _board)
        {

            int halfRow = _board.GetLength(0) / 2;
            int halfCol = _board.GetLength(1) / 2;

            for (int r = 0; r < halfRow; r++)
            {

                for (int c = 0; c < halfCol; c++)
                {

                    int temp = _board[r, c];
                    _board[r, c] = _board[r, c + halfCol];
                    _board[r, c + halfCol] = _board[r + halfRow, c + halfCol];
                    _board[r + halfRow, c + halfCol] = _board[r + halfRow, c];
                    _board[r + halfRow, c] = temp;
                }
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
