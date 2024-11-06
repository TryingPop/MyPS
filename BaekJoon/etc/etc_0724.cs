using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 25
이름 : 배성훈
내용 : 이항 계수 5

    소수판정, 에라토스 테네스의 체, 분할 정복을 이용한 거듭제곱 문제다
    오버플로우로 3번 틀렸다;

    아이디어는 다음과 같다 n이하의 모든 소수primes를 찾는다
    그리고, n C k에 대해 primes의 원소에 대해 각각 쓰인 개수를 구한다
    그리고 해당 원소를 거듭제곱하며 mod연산을 하고 곱해준다
    
    다른 사람 풀이를 보니 소수를 찾자마자 해도 되는거 같다
    이렇게 바꾸니 100ms로 나온다
*/

namespace BaekJoon.etc
{
    internal class etc_0724
    {

        static void Main724(string[] args)
        {

            int n, k, mod;

#if first
            int[] primes;
            int len;
            long ret;

            Solve();

            void Solve()
            {

                Read();

                if (k == 0)
                {

                    Console.Write(1);
                    return;
                }
                else if (k == 1)
                {

                    ret = n % mod;
                    Console.Write(ret);
                    return;
                }

                SetPrime();

                GetRet();

                Console.Write(ret);
            }

    

            void SetPrime()
            {

                bool[] notPrime = new bool[n + 1];

                primes = new int[n + 1];
                len = 0;
                for (int i = 2; i <= n; i++)
                {

                    if (notPrime[i]) continue;
                    primes[len++] = i;

                    for (int j = 2 * i; j <= n; j += i)
                    {

                        notPrime[j] = true;
                    }
                }
            }

            void GetRet()
            {

                ret = 1;

                for (int i = 0; i < len; i++)
                {

                    long cnt = 0;
                    long cur = primes[i];
                    while (cur <= n)
                    {

                        cnt += (n / cur) - (k / cur) - ((n - k) / cur);
                        cur *= primes[i];
                    }

                    ret = (ret * GetPow(primes[i], cnt)) % mod;
                }
            }

            long GetPow(long _n, long _exp)
            {

                long ret = 1;
                _n %= mod;

                while (_exp > 0)
                {

                    if ((_exp & 1) == 1) ret = (ret * _n) % mod;

                    _n = (_n * _n) % mod;
                    _exp /= 2;
                }

                return ret;
            }
#else

            long MIN = int.MaxValue;
            Solve();
            void Solve()
            {

                Read();
                long ret = 1;

                bool[] notPrime = new bool[n + 1];
                for (int i = 2; i <= n; i++)
                {

                    if (notPrime[i]) continue;
                    int cnt = 0;
                    int j = i;
                    while(true)
                    {

                        cnt += n / j - k / j - (n - k) / j;
                        if (n < 1L * j * i) break;
                        j *= i;
                    }

                    ret = (ret * GetPow(i, cnt)) % mod;

                    for (j = 2 * i; j <= n; j += i)
                    {

                        notPrime[j] = true;
                    }
                }

                Console.Write(ret);
            }

            long GetPow(long _n, int _exp)
            {

                long ret = 1;
                _n %= mod;

                while (_exp > 0)
                {

                    if ((_exp & 1) == 1) ret = (ret * _n) % mod;

                    _n = (_n * _n) % mod;
                    _exp /= 2;
                }

                return ret;
            }
#endif



            void Read()
            {

                string[] temp = Console.ReadLine().Split();

                n = int.Parse(temp[0]);
                k = int.Parse(temp[1]);
                k = Math.Min(n - k, k);
                mod = int.Parse(temp[2]);
            }
        }
    }

#if other

using System;
using System.IO;
using System.Linq;

#nullable disable

public static class DeconstructHelper
{
    public static void Deconstruct<T>(this T[] arr, out T v1, out T v2) => (v1, v2) = (arr[0], arr[1]);
    public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3) => (v1, v2, v3) = (arr[0], arr[1], arr[2]);
    public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4) => (v1, v2, v3, v4) = (arr[0], arr[1], arr[2], arr[3]);
    public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5) => (v1, v2, v3, v4, v5) = (arr[0], arr[1], arr[2], arr[3], arr[4]);
    public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6) => (v1, v2, v3, v4, v5, v6) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5]);
    public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6, out T v7) => (v1, v2, v3, v4, v5, v6, v7) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6]);
    public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6, out T v7, out T v8) => (v1, v2, v3, v4, v5, v6, v7, v8) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6], arr[7]);
}

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var (n, k, m) = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var sieve = new long[4_000_001];
        for (var p = 2L; p < sieve.Length; p++)
            if (sieve[p] == 0)
            {
                for (var mult = p * p; mult < sieve.Length; mult += p)
                    sieve[mult] = p;
            }

        var comb = new int[4_000_001];

        for (var idx = 2; idx <= n; idx++)
            comb[idx]++;

        for (var idx = 2; idx <= k; idx++)
            comb[idx]--;

        for (var idx = 2; idx <= (n - k); idx++)
            comb[idx]--;

        for (var idx = comb.Length - 2; idx >= 0; idx--)
        {
            if (sieve[idx] == 0)
                continue;

            var v = comb[idx];
            var p = sieve[idx];
            var rem = idx / p;

            comb[idx] = 0;
            comb[p] += v;
            comb[rem] += v;
        }

        var result = 1L;
        for (var p = 2; p < comb.Length; p++)
        {
            if (comb[p] == 0)
                continue;

            var prod = 1L;
            var pow = comb[p];
            var ppow = p;
            while (pow > 0)
            {
                if (pow % 2 == 1)
                    prod = prod * ppow % m;

                ppow = ppow * ppow % m;
                pow /= 2;
            }

            result = prod * result % m;
        }

        sw.WriteLine(result);
    }
}

