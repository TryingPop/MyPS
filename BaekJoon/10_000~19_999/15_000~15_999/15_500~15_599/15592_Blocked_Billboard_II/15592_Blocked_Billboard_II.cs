using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 11
이름 : 배성훈
내용 : Blocked Billboard II
    문제번호 : 15592번

    구현, 기하학, 많은 조건 분기 문제다.
    브루트포스로 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1175
    {

        static void Main1175(string[] args)
        {

#if first
            int[][] area;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }
            void GetRet()
            {

                int minX = 3_000;
                int minY = 3_000;
                int maxX = -1;
                int maxY = -1;

                for (int i = 0; i < area.Length; i++)
                {

                    for (int j = 0; j < area[i].Length; j++)
                    {

                        if (area[i][j] == 0) continue;
                        minX = Math.Min(minX, i);
                        minY = Math.Min(minY, j);

                        maxX = Math.Max(maxX, i);
                        maxY = Math.Max(maxY, j);
                    }
                }

                if (maxX != -1) Console.Write((maxX - minX + 1) * (maxY - minY + 1));
                else Console.Write(0);
            }

            void Input()
            {

                area = new int[2_001][];
                for (int i = 0; i < area.Length; i++)
                {

                    area[i] = new int[2_001];
                }

                SetColor(1);
                SetColor(0);

                void SetColor(int _color)
                {

                    string[] temp = Console.ReadLine().Split();
                    int x1 = int.Parse(temp[0]) + 1_000;
                    int y1 = int.Parse(temp[1]) + 1_000;
                    int x2 = int.Parse(temp[2]) + 1_000;
                    int y2 = int.Parse(temp[3]) + 1_000;

                    for (int i = x1; i < x2; i++)
                    {

                        for (int j = y1; j < y2; j++)
                        {

                            area[i][j] = _color;
                        }
                    }
                }
            }
#else

            int x1, x2, x3, x4, y1, y2, y3, y4;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                x1 = int.Parse(temp[0]);
                y1 = int.Parse(temp[1]);
                x2 = int.Parse(temp[2]);
                y2 = int.Parse(temp[3]);

                temp = Console.ReadLine().Split();
                x3 = int.Parse(temp[0]);
                y3 = int.Parse(temp[1]);
                x4 = int.Parse(temp[2]);
                y4 = int.Parse(temp[3]);
            }

            void GetRet()
            {

                if (x3 < x1 && x2 < x4)
                {

                    if (y1 < y4 && y1 > y3) y1 = y4;
                    if (y2 < y4 && y2 > y3) y2 = y3;
                }

                if (y3 < y1 && y2 < y4)
                {

                    if (x1 < x4 && x1 > x3) x1 = x4;
                    if (x2 < x4 && x2 > x3) x2 = x3;
                }

                Console.Write((x2 - x1) * (y2 - y1));
            }
#endif
        }
    }

#if other
// #include <stdio.h>

int main()
{
	int x1, x2, x3, x4;
	int y1, y2, y3, y4, ans;

	scanf("%d %d %d %d", &x1, &y1, &x2, &y2);
	scanf("%d %d %d %d", &x3, &y3, &x4, &y4);

	if (x3 < x1 && x2 < x4) {
		if (y1 < y4 && y1 > y3) {
			y1 = y4;
		}
		if (y2 < y4 && y2 > y3) {
			y2 = y3;
		}
	}
	if (y3 < y1 && y2 < y4) {
		if (x1 < x4 && x1 > x3) {
			x1 = x4;
		}
		if (x2 < x4 && x2 > x3) {
			x2 = x3;
		}
	}

	ans = (x2-x1)*(y2-y1);

	printf("%d", ans);

	return 0;
}

#endif
}
