using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 13
이름 : 배성훈
내용 : 조별과제
    문제번호 : 30960번

    그리디, 정렬, 누적합 문제다
    주된 아이디어는 다음과 같다

    먼저 학번 순서로 정렬한다
    정렬한 뒤 인접 한 애들끼리 묶어야 수줍음이 최소가 보장된다
    이 때문에 3명 그룹인 경우 최소값을 가질러면 정렬한 그룹에서 짝수 번째 있는 애를 기준으로
    중앙에 묶어야 최소가 보장된다!

    이제 앞에서와 뒤에서 2명씩 짝을 지으며 누적 합을 계산한다
    앞은 짝수번, 뒤는 홀수번에 담는다

    예를들어 정렬한게 1, 2, 3, 6, 8라 하자
    그러면 
        dp[0] 에는 1, 2가 짝을 지은 1
        dp[3] 에는 6, 8가 짝을 지은 2
        
        dp[2] 에는 1, 2가 짝을 지은 1과 3, 6가 짝을 지은 3의 합 4가 담긴다
        dp[1] 에는 6, 8가 짝을 지은 2과 2, 3이 짝을 지은 1의 합 3가 담긴다

    이제 dp[0] + dp[1] = 1 + 3 = 4 합쳐보면
        6, 8이 짝이고, 1, 2, 3이 짝인 경우다

    dp[2] + dp[3] = 4 + 2 = 6은 1, 2 가 짝인 경우 3, 6, 8이 짝인 값이 담긴다
    4, 6 중에 최소값이 우리가 찾는 최소 수줍음이다

    dp[1] + dp[2] = 7는 성립할 수 없다 
        1, 2가 짝, 3, 6이 짝 + 6, 8이짝, 2, 3이짝
        그래서 1, 2, 3, 6, 8 모두 짝인 현재 문제에 맞지 않는 상황이된다

    아래는 해당 아이디어를 코드로 옮겼을 뿐이다
*/

namespace BaekJoon.etc
{
    internal class etc_0218
    {

        static void Main218(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);

            int[] arr = new int[len];
            for (int i = 0; i < len; i++)
            {

                arr[i] = ReadInt(sr);
            }

            sr.Close();

            Array.Sort(arr);

            int[] dp = new int[len - 1];
            for (int i = 1; i < len; i++)
            {

                arr[i - 1] = arr[i] - arr[i - 1];
            }

            dp[0] = arr[0];
            for (int i = 2; i < len - 1; i += 2)
            {

                dp[i] = dp[i - 2] + arr[i];
            }

            dp[len - 2] = arr[len - 2];
            for (int i = len - 4; i >= 0; i -= 2)
            {

                dp[i] = dp[i + 2] + arr[i];
            }

            int ret = 1_000_000_000;
            for (int i = 1; i < len - 1; i += 2)
            {

                int calc = dp[i - 1] + dp[i];
                if (calc < ret) ret = calc;
            }

            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read())!= -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
