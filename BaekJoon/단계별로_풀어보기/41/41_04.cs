using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 1. 25
이름 : 배성훈
내용 : 박성원
    문제번호 : 1086번

    일단 긴 문자열 나눗셈 방법을 연습하고 싶어 41_07을 풀었다
    해당 방법에 따라 먼저 입력 받은 긴 숫자를 나누었다

    그리고 문제 조건에 맞게 앞 뒤로 조합할 때 규칙이 있나 찾아보고,
    앞에 붙이는거 보다는 뒤에 수를 이어 붙이는게 계산이 수월하다는 규칙 정도 밖에 못찾았다
    예를들어 111324, 4442112551231을 이어 붙인다고 한다면

        111324, 4442112551231뒤에껄 앞에 이어붙일 때는 
            4442112551231_111324
        이고 앞의 길이를 알아야 나머지 연산이 된다

        반면 앞에껄 뒤에 붙일 때는    
            111324_4442112551231
        뒤의 길이를 알아야 나머지 연산이 된다

        한 개를 이어 붙일 때는 아무런 차이가 없으나,
        여러 개를 이어붙인 상황이면 
        앞에 새로운 수를 계속 이어 붙이면 해당 길이들을 더한 값을 보관해야하는 번거로움이 생긴다
    이러한 이유로 뒤에 이어붙인다는 가정으로 했다

    또한 mod연산의 특징으로 (a + b) % c = (a % c + b % c) % c를 이용했다

    dp를 어떻게 잡아야할지 감이 하나도 안왔다
    그래서 다른 사람의 아이디어를 보고,
    dp[j][i] 는 i는 비트마스크이다
        i == 6 ( 1 << 2 | 1 << 1 )이 선택된 경우다
        그리고 뒤에 j는 나머지가 j인 경우의 수이다!
    dp로 이렇게 저장하는 것을 생각하지도 못했다

    이후 dp가 잡히니 대략적인 풀이방법이 보였다
    비트 탐색에서는 그냥 for문을 돌렸다
        0 = 0000000 -> 1 = 00000001 -> 2 = 00000010 -> 3 = 00000011 -> ...

    이렇게 가는데, 이는 다음과 같다
        첫 번째 원소로만 할 수 있는 것을 한다
        첫 번째 원소로만 할 수 있는 것을 다하면, 이제 두 번째 원소를 꺼낸다
        그리고 첫 번째 원소와 두 번째 원소로 할 수 있는 경우를 다한다
        ... 이렇게 n번째까지 확장해가는 방법이다

    그리고 결과값 얻는데서 ZeroDivision 에러로 2번 컴파일 에러 발생했다(틀렸다)
*/

namespace BaekJoon._41
{
    internal class _40_04
    {

        static void Main4(string[] args)
        {

            int len;
            int div;
            // 이어붙일 나머지
            int[] numRemainder;

            // 자리수때문에 곱해줄 값
            int[] numDigit;

            using (StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput())))
            {

                len = int.Parse(sr.ReadLine());

                string[] nums = new string[len];
                for (int i = 0; i < len; i++)
                {

                    nums[i] = sr.ReadLine();
                }

                div = int.Parse(sr.ReadLine());

                numRemainder = new int[len];
                numDigit = new int[len];

                for (int i = 0; i < len; i++)
                {

                    int num = 0;
                    for (int j = 0; j < nums[i].Length; j++)
                    {

                        num *= 10;
                        num += nums[i][j] - '0';

                        if (num >= div) num %= div;
                    }

                    numRemainder[i] = num;
                    numDigit[i] = nums[i].Length;
                }

                // 해당 부분을 그냥 0 ~ 50개 밖에 안되서 50개 경우의 수를 저장해서 푸는게 빨라보인다!
                for (int i = 0; i < len; i++)
                {

                    // 자리수 연산
                    int num = 10;
                    int nlen = 1;
                    while(nlen < numDigit[i])
                    {

                        num *= 10;
                        num %= div;
                        nlen++;
                    }
                    numDigit[i] = num;
                }
            }

            long[][] dp = new long[div][];
            
            for (int i = 0; i < dp.Length; i++)
            {

                dp[i] = new long[1 << len];
            }

            dp[0][0] = 1;
            int end = 1 << len;
            
            // DFS 탐색과 같다!
            for (int visit = 0; visit < end; visit++)
            {

                for (int curNum = 0; curNum < len; curNum++)
                {

                    if ((visit & 1 << curNum) != 0) continue;

                    int next = visit | 1 << curNum;
                    // 뒤로 이어 붙인다
                    for (int curRemainder = 0; curRemainder < div; curRemainder++)
                    {

                        int nextRemainder = ((curRemainder * numDigit[curNum]) + numRemainder[curNum]) % div;
                        dp[nextRemainder][next] += dp[curRemainder][visit];
                    }
                }
            }

            end -= 1;
            long all = 0;
            for (int i = 0; i < div; i++)
            {

                all += dp[i][end];
            }

            long chk = dp[0][end];
            GetResult(ref all, ref chk);
            Console.WriteLine($"{chk}/{all}");
        }

        static void GetResult(ref long _all, ref long _chk)
        {

            if (_chk == 0)
            {

                _all = 1;
                return;
            }

            long big = _all;
            long small = _chk;

            while (true)
            {

                long calc = big % small;
                if (calc == 0) break;
                big = small;
                small = calc;
            }

            _all /= small;
            _chk /= small;
        }
    }
}
