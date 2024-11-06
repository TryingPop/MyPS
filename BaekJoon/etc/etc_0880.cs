using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 15
이름 : 배성훈
내용 : 연속 XOR
    문제번호 : 25306번

    수학 문제다
    아이디어는 다음과 같다
    임의의 정수 x에 대해 x ^ x = 0이다

    0에서 A - 1 까지 비트연산결과 chk1,
    0에서 B까지 비트연산결과 chk2라 할때
    A에서 B까지 비트연산은 chk1 ^ chk2와 같다
    
    이후 자리별로 0, 1인지 확인했다
    다른 사람 풀이와 정답을 보니, 4단위로 규칙이 있었다
*/

namespace BaekJoon.etc
{
    internal class etc_0880
    {

        static void Main880(string[] args)
        {

            long a, b;

            Solve();
            void Solve()
            {

                Init();

                long chk1 = GetXOR(a - 1);
                long chk2 = GetXOR(b);

                Console.Write(chk1 ^ chk2);
            }

            void Init()
            {

                string[] temp = Console.ReadLine().Split();

                a = long.Parse(temp[0]);
                b = long.Parse(temp[1]);
            }

            long GetXOR(long _num)
            {

                long ret = 0;
                long chk = _num / 2;
                if (_num % 2 == 1) chk++;
                if (chk % 2 == 1) ret = 1;


                for (int i = 1; i < 60; i++)
                {

                    chk = _num % (1L << i + 1);
                    chk -= 1L << i;
                    chk++;

                    if (chk <= 0 || chk % 2 == 0) continue;
                    ret |= 1L << i;
                }

                return ret;
            }
        }
    }

#if other
using System;

public class Program
{
    static void Main()
    {
        string[] ab = Console.ReadLine().Split(' ');
        long a = long.Parse(ab[0]), b = long.Parse(ab[1]);
        Console.Write(XOR(a - 1) ^ XOR(b));
    }
    static long XOR(long n)
    {
        return (n % 4) switch
        {
            0 => n,
            1 => 1,
            2 => n + 1,
            3 => 0
        };
    }
}
#elif other2
// #include <cstdio>
using l = long long;
l A, B, m;
l S(l n)
{
    
    l u[] = { n, 1, n+1, 0 };
    m = n & 3;
    return u[m];
}

int main()
{

    scanf("%lld%lld", &A, &B);
    l r = S(A-1) ^ S(B);
    printf("%lld", r);
    return 0;
}
#endif
}
