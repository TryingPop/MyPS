using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 16
이름 : 배성훈
내용 : Jealous Numbers
    문제번호 : 3559번

    ////////////////////////////////
    현재 잘못 해석했다
    예제를 보면 1 ~ 20범위 안에 3-dominating over~2를 찾아야 한다
    alpha(n, x) = k, Math.Max(x^k : n % x^k == 0) 을 의미한다

    그리고 3-dominating over~2는
    alpha(n, 3) > alpha(n, 2)인 숫자들의 모임이된다
    3, 9, 15, 18이 있다
    6은 alpha(6, 3) = 1, alpha(6, 2) = 1이므로 alpha(6, 3) == alpha(6, 2)이므로
    될 수 없고
    12는 alpha(12, 2) = 2, alpha(12, 3) = 1이므로 alpha(12, 3) < alpha(12, 2)이므로 될 수 없다

    18은 alpha(18, 3) = 2이고, alpha(18, 2) = 1이므로 alpha(18, 3) > alpha(18, 2)
    가 성립해 포함된다
    ///////////////////////////////////

    수학, 정수론, 포함 배제 문제다
    로그를 이용해 풀었는데, 오차 판별을 위한 작은 수를 1e-9로 잡으니 오차로 틀렸다
    이후 오차를 1e-18로 더 줄이니 이상없이 통과한다
*/

namespace BaekJoon.etc
{
    internal class etc_0971
    {

        static void Main971(string[] args)
        {

            double E = 1e-18;
            double END;
            long a, b, p, q;
            long ret;

            Solve();
            void Solve()
            {

                Input();

                GetRet();

                Console.Write(ret);
            }

            void GetRet()
            {

                long val = 1L;
                END = Math.Log10(b);
                ret = 0;
                while (true)
                {

                    if (ChkInvalidRange(1.0 * val * p)) return;
                    val *= p;
                    ret += Cnt(val);
                    if (ChkInvalidRange(1.0 * val * q)) return;
                    val *= q;
                    ret -= Cnt(val);
                }
            }

            long Cnt(long _a)
            {

                if (b < _a) return 0;
                long ret = b / _a - a / _a;
                if (a % _a == 0) ret++;

                return ret;
            }

            bool ChkInvalidRange(double _a)
            {

                double chk = Math.Log10(_a);
                return chk + E > END;
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                a = long.Parse(temp[0]);
                b = long.Parse(temp[1]);
                p = long.Parse(temp[2]);
                q = long.Parse(temp[3]);
            }
        }
    }

#if other
// #define forin(i, n) for(int i{0}; i < n; ++i)
// #include <iostream>
// #include <vector>
// #include <algorithm>
using namespace std;
typedef long long ll;
typedef __int128 ib;
int main(){
    cin.tie(NULL);
    ios_base::sync_with_stdio(false);
    ll a, b; int p, q;
    cin >> a >> b >> p >> q;
    auto ax = [&p, &q](ll n){
        ib u = p, v = q;
        ll us = 0;
        while(true){
            if(u > n) break;
            us += n / (ll)u;
            u *= q;
            if(u > n) break;
            us -= n / (ll)u;
            u *= p;
        } return us;
    };
    cout << ax(b) - ax(a-1);
}
#endif
}
