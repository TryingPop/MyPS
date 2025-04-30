using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 30
이름 : 배성훈
내용 : ICPC Square
    문제번호 : 33120번

    수학, 정수론 문제다.
    가능한 층은 s의 배수다.

    이제 가능한 상한을 n층을 넘지 못하고 d * 2배도 넘지 못한다
    그래서 상한은 n과 2 * d 둘보다 낮아야 된다.
    그리고 상한은 s의 배수이다.

    이제 상한이 가능한지 확인해야 한다.
    만약 상한이 s의 짝수배인 경우 2 * d보다 낮으므로
    상한의 절반의 층은 d보다 작거나 같다.
    그래서 절반의 층으로 이동한 뒤 상한으로 이동하면 된다.

    이제 짝수층이 아닌 경우는 이동 가능한 경로를 모두 찾아본다.
    만약 이동 가능한 작은 층을 찾는다면 해당 경우로 시도한다.
    해당 차이가 가장 작은 값이므로 불가능한 경우 끝내고 나오면 된다.

    갈 수 없는 경우는 홀수에서 오기에 해당 상한 - s층은 s의 짝수배이므로 해당 층은 갈 수 있다.
    그래서 상한 - s층이 정답이 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1592
    {

        static void Main1592(string[] args)
        {

            long n, d, s;

            Input();

            GetRet();

            void GetRet()
            {

                // 배수 찾기
                n /= s;
                d /= s;

                // sup x s 층이 상한이다.
                // sup은 배수의 상한이다.
                long sup = Math.Min(n, d * 2);

                if (Chk()) sup--;
                long ret = sup * s;
                if (ret < s) ret = s;
                Console.Write(ret);

                bool Chk()
                {

                    for (long x = 2; x * x <= sup; x++)
                    {

                        // sup의 정의로 짝수면 무조건 false다.
                        // 해당층 엘리베이터 못타면 false다.
                        // x의 정의로 sup / x > x이므로
                        // sup - sup / x > d가 되면 sup - x > d가 성립한다.
                        // 그래서 sup - sup / x > d만 확인
                        if (sup % x != 0) continue;
                        else if (sup - sup / x > d) return true;
                        return false;
                    }

                    // 마지막 (sup - 1) x  s 층에서 타고올 수 있는지 확인
                    return sup - 1 > d;
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = long.Parse(temp[0]);
                d = long.Parse(temp[1]);
                s = long.Parse(temp[2]);
            }
        }
    }

#if other
// #include <bits/stdc++.h>

using namespace std;
typedef long long ll;

int main(){
	ll n, d, s;
	scanf("%lld %lld %lld", &n, &d, &s);

	if (s > d){
		printf("%lld\n", s);
		return 0;
	}

	ll n1 = n / s;
	ll d1 = d / s;
	ll ans = 0;

	if (n1 >= d1*2) ans = d1 * 2;
	else{
		if (n1%2==0) ans = n1;
		else if (n1 <= d1+1) ans = n1;
		else{
			ans = n1-1;
			for (ll i=2;i*i<=n1;i++) if (n1 % i == 0){
				if (n1 - n1/i <= d1){
					ans = n1;
				}
				break;
			}
		}
	}

	printf("%lld\n", ans * s);
}
#endif
}
