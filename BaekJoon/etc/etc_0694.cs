using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 15
이름 : 배성훈
내용 : 게시판 구멍 막기
    문제번호 : 2414번

    이분 매칭 문제다
    어떻게 이분 매칭 시킬지가 중요하다
    가로 또는 세로로만 붙일 수 있다
    그래서 영역을 가로 영역, 세로 영역으로 나눴다

    돌멩이 제거에서는 중간에 돌이 없어도 달리기가 가능하지만 
    여기서는 테이프를 이어붙이는 것은 안된다
    그래서 떨어져 있으면 영역을 구분시켜줘야한다
    가로 세로로 이분 매칭하니 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0694
    {

        static void Main694(string[] args)
        {

            StreamReader sr;

            int row, col;
            HashSet<int>[] line;
            int[][][] board;

            bool[] visit;
            int[] match;

            int len1, len2;
            Solve();

            void Solve()
            {

                Input();

                SetGroup();

                LinkLine();

                int ret = 0;

                for (int i = 1; i <= len1; i++)
                {

                    Array.Fill(visit, false);
                    if (DFS(i)) ret++;
                }
                Console.WriteLine(ret);
            }

            void SetGroup()
            {

                int idx = 0;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c][0] == 0) continue;
                        board[r][c][1] = ++idx;

                        for (int i = c + 1; i < col; i++)
                        {

                            if (board[r][i][0] == 0) break;
                            board[r][i][1] = idx;
                            c++;
                        }
                    }
                }

                len1 = idx;

                idx = 0;
                for (int c = 0; c < col; c++)
                {

                    for (int r = 0; r < row; r++)
                    {

                        if (board[r][c][0] == 0) continue;
                        board[r][c][2] = ++idx;

                        for (int i = r + 1; i < row; i++)
                        {

                            if (board[i][c][0] == 0) break;
                            board[i][c][2] = idx;
                            r++;
                        }
                    }
                }

                len2 = idx;
            }

            void LinkLine()
            {

                line = new HashSet<int>[len1 + 1];
                for (int i = 1; i <= len1; i++)
                {

                    line[i] = new();
                }

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c][0] == 0) continue;
                        line[board[r][c][1]].Add(board[r][c][2]);
                    }
                }

                match = new int[len2 + 1];
                visit = new bool[len2 + 1];
            }

            bool DFS(int _n)
            {

                foreach (int next in line[_n])
                {

                    if (visit[next]) continue;
                    visit[next] = true;

                    if (match[next] == 0 || DFS(match[next]))
                    {

                        match[next] = _n;
                        return true;
                    }
                }

                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                row = ReadInt();
                col = ReadInt();

                board = new int[row][][];

                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col][];
                    for (int c = 0; c < col; c++)
                    {

                        int cur = sr.Read();
                        board[r][c] = new int[3];
                        if (cur == '.') continue;
                        board[r][c][0] = 1;
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c =sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
