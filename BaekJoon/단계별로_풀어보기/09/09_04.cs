using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.21
 * 내용 : 백준 9단계 4번 문제
 * 
 * 별 찍기 - 10
 */

namespace BaekJoon._09
{
    internal class _09_04
    {
        static void Main4(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            int num = int.Parse(Console.ReadLine());

            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    sb.Append(Chk(i, j, num/3));
                    if (j == num - 1)
                    {
                        sb.AppendLine("");
                    }
                }
            }

            Console.WriteLine(sb);
        }
        static string Chk(int num1, int num2,int chk)
        {
            if ((num1 % (3*chk)) / chk == 1 && (num2 % (3*chk)) / chk == 1)
            {
                return " ";
            }

            else
            {
                if (chk <= 1)
                {
                    return "*";
                }
                else
                {
                    return Chk(num1, num2, chk/3);
                }
            }
        }
    }
}
