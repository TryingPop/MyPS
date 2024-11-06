using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 11
이름 : 배성훈
내용 : 연속합
    문제번호 : 1912번

    간단하게 답만 찾는거면, 바로 앞에꺼와 더해서 양수인지 확인하고, 매번 max값 갱신하는게 좋아보인다
*/
namespace BaekJoon._14
{
    internal class _14_05
    {

        static void Main5(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int length = int.Parse(sr.ReadLine());

            int[] nums = Array.ConvertAll(sr.ReadLine().Split(' '), item => int.Parse(item));
            int[] maxs = new int[length];

            for (int i = length - 1; i >= 0; i--)
            {

                maxs[i] = nums[i];

                if (i + 1 < length)
                {

                    if (maxs[i + 1] > 0)
                    {

                        maxs[i] += maxs[i+1];
                    }
                }
            }

            int result = maxs.Max();
            Console.WriteLine(result);
        }
    }
}
