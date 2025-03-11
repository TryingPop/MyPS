using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 11
이름 : 배성훈
내용 : 경사로
    문제번호 : 14890번

    구현 문제다.
    지도의 크기가 100 x 100으로 작다.
    그래서 N^3 방법도 유효하다.

    그래서 매 행과 열을 조사할 때 
    이전 l개를 일일히 조사하는 방법을 이용했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1395
    {

        static void Main1395(string[] args)
        {

            int n, l;
            int[][] board;

            Input();

            GetRet();

            void GetRet()
            {

                bool[] calc = new bool[n];
                int ret = 0;

                for (int i = 0; i < n; i++)
                {

                    if (ChkRow(i)) ret++;
                    if (ChkCol(i)) ret++;
                }

                Console.Write(ret);

                bool ChkRow(int _r)
                {

                    Array.Fill(calc, false);

                    for (int c = 0; c < n - 1; c++)
                    {

                        int diff = board[_r][c] - board[_r][c + 1];
                        if (diff == 0) continue;
                        else if (diff > 1 || diff < -1) return false;
                        else if (diff == 1)
                        {

                            int e = c + l;
                            if (e >= n) return false;
                            for (int i = c + 1; i <= e; i++)
                            {

                                if (calc[i]) return false;
                                calc[i] = true;
                            }
                        }
                        else
                        {

                            int s = c - l + 1;
                            if (s < 0) return false;
                            for (int i = c; i >= s; i--)
                            {

                                if (calc[i]) return false;
                                calc[i] = true;
                            }
                        }
                    }

                    return true;
                }

                bool ChkCol(int _c)
                {

                    Array.Fill(calc, false);
                    for (int r = 0; r < n - 1; r++)
                    {

                        int diff = board[r][_c] - board[r + 1][_c];
                        if (diff == 0) continue;
                        else if (diff > 1 || diff < -1) return false;
                        else if (diff == 1)
                        {

                            int e = r + l;
                            if (e >= n) return false;
                            for (int i = r + 1; i <= e; i++)
                            {

                                if (calc[i]) return false;
                                calc[i] = true;
                            }
                        }
                        else
                        {

                            int s = r - l + 1;
                            if (s < 0) return false;
                            for (int i = r; i >= s; i--)
                            {

                                if (calc[i]) return false;
                                calc[i] = true;
                            }
                        }
                    }

                    return true;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                l = ReadInt();

                board = new int[n][];
                for (int r = 0; r < n; r++)
                {

                    board[r] = new int[n];
                    for (int c = 0; c < n; c++)
                    {

                        board[r][c] = ReadInt();
                    }
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;

                        ret = c - '0';

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
