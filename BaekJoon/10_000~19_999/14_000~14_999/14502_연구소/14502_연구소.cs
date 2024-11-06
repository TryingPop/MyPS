using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 24
이름 : 배성훈
내용 : 연구소
    문제번호 : 14502번

    BFS 탐색과 구현 문제다

    벽 추가부분을 DFS로 구현했는데 같은 곳을 두 번 반복하는 매우 안좋은 코드다
    실제로 2배 시간이 걸렸다;

    다른 사람의 풀이를 보니, 0인 좌표들을 리스트에 모아서 3개 벽 설치해서 1번에 했다
*/
namespace BaekJoon.etc
{
    internal class etc_0084
    {

        static void Main84(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int N = ReadInt(sr);
            int M = ReadInt(sr);

            int[,] board = new int[N, M];

            for (int i = 0; i < N; i++)
            {

                for (int j = 0; j < M; j++)
                {

                    board[i, j] = ReadInt(sr);
                }
            }

            sr.Close();
            bool[,] visit = new bool[N, M];
            bool[,] notSafe = new bool[N, M];
            Queue<(int x, int y)> q = new(N * M);
            int[] dirX = { -1, 1, 0, 0 };
            int[] dirY = { 0, 0, -1, 1 };

            int ret = 0;
            DFS(board, notSafe, visit, 0, q, dirX, dirY, ref ret);

            Console.WriteLine(ret);
        }
        
        static void DFS(int[,] _board, bool[,] _notSafe, bool[,] _visit, int _depth, Queue<(int x, int y)> _q, int[] _dirX, int[] _dirY, ref int _ret) 
        { 
        
            if (_depth == 3)
            {

                int total = SetInit(_board, _notSafe, _q);
                total -= Spread(_notSafe, _q, _dirX, _dirY);
                if (_ret < total) _ret = total;
                return;
            }

            int N = _visit.GetLength(0);
            int M = _visit.GetLength(1);

            for (int i = 0; i < N; i++)
            {

                for (int j = 0; j < M; j++)
                {

                    if (_board[i, j] != 0 || _visit[i, j]) continue;

                    _board[i, j] = 1;
                    _visit[i, j] = true;

                    DFS(_board, _notSafe, _visit, _depth + 1, _q, _dirX, _dirY, ref _ret);
                    if (_depth > 0) _visit[i, j] = false;
                    _board[i, j] = 0;
                }
            }
        }

        static int SetInit(int[,] _board, bool[,] _notSafe, Queue<(int x, int y)> _q)
        {

            int ret = 0;
            int N = _board.GetLength(0);
            int M = _board.GetLength(1);
            for (int i = 0; i < N; i++)
            {

                for (int j = 0; j < M; j++)
                {

                    int cur = _board[i, j];
                    _notSafe[i, j] = cur != 0;
                    if (cur == 0) ret++;
                    else if (cur == 2) _q.Enqueue((i, j));
                }
            }

            return ret;
        }

        static int Spread(bool[,] _notSafe, Queue<(int x, int y)> _q, int[] _dirX, int[] _dirY)
        {

            int ret = 0;
            int N = _notSafe.GetLength(0);
            int M = _notSafe.GetLength(1);
            while(_q.Count > 0)
            {

                var node = _q.Dequeue();

                for (int i = 0; i < 4; i++)
                {

                    int nextX = _dirX[i] + node.x;
                    int nextY = _dirY[i] + node.y;

                    if (ChkInvalidPos(nextX, nextY, N, M) || _notSafe[nextX, nextY]) continue;

                    _notSafe[nextX, nextY] = true;
                    ret++;
                    _q.Enqueue((nextX, nextY));
                }
            }

            return ret;
        }

