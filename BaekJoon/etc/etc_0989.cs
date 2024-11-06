using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 22
이름 : 배성훈
내용 : 타일 채우기 3
    문제번호 : 14852번

    dp 문제다
    비트마스킹으로 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0989
    {

        static void Main989(string[] args)
        {

            int MOD = 1_000_000_007;

            int n;
            int[][] dp;

            Solve();
            void Solve()
            {

                Init();

                GetRet();
            }

            void GetRet()
            {

                dp[0][0] = 1;
                for (int i = 0; i < n; i++)
                {

                    dp[1][0] = (dp[0][0] << 1) % MOD;
                    dp[1][1] = dp[0][0];
                    dp[1][2] = dp[0][0];
                    dp[1][3] = dp[0][0];

                    dp[1][0] = (dp[1][0] + dp[0][1]) % MOD;
                    dp[1][2] = (dp[1][2] + dp[0][1]) % MOD;

                    dp[1][0] = (dp[1][0] + dp[0][2]) % MOD;
                    dp[1][1] = (dp[1][1] + dp[0][2]) % MOD;

                    dp[1][0] = (dp[1][0] + dp[0][3]) % MOD;

                    var temp = dp[0];
                    dp[0] = dp[1];
                    dp[1] = temp;
                }

                Console.Write(dp[0][0]);
            }

            void Init()
            {

                n = int.Parse(Console.ReadLine());

                dp = new int[2][];
                dp[0] = new int[1 << 2];
                dp[1] = new int[1 << 2];
            }
        }
    }

#if other
using System.Runtime.Intrinsics.Arm;
using static IO;
public static class IO {
public static StreamReader reader = new(Console.OpenStandardInput());
public static StreamWriter writer = new(Console.OpenStandardOutput());
public static void Cin(out int num) =>num = int.Parse(reader.ReadLine());
public static void Cin(out string str) => str = reader.ReadLine();
public static void Cin(out int[] numarr,char c= ' ') => numarr = Array.ConvertAll(reader.ReadLine().Split(c),int.Parse);
public static void Cin(out string[] strarr,char c=' ') => strarr = reader.ReadLine().Split(c);
public static void Cin(out double d) => d=double.Parse(reader.ReadLine());
public static void Cin(out int a,out int b,char c= ' ') {var s = Array.ConvertAll(reader.ReadLine().Split(c),int.Parse);(a,b) = (s[0],s[1]);}
public static void Cin(out int a,out int b,out int c,char e=' ') {var s = Array.ConvertAll(reader.ReadLine().Split(e),int.Parse);(a,b,c) = (s[0],s[1],s[2]);}
public static object? Cout { set {writer.Write(value);}}
static void Main() {Program.Coding();writer.Flush();}
}
class Program {
    public static void Coding() {
        uint mod = 1_000_000_007;
        ulong[] arr = new ulong[1_000_001];
        arr[1] = 2; 
        arr[2] = 7;
        ulong temp = 0;
        for(int i=3;i<1000001;i++) arr[i] = ((arr[i-1] << 1)%mod) + ((arr[i-2] * 3)%mod) + 2 + (temp = (temp + (arr[i-3] << 1)) % mod);
        Cin(out int n);
        Cout = arr[n] % mod;
    }
}
#endif
}
