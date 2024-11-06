using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.21
 * 내용 : 백준 10단계 2번 문제
 * 
 * 분해합
 */

namespace BaekJoon._10
{
    internal class _10_02
    {
        static void Main2(string[] args)
        {
            int num = int.Parse(Console.ReadLine());

            int result = FindGenerator(num);

            if (result == num)
            {
                Console.WriteLine(0);
            }
            else
            {
                Console.WriteLine(result);
            }
        }

        static int FindGenerator(int num)
        {
            int len = num.ToString().Length;
            for (int i = Math.Max(num - len * 9, 1); i <= num - 1; i++)
            {
                int chk = num;
                chk -= i;
                if (chk < 0)
                {
                    continue;
                }
                for (int j = 0; j < i.ToString().Length; j++)
                {
                    chk -= (int)(i.ToString()[j] - '0');
                }
                if (chk == 0)
                {
                    return i;
                }
            }
            return num;
        }
    }
}
