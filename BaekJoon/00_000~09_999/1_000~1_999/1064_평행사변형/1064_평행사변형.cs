using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

/*
날짜 : 2024. 12. 10
이름 : 배성훈
내용 : 평행사변형
    문제번호 : 1064번

    수학 문제다.
    세 점이 직선에 있으면 평행사변형을 만들 수 없다.
    한 직선에 있지 않은 세 점으로 만들 수 있는 평행 사변형을 구하면 총 3개 있다.
    그리고 평행사변형들의 길이는 각 점을 A, B, C라 하면
    2 x (AB + BC), 2 x (BC + CA), 2 x (CA + AB)이다.
    여기서 AB는 A, B를 끝점으로 하는 선분의 길이다.
    선분의 길이는 피타고라스 정리를 이용했다.
    이를 수식으로 풀어 구했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1172
    {

        static void Main1172(string[] args)
        {

            (int x, int y)[] pos;

            Solve();
            void Solve()
            {

                Input();

                Console.Write($"{GetRet():0.0########}");
            }

            double GetRet()
            {

                if (Impo()) return -1.0;

                double dis1 = GetDis(0, 1);
                double dis2 = GetDis(1, 2);
                double dis3 = GetDis(2, 0);

                double sum = dis1 + dis2 + dis3;
                double min = Math.Min(Math.Min(dis1, dis2), dis3);
                double max = Math.Max(Math.Max(dis1, dis2), dis3);

                return 2 * (max - min);

                double GetDis(int _i, int _j)
                {

                    int x = pos[_i].x - pos[_j].x;
                    int y = pos[_i].y - pos[_j].y;

                    x *= x;
                    y *= y;

                    return Math.Sqrt(x + y);
                }

                bool Impo()
                {

                    int x1 = pos[0].x - pos[1].x;
                    int y1 = pos[0].y - pos[1].y;

                    int x2 = pos[0].x - pos[2].x;
                    int y2 = pos[0].y - pos[2].y;

                    return x1 * y2 == x2 * y1;
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                pos = new (int x, int y)[3];

                pos[0] = (int.Parse(temp[0]), int.Parse(temp[1]));
                pos[1] = (int.Parse(temp[2]), int.Parse(temp[3]));
                pos[2] = (int.Parse(temp[4]), int.Parse(temp[5]));
            }
        }
    }
}
