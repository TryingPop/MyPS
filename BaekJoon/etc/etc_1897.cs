using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 17
이름 : 배성훈
내용 : Load Balancing (Bronze)
    문제번호 : 12001번

    브루트포스, 좌표 압축 문제다.
    b의 변수가 추가로 더 들어온다.
    해당 부분 코드만 추가하면 Silver의 방법으로 통과된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1897
    {

        static void Main1897(string[] args)
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
                int b = ReadInt();
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
