using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 27
이름 : 배성훈
내용 : 퇴사 2
    문제번호 : 15486번

    dp 문제다
    슬라이딩 윈도우 아이디어와 접목해서 풀어서 빠르게 못풀었다;
    그냥 150만 배열을 할당하고 날짜 계산을 하면 쉽게 되나
    150만이라는 숫자가 크고 50개만 할당해도 풀 수 있어보여 해당 방법으로 풀었다

    이렇게 제출하니 128ms에 풀린다
    그런데, 그냥 150만 배열 할당해도 132ms가 나온다...
*/

namespace BaekJoon.etc
{
    internal class etc_0731
    {

        static void Main731(string[] args)
        {

            int MOD = 50;
            StreamReader sr;

            int[] dp;

            Solve();

            void Solve()
            {


                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 8);

                int n = ReadInt();
                int ret = 0;                            // 최대 수당
#if first

                int s = 0;                              // 현재 날짜
                dp = new int[MOD];


                for (int i = 0; i < n; i++)
                {

                    int t = ReadInt();                  // 걸리는 날짜
                    int v = ReadInt();                  // 수당

                    // 날짜는 당일부터 1일이므로
                    // -1을 해줘야한다
                    int next = (s + t - 1) % MOD;

                    // 최대 수당 입력
                    // 1일에서 3일짜리 일을 받는 경우와
                    // 2일에서 2일짜리 일을 받는 경우
                    // 둘 다 4일에 기록된다
                    // 현재 날짜에서 최대 수당이 기록된다
                    dp[next] = Math.Max(dp[next], ret + v);

                    // 현재 날짜의 최대 수당 결과로 기록
                    ret = Math.Max(dp[s], ret);
                    // 그리고 초기화!
                    dp[s] = 0;
                    s = s == MOD - 1 ? 0 : s + 1;
                }
#else

                dp = new int[1_500_050];

                for (int i = 0; i < n; i++)
                {

                    int t = ReadInt();
                    int v = ReadInt();

                    int next = i + t - 1;
                    dp[next] = Math.Max(dp[next], ret + v);

                    ret = Math.Max(ret, dp[i]);
                }

#endif
                Console.WriteLine(ret);
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
var r = new Reader();
var n = r.NextInt();
var dp = new int[1500051];
for (int i = 1; i <= n; i++)
{
    var (d, p) = (r.NextInt(), r.NextInt());
    var e = i + d - 1;
    dp[e] = Math.Max(dp[e], dp[i - 1] + p);
    dp[i] = Math.Max(dp[i - 1], dp[i]);
}

Console.Write(dp[n]);

class Reader{StreamReader R;public Reader()=>R=new(new BufferedStream(Console.OpenStandardInput()));
public int NextInt(){var(v,n,r)=(0,false,false);while(true){int c=R.Read();if((r,c)==(false,'-')){(n,r)=(true,true);continue;}if('0'<=c&&c<='9'){(v,r)=(v*10+(c-'0'),true);continue;}if(r==true)break;}return n?-v:v;}
}
#elif other2
namespace Baekjoon;

public class Program
{
    readonly static StreamReader _sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
    private static void Main(string[] args)
    {
        var n = ScanInt();
        var dp = new int[n + 1];
        for (int i = 0; i < n; i++)
        {
            var cost = ScanInt();
            var paid = ScanInt();
            if (i + cost <= n)
            {
                ref var nextDp = ref dp[i + cost];
                var newBudget = paid + dp[i];
                if (nextDp < newBudget)
                    nextDp = newBudget;
            }
            dp[i + 1] = Math.Max(dp[i], dp[i + 1]);
        }
        Console.Write(dp[n]);
    }
    static int ScanInt()
    {
        int c, n = 0;
        while (!((c = _sr.Read()) is ' ' or '\n' or -1))
        {
            if (c == '\r')
            {
                _sr.Read();
                break;
            }
            n = 10 * n + c - '0';
        }
        return n;
    }
}
#endif
}
