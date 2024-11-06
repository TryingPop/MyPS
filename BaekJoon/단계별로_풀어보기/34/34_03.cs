using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 5
이름 : 배성훈
내용 : 부분합
    문제번호 : 1806번

    문제 조건 잘못 봐서 몇번 틀렸다
    result의 초기값을 10_001로 했는데
    수열이 최대 10만개 기입되니 생기는 문제..
*/

namespace BaekJoon._34
{
    internal class _34_03
    {

        static void Main3(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int[] nums = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            sr.Close();

            // nums 길이의 최대 값보다 큰 값을 넣어줘야한다
            int result = 100_000;

            int start = 0;
            int end = 0;
            int sum = nums[start];

            while (start < info[0])
            {

                if (sum < info[1])
                {

                    if (end < info[0] - 1) end++;
                    else break;

                    sum += nums[end];
                }
                else
                {

                    if (result > end + 1 - start) result = end + 1 - start;
                    sum -= nums[start];
                    start++;
                }
            }

            if (result == 100_000) Console.WriteLine(0);
            else Console.WriteLine(result);
        }
    }
}
