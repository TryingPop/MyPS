using BaekJoon._53;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 8
이름 : 배성훈
내용 : 회사 문화 5
    문제번호 : 18437번

    느리게 갱신되는 세그먼트 트리, 오일러 경로 테크닉 문제다
    a를 상사로 두는 사람을 찾아야하므로 해당 상사는 자신의 idx를 제외하고 
    누적합을 찾아야한다
*/

namespace BaekJoon._53
{
    internal class _53_07
    {

        static void Main7(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            (int s, int e)[] nTi;
            (int n, int lazy)[] seg;
            int n;
            int idx;
            List<int>[] line;

            Solve();

            void Solve()
            {

                Input();

                int len = ReadInt();
                Update(1, n, 1, n, 1);

                for (int i = 0; i < len; i++)
                {

                    int op = ReadInt();
                    int f = ReadInt();

                    if (op == 1) Update(1, n, nTi[f].s + 1, nTi[f].e, 1);
                    else if (op == 2) Update(1, n, nTi[f].s + 1, nTi[f].e, -1);
                    else
                    {

                        int ret = GetVal(1, n, nTi[f].s + 1, nTi[f].e);
                        sw.Write($"{ret}\n");
                    }
                }

                sr.Close();
                sw.Close();
            }

            void LazyUpdate(int _s, int _e, int _idx)
            {

                int lazy = seg[_idx].lazy;
                if (lazy == 0) return;
                seg[_idx].lazy = 0;

                seg[_idx].n = lazy == 1 ? (_e - _s + 1) : 0;
                if (_s == _e) return;

                seg[_idx * 2 + 1].lazy = lazy;
                seg[_idx * 2 + 2].lazy = lazy;
            }

            void Update(int _s, int _e, int _chkS, int _chkE, int _val, int _idx = 0)
            {

                LazyUpdate(_s, _e, _idx);

                if (_chkS <= _s && _e <= _chkE)
                {

                    seg[_idx].n = _val == 1 ? (_e - _s + 1) : 0;
                    if (_s != _e)
                    {

                        seg[_idx * 2 + 1].lazy = _val;
                        seg[_idx * 2 + 2].lazy = _val;
                    }

                    return;
                }

                if (_e < _chkS || _chkE < _s) return;

                int mid = (_s + _e) >> 1;
                Update(_s, mid, _chkS, _chkE, _val, _idx * 2 + 1);
                Update(mid + 1, _e, _chkS, _chkE, _val, _idx * 2 + 2);

                seg[_idx].n = seg[_idx * 2 + 1].n + seg[_idx * 2 + 2].n;
            }

            int GetVal(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                LazyUpdate(_s, _e, _idx);

                if (_chkS <= _s && _e <= _chkE) return seg[_idx].n;

                if (_e < _chkS || _chkE < _s) return 0;

                int mid = (_s + _e) >> 1;
                return GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1)
                    + GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = ReadInt();
                line = new List<int>[n + 1];

                for (int i = 0; i <= n; i++)
                {

                    line[i] = new();
                }

                for(int i = 1; i <= n; i++)
                {

                    int p = ReadInt();
                    line[p].Add(i);
                }

                nTi = new (int s, int e)[n + 1];

                idx = -1;
                DFS(0);

                int log = (int)Math.Ceiling(Math.Log2(n)) + 1;
                seg = new (int n, int lazy)[1 << log];
            }

