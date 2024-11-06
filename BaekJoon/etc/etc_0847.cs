using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 28
이름 : 배성훈
내용 : Zombie's Treasure Chest
    문제번호 : 6278번

    수학, 그리디, 브루트포스, 정수론 문제다
    주어진 문제를 그리디로 보면 다른 보석을 더 실을 수 없을 때
    최대값 후보임을 알 수 있다 
        다른 보석을 더 실으면 더큰 값이 존재하기에 최대가 아니다

    이에 맞춰 모든 경우를 조사하면 된다
    그런데 그냥 조사하는 경우면 경우의 수가 너무 많아질 수 있다
    그래서 비교적 큰 무게를 가지는 물건을 올려가면서 비교하면 된다

    그리고 lcm 이상인 경우는 lcm의 최대가치로 바꾸는게 이득이기 때문에
    lcm 이하로만 판별하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0847
    {

        static void Main847(string[] args)
        {

            string CASE = "Case #";
            string AND = ": ";

            StreamReader sr;
            StreamWriter sw;

            long n, minSize, maxSize, minValue, maxValue;
            long lcm, lcmValue;
            long ret;

            Solve();
            void Solve()
            {

                Init();
                int test = ReadInt();

                for (int i = 1; i <= test; i++)
                {

                    Input();

                    GetRet();

                    sw.Write($"{CASE}{i}{AND}{ret}\n");
                }

                sw.Close();
                sr.Close();
            }

            long GetGCD(long _a, long _b)
            {

                while(_b > 0)
                {

                    long temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
            }

            void GetRet()
            {

                ret = 0;

                // 세는 개수 조절
                // Min으로 먼저 판별안하면 밑의 for문에서 매번 탈출 확인해야한다
                long len = Math.Min(n / maxSize, lcm / maxSize) + 1;

                for (int i = 0; i < len; i++)
                {

                    long r = n - i * maxSize;

                    long lcmCnt = r / lcm;
                    r -= lcmCnt * lcm;

                    long curV = i * maxValue + lcmCnt * lcmValue + (r / minSize) * minValue;
                    if (ret < curV) ret = curV;
                }
            }

            void Input()
            {

                n = ReadInt();
                int s1 = ReadInt();
                int v1 = ReadInt();
                int s2 = ReadInt();
                int v2 = ReadInt();

                if (s1 < s2)
                {

                    minSize = s1;
                    maxSize = s2;

                    minValue = v1;
                    maxValue = v2;
                }
                else
                {

                    minSize = s2;
                    maxSize = s1;

                    minValue = v2;
                    maxValue = v1;
                }


                long gcd = GetGCD(maxSize, minSize);
                lcm = (minSize / gcd) * maxSize;

                long chk1 = minSize * maxValue;
                long chk2 = maxSize * minValue;

                if (chk1 < chk2) lcmValue = minValue * (maxSize / gcd);
                else lcmValue = maxValue * (minSize / gcd);
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
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
// #include <bits/stdc++.h>
// #define endl '\n'
const int PRECISION = 0;
using namespace std;
using ll = long long;
using pl2 = pair<ll, ll>;
using pl3 = pair<pl2, ll>;
// #define fr first
// #define sc second

// #pragma GCC optimize("Ofast")



void Main(){
	int t; cin >> t; for (int tt = 1; tt <= t; tt++){
		cout << "Case #" << tt << ": ";
		ll n; pl2 p1, p2; cin >> n >> p1.fr >> p1.sc >> p2.fr >> p2.sc;
		ll num = n / (p1.fr*p2.fr);
		ll ans = num*max(p1.fr*p2.sc, p2.fr*p1.sc); n -= num*(p1.fr*p2.fr);
		if (p1.fr > p2.fr){ swap(p1, p2); }
		ll res = 0;
		for (ll c2 = 0; c2 <= p1.fr; c2++){
			ll v2 = c2*p2.sc; ll w2 = c2*p2.fr;
			if (w2 > n){ break; }
			ll c1 = (n-w2)/p1.fr;
			ll v1 = c1*p1.sc;
			res = max(res, v1+v2);
		}
		cout << ans+res << endl;
	}
}

int main(){
	ios_base::sync_with_stdio(0); cin.tie(0); cout.tie(0);
	cout.setf(ios::fixed); cout.precision(PRECISION);
	Main();
}

#endif
}
