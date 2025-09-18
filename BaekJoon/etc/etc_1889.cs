using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 16
이름 : 배성훈
내용 : 分数 (Fraction)
    문제번호 : 24146번

    우선순위 큐 문제다.
    가장 작은거부터 찾아야 한다.
    그런데 모든 경우를 넣고 정렬하면 N^2에 가까워 보인다.
    반면 우선순위 큐로 관리하는 경우 N x 2에 찾을 수 있다.

    여기서 중복해서 세면 안되므로 매번 GCD를 찾아서 확인했다.
    3만 20만을 넣으니 1초만에 안되어서 터지지 않을까 고민했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1889
    {

        static void Main1889(string[] args)
        {

            int n, k;
            PriorityQueue<(int x, int y), (int x, int y)> pq;

            Input();

            GetRet();

            void GetRet()
            {

                for (int i = 2; i <= n; i++)
                {

                    pq.Enqueue((1, i), (1, i));
                }

                int cnt = 0;
                while (cnt < k && pq.Count > 0)
                {

                    (int x, int y) = pq.Dequeue();
                    if (GetGCD(x, y) == 1) 
                    { 
                        
                        cnt++;
                        if (cnt == k) 
                        { 
                            
                            Console.Write($"{x} {y}");
                            return;
                        }
                    }

                    if (x + 1 < y) pq.Enqueue((x + 1, y), (x + 1, y));
                }

                Console.Write(-1);

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
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                k = int.Parse(temp[1]);

                Comparer<(int x, int y)> cmp = Comparer<(int x, int y)>.Create((x, y) => (x.x * y.y).CompareTo(x.y * y.x));
                pq = new(k * 2, cmp);
            }
        }
    }

#if other
// #include<bits/stdc++.h>
// #include <ext/pb_ds/assoc_container.hpp>
//#include <atcoder/all>
//using mint = atcoder::modint998244353;
using namespace std;
using namespace __gnu_pbds;

using lint = long long;
using ii = pair<int, int>;
using il = pair<int, lint>;
using li = pair<lint, int>;
using ll = pair<lint, lint>;

const int mxn = 1000100, mxp = 1000001;
const int MOD = 998244353, inf = -1e9 - 7, INF = 1e9 + 1;
const lint lnf = -4e18, LNF = 5e18;
const double eps = 1e-10;
const int sqrtN = 200;

// #define sz(x) int(size(x))
// #define all(x) (x).begin(),(x).end()
// #define compress(x) sort(all(x)), (x).erase(unique(all(x)), (x).end())
// #define lb(x, v) (lower_bound(all(x), v) - (x).begin())
// #define ub(x, v) (upper_bound(all(x), v) - (x).begin())
// #define getName(var)  #var
vector<lint> xl, yl;

// #define Yes "Yes\n"
// #define No "No\n"

const int dr[] = {-1, 0, 1, 0};
const int dc[] = {0, -1, 0, 1};

inline int inRange(int r, int c, int R, int C) {
    return 0 <= r && r < R && 0 <= c && c < C;
}

int N, M, Q, R, C, H, K, T;

void init() {

}

vector<ii> res;
void f(int u1, int v1, int u2, int v2) {
    int u = u1 + u2, v = v1 + v2;
    if(u > N || v > N || sz(res) > K) return;
    f(u1, v1, u, v);
    res.emplace_back(u,v);
    f(u, v, u2, v2);
}

void solve() {
    cin >> N >> K;
    f(0, 1, 1, 1);
    if(sz(res) < K) {
        cout << -1;
        return;
    }
    auto [u, v] = res[K-1];
    cout << u << ' ' << v;
}

int main() {
// #ifndef ONLINE_JUDGE
    freopen("input.txt", "r", stdin);
// #endif
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    cout.tie(nullptr);
    int TC = 1;
//    cin >> TC;
    while (TC--) {
        init();
    solve();
}

return 0;
}

#endif
}
