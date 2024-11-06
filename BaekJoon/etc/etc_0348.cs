using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 25
이름 : 배성훈
내용 : 행운의 티켓
    문제번호 : 1639번

    구현, 브루트포스 문제다
    sum 배열을 따로둬서 차로 부분합을 구했다
    이중 포문으로 모든 경우를 확인했다
*/

namespace BaekJoon.etc
{
    internal class etc_0348
    {

        static void Main348(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput());

            string str = sr.ReadLine();

            int[] sum = new int[str.Length + 1];

            for (int i = 0; i < str.Length; i++)
            {

                sum[i + 1] = str[i] - '0';
                sum[i + 1] += sum[i];
            }

            int ret = 0;
            
            // 시작지점
            for (int start = 0; start < sum.Length; start++)
            {

                int maxLen = 0;
                // 길이 n
                for (int n = 1; n < sum.Length; n++)
                {

                    // 끝범위가 문자열을 벗어나는 경우 탈출
                    if (start + 2 * n >= sum.Length) break;
                    // 앞부분의 합과 뒷부분의 합이 같은지 확인
                    if (sum[start + n] - sum[start] != sum[start + 2 * n] - sum[start + n]) continue;
                    // 1부터 확인하기에 갱신하면 최대값이 담긴다
                    // 길이이므로 2의 배수이다
                    maxLen = n * 2;
                }

                if (ret < maxLen) ret = maxLen;
            }

            Console.WriteLine(ret);
        }
    }
}
