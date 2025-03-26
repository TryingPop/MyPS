using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 26
이름 : 배성훈
내용 : 삼각형으로 자르기
    문제번호 : 1198번

    기하학 문제다.
    (x1, y1), (x2, y2), (x3, y3)를 꼭짓점으로 하는 삼각형의 넓이는 다음과 같다.
    직선 ax + by + c = 0을 (x2, y2), (x3, y3)를 지나는 직선이라 할 때
    |ax1 + by1 + c|가 된다. 여기서 |A|는 A의 절댓값을 뜻한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1470
    {

        static void Main1470(string[] args)
        {
            //1198
            int n;
            (int x, int y)[] pos;

            Input();

            GetRet();

            void GetRet()
            {

                long max = 0;
                for (int i = 0; i < n; i++)
                {

                    for (int j = i + 1; j < n; j++)
                    {

                        for (int k = j + 1; k < n; k++)
                        {

                            max = Math.Max(max, GetArea(i, j, k));
                        }
                    }
                }

                Console.Write(max >> 1);
                Console.Write('.');
                if ((max & 1L) == 1L) Console.Write(5);
                else Console.Write(0);


                long GetArea(int _idx1, int _idx2, int _idx3)
                {

                    long a = pos[_idx3].y - pos[_idx2].y;
                    long b = pos[_idx2].x - pos[_idx3].x;
                    long c = pos[_idx3].x * pos[_idx2].y - pos[_idx3].y * pos[_idx2].x;

                    return Math.Abs(a * pos[_idx1].x + b * pos[_idx1].y + c);
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput());

                n = int.Parse(sr.ReadLine());

                pos = new (int x, int y)[n];

                for (int i = 0; i < n; i++)
                {

                    string[] temp = sr.ReadLine().Split();
                    pos[i] = (int.Parse(temp[0]), int.Parse(temp[1]));
                }
            }
        }
    }
}
