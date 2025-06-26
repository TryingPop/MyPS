using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 26
이름 : 배성훈
내용 : 성냥개비와 정사각형
    문제번호 : 2214번

    브루트포스, 문자열 문제다.
    크기가 20을 넘지 않는다.
    그래서 N^4의 방법으로 케이스를 조사해도 유효하다.
*/

namespace BaekJoon.etc
{
    internal class etc_1732
    {

        static void Main1732(string[] args)
        {

            // 2214
            string S = " squares\n";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int row = 0, col = 0, max = 0;
            bool[][] board;

            Init();

            while (Input())
            {

                GetRet();
            }

            void Init()
            {

                board = new bool[42][];
                
                for (int i = 0; i < board.Length; i++)
                {

                    board[i] = new bool[42];
                }
            }

            void GetRet()
            {

                int ret = 0;

                for (int r = 0; r < row; r += 2)
                {

                    for (int c = 0; c < col; c += 2)
                    {

                        for (int i = 1; i <= max; i++)
                        {

                            if (Cnt(r, c, i)) ret++;
                        }
                    }
                }

                sw.Write(ret);
                sw.Write(S);

                bool Cnt(int _sR, int _sC, int _size)
                {

                    int eR = _sR + _size * 2;
                    int eC = _sC + _size * 2;
                    if (eR > row || eC > col) return false;

                    for (int i = _sC + 1; i < eC; i += 2)
                    {

                        if (board[_sR][i] || board[eR][i]) return false;
                    }

                    for (int i = _sR + 1; i < eR; i += 2)
                    {

                        if (board[i][_sC] || board[i][eC]) return false;
                    }

                    return true;
                }

            }

            bool Input()
            {

                string[] temp = sr.ReadLine().Split();
                row = int.Parse(temp[0]);
                col = int.Parse(temp[1]);

                if (row == 0 && col == 0) return false;

                max = Math.Min(row, col);
                row = row * 2 + 1;
                col = col * 2 + 1;
                for (int r = 0; r < row; r++)
                {

                    string line = sr.ReadLine();
                    for (int c = (~r & 1), i = 0; c < col; c += 2, i++)
                    {

                        board[r][c] = line[i] == '*';  
                    }
                }

                return true;
            }
        }
    }
}
