using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 23
이름 : 배성훈
내용 : 직각 삼각형
    문제번호 : 3000번

    수학, 기하학, 조합론 문제다
    아이디어는 다음과 같다
    직각삼각형 중 빗변을 제외한 직선은 좌표축과 평행해야 한다는 조건이 있다
    그래서 서로 다른 x축에 평행한 직선 1개와 y축에 평행한 직선의 개수들을 각각 찾아주면 된다
    그러면 둘을 곱한게 직각삼각형의 개수가 된다

    해당 방법으로 x좌표와 y좌표를 입력받으면 x좌표에 해당하는 점들의 개수를 누적하고,
    y좌표에 해당하는 점들의 개수를 누적해주면 된다

    누적한 값은 x0의 개수라 볼 수 있으므로 x0 - 1 개는 y축에 평행한 서로 다른 직선의 개수가 된다
    마찬가지로 y0의 개수는 y0 - 1개는 x축에 평행한 서로 다른 직선의 개수가 된다

    이 둘을 곱하면 정답이 된다
*/

namespace BaekJoon.etc
{
    internal class etc_1072
    {

        static void Main1072(string[] args)
        {

            StreamReader sr;
            (int x, int y)[] pos;
            int n;
            int[] xpos;
            int[] ypos;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                long ret = 0;
                for (int i = 0; i < n; i++)
                {

                    ret += 1L * (xpos[pos[i].x] - 1) * (ypos[pos[i].y] - 1);
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                pos = new (int x, int y)[n];

                xpos = new int[100_001];
                ypos = new int[100_001];

                for (int i = 0; i < n; i++)
                {

                    int x = ReadInt();
                    int y = ReadInt();

                    xpos[x]++;
                    ypos[y]++;

                    pos[i] = (x, y);
                }
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
}
