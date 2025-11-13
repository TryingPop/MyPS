using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 26
이름 : 배성훈
내용 : 특별상이라도 받고 싶어
    문제번호 : 24460번

    분할 정복 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1951
    {

        static void Main1951(string[] args)
        {

            int n;
            int[][] board;

            Input();

            GetRet();

            void GetRet()
            {

                int[] calc = new int[4];

                int ret = DFS(n);

                Console.Write(ret);

                int DFS(int size, int i = 0, int j = 0)
                {

                    if (size == 1) return board[i][j];

                    int nSize = size >> 1;
                    int a = DFS(nSize, i, j);
                    int b = DFS(nSize, i + nSize, j);
                    int c = DFS(nSize, i, j + nSize);
                    int d = DFS(nSize, i + nSize, j + nSize);

                    calc[0] = a;
                    calc[1] = b;
                    calc[2] = c;
                    calc[3] = d;
                    Array.Sort(calc);
                    return calc[1];
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                board = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    board[i] = new int[n];
                    for (int j = 0; j < n; j++)
                    {

                        board[i][j] = ReadInt();
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
