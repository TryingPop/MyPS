using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 10
이름 : 배성훈
내용 : 알고리즘 수업 - 피보나치 수 1
    문제번호 : 24416번

    동적 프로그래밍을 통해 피보나치 수 구하기
*/

namespace BaekJoon._14
{
    internal class _14_01
    {

        static void Main1(string[] args)
        {

            int num = int.Parse(Console.ReadLine());

            int f1 = 1, f2 = 1;
            int result = f2;
            int tryNum = 0;

            for (int i = 2; i < num; i++)
            {

                result = f1 + f2;
                f1 = f2;
                f2 = result;    // 결과
                tryNum++;       // 시행횟수
            }

            Console.WriteLine(string.Format("{0} {1}", result, tryNum));
        }
    }
}
