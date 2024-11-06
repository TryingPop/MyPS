using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 22
이름 : 배성훈
내용 : 숫자 정사각형
    문제번호 : 1051번

    구현, 브루트포스 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0597
    {

        static void Main597(string[] args)
        {

            StreamReader sr;
            int row, col;
            int[,] board;

            Solve();

            void Solve()
            {

                Input();

                int ret = Find();
                Console.WriteLine(ret * ret);
            }

            int Find()
            {

                for (int size = Math.Min(row, col); size >= 2; size--)
                {

                    for (int r = 0; r <= row - size; r++)
                    {

                        for (int c = 0; c <= col - size; c++)
                        {

                            int cur = board[r, c];
                            if (cur != board[r, c + size - 1]) continue;
                            if (cur != board[r + size - 1, c]) continue;
                            if (cur != board[r + size - 1, c + size - 1]) continue;

                            return size;
                        }
                    }
                }

                return 1;
            }

            void Input()
            {

                sr = new(new BufferedStream(Console.OpenStandardInput()));

                row = ReadInt();
                col = ReadInt();

                board = new int[row, col];

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        board[r, c] = sr.Read() - '0';
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                sr.Close();
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
internal class Program
{
    static int rowMax, colMax;
    static string[]? map;
    private static void Main(string[] args)
    {
        int min;
        using (var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput())))
        {
            rowMax = ScanInt(sr);
            colMax = ScanInt(sr);
            min = Math.Min(rowMax, colMax);
            map = new string[rowMax];
            for (int i = 0; i < rowMax; i++)
                map[i] = sr.ReadLine()!;
        }
        int maxLength = 1;
        for (int i = min; i >= 2; i--)
        {
            if (SquareExist(i))
            {
                maxLength = i;
                break;
            }
        }
        Console.Write(maxLength * maxLength);
    }

    static bool SquareExist(int length)
    {
        var increasing = length - 1;
        for (int r = 0; r < rowMax - increasing; r++)
        {
            for (int c = 0; c < colMax - increasing; c++)
            {
                var curValue = map![r][c];
                if (map[r + increasing][c] == curValue &&
                    map[r][c + increasing] == curValue &&
                    map[r + increasing][c + increasing] == curValue)
                    return true;
            }
        }
        return false;
    }

    static int ScanInt(StreamReader sr)
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
}
#endif
}
