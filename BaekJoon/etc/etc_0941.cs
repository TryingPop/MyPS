using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 4
이름 : 배성훈
내용 : 거스름돈이 싫어요
    문제번호 : 20003번

    수학, 정수론, 유클리드 호제법 문제다
    수의 범위가 40까지이고 50개까지 들어온다
    소수끼리 곱하면, 10^12보다 크므로 int 자료형은 오버플로우가 난다
    그래서 long 으로 했다

    또한 최대 단위의 코인이므로 모든 분수들을 나누는 기약 분수를 찾아야한다
    먼저 통분을 하고, 이후 분자를 찾았다
    분자와 분모의 gcd를 나눠 확실하게 기약분수로 만들었다
*/

namespace BaekJoon.etc
{
    internal class etc_0941
    {

        static void Main941(string[] args)
        {

            StreamReader sr;
            long[] u, d;
            int n;

            long lcm;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                long gcd = (lcm / d[0]) * u[0];
                for (int i = 1; i < n; i++)
                {

                    long chk = (lcm / d[i]) * u[i];
                    gcd = GetGCD(gcd, chk);
                }

                long g = GetGCD(gcd, lcm);
                Console.Write($"{gcd / g} {lcm / g}");
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                u = new long[n];
                d = new long[n];

                lcm = 1;

                for (int i = 0; i < n; i++)
                {

                    int a = ReadInt();
                    int b = ReadInt();

                    long gcd = GetGCD(a, b);

                    u[i] = a / gcd;
                    d[i] = b / gcd;

                    gcd = GetGCD(b, lcm);
                    lcm = b * (lcm / gcd);
                }

                sr.Close();
            }

            long GetGCD(long _a, long _b)
            {

                while (_b > 0)
                {

                    long temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
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
// #include <stdio.h>
long long gcd(long long a, long long b) {
	if (a % b == 0)
	{
		return b;
	}
	return gcd(b, a % b);
}

int main() {
	long long n,J[100],K[100];
	long long L=1;
	scanf("%lld", &n);
	for (int i = 0;i < n;i++) {
		
		scanf("%lld %lld", &J[i], &K[i]);
		
	}
	for (int i = 0;i < n;i++) {
		L = L* K[i] / gcd(L, K[i]);
		
		
	}
	for (int i = 0;i < n;i++) {
		J[i] *= L / K[i];
		
	}
	for (int i = 0;i < n - 1;i++) {
		J[i + 1] = gcd(J[i], J[i + 1]);
	}
	printf("%lld %lld", J[n - 1]/gcd(J[n-1],L), L / gcd(J[n - 1], L));
}
#endif
}
