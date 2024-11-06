using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 29
이름 : 배성훈
내용 : 바닥 장식
    문제번호 : 1388번

    구현 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0778
    {

        static void Main778(string[] args)
        {

            StreamReader sr;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                int row = ReadInt();
                int col = ReadInt();
                int ret = 0;
                int[][] board = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    int before = 0;
                    for (int c = 0; c < col; c++)
                    {

                        int cur = sr.Read();
                        if (cur == '-')
                        {

                            if (before == 0)
                            {

                                before = 1;
                                ret++;
                            }

                            continue;
                        }

                        board[r][c] = 1;
                        before = 0;
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                for (int c = 0; c < col; c++)
                {

                    int before = 0;
                    for (int r = 0; r < row; r++)
                    {

                        if (board[r][c] == 0)
                        {

                            before = 0;
                            continue;
                        }

                        if (before != 0) continue;
                        before = 1;
                        ret++;
                    }
                }

                Console.Write(ret);
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
        }
    }
}
