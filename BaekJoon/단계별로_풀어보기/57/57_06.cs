using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 11
이름 : 배성훈
내용 : 수열과 쿼리 13
    문제번호 : 13925번

    느리게 갱신되는 세그먼트 트리 문제다
    ... Add쪽에 lazy 세팅하는데 인덱스를 잘못 해서 5번 틀렸다;

    아이디어는 다음과 같다
    변화, 덧셈, 곱셈 따로 lazy에 저장했다
    변화의 경우 갱신되면 이전 곱셈과 덧셈을 모두 초기화한다

    그리고 곱셈의 경우 분배법칙으로
    lazy에 곱셈과 덧셈에 해당 값만큼 각각 곱해준다

    덧셈의 경우는 덧셈만 변형한다
    이렇게 제출하니 이상없이 통과한다...
    인덱스 에러로 시간을 꽤썼다
*/

namespace BaekJoon._57
{
    internal class _57_06
    {

        static void Main6(string[] args)
        {

            int MOD = 1_000_000_007;

            (long val, long add, long mul, long change, bool lazy) ZERO = (0, 0, -1, -1, false);

            StreamReader sr;
            StreamWriter sw;

            int n;
            (long val, long add, long mul, long change, bool lazy)[] seg;
            int START = 1, END;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int len = ReadInt();

                for (int t = 0; t < len; t++)
                {

                    int op = ReadInt();
                    int x = ReadInt();
                    int y = ReadInt();

                    if (op < 4)
                    {

                        int v = ReadInt();

                        switch (op)
                        {

                            case 1:
                                UpdateAdd(START, END, x, y, v);
                                break;

                            case 2:
                                UpdateMul(START, END, x, y, v);
                                break;

                            case 3:
                                UpdateChange(START, END, x, y, v);
                                break;
                        }
                    }
                    else
                    {

                        long ret = GetVal(START, END, x, y);
                        sw.Write($"{ret}\n");
                    }
                }

                sw.Close();
                sr.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 4);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 4);

                int n = ReadInt();
                END = n;

                int log = (int)Math.Ceiling(Math.Log2(n)) + 1;

                seg = new (long val, long add, long mul, long change, bool lazy)[1 << log];

