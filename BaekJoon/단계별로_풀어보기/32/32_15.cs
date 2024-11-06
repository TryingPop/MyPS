using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 29
이름 : 배성훈
내용 : 벽 부수고 이동하기
    문제번호 : 2206번

    처음에 어떻게 맵을 표현해야할지 몰라서 많이 헤맸다
    그래서 다른 사람 풀이 방법 아이디어를 보고 풀었다
    
    다른 사람들 풀이 아이디어는 다음과 같다
    벽 부순 루트 저장소와 벽을 부수지 않은 저장소 따로 기록하는 방법이다

    그래서 맵을 벽을 부수지 않은 일반 루트 0번, 벽을 부순(broken) 루트 1번 해서 2개의 맵을 사용한다
    먼저 일반 루트를 BFS 탐색한다 
    탐색 중 벽을 마주칠 경우 벽 전용 큐에 따로 좌표와 값을 기록한다(1회만 기록한다)
    Queue 자료 구조의 선입선출 특징으로 최초 기록은 최소값이 보장 되어 기록 여부만 따지면 된다 (최소값 확인할 필요가 없다!)
    그리고 일반 길인 경우 그냥 1씩 증가하며 이동한다 O(NM)
    
    일반 탐색이 끝나면 이제 벽이 있는 곳을 기준으로 탐색한다
    무의미한 연산을 줄이기 위해(메모리 초과 방지!)
    다른 최적화 루트가 아니면 탐색을 멈추는 백트래킹을 접목시켰다
    (최적화 루트가 존재하면 멈추는식으로 했다가 메모리 초과가 떴다)
    그렇게 벽 부순 길을 탐색이 완료하면 탐색을 종료했다

    그리고 두 개의 보드판으로 결과를 해석해야 하는데 서로 분할된 4가지 경우가 있다
        1. 일반 루트와, 벽 부순 루트의 도착지 값이 모두 0인 경우 : 도착 불가능한 장소
        2. 일반 루트와, 벽 부순 루트의 도착지 값이 모두 0이 아닌 경우 : 둘 다 도착 가능한 곳이고 벽 부순 루트는 일반 루트보다 짧은게 보장되므로 벽 부순 루트에 최소값이 담긴다
        3. 일반 루트만 0인 경우 : 벽을 부수지 않으면 길이 없다는 의미이고, 벽 부순 경우만 길이 존재한다
        4. 벽 부순 루트만 0인 경우 : 벽을 부셔서 이동해도 최단 루트는 없다는 의미이다 (벽 안부수는 루트가 있다는 의미)

*/

namespace BaekJoon._32
{
    internal class _32_15
    {

        static void Main15(string[] args)
        {

            int[] info;
            int[][][] board = new int[2][][];

            // 입력
            using (StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput())))
            {

                info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                board[0] = new int[info[0]][];
                board[1] = new int[info[0]][];
                for (int i = 0; i < info[0]; i++)
                {

                    string temp = sr.ReadLine();
                    board[0][i] = new int[info[1]];
                    board[1][i] = new int[info[1]];

                    for (int j = 0; j < info[1]; j++)
                    {

                        // 지나갈 수 있는 길 -1, 벽 0
                        board[0][i][j] = temp[j] - '1';
                    }
                }
            }

            // BFS 탐색
            BFS(board, info);


            // 결과 계산
            int result;

            int value = board[0][info[0] - 1][info[1] - 1];
            int brokenValue = board[1][info[0] - 1][info[1] - 1];

            if (value == 0 && brokenValue == 0) result = -1;
            else if (value == 0) result = brokenValue;
            else if (brokenValue == 0) result = value;
            else result = brokenValue;

