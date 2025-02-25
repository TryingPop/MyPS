using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 31
이름 : 배성훈
내용 : 쓰레기 슈트
    문제번호 : 4225번

    볼록껍질(ConvexHull) 문제다.
    시작 점을 잘못잡아 계속해서 틀렸다;

    아이디어는 다음과 같다.
    볼록껍질을 구하고 볼록껍질의 각 선분에 대해
    가장 멀리 떨어진 점들과의 거리를 찾는다.
    
    그리고 각 선분들의 거리들 중 가장 작은 값이 정답이 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1229
    {

        static void Main1229(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            double E = 1e-9;

            (int x, int y)[] point;
            int n;
            int[] arr;
            int arrLen = 0, t;
            
            Comparer<(int x, int y)> cmp;

            Solve();
            void Solve()
            {

                Init();

                while (Input())
                {

                    ConvexHull();

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                t++;
                double ret = 1e9;

                for (int i = 0; i < arrLen; i++)
                {

                    double max = 0;

                    int chk1 = i;
                    int chk2 = (i + 1) % arrLen;

                    for (int j = 0; j < arrLen; j++)
                    {

                        if (chk1 == j || chk2 == j) continue;
                        max = Math.Max(max, LineDis(ref point[arr[j]], ref point[arr[chk1]], ref point[arr[chk2]]));
                    }
                    ret = Math.Min(ret, max);
                }

                
                ret = Math.Ceiling((ret - E) * 100) / 100.0;
                sw.Write($"Case {t}: {ret:0.00}\n");
            }

            double LineDis(ref (int x, int y) _pos, ref (int x, int y) _l1, ref (int x, int y) _l2) 
            {

                int a = _l1.y - _l2.y;
                int b = _l2.x - _l1.x;
                int c = _l1.x * _l2.y - _l2.x * _l1.y;

                double d = Math.Sqrt(a * a + b * b);
                double u = Math.Abs(_pos.x * a + _pos.y * b + c);

                return u / d;
            }

            void ConvexHull()
            {

                Array.Sort(point, 1, n - 1, cmp);
                arrLen = 0;
                for (int i = 0; i < n; i++)
                {

                    while(arrLen > 1 && CCW(ref point[arr[arrLen - 2]], ref point[arr[arrLen - 1]], ref point[i]) <= 0) { arrLen--; }
                    Push(i);
                }

                void Push(int _n)
                {

                    arr[arrLen++] = _n;
                }
            }

            int CCW(ref (int x, int y) _a, ref (int x, int y) _b, ref (int x, int y) _c)
            {

                int ccw = _a.x * _b.y + _b.x * _c.y + _c.x * _a.y
                    - _b.x * _a.y - _c.x * _b.y - _a.x * _c.y;

                if (ccw > 0) return 1;
                else if (ccw < 0) return -1;
                else return 0;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                point = new (int x, int y)[100];
                arr = new int[100];

                cmp = Comparer<(int x, int y)>.Create((a, b) => 
                {

                    int ccw = CCW(ref a, ref b, ref point[0]);

                    if (ccw == 0) return PosDis(ref a, ref point[0]).CompareTo(PosDis(ref b, ref point[0]));
                    return -ccw;
                });

                t = 0;
            }

            double PosDis(ref (int x, int y) _pos1, ref (int x, int y) _pos2)
            {

                int x = _pos1.x - _pos2.x;
                int y = _pos1.y - _pos2.y;
                x *= x;
                y *= y;
                return Math.Sqrt(x + y);
            }

            bool Input()
            {

                n = ReadInt();
                int minIdx = 0;
                for (int i = 0; i < n; i++)
                {

                    point[i] = (ReadInt(), ReadInt());
                    if (point[minIdx].y < point[i].y)
                        minIdx = i;
                    else if (point[minIdx].y == point[i].y && point[minIdx].x < point[i].x)
                        minIdx = i;
                }

                Swap(minIdx, 0);

                return n != 0;
            }

            void Swap(int _idx1, int _idx2)
            {

                var temp = point[_idx1];
                point[_idx1] = point[_idx2];
                point[_idx2] = temp;
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == ' ' || c == '\n') return true;
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;
using ll=long long;

struct pii {
	int x, y;
	bool operator<(pii b) const { return x<b.x || x==b.x && y<b.y; }
	pii operator-(pii b) const { return {x-b.x, y-b.y}; }
	ll operator^(pii b) const { return x*ll(b.y)-y*ll(b.x); }
	ll operator~() const { return x*ll(x)+y*ll(y); }
};
int main() {
	ios::sync_with_stdio(0);cin.tie(0);
	int N;
	cout<<fixed<<setprecision(2);
for(int tc=1; cin>>N, N; tc++) {
	pii A[N];
	for(auto&[x,y]:A) cin>>x>>y;
	sort(A, A+N);
	pii H[N+2];
	int l=-1, r=N+2;
	for(pii p:A) {
		while(l>=1 && (H[l-1]-H[l]^H[l-1]-p)>=0) l--;
		while(r<=N && (H[r+1]-H[r]^H[r+1]-p)<=0) r++;
		H[++l]=H[--r]=p;
	}
	double ans=1e18;
	for(int i=0, j=r; i<=l && j<=N+1;)
		if(j>N || i<l && (H[i+1]-H[i]^H[j+1]-H[j])>0) { i++; if(i<=l) ans=min(ans, abs(H[j]-H[i]^H[j]-H[i-1])/sqrt(~(H[i]-H[i-1]))); }
		else { j++; if(j<=N+1) ans=min(ans, abs(H[i]-H[j]^H[i]-H[j-1])/sqrt(~(H[j]-H[j-1]))); }
	cout<<"Case "<<tc<<": "<<ceil(ans*100)/100<<'\n';
}
}
#elif other2
// #include <iostream>
// #include <algorithm>
// #include <vector>
// #include <cmath>
// #include <iomanip>

using namespace std;
typedef long long ll;
typedef __uint128_t lll;
typedef long double ld;
typedef pair<ll, ll> pll;

struct point {
    ll x, y;
    bool operator < (const point &o) const {
        if(y != o.y) return y < o.y;
        return x < o.x;
    }
};

struct frac {
    lll n, d;
    frac(lll n, lll d) {
        this->n = n;
        this->d = d;
    }
    bool operator < (const frac &o) const {
        return n*o.d < d*o.n;
    }
};

ll ccw(point p1, point p2) {
    return p1.x*p2.y - p1.y*p2.x;
}

ll ccw3(point b, point p1, point p2) {
    point t1 = {p1.x-b.x, p1.y-b.y};
    point t2 = {p2.x-b.x, p2.y-b.y};
    return ccw(t1, t2);
}

frac powdist(point p, point ls, point le) {
    ll area = p.x*ls.y + ls.x*le.y + le.x*p.y;
    area -= p.y*ls.x + ls.y*le.x + le.y*p.x;
    area = abs(area);

    ll dx = ls.x-le.x;
    ll dy = ls.y-le.y;
    return {(lll)area*area, (lll)dx*dx+dy*dy};
}


ll n;
point p[105];

bool solve(int tc) {
    cin >> n;
    if(n == 0)
        return false;
    
    for(int i = 0; i < n; ++i)
        cin >> p[i].x >> p[i].y;
    p[n] = p[0];

    ll prod = 0;
    for(int i = 0; i < n; ++i) {
        prod += p[i].x*p[i+1].y;
        prod -= p[i].y*p[i+1].x;
    }

    if(prod < 0)
        reverse(p, p+n);
    
    int mnp = 0;
    for(int i = 1; i < n; ++i) {
        if(p[i] < p[mnp])
            mnp = i;
    }
    if(mnp != 0) {
        reverse(p, p+mnp);
        reverse(p+mnp, p+n);
        reverse(p, p+n);
    }
    p[n] = p[0];
    
    
    vector<point> hull;
    hull.push_back(p[0]);
    hull.push_back(p[1]);

    for(int i = 2; i <= n; ++i) {
        while(hull.size() >= 2) {
            ll hsz = hull.size();
            point p1 = hull[hsz-1];
            point p2 = hull[hsz-2];

            if(ccw3(p2, p1, p[i]) <= 0) {
                hull.pop_back();
            } else {
                break;
            }
        }
        hull.push_back(p[i]);
    }

    ll sz = hull.size();
    frac res = frac(10000000000ll, 1);
    for(int i = 0; i < sz-1; ++i) {
        frac cres = frac(0, 1);
        for(int j = 0; j < sz-1; ++j) {
            frac pd = powdist(hull[j], hull[i], hull[i+1]);
            cres = max(cres, pd);
        }
        res = min(res, cres);
    }

    ll l = 0, r = 1000000;
    while(l < r) {
        ll m = (l+r)>>1;
        frac mf = frac(m*m, 10000);
        if(mf < res) {
            l = m+1;
        } else {
            r = m;
        }
    }

    cout << "Case " << tc << ": " << l/100 << "." << setw(2) << setfill('0') << l%100 << "\n";
    return true;
}

int main() {
    cin.tie(NULL);
    ios::sync_with_stdio(false);

    int tc = 1;
    while(solve(tc++));
    return 0;
}
#endif
}
