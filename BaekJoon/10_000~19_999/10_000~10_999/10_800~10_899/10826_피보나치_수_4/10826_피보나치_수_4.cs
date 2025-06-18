using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Data.Common;

/*
날짜 : 2024. 3. -
이름 : 배성훈
내용 : 피보나치 수 4
    문제번호 : 10826번
  
    dp, 큰 수 연산 문제다
    BigInteger로 해결했다
    이후 덧셈에만 쓸 수 있는 자료를 만들어 struct를 만들어 했다
    속도는 96ms로 8ms 정도 느리다;

    일단, 자리수 연산(초기화 부분)에서 1번 틀리고, 
    이후 출력 부분에서 9자리 안맞춰서 2번 더 틀렸다
    이후에는 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0379
    {

        static void Main379(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
#if first

            BigInteger[] fibo2 = new BigInteger[Math.Max(n + 1, 2)];
            fibo2[0] = 0;
            fibo2[1] = 1;
            for (int i = 2; i <= n; i++)
            {

                fibo[i] = fibo[i - 2] + fibo[i - 1];
            }
#else

            MyInt[] fibo = new MyInt[Math.Max(n + 1, 2)];
            fibo[0].Set(0);
            fibo[1].Set(1);

            for (int i = 2; i <= n; i++)
            {

                fibo[i] = fibo[i - 2] + fibo[i - 1];
            }
#endif
            Console.WriteLine(fibo[n]);
        }

        struct MyInt
        {

            private static StringBuilder sb = new(10_000);
            private static int LEN = 250;
            private int[] arr;
            private int digit;

            public MyInt()
            {

                arr = new int[LEN];
                digit = 0;
            }

            public static MyInt operator +(MyInt _f, MyInt _b)
            {

                int digit = Math.Max(_f.digit, _b.digit);

                MyInt ret = new();
                ret.digit = digit;

                for (int i = 0; i <= digit; i++)
                {

                    ret[i] = _f[i] + _b[i];
                }

                ret.SetDigit();
                return ret;
            }

            public void Set(int _n)
            {

                arr = new int[LEN];
                for (int i = 1; i <= digit; i++)
                {

                    arr[i] = 0;
                }

                digit = 0;
                this[0] = _n;
            }

            private int this[int _idx]
            {

                get 
                {

                    return arr[_idx]; 
                }
                set 
                {

                    arr[_idx] = value;
                }
                    
            }

            private void SetDigit()
            {

                int MOD = 1_000_000_000;
                for (int i = 0; i <= digit; i++)
                {

                    if (arr[i] < MOD) continue;
                    int add = arr[i] / MOD;
                    arr[i] = arr[i] % MOD;
                    arr[i + 1] += add;
                    digit = Math.Max(digit, i + 1);
                }
            }

            public override string ToString()
            {

                sb.Append(arr[digit]);
                for (int i = digit - 1; i >= 0; i--)
                {

                    sb.Append(arr[i].ToString("D9"));
                }

                string ret = sb.ToString();
                sb.Clear();
                return ret;
            }
        }
    }

#if other
using System;
using System.Linq;
using System.Text;

public class StringInt
{
    private Char[] _chars;

    private StringInt(Char[] chars)
    {
        _chars = chars;
    }
    public StringInt(int v)
    {
        _chars = v.ToString().ToCharArray();
    }

    public static StringInt operator +(StringInt lhs, StringInt rhs)
    {
        var sb = new StringBuilder();

        bool hasCarry = false;
        for (var idx = 0; hasCarry || idx < Math.Max(lhs._chars.Length, rhs._chars.Length); idx++)
        {
            var vlhs = idx < lhs._chars.Length ? lhs._chars[lhs._chars.Length - 1 - idx] - '0' : 0;
            var vrhs = idx < rhs._chars.Length ? rhs._chars[rhs._chars.Length - 1 - idx] - '0' : 0;

            var sum = vlhs + vrhs + (hasCarry ? 1 : 0);
            hasCarry = false;

            if (sum >= 10)
            {
                hasCarry = true;
                sum -= 10;
            }

            sb.Append((Char)('0' + sum));
        }

        return new StringInt(sb.ToString().TrimEnd('0').Reverse().ToArray());
    }

    public override string ToString()
    {
        return new string(_chars);
    }
}

public static class Program
{
    public static void Main()
    {
        var n = Int64.Parse(Console.ReadLine());

        var arr = new StringInt[10001];
        arr[0] = new StringInt(0);
        arr[1] = new StringInt(1);

        for (var idx = 2; idx < arr.Length; idx++)
            arr[idx] = arr[idx - 1] + arr[idx - 2];

        Console.WriteLine(arr[n]);
    }
}

#endif
}
