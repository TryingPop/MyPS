using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 29
이름 : 배성훈
내용 : 하노이의 네 탑
    문제번호 : 9942번

    수학, dp 문제다
    https://en.wikipedia.org/wiki/Tower_of_Hanoi#With_four_pegs_and_beyond
    의 Frame–Stewart algorithm 을 구현하면 된다

    n : 링의 수,
    r : 링을 옮길 수 있는 막대기의 수
    T(n, r): r개의 막대기에서 n개의 링을 옮기는 최소 수

    T(n, r) = 2T(k, r) + T(n - k, r - 1)
    이 성립한다고 한다
    여기서 r = 4인 경우 k = n - round(sqrt(2n + 1)) + 1이라 한다

    그리고 3개인 경우 최소는 2^n - 1 임을 알고 있다
    그래서 T(n - k, r - 1) = 2^(n - k) - 1을 대입하면
    T(n, r) = 2T(k, r) + 2^(n - k) - 1, 
    k = n - round(sqrt(2n + 1)) + 1이 된다

    범위가 1000이고, 돌려보니 
    1000안의 수는 long 범위 안에 모두 담기고
    재귀형식의 호출이 잦으므로 dp를 이용해 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0922
    {

        static void Main922(string[] args)
        {

            double ERR = 1e-9;

            StreamReader sr;
            StreamWriter sw;

            long[] hanoi;

            Solve();
            void Solve()
            {

                SetRet();

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int cur = 1;
                int n;
                while ((n = ReadInt()) != 0)
                {

                    sw.Write($"Case {cur++}: {hanoi[n]}\n");
                }

                sr.Close();
                sw.Close();
            }

            void SetRet()
            {

                hanoi = new long[1_001];

                Array.Fill(hanoi, -1);
                hanoi[0] = 0;
                hanoi[1] = 1;

                for (int i = 2; i <= 1_000; i++)
                {

                    hanoi[i] = GetRet(i);
                }
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

            long GetPow(long _a, long _exp)
            {

                long ret = 1;

                while(_exp > 0)
                {

                    if ((_exp & 1L) == 1L) ret = (ret * _a);
                    _a = (_a * _a);
                    _exp >>= 1;
                }

                return ret;
            }

            long GetRet(int _n)
            {

                if (hanoi[_n] != -1) return hanoi[_n];
                int k = _n + 1 - (int)(Math.Round(Math.Sqrt(2 * _n + 1) + ERR));

                return hanoi[_n] = 2 * GetRet(k) + GetPow(2, _n - k) - 1;
            }
        }
    }

#if other
// #include<stdio.h>
// #include<string.h>
// #include<math.h>
// #include<algorithm>
using namespace std;

int main(){
    long long sum,n,i,x=1,flag=1;
    for(int k=1;scanf("%lld",&n)!=EOF;k++)
    {
        sum=0; x=1; flag=1;
        for(i=1;i<=n;i++)//n번째 수
        {
            sum+=x;
            flag*=2;
            if(flag>=2*x)
            {
                flag=1;
                x*=2;
            }
        }
        printf("Case %d: %lld\n",k ,sum);
    }
}
#endif
}
