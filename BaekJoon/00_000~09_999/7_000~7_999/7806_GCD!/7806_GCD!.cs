using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 10
이름 : 배성훈
내용 : GCD!
    문제번호 : 7806번

    정수론, 에라토스테네스 체 문제다.
    아이디어는 다음과 같다.
    먼저 k의 소인수를 찾는다.
    그리고 n!에서 k의 소인수의 갯수를 세어주면 된다.

    값의 범위가 10억이고 매번 √k 를 세기에는 많은거 같이 느꼈다.
    그래서 제곱해서 (√10억) 이하의 모든 소수를 찾아 해당 수에대해서 인자를 찾게했다.
    그러면 합성수들은 for문에서 연산하지 않기에 연산이 적을거라 생각했다.
    그러니 (√10억) 이하의 모든 소수는 3401개가 존재했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1622
    {

        static void Main1622(string[] args)
        {

            int LEN = 3_401;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int[] prime = new int[LEN];
            int[] cnt = new int[LEN];
            int n, k;

            SetPrime();

            while ((n = ReadInt()) >= 0)
            {

                k = ReadInt();

                int ret = GetGCD(n, k);

                sw.Write($"{ret}\n");
            }

            void SetPrime()
            {

                bool[] notPrime = new bool[31_624];

                int len = 0;
                for (int i = 2; i < notPrime.Length; i++)
                {

                    if (notPrime[i]) continue;
                    prime[len++] = i;

                    for (int j = i << 1; j < notPrime.Length; j += i)
                    {

                        notPrime[j] = true;
                    }
                }
            }

            int GetGCD(int _n, int _k)
            {

                if (_n >= _k) return _k;
                for (int i = 0; i < LEN; i++)
                {

                    while (_k % prime[i] == 0) 
                    {

                        cnt[i]++;
                        _k /= prime[i];
                    }
                }

                int ret = _k <= _n ? _k : 1;
                for (int i = 0; i < LEN; i++)
                {

                    if (cnt[i] == 0) continue;
                    int exp = Math.Min(ChkCnt(_n, prime[i]), cnt[i]);
                    cnt[i] = 0;

                    ret *= MyPow(prime[i], exp);
                }

                return ret;

                int MyPow(int _a, int _exp)
                {

                    int ret = 1;
                    while (_exp > 0)
                    {

                        if ((_exp & 1) == 1) ret *= _a;
                        _a *= _a;
                        _exp >>= 1;
                    }

                    return ret;
                }

                int ChkCnt(int _n, int _div)
                {

                    int ret = 0;
                    while (_n > 1)
                    {

                        _n /= _div;
                        ret += _n;
                    }

                    return ret;
                }
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    ret = c - '0';

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }

#if other
// #include <cstdio>

int main() {
	for (int n, k; ~scanf("%d %d", &n, &k); ) {
		int ans = 1;
		for (int i = 2; i * i <= k; ++i) {
			int cnt = 0;
			for (; k % i == 0; ++cnt) k /= i;
			for (int cpy = n; cpy && cnt; cpy /= i) {
				int q = cpy / i;
				for (int qq = q; qq && cnt; --qq, --cnt) ans *= i;
			}
		}

		if (k > 1 && n >= k) ans *= k;

		printf("%d\n", ans);
	}

	return 0;
}
#endif
}
