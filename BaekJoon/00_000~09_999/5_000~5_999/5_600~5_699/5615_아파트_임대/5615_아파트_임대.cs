using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 11
이름 : 배성훈
내용 : 아파트 임대
    문제번호 : 5615번

    라빈 밀러 알고리즘으로 푸는 문제다
    주된 아이디어는 귀류법과 페르마 소정리, 그리고 Zp가 체임을 이용한다

    먼저 N이 소수라고 가정한다
    그러면 N > 2이면 N - 1은 짝수이고, N - 1 = d * 2^r인 적당한 정수 d와 r이 존재한다 여기서 d는 홀수
    a를 소수라하자

    그러면 페르마 소정리로 a^(N - 1) = 1(mod N) 이다 
    또한, a^(N - 1) = a^(d * 2^r)로 표현 가능

    여기서, Zp가 체이므로
    a^(d*2^(r - 1)) = 1 or a^(d*2^(r - 1)) = -1(mod N)이다

    만약 여기서 1또는 -1이 아니라면 N이 소수라는 가정에서 생긴 모순이므로 N은 합성수가된다

    그리고 a^(d*2^(r -1)) = -1인 경우 더 판정이 안되어 탈출한다 -> 아직까지는 N이 합성수인지 소수인지 모른다
    반면 a^(d*2^(r - 2) * 2) =  a^(d * 2^(r -1)) 이므로

    앞과 같이 a^(d * 2^(r - 2)) = 1 or -1(mod N)인지 확인한다
    이를 r - ?가 0이될때까지 계속해서 판별한다

    중간에 a^(d * 2^x) != 1 or -1인 경우로 탈출하면 합성수라 내리고 끝내면 된다
    반면, 진행해서 1 또는 -1에서 판별 못하면 a를 바꾼다

    a를 몇 번 바꿔서 검출 안된다면 ??보다 작은 수에 대해서는 소수라 말할 수 있다
    여기서 a = 2, 7, 61에서 검증했을 때 합성수로 판별안된다면 2^64 이하인 수에서는 소수임이 밝혀져 있다

    그래서 ulong[] chk = { 2, 7, 61 }로 잡는다
    다른사람의 풀이에서는 chk 를 가장 작은 12개의 소수로 잡았다 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37
*/

namespace BaekJoon.etc
{
    internal class etc_0201
    {

        static void Main201(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            // 해당 수에서 검증 안되면, 2^64 밑에서는 소수이다!
            ulong[] chk = { 2, 7, 61 };
            int ret = 0;
            for (int i = 0; i < n; i++)
            {

                ulong temp = (ulong)ReadInt(sr);
                temp = 2 * temp + 1;

                if (IsPrime(temp, chk)) ret++;
            }
            sr.Close();

            Console.WriteLine(ret);
        }

        static ulong Pow(ulong _x, ulong _y, ulong _mod)
        {

            // 큰 수의 거듭제곱 계산
            // 이항 계수? 에서 실행한 방법
            ulong temp = 1;
            _x %= _mod;

            while(_y > 0)
            {

                if ((_y & 1) == 1) temp = (temp * _x) % _mod;
                _y /= 2;
                _x = (_x * _x) % _mod;
            }

            return temp;
        }

        static bool Chk(ulong _n, ulong _a)
        {

            if (_n <= 1) return false;
            // 2는 여기서 따로 판정
            if (_n == 2) return true;

            // a가 소수로 했기에 true
            if (_n == _a) return true;

            ulong k = _n - 1;

            while (true)
            {

                // 2^(d * 2^r) 값을 찾는다
                ulong temp = Pow(_a, k, _n);

                // 1인지 -1인지 확인
                // -1인 경우 탈출
                if (temp == _n - 1) return true;
                // 1이 아니면 합성수 이므로 바로 탈출
                else if (temp != 1) return false;

                // 모두 다 나눈 경우 1인지 -1인지 확인하고 탈출
                if ((k & 1) == 1) return temp == 1 || temp == _n - 1;
                k /= 2;
            }
        }

        static bool IsPrime(ulong _n, ulong[] _chk)
        {

            for (int i = 0; i < _chk.Length; i++)
            {

                if (!Chk(_n, _chk[i])) return false;
            }

            return true;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c;
            int ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
#nullable disable

using System;
using System.IO;

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var count = 0;

        while (n-- > 0)
        {
            // A = 2xy+x+y = 1/2(x+1)(y+1) - 1/2
            // 2A+1 = (x+1)(y+1)
            var a = UInt64.Parse(sr.ReadLine());

            if (MillerRabin(2 * a + 1))
                count++;
        }

        sw.WriteLine(count);
    }

    public static ulong FastPow(ulong a, ulong v, ulong mod)
    {
        var rv = 1UL;

        while (v > 0)
        {
            if ((v & 1) == 1)
                rv = (rv * a) % mod;

            a = (a * a) % mod;
            v /= 2;
        }

        return rv;
    }
    public static bool MillerRabin(ulong p)
    {
        if (p == 1)
            return false;

        var q = p - 1;
        var k = 0;
        while (q % 2 == 0)
        {
            q /= 2;
            k++;
        }

        foreach (var a in new ulong[] { 2, 7, 61 })
        {
            if (a >= p)
                break;

            var aq = FastPow(a, q, p);
            if (aq == 1 || aq == p - 1)
                continue;

            var pass = false;
            for (var step = 0; step < k; step++)
            {
                aq = (aq * aq) % p;
                if (aq == p - 1)
                {
                    pass = true;
                    break;
                }
            }

            if (!pass)
                return false;
        }

        return true;
    }
}
#elif other2
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//----------------

namespace BaekjoonStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            using StreamReader sr = new StreamReader(Console.OpenStandardInput());

            int testCount = int.Parse(sr.ReadLine());
            int count = 0;
            for (; testCount > 0; testCount--)
            {
                ulong r = ulong.Parse(sr.ReadLine());
                if (IsPrime(1L+r*2L)) {
                    count++;
                }
            }
            Console.WriteLine(count);
        }
        public static ulong ModPow(ulong b, ulong exp, ulong mod)
        {
            b %= mod;
            ulong y = 1;
            while (exp != 0L)
            {
                if ((exp & 1L) == 1L)
                {
                    y = (y * b) % mod;
                }
                b = (b * b) % mod;
                exp >>= 1;
            }
            return y;
        }
        static bool MillerRabinTest(ulong a, ulong p)
        {
            if (a % p == 0)
            {
                return false;
            }
            ulong exp = (p - 1) / 2;
            while (true)
            {
                ulong y = ModPow(a, exp, p);
                if (y == p - 1)
                {
                    return true;
                }
                if ((exp & 1) == 1)
                {
                    return y == 1;
                }
                exp >>= 1;
            }
        }
        public static bool IsPrime(ulong p)
        {
            if ((p & 1) == 0)
            {
                return p == 2;
            }
            ulong[] aList = new ulong[] {2,3,61};
            for (int i = 0; i < aList.Length; i++)
            {
                if (p == aList[i])
                {
                    return true;
                }
                if (!MillerRabinTest(aList[i], p))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
#endif
}
