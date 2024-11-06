using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 12
이름 : 배성훈
내용 : 가장 긴 바이토닉 부분 수열
    문제번호 : 11054번
*/

namespace BaekJoon._14
{
    internal class _14_13
    {

        static void Main13(string[] args)
        {

            int length = int.Parse(Console.ReadLine());

            int[] incLen = new int[length];             // 앞의 증가수열 문제에서 first 방법에 있는 배열 이용
            int[] decLen = new int[length];             // first 방법을 변형해 감소 부분수열 적용
                                                        // 인덱스 i번부터 i번째 항목을 포함하는 감소 부분수열의 길이를 모아놓은 배열
            int[] nums = Array.ConvertAll(Console.ReadLine().Split(' '), item => int.Parse(item));

            for (int i = 0; i < length; i++)
            {

                int ilen = 0;       // 증가 수열용
                int dlen = 0;       // 감소 수열용

                for (int j = 0; j < i; j++)
                {

                    // 앞의 first 로직과 같다
                    // 증가 부분
                    if (nums[j] < nums[i])
                    {

                        if (ilen < incLen[j])
                        {

                            ilen = incLen[j];
                        }
                    }

                    // 감소 부분
                    if (nums[length - 1 - j] < nums[length - 1 - i])
                    {

                        if (dlen < decLen[length - 1 - j])
                        {

                            dlen = decLen[length - 1 - j];
                        }
                    }
                }

                incLen[i] = ilen + 1;
                decLen[length - 1 - i] = dlen + 1;
            }

            int result = 0;         // 출력할 결과
            for (int i = 0; i < length; i++)
            {

                if (result < incLen[i] + decLen[i])         // incLen[i] + decLen[i]에서 뒤에 - 1을 넣어도 되고 안넣어도 된다
                                                            // 논리상 result = incLen[i] + decLen[i] - 1인 경우 같은 값으로 갱신되기 때문
                                                            // 다만 같은 값이 아주 많은 경우면 넣어 주는게 좋다
                {

                    result = incLen[i] + decLen[i] - 1;     // -1이 없는 경우 result에 nums[i]가 두 번 포함되어져 있다
                                                            // nums = { 1, 2, 1 } 이라 하면
                                                            // incLen = 2, { 1, '2' }의 개수
                                                            // decLen = 2, { '2', 1 }의 개수
                                                            // 기준이 되는 수가 2번 카운팅 되어서 1을 빼준다
                }
            }

            Console.WriteLine(result);
        }
    }
}
