using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 24
이름 : 배성훈
내용 : 반복 부분문자열
    문제번호 : 1605번

    문자열, 해싱, 접미사 배열과 lcp 배열, 라빈 - 카프 문제다
    
    반복 문자열은 lcp 배열의 정의 중 접두사 배열의 부분문자열이된다
    그래서 lcp 의 최대값을 찾아 제출하니 이상없이 통과한다
    다른 사람의 풀이를 보니 other5 라빈 - 카프로 풀었다
*/

namespace BaekJoon._56
{
    internal class _56_06
    {

        static void Main6(string[] args)
        {

            StreamReader sr;

            string str;
            int[] lcp, sfx, ord, calc;
            int len, d;

            Solve();

            void Solve()
            {

                Input();

                SetSFX();
                SetLCP();

                Console.Write(GetRet());
            }
            
            int GetRet()
            {

                int ret = 0;
                for (int i = 0; i < len; i++)
                {

                    if (ret < lcp[i]) ret = lcp[i];
                }

                return ret;
            }

            void SetLCP()
            {

                for (int i = 0; i < len; i++)
                {

                    calc[sfx[i]] = i;
                }

                for (int k = 0, i = 0; i < len; i++)
                {

                    if (calc[i] == 0) continue;
                    for (int j = sfx[calc[i] - 1]; Math.Max(i, j) + k < len && str[i + k] == str[j + k]; k++) { }
                    lcp[calc[i]] = k > 0 ? k-- : 0;
                }
            }

            void SetSFX()
            {

                for (int i = 0; i < len; i++)
                {

                    sfx[i] = i;
                    ord[i] = str[i] - 'a';
                }

                for (d = 1; ; d <<= 1)
                {

                    Array.Sort(sfx, (x, y) => MyComp(x, y));

                    calc[sfx[0]] = 0;

                    for (int i = 1; i < len; i++)
                    {

                        calc[sfx[i]] = calc[sfx[i - 1]] + (MyComp(sfx[i], sfx[i - 1]) > 0 ? 1 : 0);
                    }

                    int[] temp = ord;
                    ord = calc;
                    calc = temp;

                    if (ord[sfx[len - 1]] == len - 1) break;
                }
            }

            int MyComp(int _idx1, int _idx2)
            {

                if (ord[_idx1] != ord[_idx2]) return ord[_idx1].CompareTo(ord[_idx2]);

                _idx1 += d;
                _idx2 += d;

                return (_idx1 < len && _idx2 < len) ? ord[_idx1].CompareTo(ord[_idx2]) : _idx2.CompareTo(_idx1);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 4);
                len = ReadInt();
                str = sr.ReadLine();

                ord = new int[len];
                sfx = new int[len];
                lcp = new int[len];
                calc = new int[len];

                sr.Close();
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
using System;
using System.Linq;

public class Program
{
    static void Main()
    {
        int l = int.Parse(Console.ReadLine());
        string s = Console.ReadLine();
        int[] sa = GetSuffixArray(s), lcp = GetLCPArray(s, sa);
        Console.Write(lcp.Max());
    }
    static int[] GetSuffixArray(string s)
    {
        int n = s.Length;
        int[] sa = new int[n], rank = new int[n + 1];
        rank[n] = 0;
        for (int i = 0; i < n; i++)
        {
            sa[i] = i;
            rank[i] = Math.Max(0, s[i] - 'a' + 1);
        }
        int t = 1;
        int Compare(int a, int b)
        {
            if (rank[a] == rank[b])
            {
                int x = a + t >= n ? -1 : rank[a + t], y = b + t >= n ? -1 : rank[b + t];
                return x - y;
            }
            return rank[a] - rank[b];
        }
        int[] temp = new int[n], count = new int[Math.Max(26, n) + 1];
        while (t < n)
        {
            Array.Fill(count, 0);
            for (int i = 0; i < n; i++)
            {
                count[rank[Math.Min(n, i + t)]]++;
            }
            for (int i = 1; i <= 26 || i <= n; i++)
            {
                count[i] += count[i - 1];
            }
            for (int i = n - 1; i >= 0; i--)
            {
                temp[--count[rank[Math.Min(n, i + t)]]] = i;
            }
            Array.Fill(count, 0);
            for (int i = 0; i < n; i++)
            {
                count[rank[i]]++;
            }
            for (int i = 1; i <= 26 || i <= n; i++)
            {
                count[i] += count[i - 1];
            }
            for (int i = n - 1; i >= 0; i--)
            {
                sa[--count[rank[temp[i]]]] = temp[i];
            }
            temp[sa[0]] = 1;
            for (int i = 1; i < n; i++)
            {
                if (Compare(sa[i - 1], sa[i]) < 0)
                    temp[sa[i]] = temp[sa[i - 1]] + 1;
                else
                    temp[sa[i]] = temp[sa[i - 1]];
            }
            for (int i = 0; i < n; i++)
            {
                rank[i] = temp[i];
            }
            t *= 2;
        }
        return sa;
    }
    static int[] GetLCPArray(string s, int[] sa)
    {
        int n = s.Length;
        int[] lcp = new int[n], rank = new int[n];
        for (int i = 0; i < n; i++)
        {
            rank[sa[i]] = i;
        }
        int len = 0;
        for (int i = 0; i < n; i++)
        {
            if (rank[i] == 0)
                lcp[rank[i]] = 0;
            else
            {
                int j = sa[rank[i] - 1];
                while (i + len < n && j + len < n && s[i + len] == s[j + len])
                {
                    len++;
                }
                lcp[rank[i]] = len;
                if (len > 0)
                    len--;
            }
        }
        return lcp;
    }
}
#elif other2
// #include <bits/stdc++.h>
using namespace std;
using ll=long long;

struct sa_edges {
	union { int *p; struct { int u, v, w; }; } e;
	uint8_t a, b, c, t;
	int operator[](size_t x) { return t>7 ? e.p[x] : e.u&-(a==x) | e.v&-(b==x) | e.w&-(c==x); }
	void set(size_t x, int y) {
		if(t>7) e.p[x]=y;
		else if(t==0 || a==x) t|=1, a=x, e.u=y;
		else if(t==1 || b==x) t|=2, b=x, e.v=y;
		else if(t==3 || c==x) t|=4, c=x, e.w=y;
		else { t|=8; auto p=new int[52]{}; p[a]=e.u, p[b]=e.v, p[c]=e.w, p[x]=y; e.p=p; }
	}
	void cpy() { if(t<=7) return; auto p=e.p; e.p=new int[52]; copy(p, p+52, e.p); }
};
struct sa_node {
	int n, p;
	sa_edges e;
};
struct sa {
	vector<sa_node> a;
	int n, l;
	sa(size_t m): a(m*2+1), n(1), l(1) {}
	sa_node operator[](size_t i) { return a[i]; }
	void append(size_t c) {
		int v=l, u;
		l=++n, a[l].n=a[v].n+1, a[l].p=1;
		while(v && !(u=a[v].e[c])) a[v].e.set(c, l), v=a[v].p;
		if(!v) return;
		if(a[u].n==a[v].n+1) { a[l].p=u; return; }
		a[++n]={a[v].n+1, a[u].p, a[u].e};
		a[n].e.cpy();
		a[u].p=a[l].p=n;
		while(v && a[v].e[c]==u) a[v].e.set(c, n), v=a[v].p;
	}
};

int main() {
	ios::sync_with_stdio(0);cin.tie(0);
	int N, ans=0;
	string s;
	cin>>N>>s;
	sa a(N);
	for(char c:s) {
		a.append(c-'a');
		ans=max(ans, a[a[a.l].p].n);
	}
	cout<<ans<<'\n';
}
#elif other3
// #include <bits/stdc++.h>
// #ifdef JOON
// #define debug(...) fprintf(stderr, __VA_ARGS__)
// #else
// #define debug(...) 42
// #endif
using namespace std;

    class suffix_array
    {
        public:
    vector<int> s;
        vector<int> aux;
        vector<int> sa;
        vector<int> lcp;
        int sigma;

        suffix_array(const string &s_) : s(s_.size()), aux(s_.size()), sa(s_.size())
        {
            int n = static_cast<int>(s_.size());
            auto msc = numeric_limits < typename string::value_type >::min();
            auto mxc = numeric_limits < typename string::value_type >::max();
            vector<int> ch(mxc -msc + 1);
            for (int i = 0; i < n; i++)
            {
                ch[s_[i] - msc] = 1;
            }
            for (int i = 1; i <= mxc - msc; i++)
            {
                ch[i] += ch[i - 1];
            }
            for (int i = 0; i < n; i++)
            {
                s[i] = ch[s_[i] - msc] - 1;
            }
            sigma = ch[mxc - msc];
        }

        template<class T>
    suffix_array(const T* s_, size_t n_) : s(n_), aux(n_), sa(n_)
        {
            int n = static_cast<int>(n_);
            auto msc = numeric_limits < T >::min();
            auto mxc = numeric_limits < T >::max();
            vector<int> ch(mxc -msc + 1);
            for (int i = 0; i < n; i++)
            {
                ch[s_[i] - msc] = 1;
            }
            for (int i = 1; i <= mxc - msc; i++)
            {
                ch[i] += ch[i - 1];
            }
            for (int i = 0; i < n; i++)
            {
                s[i] = ch[s_[i] - msc] - 1;
            }
            sigma = ch[mxc - msc];
        }

        suffix_array(const char* s_) {
        suffix_array(s_, strlen(s_));
    }

    template<class Container>
    suffix_array(const Container &v) : s(v.size())
    {
        int n = static_cast<int>(v.size());
        vector<int> ch(n +1);
        for (int i = 0; i < n; i++)
        {
            ch[v[i]] = 1;
        }
        for (int i = 1; i <= n; i++)
        {
            ch[i] += ch[i - 1];
        }
        for (int i = 0; i < n; i++)
        {
            s[i] = ch[v[i]] - 1;
        }
        sigma = ch[n];
    }

    void sa_is(const vector<int> &S, int sig, vector<int> &SA)
    {
        int n = static_cast<int>(S.size());
        if (n == 1)
        {
            SA[0] = 0;
            return;
        }
        vector<bool> T(n);
        vector<int> B(sig +1), ptr(sig);
        int m = 0;
        for (int i = n - 2; i >= 0; i--)
        {
            if (S[i] < S[i + 1])
            {
                T[i] = true;
            }
            else if (S[i] == S[i + 1])
            {
                T[i] = T[i + 1];
            }
            else
            {
                m += T[i + 1];
            }
        }
        for (int i = 0; i < n; i++)
        {
            SA[i] = -1;
            B[S[i] + 1]++;
        }
        for (int i = 1; i <= sig; i++)
        {
            B[i] += B[i - 1];
        }
        if (m)
        {
            vector<int> P1(m +1), S1(m), SA1(m);
            for (int i = 0; i < sig; i++)
            {
                ptr[i] = B[i + 1];
            }
            for (int i = 1, j = 0; i < n; i++)
            {
                if (T[i] && !T[i - 1])
                {
                    aux[i] = j;
                    P1[j++] = i;
                    SA[--ptr[S[i]]] = i;
                }
            }
            P1[m] = n;
            for (int i = 0; i < sig; i++)
            {
                ptr[i] = B[i];
            }
            SA[ptr[S[n - 1]]++] = n - 1;
            for (int i = 0; i < n; i++)
            {
                int j = SA[i] - 1;
                if (j >= 0 && !T[j])
                {
                    SA[ptr[S[j]]++] = j;
                }
            }
            for (int i = 0; i < sig; i++)
            {
                ptr[i] = B[i + 1];
            }
            for (int i = n - 1; i >= 0; i--)
            {
                int j = SA[i] - 1;
                if (j >= 0 && T[j])
                {
                    SA[--ptr[S[j]]] = j;
                }
            }
            for (int i = 0, j = 0; i < n; i++)
            {
                if (SA[i] > 0 && T[SA[i]] && !T[SA[i] - 1])
                {
                    SA1[j++] = aux[SA[i]];
                }
            }
            for (int i = 1; i < m; i++)
            {
                int q1 = SA1[i - 1];
                int q2 = SA1[i];
                int j1 = P1[q1];
                int n1 = P1[q1 + 1] - j1;
                int j2 = P1[q2];
                int n2 = P1[q2 + 1] - j2;
                S1[q2] = S1[q1];
                if (n1 != n2)
                {
                    S1[q2]++;
                }
                else
                {
                    for (int j = 0; j < n1; j++)
                    {
                        if (S[j1 + j] < S[j2 + j] || T[j1 + j] < T[j2 + j])
                        {
                            S1[q2]++;
                            break;
                        }
                    }
                }
            }
            int sig1 = S1[SA1[m - 1]] + 1;
            if (sig1 < m)
            {
                sa_is(S1, sig1, SA1);
            }
            for (int i = 0; i < n; i++)
            {
                SA[i] = -1;
            }
            for (int i = 0; i < sig; i++)
            {
                ptr[i] = B[i + 1];
            }
            for (int i = m - 1; i >= 0; i--)
            {
                int j = P1[SA1[i]];
                SA[--ptr[S[j]]] = j;
            }
        }
        for (int i = 0; i < sig; i++)
        {
            ptr[i] = B[i];
        }
        SA[ptr[S[n - 1]]++] = n - 1;
        for (int i = 0; i < n; i++)
        {
            int j = SA[i] - 1;
            if (j >= 0 && !T[j])
            {
                SA[ptr[S[j]]++] = j;
            }
        }
        for (int i = 0; i < sig; i++)
        {
            ptr[i] = B[i + 1];
        }
        for (int i = n - 1; i >= 0; i--)
        {
            int j = SA[i] - 1;
            if (j >= 0 && T[j])
            {
                SA[--ptr[S[j]]] = j;
            }
        }
    }

    void run()
    {
        int n = static_cast<int>(s.size());
        sa_is(s, sigma, sa);
        lcp.resize(n - 1);
        vector<int> rnk(n);
        for (int i = 0; i < n; i++)
        {
            rnk[sa[i]] = i;
        }
        s.push_back(-1);
        for (int i = 0, j = 0; i < n; i++)
        {
            int r = rnk[i];
            if (r)
            {
                while (s[i + j] == s[sa[r - 1] + j])
                {
                    j++;
                }
                lcp[r - 1] = j;
                if (j)
                {
                    j--;
                }
            }
        }
        s.pop_back();
    }
};

int main()
{
    ios::sync_with_stdio(false);
    cin.tie(NULL);
    int n;
    string s;
    cin >> n >> s;
    suffix_array sa(s);
    sa.run();
    int mx = 0;
    for (int i = 0; i < n - 1; i++)
    {
        mx = max(mx, sa.lcp[i]);
    }
    cout << mx << "\n";
    return 0;
}

#elif other4
// #include <bits/stdc++.h>
using namespace std;

// #if __cplusplus < 202002L
template <class T> int ssize(const T& a){ return a.size(); }
// #endif
template <class T1, class T2> istream& operator>> (istream& in, pair <T1, T2>& a){ in >> a.first >> a.second; return in; }
template <class T> istream& operator>> (istream& in, vector <T>& a){ for (auto &x: a){ in >> x; } return in; }

using ll = long long;
using ld = long double;

struct suffix_array{
	vector<int> sa_naive(const vector<int> &s){
		int n = (int)s.size();
		vector<int> sa(n);
		iota(sa.begin(), sa.end(), 0);
		sort(sa.begin(), sa.end(), [&](int l, int r){
			if(l == r) return false;
			for(; l < n && r < n; ++ l, ++ r) if(s[l] != s[r]) return s[l] < s[r];
			return l == n;
		});
		return sa;
	}
	vector<int> sa_doubling(const vector<int> &s){
		int n = (int)s.size();
		vector<int> sa(n), pos = s, tmp(n);
		iota(sa.begin(), sa.end(), 0);
		for(auto k = 1; k < n; k <<= 1){
			auto cmp = [&](int x, int y){
				if(pos[x] != pos[y]) return pos[x] < pos[y];
				int rx = x + k < n ? pos[x + k] : -1;
				int ry = y + k < n ? pos[y + k] : -1;
				return rx < ry;
			};
			sort(sa.begin(), sa.end(), cmp);
			tmp[sa[0]] = 0;
			for(auto i = 1; i < n; ++ i) tmp[sa[i]] = tmp[sa[i - 1]] + (cmp(sa[i - 1], sa[i]) ? 1 : 0);
			swap(tmp, pos);
		}
		return sa;
	}
	template<int THRESHOLD_NAIVE = 10, int THRESHOLD_DOUBLING = 40>
	vector<int> sa_is(const vector<int> &s, int sigma){
		int n = (int)s.size();
		if(n == 0) return {};
		if(n == 1) return {0};
		if(n == 2){
			if(s[0] < s[1]) return {0, 1};
			else return {1, 0};
		}
		if(n < THRESHOLD_NAIVE) return sa_naive(s);
		if(n < THRESHOLD_DOUBLING) return sa_doubling(s);
		vector<int> sa(n);
		vector<bool> ls(n);
		for(auto i = n - 2; i >= 0; -- i) ls[i] = (s[i] == s[i + 1]) ? ls[i + 1] : (s[i] < s[i + 1]);
		vector<int> sum_l(sigma), sum_s(sigma);
		for(auto i = 0; i < n; ++ i){
			if(!ls[i]) ++ sum_s[s[i]];
			else ++ sum_l[s[i] + 1];
		}
		for(auto i = 0; i < sigma; ++ i){
			sum_s[i] += sum_l[i];
			if(i + 1 < sigma) sum_l[i + 1] += sum_s[i];
		}
		auto induce = [&](const vector<int> &lms){
			fill(sa.begin(), sa.end(), -1);
			vector<int> buf(sigma);
			copy(sum_s.begin(), sum_s.end(), buf.begin());
			for(auto d: lms){
				if(d == n) continue;
				sa[buf[s[d]] ++] = d;
			}
			copy(sum_l.begin(), sum_l.end(), buf.begin());
			sa[buf[s[n - 1]] ++] = n - 1;
			for(auto i = 0; i < n; ++ i){
				int v = sa[i];
				if(v >= 1 && !ls[v - 1]) sa[buf[s[v - 1]] ++] = v - 1;
			}
			copy(sum_l.begin(), sum_l.end(), buf.begin());
			for(auto i = n - 1; i >= 0; -- i){
				int v = sa[i];
				if(v >= 1 && ls[v - 1]) sa[-- buf[s[v - 1] + 1]] = v - 1;
			}
		};
		vector<int> lms_map(n + 1, -1);
		int m = 0;
		for(auto i = 1; i < n; ++ i) if(!ls[i - 1] && ls[i]) lms_map[i] = m ++;
		vector<int> lms;
		lms.reserve(m);
		for(auto i = 1; i < n; ++ i) if(!ls[i - 1] && ls[i]) lms.push_back(i);
		induce(lms);
		if(m){
			vector<int> sorted_lms;
			sorted_lms.reserve(m);
			for(auto v: sa) if(lms_map[v] != -1) sorted_lms.push_back(v);
			vector<int> rec_s(m);
			int rec_sigma = 0;
			rec_s[lms_map[sorted_lms[0]]] = 0;
			for(auto i = 1; i < m; ++ i){
				int l = sorted_lms[i - 1], r = sorted_lms[i];
				int end_l = (lms_map[l] + 1 < m) ? lms[lms_map[l] + 1] : n;
				int end_r = (lms_map[r] + 1 < m) ? lms[lms_map[r] + 1] : n;
				bool same = true;
				if(end_l - l != end_r - r) same = false;
				else{
					for(; l < end_l; ++ l, ++ r) if (s[l] != s[r]) break;
					if(l == n || s[l] != s[r]) same = false;
				}
				if(!same) ++ rec_sigma;
				rec_s[lms_map[sorted_lms[i]]] = rec_sigma;
			}
			auto rec_sa = sa_is<THRESHOLD_NAIVE, THRESHOLD_DOUBLING>(rec_s, rec_sigma + 1);
			for(auto i = 0; i < m; ++ i) sorted_lms[i] = lms[rec_sa[i]];
			induce(sorted_lms);
		}
		return sa;
	}
	int n;
	// data: sorted sequence of suffices including the empty suffix
	// pos[i]: position of the suffix i in the suffix array
	// lcp[i]: longest common prefix of data[i] and data[i + 1]
	vector<int> data, pos, lcp;
	// O(n + sigma)
	suffix_array(const vector<int> &s, int sigma): n((int)s.size()), pos(n + 1), lcp(n){
		assert(0 <= sigma);
		for(auto d: s) assert(0 <= d && d < sigma);
		data = sa_is(s, sigma);
		data.insert(data.begin(), n);
		for(auto i = 0; i <= n; ++ i) pos[data[i]] = i;
		for(auto i = 0, h = 0; i <= n; ++ i){
			if(h > 0) -- h;
			if(pos[i] == 0) continue;
			int j = data[pos[i] - 1];
			for(; j + h <= n && i + h <= n; ++ h) if((j + h == n) != (i + h == n) || j + h < n && s[j + h] != s[i + h]) break;
			lcp[pos[i] - 1] = h;
		}
	}
	// O(n * log(n)) time, O(n) space
	template<class T>
	suffix_array(const vector<T> &s): n((int)s.size()), pos(n + 1), lcp(n){
		vector<int> idx(n);
		iota(idx.begin(), idx.end(), 0);
		sort(idx.begin(), idx.end(), [&](int l, int r){ return s[l] < s[r]; });
		vector<int> s2(n);
		int now = 0;
		for(auto i = 0; i < n; ++ i){
			if(i && s[idx[i - 1]] != s[idx[i]]) ++ now;
			s2[idx[i]] = now;
		}
		data = sa_is(s2, now + 1);
		data.insert(data.begin(), n);
		for(auto i = 0; i <= n; ++ i) pos[data[i]] = i;
		for(auto i = 0, h = 0; i <= n; ++ i){
			if(h > 0) -- h;
			if(pos[i] == 0) continue;
			int j = data[pos[i] - 1];
			for(; j + h <= n && i + h <= n; ++ h) if((j + h == n) != (i + h == n) || j + h < n && s[j + h] != s[i + h]) break;
			lcp[pos[i] - 1] = h;
		}
	}
	// rmq must be built over lcp
	// O(1)
	template<class RangeMinQuery_t>
	int longest_common_prefix(int i, int j, const RangeMinQuery_t &rmq) const{
		assert(0 <= i && i <= n && 0 <= j && j <= n);
		return i == j ? n - i : rmq.query(min(pos[i], pos[j]), max(pos[i], pos[j]));
	}
	// rmq must be built over lcp
	// Compares s[p, p + len) and s[q, q + len)
	// O(1)
	template<class RangeMinQuery_t>
	bool compare(int p, int q, int len, const RangeMinQuery_t &rmq) const{
		assert(0 <= min({p, q, len}) && p + len <= n && q + len <= n);
		if(len == 0 || p == q) return false;
		int common_len = longest_common_prefix(p, q, rmq);
		if(common_len >= len) return false;
		return data[p] < data[q];
	}
};

signed main(){
	ios_base::sync_with_stdio(false);
	cin.tie(nullptr);
	if (fopen("KEK.inp", "r")){
		freopen("KEK.inp", "r", stdin);
		freopen("KEK.out", "w", stdout);
	}
	
	int n;
	cin >> n;
	string _s;
	cin >> _s;

	vector <char> s(begin(_s), end(_s));
	auto sa = suffix_array(s);

	auto ans = *max_element(begin(sa.lcp), end(sa.lcp));
	cout << ans << "\n";
}
#elif other5
// 라빈카프

// #pragma GCC optimize("Ofast")
// #include<stdio.h>
// #include<string.h>
const long long HASHL (2000091);

char str[200003];
int L, ans, map[2000091];

bool dec (int len) {

	memset (map, -1, sizeof map);

	int hash = 0, sub = 1;
	for (int i=0; i<len; i++) {
		hash = (hash * 26 + str[i]) % HASHL;
		sub = (sub * 26) % HASHL;
	}

	sub = HASHL - sub;

	for (int i=len; i<=L; i++) {
		int ptr = map[hash];

		if (~ptr) {
			for (int k = 0; k < len; k++) {
				if (str[ptr + k] != str[i - len + k]) goto hell;
			}
			return 1;
hell:;
		}

		map[hash] = i - len;
		hash = (hash * 26 + sub * str[i-len] + str[i]) % HASHL;
	}

	return 0;
}

int main() {

	scanf ("%d%s", &L, str);
	for (int le = 0, ri = L-1; le <= ri;) {
		int m = (le + ri) / 2;
		if (dec (m)) {
			ans = m > ans ? m : ans;
			le = m + 1;
		}
		else ri = m - 1;
	}

	printf ("%d\n", ans);
	return 0;
}
#endif
}
