using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 5
이름 : 배성훈
내용 : 나의 인생에는 수학과 함께
    문제번호 : 17265번

    브루트포스, DFS 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1678
    {

        static void Main1678(string[] args)
        {

            int n;
            int[][] board;

            Input();

            GetRet();

            void GetRet()
            {

                int min = 123_456_789;
                int max = -123_456_789;
                int e = n - 1;

                DFS();

                Console.Write($"{max} {min}");
                void DFS(int _r = 0, int _c = 0, int _val = 0, int _op = 0)
                {

                    if (_r == e && _c == e)
                    {

                        if (_op == 0)
                            _val += board[_r][_c];
                        else if (_op == 1)
                            _val -= board[_r][_c];
                        else
                            _val *= board[_r][_c];

                        min = Math.Min(min, _val);
                        max = Math.Max(max, _val);
                        return;
                    }
                    else if (_r > e || _c > e) return;

                    if (((_r + _c) & 1) == 1)
                    {

                        int op = ReadOp(board[_r][_c]);
                        DFS(_r + 1, _c, _val, op);
                        DFS(_r, _c + 1, _val, op);
                    }
                    else
                    {

                        if (_op == 0)
                            _val += board[_r][_c];
                        else if (_op == 1)
                            _val -= board[_r][_c];
                        else
                            _val *= board[_r][_c];

                        DFS(_r + 1, _c, _val, -1);
                        DFS(_r, _c + 1, _val, -1);
                    }
                }

                int ReadOp(int _op)
                {

                    if (_op == '+') return 0;
                    else if (_op == '-') return 1;
                    else return 2;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = int.Parse(sr.ReadLine());

                board = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    board[i] = new int[n];

                    string[] temp = sr.ReadLine().Split();
                    for (int j = 0; j < n; j++)
                    {

                        if (((i + j) & 1) == 1)
                            board[i][j] = temp[j][0];
                        else
                            board[i][j] = int.Parse(temp[j]);
                    }
                }
            }
        }
    }
}
