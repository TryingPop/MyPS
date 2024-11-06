using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 20
이름 : 배성훈
내용 : 세계 일주
    문제번호 : 31219번

    2024. 7. 21
    브루트포스, 백트래킹, 선분 교차 판정, 외판원 순회 문제다
    브루트 포스, 백트래킹으로 해결했다

    우선 조건에서 어떤 세 국가도 일직선상에 있지 않다고 한다
    이로 세계일주가 불가능한 경우는 없다!고 봤다

    느낌상 그리디하게 외부 점들로 돌면 되지 않을까 생각했다
    그런데 막상 구현하려니 모르겠고,

    브루트포스와 백트래킹으로 하나씩 체크하며 거리를 계산했다
    매번 선분 교차판정을 하는건 중복이 많다 생각했고
    선분은 많아야 100개를 넘지 않고 제곱해도 10000정도이기에
    선분간 모든 교차를 판정해 배열에 저장했다
    이후에 선분 교차판정은 배열 값으로 바로 판별시켰다
    그리고 모두 교차하지 않으면 거리를 재고 정답을 구했다

    이렇게 제출하니 이상없이 통과한다
    백준의 알고리즘 대회 문제라 해설이 존재하는데 찾아보니
    외판원으로 찾아가면 된다고 한다
    증명은 귀류법으로 가능하다
*/

namespace BaekJoon.etc
{
    internal class etc_0578
    {

        static void Main578(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));

            int n;
            (int x, int y)[] pos;
            bool[][] notCross;
            bool[] use;
            int[] move;
            double ret;

            Solve();

            void Solve()
            {

                Input();

                ChkCross();

                ret = 0.0;
                use = new bool[n];
                use[0] = true;
                move = new int[n];
                DFS();

                Console.Write($"{ret:0.00000000}");
            }

            void DFS(int _depth = 1, double _dis = 0)
            {

                if (_depth == n)
                {

                    bool flag = false;
                    int lidx = GetLineIdx(move[0], move[n - 1]);
                    for (int i = 1; i < n - 2; i++)
                    {

                        int olidx = GetLineIdx(move[i], move[i + 1]);
                        if (notCross[lidx][olidx]) continue;
                        flag = true;
                        break;
                    }

                    if (flag) return;

                    double chk = GetDis(move[0], move[_depth - 1]);

                    for (int i = 0; i < n - 1; i++)
                    {

                        chk += GetDis(move[i], move[i + 1]);
                    }

                    if (ret == 0.0) ret = chk;
                    else ret = Math.Min(ret, chk);

                    return;
                }

                for (int i = 0; i < n; i++)
                {

                    if (use[i]) continue;
                    use[i] = true;
                    move[_depth] = i;

                    bool flag = true;

                    int clidx = GetLineIdx(i, move[_depth - 1]);

                    for (int j = 0; j < _depth - 2; j++)
                    {

                        int olidx = GetLineIdx(move[j], move[j + 1]);
                        if (notCross[clidx][olidx]) continue;
                        flag = false;
                        break;
                    }

                    if (flag) DFS(_depth + 1);

                    use[i] = false;
                }
            }

            // 교차 판정
            void ChkCross()
            {

                notCross = new bool[100][];
                for (int i = 0; i < 100; i++)
                {

                    notCross[i] = new bool[100];
                }

                for (int f1 = 0; f1 < n; f1++)
                {

                    for (int t1 = 0; t1 < f1; t1++)
                    {

                        int lidx = GetLineIdx(f1, t1);

                        for (int f2 = 0; f2 < n; f2++)
                        {

                            for (int t2 = 0; t2 < f2; t2++)
                            {

                                int ridx = GetLineIdx(f2, t2);

                                if (IsCross(f1, t1, f2, t2))
                                {

                                    notCross[lidx][ridx] = true;
                                    notCross[ridx][lidx] = true;
                                }
                            }
                        }
                    }
                }
            }

            // 두 점의 idx로 선분 idx 계산
            int GetLineIdx(int _f, int _t)
            {

                return _f < _t ? _f * 10 + _t : _t * 10 + _f;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                pos = new (int x, int y)[n];
                for (int i = 0; i < n; i++)
                {

                    pos[i] = (ReadInt(), ReadInt());
                }

                sr.Close();
            }

            // 거리 찾기
            double GetDis(int _pidx1, int _pidx2)
            {

                long x = pos[_pidx1].x - pos[_pidx2].x;
                x *= x;

                long y = pos[_pidx1].y - pos[_pidx2].y;
                y *= y;

                return Math.Sqrt(x + y);
            }

