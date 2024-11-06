using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 29
이름 : 배성훈
내용 : 토마토
    문제번호 : 7569번

    32_12에서 3차원으로 바꾸고 이동방향 수정한거 밖에 없다
    빠르게 푼 사람을 보니 다차원 배열이 아닌 1차원 배열로 해결
*/

namespace BaekJoon._32
{
    internal class _32_13
    {

        static void Main13(string[] args)
        {

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            int[][][] board = new int[info[2]][][];

            for (int i = 0; i < info[2]; i++)
            {

                board[i] = new int[info[1]][];

                for (int j = 0; j < info[1]; j++)
                {

                    board[i][j] = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
                }
            }

            sr.Close();

            int result = BFS(board, info);
            Console.WriteLine(result);
        }

        static bool ChkInValidPos(int _x, int _y, int _z, int[] _size)
        {

            if (_x < 0 || _x >= _size[2]) return true;
            if (_y < 0 || _y >= _size[1]) return true;
            if (_z < 0 || _z >= _size[0]) return true;

            return false;
        }

        static int BFS(int[][][] _board, int[] _size)
        {

            int[] dirX = { 1, -1, 0, 0, 0, 0 };
            int[] dirY = { 0, 0, 1, -1, 0, 0 };
            int[] dirZ = { 0, 0, 0, 0, 1, -1 };

            // 1 찾기
            Queue<(int x, int y, int z)> queue = new Queue<(int x, int y, int z)>();
            int empty = 0;

            for (int i = 0; i < _size[2]; i++)
            {

                for (int j = 0; j < _size[1]; j++)
                {

                    for (int k = 0; k < _size[0]; k++)
                    {

                        int cur = _board[i][j][k];
                        if (cur == 1) queue.Enqueue((i, j, k));
                        else if (cur == 0) empty++;
                    }
                }
            }

            int day = 0;

            while (queue.Count > 0)
            {

                (int x, int y, int z) node = queue.Dequeue();
                int cur = _board[node.x][node.y][node.z];

                for (int i = 0; i < 6; i++)
                {

                    int x = node.x + dirX[i];
                    int y = node.y + dirY[i];
                    int z = node.z + dirZ[i];

                    if (ChkInValidPos(x, y, z, _size)) continue;
                    if (_board[x][y][z] != 0) continue;

                    _board[x][y][z] = cur + 1;
                    empty--;
                    day = day < cur ? cur: day;
                    queue.Enqueue((x, y, z));
                }
            }

            if (empty != 0) return -1;
            else return day;
        }
    }
}
