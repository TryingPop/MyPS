using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 11
이름 : 배성훈
내용 : 욕심쟁이 판다
    문제번호 : 1937번

    해당 지점에서 가장 긴 길이가 몇인지 DFS 탐색하며 해당 값을 저장했다
    100ms로 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0196
    {

        static void Main196(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            int[,] board = new int[n, n];
            int[,] memo = new int[n, n];

            for (int r = 0; r < n; r++)
            {

                for (int c = 0; c < n; c++)
                {

                    board[r, c] = ReadInt(sr);
                    memo[r, c] = -1;
                }
            }

            sr.Close();

            int[] dirR = { -1, 1, 0, 0 };
            int[] dirC = { 0, 0, -1, 1 };

            int ret = 0;
            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < n; j++)
                {

                    if (memo[i, j] == -1)
                    {

                        // 기록이 안되었다면 DFS 탐색
                        int calc = DFS(board, memo, i, j, n, dirR, dirC);
                        // 결과값 확인
                        if (calc > ret) ret = calc;
                    }
                }
            }

            Console.WriteLine(ret);
        }

        static int DFS(int[,] _board, int[,] _memo, int _curR, int _curC, int _n, int[] _dirR, int[] _dirC)
        {

            // 이미 찾은 장소면 해당 값 반환
            if (_memo[_curR, _curC] != -1) return _memo[_curR, _curC];
            // 못찾은 곳이면 0으로 초기화
            _memo[_curR, _curC] = 0;

            int chk = 0;

            for (int i = 0; i < 4; i++)
            {

                int nextR = _curR + _dirR[i];
                int nextC = _curC + _dirC[i];

                // 막혀있거나 크거나 같은 값이면 못 이어 붙이므로 넘긴다
                if (ChkInvalidPos(nextR, nextC, _n) || _board[_curR, _curC] >= _board[nextR, nextC]) continue;

                // 옆칸 탐색
                int calc = DFS(_board, _memo, nextR, nextC, _n, _dirR, _dirC);
                // 최장 길이면 해당 값
                if (calc > chk) chk = calc;
            }

            // +1은 가장 긴 곳에 현재 칸 추가
            _memo[_curR, _curC] = chk + 1;
            return _memo[_curR, _curC];
        }

        static bool ChkInvalidPos(int _r, int _c, int _n)
        {

            if (_r < 0 || _r >= _n || _c < 0 || _c >= _n) return true;
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
using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

var n = ScanInt();
var map = new int[n, n];
for (int i = 0; i < n; i++)
    for (int j = 0; j < n; j++)
        map[i, j] = ScanInt();

var moving = new int[n, n];
var max = 0;
for (int i = 0; i < n; i++)
{
    for (int j = 0; j < n; j++)
    {
        max = Math.Max(Search(i, j), max);
    }
}
Console.Write(max);

int Search(int r, int c)
{
    ref var value = ref moving[r, c];
    if (value == 0)
    {
        var curBamboo = map[r, c];

        var preValue = 0;
        if (c >= 1)
            TrySearch(0, -1);
        if (c < n - 1)
            TrySearch(0, 1);
        if (r >= 1)
            TrySearch(-1, 0);
        if (r < n - 1)
            TrySearch(1, 0);
        value = preValue + 1;

        void TrySearch(int rAdd, int cAdd)
        {
            int rNew = r + rAdd, cNew = c + cAdd;
            if (curBamboo < map[rNew, cNew])
                preValue = Math.Max(Search(rNew, cNew), preValue);
        }
    }
    return value;
}

int ScanInt()
{
    int c, n = 0;
    while (!((c = sr.Read()) is ' ' or '\n'))
    {
        if (c == '\r')
        {
            sr.Read();
            break;
        }
        n = 10 * n + c - '0';
    }
    return n;
}
#endif
}
