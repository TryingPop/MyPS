using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

/*
날짜 : 2025. 10. 3
이름 : 배성훈
내용 : Fair Division
    문제번호 : 26132번

    수학, 브루트포스 문제다.
    해당 식을 풀면, 각 j < n에 대해
    각 j에 대해 mf (1 - f)^j / (1 - f^n) 이 정수인지 확인해야 한다.
    f = a / b라 하면, m * a * b^(n-1-j) * (b- a)^j / (b^n - (b- a)^n)이 된다.
    여기서 a, b는 서로소가 보장되므로 실상은 b^n - (b-a)^n이 m * a를 나누는지만 확인하면 된다.
    여기서 a를 안곱해줘서 몇 번 틀렸다.
    왜냐하면 b^n - (b - a)^n = (b - (b - a)) * (b^n-1 + a * b^n-2 a^2 * b^n-3 + ... a^n-1)으로 인수분해 되고
    a가 인수로 1개 유일하게 들어간다!
    이외는 gcd(a, b) = 1이므로 오른쪽은 a로 나눠떨어질 수 없다.

    초기에는 이를 놓쳐서 m으로만 하다가, 반례에 여러 번 틀렸다.
    그리고 매번 a^n, b^n을 찾으려고하니 시간초과 떴다.
    반복해서 사용하니 dp로 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1926
    {

        static void Main1926(string[] args)
        {

            int n;
            BigInteger m;

            Input();

            GetRet();

            void GetRet()
            {

                BigInteger[] nPow = new BigInteger[4_000];
                nPow[1] = 1;
                for (int b = 2; b < 4_000; b++)
                {

                    BigInteger chk = BigInteger.Pow(b, n - 1);
                    nPow[b] = chk * b;
                    if (chk > m) break;
                    for (int a = 1; a < b; a++)
                    {

                        int gcd = GetGCD(a, b);
                        if (gcd != 1 || ChkInValid(a, b)) continue;
                        Console.Write($"{a} {b}");
                        return;
                    }
                }
                
                Console.Write("impossible");

                int GetGCD(int a, int b)
                {

                    while (b > 0)
                    {

                        int temp = a % b;
                        a = b;
                        b = temp;
                    }

                    return a;
                }

                bool ChkInValid(int a, int b)
                {

                    BigInteger div = nPow[b] - nPow[b - a];
                    BigInteger up = m * a;
                    return up % div != 0;
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();

                n = int.Parse(temp[0]);
                m = BigInteger.Parse(temp[1]);
            }
        }
    }

#if other
// #include <bits/stdc++.h>
// #define ll long long
using namespace std;

ll n, m;

int main(){
    ios_base::sync_with_stdio(false);
    cin.tie(NULL);
    cout.tie(NULL);
    cin>>n>>m;
    if(n>60){
        cout<<"impossible";
        return 0;
    }
    for(ll i=2; i<=5000; i++){
        for(ll j=i-1; j; j--){
            ll x=i+j;
            ll q=j*j;
            bool fl=0;
            for(ll k=2; k<n; k++){
                if(x>m/i+1){
                    fl=1;
                    break;
                }
                x=x*i+q;
                q*=j;
            }
            if(!fl){
                if(m%x==0){
                    cout<<i-j<<' '<<i<<'\n';
                    return 0;
                }
            }
        }
    }
    cout<<"impossible";
}
#endif
}
