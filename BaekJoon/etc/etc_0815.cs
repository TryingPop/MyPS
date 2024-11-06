using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. -
이름 : 배성훈
내용 : 별 찍기 - 11
    문제번호 : 2448번

    재귀 문제다
    분할을 하면 중앙 삼각형, 아래 왼쪽 삼각형, 아래 오른쪽 삼각형
    이렇게 늘어난다

    중앙 삼각형은 삼각형의 절반을 띄워야한다
    
*/

namespace BaekJoon.etc
{
    internal class etc_0815
    {

        static void Main815(string[] args)
        {

            char EMPTY = ' ';
            char STAR = '*';
            StreamWriter sw;
            int n;

            int[][] map;
            int[][] tri;

            Solve();
            void Solve()
            {

                Init();

                DNC(n / 3, 0, 0);

                for (int r = 0; r < map.Length; r++)
                {

                    for (int c = 0; c < map[r].Length; c++)
                    {

                        sw.Write(map[r][c] == '*' ? STAR : EMPTY);
                    }

                    sw.Write('\n');
                }

                sw.Close();
            }

            void DNC(int _n, int _r, int _c)
            {

                if (_n == 1)
                {

                    // 별그리기
                    for (int r = 0; r < 3; r++)
                    {

                        for (int c = 0; c < 5; c++)
                        {

                            map[_r + r][_c + c] = tri[r][c];
                        }
                    }

                    return;
                }


                // 오른쪽 절반만큼 이동
                DNC(_n / 2, _r, _c + 3 * _n / 2);
                // 왼쪽 아래로 이동
                DNC(_n / 2, _r + 3 * _n / 2, _c);
                // 오른쪽 아래로 이동
                DNC(_n / 2, _r + 3 * _n / 2, _c + 3 * _n);
            }

            void Init()
            {

                n = int.Parse(Console.ReadLine());
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                map = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    map[i] = new int[2 * n];
                }

                tri = new int[3][];

                tri[0] = new int[5] { ' ', ' ', '*', ' ', ' ' };
                tri[1] = new int[5] { ' ', '*', ' ', '*', ' ' };
                tri[2] = new int[5] { '*', '*', '*', '*', '*' };
            }
        }
    }

#if other
StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

int N = int.Parse(sr.ReadLine());
string[] baseString = { "  *  ", " * * ", "*****" };

void DrawPart(int n, int line)
{
    if(n == 3)
    {
        sw.Write(baseString[line]);
        return;
    }
    else
    {
        if(line < n/2)
        {
            for (int i = 0; i < n / 2; i++)
                sw.Write(' ');
            DrawPart(n / 2, line);
            for (int i = 0; i < n / 2; i++)
                sw.Write(' ');
        }
        else
        {
            DrawPart(n / 2, line - n / 2);
            sw.Write(' ');
            DrawPart(n / 2, line - n / 2);
        }
    }
}

for (int i = 0; i < N; i++)
{
    DrawPart(N, i);
    sw.WriteLine();
}

sr.Close();
sw.Close();
#endif
}
