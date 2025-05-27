using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 28
이름 : 배성훈
내용 : 조금 똑똑한 뢰벗과 조금 잘생긴 사냐
    문제번호 : 14856번

    수학, 그리디, 브루트포스 알고리즘 문제다
    풀고나서 힌트를 보니 힌트대로 풀었다

    아이디어는 다음과 같다
    먼저 10^18을 넘어가는 피보나치 수열의 길이를 찾았다 (88에서 처음으로 초과)
    피보나치 수열의 특징상 큰거부터 빼줘야한다
    그렇지 않으면 fibo[n] = fibo[n - 1] + fibo[n - 2]로 인접한 경우가 무조건 등장한다!
    그래서 그리디하게 큰거부터 빼줘야하고 해당 수가 최장길이임이 보장된다

    그리고 처음에 없는 경우 처리 안했는데도 통과했다
    작은 수에서 돌려본 결과 모두 표현 가능했다
    아마도 전부 표현 가능해 보인다
*/

namespace BaekJoon.etc
{
    internal class etc_0368
    {

        static void Main368(string[] args)
        {

            long n = long.Parse(Console.ReadLine());

            long[] fibo = new long[88];
            fibo[0] = 1;
            fibo[1] = 1;

            for (int i = 2; i < fibo.Length; i++)
            {

                fibo[i] = fibo[i - 1] + fibo[i - 2];
            }

            int len = 0;
            int[] arr = new int[88];
            for (int i = fibo.Length - 1; i > 0; i--)
            {

                // 그리디 하게 접근해야한다
                // 제일 큰거 부터 빼는게 아닌 중간부터 빼면
                // 피보나치 특징으로 무조건 인접항이 생기게 된다
                if (fibo[i] > n) continue;
                arr[len] = i;
                n -= fibo[i];
                len++;
                i--;
            }

            if (n > 0) len = -1;

            Console.WriteLine(len);
            for (int i = len - 1; i >= 0; i--)
            {

                Console.Write($"{fibo[arr[i]]} ");
            }
        }
    }
}
