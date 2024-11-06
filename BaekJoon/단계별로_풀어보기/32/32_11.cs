using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 28
이름 : 배성훈
내용 : 나이트의 이동
    문제번호 : 7562번

    매번 보드를 생성하니 메모리 할당한다고 속도가 느리다
    그래서 재활용하게 수정했다
*/

namespace BaekJoon._32
{
    internal class _32_11
    {

        static void Main11(string[] args)
        {

            int len = int.Parse(Console.ReadLine());

            const int MAX_BOARD = 300;
            int[][] board = new int[MAX_BOARD][];

            for (int i = 0; i < MAX_BOARD; i++)
            {

                board[i] = new int[MAX_BOARD];
            }

            for (int i = 0; i < len; i++)
            {

                ///
                /// 입력
                /// 
                // 사이즈
                int size = int.Parse(Console.ReadLine());

                // 좌표
                int[] start = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                int[] end = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

                // 보드 생성
                /*
                int[][] board = new int[size][];
                for (int  j = 0; j < size; j++)
                {

                    board[j] = new int[size];
                }
                */

                if (i != 0)
                {

                    // 보드 초기화
                    for (int j = 0; j < size; j++)
                    {

                        Array.Fill(board[j], 0);
                    }
                }

                BFS(board, start, end, size);

                Console.WriteLine(board[end[0]][end[1]]);
            }
        }

        static bool ChkInValidPos(int _x, int _y, int _size)
        {

            if (_x < 0 || _x >= _size) return true;
            if (_y < 0 || _y >= _size) return true;

            return false;
        }

        static void BFS(int[][] _board, int[] _start, int[] _end, int _size)
        {

            if (_start[0] == _end[0]
                && _start[1] == _end[1]) return;


            // 갈 수 있는 곳 체크
            Queue<(int x, int y)> queue = new Queue<(int x, int y)>();

            int[] dirX = { 1, 2, 2, 1, -1, -2, -2, -1 };
            int[] dirY = { 2, 1, -1, -2, -2, -1, 1, 2 };

            queue.Enqueue((_start[0], _start[1]));

            while (queue.Count > 0)
            {

                (int x, int y) node = queue.Dequeue();
                int cur = _board[node.x][node.y];

                for (int i = 0; i < 8; i++)
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
    }
}
