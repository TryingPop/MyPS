using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.14
 * 내용 : 백준 1단계 4번 문제
 * 
 * 개
 * 개를 출력하기
 */

namespace BaekJoon._01
{
    internal class _01_04
    {
        static void Main4(string[] args)
        {
            string[] Array = { "|\\_/|", "|q p|   /}", "( 0 )\"\"\"\\", "|\"^\"`    |", "||_/=\\\\__|" };

            foreach (string str in Array)
            {
                Console.WriteLine(str);
            }

        }
    }
}
