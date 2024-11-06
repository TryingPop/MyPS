using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 ; 2024. 7. 29
이름 : 배성훈
내용 : Fibo
    문제번호 : 11238번

    수학, 정수론, 분할 정복을 이용한 거듭제곱, 유클리드 호제법 문제다

    유클리드 호제법에 의해
    gcd(F_n, F_m) = F_gcd(n, m)
    이 성립한다

    그래서 10억 이하의 피보나치 수를 빠르게 찾는게 중요하다
*/

namespace BaekJoon.etc
{
    internal class etc_0849
    {

        static void Main849(string[] args)
        {

            int MOD = 1_000_000_007;

            StreamReader sr;
            StreamWriter sw;

            Solve();
            void Solve()
            {

                Init();

                int test = ReadInt();

                while(test-- > 0)
                {

                    int gcd = GetGCD(ReadInt(), ReadInt());

                    long ret = GetFibo(gcd);
                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            }

            long GetFibo(int _n)
            {

                if (_n <= 2) return 1L;
                return GetMul(_n - 2);
            }

            long GetMul(int _exp)
            {

                (long v11, long v12, long v21, long v22) ret = (1, 0, 0, 1);
                (long v11, long v12, long v21, long v22) mul = (1, 1, 1, 0);

                while(_exp > 0)
                {

                    if ((_exp & 1) == 1) MatMul(ref ret, ref mul);

                    _exp >>= 1;
                    MatMul(ref mul, ref mul);
                }

                return (ret.v11 + ret.v12) % MOD;
            }

            void MatMul(ref (long v11, long v12, long v21, long v22) _f, 
                ref (long v11, long v12, long v21, long v22) _b)
            {

                long v11 = (_f.v11 * _b.v11 + _f.v12 * _b.v21) % MOD;
                long v12 = (_f.v11 * _b.v12 + _f.v12 * _b.v22) % MOD;
                long v21 = (_f.v21 * _b.v11 + _f.v22 * _b.v21) % MOD;
                long v22 = (_f.v21 * _b.v12 + _f.v22 * _b.v22) % MOD;

                _f = (v11, v12, v21, v22);
            }

            int GetGCD(int _a, int _b)
            {

                while(_b > 0)
                {

                    int temp = _a % _b;
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
// #include <cstdio>
// #define loop(i,n) for(int i=0;i<n;++i)
typedef long long ll;

const int mod = 1000000007;

ll A[2][2];
ll B[2][2];
ll res[2][2];

void multiply(ll x[][2], ll y[][2]) {
	B[0][0] = B[0][1] = B[1][0] = B[1][1] = 0;
	loop(i, 2) loop(k, 2) loop(j, 2) {
		B[i][j] += x[i][k] * y[k][j];
		B[i][j] %= mod;
	}
	x[0][0] = B[0][0], x[0][1] = B[0][1];
	x[1][0] = B[1][0], x[1][1] = B[1][1];
}
int main() {
	int T; scanf("%d", &T);
START:;
	ll n, m; scanf("%lld %lld", &n, &m);
	if (n > m) { ll tmp = n;n = m;m = tmp; }

	while (1) {
		ll r = m % n;
		if (!r) break;
		m = n, n = r;
	}

	res[0][0] = res[1][1] = 1;
	res[0][1] = res[1][0] = 0;

	A[0][0] = A[0][1] = A[1][0] = 1;
	A[1][1] = 0;

	while (n) {
		if (n & 1) multiply(res, A);
		multiply(A, A);
		n /= 2;
	}

	printf("%lld\n", res[0][1]);
	if (--T) goto START;
	return 0;
}
#endif
}