#elif other2

// #include <iostream>
using namespace std;
int N, K, M;
bool chk[4000001];
long long ans = 1;
int main() {
    for (int i = 2; i * i < 4000001; i++)
        if (!chk[i]) for (int j = i * i; j < 4000001; j += i) chk[j] = 1;
    cin >> N >> K >> M;
    for (int i = 2; i <= N; i++) {
        if (chk[i]) continue;
        int num = 0;
        for (long long j = i; j <= N; j *= i) num += (N / j - K / j - (N - K) / j);
        for (int j = 0; j < num; j++) ans = ans * i % M;
    }
    cout << ans;
    return 0;
}
#elif other3
import java.io.*;
import java.math.*;
import java.util.*;
import java.util.regex.*;
import java.util.stream.*;

public class Main {
    static BufferedReader br;
    static BufferedWriter bw;
    static StringTokenizer st;

    static int cnt(int n, int p) {
        int k=0;
        while (n>0) {
            k += n/p;
            n /= p;
        }
        return k;
    }

    static long fast_mul(int a, int b, int m) {  // a^b
        long ans = 1;
        if(a == 1 || b == 0) {
            return 1;
        }
        while(b > 0) {
            if(b % 2 == 1) {
                ans *= a;
                ans %= m;
            }
            a *= a;
            a %= m;
            b /= 2;
        }
        return ans;
    }

    static long c(int n, int m, int mod) {
        long ans = 1;
        boolean[] check = new boolean[n+1];
        Arrays.fill(check, true);
        for (int i=2; i<=n; i++) {
            if (check[i]) {
                for (int j=2*i; j<=n; j+=i) {
                    check[j] = false;
                }
                int k = cnt(n,i) - cnt(m,i) - cnt(n-m,i);
                ans = ans * fast_mul(i, k, mod);
                ans = ans % mod;
            }
        }
        return ans;
    }

    public static void main(String[] args) throws Exception {
        //br = new BufferedReader(new FileReader("../input.txt"));
        br = new BufferedReader(new InputStreamReader(System.in));
        bw = new BufferedWriter(new OutputStreamWriter(System.out));

        st = new StringTokenizer(br.readLine());
        int N = Integer.parseInt(st.nextToken());
        int K = Integer.parseInt(st.nextToken());
        int M = Integer.parseInt(st.nextToken());


        long res = c(N, K, M);

        bw.write(res+"");
        bw.newLine();
        bw.flush();
    }
}
#elif other4
int[] line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
int N = line[0];
int R = line[1];
int MOD = line[2]; // 1000000007

if (N - R < R)
    R = N - R;

//System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch(); sw.Start();

List<int> mult = new List<int>();
Dictionary<int, int> prime = new Dictionary<int, int>();
prime.Add(2, 0);
Prime(N);

//sw.Stop(); long st1 = sw.ElapsedMilliseconds; Console.WriteLine("Prime: {0} ms", st1); sw.Reset();sw.Start();

for(int i = 0; i < R; i++)
    Divide(N - i, R - i);

//sw.Stop(); long st2 = sw.ElapsedMilliseconds; Console.WriteLine("Divide: {0} ms", st2); sw.Reset(); sw.Start();

long c = 1;
foreach (var item in prime)
    for (int i = 0; i < item.Value; i++)
        c = (c * item.Key) % MOD;

//sw.Stop(); long st3 = sw.ElapsedMilliseconds; Console.WriteLine("Mulit: {0} ms", st3); 
//Console.WriteLine("Total: {0} ms", st1 + st2 + st3);
Console.WriteLine(c);

return;

void Divide(int up, int down)
{
    foreach (var item in prime.Keys)
    {
        if (up == 1)
            break;
        if (item * item > up)
        {
            prime[up]++;
            break;
        }
        while (up % item == 0)
        {
            prime[item]++;
            up /= item;
        }
    }
    foreach (var item in prime.Keys)
    {
        if (down == 1)
            break;
        if (item * item > down)
        {
            prime[down]--;
            break;
        }
        while (down % item == 0)
        {
            prime[item]--;
            down /= item;
        }
    }
}

void Prime(int max)
{
    for (int i = 3; i <= max; i += 2)
        if (isPrime(i))
            prime.Add(i, 0);
}

bool isPrime(int num)
{
    foreach(var item in prime.Keys)
    {
        if (item * item > num)
            break;
        if (num % item == 0)
            return false;
    }
    return true;
}
#endif
}
