using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.14
 * 내용 : 백준 3단계 1번 문제
 * 
 * 구구단
 * A를 입력받아 A단 출력하기
 */

namespace BaekJoon._03
{
    internal class _03_01
    {
        static void Main1(string[] args)
        {
            int inputs = int.Parse(Console.ReadLine());

            for (int i = 1; i < 10; i++)
            {
                Console.WriteLine("{0} * {1} = {2}", inputs, i, inputs * i);
            }

        }
    }
}
