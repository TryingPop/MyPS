using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 29
이름 : 배성훈
내용 : 배찬우는 배열을 좋아해
    문제번호 : 25966번

    이차원 배열에서 값을 바꾸거나
    행을 바꾸는 연산을 한다

    다만 변환하는 명령(쿼리)의 양이 100만까지 오고,
    행과 열이 최대 3만까지 오기에 시간이 많이 걸린다
    그래서 1.8초 걸렸다 -> 버퍼를 확장하니 1초 안으로 해결되었다!

    C++로 푼 다른사람들도 1.x초대가 일반적이다
*/

namespace BaekJoon.etc
{
    internal class etc_0132
    {

        static void Main132(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536);

            int row = ReadInt(sr);
            int col = ReadInt(sr);

            int[][] board = new int[row][];

            int len = ReadInt(sr);

            for (int i = 0; i < row; i++)
            {

                board[i] = new int[col];

                for (int j = 0; j < col; j++)
                {

                    board[i][j] = ReadInt(sr);
                }
            }

            for (int i = 0; i < len; i++)
            {

                int order = ReadInt(sr);

                if (order == 0)
                {

                    int r = ReadInt(sr);
                    int c = ReadInt(sr);

                    board[r][c] = ReadInt(sr);
                    continue;
                }
                else
                {

                    int r1 = ReadInt(sr);
                    int r2 = ReadInt(sr);

                    int[] temp = board[r1];
                    board[r1] = board[r2];
                    board[r2] = temp;
                }
            }

            sr.Close();

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 65536))
            {

                for (int i = 0; i < row; i++)
                {

                    for (int j = 0; j < col; j++)
                    {

                        sw.Write(board[i][j]);
                        sw.Write(' ');
                    }
                    sw.Write('\n');
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
