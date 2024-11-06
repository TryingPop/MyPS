using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 29
이름 : 배성훈
내용 : 두 배 더하기
    문제번호 : 12931번

    그리디 문제다
    아이디어는 다음과 같다
    덧셈보다 곱셈이 빨리 커지므로 
    최대한 곱셈을 이용하는게 좋다
    
    각 수에 대해 2로 나눠 떨어지면 곱셈을 했다고 생각하고
    홀수는 덧셈을 진행했다고 생각하면 된다
    그러면 곱셈 최대횟수를 찾을 수 있다
    이후는 덧셈 순서를 적절히 조절하면 해당 수식으로 만들 수 있어
    덧셈 카운트와 더해주면 정답이 된다
*/

namespace BaekJoon.etc
{
    internal class etc_1006
    {

        static void Main1006(string[] args)
        {

            StreamReader sr;

            int n;
            int[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int ret = 0, mul = 0;

                for (int i = 0; i < n; i++)
                {

                    int chk = 0;
                    while (arr[i] > 0)
                    {

                        if ((arr[i] & 1) == 0)
                        {

                            chk++;
                            arr[i] >>= 1;
                        }
                        else
                        {

                            ret++;
                            arr[i]--;
                        }
                    }

                    mul = Math.Max(mul, chk);
                }

                ret += mul;

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
using System.Linq;

// #nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var b = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

        // dp[*2 count, num] = +1 count
        var dp = new int?[13, 1001];
        dp[0, 0] = 0;

        for (var doubleCount = 0; doubleCount < 13; doubleCount++)
            for (var num = 0; num <= 1000; num++)
                if (dp[doubleCount, num].HasValue)
                {
                    var mov = dp[doubleCount, num].Value;

                    if (num + 1 <= 1000)
                        dp[doubleCount, num + 1] = Math.Min(dp[doubleCount, num + 1] ?? Int32.MaxValue, mov + 1);
                    if (num != 0 && num * 2 <= 1000)
                        dp[doubleCount + 1, num * 2] = Math.Min(dp[doubleCount + 1, num * 2] ?? Int32.MaxValue, mov);
                }

        var minmove = Int64.MaxValue;

        for (var maxdc = 0; maxdc < 13; maxdc++)
        {
            var move = 0L;

            foreach (var num in b)
            {
                var nummove = Int32.MaxValue;
                for (var dc = 0; dc <= maxdc; dc++)
                    if (dp[dc, num].HasValue)
                        nummove = Math.Min(nummove, dp[dc, num].Value);

                move += nummove;
            }

            minmove = Math.Min(minmove, maxdc + move);
        }

        sw.WriteLine(minmove);
    }
}

#endif
}
