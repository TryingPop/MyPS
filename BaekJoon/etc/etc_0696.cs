using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 15
이름 : 배성훈
내용 : 테트로미노
    문제번호 : 14500번

    구현, 브루트포스 알고리즘 문제다
    가능한 모양이 19개 밖에 안되서 일일히 작성해(하드코딩) 해결했다
    2 * 2배열로 만든 뒤 읽는 순서나 연산방법을 바꾸는 식으로 회전구현해도 될거 같다
    예를들어 ㄴ형태의 경우
    90도 반시계 회전은 
        nextR = r + dirC[i]
        nextC = c - dirR[i]
*/

namespace BaekJoon.etc
{
    internal class etc_0696
    {

        static void Main696(string[] args)
        {

            StreamReader sr;
            int[][] board;
            int row, col;

            int[][] dirR;
            int[][] dirC;

            Solve();

            void Solve()
            {

                Input();

                SetDir();

                int ret = 0;
                for (int i = 0; i < 19; i++)
                {

                    int chk = Find(i);
                    ret = ret < chk ? chk : ret;
                }

                Console.WriteLine(ret);
            }

            void SetDir()
            {

                dirR = new int[19][];
                dirC = new int[19][];
                for (int i = 0; i < 19; i++)
                {

                    dirR[i] = new int[3];
                    dirC[i] = new int[3];
                }

                // | 모양
                dirR[0][0] = 1;
                dirR[0][1] = 2;
                dirR[0][2] = 3;

                // - 모양
                dirC[1][0] = 1;
                dirC[1][1] = 2;
                dirC[1][2] = 3;

                // ㅁ모양
                dirR[2][0] = 1;
                dirC[2][1] = 1;
                dirR[2][2] = 1;
                dirC[2][2] = 1;

                // ㅗ 모양
                dirC[3][0] = 1;
                dirR[3][1] = -1;
                dirC[3][1] = 1;
                dirC[3][2] = 2;

                // ㅓ 모양
                dirC[4][0] = 1;
                dirC[4][1] = 1;
                dirC[4][2] = 1;
                dirR[4][1] = -1;
                dirR[4][2] = 1;

                // ㅏ 모양
                dirR[5][0] = -1;
                dirR[5][1] = 1;
                dirC[5][2] = 1;

                // ㅜ 모양
                dirC[6][0] = 1;
                dirC[6][1] = 1;
                dirR[6][1] = 1;
                dirC[6][2] = 2;

                // z
                dirC[7][0] = 1;
                dirC[7][1] = 1;
                dirC[7][2] = 2;
                dirR[7][1] = 1;
                dirR[7][2] = 1;

                // z를 y축 대칭
                dirC[8][0] = 1;
                dirC[8][1] = 1;
                dirC[8][2] = 2;
                dirR[8][1] = -1;
                dirR[8][2] = -1;

                // z를 90도 회전
                dirR[9][0] = 1;
                dirR[9][1] = 1;
                dirR[9][2] = 2;
                dirC[9][1] = 1;
                dirC[9][2] = 1;

                // 9번을 대칭
                dirR[10][0] = 1;
                dirR[10][2] = -1;
                dirC[10][1] = 1;
                dirC[10][2] = 1;

                // ㄴ
                dirR[11][0] = 1;
                dirR[11][1] = 1;
                dirR[11][2] = 1;
                dirC[11][1] = 1;
                dirC[11][2] = 2;

                // ㄴ을 시계방향으로 90도 회전?
                dirR[12][1] = 1;
                dirR[12][2] = 2;
                dirC[12][0] = 1;

                // ㄱ
                dirR[13][2] = 1;
                dirC[13][0] = 1;
                dirC[13][1] = 2;
                dirC[13][2] = 2;

                // ㄴ을 반시계방향으로 90도 회전
                dirR[14][1] = -1;
                dirR[14][2] = -2;
                dirC[14][0] = 1;
                dirC[14][1] = 1;
                dirC[14][2] = 1;

                // ㄴ을 y축 대칭
                dirR[15][2] = -1;
                dirC[15][0] = 1;
                dirC[15][1] = 2;
                dirC[15][2] = 2;

                // L
                dirR[16][0] = 1;
                dirR[16][1] = 2;
                dirR[16][2] = 2;
                dirC[16][2] = 1;

                // ㄱ을 y축 대칭
                dirR[17][0] = 1;
                dirC[17][1] = 1;
                dirC[17][2] = 2;

                // ㄱ인데 윗쪽이 짧다
                dirR[18][1] = 1;
                dirR[18][2] = 2;
                dirC[18][0] = 1;
                dirC[18][1] = 1;
                dirC[18][2] = 1;
            }

            int Find(int _idx)
            {

                int ret = 0;

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        int score = board[r][c];

                        bool find = true;

                        for (int i = 0; i < 3; i++)
                        {

                            int nextR = r + dirR[_idx][i];
                            int nextC = c + dirC[_idx][i];

                            if (ChkInvalidPos(nextR, nextC))
                            {

                                find = false;
                                break;
                            }

                            score += board[nextR][nextC];
                        }

                        if (find && ret < score) ret = score;
                    }
                }

