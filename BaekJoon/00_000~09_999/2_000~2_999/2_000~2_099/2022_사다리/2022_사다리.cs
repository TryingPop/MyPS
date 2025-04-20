using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 30
이름 : 배성훈
내용 : 사다리
    문제번호 : 2022번

    수학, 기하학, 이분탐색, 피타고라스 정리다
    찾는 밑 변을 mid로 놓는다
    mid는 피타고라스 정리로 x, y보다 작은 값임을 알 수 있다 것이다
    그래서 끝을 x, y중 작은 값으로 놓고
    시작을 0으로 놓았다

    이제 이분 탐색으로 mid 값을 설정한다
    그리고 해당 mid에서의 c_mid의 길이를 찾는다

    h1은 sqrt(x^2 - mid^2), h2는 sqrt(y^2 - mid^2) 이다
    c의 길이는 (h1 * h2) / (h1 + h2)식을 얻을 수 있다
    이는 비례식으로 얻을 수 있다

    그리고 mid가 길면, h1, h2가 짧고, c_mid의 길이도 짧아진다
    그래서 찾은 c_mid 가 c보다 작으면 mid의 길이를 줄여야 한다
    반대로 c_mid의 길이가 c보다 큰 경우 mid의 길이를 늘려야한다

    이렇게 c와 일치하는 mid 값을 이분탐색으로 찾아가면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0257
    {

        static void Main257(string[] args)
        {

            double e = 0.0001;
            double x, y, c;
            Solve();
            void Solve()
            {

                Init();

                GetRet();
            }

            void GetRet()
            {

                double ret = 0.0;

                double low = 0.0;
                double high = Math.Min(x, y);

                while(low + e <= high)
                {

                    double mid = (low + high) / 2;

                    double h1 = Math.Sqrt(x * x - mid * mid);
                    double h2 = Math.Sqrt(y * y - mid * mid);

                    double chk = (h1 * h2) / (h1 + h2);

                    if (chk < c) high = mid;
                    else
                    {

                        ret = mid;
                        low = mid;
                    }
                }

                Console.Write($"{ret:0.000}");
            }

            void Init()
            {

                string[] temp = Console.ReadLine().Split();
                x = double.Parse(temp[0]);
                y = double.Parse(temp[1]);
                c = double.Parse(temp[2]);
            }
        }
    }

#if other
using System;

namespace AlgorithmStudy
{
    internal class boj2022
    {
        public static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(' ');
            double x = double.Parse(input[0]), y = double.Parse(input[1]), c = double.Parse(input[2]);
            Console.WriteLine("{0:F3}", BinarySearch(0, x > y ? y : x));

            double BinarySearch(double min, double max)
            {
                double mid = (min + max) / 2;
                double h_x = Math.Sqrt(x * x - mid * mid);
                double h_y = Math.Sqrt(y * y - mid * mid);
                double cTemp = Math.Round((((h_x * h_y) / (h_x + h_y)) * 1000000)) / 1000000;
                if (cTemp == c)
                    return Math.Round(mid * 1000) / 1000;
                else if (cTemp > c)
                    return BinarySearch(mid, max);
                else
                    return BinarySearch(min, mid);
            }
        }
    }
}
#endif
}