                Array.Fill(seg, ZERO);

                
                for (int i = 1; i <= n; i++)
                {

                    int val = ReadInt();
                    UpdateChange(START, END, i, i, val);
                }
            }

            void LazyUpdate(int _s, int _e, int _idx)
            {

                if (!seg[_idx].lazy) return;
                seg[_idx].lazy = false;

                long add = seg[_idx].add;
                long mul = seg[_idx].mul;
                long change = seg[_idx].change;

                if (change != -1) seg[_idx].val = ((_e - _s + 1) * change) % MOD;
                if (mul != -1) seg[_idx].val = (seg[_idx].val * mul) % MOD;
                seg[_idx].val = (seg[_idx].val + (_e - _s + 1) * add) % MOD;

                seg[_idx].add = 0;
                seg[_idx].mul = -1;
                seg[_idx].change = -1;

                if (_s == _e) return;

                int l = _idx * 2 + 1;
                int r = _idx * 2 + 2;

                seg[l].lazy = true;
                seg[r].lazy = true;

                if (change != -1)
                {

                    seg[l].change = change;
                    seg[r].change = change;

                    seg[l].mul = mul;
                    seg[r].mul = mul;

                    seg[l].add = add;
                    seg[r].add = add;

                    return;
                }

                if (mul != -1)
                {

                    if (seg[l].mul == -1) seg[l].mul = mul;
                    else seg[l].mul = (seg[l].mul * mul) % MOD;

                    if (seg[r].mul == -1) seg[r].mul = mul;
                    else seg[r].mul = (seg[r].mul * mul) % MOD;

                    seg[l].add = (seg[l].add * mul) % MOD;
                    seg[r].add = (seg[r].add * mul) % MOD;
                }

                seg[l].add = (seg[l].add + add) % MOD;
                seg[r].add = (seg[r].add + add) % MOD;
            }

            long GetVal(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                LazyUpdate(_s, _e, _idx);

                if (_e < _chkS || _chkE < _s) return 0;
                else if (_chkS <= _s && _e <= _chkE) return seg[_idx].val;

                int mid = (_s + _e) >> 1;
                return (GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1)
                        + GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2)) % MOD;
            }

            void UpdateChange(int _s, int _e, int _chkS, int _chkE, int _val, int _idx = 0)
            {

                LazyUpdate(_s, _e, _idx);

                if (_e < _chkS || _chkE < _s) return;
                else if (_chkS <= _s && _e <= _chkE)
                {

                    seg[_idx].val = (1L * (_e - _s + 1) * _val) % MOD;

                    if (_s != _e)
                    {

                        seg[_idx * 2 + 1].change = _val;
                        seg[_idx * 2 + 1].add = 0;
                        seg[_idx * 2 + 1].mul = -1;
                        seg[_idx * 2 + 1].lazy = true;

                        seg[_idx * 2 + 2].change = _val;
                        seg[_idx * 2 + 2].add = 0;
                        seg[_idx * 2 + 2].mul = -1;
                        seg[_idx * 2 + 2].lazy = true;
                    }
                    return;
                }

                int mid = (_s + _e) >> 1;
                UpdateChange(_s, mid, _chkS, _chkE, _val, _idx * 2 + 1);
                UpdateChange(mid + 1, _e, _chkS, _chkE, _val, _idx * 2 + 2);

                seg[_idx].val = (seg[_idx * 2 + 1].val + seg[_idx * 2 + 2].val) % MOD;
            }

            void UpdateAdd(int _s, int _e, int _chkS, int _chkE, int _val, int _idx = 0)
            {

                LazyUpdate(_s, _e, _idx);

                if (_e < _chkS || _chkE < _s) return;
                else if (_chkS <= _s && _e <= _chkE)
                {

                    seg[_idx].val = (seg[_idx].val + 1L * (_e - _s + 1) * _val) % MOD;

                    if (_s != _e)
                    {

                        seg[_idx * 2 + 1].add = (seg[_idx * 2 + 1].add + _val) % MOD;
                        seg[_idx * 2 + 1].lazy = true;

                        seg[_idx * 2 + 2].add = (seg[_idx * 2 + 2].add + _val) % MOD;
                        seg[_idx * 2 + 2].lazy = true;
                    }
                    return;
                }

                int mid = (_s + _e) >> 1;
                UpdateAdd(_s, mid, _chkS, _chkE, _val, _idx * 2 + 1);
                UpdateAdd(mid + 1, _e, _chkS, _chkE, _val, _idx * 2 + 2);

                seg[_idx].val = (seg[_idx * 2 + 1].val + seg[_idx * 2 + 2].val) % MOD;
            }

            void UpdateMul(int _s, int _e, int _chkS, int _chkE, int _val, int _idx = 0)
            {

                LazyUpdate(_s, _e, _idx);

                if (_e < _chkS || _chkE < _s) return;
                else if (_chkS <= _s && _e <= _chkE)
                {

                    seg[_idx].val = (seg[_idx].val * _val) % MOD;

                    if (_s != _e)
                    {

                        if (seg[_idx * 2 + 1].mul == -1) seg[_idx * 2 + 1].mul = _val;
                        else seg[_idx * 2 + 1].mul = (seg[_idx * 2 + 1].mul * _val) % MOD;

                        seg[_idx * 2 + 1].add = (seg[_idx * 2 + 1].add * _val) % MOD;
                        seg[_idx * 2 + 1].lazy = true;

                        if (seg[_idx * 2 + 2].mul == -1) seg[_idx * 2 + 2].mul = _val;
                        else seg[_idx * 2 + 2].mul = (seg[_idx * 2 + 2].mul * _val) % MOD;

                        seg[_idx * 2 + 2].add = (seg[_idx * 2 + 2].add * _val) % MOD;
                        seg[_idx * 2 + 2].lazy = true;
                    }

                    return;
                }

                int mid = (_s + _e) >> 1;
                UpdateMul(_s, mid, _chkS, _chkE, _val, _idx * 2 + 1);
                UpdateMul(mid + 1, _e, _chkS, _chkE, _val, _idx * 2 + 2);

                seg[_idx].val = (seg[_idx * 2 + 1].val + seg[_idx * 2 + 2].val) % MOD;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
class BOJ13925 {
    static long[] arr;
    static long[] tree;
    static long[,] lazy;
    static long MOD = 1000000007;
    
    static long init(int pos, int s, int e) {
        if(s == e) {
            return tree[pos] = arr[s];
        }
        int m = (s + e) / 2;
        return tree[pos] = (init(pos*2, s, m) + init(pos*2+1, m+1, e)) % MOD;
    }
    
    static void lazy_update(int pos, int s, int e) {
        if(lazy[pos, 0] == 1 && lazy[pos, 1] == 0) return;
        if(s != e) {
            lazy[pos*2, 0] = (lazy[pos, 0] * lazy[pos*2, 0]) % MOD;
            lazy[pos*2, 1] = (lazy[pos, 0] * lazy[pos*2, 1] + lazy[pos, 1]) % MOD;
            lazy[pos*2+1, 0] = (lazy[pos, 0] * lazy[pos*2+1, 0]) % MOD;
            lazy[pos*2+1, 1] = (lazy[pos, 0] * lazy[pos*2+1, 1] + lazy[pos, 1]) % MOD;
        }
        tree[pos] = (lazy[pos, 0] * tree[pos] + (e - s + 1) * lazy[pos, 1]) % MOD;
        lazy[pos, 0] = 1;
        lazy[pos, 1] = 0;
    }
    
    static void update(int pos, int s, int e, long l, long r, long mul, long sum) {
        lazy_update(pos, s, e);
        if(r < s || l > e) return;
        if(l <= s && e <= r) {
            lazy[pos, 0] = (lazy[pos, 0] * mul) % MOD;
            lazy[pos, 1] = (lazy[pos, 1] * mul) % MOD;
            lazy[pos, 1] = (lazy[pos, 1] + sum) % MOD;
            lazy_update(pos, s, e);
            return;
        }
        
        int m = (s + e) / 2;
        update(pos*2, s, m, l, r, mul, sum);
        update(pos*2+1, m+1, e, l, r, mul, sum);
        tree[pos] = (tree[pos*2] + tree[pos*2+1]) % MOD;
    }
    
    static long query(int pos, int s, int e, long l, long r) {
        lazy_update(pos, s, e);
        if(r < s || l > e) return 0;
        if(l <= s && e <= r) return tree[pos] % MOD;
        int m = (s + e) / 2;
        return (query(pos*2, s, m, l, r) + query(pos*2+1, m+1, e, l, r)) % MOD;
    }
    
    static void Main() {
        arr = new long[110000];
        tree = new long[440000];
        lazy = new long[440000,2];
        
        int N = int.Parse(Console.ReadLine());
        long[] line = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);
        for(int i=1 ; i<=N ; i++) {
            arr[i] = line[i-1];
        }
        init(1, 1, N);
        for(int i=0 ; i<440000 ; i++) {
            lazy[i, 0] = 1;
            lazy[i, 1] = 0;
        }
        
        int M = int.Parse(Console.ReadLine());
        for(int i=0 ; i<M ; i++) {
            line = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);
            if(line[0] == 1) update(1, 1, N, line[1], line[2], 1, line[3]);
            else if(line[0] == 2) update(1, 1, N, line[1], line[2], line[3], 0);
            else if(line[0] == 3) update(1, 1, N, line[1], line[2], 0, line[3]);
            else Console.WriteLine(query(1, 1, N, line[1], line[2]));
        }
    }
}
#elif other2
// #pragma GCC optimize("O3")
// #pragma GCC target("avx2")
// #include <bits/stdc++.h>
// #include <sys/stat.h>
// #include <sys/mman.h>
// #include <unistd.h>
using namespace std;

