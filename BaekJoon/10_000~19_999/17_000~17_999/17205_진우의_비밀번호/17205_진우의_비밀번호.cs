using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 9
이름 : 배성훈
내용 : 진우의 비밀번호
    문제번호 : 17205번

    수학 문제다
    주어진 예제로 변화하는 규칙을 찾아 코드로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0171
    {

        static void Main171(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            string str = Console.ReadLine();

            // 자리수 변할 때 들어가는 경우의수
            long[] up = new long[n];
            up[0] = 1;

            for (int i = 1; i < n; i++)
            {

                up[i] = up[i - 1] * 26;
            }

            long ret = 0;
            for (int i = 0; i < str.Length; i++)
            {

                // 처음 a + 1
                ret++;
                long sum = 0;
                // 0 ~ n - i 까지 합이 i번째 자리값 변화하는데 들어가는 시간이된다
                for (int j = 0; j < n - i; j++)
                {

                    sum += up[j];
                }

                ret += (str[i] - 'a') * sum;
            }

            Console.WriteLine(ret);
        }
    }
}
