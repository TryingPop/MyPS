using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 27
이름 : 배성훈
내용 : 유기농 배추
    문제번호 : 1012번

    32_07에서 다른사람이 썼던 튜플 방법 적용
*/

namespace BaekJoon._32
{
    internal class _32_08
    {

        static void Main8(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int questions = int.Parse(sr.ReadLine());
            for (int i = 0; i < questions; i++)
            {

                // 가로, 세로, 배추의 개수
                int[] info = sr.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

                bool[][] board = new bool[info[0]][];

                for (int j = 0; j < info[0]; j++)
                {

                    board[j] = new bool[info[1]];
                }

                // 보드 세팅
                for (int j = 0; j < info[2]; j++)
                {

                    int[] temp = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

                    board[temp[0]][temp[1]] = true;
                }


                // BFS 탐색
                int result = 0;

                for (int j = 0; j < info[0]; j++)
                {

                    for (int k = 0; k < info[1]; k++)
                    {

                        if (board[j][k])
                        {

                            BFS(board, j, k, info[0], info[1]);
                            result++;
                        }
                    }
                }

                // 정답 출력
                Console.WriteLine(result);
            }

            sr.Close();
        }

        static bool ChkInvalidPos(int _x, int _y, int _sizeX, int _sizeY)
        {

            if (_x < 0 || _x >= _sizeX) return true;
            if (_y < 0 || _y >= _sizeY) return true;

            return false;
        }

        static void BFS(bool[][] _board, int _xPos, int _yPos, int _sizeX, int _sizeY)
        {

            // 좌 우 상 하
            int[] dirX = { -1, 1, 0, 0 };
            int[] dirY = { 0, 0, -1, 1 };

            // 다른 사람이 썼던 튜플 방법!
            Queue<(int x, int y)> queue = new Queue<(int x, int y)>();
            _board[_xPos][_yPos] = false;

            queue.Enqueue((_xPos, _yPos));

            while(queue.Count > 0)
            {

                (int x, int y) pos = queue.Dequeue();

                for (int i = 0; i < 4; i++)
                {

                    int x = pos.x + dirX[i];
                    int y = pos.y + dirY[i];

                    if (ChkInvalidPos(x, y, _sizeX, _sizeY)) continue;
                    if (!_board[x][y]) continue;

                    _board[x][y] = false;

                    queue.Enqueue((x, y));
                }
            }
        }
    }
}
