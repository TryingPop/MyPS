using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 12
이름 : 배성훈
내용 : 낮잠 시간 
    문제번호 : 1988번

    dp 문제다
    아이디어는 다음과 같다
    dp[i][j][k]를 i는 현재와 다음을 나타내는 인덱스고,
    j는 i번째를 의미한다 그리고 k는 수면 유무를 나타내고
    dp[i][j][k]에는 최대 만족도를 저장한다

    이렇게 dp를 세우면 점화식은
    dp[1][j][1]은 dp[0][j - 1][0] 이전에 안잔 경우와
    앞번에 잔 경우 dp[0][j - 1][1] + arr[j]의 중 큰 값이 된다
    여기서 arr은 만족도 배열이다

    현재 수면이 아닌 경우는 이전 최대값
    dp[1][j - 1][0]과 dp[1][j - 1][1] 중 큰 값을 가져오면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0964
    {

        static void Main964(string[] args)
        {

            StreamReader sr;

            int[] arr;
            int[][][] dp;
            int n, b;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                for (int i = 2; i <= b; i++)
                {

                    for (int j = i - 1; j < n; j++)
                    {

                        dp[1][j][1] = Math.Max(dp[0][j - 1][1] + arr[j], dp[0][j - 1][0]);
                        dp[1][j][0] = Math.Max(dp[1][j - 1][1], dp[1][j - 1][0]);
                    }
                }

                Console.Write(Math.Max(dp[0][n - 1][0], dp[0][n - 1][1]));
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                b = ReadInt();

                dp = new int[2][][];
                dp[0] = new int[n][];
                dp[1] = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    dp[0][i] = new int[2];
                    dp[1][i] = new int[2];
                }

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
using ProblemSolving.Templates.Utility;
using System;
using System.IO;
using System.Linq;
namespace ProblemSolving.Templates.Utility {}
namespace System {}
namespace System.IO {}
namespace System.Linq {}

#nullable disable

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        Solve(sr, sw);
    }

    public static void Solve(StreamReader sr, StreamWriter sw)
    {
        var (n, b) = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var gain = new long[n];

        for (var idx = 0; idx < n; idx++)
            gain[idx] = Int64.Parse(sr.ReadLine());

        // selected, prev selected, checked
        var dp = new long?[1 + b, 2];
        var prevdp = new long?[1 + b, 2];
        prevdp[0, 0] = 0;

        var maxcost = 0L;

        for (var chk = 0; chk < n; chk++)
        {
            Array.Clear(dp);

            for (var sel = 0; sel < b; sel++)
                for (var prev = 0; prev < 2; prev++)
                    if (prevdp[sel, prev].HasValue)
                    {
                        if (prev == 0)
                        {
                            dp[1 + sel, 1] = Math.Max(dp[1 + sel, 1] ?? 0, prevdp[sel, prev].Value);
                            dp[sel, 0] = Math.Max(dp[sel, 0] ?? 0, prevdp[sel, prev].Value);
                        }
                        else
                        {
                            dp[1 + sel, 1] = Math.Max(dp[1 + sel, 1] ?? 0, gain[chk] + prevdp[sel, prev].Value);
                            dp[sel, 0] = Math.Max(dp[sel, 0] ?? 0, prevdp[sel, prev].Value);
                        }

                        maxcost = Math.Max(maxcost, Math.Max(dp[1 + sel, 1].Value, dp[sel, 0].Value));
                    }

            (dp, prevdp) = (prevdp, dp);
        }

        sw.WriteLine(maxcost);
    }
}

namespace ProblemSolving.Templates.Utility
{
    public static class DeconstructHelper
    {
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2) => (v1, v2) = (arr[0], arr[1]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3) => (v1, v2, v3) = (arr[0], arr[1], arr[2]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4) => (v1, v2, v3, v4) = (arr[0], arr[1], arr[2], arr[3]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5) => (v1, v2, v3, v4, v5) = (arr[0], arr[1], arr[2], arr[3], arr[4]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6) => (v1, v2, v3, v4, v5, v6) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6, out T v7) => (v1, v2, v3, v4, v5, v6, v7) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6, out T v7, out T v8) => (v1, v2, v3, v4, v5, v6, v7, v8) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6], arr[7]);
    }
}
#elif other2
// #include<cstdio>
// #include<algorithm>
int dp[3010][2],N,B,C;
int main(){
	scanf("%d%d",&N,&B);B--;
	for(int i=1;i<=N;i++){
		scanf("%d",&C);
		for(int j=std::min(B,i);j>0;j--){
			dp[j][0]=std::max(dp[j][0],dp[j][1]);
			dp[j][1]=std::max(dp[j-1][0],dp[j-1][1]+C);
		}
	}
	printf("%d",std::max(dp[B][0],dp[B][1]));
	return 0;
}
#endif
}
