using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 4
이름 : 배성훈
내용 : 두 수의 합
    문제번호 : 3273번

    투 포인터 알고리즘 문제이다
    두 개의 포인터로 문제를 해결해가는 방식이다
    이진 탐색의 left, right도 투 포인터 방식의 하나의 방법이다

    그리고 초기에는 Linq의 OrderBy를 이용해 정렬을 했는데
    다른 사람 풀이를 보고 Array.Sort와 1.5배 차이나서 바꿔보니 Array.Sort가 더 빨랐다
*/

namespace BaekJoon._34
{
    internal class _34_01
    {

        static void Main1(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            int[] nums = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int chk = int.Parse(sr.ReadLine());

            sr.Close();

            Array.Sort(nums);

            // 투포인트 알고리즘
            int result = 0;

            int start = 0;
            int end = len - 1;

            // 같은 경우면 탈출!
            while (start < end)
            {

                int sum = nums[start] + nums[end];

                if (sum == chk)
                {

                    result++;
                    // 서로 다른 수열이므로 둘 다 뺀다!
                    start++;
                    end--;
                }

                else if (sum < chk)
                {

                    start++;
                }
                else
                {

                    end--;
                }
            }

            Console.WriteLine(result);
        }
    }
}
