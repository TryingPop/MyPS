using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 10
이름 : 배성훈
내용 : 떡 먹는 호랑이
    문제번호 : 2502번

    수학, dp, 브루트포스 문제다
    1과 2가 사용된 횟수를 dp로 찾은 뒤
    부정방정식으로 접근했다

    문제 조건에서 해가 반드시 존재한다고 했다
    1씩 올려가는 브루트포스로 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0501
    {

        static void Main501(string[] args)
        {

            int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            int[,] use = new int[arr[0] + 1, 2];

            use[1, 0] = 1;
            use[2, 1] = 1;

            for (int i = 3; i <= arr[0]; i++)
            {

                use[i, 0] = use[i - 1, 0] + use[i - 2, 0];
                use[i, 1] = use[i - 1, 1] + use[i - 2, 1];
            }

            int ret1 = 1;
            int ret2 = 1;

            while (true)
            {

                int chk1 = use[arr[0], 0] * ret1;
                int diff = arr[1] - chk1;
                if (diff % use[arr[0], 1] == 0)
                {

                    ret2 = diff / use[arr[0], 1];
                    break;
                }
                else if (diff < 0) break;
                ret1++;
            }

            Console.WriteLine(ret1);
            Console.WriteLine(ret2);
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BackJ
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());

            int[] inputs = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            int D = inputs[0]; int N = inputs[1];
            int[] dp = new int[D + 2];
            dp[0] = 1; dp[1] = 1;

            for (int i = 2; i <= D; i++)
            {
                dp[i] = dp[i - 1] + dp[i - 2];
            }


            int x = dp[D - 3]; int y = dp[D - 2];
            int A = 1; int B = 1;

            while (true)
            {
                if (A * x + B * y == N) break;
                B++;
                if (A * x + B * y > N)
                {
                    A++; B = 1;
                }
            }

            sw.WriteLine(A);
            sw.WriteLine(B);

            sw.Flush();
            sw.Close();
            sr.Close();
        }
    }
}
#elif other2
var a = Array.ConvertAll(Console.ReadLine()!.Split(' '), int.Parse);
var f = new int[31]; f[0] = 0; f[1] = 1;
int n = 0;
for (int i = 2; i < 31; i++)
{
    f[i] = f[i - 1] + f[i - 2];
}
while ((a[1] - f[a[0] - 2] * ++n) % f[a[0] - 1] > 0) { }
Console.WriteLine($"{n}\n{(a[1] - f[a[0] - 2] * n) / f[a[0] - 1]}");
#endif
}
