using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 25
이름 : 배성훈
내용 : Burrito King
    문제번호 : 10590번

    그리디 수학 문제다.
    아이디어는 다음과 같다.
    불행도가 B가 될때까지 최대한 부리또를 받는데 행복도는 최대가 되어야 한다.
    이에 그리디로 a가 크고 b는 작은 애들을 최대한 먼저 받는게 좋다.
*/

namespace BaekJoon.etc
{
    internal class etc_1131
    {

        static void Main1131(string[] args)
        {

            decimal E = 0.0000000001m;
            int n, A, B;
            (int g, int a, int b, int idx)[] burrito;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Array.Sort(burrito, (x, y) => (1m * y.a * x.b).CompareTo(1m * x.a * y.b));

                decimal ret1 = 0;
                decimal ret2 = 0;

                decimal[] use = new decimal[n];

                for (int i = 0; i < n; i++)
                {

                    decimal g = burrito[i].g;
                    if (burrito[i].b > 0 && B < g * burrito[i].b + E + ret2)
                    {

                        decimal chk = B + E - ret2;
                        g = chk / burrito[i].b;
                    }

                    ret1 += g * burrito[i].a;
                    ret2 += g * burrito[i].b;
                    use[burrito[i].idx] = g;
                    if (ret2 >= B + E) break;
                }

                if (ret1 + E < A)
                {

                    Console.Write("-1 -1");
                    return;
                }


                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    sw.Write($"{ret1:0.#########} {ret2:0.#########}\n");
                    for (int i = 0; i < n; i++)
                    {

                        sw.Write($"{use[i]:0.#########} ");
                    }
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                A = ReadInt();
                B = ReadInt();

                burrito = new (int g, int a, int b, int idx)[n];
                for (int i = 0; i < n; i++)
                {

                    burrito[i] = (ReadInt(), ReadInt(), ReadInt(), i);
                }

                sr.Close();

                int ReadInt()
                {

                    int ret = 0;
                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;

                        ret = c - '0';

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }

#if other
// #include <bits/stdc++.h>
// #define pii pair<ll, ll>
// #define ppii pair<pii, pii>
typedef long long ll;
typedef long double ld;
using namespace std;

const int MAX = 100007;
ppii P[MAX];
ld ans[MAX];

int main() {
	ios::sync_with_stdio(0); cin.tie(0);	
	ll N;
	ld A, B, ca = 0.0, cb;
	cin >> N >> A >> B;
	cb = B;
	for (int i = 0; i < N; ++i) {
		ll g, a, b;
		cin >> g >> a >> b;
		P[i] = { {a, b}, {g, i} };
	}
	sort(P, P + N, [&](ppii& a, ppii& b) {
		auto& [aa, ab] = a.first;
		auto& [ba, bb] = b.first;
		return aa * bb > ab * ba;
		});
	for (int i = 0; i < N; ++i) {
		auto& [ta, tb] = P[i];
		auto& [a, b] = ta;
		auto& [g, idx] = tb;
		if (b * g <= cb) {
			ans[idx] = (ld)g;
			ca += a * g;
			cb -= b * g;
		}
		else {
			ld p = cb / b;
			ca += p * a;
			ans[idx] = p;
			cb = 0;
		}
	}
	if (ca < A) cout << -1 << ' ' << -1;
	else {
		cout.precision(12);
		cout << ca << ' ' << ((ld)B - cb) << '\n';
		for (int i = 0; i < N; ++i) cout << ans[i] << ' ';
	}
	return 0;
}
#endif
}