                return ret;
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row || _c >= col) return true;
                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = ReadInt();
                    }
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
using System;
using System.IO;

var sr = new StreamReader(new BufferedStream(
    Console.OpenStandardInput()));

int n = ScanInt(sr), m = ScanInt(sr);
var map = new int[n, m];
for (int i = 0; i < n; i++)
    for (int j = 0; j < m; j++)
        map[i, j] = ScanInt(sr);
sr.Close();
var bricks = new Brick[] {
    new((0, 0), (1, 0), (2, 0), (3, 0), 4, 1),

    new((0, 0), (1, 0), (2, 0), (0, 1), 3, 2),
    new((0, 0), (1, 0), (2, 0), (1, 1), 3, 2),
    new((0, 0), (1, 0), (2, 0), (2, 1), 3, 2),

    new((0, 0), (1, 0), (0, 1), (1, 1), 2, 2),

    new((0, 0), (1, 0), (1, 1), (2, 1), 3, 2),
    new((1, 0), (2, 0), (0, 1), (1, 1), 3, 2),

    new((0, 0), (0, 1), (1, 1), (2, 1), 3, 2),
    new((1, 0), (0, 1), (1, 1), (2, 1), 3, 2),
    new((2, 0), (0, 1), (1, 1), (2, 1), 3, 2),

    new((0, 0), (0, 1), (0, 2), (1, 0), 2, 3),
    new((0, 0), (0, 1), (0, 2), (1, 1), 2, 3),
    new((0, 0), (0, 1), (0, 2), (1, 2), 2, 3),

    new((1, 0), (1, 1), (1, 2), (0, 0), 2, 3),
    new((1, 0), (1, 1), (1, 2), (0, 1), 2, 3),
    new((1, 0), (1, 1), (1, 2), (0, 2), 2, 3),

    new((0, 0), (0, 1), (1, 1), (1, 2), 2, 3),
    new((1, 0), (1, 1), (0, 1), (0, 2), 2, 3),

    new((0, 0), (0, 1), (0, 2), (0, 3), 1, 4),
};
Span<ValueTuple<int, int>> points = stackalloc ValueTuple<int, int>[4];
int max = 0;
foreach (var b in bricks)
{
    var hEnd = n - b.Height;
    points[0] = b.P0;
    points[1] = b.P1;
    points[2] = b.P2;
    points[3] = b.P3;

    for (int i = 0; i <= hEnd; i++)
    {
        var wEnd = m - b.Width;
        for (int j = 0; j <= wEnd; j++)
        {
            int sum = 0;
            foreach (var d in points)
            {
                (var y, var x) = d;
                sum += map[y + i, x + j];
            }
            if (max < sum)
                max = sum;
        }
    }
}
Console.Write(max);

static int ScanInt(StreamReader sr)
{
    int c, ret = 0;
    while ((c = sr.Read()) != ' ' && c != '\n' && c != -1)
    {
        if (c == '\r')
        {
            sr.Read();
            break;
        }
        ret = 10 * ret + (c - '0');
    }
    return ret;
}

struct Brick
{
    public ValueTuple<int, int> P0;
    public ValueTuple<int, int> P1;
    public ValueTuple<int, int> P2;
    public ValueTuple<int, int> P3;
    public int Height;
    public int Width;

