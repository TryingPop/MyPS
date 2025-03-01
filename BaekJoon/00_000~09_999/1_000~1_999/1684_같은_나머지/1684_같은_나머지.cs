using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 11
이름 : 배성훈
내용 : 같은 나머지
    문제번호 : 1684번

    수학 문제다
    a_i = a_j(mod n) 인 n의 최대값을 찾는 문제다

    이는 a_i - a_j = 0 (mod n) 으로 식을 변형하면
    a_i - a_j의 최대 공약수가 n의 최대값이 됨을 알 수 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0185
    {

        static void Main185(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            int[] arr = new int[n];

            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt(sr);
            }

            sr.Close();

            int[] dp = new int[n];

            for (int i = 1; i < n; i++)
            {

                dp[i] = arr[i] - arr[0];
            }

            int ret = dp[1];
            ret = ret < 0 ? -ret : ret;
            for (int i = 2; i < n; i++)
            {

                int next = dp[i] < 0 ? -dp[i] : dp[i];
                ret = GetGCD(ret, next);
            }

            Console.WriteLine(ret);
        }

        static int GetGCD(int _a, int _b)
        {

            if (_a < _b)
            {

                int temp = _a;
                _a = _b;
                _b = temp;
            }

            while(_b > 0)
            {

                int temp = _a % _b;
                _a = _b;
                _b = temp;
            }

            return _a;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            bool plus = true;
            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
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
