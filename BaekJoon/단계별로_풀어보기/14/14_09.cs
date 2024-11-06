using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 11
이름 : 배성훈
내용 : 1로 만들기
    문제번호 : 1463번
*/

namespace BaekJoon._14
{
    internal class _14_09
    {

        static void Main9(string[] args)
        {

            int input = int.Parse(Console.ReadLine());
            int[] nums = new int[input];
            for (int i = 2; i <= input; i++)
            {

                nums[i - 1] = nums[i - 2] + 1;

                if (i % 3 == 0)
                {

                    nums[i - 1] = nums[i - 1] < nums[(i / 3) - 1] ? nums[i - 1] : nums[(i / 3) - 1] + 1;
                }

                if (i % 2 == 0) 
                {

                    nums[i - 1] = nums[i - 1] < nums[(i / 2) - 1] ? nums[i - 1] : nums[(i / 2) - 1] + 1;
                }

            }

            Console.WriteLine(nums[input - 1]);
        }
    }
}
