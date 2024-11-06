using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 30
이름 : 배성훈
내용 : 큰 수
    문제번호 : 7894번

    수학 문제다
    로그로 n! 자리수를 찾는다
*/

namespace BaekJoon.etc
{
    internal class etc_0666
    {

        static void Main666(string[] args)
        {

            int MAX = 10_000_000;

            StreamReader sr;
            StreamWriter sw;

            int[] dp;

            Solve();

            void Solve()
            {

                SetDigit();

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int test = ReadInt();
                while(test-- > 0)
                {

                    int n = ReadInt();
                    sw.Write($"{dp[n]}\n");
                }

                sr.Close();
                sw.Close();
            }

            void SetDigit()
            {

                dp = new int[MAX + 1];
                double digit = 0;
                for (int i = 1; i <= MAX; i++)
                {

                    digit += Math.Log10(i);
                    dp[i] = (int)Math.Ceiling(digit);
                }
                dp[1] = 1;
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

#nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var t = Int32.Parse(sr.ReadLine());
        while (t-- > 0)
        {
            var n = Int32.Parse(sr.ReadLine());

            var logfac = Math.Log10(1);
            for (var v = 2; v <= n; v++)
                logfac += Math.Log10(v);

            sw.WriteLine(1 + Math.Floor(logfac));
        }
    }
}

#elif other2
// #include <cstdio>
// #include <cmath>
using namespace std;

const double PI = 3.14159265;
const double E = 2.71828183;

void proc() {
	int n;
	scanf("%d", &n);

	double r = 0;
	if (n <= 1000) {
		for (int i = 2; i <= n; ++i) {
			r += log10(i);
		}
	}
	else {
		double a = log(sqrt(2 * PI * n));
		double b = n * log(n / E);
		r = (a + b) / log(10);
	}
	printf("%d\n", (int)r + 1);
}

int main() {
	int t;
	scanf("%d", &t);
	while (t-- > 0) {
		proc();
	}
	return 0;
}
#endif
}
