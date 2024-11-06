using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.14
 * 내용 : 백준 2단계 4번 문제
 * 
 * 사분면 고르기
 * 좌표를 입력받아 몇 사분면에 속하는지 출력하기
 */

namespace BaekJoon._02
{
    internal class _02_04
    {
        static void Main4(string[] args)
        {
            int x = int.Parse(Console.ReadLine());
            int y = int.Parse(Console.ReadLine());

            if (x > 0)
            {
                if (y > 0)
                {
                    Console.WriteLine("1");
                }
                else
                {
                    Console.WriteLine("4");
                }
            }
            else
            {
                if (y > 0)
                {
                    Console.WriteLine("2");
                }
                else
                {
                    Console.WriteLine("3");
                }
            }
        }
    }
}
