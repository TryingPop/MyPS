using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 18
이름 : 배성훈
내용 : 파스칼 삼각형
    문제번호 : 15489번

    수학, dp, 조합론 문제다
    최대 범위의 파스칼 삼각형을 만들고, 이후에 합을 구했다
*/

namespace BaekJoon.etc
{
    internal class etc_0565
    {

        static void Main565(string[] args)
        {

            int[] info = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            int[,] dp = new int[31, 31];
            Solve();

            void Solve()
            {

                SetPascal();
                int ret = 0;
                for (int i = 0; i < info[2]; i++)
                {

                    for (int j = 0; j <= i; j++)
                    {

                        ret += dp[info[0] + i, info[1] + j];
                    }
                }

                Console.WriteLine(ret);
            }

            void SetPascal()
            {

                // 파스칼 삼각형 세팅
                // 여기 문제의 좌표에 맞게 1씩 증가시켜 계산했다
                // ex 7 C 5 -> 인덱스가 8, 6에 담긴다
                dp[1, 1] = 1;
                for (int i = 1; i < 30; i++)
                {

                    dp[i, 1] = 1;
                    dp[i, i] = 1;
                    for (int j = 2; j <= i - 1; j++)
                    {

                        dp[i, j] = dp[i - 1, j - 1] + dp[i - 1, j];
                    }
                }
            }
        }
    }

#if other
var a = Array.ConvertAll(Console.ReadLine()!.Split(' '), int.Parse);
var b = new int[31, 31]; b[1, 1] = 1;
for (int i = 2; i < 31; i++)
{
    for (int j = 1; j <= i; j++)
    {
        b[i, j] = b[i - 1, j] + b[i - 1, j - 1];
    }
}
long s = 0;
for (int i = 0; i < a[2]; i++)
{
    for (int j = 0; j <= i; j++)
    {
        s += b[i + a[0], j + a[1]];
    }
}
Console.WriteLine($"{s}");
#elif other2
using StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
using StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
int[] RCW = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
int R = RCW[0]; // R번째 줄
int C = RCW[1]; // C번째 수를 위 꼭지점
int W = RCW[2]; // 한 변이 포함하는 수의 개수

// 파스칼 삼각형
int[][] pascal = new int[31][];
pascal[0] = new int[1];
pascal[1] = new int[2];
pascal[1][1] = 1;
for(int i = 2; i <= 30; i++) {
    pascal[i] = new int[pascal[i - 1].Length + 1];
    pascal[i][1] = 1;
    for(int j = 2; j < pascal[i].Length - 1; j++) {
        pascal[i][j] = pascal[i - 1][j - 1] + pascal[i - 1][j];
    }
    pascal[i][pascal[i].Length - 1] = 1;
}

int sum = 0;
int iii = 1;
for(int i = R; i < R + W; i++) {
    for(int j = C; j < C + iii; j++) {
        sum += pascal[i][j];
    }
    iii++;
}
sw.WriteLine(sum);

/*
foreach(var item in pascal) {
    foreach(var item2 in item) {
        sw.Write(item2 + " ");
    }
    sw.WriteLine();
}
*/

sw.Close();
sr.Close();
#endif

}
