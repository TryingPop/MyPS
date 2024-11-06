using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 24
이름 : 배성훈
내용 : 오델로
    문제번호 : 15671번

    구현 문제다
    처음에는 턴을 넘기는 경우가 있다고 판단했다

    그래서 해당 자리에 놓을 수 있는가로 판별했다
    만약 돌을 바꾸지 못하는 자리인데 놓는 경우면
    입력에서 놓는 자리는 유효한 자리라고 봐서 색상을 바꿔 놓았다
    
    그런데, 이후 해당 코드는 문제가 있어 보였다
    그래서 문제를 다시 보게되었고 턴을 넘기는 경우는 없다고 한다
    이에 돌 변환만 해서 제출하니 이상없이 통과했다

    만약 오델로를 게임으로 구현해보라고하면, 검은 돌을 놓을 수 있는 위치를 저장할거 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0086
    {

        static void Main86(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);


            int[,] board = new int[7, 7];
            board[3, 3] = 1;
            board[4, 4] = 1;
            board[3, 4] = 2;
            board[4, 3] = 2;

            int[] dirX = { -1, 1, 0, 0, -1, -1, 1, 1 };
            int[] dirY = { 0, 0, -1, 1, -1, 1, -1, 1 };

            // bool cur = true;
            for (int i = 0; i < len; i++)
            {

                bool cur = i % 2 == 0;
                // cur = !cur;

                int x = ReadInt(sr);
                int y = ReadInt(sr);

                // 자리 유효한지 검사용! -> 불필요함!
                // if (!ChkUser(board, x, y, cur, dirX, dirY)) cur = !cur;

                // 돌 놓기
                board[x, y] = cur ? 2 : 1;
                // 돌 변환
                ChangeDol(board, x, y, cur, dirX, dirY);
            }

            sr.Close();
            using (StreamWriter sw = new(Console.OpenStandardOutput()))
            {

                int blackWin = 0;
                for (int i = 1; i <= 6; i++)
                {

                    for (int j = 1; j <= 6; j++)
                    {

                        int cur = board[i, j];
                        if (cur == 0) sw.Write('.');
                        else if (cur == 1)
                        {

                            sw.Write('W');
                            blackWin--;
                        }
                        else 
                        { 
                            
                            sw.Write('B');
                            blackWin++;
                        }
                    }

                    sw.Write('\n');
                }

                sw.Write((blackWin > 0 ? "Black" : "White"));
            }
        }

        static bool ChkUser(int[,] _board, int _x, int _y, bool _isBlack, int[] _dirX, int[] _dirY)
        {

            int turnUser = _isBlack ? 2 : 1;
            int turnEnemy = _isBlack ? 1 : 2;

            for (int i = 0; i < 8; i++)
            {

                bool find = false;

                int curX = _x;
                int curY = _y;

                while (true)
                {

                    curX += _dirX[i];
                    curY += _dirY[i];
                    if (ChkInValidPos(curX, curY)) break;

                    int cur = _board[curX, curY];
                    if (cur == turnEnemy) find = true;
                    else if (find && cur == turnUser)
                    {

                        return true;
                    }
                    else break;
                }
            }

            return false;
        }


        static void ChangeDol(int[,] _board, int _x, int _y, bool _isBlack, int[] _dirX, int[] _dirY)
        {

            int turnUser = _isBlack ? 2 : 1;
            int turnEnemy = _isBlack ? 1 : 2;

            for (int i = 0; i < 8; i++)
            {

                bool find = false;

                int curX = _x;
                int curY = _y;
                int jump = 0;
                while (true)
                {

                    curX += _dirX[i];
                    curY += _dirY[i];
                    jump++;
                    if (ChkInValidPos(curX, curY)) break;

                    int cur = _board[curX, curY];
                    if (cur == turnEnemy) find = true;
                    else if (find && cur == turnUser)
                    {

                        for (int j = jump - 1; j >= 1; j--)
                        {

                            int x = curX - j * _dirX[i];
                            int y = curY - j * _dirY[i];

                            _board[x, y] = turnUser;
                        }

                        break;
                    }
                    else break;
                }
            }
        }

        static bool ChkInValidPos(int _x, int _y)
        {

            if (_x < 1 || _x >= 7 || _y < 1 || _y >= 7) return true;
            
            return false;
        }


        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
