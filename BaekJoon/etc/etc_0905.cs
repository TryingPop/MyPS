using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 24
이름 : 배성훈
내용 : The Strange Sequence
    문제번호 : 7111번

    수학, 그리디 문제다
    조건대로 구현하기만 하면 된다
    다만, int 범위를 벗어날 수 있다
*/

namespace BaekJoon.etc
{
    internal class etc_0905
    {

        static void Main905(string[] args)
        {

            int LEN = 18;
            int n;
            long[] arr;
            long[] digit;
            long[] num;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                for (int i = 2; i <= n; i++)
                {

                    long sum = DigitSum(4 * arr[i - 1]);

                    IntToArr(arr[i - 1]);
                    Next(sum);
                    arr[i] = ArrToInt();
                }

                Validate();

                Console.Write(arr[n]);
            }

            void Validate()
            {

                for (int i = 2; i <= n; i++)
                {

                    bool chk = DigitSum(4 * arr[i - 1]) == DigitSum(arr[i]);
                    if (chk && arr[i - 1] < arr[i]) continue;

                    Console.WriteLine($"{i} : {arr[i - 1]}, {arr[i]}");
                    break;
                }
            }

            void IntToArr(long _num)
            {

                for (int i = 0; i < LEN; i++)
                {

                    num[i] = _num % 10;
                    _num /= 10;
                }
            }

            long ArrToInt()
            {

                long ret = 0;
                for (int i = 0; i < LEN; i++)
                {

                    ret += num[i] * digit[i];
                }

                return ret;
            }

            void Next(long _sum)
            {

                long cur = 0;
                for (int i = 0; i < LEN; i ++)
                {

                    cur += num[i];
                }

                if (cur < _sum) Up(_sum - cur);
                else Down(_sum, cur);
            }

            void Up(long _sum)
            {

                for (int i = 0; i < LEN; i++)
                {

                    if (num[i] + _sum <= 9)
                    {

                        num[i] += _sum;
                        break;
                    }
                    else
                    {

                        long sub = 9 - num[i];
                        _sum -= sub;
                        num[i] = 9;
                    }
                }
            }

            void Down(long _sum, long _cur)
            {

                for (int i = 1; i < LEN; i++)
                {

                    _cur -= num[i - 1];
                    num[i - 1] = 0;

                    if (_cur >= _sum) continue;
                    num[i]++;
                    _cur++;

                    for (int j = i; j < LEN; j++)
                    {

                        if (num[j] < 10) break;
                        _cur -= 9;
                        num[j] = 0;
                        num[j + 1]++;
                    }
                    break;
                }

                Up(_sum - _cur);
            }

            long DigitSum(long _n)
            {

                long ret = 0;
                while(_n > 0)
                {

                    ret += _n % 10;
                    _n /= 10;
                }

                return ret;
            }

            void Input()
            {

                string[] input = Console.ReadLine().Split();
                n = int.Parse(input[1]);
                arr = new long[n + 1];
                arr[1] = int.Parse(input[0]);

                digit = new long[LEN];
                num = new long[LEN];
                digit[0] = 1;
                for (int i = 1; i < LEN; i++)
                {

                    digit[i] = 10 * digit[i - 1];
                }
            }
        }
    }

#if other
// #include <iostream>
using namespace std;

int ds(long n){
    int s = 0;
    while (n){
        s += n % 10;
        n /= 10;
    }
    return s;
}

int f(int a, int s, int ts){
    int n09 = 0;
    while (1){
        if (a % 10 < 9){
            a += 1;
            s += 1;
            int diff = ts - s;
            if (0 <= diff && diff <= n09*9 + 9 - a%10){
                int p = 1, ans = 0;
                for (int i = 0; i < n09; i++){
                    int d = min(diff, 9);
                    diff -= d;
                    ans += p*d;
                    p *= 10;
                }
                return (a + diff)*p + ans; 
            }
        }
        s -= a % 10;
        a /= 10;
        n09++;
    }
}

int main(void){
    ios::sync_with_stdio(0);
    cin.tie(0);
    int a,n; cin >> a >> n;
    int s = ds(a);
    for (int i = 1; i < n; i++){
        int ts = ds(a*4l);
        a = f(a, s, ts);
        s = ts;
    }
    cout << a;
}

#elif other2
// #include <bits/stdc++.h>

using namespace std;

// #define all(v) v.begin(), v.end()
typedef long long ll;
ll a, n, pw[11], need, x;

ll digit_sum(ll x){
    ll ret = 0;
    while(x){
        ret += x % 10;
        x /= 10;
    }
    return ret;
}

int main(void){
    ios::sync_with_stdio(0); cin.tie(0); cout.tie(0);
    
    pw[0] = 1;
    for(int i = 1; i <= 10; i++) pw[i] = pw[i - 1] * 10;
    cin >> a >> n; n--;
    while(n--){
        need = digit_sum(4 * a) - digit_sum(a + 1); a++;
        ll digit;
        if(need){
            for(int i = 0;;i++){
                digit = a / pw[i] % 10;
                if(digit != 9 && need - 1 >= 0 && need <= 9 * i + 9 - digit){
                    a += pw[i]; need--;
                    for(int j = i; j >= 0; j--){
                        while(need > 9 * j) a += pw[j], need--;
                    }
                    break;
                }
                a -= pw[i] * digit;
                need += digit;
            }
        }
    }
    cout << a;
    return 0;
}
#endif
}