/*
 * Author : jinhan814
 * Date : 2021-05-06
 * Description : FastIO implementation for cin, cout. (mmap ver.)
 */
constexpr int SZ = 1 << 20;

class INPUT {
private:
	char* p;
	bool __END_FLAG__, __GETLINE_FLAG__;
public:
	explicit operator bool() { return !__END_FLAG__; }
    INPUT() {
        struct stat st; fstat(0, &st);
        p = (char*)mmap(0, st.st_size, PROT_READ, MAP_SHARED, 0, 0);
    }
	bool IsBlank(char c) { return c == ' ' || c == '\n'; }
	bool IsEnd(char c) { return c == '\0'; }
	char _ReadChar() { return *p++; }
	char ReadChar() {
		char ret = _ReadChar();
		for (; IsBlank(ret); ret = _ReadChar());
		return ret;
	}
	template<typename T> T ReadInt() {
		T ret = 0; char cur = _ReadChar(); bool flag = 0;
		for (; IsBlank(cur); cur = _ReadChar());
		if (cur == '-') flag = 1, cur = _ReadChar();
		for (; !IsBlank(cur) && !IsEnd(cur); cur = _ReadChar()) ret = 10 * ret + (cur & 15);
		if (IsEnd(cur)) __END_FLAG__ = 1;
		return flag ? -ret : ret;
	}
	string ReadString() {
		string ret; char cur = _ReadChar();
		for (; IsBlank(cur); cur = _ReadChar());
		for (; !IsBlank(cur) && !IsEnd(cur); cur = _ReadChar()) ret.push_back(cur);
		if (IsEnd(cur)) __END_FLAG__ = 1;
		return ret;
	}
	double ReadDouble() {
		string ret = ReadString();
		return stod(ret);
	}
	string getline() {
		string ret; char cur = _ReadChar();
		for (; cur != '\n' && !IsEnd(cur); cur = _ReadChar()) ret.push_back(cur);
		if (__GETLINE_FLAG__) __END_FLAG__ = 1;
		if (IsEnd(cur)) __GETLINE_FLAG__ = 1;
		return ret;
	}
	friend INPUT& getline(INPUT& in, string& s) { s = in.getline(); return in; }
} _in;

