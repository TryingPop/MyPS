using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 16
이름 : 배성훈
내용 : 쉬운 최단거리
    문제번호 : 14940번

    BFS로 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0045
    {

        static void Main45(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int height = ReadInt(sr);
            int width = ReadInt(sr);

            int[][] board = new int[height][];
            bool[][] visit = new bool[height][];
            // 시작지점 조사해서 담는다
            Queue<(int x, int y)> q = new Queue<(int x, int y)>();
            for (int i = 0; i < height; i++)
            {

                board[i] = new int[width];
                visit[i] = new bool[width];
                for (int j = 0; j < width; j++)
                {

                    int chk = ReadInt(sr);
                    board[i][j] = chk;

                    // 2가 시작지점
                    if (chk == 2)
                    {

                        // 담고, 방문한걸로 처리
                        // 그리고 q의 시작지점으로 설정
                        board[i][j] = 0;
                        visit[i][j] = true;
                        q.Enqueue((j, i));
                    }
                    // 못가는 지역은 방문처리로 한다
                    // 출력에서 0을 요구한다
                    else if (chk == 0) visit[i][j] = true;
                }
            }

            sr.Close();

            int[] dirX = { -1, 1, 0, 0 };
            int[] dirY = { 0, 0, -1, 1 };
            while (q.Count > 0)
            {

                var curPos = q.Dequeue();
                int cur = board[curPos.y][curPos.x];
                for (int i = 0; i < 4; i++)
                {

                    int nextX = curPos.x + dirX[i];
                    int nextY = curPos.y + dirY[i];

                    if (ChkInvalidPos(nextX, nextY, width, height) || visit[nextY][nextX]) continue;

                    board[nextY][nextX] = cur + 1;
                    visit[nextY][nextX] = true;

                    q.Enqueue((nextX, nextY));
                }
            }

            for (int i = 0; i < height; i++)
            {

                for (int j = 0; j < width; j++)
                {

                    if (!visit[i][j]) board[i][j] = -1;
                }
            }


            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 0; i < height; i++)
                {

                    for (int j = 0; j < width; j++)
                    {

                        sw.Write(board[i][j]);
                        sw.Write(' ');
                    }

                    sw.Write('\n');
                }
            }
        }

        static bool ChkInvalidPos(int _x, int _y, int _width, int _height)
        {

            if (_x < 0 || _x >= _width) return true;
            else if (_y < 0 || _y >= _height) return true;

            return false;
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;

            while((c = _sr.Read()) != ' ' && c != '\n' && c != -1)
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
