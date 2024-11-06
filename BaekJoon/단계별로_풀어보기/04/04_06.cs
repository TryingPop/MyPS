using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.18
 * 내용 : 백준 4단계 6번 문제
 * 
 * OX퀴즈
 */

namespace BaekJoon._04
{
    internal class _04_06
    {
        static void Main6(string[] args)
        {
            int length = int.Parse(Console.ReadLine());
            int result = 0;
            int num = 0;
            string str;

            for (int i = 0; i < length; i++)
            {
                str = Console.ReadLine();

                result = 0;
                num = 0;

                for (int j = 0; j < str.Length; j++)
                {
                    if (str[j] == 'O')
                    {
                        num++;
                    }
                    else if (str[j] == 'X')
                    {
                        num = 0;
                    }
                    result += num;
                }

                Console.WriteLine(result);
            }
        }
    }
}
