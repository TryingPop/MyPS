using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 2
이름 : 배성훈
내용 : 피타고라스 정리의 증명
    문제번호 : 33466번

    수학 문제다.
    우선 조건을 풀면 2 * (a / b + b / a)가 정수인지 확인해야 한다.
    정수가 되는 때를 보면 a = b일 때는 자명하게 정수이다.
    이후에 a = 2b, 2a = b인 경우도 정수가 됨을 알 수 있다.

    이를 카운팅하면 n이 홀수이면 2 n - 1이고, n이 짝수이면 2n개임을 알 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1667
    {

        static void Main1667(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t = int.Parse(sr.ReadLine());

            while (t-- > 0)
            {

                long cur = long.Parse(sr.ReadLine());
                if ((cur & 1L) == 1) sw.Write(cur * 2 - 1);
                else sw.Write(cur * 2);

                sw.Write('\n');
            }
        }
    }
}
