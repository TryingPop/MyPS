using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 25 
이름 : 배성훈
내용 : 두더지 잡기
    문제번호 : 2259번

    dp, 정렬 문제다
    거리 계산쪽을 잘못해 4번 틀렸다

    다음항을 확인하는 식이면 어떨까 하고
    연산횟수를 보니 N을 원숭이 수라 하면 
    N^2 / 2번 연산을 한다
    
    그래서 이중 포문이 해볼만하다고 생각하고 진행했다
*/

namespace BaekJoon.etc
{
    internal class etc_0909
    {

        static void Main909(string[] args)
        {

            StreamReader sr;
            int n, s;
            (int x, int y, int t)[] mon;
            int[] dp;

            Solve();
            void Solve()
            {

                Input();

                FillDp();

                Console.Write(dp[n]);
            }

            void FillDp()
            {

                for (int i = 0; i < n; i++)
                {

                    dp[i]++;
                    for (int j = i + 1; j <= n; j++)
                    {

                        if (mon[i].t == mon[j].t || Chk(i, j)) continue;
                        dp[j] = Math.Max(dp[j], dp[i]);
                    }
                }
            }

            bool Chk(int _idx1, int _idx2)
            {

                double t = Math.Abs(mon[_idx2].t - mon[_idx1].t);
                double x = (mon[_idx1].x - mon[_idx2].x);
                double y = (mon[_idx1].y - mon[_idx2].y);

                return Math.Sqrt(x * x + y * y) > t * s;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                s = ReadInt();
                mon = new (int x, int y, int t)[n + 1];

                for (int i = 0; i < n; i++)
                {

                    mon[i] = (ReadInt(), ReadInt(), ReadInt());
                }

                Array.Sort(mon, (x, y) => y.t.CompareTo(x.t));
                dp = new int[n + 1];

                sr.Close();
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool positive = c != '-';
                int ret = positive ? c - '0' : 0;

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return positive ? ret : -ret;
            }
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;

// #pragma GCC optimize("O3")
// #pragma GCC optimize("Ofast")
// #pragma GCC optimize("unroll-loops")

// #define F first
// #define S second
// #define X first
// #define Y second

// #define rep(i, s, e) for (int i = s; i <= e; i++)
// #define per(i, e, s) for (int i = e; i >= s; i--)
// #define fx(each, vc) for (auto &each: vc)
// #define fy(x, y, vc) for (auto &[x, y]: vc)
// #define fz(x, y, z, vc) for (auto &[x, y, z]: vc)

// #define all(x) (x).begin(), (x).end()
// #define comp(x) sort(all((x))); (x).erase(unique(all((x))), (x).end())
// #define tomp(val, vc) val = (lower_bound(all((vc)), (val)) - (vc).begin())
// #define sz(x) (int)((x).size())
// #define sq(x) ((x) * (x))
// #define srt(x) sort(all(x))
// #define ktx(arr, n) sort((arr)+1, (arr)+(n)+1)
// #define rav(x) reverse(all(x))

// #define pb push_back
// #define pf push_front
// #define eb emplace_back
// #define ef emplace_front
// #define pu push
// #define em emplace

// #define tax(a, b) a = max(a, b)
// #define tin(a, b) a = min(a, b)
// #define tod(a, b) a = (a%MOD+b%MOD)%MOD

// #define priqu priority_queue
// #define maxpq(name, type) priqu<type> name
// #define minpq(name, type) priqu<type, vector<type>, greater<type> > name

using ll = long long;
using vi = vector<int>;
using vl = vector<ll>;
using pi = pair<int, int>;
using pl = pair<ll, ll>;
using vp = vector<pair<int, int> >;

// #define yes cout << "Yes\n"
// #define no cout << "No\n"
// #define imp cout << "-1\n"
// #define el cout << "\n"

const int MAX = 1e5 + 5;
const int LOG = 20;
const int INF = 1e9;
const ll LINF = 1e18;
const int MOD = 1e9 + 7;
const int dy[8] = { -1, 0, 1, 0, -1, 1, 1, -1 };
const int dx[8] = { 0, 1, 0, -1, 1, 1, -1, -1 };

template<typename ...Args>
void sc(Args&... args) { (cin >> ... >> args); }

template <typename Arg, typename ...Args>
void pr(const Arg& arg, const Args&... args) {
    cout << arg;
    ((cout << ' ' << args), ...);
    cout << '\n';
}

template<typename T> istream& operator>>(istream& in, vector<T>& a) {for(auto &x : a) in >> x; return in;};
template<typename T> ostream& operator<<(ostream& out, vector<T>& a) {for(auto &x : a) out << x << ' '; return out;};

/*
 * Notes;
 * Use 'cin.ignore();' before using getline(cin, s);
*/

struct Data {
    ll x, y, t;
    bool operator<(const Data &r)const{
        return t < r.t;
    }
};

ll n, s, D[7005];
Data a[7005];

int main(void)
{
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr); cout.tie(nullptr);
    cout << fixed; cout.precision(20);

    sc(n, s);
    rep(i, 1, n) {
        sc(a[i].x, a[i].y, a[i].t);
    }
    sort(a+1, a+n+1);

    D[0] = 0;
    rep(i, 0, n) {
        if (i && !D[i]) continue;
        rep(j, i+1, n) {
            double ti = a[j].t - a[i].t;
            if (ti*s*ti*s >= sq(a[i].x-a[j].x) + sq(a[i].y-a[j].y)) {
                tax(D[j], D[i] + 1);
            }
        }
    }

    pr(*max_element(D, D+n+1));

    return 0;
}
#endif
}
