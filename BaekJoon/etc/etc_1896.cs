using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 17
이름 : 배성훈
내용 : Load Balancing (Silver)
    문제번호 : 11997번

    브루트포스, 누적합, 값 / 좌표 압축 문제다.
    아이디어는 다음과 같다.
    먼저 좌표압축으로 좌표 값을 줄인다.

    N이 1000이하이므로 좌표압축한 값은 커봐야 1000이다.
    이제 2차원 배열로 소의 수를 저장한다.
    그리고 누적합으로 배열을 수정한다.

    -> 그러면 sum[x][y]는 (0, 0) 에서 (x, y)에 있는 소들의 수가 된다.
    이렇게 O(1)에 각 영역의 소의 수를 찾을 수 있다.

    모두 조사하는 경우 N^2이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1896
    {

        static void Main1896(string[] args)
        {

            int n;
            (int x, int y)[] pos;
            int X, Y;
            Input();

            Compact();

            GetRet();

            void GetRet()
            {

                int[][] sum = new int[X + 1][];
                for (int i = 0; i <= X; i++)
                {

                    sum[i] = new int[Y + 1];
                }

                for (int i = 0; i < n; i++)
                {

                    sum[pos[i].x][pos[i].y]++;
                }

                for (int i = 1; i <= X; i++)
                {

                    for (int j = 0; j <= Y; j++)
                    {

                        sum[i][j] += sum[i - 1][j];
                    }
                }

                for (int j = 1; j <= Y; j++)
                {

                    for (int i = 0; i <= X; i++)
                    {

                        sum[i][j] += sum[i][j - 1];
                    }
                }

                int ret = n;
                for (int x = 1; x <= X; x++)
                {

                    for (int y = 1; y <= Y; y++)
                    {

                        int a1 = sum[x][y];
                        int a2 = sum[X][y] - sum[x][y];
                        int a3 = sum[x][Y] - sum[x][y];
                        int a4 = sum[X][Y] - sum[x][Y] - sum[X][y] + sum[x][y];

                        int m = Math.Max(Math.Max(a1, a2), Math.Max(a3, a4));
                        ret = Math.Min(ret, m);
                    }
                }

                Console.Write(ret);
            }

            void Compact()
            {

                Array.Sort(pos, (x, y) => x.x.CompareTo(y.x));
                int prev = -1;
                X = 0;
                for (int i = 0; i < n; i++)
                {

                    if (prev != pos[i].x)
                    {

                        X++;
                        prev = pos[i].x;
                    }

                    pos[i].x = X;
                }

                Array.Sort(pos, (x, y) => x.y.CompareTo(y.y));
                prev = -1;
                Y = 0;

                for (int i = 0; i < n; i++)
                {

                    if (prev != pos[i].y)
                    {

                        Y++;
                        prev = pos[i].y;
                    }

                    pos[i].y = Y;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                pos = new (int x, int y)[n];
                for (int i = 0; i < n; i++)
                {

                    pos[i] = (ReadInt(), ReadInt());
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }
}
