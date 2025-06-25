using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 2
이름 : 배성훈
내용 : 똥게임
    문제번호 : 23815번

    dp, 그리디 문제다.
    사칙연산을 순차적으로 진행한다.
    양수 범위 안에서 연산을 진행하며 가능한 최댓값을 찾아야 한다.
    그래서 현재 최댓값이 되게 진행하는 것이 최대가됨을 보장한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1666
    {

        static void Main1666(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = int.Parse(sr.ReadLine());

            long cur = 1;
            long next = 1;

            for (int i = 0; i < n; i++)
            {

                string[] temp = sr.ReadLine().Split();
                long max1 = int.MinValue;
                long max2 = int.MinValue;

                for (int j = 0; j < 2; j++)
                {

                    max1 = Math.Max(GetNext(temp[j], cur), max1);
                    max2 = Math.Max(GetNext(temp[j], next), max2);
                }

                next = Math.Max(max2, cur);
                cur = max1;

                if (next > 0) continue;

                next = -1;
                break;
            }

            if (next == -1) Console.Write("ddong game");
            else Console.Write(Math.Max(next, cur));

            long GetNext(string _str, long _num)
            {

                if (_num <= 0) return -1;
                int _val = 0;
                for (int i = 1; i < _str.Length; i++)
                {

                    _val = _val * 10 + _str[i] - '0';
                }

                switch (_str[0])
                {

                    case '+':
                        return _num + _val;

                    case '-':
                        return _num - _val;

                    case '*':
                        return _num * _val;

                    default:
                        return _num / _val;
                }
            }
        }
    }
#if other
using System;

public class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int[] dp = new int[2];
        dp[1] = 1;
        for (int i = 0; i < n; i++)
        {
            string[] ab = Console.ReadLine().Split(' ');
            if (dp[0] > 0)
                dp[0] = Math.Max(Calculation(dp[0], ab[0]), Calculation(dp[0], ab[1]));
            dp[0] = Math.Max(dp[0], dp[1]);
            if (dp[1] > 0)
                dp[1] = Math.Max(Calculation(dp[1], ab[0]), Calculation(dp[1], ab[1]));
            if (dp[0] <= 0 && dp[1] <= 0)
            {
                Console.Write("ddong game");
                return;
            }
        }
        Console.Write(Math.Max(dp[0], dp[1]));
    }
    static int Calculation(int v, string op)
    {
        int x = op[1] - '0';
        if (op[0] == '+')
            return v + x;
        if (op[0] == '-')
            return v - x;
        if (op[0] == '*')
            return v * x;
        return v / x;
    }
}
#endif
}
