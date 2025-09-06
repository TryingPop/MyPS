using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 3
이름 : 배성훈
내용 : 도넛 행성
    문제번호 : 27211번

    BFS 문제다.
    토러스 형태로 이동가능하게 해준 뒤 플러드 필을 찾아주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1862
    {

        static void Main1862(string[] args)
        {

            int row, col;
            bool[][] visit;

            Input();

            GetRet();

            void GetRet()
            {

                Queue<(int r, int c)> q = new(row * col);
                int[] dirR = { -1, 0, 1, 0 };
                int[] dirC = { 0, -1, 0, 1 };

                int ret = 0;

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (visit[r][c]) continue;
                        BFS(r, c);
                        ret++;
                    }
                }

                Console.Write(ret);

                void BFS(int _r, int _c)
                {

                    q.Clear();
                    q.Enqueue((_r, _c));
                    visit[_r][_c] = true;

                    while (q.Count > 0)
                    {

                        (int r, int c) = q.Dequeue();

                        for (int i = 0; i < 4; i++)
                        {

                            int nR = ChkRow(r + dirR[i]);
                            int nC = ChkCol(c + dirC[i]);

                            if (visit[nR][nC]) continue;
                            visit[nR][nC] = true;
                            q.Enqueue((nR, nC));
                        }

                        int ChkCol(int _c)
                        {

                            if (_c < 0) _c += col;
                            else if (_c >= col) _c -= col;

                            return _c;
                        }

                        int ChkRow(int _r)
                        {

                            if (_r < 0) _r += row;
                            else if (_r >= row) _r -= row;

                            return _r;
                        }
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                row = ReadInt();
                col = ReadInt();

                visit = new bool[row][];

                for (int r = 0; r < row; r++)
                {

                    visit[r] = new bool[col];
                    for (int c = 0; c < col; c++)
                    {

                        visit[r][c] = ReadInt() == 1;
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