        static bool ChkInvalidPos(int _x, int _y, int _N, int _M)
        {

            if (_x < 0 || _x >= _N) return true;
            if (_y < 0 || _y >= _M) return true;

            return false;
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

#if other
string inputSize = Console.ReadLine();
string[] splitSize = inputSize.Split(' ');

int h = int.Parse(splitSize[0]);
int w = int.Parse(splitSize[1]);

int[,] map = new int[h, w];
bool[,] visited;

List<(int y, int x)> wallPair = new List<(int y, int x)>();
List<(int y, int x)> virusPair = new List<(int y, int x)>();

int[] dirX ={0, 0, -1, 1};
int[] dirY ={1, -1, 0, 0};

int result = -1;

for (int y = 0; y < h; y++)
{
    string input = Console.ReadLine();
    string[] splitInput = input.Split(' ');

    for (int x = 0; x < splitInput.Length; x++)
    {
        map[y, x] = int.Parse(splitInput[x]);
        
        if (map[y,x] == 0)
        {
            wallPair.Add((y,x));
        }
        else if (map[y,x] == 2)
        {
            virusPair.Add((y, x));
        }
    }
}

for (int i = 0; i < wallPair.Count; i++)
{
    for (int j = 0; j < i; j++)
    {
        for (int k = 0; k < j; k++)
        {
            map[wallPair[i].y, wallPair[i].x] = 1;
            map[wallPair[j].y, wallPair[j].x] = 1;
            map[wallPair[k].y, wallPair[k].x] = 1;
            result = Math.Max(result, Solve());
            map[wallPair[i].y, wallPair[i].x] = 0;
            map[wallPair[j].y, wallPair[j].x] = 0;
            map[wallPair[k].y, wallPair[k].x] = 0;
        }
    }
}

Console.WriteLine(result);

int Solve()
{
    visited = new bool[h, w];

    foreach (var virus in virusPair)
    {
        visited[virus.y, virus.x] = true;
        DFS(virus.x,virus.y);
    }

    int count = 0;

    for (int i = 0; i < h; i++)
    {
        for (int j = 0; j < w; j++)
        {
            if (map[i, j] == 0 && !visited[i, j])
                count++;
        }
    }

    return count;
}

void DFS(int x, int y)
{
    for(int i = 0; i < 4; i++)
    {
        int ny = y + dirY[i];
        int nx = x + dirX[i];
        
        if(ny < 0 || ny >= h || nx < 0 || nx >= w || visited[ny,nx] || map[ny,nx] == 1) 
            continue;

        visited[ny, nx] = true;
        DFS(nx, ny);
    }
}
#elif other2
StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

int[] inputs = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
int N = inputs[0];
int M = inputs[1];

int[,] field = new int[N + 2, M + 2];
for (int i = 0; i <= N + 1; i++)
    for (int j = 0; j <= M + 1; j++)
        field[i, j] = -1;

List<(int r, int c)> list0 = new(), list1 = new(), list2 = new();

for (int i = 1; i <= N; i++)
{
    inputs = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
    for (int j = 1; j <= M; j++)
    {
        field[i, j] = inputs[j - 1];
        switch (field[i, j])
        {
            case 0:
                list0.Add((i, j));
                break;
            case 1:
                list1.Add((i, j));
                break;
            case 2:
                list2.Add((i, j));
                break;
        }
    }
}

(int r, int c)[] finder = { (-1, 0), (1, 0), (0, -1), (0, 1) };
Queue<(int r, int c)> queue = new Queue<(int r, int c)>();
int[,] tmpField = new int[N + 2, M + 2];
int minVirusCount = int.MaxValue;
for (int i1 = 0; i1 < list0.Count; i1++)
{
    field[list0[i1].r, list0[i1].c] = 1;
    for (int i2 = i1 + 1; i2 < list0.Count; i2++)
    {
        field[list0[i2].r, list0[i2].c] = 1;
        for (int i3 = i2 + 1; i3 < list0.Count; i3++)
        {
            field[list0[i3].r, list0[i3].c] = 1;
            Array.Copy(field, tmpField, (N + 2) * (M + 2));
            queue.Clear();
            int virusCount = 0;
            foreach (var pos2 in list2)
            {
                queue.Enqueue(pos2);
                virusCount++;
            }

            while (queue.Count > 0)
            {
                var cur = queue.Dequeue();
                for (int i = 0; i < 4; i++)
                {
                    (int r, int c) next = (cur.r + finder[i].r, cur.c + finder[i].c);
                    if (tmpField[next.r, next.c] == 0)
                    {
                        tmpField[next.r, next.c] = 2;
                        queue.Enqueue(next);
                        virusCount++;
                    }
                }

            }
            if (minVirusCount > virusCount)
                minVirusCount = virusCount;

            field[list0[i3].r, list0[i3].c] = 0;
        }
        field[list0[i2].r, list0[i2].c] = 0;
    }
    field[list0[i1].r, list0[i1].c] = 0;
}
sw.WriteLine(list0.Count - 3 - (minVirusCount - list2.Count));

sw.Flush();
sr.Close();
sw.Close();
#endif
}
