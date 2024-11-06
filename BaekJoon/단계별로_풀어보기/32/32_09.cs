using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 27
이름 : 배성훈
내용 : 미로 탐색
    문제번호 : 2178번

    A* 알고리즘 구현? 문제 같다

    반면 나는 두 개의 큐를 만들어 현재꺼에서 다음껄 구분해서 넣고 뺐다 그리고 스위칭할 때 횟수를 증가시켜 전체 걸리는 횟수를 기록했다
    그런데 다른 사람 풀이를 보니 int로 보드를 받아서 몇 칸 이동했는지를 기록했다
    이게 더 좋은 방법처럼 보인다
*/

namespace BaekJoon._32
{
    internal class _32_09
    {

        static void Main9(string[] args)
        {

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] size = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            bool[][] board = new bool[size[0]][];

            for (int i = 0; i < size[0]; i++)
            {


                board[i] = sr.ReadLine().ToCharArray().Select(x => x == '1').ToArray();
            }

            sr.Close();

            // 탐색
            int result = BFS(board, size);

            // 출력
            Console.WriteLine(result);
        }

        static bool ChkInValidPos(int _x, int _y, int[] _size)
        {

            if (_x < 0 || _x >= _size[0]) return true;
            if (_y < 0 || _y >= _size[1]) return true;

            return false;
        }

        static int BFS(bool[][] _board, int[] _size)
        {

            // 방향
            int[] dirX = { -1, 1, 0, 0 };
            int[] dirY = { 0, 0, -1, 1 };

            // 스위칭용도
            Queue<(int x, int y)>[] queue = new Queue<(int x, int y)>[2];
            queue[0] = new Queue<(int x, int y)>();
            queue[1] = new Queue<(int x, int y)>();

            bool chk = false;

            // 0번이 처음들어갈 때 현재 
            queue[0].Enqueue((0, 0));
            int result = 1;
            // 목표지점 도달 여부
            bool goal = false;

            while (true)
            {

                // 골 확인
                if (!_board[_size[0] - 1][_size[1] - 1]) 
                {

                    goal = true;
                    break; 
                }

                // 현재와 다음 스위칭
                chk = !chk;
                int cur = chk ? 0 : 1;
                int next = chk ? 1 : 0;

                result++;

                while (queue[cur].Count > 0)
                {

                    var node = queue[cur].Dequeue();

                    for (int i = 0; i < 4; i++)
                    {

                        int x = node.x + dirX[i];
                        int y = node.y + dirY[i];
                        if (ChkInValidPos(x, y, _size)) continue;
                        if (!_board[x][y]) continue;

                        _board[x][y] = false;
                        queue[next].Enqueue((x, y));
                    }
                }

                // 다음 지점이 없는 경우도 끝낸다
                // 즉 못지나간다
                if (queue[next].Count == 0)
                {

                    break;
                }
            }

            if (goal) return result;
            return -1;
        }
    }
}
