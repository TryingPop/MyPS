using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 14
이름 : 배성훈
내용 : Basalt Breakdown
    문제번호 : 21983번

    수학 문제다.
    정육면체의 넓이가 주어질 때 둘레를 구하는 문제다.
    길이가 같은 정삼각형이 6개를모으면 정육면체가 된다.
    그리고 해당 정삼각형의 한 변의 길이 x 6이 정육면체의 둘레가된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1542
    {

        static void Main1542(string[] args)
        {

            long area = long.Parse(Console.ReadLine());

            double div = 6.0 * Math.Sqrt(3);
            double ret = 12.0 * Math.Sqrt(area / div);

            Console.Write(ret);
        }
    }
}
