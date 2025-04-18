using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 16
이름 : 배성훈
내용 : 석판 자르기
    문제번호 : 2339번

    분할 정복 문제다.
    아이디어는 다음과 같다.
    먼저 해당 영역에 불순물과 보석 수를 센다.
    그리고 보석이 1개이고, 불순물이 0개이면 가능하다는 의미로 1을 반환한다.
    반면 보석이 2개 이상이고, 불순물이 0개면 한 장소에 보석은 1개 있어야 한다는 조건을 만족 못하므로 0을 반환한다.
    또한 보석이 없는 경우도 0을 반환한다. 비슷하게 불가능한 경우는 0으로 탈출한다.

    이후 모르는 경우는 영역을 분할하면서 확인한다.
    불순물을 발견하면 해당 방향으로 자를 수 있는지 확인하고 자른다.
    그리고 자른 방향을 기준으로 나눠진 영역을 조사한다.
    이렇게 자를 수 있는지 확인한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1117
    {

        static void Main1117(string[] args)
        {

            int n;
            int[][] board;
            int one, two;

            Solve();
            void Solve()
            {

                Input();

                int ret = GetRet();

                Console.Write(ret == 0 ? -1 : ret);
            }

            int GetRet()
            {

                if (one == 0 && two == 1) return 1;
                int ret2 = DFS(0, 0, n, n, true);
                int ret1 = DFS(0, 0, n, n, false);
                return ret1 + ret2;
            }

            // 영역은 사각형이므로 영역의 시작지점 f?, 영역의 끝지점 t?로 표현
            // 끝지점은 포함 X
            // isWidth는 자르는 방향이다.
            int DFS(int _fr, int _fc, int _tr, int _tc, bool _isWidth)
            {

                int areaOne = 0, areaTwo = 0;
                for (int r = _fr; r < _tr; r++)
                {

                    for (int c = _fc; c < _tc; c++)
                    {

                        if (board[r][c] == 1) areaOne++;
                        else if (board[r][c] == 2) areaTwo++;
                    }
                }

                // 가능한 경우 1 반환
                if (areaOne == 0 && areaTwo == 1) return 1;
                // 불가능한 경우는 0 반환
                else if ((areaOne == 1 && areaTwo == 1) 
                    || areaTwo == 0
                    || (areaOne == 0 && areaTwo > 1)) return 0;

                // 탐색이 필요한 경우
                int ret = 0;

                for (int r = _fr; r < _tr; r++)
                {

                    for (int c = _fc; c < _tc; c++)
                    {

                        // 불순물이 발견되면 불순물을 기준으로 자른다.
                        if (board[r][c] != 1) continue;

                        // 가로방향 자르기?
                        if (_isWidth)
                        {

                            // 모서리는 자를 수 없다.
                            if (r == _fr || r == _tr - 1) continue;
                            bool flag = false;

                            // 자르는 장소에 보석이 있으면 해당 방향으로 못자른다.
                            for (int k = _fc; k < _tc; k++)
                            {

                                if (board[r][k] != 2) continue;
                                flag = true;
                                break;
                            }

                            if (flag) continue;

                            // 자르기 시도 위 아래 가능한지 확인
                            ret += DFS(_fr, _fc, r, _tc, false) * DFS(r + 1, _fc, _tr, _tc, false);
                        }
                        else
                        {

                            // 세로 자르기, 끝부분은 못자른다.
                            if (c == _fc || c == _tc - 1) continue;
                            bool flag = false;

                            // 세로 방향에 보석이 있으면 못 자른다.
                            for (int k = _fr; k < _tr; k++)
                            {

                                if (board[k][c] != 2) continue;
                                flag = true;
                                break;
                            }

                            if (flag) continue;

                            // 자르기 시도 좌 우 가능한지 확인
                            ret += DFS(_fr, _fc, _tr, c, true) * DFS(_fr, c + 1, _tr, _tc, true);
                        }
                    }
                }

                return ret;
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                board = new int[n][];
                one = 0;
                two = 0;
                for (int r = 0; r < n; r++)
                {

                    board[r] = new int[n];
                    for (int c = 0; c < n; c++)
                    {

                        int cur = ReadInt();
                        board[r][c] = cur;
                        if (cur == 1) one++;
                        else if (cur == 2) two++;
                    }
                }

                sr.Close();
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
}