            void DFS(int _n)
            {

                nTi[_n].s = ++idx;

                for (int i = 0; i < line[_n].Count; i++)
                {

                    DFS(line[_n][i]);
                }

                nTi[_n].e = idx;
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
// # include <bits/stdc++.h>
// # include <bits/extc++.h>
//#pragma GCC optimize("O3,unroll-loops")
// #define fastIO ios_base::sync_with_stdio(false); cin.tie(0); cout.tie(0)
// #define endl '\n'
// #define all(v) (v).begin(), (v).end()
using namespace std;
using namespace __gnu_cxx;
using ll [[maybe_unused]] = long long;
using ull [[maybe_unused]] = unsigned long long;
using lll [[maybe_unused]] = __int128;
using lld [[maybe_unused]] = long double;
using llld [[maybe_unused]] = __float128;
template<typename T> using graph [[maybe_unused]] = vector<vector<pair<int,T>>>;
template<typename T> using matrix [[maybe_unused]] = vector<vector<T>>;

/**
 *  "Premature optimization is the root of all evil."
 *  <sub> Donald Knuth </sub>
 */

// # ifdef ONLINE_JUDGE // Be careful if problem is about strings.
/**
 * Namespace for Fast I/O
 *
 * @class@b INPUT
 * class which can replace the cin
 *
 * @class@b OUTPUT
 * class which can replace the cout
 *
 * @Description
 * These classes use low-level i/o functions (@c fread() for input, @c fwrite() for output)
 * so that they can read or write much faster than cin and cout. <br>
 * Several macros are defined for convenience of using them.
 *
 * @Author  <a href="https://blog.naver.com/jinhan814/222266396476">jinhan814</a>
 * @Date    2021-05-06
 */
namespace FastIO {
    constexpr int SZ = 1 << 20;

    /* Class INPUT */
    class INPUT {
    private:
        char readBuffer[SZ];
        int read_idx, next_idx;
        bool __END_FLAG__, __GET_LINE_FLAG__; // NOLINT
    public:
        explicit operator bool() const { return !__END_FLAG__; }

        static bool IsBlank(char c) { return c == ' ' || c == '\n'; }

        static bool IsEnd(char c) { return c == '\0'; }

        char _ReadChar() { // NOLINT
            if (read_idx == next_idx) {
                next_idx = (int)fread(readBuffer, sizeof(char), SZ, stdin);
                if (next_idx == 0) return 0;
                read_idx = 0;
            }
            return readBuffer[read_idx++];
        }

        char ReadChar() {
            char ret = _ReadChar();
            for (; IsBlank(ret); ret = _ReadChar());

            return ret;
        }

        template<class T>
        T ReadInt() {
            T ret = 0;
            char curr = _ReadChar();
            bool minus_flag = false;

            for (; IsBlank(curr); curr = _ReadChar());
            if (curr == '-') minus_flag = true, curr = _ReadChar();
            for (; !IsBlank(curr) && !IsEnd(curr); curr = _ReadChar())
                ret = 10 * ret + (curr & 15);
            if (IsEnd(curr)) __END_FLAG__ = true;

            return minus_flag ? -ret : ret;
        }

        std::string ReadString() {
            std::string ret;
            char curr = _ReadChar();
            for (; IsBlank(curr); curr = _ReadChar());
            for (; !IsBlank(curr) && !IsEnd(curr); curr = _ReadChar())
                ret += curr;
            if (IsEnd(curr)) __END_FLAG__ = true;

            return ret;
        }

        double ReadDouble() {
            std::string ret = ReadString();
            return std::stod(ret);
        }

        std::string getline() {
            std::string ret;
            char curr = _ReadChar();
            for (; curr != '\n' && !IsEnd(curr); curr = _ReadChar())
                ret += curr;
            if (__GET_LINE_FLAG__) __END_FLAG__ = true;
            if (IsEnd(curr)) __GET_LINE_FLAG__ = true;

            return ret;
        }

        friend INPUT &getline(INPUT &in, std::string &s) {
            s = in.getline();
            return in;
        }
    } _in;
    /* End of Class INPUT */

    /* Class OUTPUT */
    class OUTPUT {
    private:
        char writeBuffer[SZ];
        int write_idx;
    public:
        ~OUTPUT() { Flush(); }

        explicit operator bool() const { return true; }

        void Flush() {
            fwrite(writeBuffer, sizeof(char), write_idx, stdout);
            write_idx = 0;
        }

        void WriteChar(char c) {
            if (write_idx == SZ) Flush();
            writeBuffer[write_idx++] = c;
        }

        template<class T>
        void WriteInt(T total) {
            int sz = GetSize(total);
            if (write_idx + sz + 1 >= SZ) Flush();
            if (total < 0) writeBuffer[write_idx++] = '-', total = -total;
            for (int i = sz; i-- > 0; total /= 10)
                writeBuffer[write_idx + i] = total % 10 | 48;
            write_idx += sz;
        }

        void WriteString(const std::string& s) {
            for (auto &c: s) WriteChar(c);
        }

        void WriteDouble(double d) {
            WriteString(std::to_string(d));
        }

        template<class T>
        int GetSize(T total) {
            int ret = 1;
            for (total = total >= 0 ? total : -total; total >= 10; total /= 10) ret++;

            return ret;
        }
    } _out;
    /* End of Class OUTPUT */

    /* Operators */
    INPUT &operator>>(INPUT &in, char &i) {
        i = in.ReadChar();
        return in;
    }

    INPUT &operator>>(INPUT &in, std::string &i) {
        i = in.ReadString();
        return in;
    }

    template<class T, typename std::enable_if_t<std::is_arithmetic_v<T>> * = nullptr>
    INPUT &operator>>(INPUT &in, T &i) {
        if constexpr (std::is_floating_point_v<T>) i = in.ReadDouble();
        else if constexpr (std::is_integral_v<T>) i = in.ReadInt<T>();
        return in;
    }

    OUTPUT &operator<<(OUTPUT &out, char i) {
        out.WriteChar(i);
        return out;
    }

    OUTPUT &operator<<(OUTPUT &out, const std::string &i) {
        out.WriteString(i);
        return out;
    }

    template<class T, typename std::enable_if_t<std::is_arithmetic_v<T>> * = nullptr>
    OUTPUT &operator<<(OUTPUT &out, T i) {
        if constexpr (std::is_floating_point_v<T>) out.WriteDouble(i);
        else if constexpr (std::is_integral_v<T>) out.WriteInt(i);
        return out;
    }

    /* Macros for convenience */
// #undef fastIO
// #define fastIO 1
// #define cin _in
// #define cout _out
// #define istream INPUT
// #define ostream OUTPUT
};
using namespace FastIO;
// #endif

    /**
     * Segment Tree using iterative method
     * @tparam T type of elements
     * @tparam func function object to be called to perform the query
     * @tparam updating_func function object to be called to update the containing element
     */
    template<typename T,
            typename func = plus<T>,
            typename updating_func = plus<T>>
class LazySegTree_iter
    {
        private:
    func f;
        updating_func updating_f;
        T default_query;
        T default_lazy;

        vector<T> tree, lazy, arr;
        int size, height, n;

        void init()
        {
            for (int i = n - 1; i >= 1; i--) pull(i);
        }

        void apply(int node, T value)
        {
            tree[node] = updating_f(tree[node], value);
            if (node < n) lazy[node] = updating_f(lazy[node], value);
        }

        void pull(int node)
        {
            tree[node] = f(tree[node << 1], tree[node << 1 | 1]);
        }

        void pull_all(int l, int r)
        {
            for (int i = 1; i <= height; i++)
            {
                if ((l >> i << i) != l) pull(l >> i);
                if ((r >> i << i) != r) pull((r - 1) >> i);
            }
        }

        void push(int node)
        {
            if (lazy[node] == default_lazy) return;
            apply(node << 1, lazy[node] >> 1);
            apply(node << 1 | 1, lazy[node] >> 1);
            lazy[node] = default_lazy;
        }

        void push_all(int l, int r)
        {
            for (int i = height; i >= 1; i--)
            {
                if ((l >> i << i) != l) push(l >> i);
                if ((r >> i << i) != r) push((r - 1) >> i);
            }
        }

        void _update(int i, T value)
        {
            i += n;
            for (int j = height; j >= 1; j--) push(i >> j);
            tree[i] = updating_f(tree[i], value);
            for (int j = 1; j <= height; j++) pull(i >> j);
        }

        void _update(int l, int r, T value)
        {
            l += n, r += n;
            push_all(l, r + 1);

            int l0 = l, r0 = r;
            for (int k = 1; l <= r; l >>= 1, r >>= 1, k <<= 1)
            {
                if (l & 1) apply(l++, value * k);
                if (~r & 1) apply(r--, value * k);
            }

            l = l0, r = r0;
            pull_all(l, r + 1);
        }

        T _query(int i)
        {
            i += n;
            for (int j = height; j >= 1; j--) push(i >> j);
            return tree[i];
        }

        T _query(int l, int r)
        {
            l += n, r += n;
            push_all(l, r + 1);

            T res1 = default_query, res2 = default_query;
            for (; l <= r; l >>= 1, r >>= 1)
            {
                if (l & 1) res1 = f(res1, tree[l++]);
                if (~r & 1) res2 = f(tree[r--], res2);
                // NOTE: There exists cases that the operation's order must be considered
            }
            return f(res1, res2);
        }

        public:
    /**
     * Constructor for a segment tree
     * @param v Array that the segment tree will be constructed from
     * @param default_query The result of query that doesn't affect the other query result when performed <i>func</i> with
     */
    LazySegTree_iter(const vector<T> &v, T default_query = 0, T default_lazy = 0)
    : default_query(std::move(default_query)), default_lazy(std::move(default_lazy))
        {
            arr = v;
            height = (int)ceil(log2(v.size()));
            size = (1 << (height + 1));
            n = size >> 1;
            tree.resize(size + 1, default_query);
            lazy.resize(size + 1, default_lazy);
            for (int i = 0; i < arr.size(); i++) tree[n + i] = arr[i];
            init();
        }

        void update(int idx, T value)
        {
            _update(idx, value);
        }

        void update(int l, int r, T value)
        {
            _update(l, r, value);
        }

        T query(int idx)
        {
            return _query(idx);
        }

        T query(int left, int right)
        {
            return _query(left, right);
        }
    }; // class SegTree_iter

    template<typename T,
            typename func = plus<T>,
            typename updating_func = plus<T>,
            typename updating_func2 = updating_func>
class LazySegTree
    {
        private:
    func f;
        updating_func updating_f;
        updating_func2 lazy_to_tree; // may be needed or not, IDK

        vector<T> tree, lazy;
        vector<T> v;
        int size, height;
        T default_lazy;
        T default_query;

        T init(int node, int left, int right)
        {
            if (left == right) return tree[node] = v[left];

            int mid = (left + right) >> 1;
            auto left_result = init(node << 1, left, mid);
            auto right_result = init(node << 1 | 1, mid + 1, right);
            tree[node] = f(left_result, right_result);

            return tree[node];
        }

        void update_lazy(int node, int start, int end)
        {
            if (lazy[node] != default_lazy)
            {
                tree[node] = lazy_to_tree(tree[node], lazy[node] * (end - start + 1));
                if (start != end)
                {
                    lazy[node << 1] = updating_f(lazy[node << 1], lazy[node]);
                    lazy[node << 1 | 1] = updating_f(lazy[node << 1 | 1], lazy[node]);
                }
                lazy[node] = default_lazy;
            }
        }

        /**
         * Update the tree as a result of adding values to the array from s_idx to e_idx
         * @param node current node index in the tree
         * @param start starting index that the current node is covering
         * @param end ending index that the current node is covering
         * @param s_idx starting index that updating is required
         * @param e_idx ending index that updating is required
         * @param value the value to be added to the array
         */
        void _update(int node, int start, int end, int s_idx, int e_idx, int value)
        {
            update_lazy(node, start, end);

            if (e_idx < start || end < s_idx) return;

            if (s_idx <= start && end <= e_idx)
            {
                lazy[node] = updating_f(lazy[node], value);
                update_lazy(node, start, end);
                return;
            }

            int mid = (start + end) >> 1;
            _update(node << 1, start, mid, s_idx, e_idx, value);
            _update(node << 1 | 1, mid + 1, end, s_idx, e_idx, value);
            tree[node] = f(tree[node << 1], tree[node << 1 | 1]);
        }

        /**
         * Find the sum of the array elements in range [left, right]
         * @param node current node index in the tree
         * @param start starting index that the current node is covering
         * @param end ending index that the current node is covering
         * @param left starting index of summation
         * @param right ending index of summation
         * @return the sum of the array elements in range [left, right]
         */
        T _query(int node, int start, int end, int left, int right)
        {
            update_lazy(node, start, end);

            if (end < left || right < start) return default_query;
            if (left <= start && end <= right) return tree[node];

            int mid = (start + end) >> 1;
            auto left_result = _query(node << 1, start, mid, left, right);
            auto right_result = _query(node << 1 | 1, mid + 1, end, left, right);
            return f(left_result, right_result);
        }

        public:

    /**
     * Constructor for a lazy segment tree
     * @param arr  Build a segment tree from the given array
     */
    LazySegTree(const vector<int> &arr, T default_query, T default_lazy)
    : default_query(std::move(default_query)),
      default_lazy(std::move(default_lazy))
        {
            v = arr;
            height = (int)ceil(log2(v.size()));
            size = (1 << (height + 1));
            tree.resize(size + 1);
            lazy.resize(size + 1, default_lazy);
            init(1, 0, v.size() - 1);
        }

        void update(int s_idx, int e_idx, T value)
        {
            if (s_idx > e_idx) return;
            _update(1, 0, v.size() - 1, s_idx, e_idx, value);
        }

        T query(int left, int right)
        {
            if (left > right) return 0;
            return _query(1, 0, v.size() - 1, left, right);
        }
    }; // class LazySegTree

    pair<vector<int>, vector<int>> EulerTour(const graph<int>& g)
    {
        int n = (int)g.size() - 1;
        vector<int> S(n +1), T(n + 1);

        vector<bool> visited(n +1, false);
        function < void(int, int &) > dfs = [&](int u, int & d)-> void {
            S[u] = d;
            for (const auto& [v, _]: g[u]) {
                if (visited[v]) continue;
                visited[v] = true;
                dfs(v, ++d);
            }
            T[u] = d;
        };

        int d = 1;
        dfs(1, d);

        return { S, T};
    }

    struct replacement
    {
        int operator()(int l, int r) const {
        return r;
    }
};

int32_t main()
{
    fastIO;
    int n, m;
    cin >> n;
    graph<int> g(n +1);
for (int i = 1; i <= n; i++)
{
    int u;
    cin >> u;
    if (u == 0) continue;
    g[u].emplace_back(i, 1);
}

auto[S, T] = EulerTour(g);

vector<int> tree(n + 1, 1);
//LazySegTree<int,plus<>,replacement> rsq(tree, 0, -1);
LazySegTree_iter<int, plus<>, replacement> rsq2(tree, 0, -1);

cin >> m;
for (int i = 0; i < m; i++)
{
    int cmd, id;
    cin >> cmd >> id;
    if (cmd == 1)
    {
        //rsq.update(S[id] + 1, T[id], 1);
        rsq2.update(S[id] + 1, T[id], 1);
    }
    else if (cmd == 2)
    {
        //rsq.update(S[id] + 1, T[id], 0);
        rsq2.update(S[id] + 1, T[id], 0);
    }
    else
    {
        //cout << rsq.query(S[id] + 1, T[id]) << endl;
        cout << rsq2.query(S[id] + 1, T[id]) << endl;
    }
}

return 0;
}
#elif other2
// #include <iostream>
// #include <algorithm>
// #include <vector>
// #include <math.h>
using namespace std;
typedef long long ll;

vector<int> g[100001];
int s[100001], e[100001], k = 0;
void dfs(int i) {
    s[i] = k;
    for (int j : g[i]) {
        k++;
        dfs(j);
    }
    e[i] = k;
}

struct segtree_lazy {
    int n, h = 0;
    vector<int> a, node;
    vector<int> lazy;
    segtree_lazy() {}
    segtree_lazy(int _n) : n(_n) {
        for (int i = 1; i < n; i *= 2) h++;
        node.resize(2 * n);
        a.resize(n);
        lazy.resize(n);
    }
    void build() {
        for (int i = 0; i < n; i++) node[n + i] = a[i];
        for (int i = n - 1; i; i--) node[i] = node[i << 1] + node[i << 1 | 1];
    }
    void apply_node(int i, int len, int val) {
        node[i] = val == 1 ? len : 0;
        if (i < n) lazy[i] = val;
    }
    void merge_node(int i, int len) {
        if (i >= n) return;
        node[i] = node[i << 1] + node[i << 1 | 1];
        if (lazy[i]) node[i] = lazy[i] == 1 ? len : 0;
    }
    void prop(int i) {
        for (int hh = h, len = 1 << h; hh > 0; --hh, len >>= 1) {
            int j = i >> hh;
            if (lazy[j]) {
                apply_node(j << 1, len >> 1, lazy[j]);
                apply_node(j << 1 | 1, len >> 1, lazy[j]);
                lazy[j] = 0;
            }
        }
    }
    void upd(int l, int r, int val) {
        l += n; r += n;
        prop(l); prop(r);  
        for (int i = l, j = r, len = 1; i <= j; i >>= 1, j >>= 1, len <<= 1) {
            if (i & 1) apply_node(i++, len, val);
            if (!(j & 1)) apply_node(j--, len, val);
        }
        for (int i = l >> 1, len = 2; i; i >>= 1, len <<= 1) merge_node(i, len);
        for (int i = r >> 1, len = 2; i; i >>= 1, len <<= 1) merge_node(i, len);
    }
    ll query(int l, int r) {
        l += n; r += n;
        prop(l); prop(r);
        ll res = 0;
        while (l <= r) {
            if (l & 1) res += node[l++];
            if (!(r & 1)) res += node[r--];
            l >>= 1; r >>= 1;
        }
        return res;
    }
};

int main() {
    ios_base::sync_with_stdio(false);
    cin.tie(NULL); cout.tie(NULL);
    
    int n, p; cin >> n >> p;
    for (int i = 2; i <= n; i++) {
        cin >> p;
        g[p].push_back(i);
    }
    dfs(1);
    
    segtree_lazy tree(n);
    for (int i = 0; i < n; i++) tree.a[i] = 1;
    tree.build();
    
    int m; cin >> m;
    for (int j = 1; j <= m; j++) {
        int t, i; cin >> t >> i;
        if (s[i] == e[i]) {
            if (t == 3) cout << "0\n";
            continue;
        }
        if (t == 3) cout << tree.query(s[i] + 1, e[i]) << "\n";
        else tree.upd(s[i] + 1, e[i], t);
    }
}
#endif
}
