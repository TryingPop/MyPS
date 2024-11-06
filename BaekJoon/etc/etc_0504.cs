using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 11
이름 : 배성훈
내용 : 연속합2
    문제번호 : 13398번

    dp 문제다
    dp를 다음과 같이 설정했다
    인덱스가 i, 0의 경우는 i번째를 포함하고 제외된 원소가 없는 경우다
    인덱스가 i, 1의 경우 1개를 제외시킨 경우의 최대값이 담기게 dp를 설정했다
*/

namespace BaekJoon.etc
{
    internal class etc_0504
    {

        static void Main504(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int n = ReadInt();

            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
            }
            sr.Close();

            int[,] dp = new int[n, 2];

            int max = arr[0];
            dp[0, 0] = arr[0];
            for (int i = 1; i < n; i++)
            {

                // 최소한 하나를 포함시켜야한다
                int chk = dp[i - 1, 0] + arr[i];
                // 만약 이전꺼와 현재껄 포함시켜 잇는게 큰 경우
                // 함께 잇는다
                if (chk >= arr[i]) dp[i, 0] = chk;
                // 현재꺼만 드는게 큰 경우 끊는다
                else dp[i, 0] = arr[i];

                // 이제 카드 빼는 경우 확인
                chk = dp[i - 1, 1] + arr[i];
                // 이전에 뺀 경우에 현재꺼 잇는게 큰 경우다
                if (dp[i - 1, 0] < chk) dp[i, 1] = chk;
                // 현재껄 그냥 빼는게 큰 경우다
                else dp[i, 1] = dp[i - 1, 0];

                max = Math.Max(max, dp[i, 0]);
                max = Math.Max(max, dp[i, 1]);
            }

            Console.WriteLine(max);

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
namespace Baekjoon;

public class Program
{
    private static void Main(string[] args)
    {
        using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        var n = ScanSignedInt(sr);
        int max = int.MinValue + 100_000_000,
            preUsedFollowingMax = int.MinValue + 100_000_000,
            preUnusedFollowingMax = int.MinValue + 100_000_000;

        for (int i = 0; i < n; i++)
        {
            int num = ScanSignedInt(sr),
                curUnusedFollowingMax,
                curUsedFollowingMax;
            curUnusedFollowingMax = Math.Max(num, preUnusedFollowingMax + num);
            curUsedFollowingMax = Math.Max(preUnusedFollowingMax, preUsedFollowingMax + num);
            preUnusedFollowingMax = curUnusedFollowingMax;
            preUsedFollowingMax = curUsedFollowingMax;
            max = Math.Max(curUnusedFollowingMax, Math.Max(curUsedFollowingMax, max));
        }
        Console.Write(max);
    }

    static int ScanSignedInt(StreamReader sr)
    {
        int c = sr.Read(), n = 0;
        if (c == '-')
            while (!((c = sr.Read()) is ' ' or '\n' or -1))
            {
                if (c == '\r')
                {
                    sr.Read();
                    break;
                }
                n = 10 * n - c + '0';
            }
        else
        {
            n = c - '0';
            while (!((c = sr.Read()) is ' ' or '\n' or -1))
            {
                if (c == '\r')
                {
                    sr.Read();
                    break;
                }
                n = 10 * n + c - '0';
            }
        }
        return n;
    }
}

#elif other2
using System;

namespace codingtest
{
    class 기초_다프1
    {
        static void Main(){ //연속합 2

            int n = int.Parse(Console.ReadLine());
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int[,] dp = new int[n, 2];
            dp[0, 0] = dp[0, 1] = arr[0];
            int result = arr[0];

            for(int i=1; i<n; i++){
                dp[i, 0] = Math.Max(dp[i-1, 0] + arr[i], arr[i]);
                dp[i, 1] = Math.Max(dp[i-1, 0], dp[i-1, 1] + arr[i]);
                result = Math.Max(result, Math.Max(dp[i, 0], dp[i, 1]));
            }   
            Console.Write(result);
        }
    }
}
#elif other3
using System;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Serialization.Formatters;

class Program
{
    int[] prime = new int[10000];
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        int[] inputArr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int[,] dp = new int[inputArr.Length,2];
        dp[0,0] = dp[0,1] =  inputArr[0];
        int max = dp[0,1];

        for (int i = 1; i < inputArr.Length; i++)
        {
            dp[i,0] = dp[i - 1,0] + inputArr[i] > inputArr[i] ? dp[i - 1,0] + inputArr[i] : inputArr[i];
            dp[i,1] = dp[i - 1,0] > dp[i - 1,1] + inputArr[i] ? dp[i-1,0] : dp[i - 1,1] + inputArr[i];
            //Console.WriteLine($"{dp[i, 1]},{dp[i,0]} : {dp[i-1,1]}, {dp[i-1,0]}, {inputArr[i]}");
            max = max > (dp[i,0] > dp[i,1] ? dp[i,0] : dp[i, 1]) ? max : (dp[i, 0] > dp[i, 1] ? dp[i, 0] : dp[i, 1]);
        }

        Console.WriteLine(max);
    }
}
#endif
}
