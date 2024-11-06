using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 31
이름 : 배성훈
내용 : 합과 곱
    문제번호 : 1353번

    수학, 미적분학 문제다
    아이디어는 산술 - 기하 평균이다
    그리고 극점을 통해 최대값을 찾는다
    그리고 자연수 값에서 성립할 수 있는지 확인해야한다
    input[1]이 자연수이므로 자연수 범위는 input[0]까지만 확인하면 된다
    input[1]에 실수값이 들어온다면, 극점으로 판별해야한다
    아니면 극대값을 벗어났는지 확인해서 for문을 돌려도 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0401
    {

        static void Main401(string[] args)
        {

            double[] input = Array.ConvertAll(Console.ReadLine().Split(), double.Parse);

            if (input[0] == input[1])
            {

                Console.WriteLine(1);
                return;
            }

            if (ChkInvalid()) 
            {

                Console.WriteLine(-1);
                return; 
            }

            // 오버플로우가 매우 빈번하게 발생한다!
            // 당장 100, 100만을 하면 10^17값이다
            // long max = (long)Math.Pow(Math.E, input[0] / Math.E) + 10;

            double before = -1;
            long ret = -1;


            // 그리디하게 생각하면, input[0]까지만 해도 된다
            // for (int i = 2; ; i++)
            for (int i = 2; i <= input[0]; i++)
            {

                double calc = Find(i);
                if (calc >= input[1])
                {

                    // 정답을 찾은 경우
                    ret = i;
                    break;
                }
#if _
                else if (before > calc) 
                { 
                    
                    // 극점을 넘어가는 경우
                    // MAX를 찾아서 하고 싶었으나, 오버플로우 발생이 잦다
                    ret = -1;
                    break;
                }

                before = calc;
#endif
            }

            Console.WriteLine(ret);

            double Find(long _n)
            {

                // 연산값
                return Math.Pow(input[0] / _n, _n);
            }


            bool ChkInvalid()
            {

                // 산술 - 기하 평균
                double chk = Math.Pow(Math.E, input[0] / Math.E);
                if (chk < input[1]) return true;
                return false;
            }
        }
    }

#if other
using System;
using System.IO;
using System.Linq;
using System.Numerics;

#nullable disable

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
            using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

            Solve(sr, sw);
        }

        public static void Solve(StreamReader sr, StreamWriter sw)
        {
            var sp = sr.ReadLine().Split(' ').Select(Int64.Parse).ToArray();
            var s = sp[0];
            var p = sp[1];

            if (s == p)
            {
                sw.WriteLine(1);
                return;
            }

            for (var divcount = 2; divcount <= 10 + s / Math.E; divcount++)
            {
                var max = Math.Pow((double)s / divcount, divcount);
                if (p <= max)
                {
                    sw.WriteLine(divcount);
                    return;
                }

                //if (p * FastPow(divcount, divcount) <= FastPow(s, divcount))
                //{
                //    sw.WriteLine(divcount);
                //    return;
                //}
            }

            sw.WriteLine(-1);
        }

        public static BigInteger FastPow(BigInteger a, BigInteger x)
        {
            var result = BigInteger.One;

            while (x > 0)
            {
                if (x % 2 == 1)
                    result *= a;

                x /= 2;
                a *= a;
            }

            return result;
        }
    }
}
#elif other2
S, P = map(int, input().split())

n = -1
for i in range(2, S + 1):
  A = (S / i)**i
  if A >= P:
    n = i
    break

if S == P:
  n = 1

print(n)
#elif other3

// #include<stdio.h>
// #include<math.h>
long long int n, m;
void process()
{
	//10 <= x < n
	//(n/x)^x >= m
	//x *(ln(n) - ln(x)) >= ln(m)

	long long int x;
	for (x = 3; x < n; x++) {
		double lnn = log(n);
		double lnx = log(x);
		double lnm = log(m);
		if (x * (lnn - lnx) >= lnm) {
			printf("%lld\n", x);
			return;
		}
	}
	printf("-1\n");
}
int main() {
	//test();
	scanf("%lld %lld", &n, &m);
	if (n == m)printf("1\n");
	else if (n*n >= 4 * m)printf("2\n");
	else {
        process();
	}
}
#endif
}
    