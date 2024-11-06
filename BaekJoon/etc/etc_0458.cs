using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 5
이름 : 배성훈
내용 : 숫자카드
    문제번호 : 2591번

    dp문제다
    잘못 입력되는 경우는 카드의 숫자를 차례대로 적어 놓은 것이라는 문장이 막아준다!
    (질문 게시판 보니 이전에는 0, 50 이런 잘못입력되는 경우가 있었나보다, 막아줘서 감사합니다!)

    아이디어는 다음과 같다
    1234이나 123123을 해보니 어느 경우든 2장씩 카운트 가능한 경우의 수를 보면 피보나치 수열을 따름을 알 수 있다
    그리고 카드들은 어느 경우든 2장씩 카운트 가능한 경우의 수들을 모아놓은것으로 볼 수 있다
    이어 붙인 경우에 경우의 수는 이어 붙인 것을 곱한게 된다

    여기서는 문제 조건에서 막았지만, 입력되는 수가 40자리의 임의의 수라면 
    시작이 0인 경우나 중간에 2개가 40, 50, 60, 70, 80, 90, 00 부분만 따로 처리해주면 된다
    반례처리를 안하니 60ms에 빠르게 통과된다
*/

namespace BaekJoon.etc
{
    internal class etc_0458
    {

        static void Main458(string[] args)
        {

            string str = Console.ReadLine();

            long[] dp = new long[41];
            dp[0] = 1;
            dp[1] = 1;
            for (int i = 2; i < dp.Length; i++)
            {

                dp[i] = dp[i - 1] + dp[i - 2];
            }

            long ret = 1;
            int before = 0;
            int conn = 0;
            for (int i = 0; i < str.Length; i++)
            {

                int cur = str[i] - '0';
                int chk = before * 10 + cur;
                if (cur == 0)
                {

                    ret *= dp[conn - 1];
                    conn = 0;
                }
                else if (chk > 34)
                {

                    ret *= dp[conn];
                    conn = 1;
                }
                else conn++;

                before = cur;
            }

            ret *= dp[conn];

            Console.WriteLine(ret);
        }
    }

#if other
using System;
using System.Text.RegularExpressions;

// CapitalLetters for class names and methods, camelCase for variable names.
// Write your code clearly enough so that it doesn't need to be commented, or at least, so that it rarely needs to be commented.

namespace Testpad
{
    public class BaekJoon2591
    {
        // 비밀번호 문제랑 비슷한듯
        static void Main()  
        {
            string input = Console.ReadLine();
            char[] inputs = input.ToCharArray(); // 주어지는 수열

            long[] dp = new long[inputs.Length]; // 특정 지점에서 몇가지 경우의 수가 있는지 나타내는 dp
            long[] dp_Pass = new long[inputs.Length]; // 이번에 건너뛰는 경우의 dp

            Regex regex = new Regex(@"[4-9]0");
            // 뭔짓을 해도 안될 경우는 걸러냄
            if (inputs[0] == '0')
            {
                Console.Write(0);
                return;
            }
            else if (input.Contains("00") == true)
            {
                Console.Write(0);
                return;
            }
            else if (regex.IsMatch(input) == true)
            {
                Console.Write(0);
                return;
            }

            // 수가 1자리인 경우의 예외처리
            if (inputs.Length == 1)
            {
                Console.Write(1);
                return;
            }

            // dp를 채워 넣음
            if (inputs[1] == '0' && inputs[0] <= '3')
            {
                dp_Pass[0] = 1;
            }
            else if ((inputs[0] == '3' && inputs[1] <= '4') || inputs[0] <= '2')
            {
                dp[0] = 1;

                if (inputs.Length > 2)
                {
                    if (inputs[2] != '0')
                    {
                        dp_Pass[0] = 1;
                    }
                }
                else
                {
                    dp_Pass[0] = 1;
                }
            }
            else
            {
                if (inputs.Length >= 2)
                {
                    if (inputs[1] == '0')
                    {
                        Console.Write(0);
                        return;
                    }

                    dp[0] = 1;
                }
            }

            for (int i = 1; i < inputs.Length; i++)
            {
                if (i != inputs.Length - 1)
                {
                    if (inputs[i + 1] == '0')
                    {
                        dp_Pass[i] = dp[i - 1];
                    }
                    else if ((inputs[i] == '3' && inputs[i + 1] <= '4') || inputs[i] <= '2')
                    {
                        dp[i] = dp[i - 1];

                        if (i + 2 < inputs.Length)
                        {
                            if (inputs[i + 2] != '0')
                            {
                                dp_Pass[i] = dp[i - 1];
                            }
                        }
                        else if (i == inputs.Length - 2)
                        {
                            dp_Pass[i] = dp[i - 1];
                        }
                    }
                    else
                    {
                        dp[i] = dp[i - 1];
                    }
                }
                else
                {
                    dp[i] = dp[i - 1];
                }

                dp[i] += dp_Pass[i - 1];
            }

            // 디버그용
            /*for (int i = 0; i < dp.Length; i++)
            {
                Console.WriteLine($"dp[{i}] = {dp[i]}");
                Console.WriteLine($"dp_Pass[{i}] = {dp_Pass[i]}");
            }*/

            // 결과 출력
            Console.Write(dp[inputs.Length - 1]);
        }
    }
}
#endif
}
