using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.19
 * 내용 : 백준 6단계 3번 문제
 * 
 * 알파벳 찾기
 */

namespace BaekJoon._06
{
    internal class _06_03
    {
        static void Main3(string[] args)
        {
            string input = Console.ReadLine();
            int start = 'a';
            int end = 'z';
            char c = 'a';

            for (int i = start; i <= end; i++)
            {
                c = (char)i;
                Console.WriteLine(input.IndexOf(c));
            }
        }
    }
}