            // 출력
            Console.WriteLine(result);
        }

        static bool ChkInValidPos(int _x, int _y, int[] _size)
        {

            // 인덱스 조사
            if (_x < 0 || _x >= _size[0]) return true;
            if (_y < 0 || _y >= _size[1]) return true;

            return false;
        }

        static void BFS(int[][][] _board, int[] _size)
        {

            // 방향
            int[] dirX = { -1, 1, 0, 0 };
            int[] dirY = { 0, 0, -1, 1 };

            // 일반 탐색 용 큐
            Queue<(int x, int y)> queue = new Queue<(int x, int y)>();
            // 벽 부순 루트 탐색용 큐
            Queue<(int x, int y)> broken = new Queue<(int x, int y)>();

            // 초기에 시작 좌표를 일반탐색에 넣는다
            // 조건에서 시작 구간은 벽이 없다!
            queue.Enqueue((0, 0));
            // 길 기록
            _board[0][0][0] = 1;

            // 일반 BFS 탐색
            while(queue.Count > 0)
            {

                (int x, int y) node = queue.Dequeue();
                int cur = _board[0][node.x][node.y];

                for (int i = 0; i < 4; i++)
                {

                    int x = node.x + dirX[i];
                    int y = node.y + dirY[i];

                    if (ChkInValidPos(x, y, _size)) continue;

                    int value = _board[0][x][y];
                    
                    if (value == -1)
                    {

                        // 지나갈 수 있는 길이고 아직 안지나간 곳
                        // Queue의 특징으로 넘버링을 하면 최소값이 담긴다
                        _board[0][x][y] = cur + 1;
                        queue.Enqueue((x, y));
                    }
                    else if (value == 0 && _board[1][x][y] == 0)
                    {

                        // 부서진 길이고 아직 안지나간 곳
                        // Queue의 특징으로 넘버링을 하면 최소값이 담긴다
                        _board[1][x][y] = cur + 1;
                        broken.Enqueue((x, y));
                    }
                }
            }

            // 부순 길 BFS 탐색
            while (broken.Count > 0)
            {

                (int x, int y) node = broken.Dequeue();
                int cur = _board[1][node.x][node.y];

                for (int i = 0; i < 4; i++)
                {

                    int x = node.x + dirX[i];
                    int y = node.y + dirY[i];

                    if (ChkInValidPos(x, y, _size)) continue;

                    int value = _board[0][x][y];
                    // 두 번은 못 뚫기에 벽이 있으면 넘긴다
                    if (value == 0) continue;

                    // 벽 안뚫고 더 빠른 길이 있으면 굳이 벽뚫고 안간다
                    if (value > 0 && cur + 1 >= value) continue;

                    // 이미 뚫고 지나간 길인데 더 가까운 루트가 있으면 넘긴다
                    int brokenValue = _board[1][x][y];
                    // 최단 루트인 경우만 기록
                    // 여기서 = 안해줘서 메모리 초과 났다;
                    if (brokenValue != 0 && cur + 1 >= brokenValue) continue;
                    _board[1][x][y] = cur + 1;
                    broken.Enqueue((x, y));
                }
            }
        }

#if Wrong
        static void Main(string[] args)
        {

            // 중복 진입 방지를 안해서 시간 초과가 날 것이다
            int[] info;
            int[][] board;
            using (StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput())))
            {

                info = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
                board = new int[info[0]][];

                for (int i = 0; i < info[0]; i++)
                {

                    string temp = sr.ReadLine();

                    board[i] = new int[info[1]];

                    for (int j = 0; j < info[1]; j++)
                    {

                        // 갈 수 있는 길 -1, 벽 0
                        board[i][j] = temp[j] - '1';
                    }
                }
            }

            int[][] dir = new int[2][];
            dir[0] = new int[] { -1, 1, 0, 0 };
            dir[1] = new int[] { 0, 0, -1, 1 };

            int result = 1_000_001;
            DFS(board, info, dir, ref result);

            if (result > 1_000_000) result = -1;
            Console.WriteLine(result);
            
        }

        static bool ChkInValidPos(int _x, int _y, int[] _size)
        {

            if (_x < 0 || _x >= _size[0]) return true;
            if (_y < 0 || _y >= _size[1]) return true;

            return false;
        }

        static void DFS(int[][] _board, int[] _size, int[][] _dir, ref int _result, int _step = 1, int _x = 0, int _y = 0, bool _broken = false)
        {

            _board[_x][_y] = _step;

            if (_x == _size[0] - 1
                && _y == _size[1] - 1)
            {

                if (_result > _board[_x][_y]) _result = _board[_x][_y];
                return;
            }

            for (int i = 0; i < 4; i++)
            {

                int x = _x + _dir[0][i];
                int y = _y + _dir[1][i];

                if (ChkInValidPos(x, y, _size)) continue;
                if (_board[x][y] == 0)
                {

                    // 이미 부셨다면 넘긴다
                    if (_broken) continue;

                    _broken = true;

                    DFS(_board, _size, _dir, ref _result, _step + 1, x, y, _broken);

                    _broken = false;
                    _board[x][y] = 0;
                }
                else if (_board[x][y] == -1)
                {

                    DFS(_board, _size, _dir, ref _result, _step + 1, x, y, _broken);
                    _board[x][y] = -1;
                }
            }
        }
#endif
    }
}