            // 선분 교차 판정
            bool IsCross(int _f1, int _t1, int _f2, int _t2)
            {

                if (_f1 == _f2 || _f1 == _t2 || _t1 == _f2 || _t1 == _t2) return true;
                int chk1 = CCW(_f1, _t1, _f2) * CCW(_f1, _t1, _t2);
                int chk2 = CCW(_f2, _t2, _f1) * CCW(_f2, _t2, _t1);

                if (chk1 == 0 && chk2 == 0)
                {

                    if (pos[_f1].x == pos[_t1].x)
                    {

                        int min = pos[_f1].y < pos[_t1].y ? pos[_f1].y : pos[_t1].y;
                        int max = pos[_f1].y < pos[_t1].y ? pos[_t1].y : pos[_f1].y;

                        return (min <= pos[_f2].y && pos[_f2].y <= max)
                            || (min <= pos[_t2].y && pos[_t2].y <= max);
                    }
                    else
                    {

                        int min = pos[_f1].x < pos[_t1].x ? pos[_f1].x : pos[_t1].x;
                        int max = pos[_f1].x < pos[_t1].x ? pos[_t1].x : pos[_f1].x;

                        return (min <= pos[_f2].x && pos[_f2].x <= max)
                            || (min <= pos[_t2].x && pos[_t2].x <= max);
                    }
                }
                else return chk1 > 0 || chk2 > 0;
            }

