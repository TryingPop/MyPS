using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 17
이름 : 배성훈
내용 : 캐슬 디펜스
    문제번호 : 17135번

    구현, 브루트포스, 시뮬레이션 문제다.
    적을 찾아가는 로직이 잘못되어 여러 번 틀렸다.
    이후 그냥 좌표를 연산을 통해 모두 찾아 해당 지점을 확인해 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_1119
    {

        static void Main1119(string[] args)
        {

            (int r, int c) EMPTY = (-1, -1);

            int row, col, dis;
            int[][] map, initMap;
            (int r, int c)[][] chk;
            (int r, int c)[] archers, kill;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int ret = 0;
                for (int i = 0; i < col; i++)
                {

                    archers[0] = (row, i);
                    for (int j = i + 1; j < col; j++)
                    {

                        archers[1] = (row, j);
                        for (int k = j + 1; k < col; k++)
                        {

                            archers[2] = (row, k);
                            ret = Math.Max(Simulation(), ret);
                        }
                    }
                }

                Console.Write(ret);
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                return _r < 0 || _c < 0 || row <= _r || col <= _c;
            }

            int Simulation()
            {

                int ret = 0;
                Copy();
                for (int t = 0; t < row; t++)
                {

                    for (int j = 0; j < 3; j++)
                    {

                        Find(j);
                    }

                    ret += MoveEnemy();
                }

                return ret;
            }

            void Find(int _idx)
            {

                kill[_idx] = EMPTY;
                for (int i = 0; i < dis; i++)
                {

                    for (int j = 0; j < chk[i].Length; j++)
                    {

                        int r = archers[_idx].r + chk[i][j].r;
                        int c = archers[_idx].c + chk[i][j].c;

                        if (ChkInvalidPos(r, c) || map[r][c] == 0) continue;
                        kill[_idx] = (r, c);
                        return;
                    }
                }
            }

            int MoveEnemy()
            {

                int ret = 0;
                for (int i = 0; i < 3; i++)
                {

                    if (kill[i] == EMPTY) continue;

                    if (map[kill[i].r][kill[i].c] == 1) ret++;
                    map[kill[i].r][kill[i].c] = 0;
                }

                for (int r = row - 1; r >= 1; r--)
                {

                    for (int c = 0; c < col; c++)
                    {

                        map[r][c] = map[r - 1][c];
                    }
                }

                for (int c = 0; c < col; c++)
                {

                    map[0][c] = 0;
                }

                return ret;
            }

            void Copy()
            {

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        map[r][c] = initMap[r][c];
                    }
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();
                dis = ReadInt();

                initMap = new int[row + 1][];
                map = new int[row + 1][];
                chk = new (int r, int c)[dis][];

                for (int r = 0; r < row; r++)
                {

                    initMap[r] = new int[col];
                    map[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        initMap[r][c] = ReadInt();
                    }
                }

                initMap[row] = new int[col];
                map[row] = new int[col];

                kill = new (int r, int c)[3];
                archers = new (int r, int c)[3];

                for (int i = 0; i < dis; i++)
                {

                    int len = i * 2 + 1;
                    chk[i] = new (int r, int c)[len];

                    for (int j = 0; j <= i; j++)
                    {

                        int c = -i + j;
                        int r = - 1 - j;
                        chk[i][j] = (r, c);
                    }

                    for (int j = 1; j <= i; j++)
                    {

                        int c = j;
                        int r = -i + j - 1;
                        chk[i][j + i] = (r, c);
                    }
                }

                sr.Close();

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
