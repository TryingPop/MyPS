using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 6
이름 : 배성훈
내용 : 이항 계수 4
    문제번호 : 11402번

    Lucas(뤼카?)의 정리로 푸는 문제이다!
    설명은 링크로 대신한다
    https://bowbowbow.tistory.com/2

    간단하게 보면
    큰수 n, k 에 대해 n C k를 p로 나눈 나머지를 구할 때,
    작은 m_i, k_i인 m_i C k_i 들의 곱으로 표현할 수 있는 정리다

    해당 정리를 보고 푸니 66%에서 p^i 인 i승을 찾는 방법을 잘못짜서 1번 틀리고,
    2번째에 이를 수정하니 68ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0142
    {

        static void Main142(string[] args)
        {

            long[] info = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            // 2 ^ 60 > 10^18 > 2^ 59
            // 그래서 최대값 59개까지 선언한다
            long[] pow = new long[60];


            // 초기에 0, 1은 넣는다!
            pow[0] = 1;
            pow[1] = info[2];
            int powLen = 0;
            // 먼저 pow 계산!
            {

                // n = m_i * p^i + m_(i - 1) * p^(i - 1) + ...
                // 인 0이 아닌 i중 가장 큰 수를 찾아야한다
                // 이걸 찾기 위해 n에 info[2]로 몇 번 나누어야 0이되는지로 확인했다
                long calc = info[0];
                for (int i = 2; i < 60; i++)
                {

                    calc /= info[2];
                    if (calc == 0) break;
                    pow[i] = pow[i - 1] * info[2];
                    powLen++;
                }
            }

            // 기존에 n = info[0]이고 k = info[1]로 쓰고 있으므로
            // Lucas 정리에서 m_i, k_i들을 n, k로 표현했다
            int[] n = new int[60];
            int[] k = new int[60];

            int idx = 0;
            int max = -1;

            for (int i = powLen; i >= 0; i--)
            {

                // 여기서 n_i, k_i를 찾는다
                long chk = info[0] / pow[i];
                info[0] %= pow[i];

                n[idx] = (int)chk;
                int calc = (int)chk;
                if (calc > max) max = calc;

                chk = info[1] / pow[i];
                info[1] %= pow[i];
                k[idx] = (int)chk;

                idx++;
            }

            int ret = 1;
            int mod = (int)info[2];

            // 여기서 Lucas 정리 사용
            // 앞에는 Lucas 정리를 쓰기위한 전처리단계다
            for (int i = 0; i < idx; i++)
            {

                // 조합값 찾기
                ret *= GetCombination(n[i], k[i], mod);
                ret %= mod;
            }

            Console.WriteLine(ret);
        }

        static int GetCombination(int _n, int _k, int _mod)
        {

            if (_n < _k) return 0;

            // 조합에서는 n C k == n C n - k 이므로 
            // _k <= _n - k 가 되게 조절!
            if (_k > _n - _k) _k = _n - _k;

            // n!, (n - k)!, k!
            int A, B, C = 1;
            for (int i = 1; i <= _k; i++)
            {

                C *= i;
                C %= _mod;
            }

            B = C;
            for (int i = _k + 1; i <= _n - _k; i++)
            {

                B *= i;
                B %= _mod;
            }

            A = B;
            for (int i = _n - _k + 1; i <= _n; i++)
            {

                A *= i;
                A %= _mod;
            }

            // 페르마 소정리로
            // n! / ((k!) * ((n - k)!))
            // = n! * (k!) ^ (p - 2) * ((n - k)!)^(p - 2)
            // A = n!, B = (n - k)!, C = k!이므로
            // = A * B^(p - 2) * C^(p - 2)
            int ret = A;
            ret *= Multiple(B, _mod - 2, _mod);
            ret %= _mod;
            ret *= Multiple(C, _mod - 2, _mod);
            ret %= _mod;

            return ret;
        }

        static int Multiple(int _n, int _pow, int _mod)
        {

            // 제곱 연산
            int ret = 1;
            while (_pow > 0)
            {

                if ((_pow & 1) == 1)
                {

                    ret *= _n;
                    ret %= _mod;
                }

                _pow /= 2;
                _n = _n * _n;
                _n %= _mod;
            }

            return ret;
        }
    }

#if other1
using System;
using System.IO;
using System.Linq;

#nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var nkm = sr.ReadLine().Split(' ').Select(Int64.Parse).ToArray();
        var n = nkm[0];
        var k = nkm[1];
        var m = nkm[2];

        var fac = new long[m];
        fac[0] = 1;
        for (var idx = 1; idx < m; idx++)
            fac[idx] = (idx * fac[idx - 1]) % m;

        var facinv = new long[m];
        facinv[^1] = FastPow(fac[^1], m - 2, m);
        for (var idx = m - 2; idx >= 0; idx--)
            facinv[idx] = ((idx + 1) * facinv[idx + 1]) % m;

        var comb = 1L;
        while (true)
        {
            if (n == 0 && k == 0)
                break;

            var nmod = n % m;
            var kmod = k % m;

            if (nmod < kmod)
            {
                comb = 0L;
                break;
            }
            else
            {
                var c = fac[nmod] * facinv[nmod - kmod] * facinv[kmod];
                comb = (comb * c) % m;
            }

            n /= m;
            k /= m;
        }

        sw.WriteLine(comb);
    }

    private static long FastPow(long a, long p, long mod)
    {
        var rv = 1L;

        while (p > 0)
        {
            if (p % 2 == 1)
                rv = (rv * a) % mod;

            a = (a * a) % mod;
            p /= 2;
        }

        return rv;
    }
}
#elif other2
var arr = Console.ReadLine()!.Split(' ');
var a = new int[2001, 2001];
var b = new (int, int)[60];
var c = (long.Parse(arr[0]), long.Parse(arr[1]));
long d = 0, e = 1;
int mod = int.Parse(arr[2]);
if (c.Item1 / 2 < c.Item2) c.Item2 = c.Item1 - c.Item2;
while (c.Item1 > 0)
{
    b[d] = ((int)(c.Item1 % mod), (int)(c.Item2 % mod));
    c = (c.Item1 / mod, c.Item2 / mod);
    d++;
}
for (int i = 0; i < d; i++)
{
    e *= com(b[i].Item1, b[i].Item2);
}
Console.WriteLine($"{e % mod}");
int com(int n, int k)
{
    if (n < k) return 0;
    if (n / 2 < k) return com(n, n - k);
    if (k == 0 || k == n) { return a[n, k] = 1; }
    if (a[n, k] > 0) return a[n, k];
    return a[n, k] = (com(n - 1, k) + com(n - 1, k - 1)) % mod;
}
#endif
}
