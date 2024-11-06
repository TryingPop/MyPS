using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 19
이름 : 배성훈
내용 : 다리 만들기 2
    문제번호 : 17472번

    여태까지 배운 것들을 전부 써먹는 문제이다

    먼저 섬분할 -> 거리 측정 -> 최소 신장 트리 만들기
    순서로 풀어야한다

    섬 분할은 동서남북을 탐색하는 BFS 알고리즘을 이용했다
    섬의 시작번호는 2번부터다.. -> 1번부터 하려면 visit여부 체크하면 될거같다

    그리고 거리 측정은 조건이 복잡해 보이는데, 다리 겹치기 허용하고, 
        1. 다리가 겹쳐진 구간은 겹쳐진만큼 카운팅한다
        2. 그리고, 다리 길이는 2이상 되어야한다는 조건이 있다
        3. 그리고 다리는 꺾어지지 않는다 <<< 이 말은 섬의 두 점이 일직선으로 이어져야한다는 말이다!
        
            1. 덕분에 다리 놓은 여부 판별을 안해도 된다 -> 했다면 경우의 수가 엄청나게 증가했을거 같다
            3. 덕분에 다리를 이어붙이는데 상하좌우 판별할 필요가 없이 직선 탐색만 하면 된다

        그래서 그냥 가로, 세로로 dp 방식으로 탐색을 했다
    
    그리고 최소신장 트리 만드는건 다리를 넣을 때, 앞뒤로 넣어서, 크루스칼로 했다
    그런데, 앞 뒤를 넣지않고 비교를 한 번해서 넣었으면, 더 적게 넣을 수 있으나, 
    다리의 개수는 36개이하이므로 그냥 앞뒤로 다 넣어버렸다
*/

namespace BaekJoon._38
{
    internal class _38_06
    {

        static void Main6(string[] args)
        {

            StreamReader sr = new StreamReader(Console.OpenStandardInput());

            int[] info = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int[][] board = new int[info[0]][];

            for (int i = 0; i < info[0]; i++)
            {

                board[i] = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
            }

            sr.Close();

            int[] dirX = new int[4] { -1, 1, 0, 0 };
            int[] dirY = new int[4] { 0, 0, -1, 1 };

            // 섬에 번호 세팅!
            int landNum = SetLand(board, dirX, dirY, info);

            // lines[i][j] = i to j 그런데 최대 도시 수가 6이므로 다차원 배열로한다!
            int[][] lines = new int[landNum][];
            // 이제 유니온 파인드 알고리즘 용도
            int[] group = new int[landNum];

            for (int i = 0;  i < landNum; i++)
            {

                lines[i] = new int[landNum];

                group[i] = i;
            }

            // 최단 거리 다리 찾기
            SetLine(board, info, lines);

            ///
            // 크루스칼!알고리즘!
            int result = 0;
            PriorityQueue<(int pos1, int pos2, int dis), int> q = new();

            for (int i = 0; i < landNum; i++)
            {

                for (int j =0; j < landNum; j++)
                {

                    if (lines[i][j] == 0) continue;

                    q.Enqueue((i, j, lines[i][j]), lines[i][j]);
                }
            }

            Stack<int> s = new Stack<int>();
            // 크루스칼 탈출용!
            int link = 1;
            while(q.Count > 0)
            {

                var node = q.Dequeue();

                int chk1 = Find(group, node.pos1, s);
                int chk2 = Find(group, node.pos2, s);

                if (chk1 == chk2) continue;

                result += node.dis;
                link++;
                group[chk1] = chk2;
                // 다 연결되면 끝낸다
                if (link == landNum) q.Clear();
            }
            ///

            // 1번만 출력하기에 그냥 콘솔로 했다
            if (link == landNum) Console.WriteLine(result);
            else Console.WriteLine(-1);
        }

        static int Find(int[] _groups, int _chk, Stack<int> _calc)
        {

            while(_chk != _groups[_chk])
            {

                _calc.Push(_chk);
                _chk = _groups[_chk];
            }

            while(_calc.Count > 0)
            {

                _groups[_calc.Pop()] = _chk;
            }

            return _chk;
        }

        static int SetLand(int[][] _board, int[] _dirX, int[] _dirY, int[] _size)
        {

            Queue<(int x, int y)> q = new();
            int cur = 2;

            for (int i = 0; i < _size[0]; i++)
            {

                for (int j = 0; j < _size[1]; j++)
                {

                    if (_board[i][j] == 1) BFS(_board, i, j, cur++, _dirX, _dirY, _size, q);
                }
            }

            return cur - 2;
        }

        static bool ChkInValid(int _x, int _y, int[] _size)
        {

            if (_x < 0 || _x >= _size[0]) return true;
            if (_y < 0 || _y >= _size[1]) return true;

            return false;
        }

        static void BFS(int[][] _board, int _x, int _y, int _num, int[] _dirX, int[] _dirY, int[] _size, Queue<(int x, int y)> _q)
        {

            _board[_x][_y] = _num;
            _q.Enqueue((_x, _y));

            while (_q.Count > 0)
            {

                var cur = _q.Dequeue();

                for (int i = 0; i < 4; i++)
                {

                    int nextX = cur.x + _dirX[i];
                    int nextY = cur.y + _dirY[i];

                    if (ChkInValid(nextX, nextY, _size)) continue;
                    if (_board[nextX][nextY] != 1) continue;

                    _board[nextX][nextY] = _num;
                    _q.Enqueue((nextX, nextY));
                }
            }
        }

        static void SetLine(int[][] _board, int[] _size, int[][] _lines)
        {


            // 가로 탐색
            for (int i = 0; i < _size[0]; i++)
            {

                int cur = 0;
                int dis = 0;
                for (int j = 0; j < _size[1]; j++)
                {

                    if (cur == _board[i][j]) dis = 0;
                    else if (_board[i][j] == 0) dis++;
                    else if (cur == 0) cur = _board[i][j];
                    else
                    {

                        int start = cur - 2;
                        int end = _board[i][j] - 2;

                        int chk = _lines[start][end];
                        if (dis > 1
                            && (chk == 0 || dis < chk))
                        {


                            _lines[start][end] = dis;
                            _lines[end][start] = dis;
                        }

                        // 갱신
                        cur = _board[i][j];
                        dis = 0;
                    }
                }
            }

            // 세로 탐색
            for (int j = 0; j < _size[1]; j++)
            {

                int cur = 0;
                int dis = 0;
                for (int i = 0; i < _size[0]; i++)
                {

                    if (cur == _board[i][j]) dis = 0;
                    else if (_board[i][j] == 0) dis++;
                    else if (cur == 0) cur = _board[i][j];
                    else
                    {

                        int start = cur - 2;
                        int end = _board[i][j] - 2;

                        int chk = _lines[start][end];
                        if (dis > 1
                            && (chk == 0 || dis < chk))
                        {

                            _lines[start][end] = dis;
                            _lines[end][start] = dis;
                        }

                        // 갱신
                        cur = _board[i][j];
                        dis = 0;
                    }
                }
            }
        }
    }
}
