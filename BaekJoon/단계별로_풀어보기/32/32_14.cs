using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 29
이름 : 배성훈
내용 : 뱀과 사다리 게임
    문제번호 : 16928번
*/

namespace BaekJoon._32
{
    internal class _32_14
    {

        static void Main14(string[] args)
        {

            const int MAX = 100;

            int[] ladder = new int[MAX + 1];
            int[] snake = new int[MAX + 1];

            using (StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput())))
            {

                int[] info = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

                for (int i = 0; i < info[0]; i++)
                {

                    int[] temp = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

                    ladder[temp[0]] = temp[1];
                }

                for (int i = 0; i < info[1]; i++)
                {

                    int[] temp = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

                    snake[temp[0]] = temp[1];
                }
            }

            int[] board = new int[MAX + 1];
            BFS(board, ladder, snake);

            Console.WriteLine(board[MAX] - 1);
        }

        static void BFS(int[] _board, int[] _ladder, int[] _snake, int _start = 1)
        {

            Queue<int> queue = new Queue<int>();
            // 시작에서 스네이크나 사다리가 없다
            queue.Enqueue(_start);

            int day = 1;
            _board[_start] = day;

            while (queue.Count > 0)
            {

                int node = queue.Dequeue();
                day = _board[node];

                for(int i = 1; i <= 6; i++)
                {

                    int next = node + i;
                    // 탈출
                    if (next > 100) 
                    {

                        queue.Clear();
                        break; 
                    }

                    // 사다리 혹은 뱀을 탄다
                    // 사다리 혹은 뱀은 겹치는 경우가 없고 많아야 하나의 뱀과 사다리를 갖기에 한번만 체크하면 된다
                    if (_ladder[next] != 0) next = _ladder[next];
                    else if (_snake[next] != 0) next = _snake[next];

                    if (_board[next] != 0) continue;
                    _board[next] = day + 1;
                    queue.Enqueue(next);
                }
            }
        }

        /// <summary>
        /// 쓰지 않는다! 조건에서 최대 하나 또는 사다리나 스네이크를 갖기 때문에!
        /// 중복되면 쓴다!
        /// </summary>
        static int Teleport(int[] _ladder, int[] _snake, int _num)
        {


            while (true)
            {

                if (_ladder[_num] != 0)
                {

                    _num = _ladder[_num];
                }
                else if (_snake[_num] != 0)
                {

                    _num = _ladder[_num];
                }
                else break;
            }

            return _num;
        }
    }
}
