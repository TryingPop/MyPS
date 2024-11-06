using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 29
이름 : 배성훈
내용 : 배열 돌리기 6
    문제번호 : 20327번

    구현, 시뮬레이션 문제다
    회전을 일일히 시행하면서 결과를 찾는다
    회전 함수를 하나씩 나눠서 구현해서 400줄이 넘어간다
*/

namespace BaekJoon.etc
{
    internal class etc_0655
    {

        static void Main655(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;
            int size;
            int len;
            int[][] board;
            int op;
            int[] calc;

            Solve();

            void Solve()
            {

                Input();

                Rotate();

                Output();
            }

            void Output()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                for (int r = 0; r < len; r++)
                {

                    for (int c = 0; c < len; c++)
                    {

                        sw.Write($"{board[r][c]} ");
                    }

                    sw.Write('\n');
                }

                sw.Close();
            }

            void Rotate()
            {

                for (int i = 0; i < op; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    switch (f)
                    {

                        case 1:
                            Func1(b);
                            break;

                        case 2:
                            Func2(b);
                            break;

                        case 3:
                            Func3(b);
                            break;

                        case 4:
                            Func4(b);
                            break;

                        case 5:
                            Func5(b);
                            break;

                        case 6:
                            Func6(b);
                            break;

                        case 7:
                            Func7(b);
                            break;

                        case 8:
                            Func8(b);
                            break;

                        default:
                            break;
                    }
                }

                sr.Close();
            }

            void Func1(int _size)
            {

                if (_size == 0) return;
                int funcLen = 1 << _size;
                int funcSize = len / funcLen;

                int half = funcLen / 2;
                for (int r = 0; r < funcSize; r++)
                {

                    for (int i = 0; i < half; i++)
                    {

                        int sR = r * funcLen + i;
                        int eR = (r + 1) * funcLen - 1 - i;

                        for (int c = 0; c < len; c++)
                        {

                            int temp = board[sR][c];
                            board[sR][c] = board[eR][c];
                            board[eR][c] = temp;
                        }
                    }
                }
            }

            void Func2(int _size)
            {

                if (_size == 0) return;
                int funcLen = 1 << _size;
                int funcSize = len / funcLen;

                int half = funcLen / 2;
                for (int c = 0; c < funcSize; c++)
                {

                    for (int i = 0; i < half; i++)
                    {

                        int sC = c * funcLen + i;
                        int eC = (c + 1) * funcLen - 1 - i;

                        for (int r = 0; r < len; r++)
                        {

                            int temp = board[r][sC];
                            board[r][sC] = board[r][eC];
                            board[r][eC] = temp;
                        }
                    }
                }
            }

            void Func3(int _size)
            {

                if (_size == 0) return;
                int funcLen = 1 << _size;
                int funcSize = len / funcLen;
                int ring = funcLen / 2;

                for (int r = 0; r < funcSize; r++)
                {

                    for (int c = 0; c < funcSize; c++)
                    {

                        for (int i = 0; i < ring; i++)
                        {

                            for (int j = 0; j < ring; j++)
                            {

                                int r1 = r * funcLen + i;
                                int c1 = c * funcLen + j;

                                int r2 = (r + 1) * funcLen - 1 - j;
                                int c2 = c * funcLen + i;

                                int r3 = (r + 1) * funcLen - 1 - i;
                                int c3 = (c + 1) * funcLen - 1 - j;

                                int r4 = r * funcLen + j;
                                int c4 = (c + 1) * funcLen - 1 - i;

                                int temp = board[r1][c1];
                                board[r1][c1] = board[r2][c2];
                                board[r2][c2] = board[r3][c3];
                                board[r3][c3] = board[r4][c4];
                                board[r4][c4] = temp;
                            }
                        }
                    }
                }
            }

            void Func4(int _size)
            {

                if (_size == 0) return;
                int funcLen = 1 << _size;
                int funcSize = len / funcLen;
                int ring = funcLen / 2;

                for (int r = 0; r < funcSize; r++)
                {

                    for (int c = 0; c < funcSize; c++)
                    {

                        for (int i = 0; i < ring; i++)
                        {

                            for (int j = 0; j < ring; j++)
                            {

                                int r1 = r * funcLen + i;
                                int c1 = c * funcLen + j;

                                int r2 = (r + 1) * funcLen - 1 - j;
                                int c2 = c * funcLen + i;

                                int r3 = (r + 1) * funcLen - 1 - i;
                                int c3 = (c + 1) * funcLen - 1 - j;

                                int r4 = r * funcLen + j;
                                int c4 = (c + 1) * funcLen - 1 - i;

                                int temp = board[r1][c1];
                                board[r1][c1] = board[r4][c4];
                                board[r4][c4] = board[r3][c3];
                                board[r3][c3] = board[r2][c2];
                                board[r2][c2] = temp;
                            }
                        }
                    }
                }
            }

