using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 15
이름 : 배성훈
내용 : 수영장 만들기
    문제번호 : 1113번

    구현, BFS 문제다
    etc_0698을 풀기 전에 크기가 작은 해당 문제부터 풀었다

    산에 물을 가득 채우고, 물을 빼갔다
    물을 빼는 방법은 가장자리에서 시작해서 물을 빼갔다
*/

namespace BaekJoon.etc
{
    internal class etc_0697
    {

        static void Main697(string[] args)
        {

            StreamReader sr;

            int[][] board;
            bool[][] visit;
            int[][] water;
            int row, col;
            Queue<(int r, int c)> q;
            int[] add;

            int[] dirR = { -1, 0, 1, 0 };
            int[] dirC = { 0, -1, 0, 1 };

            Solve();

            void Solve()
            {

                Input();
                Init();

                for (int i = 9; i >= 1; i--)
                {

                    BFS(i, (i & 1) == 1);
                }

                Console.WriteLine(GetRet());
            }

            void Init()
            {

                q = new(row * col);
                water = new int[row + 2][];
                for (int r = 0; r < row + 2; r++)
                {

                    water[r] = new int[col + 2];
                    Array.Fill(water[r], 9);
                }
            }

            int GetRet()
            {

                int ret = 0;
                for (int r = 1; r <= row; r++)
                {

                    for (int c = 1; c <= col; c++)
                    {

                        ret += water[r][c] - board[r][c];
                    }
                }

                return ret;
            }

            void BFS(int _height, bool _tf)
            {

                q.Enqueue((0, 0));
                while (q.Count > 0)
                {

                    (int r, int c) node = q.Dequeue();

                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];

                        if (ChkInvalidPos(nextR, nextC) || visit[nextR][nextC] == _tf) continue;
                        visit[nextR][nextC] = _tf;

                        // 현재 맵의 높이가 설정한 물의 높이보다 작은 경우 해당 칸의 물을 뺀다
                        if (board[nextR][nextC] < _height)
                        {

                            water[nextR][nextC] = _height - 1;
                            q.Enqueue((nextR, nextC));
                        }
                    }
                }
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row + 2 || _c >= col + 2) return true;
                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                row = ReadInt();
                col = ReadInt();

                board = new int[row + 2][];
                board[0] = new int[col + 2];

                visit = new bool[row + 2][];
                visit[0] = new bool[col + 2];


                for (int r = 1; r <= row; r++)
                {

                    board[r] = new int[col + 2];
                    visit[r] = new bool[col + 2];

                    for (int c = 1; c <= col; c++)
                    {

                        board[r][c] = sr.Read() - '0';
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                board[row + 1] = new int[col + 2];
                visit[row + 1] = new bool[col + 2];
                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c =sr.Read()) != -1 && c != ' ' && c!= '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
int rows, cols;
int[,] board;
int[,] filled;
int[] dr = { 0, 0, 1, -1 };
int[] dc = { 1, -1, 0, 0 };

StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
string[] head = sr.ReadLine().Split();
rows = int.Parse(head[0]);
cols = int.Parse(head[1]);
board = new int[rows, cols];
filled = new int[rows, cols];
for (int i = 0; i < rows; i++)
{
    string line = sr.ReadLine();
    for (int j = 0; j < cols; j++)
    {
        board[i, j] = line[j] - '0';
    }
}

bool[,] visit = new bool[rows, cols];
bool apply;
List<(int, int)> list = new List<(int, int)>();

for (int level = 1; level <= 9; level++)
{
    Vinit();
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            apply = true;
            list.Clear();
            if (board[i, j] >= level || visit[i, j]) continue;
            Spread(i, j, level);
            if (apply)
            {
                foreach ((int, int) item in list)
                {
                    filled[item.Item1, item.Item2] = level;
                }
            }
        }
    }
}

int sum = 0;
for (int i = 0; i < rows; i++)
{
    for (int j = 0; j < cols; j++)
    {
        if (filled[i, j] < 1) continue;
        sum += filled[i, j] - board[i, j];
    }
}
System.Console.WriteLine(sum);

void Vinit()
{
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            visit[i, j] = false;
        }
    }
}

