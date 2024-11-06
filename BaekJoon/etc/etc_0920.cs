using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 28
이름 : 배성훈
내용 : 무기 공학
    문제번호 : 18430번

    백트래킹, 브루트 포스 문제다
    크기가 작아 일일히 확인했다
*/

namespace BaekJoon.etc
{
    internal class etc_0920
    {

        static void Main920(string[] args)
        {

            StreamReader sr;
            int row, col;
            int[][] board;
            bool[][] use;
            int[][] chkR, chkC;
            int ret;

            Solve();
            void Solve()
            {

                Input();

                GetRet();

                Console.Write(ret);
            }

            void GetRet()
            {

                if (row == 1 || col == 1) 
                {

                    ret = 0;
                    return; 
                }

                chkR = new int[4][];
                chkC = new int[4][];

                chkR[0] = new int[3] { -1, -1, 0 };
                chkC[0] = new int[3] { -1, 0, -1 };

                chkR[1] = new int[3] { -1, -1, 0 };
                chkC[1] = new int[3] { 0, -1, 0 };

                chkR[2] = new int[3] { 0, -1, 0 };
                chkC[2] = new int[3] { 0, 0, -1 };

                chkR[3] = new int[3] { 0, -1, 0 };
                chkC[3] = new int[3] { -1, -1, 0 };

                ret = DFS();
            }

            int DFS(int _r = 1, int _c = 1)
            {

                /*
                 
                부메랑을 보면
                    ㅇ   ㅇ       ㅇ   ㅇ            ㅇ       ㅇ     
                    ㅇ                 ㅇ       ㅇ   ㅇ       ㅇ   ㅇ

                형태인데
                    ㅇ   ㅇ
                    ㅇ   ㅁ

                ㅁ인 곳에서 부메랑을 생성한다고 가정한다
                ㅁ을 꼭 포함할 필요는 없다!
                */

                if (col <= _c) 
                { 
                    _r++;
                    _c = 1;
                }

                if (row <= _r) return 0;
                int ret = DFS(_r, _c + 1);

                for (int i = 0; i < 4; i++)
                {

                    bool flag = false;
                    for (int j = 0; j < 3; j++)
                    {

                        int r = _r + chkR[i][j];
                        int c = _c + chkC[i][j];
                        if (!use[r][c]) continue;
                        flag = true;
                        break;
                    }

                    if (flag) continue;

                    int cur = GetScore(_r, _c, i);
                    for (int j = 0; j < 3; j++)
                    {

                        int r = _r + chkR[i][j];
                        int c = _c + chkC[i][j];
                        use[r][c] = true;
                    }

                    cur += DFS(_r, _c + 1);

                    for (int j = 0; j < 3; j++)
                    {

                        int r = _r + chkR[i][j];
                        int c = _c + chkC[i][j];
                        use[r][c] = false;
                    }

                    if (ret < cur) ret = cur;
                }

                return ret;
            }

            int GetScore(int _r, int _c, int _type)
            {

                int ret = board[_r + chkR[_type][0]][_c + chkC[_type][0]];

                for (int i = 0; i < 3; i++)
                {

                    int r = _r + chkR[_type][i];
                    int c = _c + chkC[_type][i];
                    ret += board[r][c];
                }

                return ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                use = new bool[row][];
                board = new int[row][];

                for (int r = 0; r < row; r++)
                {

                    use[r] = new bool[col];
                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = ReadInt();
                    }
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
