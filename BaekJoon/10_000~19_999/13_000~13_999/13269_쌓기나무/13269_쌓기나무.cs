using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 24
이름 : 배성훈
내용 : 쌓기나무
    문제번호 : 13269번

    그리디, 해 구성하기 문제다.
    윗면부터 주어진다.
    윗면에 나무가 쌓여있다면 최대 높이로 쌓는다.

    다음으로 정면으로 쌓인 것을 확인하는데
    해당 행에 정면에 있는 갯수가 되게 수정한다.

    마지막으로 오른쪽에서 본 경우도 똑같이
    해당 열에 오른쪽 경우의 갯수가 되게 줄인다.

    이렇게 진행한 뒤 기존 모양이 되는지 확인하면된다.
    마지막 검증을 안해서 1번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1573
    {

        static void Main1573(string[] args)
        {

            int MAX = 100;
            
            int n, m;
            int[][] board, up;
            int[] front, right;

            Input();

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                if (ChkInvalid())
                {

                    sw.Write(-1);
                    return;
                }

                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < m; c++)
                    {

                        sw.Write($"{board[r][c]} ");
                    }

                    sw.Write('\n');
                }

                bool ChkInvalid()
                {

                    for (int r = 0; r < n; r++)
                    {

                        for (int c = 0; c < m; c++)
                        {

                            if (up[r][c] == 0) continue;

                            if (board[r][c] == 0) return true;
                        }
                    }

                    for (int c = 0; c < m; c++)
                    {

                        bool flag = false;
                        for (int r = 0; r < n; r++)
                        {

                            if (board[r][c] != front[c]) continue;
                            flag = true;
                            break;
                        }

                        if (flag) continue;
                        return true;
                    }

                    for (int r = 0; r < n; r++)
                    {

                        bool flag = false;
                        for (int c = 0; c < m; c++)
                        {

                            if (board[r][c] != right[r]) continue;
                            flag = true;
                            break;
                        }

                        if (flag) continue;
                        return true;
                    }

                    return false;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                board = new int[n][];
                up = new int[n][];

                for (int r = 0; r < n; r++)
                {

                    board[r] = new int[m];
                    up[r] = new int[m];
                    for (int c = 0; c < m; c++)
                    {

                        int cur = ReadInt();
                        up[r][c] = cur;
                        if (cur == 0) continue;
                        board[r][c] = MAX;
                    }
                }

                front = new int[m];

                for (int c = 0; c < m; c++)
                {

                    int cur = ReadInt();
                    front[c] = cur;
                    for (int r = 0; r < n; r++)
                    {

                        board[r][c] = Math.Min(board[r][c], cur);
                    }
                }

                right = new int[n];

                for (int r = n - 1; r >= 0; r--)
                {

                    int cur = ReadInt();
                    right[r] = cur;
                    for (int c = 0; c < m; c++)
                    {

                        board[r][c] = Math.Min(board[r][c], cur);
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
