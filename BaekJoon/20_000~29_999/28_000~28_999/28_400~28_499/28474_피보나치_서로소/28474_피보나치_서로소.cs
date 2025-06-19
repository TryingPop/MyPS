using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 15
이름 : 배성훈
내용 : 피보나치 서로소
    문제번호 : 28474번

    수학, 오일러 피 함수 문제다.
    F(n)을 n번째 피보나치 수라 하자.
    그러면 gcd(F(i), F(j)) = F(gcd(i, j))와 같다.

    여기 문제에선 gcd(i, j) = 1일 때, i와 j가 서로소라고 정의했다.
    i와 서로 소의 갯수는 오일러 피 함수의 값으로 정의했음을 정수론에서 배웠고,
    이는 홀수인 원소들을 세는 것이다.

    오일러 피함수를 Φ로 표현하자.
    그러면 gcd(n, m) = 1일 때, Φ(n x m) = Φ(n) x Φ(m)이고,
    소수 p에 대해 Φ(p^k) = p^(k - 1) x (p - 1)이 된다.(체이론)

    오일러 피함수에서 피 연산을 소수로만 나눠서 비교하면 되기에 
    에라토스테네스의 체로 제곱해서 10억 이하인 소수를 모두 찾았다.

    이 사실을 이용하면 i, j의 서로 소인 경우 F(j)는 F(i)와 서로 소가 된다.
    즉 Φ(i)를 세면 된다.
    그리고 F(2) = 1이므로 gcd(F(i), F(j)) = F(gcd(i, j)) = F(2)인 경우도 될 수 있다.
    이는 gcd(i, j) = 2인 경우 이므로 i는 2로 나눠떨어진다.
    i가 짝수인 경우 Φ(i/2)를 추가해주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1547
    {

        static void Main1547(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int[] primes;
            int t = ReadInt();

            SetPrime();

            while (t-- > 0)
            {

                int n = ReadInt();
                int ret = GetEuler(n);
                if ((n & 1) == 0) ret += GetEuler(n >> 1);
                sw.Write(ret);
                sw.Write('\n');
            }

            // 오일러 피함수 값 찾기
            int GetEuler(int _n)
            {

                // 1은 반례 처리
                if (_n == 1) return 0;

                int ret = 1;
                for (int i = 0; i < primes.Length && primes[i] <= _n && _n != 1; i++)
                {

                    if (_n % primes[i] != 0) continue;

                    // 소수로 나눠지는 경우
                    _n /= primes[i];
                    int mul = primes[i] - 1;
                    // 멱(pow) 찾기
                    while (_n % primes[i] == 0)
                    {

                        _n /= primes[i];
                        mul *= primes[i];
                    }

                    ret *= mul;
                }

                // 소수라 남아있는 경우
                // 제곱승은 1일 수 밖에 없다.
                if (_n != 1) ret *= _n - 1;

                return ret;
            }

            SetPrime();

            // 소수 찾기
            void SetPrime()
            {

                int MAX_PRIME = 31_607;
                int MAX_LEN = 3_401;
                bool[] notPrime = new bool[MAX_PRIME + 1];

                int cnt = 0;
                for (int i = 2; i < notPrime.Length; i++)
                {

                    if (notPrime[i]) continue;
                    cnt++;
                    for (int j = i << 1; j < notPrime.Length; j += i)
                    {

                        notPrime[j] = true;
                    }
                }

                primes = new int[MAX_LEN];
                int len = 0;
                for (int i = 2; i < notPrime.Length; i++)
                {

                    if (notPrime[i]) continue;
                    primes[len++] = i;
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

                    while ((c = sr.Read()) != ' ' && c != '\n' && c != -1)
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
using System;
using System.IO;

// #nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var t = Int32.Parse(sr.ReadLine());
        while (t-- > 0)
        {
            var x = Int32.Parse(sr.ReadLine());

            if (x == 1)
                sw.WriteLine(0);
            else if (x == 2)
                sw.WriteLine(1);
            else if (x % 2 == 0)
                sw.WriteLine(Phi(x) + Phi(x / 2));
            else
                sw.WriteLine(Phi(x));
        }
    }

    private static long Phi(long x)
    {
        var sqrt = (long)Math.Sqrt(x);
        var phi = x;

        for (var p = 2; p < 2 + sqrt; p++)
            if (x % p == 0)
            {
                phi /= p;
                phi *= p - 1;

                while (x % p == 0)
                    x /= p;
            }

        if (x != 1)
        {
            phi /= x;
            phi *= x - 1;
        }

        return phi;
    }
}

#endif
}
