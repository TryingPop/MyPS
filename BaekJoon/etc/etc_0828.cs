using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 19
이름 : 배성훈
내용 : 거의 소수
    문제번호 : 1456번

    수학, 정수론, 소수판정 문제다
    아이디어는 간단하다 소수의 제곱수들을 찾는 것이기에
    루트 범위로 소수판정을 했다

    그리고 10^14승까지 들어오기에 7만부터는 (7만)^3 > 3 * 10^14 이기에 3제곱이 못들어온다
    그래서 5만이상부터는 제곱만 확인했다

    그리고 이후에는 (5만)^4 < long 범위 안이므로 오버플로우가 발생하지 않는다
*/

namespace BaekJoon.etc
{
    internal class etc_0828
    {

        static void Main828(string[] args)
        {

            long MAX_TWO = 50_000L;
            long a, b;

            bool[] notPrime;
            Solve();
            void Solve()
            {

                Input();

                SetPrime();

                GetRet();
            }

            void GetRet()
            {

                int ret = 0;

                long s = (long)Math.Round(Math.Sqrt(a));
                if (s * s < a) s++;
                long e = (long)Math.Round(Math.Sqrt(b));
                if (e * e > b) e--;

                for (long i = 2; i <= e; i++)
                {

                    if (notPrime[i]) continue;
                    long mul = i * i;
                    
                    if (i >= MAX_TWO)
                    {

                        if (a <= mul) ret++;
                        continue;
                    }

                    while(mul <= b)
                    {

                        if (a <= mul) ret++;
                        mul *= i;
                    }
                }

                Console.Write(ret);
            }

            void SetPrime()
            {

                int len = (int)Math.Round(Math.Sqrt(b)) + 1;

                notPrime = new bool[len];
                notPrime[0] = true;
                notPrime[1] = true;
                for (int i = 2; i < len; i++)
                {

                    if (notPrime[i]) continue;

                    for (int j = i << 1; j < len; j += i)
                    {

                        notPrime[j] = true;
                    }
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();

                a = long.Parse(temp[0]);
                b = long.Parse(temp[1]);
            }
        }
    }

#if other
#nullable disable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var ab = sr.ReadLine().Split(' ').Select(Int64.Parse).ToArray();
        var a = ab[0];
        var b = ab[1];

        var s = new Sieve(10_000_000);
        var primes = s.GetPrimeList();

        var count = 0;

        for (var pow = 2; pow <= 46; pow++)
            foreach (var prime in primes)
            {
                var primepow = FastPow(prime, pow);

                if (b < primepow)
                    break;

                if (a <= primepow)
                    count++;
            }

        sw.WriteLine(count);
    }

    public static long FastPow(long a, long x)
    {
        var rv = 1L;
        while (x > 0)
        {
            if ((x & 1) == 1)
                rv *= a;

            a *= a;
            x /= 2;
        }

        return rv;
    }
}
#elif other2
// #include <cstdio>
// #include <algorithm>
// #include <cstdlib>
// #include <cmath>
// #include <climits>
// #include <cstring>
// #include <string>
// #include <vector>
// #include <queue>
// #include <numeric>
// #include <functional>
// #include <set>
// #include <map>
// #include <unordered_map>
// #include <unordered_set>
// #include <memory>
// #include <thread>
// #include <tuple>

using namespace std;

long long intpow(long long v, long long p) {
  long long res = 1;
  if (p == 0) return 1;
  if (v <= 0) return 0;
  while (p-- > 0) {
    if (v > LLONG_MAX / res) return LLONG_MAX;
    res *= v;
  }
  return res;
}

constexpr size_t blocksize = 3 * 5 * 7 * 128;
constexpr size_t valsz = (10'000'000 + 1) / 2 + blocksize;
uint8_t val[valsz];

int main() {
  for (int p : vector<int>{3,5,7}) {
    for (int j = p/2; j < blocksize; j += p) {
      val[j] = 1;
    }
  }
  {
    for (int j = blocksize; j + blocksize <= valsz; j += blocksize) {
      memcpy(val + j, val, blocksize);
    }
  }
  val[3/2] = 0;
  val[5/2] = 0;
  val[7/2] = 0;
  val[0] = 1;
  for (unsigned int i = 11; i * i <= 10'000'000; i += 2) {
    if (val[i>>1]) continue;
    for (unsigned int j = (i*i)>>1; j <= 5'000'000; j += i) {
      val[j] = 1;
    }
  }
  int ans = 0;
  long long a, b;
  scanf("%lld%lld", &a, &b);
  a = max(a, 4ll);
  {
    unsigned int p = 2;
    unsigned long long v = p * p;
    while (v <= b) {
      ans += (a <= v);
      v *= p;
    }
  }
  for (int po = 2; po <= 30; po++) {
    double approxLow = pow((double)a, 1.0 / po);
    double approxHigh = pow((double)b, 1.0 / po);
    int low = approxLow, high = approxHigh;
    while (intpow(low, po) < a) low++;
    while (intpow(low - 1, po) >= a) low--;
    while (intpow(high, po) > b) high--;
    while (intpow(high + 1, po) <= b) high++;
    if (low % 2 == 0) low++;
    if (high % 2 == 0) high--;
    if (low > high) continue;
    low /= 2;
    high /= 2;
    for (int i = low; i <= high; i++) {
      ans += 1-val[i];
    }
  }
  printf("%d\n", ans);
  return 0;
}

#endif
}
