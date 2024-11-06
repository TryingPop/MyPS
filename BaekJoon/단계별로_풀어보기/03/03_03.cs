using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.14
 * 내용 : 백준 3단계 3번 문제
 * 
 * 합
 * A를 입력받아 1부터 A까지 정수들의 합 출력하기
 */

namespace BaekJoon._03
{
    internal class _03_03
    {
        static void Main3(string[] args)
        {
            int inputs = int.Parse(Console.ReadLine());
            int result = 0;
            for (int i = 1; i <= inputs; i++)
            {
                result += i;
            }
            Console.WriteLine(result);
        }
    }
}
