using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 4
이름 : 배성훈
내용 : 수익
    문제번호 : 4097번

    dp문제다
    그리디하게 해결했다

    아이디어는 다음과 같다
    앞에서부터 누적해간다
    이전 최대값보다 크다면 해당 값을 최대값으로 설정한다
    그리고 누적합이 음수가 되면 해당 이전 값들을 모두 버린다(0으로 초기화)
    이렇게 진행해가면 최대 이익값을 찾을 수 있다

    다만 모두 음수인 경우를 고려해서 처음 최대값은 들어올 수 있는 가장 작은값보다 작아야한다
    이렇게 제출하니 128ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0448
    {

        static void Main448(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            while (true)
            {

                int n = ReadInt();
                if (n == 0) break;

                int ret = -10_000;
                int sum = 0;
                for (int i = 0; i < n; i++)
                {

                    
                    sum += ReadInt();

                    if (ret < sum) ret = sum;
                    if (sum < 0) sum = 0;
                }

                sw.WriteLine(ret);
            }

            sr.Close();
            sw.Close();

            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if (c == '-')
                    {

                        plus = false;
                        continue;
                    }

                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }
#if other
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
	static void Main(string[] args)
    {
		while (true)
        {
			int n = ReadLine();
			if (n == 0)
				break;

			int[] arr = new int[n];
			for (int i = 0; i < n; ++i)
				arr[i] = ReadLine();

			int[] dp = new int[n];
			dp[0] = arr[0];

			for (int i = 1; i < n; ++i)
			{
				dp[i] = Math.Max(dp[i - 1] + arr[i], arr[i]);
			}

			Console.WriteLine(dp.Max());
		}
	}
    
    static int ReadLine()
    {
		return Convert.ToInt32(Console.ReadLine());
	}
}


#endif
}