            void Func5(int _size)
            {

                int funcLen = 1 << _size;
                int funcSize = len / funcLen;

                int half = funcSize / 2;
                for (int r = 0; r < half; r++)
                {

                    int sR = r * funcLen;
                    int oR = (funcSize - 1 - r) * funcLen;
                    
                    for (int i = 0; i < funcLen; i++)
                    {

                        for (int c = 0; c < len; c++)
                        {

                            int temp = board[sR + i][c];
                            board[sR + i][c] = board[oR + i][c];
                            board[oR + i][c] = temp;
                        }
                    }
                }
            }

            void Func6(int _size)
            {

                int funcLen = 1 << _size;
                int funcSize = len / funcLen;

                int half = funcSize / 2;
                for (int c = 0; c < half; c++)
                {

                    int sC = c * funcLen;
                    int oC = (funcSize - 1 - c) * funcLen;

                    for (int i = 0; i < funcLen; i++)
                    {

                        for (int r = 0; r < len; r++)
                        {

                            int temp = board[r][sC + i];
                            board[r][sC + i] = board[r][oC + i];
                            board[r][oC + i] = temp;
                        }
                    }
                }
            }

            void Func7(int _size)
            {

                int funcLen = 1 << _size;
                int funcSize = len / funcLen;

                int half = funcSize / 2;
                for (int r = 0; r < half; r++)
                {

                    for (int c = 0; c < half; c++)
                    {

                        int r1 = r * funcLen;
                        int c1 = c * funcLen;

                        int r2 = (funcSize - 1 - c) * funcLen;
                        int c2 = r * funcLen;

                        int r3 = (funcSize - 1 - r) * funcLen;
                        int c3 = (funcSize - 1 - c) * funcLen;

                        int r4 = c * funcLen;
                        int c4 = (funcSize - 1 - r) * funcLen;

                        for (int i = 0; i < funcLen; i++)
                        {

                            for (int j = 0; j < funcLen; j++)
                            {

                                int temp = board[r1 + i][c1 + j];
                                board[r1 + i][c1 + j] = board[r2 + i][c2 + j];
                                board[r2 + i][c2 + j] = board[r3 + i][c3 + j];
                                board[r3 + i][c3 + j] = board[r4 + i][c4 + j];
                                board[r4 + i][c4 + j] = temp;
                            }
                        }
                    }
                }
            }

