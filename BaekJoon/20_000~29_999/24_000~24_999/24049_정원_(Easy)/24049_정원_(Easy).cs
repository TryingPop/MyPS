using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 15
이름 : 배성훈
내용 : 정원 (Easy)
    문제번호 : 24049번

    간단한 구현 문제다
    주어진 조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0232
    {

        static void Main232(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int row = ReadInt(sr);
            int col = ReadInt(sr);

            int[,] board = new int[row + 1, col + 1];

            {

                for (int i = 1; i <= row; i++)
                {

                    board[i, 0] = ReadInt(sr);
                }

                for (int j = 1; j <= col; j++)
                {

                    board[0, j] = ReadInt(sr);
                }
            }
            sr.Close();


            for (int r = 1; r <= row; r++)
            {

                for (int c = 1; c <= col; c++)
                {

                    if (board[r - 1, c] == board[r, c - 1]) board[r, c] = 0;
                    else board[r, c] = 1;
                }
            }

            Console.WriteLine(board[row, col]);
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
