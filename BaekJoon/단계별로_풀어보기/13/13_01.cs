using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 9
이름 : 배성훈
내용 : 동전 0
    문제번호 : 11047번

    그리디 알고리즘을 이용한 문제
*/

namespace BaekJoon._13
{
    internal class _13_01
    {

        static void Main1(string[] args)
        {

            StreamReader sr = new StreamReader(Console.OpenStandardInput());

            int[] nums = Array.ConvertAll(sr.ReadLine().Split(' '), item => int.Parse(item));

            int length = nums[0];
            int target = nums[1];

            int[] coins = new int[length];

            for (int i = 0; i < length; i++)
            {

                coins[i] = int.Parse(sr.ReadLine());
            }

            int result = 0;
            int calc = 0;

            for (int i = length - 1; i >= 0; i--)
            {

                calc = target;
                while (true)
                {

                    calc -= coins[i];

                    if (calc <= 0)
                    {

                        break;
                    }

                    result++;
                }

                if (calc == 0)
                {

                    target = 0;
                    result++;
                    break;
                }

                target = calc + coins[i];
            }

            if (target != 0)
            {

                result = -1;
            }

            Console.WriteLine(result);
        }
    }
}
