using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 27
이름 : 배성훈
내용 : 지구 온난화
    문제번호 : 5212번

    구현, 시뮬레이션 문제다
    인접한 칸에 물이 있는지 일일히 세었다
    그리고 모두 물이 찼는지 테두리를 확인해 땅을 지웠다
*/

namespace BaekJoon.etc
{
    internal class etc_0635
    {

        static void Main635(string[] args)
        {

            StreamReader sr;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 1024 * 16);
                int[] size = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
                string[] map = new string[size[0]];
                char[][] ret = new char[size[0]][];

                for (int i = 0; i < size[0]; i++)
                {

                    map[i] = sr.ReadLine();
                    ret[i] = new char[size[1]];
                }

                sr.Close();

                int[] dirR = { -1, 0, 1, 0 };
                int[] dirC = { 0, -1, 0, 1 };

                for (int r = 0; r < size[0]; r++)
                {

                    for (int c = 0; c < size[1]; c++)
                    {


                        if (map[r][c] == '.')
                        {

                            ret[r][c] = '.';
                            continue; 
                        }

                        int cnt = 0;

                        for (int i = 0; i < 4; i++)
                        {

                            int nextR = r + dirR[i];
                            int nextC = c + dirC[i];
                            if (ChkInvalidPos(nextR, nextC, size[0], size[1]) || map[nextR][nextC] == '.') cnt++;
                        }

                        if (cnt >= 3) ret[r][c] = '.';
                        else ret[r][c] = 'X';
                    }
                }

                int minR = 0;
                int maxR = size[0] - 1;
                int minC = 0;
                int maxC = size[1] - 1;
                for (int r = 0; r < size[0]; r++)
                {

                    bool find = false;
                    for (int c = 0; c < size[1]; c++)
                    {

                        if (ret[r][c] == '.') continue;

                        find = true;
                        break;
                    }

                    if (find) break;
                    minR++;
                }

                for (int r = size[0] - 1; r >= minR; r--)
                {

                    bool find = false;
                    for (int c = 0; c < size[1]; c++)
                    {

                        if (ret[r][c] == '.') continue;

                        find = true;
                        break;
                    }

                    if (find) break;
                    maxR--;
                }

                for (int c = 0; c < size[1]; c++)
                {

                    bool find = false;
                    for (int r = minR; r <= maxR; r++)
                    {

                        if (ret[r][c] == '.') continue;

                        find = true;
                        break;
                    }

                    if (find) break;
                    minC++;
                }

                for (int c = size[1] - 1; c >= minC; c--)
                {

                    bool find = false;
                    for(int r = minR; r <= maxR; r++)
                    {

                        if (ret[r][c] == '.') continue;

                        find = true;
                        break;
                    }

                    if (find) break;
                    maxC--;
                }

                for(int r = minR; r <= maxR; r++)
                {

                    for (int c = minC; c <= maxC; c++)
                    {

                        Console.Write(ret[r][c]);
                    }
                    Console.Write('\n');
                }
            }

            bool ChkInvalidPos(int _r, int _c, int _maxR, int _maxC)
            {

                if (_r < 0 || _r >= _maxR || _c < 0 || _c >= _maxC) return true;
                return false;
            }
        }
    }

#if other
namespace boj_5212
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());

            string[] input = sr.ReadLine().Split();

            int[] dirY = { -1, 0, 1, 0 }, dirX = { 0, -1, 0, 1 };

            int R = int.Parse(input[0]), C = int.Parse(input[1]);

            char[,] board = new char[R, C];
            char[,] after = new char[R, C];

            int minY = R, maxY = 0, minX = C, maxX = 0;

            for (int i = 0; i < R; i++)
            {
                string row = sr.ReadLine().TrimEnd();

                for (int j = 0; j < C; j++)
                {
                    board[i, j] = row[j];
                }
            }

            for (int i = 0; i < R; i++)
            {
                for (int j = 0; j < C; j++)
                {
                    int cnt = 0;

                    for (int d = 0; d < 4; d++)
                    {
                        if (i + dirY[d] < 0 || i + dirY[d] >= R || j + dirX[d] < 0 || j + dirX[d] >= C || board[i + dirY[d], j + dirX[d]] == '.')
                            cnt++;
                    }

                    if (cnt >= 3)
                        after[i, j] = '.';

                    else
                    {
                        after[i, j] = board[i, j];

                        if (after[i, j] == 'X')
                        {
                            minY = (int)MathF.Min(i, minY);
                            minX = (int)MathF.Min(j, minX);
                            maxY = (int)MathF.Max(i, maxY);
                            maxX = (int)MathF.Max(j, maxX);
                        }
                    }
                }
            }

            for (int i = minY; i < maxY + 1; i++)
            {
                for (int j = minX; j < maxX + 1; j++)
                {
                    sw.Write(after[i, j]);
                }
                sw.WriteLine();
            }

            sw.Close();
        }
    }
}
#endif
}
