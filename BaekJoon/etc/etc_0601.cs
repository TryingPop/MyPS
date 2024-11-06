using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 23
이름 : 배성훈
내용 : 배열 돌리기 4
    문제번호 : 17406번

    구현, 브루트포스, 백트래킹 문제다
    모든 경우를 회전시키며 확인했다
*/

namespace BaekJoon.etc
{
    internal class etc_0601
    {

        static void Main601(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int row, col;
            int[][][] board;
            int[][] rots;
            int op;

            int[] calc;
            bool[] visit;

            int ret = 10_000;
            Solve();

            void Solve()
            {

                Input();
                DFS(0);

                Console.WriteLine(ret);
            }

            void DFS(int _depth)
            {

                if (_depth == op)
                {

                    Copy(0, 1);
                    for (int i = 0; i < _depth; i++)
                    {

                        Rotate(calc[i]);
                    }

                    ret = Math.Min(ret, GetMin());
                    return;
                }

                for (int i = 0; i < op; i++)
                {

                    if (visit[i]) continue;
                    visit[i] = true;

                    calc[_depth] = i;
                    DFS(_depth + 1);
                    visit[i] = false;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 8);
                row = ReadInt();
                col = ReadInt();

                op = ReadInt();
                board = new int[3][][];

                board[0] = new int[row][];
                board[1] = new int[row][];
                board[2] = new int[row][];

                for (int r = 0; r < row; r++)
                {

                    board[0][r] = new int[col];
                    board[1][r] = new int[col];
                    board[2][r] = new int[col];

                    for (int c = 0; c < col; c++)
                    {

                        board[0][r][c] = ReadInt();
                    }
                }

                rots = new int[op][];
                for (int i = 0; i < op; i++)
                {

                    rots[i] = new int[3];
                    for (int j = 0; j < 3; j++)
                    {

                        rots[i][j] = ReadInt();
                    }

                    rots[i][0]--;
                    rots[i][1]--;
                }

                calc = new int[op];
                visit = new bool[op];
                sr.Close();
            }

            void Copy(int _f, int _t)
            {

                for (int i = 0; i < row; i++)
                {

                    for (int j = 0; j < col; j++)
                    {

                        board[_t][i][j] = board[_f][i][j];
                    }
                }
            }

            void Rotate(int _rotIdx)
            {

                Copy(1, 2);

                int midR = rots[_rotIdx][0];
                int midC = rots[_rotIdx][1];
                for (int size = rots[_rotIdx][2]; size >= 1; size--)
                {

                    for (int i = 0; i < size * 2; i++)
                    {

                        board[2][midR - size][midC - size + i + 1] = board[1][midR - size][midC - size + i];
                        board[2][midR - size + i + 1][midC + size] = board[1][midR - size + i][midC + size];
                        board[2][midR + size][midC + size - i - 1] = board[1][midR + size][midC + size - i];
                        board[2][midR + size - i - 1][midC - size] = board[1][midR + size - i][midC - size]; 
                    }
                }

                int[][] temp = board[1];
                board[1] = board[2];
                board[2] = temp;
            }

            int GetMin()
            {

                int ret = 0;
                for (int c = 0; c < col; c++)
                {

                    ret += board[1][0][c];
                }

                for (int r = 1; r < row; r++)
                {

                    int calc = 0;
                    for (int c = 0; c < col; c++)
                    {

                        calc += board[1][r][c];
                    }

                    ret = calc < ret ? calc : ret;
                }

                return ret;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
namespace Baekjoon;

public class Program
{
    static StreamReader _sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

    private static void Main(string[] args)
    {
        int n = ScanInt(), m = ScanInt(), k = ScanInt();
        var map = new int[n, m];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                map[i, j] = ScanInt();
        var rotation = new (int, int, int)[k];
        for (int i = 0; i < k; i++)
        {
            rotation[i] = (ScanInt() - 1, ScanInt() - 1, ScanInt());
        }

        var used = new bool[k];
        var ret = Check(0);
        Console.Write(ret);

        int Check(int level)
        {
            if (level == k)
            {
                return GetCurValue();
            }
            var min = int.MaxValue;
            for (int i = 0; i < k; i++)
            {
                if (!used[i])
                {
                    used[i] = true;
                    (var r, var c, var size) = rotation[i];
                    Rotate(r, c, size);
                    min = Math.Min(Check(level + 1), min);
                    RotateBack(r, c, size);
                    used[i] = false;
                }
            }
            return min;
        }

        void Rotate(int r, int c, int size)
        {
            if (size == 0)
                return;
            var temp = map[r - size, c - size];

            for (int i = 0; i < 2 * size; i++)
            {
                map[r - size + i, c - size] = map[r - size + i + 1, c - size];
            }
            for (int i = 0; i < 2 * size; i++)
            {
                map[r + size, c - size + i] = map[r + size, c - size + i + 1];
            }
            for (int i = 0; i < 2 * size; i++)
            {
                map[r + size - i, c + size] = map[r + size - i - 1, c + size];
            }
            for (int i = 0; i < 2 * size - 1; i++)
            {
                map[r - size, c + size - i] = map[r - size, c + size - i - 1];
            }
            map[r - size, c - size + 1] = temp;
            Rotate(r, c, size - 1);
        }

        void RotateBack(int r, int c, int size)
        {
            if (size == 0)
                return;
            var temp = map[r - size, c - size];
            for (int i = 0; i < 2 * size; i++)
            {
                map[r - size, c - size + i] = map[r - size, c - size + i + 1];
            }
            for (int i = 0; i < 2 * size; i++)
            {
                map[r - size + i, c + size] = map[r - size + i + 1, c + size];
            }
            for (int i = 0; i < 2 * size; i++)
            {
                map[r + size, c + size - i] = map[r + size, c + size - i - 1];
            }
            for (int i = 0; i < 2 * size - 1; i++)
            {
                map[r + size - i, c - size] = map[r + size - i - 1, c - size];
            }
            map[r - size + 1, c - size] = temp;
            RotateBack(r, c, size - 1);
        }

        int GetCurValue()
        {
            var value = int.MaxValue;
            for (int r = 0; r < n; r++)
            {
                var sum = 0;
                for (int c = 0; c < m; c++)
                {
                    sum += map[r, c];
                }
                value = Math.Min(sum, value);
            }
            return value;
        }
    }


    static int ScanInt()
    {
        int c, n = 0;
        while (!((c = _sr.Read()) is ' ' or '\n' or -1))
        {
            if (c == '\r')
            {
                _sr.Read();
                break;
            }
            n = 10 * n + c - '0';
        }
        return n;
    }
}
#endif
}
