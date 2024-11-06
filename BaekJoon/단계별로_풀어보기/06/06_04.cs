using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.19
 * 내용 : 백준 6단계 4번 문제
 * 
 * 문자열 반복
 */

namespace BaekJoon._06
{
    internal class _06_04
    {
        static void Main4(string[] args)
        {
            int length = int.Parse(Console.ReadLine());
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                string[] inputs = Console.ReadLine().Split(" ");
                int repeat = int.Parse(inputs[0]);

                for (int j = 0; j < inputs[1].Length; j++)
                {
                    for (int k = 0; k < repeat; k++)
                    {
                        sb.Append(inputs[1][j].ToString());
                    }
                }
                sb.Append("\n");
            }
            Console.WriteLine(sb);
        }
    }
}