class OUTPUT {
private:
	char write_buf[SZ];
	int write_idx;
public:
	~OUTPUT() { Flush(); }
	explicit operator bool() { return 1; }
	void Flush() {
        write(1, write_buf, write_idx);
		write_idx = 0;
	}
	void WriteChar(char c) {
		if (write_idx == SZ) Flush();
		write_buf[write_idx++] = c;
	}
	template<typename T> int GetSize(T n) {
		int ret = 1;
		for (n = n >= 0 ? n : -n; n >= 10; n /= 10) ret++;
		return ret;
	}
	template<typename T> void WriteInt(T n) {
		int sz = GetSize(n);
		if (write_idx + sz >= SZ) Flush();
		if (n < 0) write_buf[write_idx++] = '-', n = -n;
		for (int i = sz; i --> 0; n /= 10) write_buf[write_idx + i] = n % 10 | 48;
		write_idx += sz;
	}
	void WriteString(string s) { for (auto& c : s) WriteChar(c); }
	void WriteDouble(double d) { WriteString(to_string(d)); }
} _out;

/* operators */
INPUT& operator>> (INPUT& in, char& i) { i = in.ReadChar(); return in; }
INPUT& operator>> (INPUT& in, string& i) { i = in.ReadString(); return in; }
template<typename T, typename std::enable_if_t<is_arithmetic_v<T>>* = nullptr>
INPUT& operator>> (INPUT& in, T& i) {
	if constexpr (is_floating_point_v<T>) i = in.ReadDouble();
	else if constexpr (is_integral_v<T>) i = in.ReadInt<T>(); return in; }

OUTPUT& operator<< (OUTPUT& out, char i) { out.WriteChar(i); return out; }
OUTPUT& operator<< (OUTPUT& out, string i) { out.WriteString(i); return out; }
template<typename T, typename std::enable_if_t<is_arithmetic_v<T>>* = nullptr>
OUTPUT& operator<< (OUTPUT& out, T i) {
	if constexpr (is_floating_point_v<T>) out.WriteDouble(i);
	else if constexpr (is_integral_v<T>) out.WriteInt<T>(i); return out; }

/* macros */
// #define fastio 1
// #define cin _in
// #define cout _out
// #define istream INPUT
// #define ostream OUTPUT

/*
 * Author : jinhan814
 * Date : 2022-04-16
 * Description : Non-recursive implementation of Segment Tree with Lazy Propagation
 */

template<typename NodeType,
         typename LazyType,
         typename F_Merge,
         typename F_Update,
         typename F_Composition>
