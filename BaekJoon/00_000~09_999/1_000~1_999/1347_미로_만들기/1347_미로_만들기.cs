using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 3
이름 : 배성훈
내용 : 미로 만들기
    문제번호 : 1347번

    구현, 시뮬레이션 문제다.
    맵을 모두 어떤 경로를 설정해도 이동할 수 있게 크게 잡았다.
    그리고 직접 이동을 하며 이동한 지점을 모두 찾는다.
    그리고 이동지점을 모두 포함하는 가장 작은 직사각형을 찾아 출력했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1312
    {

        static void Main1312(string[] args)
        {

            int MAX = 100;

            int[][] board;
            (int r, int c) cur;
            int dir, n;

            Solve();
            void Solve()
            {

                MakeMaze();

                GetRet();
            }

            void GetRet()
            {

                (int r, int c) s = (MAX + 1, MAX + 1), e = (-1, -1);

                for (int r = 0; r <= MAX; r++)
                {

                    for (int c = 0; c <= MAX; c++)
                    {

                        if (board[r][c] == 0) continue;
                        s.r = Math.Min(s.r, r);
                        s.c = Math.Min(s.c, c);

                        e.r = Math.Max(e.r, r);
                        e.c = Math.Max(e.c, c);
                    }
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int r = s.r; r <= e.r; r++)
                {

                    for (int c = s.c; c <= e.c; c++)
                    {

                        if (board[r][c] == 0) sw.Write('#');
                        else sw.Write('.');
                    }

                    sw.Write('\n');
                }
            }

            void MakeMaze()
            {

                
                board = new int[MAX + 1][];
                for (int i = 0; i <= MAX; i++)
                {

                    board[i] = new int[MAX + 1];
                }

                int[] dirR = { -1, 0, 1, 0 };
                int[] dirC = { 0, -1, 0, 1 };
                dir = 2;

                n = int.Parse(Console.ReadLine());
                string input = Console.ReadLine();
                cur = (MAX >> 1, MAX >> 1);
                board[cur.r][cur.c] = 1;

                for (int i = 0; i < input.Length; i++)
                {

                    if (input[i] == 'L') RotL();
                    else if (input[i] == 'R') RotR();
                    else
                    {

                        cur.r += dirR[dir];
                        cur.c += dirC[dir];

                        board[cur.r][cur.c] = 1;
                    }
                }

                void RotL()
                {

                    dir = dir == 3 ? 0 : dir + 1;
                }

                void RotR()
                {

                    dir = dir == 0 ? 3 : dir - 1;
                }
            }
        }
    }
}
