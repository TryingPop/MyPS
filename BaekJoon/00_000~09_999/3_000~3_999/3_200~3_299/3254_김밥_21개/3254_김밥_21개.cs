using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 9
이름 : 배성훈
내용 : 김밥 21개
    문제번호 : 3254번

    구현, 시뮬레이션 문제다.
    맵의 크기가 작아 8방향으로 4개 이상이 이어졌는지 확인했다.
    만약 맵의 크기가 크다면 유니온 - 파인드 알고리즘을 이용해 이어진 갯수를세었을 것이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1531
    {

        static void Main1531(string[] args)
        {

            // 승리자 이름
            string[] name = { "", "sk", "ji" };

            // 돌 놓은 상태
            int[][] board = new int[6][];
            for (int r = 0; r < 6; r++)
            {

                board[r] = new int[7];
            }

            // 바닥 번호 확인
            int[] bot = new int[7];

            // 방향
            int[] dirR = { -1, 0, 1, 1 };
            int[] dirC = { -1, -1, -1, 0 };

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            for (int round = 1; round <= 21; round++)
            {

                string[] temp = sr.ReadLine().Split();

                for (int user = 1; user <= 2; user++)
                {

                    int col = int.Parse(temp[user - 1]) - 1;
                    int row = bot[col]++;

                    board[row][col] = user;

                    if (ChkWin(row, col, user))
                    {

                        Console.Write($"{name[user]} {round}");
                        return;
                    }
                }
            }

            Console.Write("ss");

            bool ChkWin(int _r, int _c, int _color)
            {

                // 4방향으로 이어진거 확인
                for (int dir = 0; dir < 4; dir++)
                {

                    int cnt = 1;

                    // 정방향
                    for (int dis = 1; dis <= 4; dis++)
                    {

                        int chkR = _r + dirR[dir] * dis;
                        int chkC = _c + dirC[dir] * dis;
                        if (ChkInvalidPos(chkR, chkC) || board[chkR][chkC] != _color) break;
                        cnt++;
                    }

                    // 역방향
                    for (int dis = 1; dis <= 4; dis++)
                    {

                        int chkR = _r - dirR[dir] * dis;
                        int chkC = _c - dirC[dir] * dis;

                        if (ChkInvalidPos(chkR, chkC) || board[chkR][chkC] != _color) break;
                        cnt++;
                    }

                    // 4개 이상인지 확인
                    if (4 <= cnt) return true;
                }

                return false;

                // 유효하지 않은 좌표
                bool ChkInvalidPos(int _r, int _c)
                    => _r < 0 || _c < 0 || _r >= 6 || _c >= 7;
            }
        }
    }
}
