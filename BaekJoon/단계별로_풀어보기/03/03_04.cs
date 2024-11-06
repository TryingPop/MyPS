using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.14
 * 내용 : 백준 3단계 4번 문제
 * 
 * 빠른 A+B
 * 케이스와 두 정수 A, B를 입력받아 각 케이스마다 A + B 출력하기
 */

namespace BaekJoon._03
{
    internal class _03_04
    {
        static void Main4(string[] args)
        {
            int maxLine = int.Parse(Console.ReadLine());
            StringBuilder sb = new StringBuilder();
            int[] a = new int[maxLine];
            int[] b = new int[maxLine];

            for (int i = 0; i < maxLine; i++)
            {
                string[] strinput = Console.ReadLine().Split(' ');
                a[i] = int.Parse(strinput[0]);
                b[i] = int.Parse(strinput[1]);

                sb.Append(a[i] + b[i] + "\n");

            }

            Console.WriteLine(sb.ToString());
        }
    }
}
