using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.14
 * 내용 : 백준 1단계 11번 문제
 * 
 * 1998년생인 내가 태국에서는 2541년생?!
 * 불기연도를 입력받아 서기연도로 출력하기
 */

namespace BaekJoon._01
{
    internal class _01_11
    {
        static void Main11(string[] args)
        {
            int plus = 1998 - 2541;
            int year = int.Parse(Console.ReadLine());
            Console.WriteLine(year + plus);
        }
    }
}
