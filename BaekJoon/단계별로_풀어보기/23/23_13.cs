using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 공 바꾸기
    문제번호 : 10813번
*/

namespace BaekJoon._23
{
    internal class _23_13
    {

        static void Main13(string[] args)
        {

            StringBuilder sb = new StringBuilder();

            int[] info = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int[] nums = new int[info[0] + 1];

            for (int i = 1; i <= info[0]; i++)
            {

                nums[i] = i;
            }

            for (int i = 0; i < info[1]; i++)
            {

                int[] input = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

                int temp = nums[input[0]];
                nums[input[0]] = nums[input[1]];
                nums[input[1]] = temp;
            }

            for (int i = 1; i < nums.Length; i++)
            {

                sb.Append($"{nums[i]} ");
            }

            sb.Append('\n');

            Console.WriteLine(sb);
        }
    }
}