void Spread(int ir, int ic, int level)
{
    visit[ir, ic] = true;
    list.Add((ir, ic));
    
    for (int d = 0; d < 4; d++)
    {
        int nr = ir + dr[d];
        int nc = ic + dc[d];
        if (nr < 0 || nc < 0 || nr >= rows || nc >= cols)
        {
            apply = false;
            continue;
        }
        if (board[nr, nc] >= level || visit[nr, nc])
        {
            continue;
        }
        Spread(nr, nc, level);
    }
}
#elif other2
using System;
using System.Collections.Generic;
using System.Linq;

// CapitalLetters for class names and methods, camelCase for variable names.
// Write your code clearly enough so that it doesn't need to be commented, or at least, so that it rarely needs to be commented.

namespace Testpad
{
    public class BaekJoon1113
    {
        // 좌표
        struct Pos
        {
            public int x;
            public int y;

            public Pos(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        static void Main()
        {
            Pos[] move = new Pos[4] { new Pos(0, 1), new Pos(1, 0), new Pos(-1, 0), new Pos(0, -1) }; // 물의 이동

            int[] inputs = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            int x = inputs[1];
            int y = inputs[0];

            int[,] map = new int[x, y]; // 지도

            // 땅의 모양을 받음
            for (int i = 0; i < y; i++)
            {
                char[] thisInputs = Console.ReadLine().ToCharArray();

                for (int j = 0; j < x; j++)
                {
                    map[j, i] = int.Parse(thisInputs[j].ToString());
                }
            }

            // 땅의 높이가 1 ~ 9이니, 높이가 낮은 순으로 물을 채워 보자
            Queue<Pos> queue = new Queue<Pos>();
            int[,] water = new int[x, y]; // 현재 물의 양

            for (int i = 1; i <= 9; i++)
            {
                bool[,] used = new bool[x, y]; // 방문 체크

                // 가장자리는 볼 필요가 없음.
                for (int j = 1; j < y - 1; j++)
                {
                    for (int k = 1; k < x - 1; k++)
                    {
                        // 해당 위치가 적절하면, 물을 채워 넣음
                        if (map[k, j] <= i && used[k, j] == false)
                        {
                            bool leaking = false; // 물이 새는지 여부
                            List<Pos> vaildSpots = new List<Pos>(); // 인접한 곳들 중, 물을 담을 수 있는 좌표들 목록

                            queue.Clear();
                            queue.Enqueue(new Pos(k, j));
                            used[k, j] = true;
                            vaildSpots.Add(new Pos(k, j));

                            while (queue.Count > 0)
                            {
                                int thisTime = queue.Count;

                                for (int u = 0; u < thisTime; u++)
                                {
                                    for (int h = 0; h < 4; h++)
                                    {
                                        // 4방향을 확인
                                        try
                                        {
                                            int thisX = queue.Peek().x + move[h].x;
                                            int thisY = queue.Peek().y + move[h].y;

                                            // 높이가 같거나 낮고, 사용한 좌표가 아니면 큐에 추가
                                            if (map[thisX, thisY] <= i && used[thisX, thisY] == false)
                                            {
                                                queue.Enqueue(new Pos(thisX, thisY));
                                                used[thisX, thisY] = true;
                                                vaildSpots.Add(new Pos(thisX, thisY));
                                            }
                                        }
                                        // 배열을 벗어난다면, 해당 영역의 물은 죄다 새버린다는 소리이니 계산은 하되 종료시 해당 영역에 물을 채우지 않음
                                        catch (IndexOutOfRangeException)
                                        {
                                            leaking = true;
                                        }
                                    }


                                    queue.Dequeue();
                                }
                            }

                            // 물이 새지 않는다면, 해당 영역에 물을 채운다.
                            if (leaking == false)
                            {
                                foreach (var item in vaildSpots)
                                {
                                    water[item.x, item.y]++;
                                }
                            }
                        }
                    }
                }
            }

            // 채울 수 있는 물의 양을 출력
            int maxWater = 0; 
            foreach (var item in water)
            {
                maxWater += item;
            }

            Console.Write(maxWater);
        }
    }
}
#endif
}
