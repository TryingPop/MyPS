using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 16
이름 : 배성훈
내용 : Alice and Bob
    문제번호 : 12902번

    수학, 정수론, 유클리드 호제법 문제다
    아이디어는 다음과 같다
    진행 방식을 보면, 서로 다른 x, y에 대해 
    abs(x - y)를 뺀 값을 찾아야 한다

    이렇게 존재하지 않는 경우가 나올때 까지 채운다
    이렇게 끝나는 값은 유클리드 호제법으로 찾은 gcd와 같다

    그리고 중간 과정에서 나오는 값이나 나중 값들은 
    모두 gcd로 나눠떨어진다
    그래서 나올 수 있는 수들의 개수를 알 수 있다

    이로 홀수면 Alice 승리, 짝수면 Bob 승리를 내릴 수 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0883
    {

        static void Main883(string[] args)
        {

            StreamReader sr;
            int n;
            int[] arr;
            int len, gcd;

            Solve();
            void Solve()
            {

                Input();

                Cnt();

                Console.Write(len % 2 == 1 ? "Alice" : "Bob");
            }

            void Cnt()
            {

                gcd = arr[0];
                for (int i = 1; i < n; i++)
                {

                    gcd = GetGCD(gcd, arr[i]);
                }

                len = (len / gcd) - n;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                len = 0;
                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    arr[i] = cur;
                    if (len < arr[i]) len = arr[i];
                }

                sr.Close();
            }

            int GetGCD(int _a, int _b)
            {

                while(_b > 0)
                {

                    int temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
using ProblemSolving.Templates;
using System;
using System.IO;
using System.Linq;
namespace ProblemSolving.Templates {}
namespace System {}
namespace System.IO {}
namespace System.Linq {}

#nullable disable

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        Solve(sr, sw);
    }

    public static void Solve(StreamReader sr, StreamWriter sw)
    {
        var n = Int32.Parse(sr.ReadLine());
        var seq = sr.ReadLine().Split(' ').Select(Int64.Parse).ToArray();

        var max = seq.Max();
        var g = seq.Aggregate((l, r) => NumberTheory.GCD(l, r));

        var available = max / g;
        sw.WriteLine((available - n) % 2 == 0 ? "Bob" : "Alice");
    }
}


namespace ProblemSolving.Templates
{
    public static class NumberTheory
    {
        public static long GCD(long x, long y)
        {
            while (x > 0 && y > 0)
                if (x > y) x %= y;
                else y %= x;

            return Math.Max(x, y);
        }

        /// <summary>
        /// Returns {1+p+..p^(n-1)} % mod
        /// </summary>
        public static long GeometricSum(long p, long n, long mod)
        {
            p %= mod;

            if (n <= 0)
                throw new ArgumentException();

            if (n == 1)
                return 1;
            if (n == 2)
                return (1 + p) % mod;

            if (n % 2 == 0)
            {
                // 1+p+...+p^{2k-1} = (1+p^k)(1+p+...+p^{k-1})
                var k = n / 2;

                return (1 + FastPow(p, k, mod)) * GeometricSum(p, k, mod) % mod;
            }
            else
            {
                // 1+p+...+p^{n-1} = 1 + p(1+p+...+p^{n-2})

                return (1 + p * GeometricSum(p, n - 1, mod)) % mod;
            }
        }

        public static long FastPow(long b, long p, long mod)
        {
            var rv = 1L;
            while (p != 0)
            {
                if ((p & 1) == 1)
                    rv = rv * b % mod;

                b = b * b % mod;
                p >>= 1;
            }

            return rv;
        }
    }
}

// This is source code merged w/ template
// Timestamp: 2024-06-24 17:11:55 UTC+9
#elif other2
// #include<bits/stdc++.h>
using namespace std;
typedef long long ll;
ll gcd(ll a,ll b){
    if(b==0)return a;
    return gcd(b,a%b);
}
typedef long long ll;
int main(){
    int n,i;
    cin>>n;
    vector<ll> a(n);
    ll g=0,mx=-1;;
    for(i=0;i<n;i++){
        cin>>a[i];
        if(mx<a[i])mx=a[i];
        g=gcd(a[i],g);
    }
    cout<<((mx/g-n)%2?"Alice":"Bob");
}

#endif
}
