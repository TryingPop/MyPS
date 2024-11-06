using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 15
이름 : 배성훈
내용 : 불 끄기
    문제번호 : 14939번

    그리디, 브루트포스, 비트마스킹 문제다
    아이디어가 떠오르지 않아 검색을 해서 풀이방법을 알고난 뒤 풀었다
    아이디어는 다음과 같다
    모든 전구를 껐다 켰다 하는 경우는 2^100이므로 시간초과가 자명하다

    맨 위에 줄만 모든 경우를 확인한다
    그리고 각 경우에 대해 두 번째줄부터 위에 켜져 있으면 현재 전구를 끄는 연산을 하며
    모두 끌 수 있는지 확인하고 최소값을 기록한다
    이렇게 찾은 최솟값이 정답이 된다

    다 풀고 힌트를 보니, 이차원 bool 배열로 전구 상태를 나타냈는데
    비트마스킹 방법도 좋아보인다
*/

namespace BaekJoon.etc
{
    internal class etc_0882
    {

        static void Main882(string[] args)
        {

            int INF = 1_001;
            int ret;
            StreamReader sr;
            bool[][] initMap, calcMap;
            int[] dirR, dirC;
            Solve();
            void Solve()
            {

                Input();

                DFS();

                Console.Write(ret);
            }

            void DFS(int _depth = 0, int _cnt = 0)
            {

                if (_depth == 10)
                {

                    InitCalcMap();
                    _cnt += GetRet();

                    if (_cnt < ret) ret = _cnt;
                    return;
                }

                DFS(_depth + 1, _cnt);

                ChangeBulb(initMap, 0, _depth);
                DFS(_depth + 1, _cnt + 1);
                ChangeBulb(initMap, 0, _depth);
            }

            void InitCalcMap()
            {

                for (int i = 0; i < 10; i++)
                {

                    for (int j = 0; j < 10; j++)
                    {

                        calcMap[i][j] = initMap[i][j];
                    }
                }
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                return _r < 0 || _c < 0 || _r >= 10 || _c >= 10;
            }

            void ChangeBulb(bool[][] _map, int _r, int _c)
            {

                _map[_r][_c] = !_map[_r][_c];

                for (int i = 0; i < 4; i++)
                {

                    int nextR = _r + dirR[i];
                    int nextC = _c + dirC[i];

                    if (ChkInvalidPos(nextR, nextC)) continue;
                    _map[nextR][nextC] = !_map[nextR][nextC];
                }
            }

            int GetRet()
            {

                int ret = 0;
                for (int r = 1; r < 10; r++)
                {

                    for (int c = 0; c < 10; c++)
                    {

                        if (!calcMap[r - 1][c]) continue;
                        ChangeBulb(calcMap, r, c);
                        ret++;
                    }
                }

                for (int c = 0; c < 10; c++)
                {

                    if (calcMap[9][c]) return INF;
                }

                return ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                initMap = new bool[10][];
                calcMap = new bool[10][];

                for (int i = 0; i < 10; i++)
                {

                    initMap[i] = new bool[10];
                    calcMap[i] = new bool[10];

                    for (int j = 0; j < 10; j++)
                    {

                        int cur = sr.Read();
                        if (cur == '#') continue;
                        initMap[i][j] = true;
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };

                ret = INF;
                sr.Close();
            }
        }
    }

#if other
bool[,] orinGrid = new bool[10, 10];
for (int i = 0; i < 10; i++)
{
    string str = Console.ReadLine()!;
    for (int j = 0; j < 10; j++)
        orinGrid[i, j] = str[j] == 'O';
}

int[] dx = { 0, 0, 1, 0, -1 };
int[] dy = { 0, 1, 0, -1, 0 };
int minCount = int.MaxValue;
bool[,] tempGrid = new bool[10, 10];

int count = 0;

PressFirstLine();
Console.WriteLine(minCount == int.MaxValue ? -1 : minCount);

void PressFirstLine()
{
    for (int i = 0; i < 1 << 10; i++)
    {
        tempGrid = (bool[,])orinGrid.Clone();   // 원본은 유지해야 하니까
        count = 0;
        for (int j = 0; j < 10; j++)
        {
            if ((i & (1 << j)) != 0)
            {
                count++;
                PressButton(0, j);
            }
        }

        CheckAndPressButton();
    }
}

void CheckAndPressButton()
{
    for (int i = 1; i < 10; i++)    // 첫째 줄은 이미 누른 상태에서 검사하기에 첫째 줄은 건너뛴다
    {
        for (int j = 0; j < 10; j++)
        {
            if (tempGrid[i - 1, j])
            {
                PressButton(i, j);
                count++;
            }
        }
    }

    // 윗부분의 전구를 모두 끄며 왔으니 맨 아랫 줄만 검사하면 된다
    for (int i = 0; i < 10; i++)
        if (tempGrid[9, i]) return;

    if (count < minCount)
        minCount = count;
}

void PressButton(int y, int x)
{
    for (int i = 0; i < 5; i++)
    {
        int nextY = dy[i] + y;
        int nextX = dx[i] + x;
        if (nextY >= 10 || nextY < 0 || nextX >= 10 || nextX < 0)
            continue;

        tempGrid[nextY, nextX] = !tempGrid[nextY, nextX];
    }
}

#elif other2
StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

int N = 10;
(int r, int c)[] finder = { (0, 0), (1, 0), (-1, 0), (0, 1), (0, -1) };
string[] map = new string[N];
for (int i = 0; i < N; i++)
    map[i] = sr.ReadLine();
bool[,] light = new bool[N + 2, N + 2];
for (int i = 0; i < N; i++)
    for (int j = 0; j < N; j++)
        light[i + 1, j + 1] = (map[i][j] == 'O');

int count;
bool[,] tmpL;
for (int i=0; i<(1<<N); i++)
{
    count = 0;
    tmpL = (bool[,])light.Clone();
    for(int j=0; j<N; j++)
        if((i&(1<<j)) != 0)
            changeLight(1, j+1);

    for(int j=2; j<=N; j++)
        for(int k=1; k<=N; k++)
            if (tmpL[j-1, k])
                changeLight(j, k);

    bool failed = false;
    for (int j = 1; j <= N; j++)
        if (tmpL[N, j])
            failed = true;

    if (!failed)
    {
        sw.WriteLine(count);
        break;
    }    
}

sr.Close();
sw.Close();

void changeLight(int x, int y)
{
    for(int i=0; i<5; i++)
        tmpL[x + finder[i].r, y + finder[i].c] ^= true;
    count++;
}
#elif other3
using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var original = new bool[10, 10];
        for (var y = 0; y < 10; y++)
        {
            var s = sr.ReadLine().Select(ch => ch == 'O').ToArray();

            for (var x = 0; x < 10; x++)
                original[y, x] = s[x];
        }

        var move = new[] { (0, 0), (-1, 0), (1, 0), (0, -1), (0, 1) };
        var minmove = default(int?);

        for (var mask = 0; mask < (1 << 10); mask++)
        {
            var map = original.Clone() as bool[,];
            var moveCount = 0;

            for (var idx = 0; idx < 10; idx++)
                if ((mask & (1 << idx)) != 0)
                {
                    Flip(map, 0, idx, move);
                    moveCount++;
                }

            for (var y = 0; y < 9; y++)
                for (var x = 0; x < 10; x++)
                    if (map[y, x])
                    {
                        Flip(map, y + 1, x, move);
                        moveCount++;
                    }

            var isSolved = true;
            for (var x = 0; x < 10; x++)
                if (map[9, x])
                    isSolved = false;

            if (isSolved)
                minmove = Math.Min(minmove ?? moveCount, moveCount);
        }

        sw.WriteLine(minmove ?? -1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static void Flip(bool[,] map, int y, int x, (int dy, int dx)[] move)
    {
        foreach (var (dy, dx) in move)
        {
            var (ny, nx) = (y + dy, x + dx);
            if (0 <= nx && nx < 10 && 0 <= ny && ny < 10)
                map[ny, nx] ^= true;
        }
    }
}

#endif
}
