using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 10
이름 : 배성훈
내용 : 제곱 ㄴㄴ
    문제번호 : 1557번

    수학, 정수론, 이분탐색, 포함 배제의 원리 문제다
    문제에 막히면 혼자 고민 -> 다른 사람 질문글 참고 -> 힌트 참고 -> 여기서 다음에 푼다 or 검색한다
    순으로 보통 행동을 취한다 여기서 다음 행동으로 넘어가는데 짧으면 30분 길면 3시간 가까이 걸린다
    여기서 다른 사람 질문글을 참고하다가 뫼비우스 함수를 참고하면 빨리 풀 수 있다는 정보를 얻었다
    그래서 뫼비우스 함수를 알아보고, 해당 범위 안의 모든 제곱 ㄴㄴ수를 찾을 수 있게 되었다
    이를 이용해 n번째 제곱 ㄴㄴ수를 찾았다

    아이디어는 다음과 같다
    위에서 언급했듯이 n이 주어지면 1 ~ n안의 제곱 수를 찾는 함수를 만들었다
    그리고, n - 제곱 수를 뺀게 해당 범위 안의 제곱 ㄴㄴ수의 개수가된다

    제곱 ㄴㄴ수의 개수가 입력값이 되는 숫자를 이분탐색으로 찾는다
    찾은 값을 바로 반환하면 대부분의 경우는 해결될 것이다
    그런데, 그런데 찾은 값이 제곱 ㄴㄴ수인 경우도 있다 (100 -> 164, 163)
    이경우 해당 수가 제곱 수를 약수로 갖는지 확인해서 제곱 ㄴㄴ수가 될때까지 1씩 줄여나갔다
    끽해야 4.5만번 연산이고 20억 안에 연달아 1000개가 제곱 수인 경우는 없다고 판단했기 때문이다

    주된 아이디어는 뫼비우스 함수로 제곱 수를 찾는 것이다
    뫼비우스 함수를 보면,
    m : N -> { -1, 0, 1 }, N : 자연수 집합
    자연수 x를 소인수 분해 했을 때, 제곱인수 2^2, 3^5, 7^3같은 것을 포함하면 m(x) = 0
    제곱인수가 없는 경우 m(x) = (-1)^k, 여기서 k는 서로 소인 소인수의 개수가된다
    만약 소인수가 없는 1인 경우 m(1) = 1이다 그러면 잘 정의된 함수이다
    
    뫼비우스를 채우는 방법은 소수를 발견할 때마다
    -1을 곱해준다, 해당 소수의 제곱수들은 0으로 처리한다|

    혹은 뫼비우스 함수의 정의로부터 a, b가 서로오시면 m(a * b) = m(a) * m(b)와
    sig(m(d))는 sig는 시그마 기호를 줄여 쓴 것이고, 여기서 d는 n을 나누는 수라고 하자
    그러면 f(n) = sig((m(d)) 는 n == 1이면 f(n) = 1, n > 1이면 f(n) = 0이된다
    만약 12에대해 보면 12의 약수는 1, 2, 4, 3, 6, 12이고
    m(1) = 1, m(2) = -1, m(3) = -1, m(4) = 0, m(6) = 1, m(12) = 0
    f(12) = sig(m(d)) = 1 + -1 + -1 + 0 + 1 + 0 = 0 이성립한다
    이걸로 약수 중에 자기자신과 빼고 나머지 약수들을 모두 빼면 m(a) 값을 찾을 수 있다
    
    여기까지가 뫼비우스 함수를 찾는 방법이다

    이제 뫼비우스를 이용해 제곱수의 개수를 찾자
    뫼비우스 함수의 경우 제곱수는 0으로 표시된다
    그래서 간단하게 0 ~ n까지 뫼비우스가 0인 경우를 세어도 되나 여기서만 쓸 수 있다 

    이하 아래의 방법은 세 제곱수를 찾는 방법이나 네 제곱수를 찾는 방법에서도 쓸 수 있다
    우선 제곱수를 보자 제곱수는 n^2이므로 n에 관해 조사만 하면 된다 포함배제의 원리를 예를 들어 보자

    2^2으로 나눠떨어지는 것을 전부 카운팅 + 1하자
    그리고 3^2으로 나눠떨어지는 것들을 전부 카운팅 + 1하자
    그러면 6^2으로 나눠떨어지는 것들은 중복되어 카운팅 - 1을 진행해야한다
    이후 5^2으로 나눠떨어지는 것들을 전부 카운팅 +1 하자
    그러면 10^2, 15^2은 나눠 떨어지는 것들은 중복되어 카운팅 -1을 진행해야한다
    그리고나면 30^2으로 나눠 떨어지는 것들은 카운팅 +1을 해야한다

    해당 +1, -1 부분을 보면 뫼비우스 함수의 값에 -1을 곱한 것과 같다
    그래서 해당 개수에 뫼비우스 함수를 곱해가면서 누적해가면 제곱수로 떨어지는 애들을 모두 찾을 수 있게 된다
    그래서 GetSqN 함수식이 나온다

    만약 세제곱수라면 카운팅 부분을 세제곱수만 세게 변형시켜주면 된다
    그냥 제곱없이 원래 것을 넣으면 오일러 피 함수의 공식과 같아진다 (반전공식? 이라 한다)
    정수론에서, 봤었던 기억이 떠오른다
*/

namespace BaekJoon.etc
{
    internal class etc_0500
    {

        static void Main500(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            // 20억에 제곱 수가 8억개 조금 안된다
            // 4.5만^2 > 20억이므로 4.5만개만 확인하면
            // 12억이하의 제곱 ㄴㄴ수는 찾을 수 있다
            // 뫼비우스 배열이다
            int[] m = new int[45_000];
            int[] sqN = new int[45_000];
            m[1] = 1;
            for (int i = 1; i < m.Length; i++)
            {

                // 뫼비우스 함수 찾아가기
                // 자기자신을 제외한 모든 약수들을 모두뺀다
                int j = 2 * i;
                while (j < m.Length)
                {

                    m[j] -= m[i];
                    j += i;
                }

                // 제곱 값 넣는다
                sqN[i] = i * i;
            }

            int l = 1;
            int r = 2_000_000_000;

            // 해당 안에 제곱 ㄴㄴ수를 뺀 수를 찾는다
            while (l <= r)
            {

                int mid = GetMid(l, r);
                int chk = mid - GetSqN(mid);
                if (n <= chk) r = mid - 1;
                else l = mid + 1;
            }

            bool isRet = true;
            int ret = r + 1;
            // 이제 제곱 ㄴㄴ수인지 확인
            while (true)
            {

                isRet = true;
                for (int i = 2; i < sqN.Length; i++)
                {

                    if (i * i > ret) break;
                    if (ret % sqN[i] == 0)
                    {

                        isRet = false;
                        break;
                    }
                }


                if (isRet) break;
                ret--;
            }

            Console.WriteLine(ret);
            int GetMid(int _a, int _b)
            {

                long ret = ((long)_a + _b) / 2;
                return (int)ret;
            }

            int GetSqN(int _num)
            {

                // 제곱수로 나눠 떨어지는 값들을 찾는다!
                int ret = 0;
                for (int i = 2; i < m.Length; i++)
                {

                    int diff = m[i] * (_num / sqN[i]);
                    ret -= diff;
                }

                return ret;
            }
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public class Sieve
{
    private int _n;
    private bool[] _isPrime;

    public Sieve(int n)
    {
        _n = n;

        // 2k+1 maps to k
        _isPrime = new bool[1 + (n - 1) / 2];

        for (var x = 3; x <= n; x += 2)
            _isPrime[(x - 1) / 2] = true;

        var sqn = 1 + (int)Math.Sqrt(n);
        for (var x = 3; x <= sqn; x += 2)
            if (_isPrime[(x - 1) / 2])
            {
                for (var y = x * x; y <= n; y += 2 * x)
                    _isPrime[(y - 1) / 2] = false;
            }
    }

    public bool IsPrime(int x)
    {
        if (x % 2 == 0)
            return x == 2;

        return _isPrime[(x - 1) / 2];
    }

    public List<long> GetPrimeList()
    {
        var l = new List<long>();

        for (var x = 2; x <= _n; x++)
            if (IsPrime(x))
                l.Add(x);

        return l;
    }
}

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var sqmax = 50000;
        var s = new Sieve(sqmax);
        var primeSq = s.GetPrimeList().Select(v => v * v).ToArray();

        var k = Int64.Parse(sr.ReadLine());
        var st = 1L;
        var ed = 2_000_000_000L;

        while (st <= ed)
        {
            var mid = st + (ed - st) / 2;
            var fmid = InclExcl(primeSq, 0, 1, 1, mid);

            if (fmid == k)
            {
                while (true)
                {
                    if (primeSq.Any(v => mid % v == 0))
                    {
                        mid--;
                        continue;
                    }

                    break;
                }

                sw.WriteLine(mid);
                return;
            }

            if (fmid > k)
                ed = mid - 1;
            else
                st = mid + 1;
        }

        while (true)
        {
            if (primeSq.Any(v => st % v == 0))
            {
                st--;
                continue;
            }

            break;
        }

        sw.WriteLine(st);
    }

    public static long InclExcl(long[] primeSq, int index, int parity, long div, long k)
    {
        if (index == primeSq.Length)
            return parity * k / div;

        if (k < div * primeSq[index])
            return parity * k / div;

        return InclExcl(primeSq, 1 + index, parity, div, k)
            + InclExcl(primeSq, 1 + index, parity * -1, div * primeSq[index], k);
    }
}

#elif other2

using System;

namespace sweetiemahiro
{
    class boj1557
    {
        public static ulong[] mobius = new ulong[1000010];
        static void MobiusInversion() // 뫼비우스 반전식
        {

            mobius[1] = 1;
            for(int i = 1; i <= 1000000; i++)
            {
                if (mobius[i] != 0)
                {
                    for (int j = 2 * i; j <= 1000000; j += i)
                    {
                        mobius[j] -= mobius[i];
                    }
                }
            }
        }

        static ulong squareFree(ulong n) //제곱 인수가 없는 정수 구하기
        {
            ulong k = 0;
            for (ulong i = 1; i * i <= n; ++i)
                k += (mobius[i] * (n / (i * i)));
            return k;
        }

        static void Main()
        {
            ulong low = 0;
            ulong high = 2000000000;
            ulong k = ulong.Parse(Console.ReadLine());
            MobiusInversion();
            while (low + 1 < high)
            {
                ulong mid = (low + high) / 2;
                if (squareFree(mid) < k) low = mid;
                else high = mid;
            }

            Console.Write(high);
        }
    }
}
#elif other3
// #pragma GCC optimize("Ofast")
// #pragma GCC target("arch=skylake,tune=native")
// #include<unistd.h>
// #include<math.h>
// #define MAXK 1644934081
// #define MAXN 40558

signed char mu[MAXN] = {0, 1};

int f(int n)
{
	int ret = n;
	for (long i = 2; i*i <= n; i++) {
		int x = n / (i*i);
		int j = sqrt(n / x);
		ret += x * (mu[j] - mu[i-1]);
		i = j;
	}
	return ret;
}

void _write(int n) {
	char buf[16] = {};
	int len = 0;
	for (; n; n /= 10) buf[len++] = n % 10 | 48;
	while (len) write(1, buf+--len, 1);
}

int _read() {
	char buf[16] = {};
	read(0, buf, 16);
	int ret = 0;
	for (int i = 0; buf[i] >= 48; i++) ret = ret * 10 + (buf[i] & 15);
	return ret;
}

int __libc_start_main()
{
	for (int i = 1; i < MAXN; i++) {
		for (int j = i+i; j < MAXN; j+=i)
			mu[j] -= mu[i];
		mu[i] += mu[i-1];
	}

	int n = _read();
	int le = 1, ri = MAXK, ans = MAXK;
	while (le <= ri) {
		int mid = ((long)le) + ri >> 1;
		if (f(mid) < n) le = mid + 1;
		else ri = mid - 1, ans = mid;
	}

	_write(ans);
	_Exit(0);
}
int main;
// #elif other4
// #include <bits/stdc++.h>
using namespace std;

vector<int> mobius(long long n) {
    assert(n > 0);
    vector<int> prime, mu(n + 1);
    vector<char> is_prime(n + 1, true);
    is_prime[0] = is_prime[1] = false;
    mu[1] = 1;
    for (auto i = 2; i <= n; ++i) {
        if (is_prime[i]) {
            prime.push_back(i);
            mu[i] = -1;
        }
        for (auto p : prime) {
            if (i * p > n) {
                break;
            }
            is_prime[i * p] = false;
            if (i % p == 0) {
                mu[i * p] = 0;
                break;
            } else {
                mu[i * p] = -mu[i];
            }
        }
    }
    return mu;
}

long long squarefree(long long N) {
    if (N <= 0) {
        return 0;
    }
    auto Imax = static_cast<long long>(pow(N, 0.2));
    auto D = static_cast<long long>(sqrt(N / Imax));
    auto mu = mobius(D);
    auto s1 = 0LL;
    for (auto i = 1LL; i <= D; ++i) {
        s1 += mu[i] * (N / (i * i));
    }
    vector<long long> M_list(D + 1);
    partial_sum(mu.begin(), mu.end(), M_list.begin());
    vector<long long> Mxi_list;
    auto Mxi_sum = 0LL;
    for (auto i = Imax - 1; i > 0; --i) {
        auto Mxi = 1LL;
        auto xi = static_cast<long long>(sqrt(N / i));
        auto sqd = static_cast<long long>(sqrt(xi));
        for (auto j = 1LL; j <= xi / (sqd + 1); ++j) {
            Mxi -= (xi / j - xi / (j + 1)) * M_list[j];
        }
        for (auto j = 2LL; j <= sqd; ++j) {
            if (xi / j <= D) {
                Mxi -= M_list[xi / j];
            } else {
                Mxi -= Mxi_list[Imax - j * j * i - 1];
            }
        }
        Mxi_list.push_back(Mxi);
        Mxi_sum += Mxi;
    }
    auto s2 = Mxi_sum - (Imax - 1) * M_list[D];
    return s1 + s2;
}

unsigned long long modmul(unsigned long long a, unsigned long long b,
                          unsigned long long M) {
    long long ret =
        a * b - M * static_cast<unsigned long long>(1.L / M * a * b);
    return ret + M * (ret < 0) - M * (ret >= static_cast<long long>(M));
}

unsigned long long modpow(unsigned long long b, unsigned long long e,
                          unsigned long long mod) {
    unsigned long long ans = 1;
    for (; e; b = modmul(b, b, mod), e /= 2) {
        if (e & 1) {
            ans = modmul(ans, b, mod);
        }
    }
    return ans;
}

bool is_prime(unsigned long long n) {
    if (n < 2 || n % 6 % 4 != 1) {
        return (n | 1) == 3;
    }
    unsigned long long A[] = {2, 325, 9375, 28178, 450775, 9780504, 1795265022},
                       s = __builtin_ctzll(n - 1), d = n >> s;
    for (unsigned long long a : A) {
        unsigned long long p = modpow(a % n, d, n), i = s;
        while (p != 1 && p != n - 1 && a % n && i--) {
            p = modmul(p, p, n);
        }
        if (p != n - 1 && i != s) {
            return 0;
        }
    }
    return 1;
}

unsigned long long pollard(unsigned long long n) {
    auto f = [n](unsigned long long x) { return modmul(x, x, n) + 1; };
    unsigned long long x = 0, y = 0, t = 30, prd = 2, i = 1, q;
    while (t++ % 40 || std::gcd(prd, n) == 1) {
        if (x == y) {
            x = ++i, y = f(x);
        }
        if ((q = modmul(prd, std::max(x, y) - std::min(x, y), n))) {
            prd = q;
        }
        x = f(x), y = f(f(y));
    }
    return std::gcd(prd, n);
}

std::vector<unsigned long long> factor(unsigned long long n) {
    if (n == 1) {
        return {};
    }
    if (is_prime(n)) {
        return {n};
    }
    unsigned long long x = pollard(n);
    auto l = factor(x), r = factor(n / x);
    l.insert(l.end(), r.begin(), r.end());
    return l;
}

bool is_squarefree(unsigned long long n) {
    auto f = factor(n);
    std::sort(f.begin(), f.end());
    return std::unique(f.begin(), f.end()) == f.end();
}

int main() {
    cin.tie(0)->sync_with_stdio(0);
    long long N;
    cin >> N;
    auto pi = acos(-1);
    auto x = static_cast<long long>((static_cast<double>(N) * pi * pi) / 6);
    auto cnt = squarefree(x);
    if (cnt < N) {
        while (cnt < N) {
            if (is_squarefree(x + 1)) {
                ++cnt;
            }
            ++x;
        }
    } else {
        while (cnt >= N) {
            if (is_squarefree(x)) {
                --cnt;
            }
            --x;
        }
        ++x;
    }
    cout << x;
}

#endif
}
