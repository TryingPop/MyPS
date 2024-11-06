using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 10. 21
이름 : 배성훈
내용 : 수열
    문제번호 : 2559번
*/

namespace BaekJoon._26
{
    internal class _26_02
    {

        static void Main2(string[] args)
        {

            int[] info = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int[] nums = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();


            // 첫 항 == 1항부터 n항까지의 합
            int calc = 0;
            for (int i = 0; i < info[1]; i++)
            {

                calc += nums[i];
            }

            // 최대 값
            int max = calc;
            
            
            for (int i = info[1]; i < nums.Length; i++)
            {

                // n항 변환
                calc += nums[i] - nums[i - info[1]];
                // 최대값인지 확인
                if (max < calc) max = calc;
            }

            // 최대값 출력
            Console.WriteLine(max);
        }
    }
}
