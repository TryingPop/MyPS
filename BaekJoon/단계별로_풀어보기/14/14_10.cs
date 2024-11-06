using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 11
이름 : 배성훈
내용 : 쉬운 계단 수
    문제번호  : 10844번
*/

namespace BaekJoon._14
{
    internal class _14_10
    {

        static void Main10(string[] args)
        {

            int length = int.Parse(Console.ReadLine());
            int[] nums = new int[10] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
            for (int i = 1; i < length; i++)
            {

                int[] calc = nums.Clone() as int[];

                for (int j = 0; j < nums.Length; j++)
                {

                    if (j == 0)
                    {

                        nums[j] = calc[j + 1] % (1000000000);
                    }
                    else if (j == 9)
                    {

                        nums[j] = calc[j - 1] % (1000000000);
                    }
                    else
                    {

                        nums[j] = (calc[j - 1] + calc[j + 1]) % (1000000000);
                    }
                }
            }

            int result = 0;
            for (int i = 1; i < nums.Length; i++)
            {

                result += nums[i];
                result %= (1000000000);
            }

            Console.WriteLine(result);
        }
    }
}
