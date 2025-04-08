using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 14
이름 : 배성훈
내용 : 딸기와 토마토
    문제번호 : 25565번

    구현, 조건많은 분기 문제다
        3 3 2
        0 0 1
        0 0 1
        0 0 1

    반례를 못캐치해서 88%에서 엄청나게 틀렸다

    처음 정답은 4가지 경우로 쪼개서 제출하니 먼저 통과했다
        - 정답이 없는 경우
        - k개 겹쳐 k개 있는그대로 출력하는 경우
        - 2개 이상 겹치는 경우
        - 1개 겹치는 경우

    이후에 이건 되고 왜 앞에껀 안되는지 코드를 몇 가지 예제를 들어 다시 분석했다
    쓰인건 3 3 3의
    1점에서 만나는 경우 모두와
    겹치는 경우, 그리고 안만나는 경우 총 11개로 했고,
    이후에 3 3 2에서 한 점에서 만나는 경우를 체크했다
    그래서 위의 반례를 찾았다;

    이후 해당 반례부분만 고쳐 처리하니 이상없이 통과했다;
*/

namespace BaekJoon.etc
{
    internal class etc_0229
    {


        static void Main229(string[] args)
        {

#if other
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int row = ReadInt(sr);
            int col = ReadInt(sr);
            int k = ReadInt(sr);

            int[,] board = new int[row + 1, col + 1];

            int seed = 0;
            for (int r = 1; r <= row; r++)
            {

                for (int c = 1; c <= col; c++)
                {

                    int chk = ReadInt(sr);
                    board[r, c] = chk;

                    if (chk == 1) seed++;
                }
            }

            sr.Close();

            int ret = (2 * k) - seed;
            StringBuilder sb = new(20 * (ret + 1));

            if (ret == 0)
            {

                sb.Append(0);
                sw.Write(sb);
                sw.Close();
                return;
            }

            sb.Append($"{ret}\n");
            
            if (ret == k)
            {


                for (int r = 1; r <= row; r++)
                {

                    for (int c = 1; c <= col; c++)
                    {

                        if (board[r, c] != 1) continue;
                        sb.Append($"{r} {c}\n");
                    }
                }

                sw.Write(sb);
                sw.Close();
                return;
            }
            else if (ret > 1)
            {

                bool find = false;
                for (int r = 1; r <= row; r++)
                {

                    for (int c = 1; c <= col; c++)
                    {

                        if (board[r, c] != 1) continue;

                        find = true;
                        int add = k - ret;
                        if (r < row && board[r + 1, c] == 1)
                        {

                            // 세로
                            for (int i = 0; i < ret; i++)
                            {

                                sb.Append($"{r + add + i} {c}\n");
                            }
                        }
                        else
                        {

                            // 가로
                            for (int i = 0; i < ret; i++)
                            {

                                sb.Append($"{r} {c + add + i}\n");
                            }
                        }

                        sw.Write(sb);
                        sw.Close();
                        if (find) break;
                    }

                    if (find) break;
                }
            }
            else
            {

                bool first = true;
                for (int r = 1; r <= row; r++)
                {

                    for (int c = 1; c <= col; c++)
                    {

                        if (board[r, c] == 1)
                        {

                            if (first)
                            {

                                first = false;
                                if (r < row && board[r + 1, c] == 1)
                                {

                                    for (int i = 0; i < k; i++)
                                    {

                                        board[r + i, c] = -1;
                                    }
                                }
                                else
                                {

                                    for (int i = 0; i < k; i++)
                                    {

                                        board[r, c + i] = -1;
                                    }
                                }
                            }
                            else
                            {

                                if (r > 1 && board[r - 1, c] == -1)
                                {

                                    sb.Append($"{r - 1} {c}\n");
                                    sw.Write(sb);
                                    sw.Close();
                                    return;
                                }

                                if (c > 1 && board[r, c - 1] == -1)
                                {

                                    sb.Append($"{r} {c - 1}\n");
                                    sw.Write(sb);
                                    sw.Close();
                                    return;
                                }

                                if (r < row && board[r + 1, c] == 1)
                                {

                                    

                                    for (int i = 1; i < k; i++)
                                    {

                                        if (board[r + i, c] == -1)
                                        {

                                            sb.Append($"{r + i} {c}\n");
                                            sw.Write(sb);
                                            sw.Close();
                                            return;
                                        }
                                    }
                                }
                                else
                                {

                                    for (int i = 1; i < k; i++)
                                    {

                                        if (board[r, c + i] == -1)
                                        {

                                            sb.Append($"{r} {c + i}\n");
                                            sw.Write(sb);
                                            sw.Close();
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    
                }
            }
#else

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int row = ReadInt(sr);
            int col = ReadInt(sr);

            int k = ReadInt(sr);

            int seed = 0;
            int[,] board = new int[row, col];
            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    int chk = ReadInt(sr);

                    board[r, c] = chk;
                    if (chk == 1) seed++;
                }
            }

            sr.Close();

            int ret = (2 * k) - seed;
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(ret);

                if (ret != 0)
                {

                    bool isFirst = true;
                    for (int r = 0; r < row; r++)
                    {

                        for (int c = 0; c < col; c++)
                        {

                            if (board[r, c] == 1)
                            {

                                if (isFirst)
                                {

                                    isFirst = false;
                                    SetFirst(board, r, c, row, col, k);
                                }
                                else
                                {

                                    if (r > 0 && board[r - 1, c] == -1)
                                    {

                                        for (int i = r - ret; i < r; i++)
                                        {

                                            sw.WriteLine($"{i + 1} {c + 1}");
                                            ret--;
                                        }

                                    }
                                    else if (c > 0 && board[r, c - 1] == -1)
                                    {

                                        for (int i = c - ret; i < c; i++)
                                        {

                                            sw.WriteLine($"{r + 1} {i + 1}");
                                            ret--;
                                        }
                                    }
                                    else if (r < row - 1 && board[r + 1, c] == 1)
                                    {

                                        for (int i = r + 1; i < row; i++)
                                        {

                                            if (board[i, c] == -1)
                                            {

                                                sw.WriteLine($"{i + 1} {c + 1}");
                                                ret--;
                                                break;
                                            }
                                            else if (board[i, c] == 0) break;
                                        }
                                    }
                                    else
                                    {

                                        for(int i = c + 1; i < col; i++)
                                        {

                                            if (board[r, i] == -1)
                                            {

                                                sw.WriteLine($"{r + 1} {i + 1}");
                                                ret--;
                                                break;
                                            }
                                            else if (board[r, i] == 0) break;
                                        }
                                    }
                                }
                            }

                            if (ret == 0) break;
                        }

                        if (ret == 0) break;
                    }

                    if (ret != 0)
                    {

                        for (int r = 0; r < row; r++)
                        {

                            for (int c = 0; c < col; c++)
                            {

                                if (board[r, c] == -1)
                                {

                                    sw.WriteLine($"{r + 1} {c + 1}");
                                }
                            }
                        }
                    }
                }
            }

#endif
        }

#if !other
        static void SetFirst(int[,] _board, int _r, int _c, int _row, int _col, int _k)
        {

            int calc = _k - 1;
            _board[_r, _c] = -1;
            bool width = true;

            for (int i = _r + 1; i < _row; i++)
            {

                if (_board[i, _c] == 1)
                {

                    width = false;
                    calc--;
                    _board[i, _c] = -1;
                    if (calc == 0) break;
                }
                else break;
            }

            if (width)
            {

                for (int i = _c + 1; i < _col; i++)
                {

                    if (_board[_r, i] == 1)
                    {

                        calc--;
                        _board[_r, i] = -1;
                        if (calc == 0) break;
                    }
                    else break;
                }
            }
        }
#endif

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
