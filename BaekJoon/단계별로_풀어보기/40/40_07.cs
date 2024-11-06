using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 23
이름 : 배성훈
내용 : 두 원
    문제번호 : 7869번

    ...두 원이 겹치는 면적을 구하는 문제이다
    겹치는 부분의 호의 넓이를 구하고 삼각형의 넓이를 빼는 아이디어이다
    여기서 중심 정리는 삼각형의 넓이를 사용하는데 코사인 제 2법칙이 중요하게 쓰인다

    바로 큰 각을 구해서 하는 방법이나 작은 각을 이용해서 구하는 방법이나 둘 다 이용가능하다
    큰각 연산해주는게 속도 면에서는 빠르다

    다만 출력 형태를 맞추지 않아 94%에서 계속 틀렸다.
    소수점 셋째자리까지 표현이라는데, 소수 부분이 존재하면 셋째 자리까지 표현하는 줄 알았다
    그래서 없는 경우 그냥 0만 반환했었고, 이를 0.000으로 수정하니 맞았다;;;
*/

namespace BaekJoon._40
{
    internal class _40_07
    {

        static void Main7(string[] args)
        {
#if First
            double[] circles = Console.ReadLine().Split(' ').Select(double.Parse).ToArray();

            // 반지름이 큰 것을 뒤로 가게 했다
            if (circles[2] > circles[5]) Swap(circles);

            // 코사인 제 2법칙을 이용할꺼라 제곱값도 저장한다!
            double cenDis = GetTwoPosDis(circles[0], circles[1], circles[3], circles[4]);

            if (cenDis > circles[2] + circles[5])
            {

                Console.WriteLine("0.000");
                return;
            }
            
            double area = 0;
            if (cenDis <= circles[5] - circles[2])
            {

                area = circles[2] * circles[2] * Math.PI;

                Console.WriteLine($"{area:0.000}");
                return;
            }

            // 이젠 겹치는 경우다!
            // 먼저 각도 찾기
            double rad1 = GetCosHalfAngle(circles[2], circles[5], cenDis);
            double rad2 = GetCosHalfAngle(circles[5], circles[2], cenDis);

            area = GetArea(circles[2], rad1) + GetArea(circles[5], rad2); 
            
            Console.WriteLine($"{area:0.000}");
#else

            double[] circles = Console.ReadLine().Split(' ').Select(double.Parse).ToArray();

            if (circles[2] > circles[5]) Swap(circles);

            double cenDisPow = GetTwoPosDisPow(circles[0], circles[1], circles[3], circles[4]);
            double cenDis = Math.Sqrt(cenDisPow);

            if (cenDis > circles[2] + circles[5])
            {

                Console.WriteLine("0.000");
                return;
            }

            double area = 0;
            if (cenDis <= circles[5] - circles[2])
            {

                area = (circles[2] * circles[2] * Math.PI);

                Console.WriteLine($"{area:0.000}");
                return;
            }

            double rad1 = GetHalfAngle(circles[2], circles[5], cenDis, cenDisPow);
            double rad2 = GetHalfAngle(circles[5], circles[2], cenDis, cenDisPow);

            area = GetArea(circles[2], rad1) + GetArea(circles[5], rad2);

            Console.WriteLine($"{area:0.000}");
#endif


        }
#if First
        static double GetArea(double _r, double _rad)
        {

            return _r * _r * (_rad - Math.Sin(_rad))* 0.5;
        }

        static void Swap(double[] _arr)
        {

            for (int i = 0; i < 3; i++)
            {

                double temp = _arr[i];
                _arr[i] = _arr[i + 3];
                _arr[i + 3] = temp;
            }
        }

        static double GetTwoPosDis(double _x1, double _y1, double _x2, double _y2)
        {

            double x = _x1 - _x2;
            x *= x;

            double y = _y1 - _y2;
            y *= y;

            return Math.Sqrt(x + y);
        }

        // 코사인 각도 구하기 단위는 라디안!
        static double GetCosHalfAngle(double _getR, double _calcR, double _cenDis)
        {

            double result = _cenDis * _cenDis;
            result += _getR * _getR;
            result -= _calcR * _calcR;

            double calc = 2 * _cenDis * _getR;
            result /= calc;

            return 2 * Math.Acos(result);
        }
#else
        static double GetArea(double _r, double _rad)
        {

            return _r * _r * (_rad - Math.Sin(_rad) * Math.Cos(_rad));
        }

        static void Swap(double[] _arr)
        {

            for (int i = 0; i < 3; i++)
            {

                double temp = _arr[i];
                _arr[i] = _arr[i + 3];
                _arr[i + 3] = temp;
            }
        }

        static double GetTwoPosDisPow(double _x1, double _y1, double _x2, double _y2)
        {

            double x = _x1 - _x2;
            x *= x;

            double y = _y1 - _y2;
            y *= y;

            return x + y;
        }

        static double GetHalfAngle(double _getR, double _calcR, double _cenDis, double _cenDisPow)
        {

            double result = _cenDisPow;
            result += _getR * _getR;
            result -= _calcR * _calcR;

            double calc = 2 * _cenDis * _getR;
            result /= calc;

            return Math.Acos(result);
        }
#endif
    }
}