    public Brick(ValueTuple<int, int> p0, ValueTuple<int, int> p1, ValueTuple<int, int> p2, ValueTuple<int, int> p3, int width, int length)
    {
        P0 = p0;
        P1 = p1;
        P2 = p2;
        P3 = p3;
        Height = width;
        Width = length;
    }
}
#elif other2
using System;
using System.Collections;
using System.Reflection.Metadata;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string[] s = Console.ReadLine().Split();

        int n = int.Parse(s[0]);
        int m = int.Parse(s[1]);

        int[][] arr = new int[n][];

        for (int i = 0; i < n; i++)
        {
            string[] t = Console.ReadLine().Split();
            int[] ints = new int[m];
            for (int j = 0; j < m; j++)
            {
                ints[j] = int.Parse(t[j]);
            }
            arr[i] = ints;
        }

        int max = 0;

        // ㅣ
        for (int i = 0; i + 3 < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                max = Math.Max(arr[i][j] + arr[i + 1][j] + arr[i + 2][j] + arr[i + 3][j], max);
            }
        }

        // ㅡ
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j + 3 < m; j++)
            {
                max = Math.Max(arr[i][j] + arr[i][j + 1] + arr[i][j + 2] + arr[i][j + 3], max);
            }
        }

        // 2 X 2
        for (int i = 0; i + 1 < n; i++)
        {
            for (int j = 0; j + 1 < m; j++)
            {
                max = Math.Max(arr[i][j + 1] + arr[i + 1][j] + arr[i][j] + arr[i + 1][j + 1], max);
            }
        }

        // 왼쪽 기준
        for (int i = 1; i + 1 < n; i++)
        {
            for (int j = 0; j + 1 < m; j++)
            {
                int a = arr[i - 1][j];
                int b = arr[i + 1][j];
                int c = arr[i - 1][j + 1];
                int d = arr[i][j + 1];
                int e = arr[i + 1][j + 1];

                int sum1 = a + b + Math.Max(c, e);
                int sum2 = a + d + Math.Max(b, e);
                int sum3 = b + c + d;
                max = Math.Max(max, arr[i][j] + Math.Max(sum1, Math.Max(sum2, sum3)));
            }
        }

        // 오른쪽 기준
        for (int i = 1; i + 1 < n; i++)
        {
            for (int j = 1; j < m; j++)
            {
                int a = arr[i + 1][j];
                int b = arr[i - 1][j];
                int c = arr[i + 1][j - 1];
                int d = arr[i][j - 1];
                int e = arr[i - 1][j - 1];

                int sum1 = a + b + Math.Max(c, e);
                int sum2 = a + d + Math.Max(b, e);
                int sum3 = b + c + d;
                max = Math.Max(max, arr[i][j] + Math.Max(sum1, Math.Max(sum2, sum3)));
            }
        }

        // 위쪽 기준
        for (int i = 0; i + 1 < n; i++)
        {
            for (int j = 1; j + 1 < m; j++)
            {
                int a = arr[i][j + 1];
                int b = arr[i][j - 1];
                int c = arr[i + 1][j + 1];
                int d = arr[i + 1][j];
                int e = arr[i + 1][j - 1];

                int sum1 = a + b + Math.Max(c, e);
                int sum2 = a + d + Math.Max(b, e);
                int sum3 = b + c + d;
                max = Math.Max(max, arr[i][j] + Math.Max(sum1, Math.Max(sum2, sum3)));
            }
        }

        // 아래쪽 기준
        for (int i = 1; i < n; i++)
        {
            for (int j = 1; j + 1 < m; j++)
            {
                int a = arr[i][j - 1];
                int b = arr[i][j + 1];
                int c = arr[i - 1][j - 1];
                int d = arr[i - 1][j];
                int e = arr[i - 1][j + 1];

                int sum1 = a + b + Math.Max(c, e);
                int sum2 = a + d + Math.Max(b, e);
                int sum3 = b + c + d;
                max = Math.Max(max, arr[i][j] + Math.Max(sum1, Math.Max(sum2, sum3)));
            }
        }

        Console.Write(max);
    }
}

