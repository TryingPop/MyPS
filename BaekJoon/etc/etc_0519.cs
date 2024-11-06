using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 13
이름 : 배성훈
내용 : RSA
    문제번호 : 13618번

    수학, 정수론, 분할 정복을 이용한 거듭제곱, 확장 유클리드 호제법, 오일러 피함수 문제다
    대문자 N에서 갑자기 n이 나와 당황하고 m = c^d(mod n) 을 구하는게 아닌가? 의문을 품고
    음수 문제로 예제가 안맞자 곱셈으로 해서 2번 틀렸다
    n == N으로 보면 매우 친절한 문제라 지문대로만 따라가면 풀린다

    아이디어는 다음과 같다
    Z_ep(n) 곱셈 군에서 e의 역원 d를 찾아야한다, 여기서 ep는 오일러 피함수이다
    이는 유클리드 알고리즘을 이용해 d를 찾았다 (이걸 찾아서 풀라고 알려줘서 매우 친절한 문제다!)

    그리고 c = m^e(mod n) 인 m을 찾는 것인데,
    (c)^d = (m^e)^d = m^(e * d) = m(mod n)
    이므로, m = c^d(mod n)연산을 해주면된다
    여기서 c, d, n을 알고 있으므로 제곱 해주면 이상없이 통과한다

    문제에서는 n이 대문자 N과 함께 쓰여 변수를 하나 무시했나 하고 혼동이 왔고 이에 문제를 분석하느라 시간을 꽤 소요했다
    이후에는 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0519
    {

        static void Main519(string[] args)
        {

            Read(out int n, out int e, out int c);

            int p = GetPrime(n);
            int q = n / p;
            int pN = (p - 1) * (q - 1);
            int d = GetMulInv(pN, e);
            int ret = GetPow(c, d, n);

            Console.WriteLine(ret);

            int GetPrime(int _n)
            {

                for (long i = 2; i <= _n; i++)
                {

                    if (_n < i * i) break;
                    if (_n % i != 0) continue;

                    return (int)i;
                }

                return -1;
            }

            int GetPow(long _bot, int _exp, int _div)
            {

                long calc = 1;

                while(_exp > 0)
                {

                    if ((_exp & 1) == 1) 
                    { 
                        
                        calc = (calc * _bot) % _div; 
                    }

                    _bot = (_bot * _bot) % _div;
                    _exp /= 2;
                }

                int ret = (int)calc;
                return ret;
            }

            int GetMulInv(int _n, int _e)
            {

                int save = _n;
                int s1 = 1;
                int s2 = 0;

                int t1 = 0;
                int t2 = 1;

                int q;
                int temp;
                while(_e > 0)
                {

                    temp = _n % _e;
                    q = (_n - temp) / _e;

                    _n = _e;
                    _e = temp;

                    temp = -q * s2 + s1;
                    s1 = s2;
                    s2 = temp;

                    temp = -q * t2 + t1;
                    t1 = t2;
                    t2 = temp;
                }

                if (t1 < 0) t1 += save;
                return t1;
            }

            void Read(out int _n, out int _e, out int _c)
            {

                string[] temp = Console.ReadLine().Split();
                _n = int.Parse(temp[0]);
                _e = int.Parse(temp[1]);
                _c = int.Parse(temp[2]);
            }
        }
    }

#if other
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Diagnostics;

//Stopwatch swtotal = new();

// 확인용
//int N = 9103 * 9109; //1039 * 1049;
//int e = 15842891; //d = 73759163
//int encrypt = 34866009;

int[] numbers = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
int N = numbers[0];
int e = numbers[1];
int encrypt = numbers[2];
int decrypt = 0;
int recrypt = 0;

int p = 0;
int q = 0;

for (int i = 3; i <= N; i += 2)
{
    if (N % i == 0)
    {
        if (p == 0)
            p = i;
        else
        {
            q = i;
            break;
        }
    }
}

//Console.WriteLine("p,q = {0}, {1}", p, q);

int piN = (p - 1) * (q - 1);

int s = 0;
int t = 0;
EuclidExtend(piN, e, out s, out t);
int d = piN + t;

//Console.WriteLine("d = {0}", d);

//decrypt = FastRSA(encrypt, d, N);
//Console.WriteLine(decrypt);

Console.WriteLine(FastFastRSA(encrypt, d, N));



//recrypt = FastRSA(decrypt, e, N);
//if (encrypt == recrypt)
//{
//    Console.WriteLine("{0} -> {1} -> {2}", encrypt, decrypt, recrypt);
//    Console.WriteLine(decrypt);
//    return;
//}







// FastRSA 원리
// C = M^e % N    (e = e1 * e2)
// C = M^(e1*e2) % N
// C = (M^e1 % N)^e2 % N
int FastRSA(int number, int power, int mod)
{
    // List 채우기
    List<int> list = new List<int>();
    int n = power;
    for (int i = 2; i <= n; i++)
    {
        if (i > Math.Sqrt(n))
        {
            // 남은 부분은 prime Number
            list.Add(n);
            break;
        }
        if (n % i == 0)
        {
            list.Add(i);
            n /= i--; // n/=i   i-=1 한 줄에
        }
    }

    // 내용 확인하고
    //foreach (var item in list)
    //    Console.Write("{0}  ",item);

    // 따로 계산
    foreach (var item in list)
        number = (int)(BigInteger.Pow(number, item) % N);

    return number;
}

// 더 빨라진 나머지 연산
// O(N) -> O(logN)
long FastFastRSA(int number, int power, int mod)
{
    // 지수를 10자리수로 나눈다
    List<int> powlist = new List<int>();
    while(power>0)
    {
        powlist.Add(power % 10);
        power /= 10;
    }

    // 나머지 모으는 리스트
    List<long> modlist = new List<long>();
    long num = number;
    long num10 = number;
    foreach (var item in powlist)
    {
        num = num10;
        for (int i = 1; i <= 9; i++)
        {
            if (i == item)
            {
                //Console.WriteLine(num10);
                modlist.Add(num10);
            }
            num10 = num10 * num % mod;
        }
    }

    num = 1;
    foreach (var item in modlist)
        num = num * item % mod;


    return num;
}

int EuclidExtend(int a, int b, out int s, out int t)
{
    int p;
    int r;
    int s1 = 1;
    int s2 = 0;
    int t1 = 0;
    int t2 = 1;

    if (a < b)
        (a, b) = (b, a);
    while (b > 0)
    {
        p = a / b;
        r = a % b;

        a = b;
        b = r;

        s = s1 - p * s2;
        s1 = s2;
        s2 = s;

        t = t1 - p * t2;
        t1 = t2;
        t2 = t;
    }
    s = s1;
    t = t1;
    return a;
}
#elif other2
N,E,C=map(int,input().split())
P=2
while N%P: P+=1
print(pow(C,pow(E,-1,(P-1)*(N//P-1)),N))
#endif
}
