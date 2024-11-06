using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 27
이름 : 배성훈
내용 : 단지번호붙이기
    문제번호 : 2667번

    좌표 정보를 담는 구조체를 만들었다
    메서드 실행 후에 사라지게 구조체로 설정했다

    그리고 큐에 해당 좌표 정보를 넣어주면서 BFS 알고리즘을 썼다

    다른 사람의 풀이를 보니 튜플로 해결하거나 DFS 방식으로 해결했다
*/

namespace BaekJoon._32
{
    internal class _32_07
    {

        static void Main7(string[] args)
        {

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int size = int.Parse(sr.ReadLine());

            char[][] board = new char[size][];

            for (int i = 0; i < size; i++)
            {

                board[i] = sr.ReadLine().ToCharArray();
            }

            sr.Close();

            // 탐색
            List<int> result = new List<int>();


            for (int i = 0; i < size; i++)
            {

                for (int j = 0; j < size; j++)
                {

                    if (board[i][j] == '1')
                    {

                        result.Add(Numbering(board, i, j, size));
                    }
                }
            }

            // 조건에 맞춰 오름차순 정렬
            result.Sort();

            // 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(result.Count);
                for (int i = 0; i < result.Count; i++)
                {

                    sw.WriteLine(result[i]);
                }
            }

        }

        // 좌표 보관용 struct
        public struct Pos
        {

            private int xPos;
            private int yPos;

            public int x => xPos;
            public int y => yPos;

            public Pos(int _xPos, int _yPos)
            {

                xPos = _xPos;
                yPos = _yPos;
            }
        }

        // 배열 벗어났는지 확인
        static bool ChkInValidPos(int _x, int _y, int _size)
        {


            if (_x < 0 || _x >= _size) return true;
            if (_y < 0 || _y >= _size) return true;

            return false;
        }
        
        // 인접한 아파트 개수 확인
        static int Numbering(char[][] _board, int _startX, int _startY, int _size)
        {

            _board[_startX][_startY] = '0';
            int result = 1;

            Queue<Pos> queue = new Queue<Pos>();

            queue.Enqueue(new Pos(_startX, _startY));
            

            int[] dirX = { -1, 1, 0, 0 };
            int[] dirY = { 0, 0, -1, 1 };

            while(queue.Count > 0)
            {

                Pos pos = queue.Dequeue();

                for (int i = 0; i < 4; i++)
                {

                    int x = pos.x + dirX[i];
                    int y = pos.y + dirY[i];
                    if (ChkInValidPos(x, y, _size)) continue;
                    if (_board[x][y] == '0') continue;

                    // 재진입 방지로 0을 만들고 추가
                    _board[x][y] = '0';
                    result++;

                    queue.Enqueue(new Pos(x, y));
                }
            }

            return result;
        }
    }
}
