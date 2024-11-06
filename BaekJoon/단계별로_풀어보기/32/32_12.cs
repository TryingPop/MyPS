using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 28
이름 : 배성훈
내용 : 토마토
    문제번호 : 7576번

    입력 쪽이 기존과 다를뿐 BFS를 이용해 풀었다
    그리고 시작점이 여러 곳이라는게 특징이다

    빠르게 푼 사람을 보니 1의 개수를 입력과 동시에 세고, 위치 저장을 바로바로 했다
    날짜를 기입해서 넣는게 아닌, 단순 int 하나로 두고 증가시키며
    정답을 출력
*/

namespace BaekJoon._32
{
    internal class _32_12
    {

        static void Main12(string[] args)
        {

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] size = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int[][] board = new int[size[1]][];

            for (int i = 0; i < size[1]; i++)
            {

                board[i] = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
            }

            sr.Close();

            // BFS 탐색
            BFS(board, size);

            Console.WriteLine(GetResult(board, size));
        }

        static void BFS(int[][] _board, int[] _size)
        {

            int[] dirX = { -1, 1, 0, 0 };
            int[] dirY = { 0, 0, -1, 1 };

            Queue<(int x, int y)> queue = new Queue<(int x, int y)>();

            for (int i = 0; i < _size[1]; i++)
            {

                for (int j = 0; j < _size[0]; j++)
                {

                    if (_board[i][j] == 1) queue.Enqueue((i, j));
                }
            }

            while (queue.Count > 0)
            {

                (int x, int y) node = queue.Dequeue();
                int cur = _board[node.x][node.y];

                for (int i = 0; i < 4; i++)
                {

                    int x = node.x + dirX[i];
                    int y = node.y + dirY[i];

                    if (ChkInValidPos(x, y, _size)) continue;
                    if (_board[x][y] != 0) continue;

                    _board[x][y] = cur + 1;
                    queue.Enqueue((x, y));
                }
            }
        }

        static bool ChkInValidPos(int _x, int _y, int[] _size)
        {

            if (_x < 0 || _x >= _size[1]) return true;
            if (_y < 0 || _y >= _size[0]) return true;

            return false;
        }

        static int GetResult(int[][] _board, int[] _size)
        {

            int max = 0;

            for (int i = 0; i < _size[1]; i++)
            {

                for (int j = 0; j < _size[0]; j++)
                {

                    int date = _board[i][j];
                    if (date > max) max = date;
                    else if (date == 0) return -1; 
                }
            }

            return max - 1;
        }
    }
}
