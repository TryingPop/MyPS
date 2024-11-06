using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 21
이름 : 배성훈
내용 : 대표값 2
    문제번호 : 2587번
*/

namespace BaekJoon._18
{
    internal class _18_02
    {

        static void Main2(string[] args)
        {

            const int size = 5;
            int[] nums = new int[size];
            int ex = 0;

            for (int i = 0; i < size; i++)
            {

                int input = int.Parse(Console.ReadLine());
                nums[i] = input;
                ex += input;
                for (int j = 0 ; j < i; j++)
                {

                    if (nums[j] >= nums[i])
                    {

                        int temp = nums[i];

                        for (int k = j; k < i; k++)
                        {

                            nums[k + 1] = nums[k];
                        }
                        nums[j] = temp;
                        break;
                    }
                }
            }
            ex = ex / size;
            // Console.WriteLine(ex);
            // Console.WriteLine(nums[2]);

            foreach(var s in nums)
            {

                Console.WriteLine(s);
            }
        }
    }
}