            void Func8(int _size)
            {

                int funcLen = 1 << _size;
                int funcSize = len / funcLen;

                int half = funcSize / 2;
                for (int r = 0; r < half; r++)
                {

                    for (int c = 0; c < half; c++)
                    {

                        int r1 = r * funcLen;
                        int c1 = c * funcLen;

                        int r2 = (funcSize - 1 - c) * funcLen;
                        int c2 = r * funcLen;

                        int r3 = (funcSize - 1 - r) * funcLen;
                        int c3 = (funcSize - 1 - c) * funcLen;

                        int r4 = c * funcLen;
                        int c4 = (funcSize - 1 - r) * funcLen;

                        for (int i = 0; i < funcLen; i++)
                        {

                            for (int j = 0; j < funcLen; j++)
                            {

                                int temp = board[r1 + i][c1 + j];
                                board[r1 + i][c1 + j] = board[r4 + i][c4 + j];
                                board[r4 + i][c4 + j] = board[r3 + i][c3 + j];
                                board[r3 + i][c3 + j] = board[r2 + i][c2 + j];
                                board[r2 + i][c2 + j] = temp;
                            }
                        }
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                size = ReadInt();
                op = ReadInt();

                len = 1 << size;
                board = new int[len][];

                calc = new int[len];

                for (int r = 0; r < len; r++)
                {

                    board[r] = new int[len];
                    for (int c = 0; c < len; c++)
                    {

                        board[r][c] = ReadInt();
                    }
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;

                while((c = sr.Read()) != -1 && c != '\n' && c != ' ')
                {

                    if (c == '\r') continue;
                    else if (c == '-')
                    {

                        plus = false;
                        continue;
                    }
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
using System;
using System.Text;

namespace Algorithm
{
    public class MainApp
    {
        static private readonly int m_MAX_N = 128;
        static private int[,] m_Arr = new int[m_MAX_N, m_MAX_N];
        static private int[,] m_Cache = new int[m_MAX_N, m_MAX_N];

        static private void Main(string[] args)
        {
            string[] inputLine = Console.ReadLine().Split(' ');
            int N = int.Parse(inputLine[0]);
            int R = int.Parse(inputLine[1]);

            for (int i = 0, endI = 1 << N; i < endI; ++i)
            {
                inputLine = Console.ReadLine().Split(' ');
                for (int j = 0, endJ = 1 << N; j < endJ; ++j)
                {
                    m_Arr[i, j] = int.Parse(inputLine[j]);
                }
            }

            for (int i = 0; i < R; ++i)
            {
                inputLine = Console.ReadLine().Split(' ');
                int k = int.Parse(inputLine[0]);
                int l = int.Parse(inputLine[1]);

                if (k >= 1 & k <= 4)
                {
                    OperateType1(k, l, N);
                }
                else
                {
                    OperateType2(k, l, N);
                }
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0, endI = 1 << N; i < endI; ++i)
            {
                for (int j = 0, endJ = 1 << N; j < endJ; ++j)
                {
                    sb.Append(m_Arr[i, j]);
                    sb.Append(' ');
                }

                sb.Append('\n');
            }

            Console.Write(sb);
        }

        static private void OperateType1(int K, int L, int N)
        {
            switch (K)
            {
                case 1:
                    {
                        InvertUpDown(L, N);
                        break;
                    }

                case 2:
                    {
                        InvertLeftRight(L, N);
                        break;
                    }

                case 3:
                    {
                        RotateRight(L, N);
                        break;
                    }

                case 4:
                    {
                        RotateLeft(L, N);
                        break;
                    }

                default:
                    break;
            }
        }

        static private void OperateType2(int K, int L, int N)
        {
            switch (K)
            {
                case 5:
                    {
                        InvertUpDown(N, N);
                        InvertUpDown(L, N);
                        break;
                    }

                case 6:
                    {
                        InvertLeftRight(N, N);
                        InvertLeftRight(L, N);
                        break;
                    }

                case 7:
                    {
                        RotateRight(N, N);
                        RotateLeft(L, N);
                        break;
                    }

                case 8:
                    {
                        RotateLeft(N, N);
                        RotateRight(L, N);
                        break;
                    }

                default:
                    break;
            }
        }

        static private void InvertUpDown(int L, int N)
        {
            int totalSize = 1 << N;
            int step = 1 << L;

            for (int i = 0; i < totalSize; i += step)
            {
                for (int j = 0; j < totalSize; ++j)
                {
                    for (int k = 0, endK = step / 2; k < endK; ++k)
                    {
                        int currentIndexY = i + k;
                        int pairIndexY = i + step - 1 - k;

                        int temp = m_Arr[currentIndexY, j];
                        m_Arr[currentIndexY, j] = m_Arr[pairIndexY, j];
                        m_Arr[pairIndexY, j] = temp;
                    }
                }
            }
        }

        static private void InvertLeftRight(int L, int N)
        {
            int totalSize = 1 << N;
            int step = 1 << L;

            for (int i = 0; i < totalSize; ++i)
            {
                for (int j = 0; j < totalSize; j += step)
                {
                    for (int k = 0, endK = step / 2; k < endK; ++k)
                    {
                        int currentIndexX = j + k;
                        int pairIndexX = j + step - 1 - k;

                        int temp = m_Arr[i, currentIndexX];
                        m_Arr[i, currentIndexX] = m_Arr[i, pairIndexX];
                        m_Arr[i, pairIndexX] = temp;
                    }
                }
            }
        }

        static private void RotateRight(int L, int N)
        {
            int totalSize = (1 << N);
            int step = (1 << L);

            for (int i = 0; i < totalSize; ++i)
            {
                for (int j = 0; j < totalSize; ++j)
                {
                    m_Cache[i, j] = m_Arr[i, j];
                }
            }

            for (int i = 0; i < totalSize; i += step)
            {
                for (int j = 0; j < totalSize; j += step)
                {
                    // 오른쪽 90도 회전 결과를 저장.
                    int writingX = j;
                    int writingY = i;
                    for (int curX = j, endX = j + step; curX < endX; ++curX)
                    {
                        for (int curY = i + step - 1; curY >= i; --curY)
                        {
                            m_Arr[writingY, writingX] = m_Cache[curY, curX];
                            ++writingX;
                        }
                        ++writingY;
                        writingX = j;
                    }
                }
            }
        }

        static private void RotateLeft(int L, int N)
        {
            int totalSize = 1 << N;
            int step = 1 << L;

            for (int i = 0; i < totalSize; ++i)
            {
                for (int j = 0; j < totalSize; ++j)
                {
                    m_Cache[i, j] = m_Arr[i, j];
                }
            }

            for (int i = 0; i < totalSize; i += step)
            {
                for (int j = 0; j < totalSize; j += step)
                {
                    // 왼쪽 90도 회전 결과를 저장.
                    int writingX = j;
                    int writingY = i;
                    for (int curX = j + step - 1, endX = j; curX >= endX; --curX)
                    {
                        for (int curY = i, endY = i + step; curY < endY; ++curY)
                        {
                            m_Arr[writingY, writingX] = m_Cache[curY, curX];
                            ++writingX;
                        }
                        ++writingY;
                        writingX = j;
                    }
                }
            }
        }
    }
}
#endif
}
