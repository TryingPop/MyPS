using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 4
이름 : 배성훈
내용 : 두 용액
    문제번호 : 2470번

    조건만 추가했을 뿐 앞과 같이 풀었다
*/

namespace BaekJoon._34
{
    internal class _34_02
    {

        static void Main2(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            int[] nums = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            sr.Close();

            Array.Sort(nums);


            int start = 0;
            int end = len - 1;

            int result1 = start;
            int result2 = end;
            int chk = nums[start] + nums[end];

            // 탐색시작
            // 서로 다른 값이므로 34_01과 같은 탐색을 한다!
            // 만약 다른 경우면 배열을 써서 하면 될거 같다!
            while(start < end)
            {

                // 농도 확인
                int sums = nums[start] + nums[end];
                // 이전 농도 저장 양수로
                int comp = chk > 0 ? chk : -chk;

                if (sums > 0)
                {

                    // 산성이면 여기로
                    // 절대값 비교해서 이전 값이 더 작은 경우면 갱신 X
                    if (comp > sums) 
                    { 
                        
                        // 현재 값이 이전 값보다 더 작으므로 갱신
                        chk = sums;
                        result1 = start;
                        result2 = end;
                    }

                    // 산의 농도가 커서 산성으로 왔으므로 산성 농도를 줄인다
                    end--;
                }
                else if (sums < 0)
                {

                    // 알카리면 여기로
                    // 절대값 비교해서 이전 값이 더 작은 경우면 갱신 X
                    if (comp > -sums)
                    {

                        // 현재 값이 이전 값보다 더 작으므로 갱신
                        chk = sums;
                        result1 = start;
                        result2 = end;
                    }

                    // 알카리 농도가 높아 알카리이므로 알카리 농도를 줄인다
                    start++;
                }
                else
                {

                    // 합계 0 이되면 탐색 더 안하고 강제종료!
                    result1 = start;
                    result2 = end;
                    break;
                }
            }

            Console.WriteLine($"{nums[result1]} {nums[result2]}");
        }
    }
}