struct LazySegTree { // 1-indexed
public:
    using A = NodeType;
    using B = LazyType;
    LazySegTree() : LazySegTree(0, A(), B()) {}
    explicit LazySegTree(int n, const A& e, const B& id)
        : n(n), e(e), id(id), lg(Log2(n)), sz(1 << lg), tree(sz << 1, e), lazy(sz, id) {}
    void Set(int i, const A& val) { tree[--i | sz] = val; }
    void Init() { for (int i = sz - 1; i; i--) tree[i] = M(tree[i << 1], tree[i << 1 | 1]); }
    void Update(int i, const B& f) {
        --i |= sz;
        for (int j = lg; j; j--) Push(i >> j);
        Apply(i, f);
        for (int j = 1; j <= lg; j++) Pull(i >> j);
    }
    void Update(int l, int r, const B& f) {
        --l |= sz, --r |= sz;
        for (int i = lg; i; i--) {
            if (l >> i << i != l) Push(l >> i);
            if (r + 1 >> i << i != r + 1) Push(r >> i);
        }
        for (int L = l, R = r; L <= R; L >>= 1, R >>= 1) {
            if (L & 1) Apply(L++, f);
            if (~R & 1) Apply(R--, f);
        }
        for (int i = 1; i <= lg; i++) {
            if (l >> i << i != l) Pull(l >> i);
            if (r + 1 >> i << i != r + 1) Pull(r >> i);
        }
    }
    A Query(int i) {
        --i |= sz;
        for (int j = lg; j; j--) Push(i >> j);
        return tree[i];
    }
    A Query(int l, int r) {
        A L = e, R = e; --l |= sz, --r |= sz;
        for (int i = lg; i; i--) {
            if (l >> i << i != l) Push(l >> i);
            if (r + 1 >> i << i != r + 1) Push(r >> i);
        }
        for (; l <= r; l >>= 1, r >>= 1) {
            if (l & 1) L = M(L, tree[l++]);
            if (~r & 1) R = M(tree[r--], R);
        }
        return M(L, R);
    }
private:
    const int n, lg, sz; const A e; const B id;
    vector<A> tree; vector<B> lazy;
    F_Merge M; F_Update U; F_Composition C;
    static int Log2(int n) {
        int ret = 0;
        while (n > 1 << ret) ret++;
        return ret;
    }
    void Apply(int i, const B& f) {
        tree[i] = U(f, tree[i]);
        if (i < sz) lazy[i] = C(f, lazy[i]);
    }
    void Push(int i) {
        Apply(i << 1, lazy[i]);
        Apply(i << 1 | 1, lazy[i]);
        lazy[i] = id;
    }
    void Pull(int i) {
        tree[i] = M(tree[i << 1], tree[i << 1 | 1]);
    }
};

constexpr int MOD = 1e9 + 7;

struct NodeType {
    int val, sz;
    NodeType() : NodeType(0, 0) {}
    constexpr NodeType(int val, int sz) : val(val), sz(sz) {}
};

constexpr NodeType e(0, 0);

struct LazyType {
    int mul, sum;
    LazyType() : LazyType(1, 0) {}
    constexpr LazyType(int mul, int sum) : mul(mul), sum(sum) {}
};

constexpr LazyType id(1, 0);

struct F_Merge {
    NodeType operator() (const NodeType& a, const NodeType& b) const {
        NodeType ret(a.val + b.val, a.sz + b.sz);
        if (ret.val >= MOD) ret.val -= MOD;
        return ret;
    }
};

struct F_Update {
    NodeType operator() (const LazyType& a, const NodeType& b) const {
        NodeType ret((1LL * a.mul * b.val + 1LL * a.sum * b.sz) % MOD, b.sz);
        return ret;
    }
};

struct F_Composition {
    LazyType operator() (const LazyType& a, const LazyType& b) const {
        LazyType ret(1LL * a.mul * b.mul % MOD, (1LL * a.mul * b.sum + a.sum) % MOD);
        return ret;
    }
};

int main() {
	fastio;
    int n; cin >> n;
    LazySegTree<NodeType, LazyType, F_Merge, F_Update, F_Composition> ST(n, e, id);
    for (int i = 1; i <= n; i++) {
        int t; cin >> t;
        ST.Set(i, NodeType(t, 1));
    }
    ST.Init();
    int q; cin >> q;
    while (q--) {
        int t, a, b, c; cin >> t >> a >> b;
        if (t != 4) cin >> c;
        if (t == 1) ST.Update(a, b, LazyType(1, c));
        else if (t == 2) ST.Update(a, b, LazyType(c, 0));
        else if (t == 3) ST.Update(a, b, LazyType(0, c));
        else cout << ST.Query(a, b).val << '\n';
    }
}
#endif
}