            // 선분 교차에 쓰이는 CCW 알고리즘
            int CCW(int _a, int _b, int _c)
            {

                long ccw = 1L * pos[_a].x * pos[_b].y
                    + 1L * pos[_b].x * pos[_c].y
                    + 1L * pos[_c].x * pos[_a].y;

                ccw -= 1L * pos[_b].x * pos[_a].y
                    + 1L * pos[_c].x * pos[_b].y
                    + 1L * pos[_a].x * pos[_c].y;

                if (ccw == 0) return 0;
                else if (ccw > 0) return 1;
                else return -1;
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool plus = c != '-';
                int ret = plus ? c - '0' : 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;


template <typename T> struct Point {
public:
	T x, y;
	Point() : x(0), y(0) {}
	Point(T x_, T y_) : x(x_), y(y_) {}
	template <typename U> explicit Point(const Point<U>& p) : x(p.x), y(p.y) {}
	Point(const std::pair<T, T>& p) : x(p.first), y(p.second) {}
	Point(const std::complex<T>& p) : x(real(p)), y(imag(p)) {}
	explicit operator std::pair<T, T> () const { return std::pair<T, T>(x, y); }
	explicit operator std::complex<T> () const { return std::complex<T>(x, y); }

	friend std::ostream& operator << (std::ostream& o, const Point& p) { return o << '(' << p.x << ',' << p.y << ')'; }
	friend std::istream& operator >> (std::istream& i, Point& p) { return i >> p.x >> p.y; }
	friend bool operator == (const Point& a, const Point& b) { return a.x == b.x && a.y == b.y; }
	friend bool operator != (const Point& a, const Point& b) { return !(a==b); }

	Point operator + () const { return Point(+x, +y); }
	Point operator - () const { return Point(-x, -y); }

	Point& operator += (const Point& p) { x += p.x, y += p.y; return *this; }
	Point& operator -= (const Point& p) { x -= p.x, y -= p.y; return *this; }
	Point& operator *= (const T& t) { x *= t, y *= t; return *this; }
	Point& operator /= (const T& t) { x /= t, y /= t; return *this; }

	friend Point operator + (const Point& a, const Point& b) { return Point(a.x+b.x, a.y+b.y); }
	friend Point operator - (const Point& a, const Point& b) { return Point(a.x-b.x, a.y-b.y); }
	friend Point operator * (const Point& a, const T& t) { return Point(a.x*t, a.y*t); }
	friend Point operator * (const T& t ,const Point& a) { return Point(t*a.x, t*a.y); }
	friend Point operator / (const Point& a, const T& t) { return Point(a.x/t, a.y/t); }

	T dist2() const { return x * x + y * y; }
	auto dist() const { return std::sqrt(dist2()); }
	Point unit() const { return *this / this->dist(); }
	auto angle() const { return std::atan2(y, x); }

	T int_norm() const { return __gcd(x,y); }
	Point int_unit() const { if (!x && !y) return *this; return *this / this->int_norm(); }

	// Convenient free-functions, mostly for generic interop
	friend auto norm(const Point& a) { return a.dist2(); }
	friend auto abs(const Point& a) { return a.dist(); }
	friend auto unit(const Point& a) { return a.unit(); }
	friend auto arg(const Point& a) { return a.angle(); }
	friend auto int_norm(const Point& a) { return a.int_norm(); }
	friend auto int_unit(const Point& a) { return a.int_unit(); }

	Point perp_cw() const { return Point(y, -x); }
	Point perp_ccw() const { return Point(-y, x); }

	friend T dot(const Point& a, const Point& b) { return a.x * b.x + a.y * b.y; }
	friend T cross(const Point& a, const Point& b) { return a.x * b.y - a.y * b.x; }
	friend T cross3(const Point& a, const Point& b, const Point& c) { return cross(b-a, c-a); }

	// Complex numbers and rotation
	friend Point conj(const Point& a) { return Point(a.x, -a.y); }

	// Returns conj(a) * b
	friend Point dot_cross(const Point& a, const Point& b) { return Point(dot(a, b), cross(a, b)); }
	friend Point cmul(const Point& a, const Point& b) { return dot_cross(conj(a), b); }
	friend Point cdiv(const Point& a, const Point& b) { return dot_cross(b, a) / b.norm(); }

	// Must be a unit vector; otherwise multiplies the result by abs(u)
	Point rotate(const Point& u) const { return dot_cross(conj(u), *this); }
	Point unrotate(const Point& u) const { return dot_cross(u, *this); }

	friend bool lex_less(const Point& a, const Point& b) {
		return std::tie(a.x, a.y) < std::tie(b.x, b.y);
	}

	friend bool same_dir(const Point& a, const Point& b) { return cross(a,b) == 0 && dot(a,b) > 0; }

	// check if 180 <= s..t < 360
	friend bool is_reflex(const Point& a, const Point& b) { auto c = cross(a,b); return c ? (c < 0) : (dot(a, b) < 0); }

	// operator < (s,t) for angles in [base,base+2pi)
	friend bool angle_less(const Point& base, const Point& s, const Point& t) {
		int r = is_reflex(base, s) - is_reflex(base, t);
		return r ? (r < 0) : (0 < cross(s, t));
	}

	friend auto angle_cmp(const Point& base) {
		return [base](const Point& s, const Point& t) { return angle_less(base, s, t); };
	}
	friend auto angle_cmp_center(const Point& center, const Point& dir) {
		return [center, dir](const Point& s, const Point& t) -> bool { return angle_less(dir, s-center, t-center); };
	}

	// is p in [s,t] taken ccw? 1/0/-1 for in/border/out
	friend int angle_between(const Point& s, const Point& t, const Point& p) {
		if (same_dir(p, s) || same_dir(p, t)) return 0;
		return angle_less(s, p, t) ? 1 : -1;
	}
};

using ll = int64_t;
using P = Point<ll>;

int main(){
	ios_base::sync_with_stdio(false), cin.tie(nullptr);
	int N;
	cin >> N;
	vector<P> pts(N);
	for(int i = 0; i < N; i++) cin >> pts[i];
	vector<vector<double> > dp(N, vector<double>(1 << N, 1e9));
	dp[0][0] = 0;
	for(int mask = 0; mask < (1 << N); mask++){
		for(int a = 0; a < N; a++){
			for(int b = 0; b < N; b++){
				if(1 & (mask >> b)) continue;
				auto& nxt = dp[b][mask ^ (1 << b)];
				nxt = min(nxt, (pts[a] - pts[b]).dist() + dp[a][mask]);
			}
		}
	}
	cout << fixed << setprecision(10);
	cout << dp[0][(1 << N) - 1] << '\n';
}
#elif other2
// #include <iostream>
// #include <vector>
// #include <math.h>

// #define endl '\n'
// #define LL long long
// #define INF (1'000'000'000)

using namespace std;

int n;
LL x[10], y[10];
double W[10][10];
double cost[10][1 << 10];

double getDist(int A, int B) {
	return sqrt((x[A] - x[B]) * (x[A] - x[B])
		+ (y[A] - y[B]) * (y[A] - y[B]));
}

double DFS(int idx, int cur, int bits) {
	if (bits == ((1 << n) - 1)) {
		if (!W[cur][0])
			return INF;
		return W[cur][0];
	}
	if (cost[cur][bits] != INF)
		return cost[cur][bits];

	for (int next = 0; next < n; next++) {
		if (!W[cur][next])
			continue;
		if (bits & (1 << next))
			continue;
		cost[cur][bits] = min(cost[cur][bits],
			W[cur][next] + DFS(idx + 1, next, bits | (1 << next)));
	}
	return cost[cur][bits];
}

int main(void)
{
	ios_base::sync_with_stdio(false); cin.tie(0); cout.tie(0);
	double res = INF;

	cin >> n;
	for (int i = 0; i < n; i++) {
		cin >> x[i] >> y[i];
		for (int j = 0; j < (1 << n); j++)
			cost[i][j] = INF;
	}
	for (int i = 0; i < n; i++)
		for (int j = 0; j < n; j++)
			W[i][j] = getDist(i, j);
	
	res = DFS(0, 0, 1);

	cout.precision(13);
	cout << res;
	return 0;
}
#endif
}
