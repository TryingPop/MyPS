using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 6
이름 : 배성훈
내용 : 섬의 개수
    문제번호 : 4963번

    간단한 BFS 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0156
    {

        static void Main156(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536);
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 1024 * 8);

            int[,] board = new int[50, 50];
            
            int[] dirX = { -1, 1, 0, 0, -1, -1, 1, 1 };
            int[] dirY = { 0, 0, -1, 1, -1, 1, -1, 1 };

            Queue<(int x, int y)> q = new(50 * 50);
            while (true)
            {

                // 전체 크기
                // n : row, m : col
                int n = ReadInt(sr);
                int m = ReadInt(sr);

                if (n == 0 && m == 0) break;

                for (int i = 0; i < m; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        // 지면 바다 입력
                        board[i, j] = ReadInt(sr);
                    }
                }

                int ret = 0;
                for (int i = 0; i < m; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        if (board[i, j] == 0) continue;
                        board[i, j] = 0;

                        // 섬 발견 BFS 탐색 시작
                        q.Enqueue((i, j));
                        ret++;
                        while(q.Count > 0)
                        {

                            var node = q.Dequeue();

                            for (int k = 0; k < 8; k++)
                            {

                                int nextX = node.x + dirX[k];
                                int nextY = node.y + dirY[k];
                                if (ChkInvalidPos(nextX, nextY, m, n) || board[nextX, nextY] == 0) continue;
                                board[nextX, nextY] = 0;

                                q.Enqueue((nextX, nextY));
                            }
                        }
                    }
                }

                sw.WriteLine(ret);
            }

            sr.Close();
            sw.Close();
        }

        static bool ChkInvalidPos(int _x, int _y, int _n, int _m)
        {

            if (_x < 0 || _x >= _n || _y < 0 || _y >= _m) return true;
            else return false;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
