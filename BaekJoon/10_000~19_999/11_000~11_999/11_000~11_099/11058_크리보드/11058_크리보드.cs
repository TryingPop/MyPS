using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 16
이름 : 배성훈
내용 : 크리보드
    문제번호 : 11058번

    동적 계획법 문제다
    자료 구조를 int로 해서 한번 틀렸다
    예시를 들어보니 최대값 입력 시 1조단위까지 간다;
    
    문제 아이디어는 브루트 포스로 접근했다
    우선 A키 연타를 해서 dp를 채운다
    그리고 2번부터, ctrl + a와 ctrl + c를 누르고
    ctrl + v를 연타한다 그리고 기존 값과 비교해서 dp를 갱신한다
    1의 경우는 a를 누르는것과 같기에 제외했다

    해당 아이디어로 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0242
    {

        static void Main242(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            long[] dp = new long[n + 1];
            for (int i = 1; i <= n; i++)
            {

                dp[i] = i;
            }

            for (int i = 2; i <= n; i++)
            {

                long cur = dp[i];
                for (int j = i + 3; j <= n; j++)
                {

                    long chk = cur * (j - i - 1);
                    if (chk > dp[j]) dp[j] = chk;
                }
            }

            Console.WriteLine(dp[n]);
        }
    }
}