#elif other3
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace no14500try1
{
    internal class Program
    {
        const bool O = true;
        const bool X = false;

        struct Block
        {
            public bool[,] square; // square[x, y]
            public int sizeX;
            public int sizeY;

            public Block(bool[,] data)
            {
                square = data;
                sizeX = data.GetLength(0);
                sizeY = data.GetLength(1);
            }
        }

        static void Main(string[] args)
        {
            int result = 0;
            string[] recvLineNM = Console.ReadLine().Split(' ');
            int n = int.Parse(recvLineNM[0]);
            int m = int.Parse(recvLineNM[1]);
            int[,] map = new int[m, n];
            for (int y = 0; y < n; ++y)
            {
                string[] recvLine = Console.ReadLine().Split(' ');

                for (int x = 0; x < recvLine.Length; ++x)
                {
                    map[x, y] = int.Parse(recvLine[x]);
                }
            }

            Block[] blocks = new Block[19]
            {
                new Block(
                    new bool[1, 4]
                    {
                        { O, O, O, O }
                    }
                    ),
                new Block(
                    new bool[4, 1]
                    {
                        { O },
                        { O },
                        { O },
                        { O }
                    }
                    ),
                new Block(
                    new bool[2, 2]
                    {
                        { O, O },
                        { O, O }
                    }
                    ),
                new Block(
                    new bool[3, 2]
                    {
                        { O, X },
                        { O, X },
                        { O, O }
                    }
                    ),
                new Block(
                    new bool[2, 3]
                    {
                        { O, O, O },
                        { O, X, X }
                    }
                    ),
                new Block(
                    new bool[3, 2]
                    {
                        { O, O },
                        { X, O },
                        { X, O }
                    }
                    ),
                new Block(
                    new bool[2, 3]
                    {
                        { X, X, O },
                        { O, O, O }
                    }
                    ),
                new Block(
                    new bool[3, 2]
                    {
                        { X, O },
                        { X, O },
                        { O, O }
                    }
                    ),
                new Block(
                    new bool[2, 3]
                    {
                        { O, X, X },
                        { O, O, O }
                    }
                    ),
                new Block(
                    new bool[3, 2]
                    {
                        { O, O },
                        { O, X },
                        { O, X }
                    }
                    ),
                new Block(
                    new bool[2, 3]
                    {
                        { O, O, O },
                        { X, X, O }
                    }
                    ),
                new Block(
                    new bool[3, 2]
                    {
                        { O, X },
                        { O, O },
                        { X, O }
                    }
                    ),
                new Block(
                    new bool[2, 3]
                    {
                        { X, O, O },
                        { O, O, X }
                    }
                    ),
                new Block(
                    new bool[3, 2]
                    {
                        { X, O },
                        { O, O },
                        { O, X }
                    }
                    ),
                new Block(
                    new bool[2, 3]
                    {
                        { O, O, X },
                        { X, O, O }
                    }
                    ),
                new Block(
                    new bool[2, 3]
                    {
                        { O, O, O },
                        { X, O, X }
                    }
                    ),
                new Block(
                    new bool[3, 2]
                    {
                        { O, X },
                        { O, O },
                        { O, X }
                    }
                    ),
                new Block(
                    new bool[2, 3]
                    {
                        { X, O, X },
                        { O, O, O }
                    }
                    ),
                new Block(
                    new bool[3, 2]
                    {
                        { X, O },
                        { O, O },
                        { X, O }
                    }
                    )
            };

            for (int blockIndex = 0; blockIndex < 19; ++blockIndex)
            {
                // pivotX과 pivotY는 테트로미노의 왼쪽 위 포지션입니다.
                for (int pivotX = 0; pivotX < m - blocks[blockIndex].sizeX + 1; ++pivotX)
                {
                    for (int pivotY = 0; pivotY < n - blocks[blockIndex].sizeY + 1; ++pivotY)
                    {
                        int oneSum = 0;
                        
                        for (int blockIndexX = 0; blockIndexX < blocks[blockIndex].sizeX; ++blockIndexX)
                        {
                            for (int blockIndexY = 0; blockIndexY < blocks[blockIndex].sizeY; ++blockIndexY)
                            {
                                if (blocks[blockIndex].square[blockIndexX, blockIndexY])
                                    oneSum += map[pivotX + blockIndexX, pivotY + blockIndexY];
                            }
                        }

                        if (result < oneSum) result = oneSum;
                    }
                }
            }

            Console.WriteLine(result);
        }
    }
}
#endif
}
