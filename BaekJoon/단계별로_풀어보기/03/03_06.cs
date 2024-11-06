using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.14
 * 내용 : 백준 3단계 6번 문제
 * 
 * 기찍 N
 * A를 입력받아 A부터 1까지 차례대로 출력하기
 */

namespace BaekJoon._03
{
    internal class _03_06
    {
        static void Main6(string[] args)
        {
            int input = int.Parse(Console.ReadLine());
            StringBuilder sb = new StringBuilder();
            for (int i = input; i >= 1; i--)
            {
                sb.Append($"{i}\n");
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
