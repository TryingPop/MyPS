using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 15
이름 : 배성훈
내용 : 정원 (Hard)
    문제번호 : 24050번

    직접 표로 적어 경우의 수를 찾아봤다
    찾아야할껀 2로 나눈 나머지였고
    그러니 다음과 같은 결과를 얻었다    
        k, 0 이 나오는 경우는 n + m - 2 - k C n - 1
        0, k 가 나오는 경우의 수는 n - m - 2 - k C m - 1

    이후에 뤼카의 정리로 m - 1 = 2^i * p_i + 2^(i - 1) * p_(i - 1) + ... + p_0
    에서 0 <= k <= n - 1인 k에 대해 n + m - 2 - k = 2^i * a_i + 2^(i - 1) * a_(i - 1) + ... + a_0
    인 부분에서 p_i != 0인 모든 i에 대해 때, a_i != 0인지 확인해줘야한다
    만약 적어도 1개가 a_i = 0인 경우 n + m - 2 - k C m - 1 = 0이고,
    모두 a_i != 0인 경우면, 1 C 0 나 1 C 1 의 곱들로 표현되므로
    ((n + m - 2 - k) C (m - 1)) = (1 C 0)^q * (1 C 1)^w = 1^q * 1^w = 1 이다
    비슷하게 n - 1도 해서 찾아주면 된다

    아래는 해당 아이디어를 코드로 나타낸것 이다
    시간은 72ms로 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0233
    {

        static void Main233(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int rowLen = ReadInt(sr);
            int colLen = ReadInt(sr);

            int[] row = new int[rowLen];

            for (int i = 0; i < rowLen; i++)
            {

                // 연산은 곱셈이고 , 1 -> -1, 0 -> 1로 하고
                // 해당 규칙을 곱셈으로 하면 군 동형이된다!
                int chk = ReadInt(sr);
                row[i] = chk == 0 ? 1 : -1;
            }
            int[] col = new int[colLen];

            for (int i = 0; i < colLen; i++)
            {

                int chk = ReadInt(sr);
                col[i] = chk == 0 ? 1 : -1;
            }

            sr.Close();

            int calc1 = 0;
            int calc2 = 0;

            // (1 << 16) > 10만
            for (int i = 0; i < 17; i++)
            {

                // 뤼카의 정리 적용
                calc1 |= ((rowLen - 1) & (1 << i));
                calc2 |= ((colLen - 1) & (1 << i));
            }

            int ret = 1;
            for (int k = 0; k < rowLen; k++)
            {

                // rowLen이 나오는 횟수가 홀수인지 짝수인지 찾는다
                // 만약 row부분은 
                // (rowLen + colLen - 2 - k) C (colLen - 1)
                // 의 형태이다
                // 여기서 우리가 찾아야 할껀 mod 2연산한 값이다
                // 그래서 colLen - 1에서 1인 비트를 모두 포함안하면 
                // 적어도 하나에 대해 0 C 1 = 0이 나온다
                // 그래서 곱하면 0이되므로강제 짝수다
                // 반면 모두 포함하면 1 C 1 or 1 C 0으로만 이루어져 있고
                // 홀수개라는 의미다!
                int calc = rowLen + colLen - 2 - k;
                if ((calc & calc2) == calc2) ret *= row[k];
            }

            for (int k = 0; k < colLen; k++)
            {

                // 여기는
                // (rowLen + colLen - 2 - k) C (rowLen - 1)
                // 의 형태이다
                // 앞과 같이 뤼카의 정리 적용
                int calc = colLen + rowLen - 2 - k;
                if ((calc & calc1) == calc1) ret *= col[k];
            }

            // 기존 문제로 되돌리기
            if (ret == 1) Console.WriteLine(0);
            else Console.WriteLine(1);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
// #include <bits/stdc++.h>
// #define sz(v) ((int)(v).size())
// #define all(v) (v).begin(), (v).end()
using namespace std;
typedef long long lint;
typedef pair<lint, lint> pi;
const int mod = 998244353; //1e9 + 7;//998244353;

template<typename T>
T gcd(const T &a, const T &b) {
    return b == T(0) ? a : gcd(b, a % b);
}

struct mint {
    int val;
    mint() { val = 0; }
    mint(const lint& v) {
        val = (-mod <= v && v < mod) ? v : v % mod;
        if (val < 0) val += mod;
    }

    friend ostream& operator<<(ostream& os, const mint& a) { return os << a.val; }
    friend bool operator==(const mint& a, const mint& b) { return a.val == b.val; }
    friend bool operator!=(const mint& a, const mint& b) { return !(a == b); }
    friend bool operator<(const mint& a, const mint& b) { return a.val < b.val; }

    mint operator-() const { return mint(-val); }
    mint& operator+=(const mint& m) { if ((val += m.val) >= mod) val -= mod; return *this; }
    mint& operator-=(const mint& m) { if ((val -= m.val) < 0) val += mod; return *this; }
    mint& operator*=(const mint& m) { val = (lint)val*m.val%mod; return *this; }
    friend mint ipow(mint a, lint p) {
        mint ans = 1; for (; p; p /= 2, a *= a) if (p&1) ans *= a;
        return ans;
    }
    friend mint inv(const mint& a) { assert(a.val); return ipow(a, mod - 2); }
    mint& operator/=(const mint& m) { return (*this) *= inv(m); }

    friend mint operator+(mint a, const mint& b) { return a += b; }
    friend mint operator-(mint a, const mint& b) { return a -= b; }
    friend mint operator*(mint a, const mint& b) { return a *= b; }
    friend mint operator/(mint a, const mint& b) { return a /= b; }
    operator int64_t() const {return val; }
};

struct point{
	lint x, y;
	bool operator<(const point &p)const {
		return x * p.y < p.x * y;
	}
};

struct car{
	lint t, v, cnt;
	point p;
	bool operator<(const car &c) const{
		return p < c.p;
	}
};

int main(){
	ios_base::sync_with_stdio(0);
	cin.tie(0);
	cout.tie(0);
	int n, m;
	cin >> n >> m;
	int ans = 0;
	auto bino = [&](int x, int y){
		return ((x + y) & y) == y;
	};
	for(int i = 0; i < n; i++){
		int x; cin >> x;
		if(x && bino(n - i - 1, m - 1)) ans ^= 1;
	}
	for(int i = 0; i < m; i++){
		int x; cin >> x;
		if(x && bino(n - 1, m - i - 1)) ans ^= 1;
	}
	cout << ans << "\n";
}
#elif other2

// #include <bits/stdc++.h>
using namespace std;;;;;;;;;;;;;;;;
int f(int x, int y){
    return ((x-y)&y)==0;
}
int main(){
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);
    int n,m;
    cin >> n >> m;
    int ans=0;
    for(int i=0;i<n;i++){
        int x;
        cin >> x;
        if(x && f(n-1-i+m-1,m-1)) ans=!ans;
    }
    for(int i=0;i<m;i++){
        int x;
        cin >> x;
        if(x && f(m-1-i+n-1,n-1)) ans=!ans;
    }
    cout << ans;
}
#endif
}
