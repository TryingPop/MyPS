using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 15
이름 : 배성훈
내용 : 여중생 파댕이와 공부를
    문제번호 : 30980번

    구현, 문자열, 파싱 문제다
    그냥 조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0236
    {

        static void Main236(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            int row = 3 * info[0];
            int col = 8 * info[1];

            int[,] board = new int[row, col];

            // 정답 여부
            bool[,] calc = new bool[info[0], info[1]];
            for (int i = 0; i < row; i++)
            {

                if ((i % 3) != 1)
                {

                    // 연산 아닌 줄
                    sr.ReadLine();
                    for (int j = 0; j < col; j++)
                    {

                        board[i, j] = '.';
                    }
                }
                else
                {

                    // 연산 줄
                    int calc1 = 0;
                    int calc2 = 0;
                    int calc3 = 0;

                    bool chk1 = true;
                    bool chk2 = true;
                    int cur = 0;
                    for (int j = 0; j < col; j++)
                    {

                        int c = sr.Read();

                        board[i, j] = c;
                        if (chk1)
                        {

                            if (c == '.') continue;
                            else if (c == '+')
                            {

                                chk1 = false;
                                continue;
                            }

                            calc1 = calc1 * 10 + c - '0';
                        }
                        else if (chk2)
                        {

                            if (c == '=')
                            {

                                chk2 = false;
                                continue;
                            }

                            calc2 = calc2 * 10 + c - '0';
                        }
                        else 
                        {

                            if (c == '.')
                            {

                                // 정답 확인
                                if (calc1 + calc2 == calc3) calc[i / 3, cur] = true;
                                cur++;
                                chk1 = true;
                                chk2 = true;
                                calc1 = 0;
                                calc2 = 0;
                                calc3 = 0;
                                continue;
                            }

                            calc3 = calc3 * 10 + c - '0';
                        }
                    }

                    sr.ReadLine();
                }
            }

            sr.Close();

            for (int i = 0; i < info[0]; i++)
            {

                for (int j = 0; j < info[1]; j++)
                {

                    if (calc[i, j])
                    {

                        // 정답 표시
                        for (int c = 8 * j; c < 8 * j + 8; c++)
                        {

                            if (board[3 * i + 1, c] == '.') continue;
                            board[3 * i, c] = '*';
                            board[3 * i + 2, c] = '*';
                        }

                        for (int c = 0; c < 4; c++)
                        {

                            if (board[3 * i + 1, 8 * j + c] == '.' && board[3 * i + 1, 8 * j + c + 1] != '.')
                            {

                                board[3 * i + 1, 8 * j + c] = '*';
                                break;
                            }
                        }

                        for (int c = 7; c >= 4; c--)
                        {

                            if (board[3 * i + 1, 8 * j + c] == '.' && board[3 * i + 1, 8 * j + c - 1] != '.')
                            {

                                board[3 * i + 1, 8 * j + c] = '*';
                                break;
                            }
                        }
                    }
                    else
                    {

                        // 오답 표시
                        board[3 * i, 8 * j + 3] = '/';
                        board[3 * i + 1, 8 * j + 2] = '/';
                        board[3 * i + 2, 8 * j + 1] = '/';
                    }
                }
            }

            // 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 0; i < row; i++)
                {

                    for (int j = 0; j < col; j++)
                    {

                        sw.Write((char)board[i, j]);
                    }
                    sw.Write('\n');
                }
            }
        }
    }
}
