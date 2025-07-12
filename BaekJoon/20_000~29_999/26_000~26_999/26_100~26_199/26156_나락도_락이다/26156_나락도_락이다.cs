using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 20
이름 : 배성훈
내용 : 나락도 락이다
    문제번호 : 26156번

    로직은 맞겠지 싶어서 제출을 했는데
    이번에도 오버플로우로 한 번 틀렸다;

    그래서, 로직부터 점검해봤고, 로직에 이상이 없을거 같아서 경우의 수를 봤다
    OCK를 만족하는 dp연산에서 수가 1억을 안넘겠지 했는데, 넘을 수 있었다;
    
    실제로, C -> 10만개, K -> 10만개로 이어 붙이면
    10만 * 10만 = 100억이 나왔다

    그래서 나머지 연산을 하니 이상없이 통과했다;

    주된 아이디어는 다음과 같다
    해당 R을 ROCK의 앞으로 하는 경우의 수를 찾기 위해 dp를 설계했다

    그래서 해당 자리수 뒤의 K의 개수를 기록한다
    그리고 C를 뒤에서부터 탐색하는데, C가 발견되면 해당 뒤의 K의 개수만큼 더해준다!
    그러면 해당 C로 만들 수 있는 CK의 수가 된다
    현재 자리에서 맨 끝 문자열까지 CK를 만들 수 잇는 경우의 수가 dp에 담기게 하기 위해서
    해당 C의 값을 계승하는 연산을 한다

    O도 같은 방법으로 O가 발견되면 해당 뒤의 CK의 개수만큼 더해준다
    그리고 O도 뒤의 값을 계승하는 연산을 한다
    그러면 dp에는 해당 자리에서 맨 끝 문자열까지 OCK를 만들 수 있는 경우의 수가 담긴다
    이렇게 OCK까지 찾는다

    이제 정방향으로 R을 찾는다
    n번 인덱스에서 R을 찾을 경우 dp[n + 1]의 값은 해당 R을 ROCK의 맨 앞으로 했을 때 ROCK이 되는 경우의 수가 된다!

    예를들어 RROOCK,
        dp에는
            2 2 2 1 0 0
            의 값이 있다!

    그러면 dp[0]이 의미하는건 0번 인덱스의 R을 맨 앞으로 하면서 ROCK가 되는 경우의 수가 담긴다!
        즉 
            idx         0       1       2       3       4       5
            사용유무    O       X       O       X       O       O
                        O       X       X       O       O       O

            dp[0]은 다음 인덱스를 사용해서 만들어진 ROCK의 경우의 수를 나타낸다

            idx         0       1       2       3       4       5
            사용유무    X       O       O       X       O       O
                        X       O       X       O       O       O

            해당 방법으로도 ROCK를 만들 수 있는데, 이는 dp[1]이 의미하는 것이지 dp[0]이 의미하는게 아니다!
            !!! 부분열로 하기에 두 ROCK은 다르다 !!!


    그렇게 ROCK끝 부분을 찾았다
    또한 문제 조건에서는 앞에 문자열이 있던 없던 상관없다
    그래서 ROCK 의 앞에 문자열의 개수의 2의 지수승만큼 곱해주면 해당 R로 만들 수 있는 경우의 수가 된다

    예를들어
        NARROOCK 이라 가정하자
        
            2번 인덱스의 R을 ROCK의 R로 하는 경우 ROCK는 2개 있다
            그리고 2번 인덱스 R 앞에는 2개의 문자열 NA가 있다 N을 써도되고 안써도 되고
            A를 써도 되고 안써도 된다, 그리고 각각의 경우는 ROCK의 개수만큼 있기에
            2 ^2 * 2 = 8

            그리고 3번 인덱스의 R을 ROCK의 R로 하는 경우 ROCK는 마찬가지로 2개 있다
            그리고 3번 인덱스 R 앞에는 3개의 NAR문자가 있다
            N, A, R을 각각 쓰고 안쓰고 경우가 있고 각각의 경우는 ROCK의 개수만큼 있기에
            2 ^3 * 2 = 16

            그래서 전체 결과는 24개이다

            ///
                NA를 쓰고 2번 R을 빼고 3번 R을 쓰는 경우는 앞에서 나왔냐고 물을 수 있지만 
                부분열이기에 다른 경우로 봐야한다!

                그리고 예제 입력 2의 설명을 보면 다른 경우라고 명시했다!
                    5
                    RROCK
            ///
            
*/

namespace BaekJoon.etc
{
    internal class etc_0067
    {

        static void Main67(string[] args)
        {

            int R = 1_000_000_007;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int len = int.Parse(sr.ReadLine());
            string str = sr.ReadLine();
            sr.Close();
            int[] dp = new int[len];
            int[] calc = new int[len];

            // K의 개수 카운팅
            dp[len - 1] = str[len - 1] == 'K' ? 1 : 0;
            for (int i = len - 2; i >= 0; i--)
            {

                dp[i] = dp[i + 1];
                if (str[i] == 'K')
                {

                    dp[i]++;
                }
            }

            // CK를 만들 수 있는 경우 카운팅
            // calc에 기록한다
            for (int i = len - 2; i >= 0; i--)
            {

                if (str[i] == 'C')
                {

                    calc[i] = dp[i + 1];
                }

                calc[i] += calc[i + 1];
                calc[i] %= R;
            }

            {

                // calc의 배열을 dp로 옮기고
                // calc에 새로운 ? 100만짜리 배열 재할당
                var temp = calc;
                calc = dp;
                dp = temp;

                for (int i = 0; i < len; i++)
                {

                    calc[i] = 0;
                }
            }

            // OCK 찾는 연산
            for (int i = len - 3; i >= 0; i--)
            {

                if (str[i] == 'O')
                {

                    calc[i] = dp[i + 1];
                }

                calc[i] += calc[i + 1];
                calc[i] %= R;
            }

            {

                // 앞과 마찬가지로 재할당?
                var temp = calc;
                calc = dp;
                dp = temp;

                /*
                // 초기화 할 필요 없다
                for (int i = 0; i < len; i++)
                {

                    calc[i] = 0;
                }
                */
            }

            long ret = 0;

            // 왼쪽 문자열에 나올 수 있는 경우의 수
            long l = 1;
            for (int i = 0; i < len - 3; i++)
            {

                // ROCK 찾기
                if (str[i] == 'R')
                {

                    // dp[i + 1] 은 해당 R로 만들 수 있는 ROCK의 경우의 수가 된다!
                    long add = l * dp[i + 1];
                    ret += add;
                    ret %= R;
                }

                l *= 2;
                l %= R;
            }

            Console.WriteLine(ret);
        }
    }
#if other
using System;

public class Program
{
    const int Mod = 1000000007;
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        string s = Console.ReadLine();
        int k = 0, ck = 0, ock = 0, rock = 0;
        int[] mul = new int[n];
        mul[0] = 1;
        for (int i = 1; i < n; i++)
        {
            mul[i] = mul[i - 1] * 2;
            if (mul[i] >= Mod)
                mul[i] -= Mod;
        }
        for (int i = n - 1; i >= 0; i--)
        {
            if (s[i] == 'K')
                k++;
            else if (s[i] == 'C')
                ck = ModAdd(ck, k);
            else if (s[i] == 'O')
                ock = ModAdd(ock, ck);
            else if (s[i] == 'R')
                rock = ModAdd(rock, (int)((long)mul[i] * ock % Mod));
        }
        Console.Write(rock);
    }
    static int ModAdd(int a, int b)
    {
        int c = a + b;
        if (c >= Mod)
            c -= Mod;
        return c;
    }
}
#endif
}
