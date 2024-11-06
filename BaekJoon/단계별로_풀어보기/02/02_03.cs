using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.14
 * 내용 : 백준 2단계 3번 문제
 * 
 * 윤년
 * 년도를 입력받아 윤년인지 판별하기
 */

namespace BaekJoon._02
{
    internal class _02_03
    {
        static void Main3(string[] args)
        {
            int year = int.Parse(Console.ReadLine());
            if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
            {
                Console.WriteLine("1");
            }
            else
            {
                Console.WriteLine("0");
            }
        }
    }
}
