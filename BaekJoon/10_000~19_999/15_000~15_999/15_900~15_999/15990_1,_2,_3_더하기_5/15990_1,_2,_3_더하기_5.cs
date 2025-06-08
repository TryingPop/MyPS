using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 31
이름 : 배성훈
내용 : 1, 2, 3 더하기 5
    문제번호 : 15990번

    dp문제다
    아이디어는 다음과 같다
    2개 이상 연속해서 나올 수 없기에
    맨 앞에 숫자를 이어붙이는데, dp를 이전에 앞에 이어붙인 수와 현재 숫자를 인덱스로하고
    그리고 해당 경우의 수를 값으로 했다

    그리고 결과에서 찾을 때, 1, 2, 3의 경우를 합쳐 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0409
    {

        static void Main409(string[] args)
        {

            int MOD = 1_000_000_009;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int[,] dp = new int[3, 100_001];

            dp[0, 1] = 1;
            dp[1, 2] = 1;
            dp[2, 3] = 1;
            dp[0, 3] = 1;
            dp[1, 3] = 1;

            for (int i = 4; i < 100_001; i++)
            {

                dp[0, i] = (dp[1, i - 1] + dp[2, i - 1]) % MOD;
                dp[1, i] = (dp[0, i - 2] + dp[2, i - 2]) % MOD;
                dp[2, i] = (dp[0, i - 3] + dp[1, i - 3]) % MOD;
            }

            int test = ReadInt();

            while(test-- > 0)
            {

                int find = ReadInt();

                int ret = dp[0, find];
                ret = (ret + dp[1, find]) % MOD;
                ret = (ret + dp[2, find]) % MOD;

                sw.WriteLine(ret);
            }

            sr.Close();
            sw.Close();

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
using System;
using System.IO;
class HelloWorld {
  const int range = 1_000_000_009;
  static int[,] arr = new int[100_000 + 1, 3];
  static void Main() {
      StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
      int T = int.Parse(Console.ReadLine());
      arr[1, 0] = 1;
      arr[2, 1] = 1;
      for(int i = 0; i < 3; i++){
          arr[3, i] = 1;
      }
      
      for(int i = 4; i <= 100_000; i++){
          arr[i, 0] = (arr[i - 1, 1] + arr[i - 1, 2]) % range;
          arr[i, 1] = (arr[i - 2, 0] + arr[i - 2, 2]) % range; 
          arr[i, 2] = (arr[i - 3, 0] + arr[i - 3, 1]) % range;
      }
      
      for(int i = 0; i < T; i++){
          int n = int.Parse(Console.ReadLine());
          sw.WriteLine(((arr[n, 0] + arr[n, 1]) % range + arr[n, 2]) % range);
      }
      sw.Close();
  }
}
#endif
}
