using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 4
이름 : 배성훈
내용 : 회사 문화 3, 회사 문화 4
    문제번호 : 14287번, 14288번

    세그먼트 트리, 오일러 경로 테크닉 문제다
    회사 문화 3은 GetValUp, UpdateUp만 써서 하면 된다
    GetValDown, UpdateDown은 회사문화 2 즉 53_01의 문제에 것이다
    회사 문화 4는 둘이 섞어서 사용한다

    아래 코드는 회사 문화 4의 코드이다

    아이디어는 다음과 같다
    부하 직원쪽은 53_01에서 해결했다
    상사 직원 쪽은 해당 구간에 idx를 포함하면 칭찬 값을 더해줘야한다
    이는 세그먼트 트리에서 인덱스가 구간을 의미하고
    값으로 누적합을 보관하는 세그먼트 트리로 해결가능하다

    seg u에 해당 인덱스에 칭찬 받은 값을 저장하고, 
    해당 범위 안에 칭찬 받은 것들을 u에 누적해서 더했다
    그리고 seg에서 d 부분은 범위에 해당하는 칭찬 값을 저장시켰다(53_01)

    칭찬은 방향에 맞게 한쪽만 저장했고 받은 값 출력은 양쪽을 탐색해서 출력하게 했다
*/

namespace BaekJoon._53
{
    internal class _53_02
    {

        static void Main2(string[] args)
        {

            string ONE = "1";
            string TWO = "2";

            StreamReader sr;
            StreamWriter sw;

            int n, m;
            (int s, int e)[] nTi;
            List<int>[] line;
            int idx;

            (int d, int u)[] seg;
            bool isD;
            Solve();

            void Solve()
            {

                Init();
                isD = true;
                for (int i = 0; i < m; i++)
                {

                    string[] temp = sr.ReadLine().Split();
                    if (temp[0] == ONE)
                    {

                        int f = int.Parse(temp[1]);
                        int b = int.Parse(temp[2]);
                        Query1(f, b);
                    }
                    else if (temp[0] == TWO)
                    {

                        int f = int.Parse(temp[1]);
                        Query2(f);
                    }
                    else isD = !isD;
                }

                sr.Close();
                sw.Close();
            }

            void Query1(int _f, int _b)
            {

                if (isD) UpdateDown(1, n, nTi[_f].s, nTi[_f].e, _b);
                else UpdateUp(1, n, nTi[_f].s, _b);
            }

            void Query2(int _f)
            {

                int ret = GetValUp(1, n, nTi[_f].s, nTi[_f].e) + GetValDown(1, n, nTi[_f].s);
                sw.Write($"{ret}\n");
            }

            void UpdateUp(int _s, int _e, int _chk, int _add, int _idx = 0)
            {

                if (_s == _e) 
                {

                    seg[_idx].u += _add;
                    return;
                }

                int mid = (_s + _e) >> 1;
                if (mid < _chk) UpdateUp(mid + 1, _e, _chk, _add, _idx * 2 + 2);
                else UpdateUp(_s, mid, _chk, _add, _idx * 2 + 1);

                seg[_idx].u = seg[_idx * 2 + 1].u + seg[_idx * 2 + 2].u;
            }

            void UpdateDown(int _s, int _e, int _chkS, int _chkE, int _add, int _idx = 0)
            {

                if (_chkS <= _s && _e <= _chkE)
                {

                    seg[_idx].d += _add;
                    return;
                }

                if (_chkE < _s || _e < _chkS) return;

                int mid = (_s + _e) >> 1;
                UpdateDown(_s, mid, _chkS, _chkE, _add, _idx * 2 + 1);
                UpdateDown(mid + 1, _e, _chkS, _chkE, _add, _idx * 2 + 2);
            }

            int GetValUp(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                if (_chkS <= _s && _e <= _chkE) return seg[_idx].u;

                if (_chkE < _s || _e < _chkS) return 0;

                int mid = (_s + _e) >> 1;
                int ret = GetValUp(_s, mid, _chkS, _chkE, _idx * 2 + 1) + GetValUp(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2);

                return ret;
            }

            int GetValDown(int _s, int _e, int _chk, int _idx = 0)
            {

                if (_s == _e) return seg[_idx].d;

                int mid = (_s + _e) >> 1;
                int ret = seg[_idx].d;

                if (mid < _chk) ret += GetValDown(mid + 1, _e, _chk, _idx * 2 + 2);
                else ret += GetValDown(_s, mid, _chk, _idx * 2 + 1);

                return ret;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                string[] temp = sr.ReadLine().Split();
                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);

                temp = sr.ReadLine().Split();
                line = new List<int>[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    line[i] = new();
                    if (i == 1) continue;
                    int p = int.Parse(temp[i - 1]);
                    line[p].Add(i);
                }

                nTi = new (int s, int e)[n + 1];
                idx = 0;
                Euler(1);

                int log = (int)Math.Ceiling(Math.Log2(n)) + 1;
                seg = new (int d, int u)[1 << log];
            }

            void Euler(int _n)
            {

                nTi[_n].s = ++idx;

                for (int i = 0; i < line[_n].Count; i++)
                {

                    int next = line[_n][i];
                    Euler(next);
                }

                nTi[_n].e = idx;
            }
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

var mn = Console.ReadLine()!.Trim().Split().Select(int.Parse).ToArray();
var (n, m) = (mn[0], mn[1]);
var bosses = Console.ReadLine()!.Trim().Split().Select(int.Parse).ToArray();

// build tree
var tree = new Dictionary<int, List<int>>();
for (var i = 1; i < n; i++) {
    if (!tree.TryGetValue(bosses[i], out var children))
        tree[bosses[i]] = children = new List<int>();
    children.Add(i + 1);
}

IComplimentStrategy[] strategies = {new TopToBottomComplimentStrategy(bosses, tree), new BottomToTopComplimentStrategy(bosses, tree)};
var strategyIndex = 0;

while (m-- > 0) {
    var inputArray = Console.ReadLine()!.Trim().Split().ToArray();
    
    switch (inputArray.Length) {
        case 3: {
            var employee = int.Parse(inputArray[1]);
            strategies[strategyIndex].Compliment(employee, decimal.Parse(inputArray[2], CultureInfo.InvariantCulture));
            break;
        }
        case 2: {
            var employee = int.Parse(inputArray[1]);
            Console.WriteLine(strategies.Sum(s => s.GetSum(employee)).ToString(CultureInfo.InvariantCulture));
            break;
        }
        case 1:
            strategyIndex = strategyIndex == 0 ? 1 : 0;
            break;
        default:
            throw new ArgumentException($"inputArray.Length={inputArray.Length}");
    }
}

interface IComplimentStrategy {
    void Compliment(int employeeNumber, decimal amount);
    decimal GetSum(int employeeNumber);
}

class TopToBottomComplimentStrategy : IComplimentStrategy {
    private readonly int[] bosses;
    private readonly List<int> orderByDfs;   // 조직도를 dfs 순서로 저장
    private readonly int[] dfsIndex;
    private readonly int[] lastChildIndexes;   // 각 직원에 대해 자신의 서브트리가 마지막으로 방문하는 직원 번호 저장
    private readonly SegmentTree segmentTree;

    public TopToBottomComplimentStrategy(int[] bosses, Dictionary<int, List<int>> tree) {
        this.bosses = bosses;
        orderByDfs = new List<int>();
        dfsIndex = new int[bosses.Length + 1];
        lastChildIndexes = new int[bosses.Length + 1];

        BuildOrganizationChartRecursive(1);

        int BuildOrganizationChartRecursive(int cur) {
            orderByDfs.Add(cur);
            dfsIndex[cur] = orderByDfs.Count;

            if (!tree.TryGetValue(cur, out var children)) return lastChildIndexes[cur] = cur;
            foreach (var c in children) lastChildIndexes[cur] = BuildOrganizationChartRecursive(c); //마지막으로 방문한 직원 번호로 덮어씌운다.
            return lastChildIndexes[cur];
        }

        segmentTree = new SegmentTree(bosses.Length);
    }

    public void Compliment(int employeeNumber, decimal amount) {
        segmentTree.Update(1, bosses.Length, dfsIndex[employeeNumber], dfsIndex[lastChildIndexes[employeeNumber]], 1, amount);
    }

    public decimal GetSum(int employeeNumber) {
        return segmentTree.Sum(1, bosses.Length, dfsIndex[employeeNumber], dfsIndex[employeeNumber], 1);
    }
}

class BottomToTopComplimentStrategy : IComplimentStrategy {
    private readonly Dictionary<int, (SegmentTree, List<int> members)> segments = new();
    private readonly int[] segmentKeys;
    private readonly int[] indexInSegments;
    private readonly int[] bosses;
    
    public BottomToTopComplimentStrategy(int[] bosses, Dictionary<int, List<int>> tree) {
        this.bosses = bosses;
        segmentKeys = new int[bosses.Length + 1]; // 각 멤버의 세그먼트 키(시작 멤버 번호)
        indexInSegments = new int[bosses.Length + 1];   // 각 멤버의 세그먼트 내 순서
        var segmentHeights = new int[bosses.Length + 1];
        var bossDict = new Dictionary<int, int>();
        var queue = new Queue<(int, int)>();
        
        queue.Enqueue((1, 0));
        while (queue.Count > 0) {
            var (cur, depth) = queue.Dequeue();

            if (tree.TryGetValue(cur, out var children)) {
                foreach (var child in children) {
                    queue.Enqueue((child, depth + 1));
                    bossDict[child] = cur;
                }
            }
            else {
                //children이 없는 경우, 자신을 시작으로 한 세그먼트를 생성한다.
                CreateSegment(cur, depth);
            }

            void CreateSegment(in int startMember, in int height) {
                var boss = segmentKeys[startMember] = startMember;

                // 보스를 타고 올라가면서 현재 생성중인 세그먼트의 길이보다 낮은 길이에 속해있는 boss들을 자신의 세그먼트에 포함시킨다. (segment에 속해있지 않은 보스도 충분조건으로 포함된다)
                while (bossDict.TryGetValue(boss, out boss) && segmentHeights[boss] < height) {
                    segmentHeights[boss] = height;
                    segmentKeys[boss] = startMember;
                }
            }
        }

        for (var i = 1; i <= bosses.Length; i++) {
            var segStart = segmentKeys[i];
            if (segments.ContainsKey(segStart)) continue;
            
            var members = new List<int>();
            for (var cur = segStart; cur != -1 && segmentKeys[cur] == segStart; cur = bosses[cur - 1]) {
                members.Add(cur);
                indexInSegments[cur] = members.Count;
            }

            segments[segStart] = (new SegmentTree(members.Count), members);
        }
    }

    public void Compliment(int employee, decimal howMuch) {
        var segKey = segmentKeys[employee];
        var (seg, members) = segments[segKey];
        var idxInSeg = indexInSegments[employee];
        seg.Update(1, members.Count, idxInSeg, members.Count, 1, howMuch);

        if (members[^1] == 1) return;   // 마지막 멤버가 사장일 경우 리턴
        Compliment(bosses[members[^1] - 1], howMuch);   // 아닌 경우 보스 세그먼트 전체를 업데이트
    }

    public decimal GetSum(int employee) {
        var segKey = segmentKeys[employee];
        var (seg, members) = segments[segKey];
        var idxInSeg = indexInSegments[employee];
        return seg.Sum(1, members.Count, idxInSeg, idxInSeg, 1);
    }
}

class SegmentTree {
    private readonly decimal[] segmentTree;
    private readonly decimal[] lazy;
    
    public SegmentTree(int n) {
        segmentTree = new decimal[n * 4 + 1];
        lazy = new decimal[n * 4 + 1];
    }

    public void Update(int s, int e, int l, int r, int index, decimal diff) {
        if (lazy[index] != 0) {
            segmentTree[index] += lazy[index] * (e - s + 1);
            if (s != e) {
                lazy[index * 2] += lazy[index];
                lazy[index * 2 + 1] += lazy[index];
            }

            lazy[index] = 0;
        }

        if (s > r || e < l) return;
        if (s >= l && e <= r) {
            segmentTree[index] += (e - s + 1) * diff;
            if (s != e) {
                lazy[index * 2] += diff;
                lazy[index * 2 + 1] += diff;
            }

            return;
        }

        Update(s, (s + e) / 2, l, r, index * 2, diff);
        Update((s + e) / 2 + 1, e, l, r, index * 2 + 1, diff);
        if (s != e) segmentTree[index] = segmentTree[index * 2] + segmentTree[index * 2 + 1];
    }

    public decimal Sum(int s, int e, int l, int r, int index) {
        if (lazy[index] != 0) {
            segmentTree[index] += lazy[index] * (e - s + 1);
            if (s != e) {
                lazy[index * 2] += lazy[index];
                lazy[index * 2 + 1] += lazy[index];
            }

            lazy[index] = 0;
        }

        if (s > r || e < l) return 0;
        if (s >= l && e <= r) return segmentTree[index];
        return Sum(s, (s + e) / 2, l, r, index * 2) + Sum((s + e) / 2 + 1, e, l, r, index * 2 + 1);
    }
}
#elif other2
// # include <bits/stdc++.h>
// # include <bits/extc++.h>
// #pragma GCC optimize("O3,unroll-loops")
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
class SegTree_iter
    {
        private:
    func f;
        updating_func updating_f;
        T default_query;

        vector<T> tree, arr;
        int size, height;

        void init()
        {
            for (int i = size / 2 - 1; i > 0; i--)
                tree[i] = f(tree[i << 1], tree[i << 1 | 1]);
        }

        void _update(int i, T value)
        {
            i += size / 2;
            tree[i] = updating_f(tree[i], value);
            for (i >>= 1; i > 0; i >>= 1) tree[i] = f(tree[i << 1], tree[i << 1 | 1]);
        }

        void _update(int l, int r, T value)
        {
            for (l += size / 2, r += size / 2; l <= r; l >>= 1, r >>= 1)
            {
                if (l & 1) tree[l++] = updating_f(tree[l], value);
                if (~r & 1) tree[r--] = updating_f(tree[r], value);
            }
        }

        T _query(int i)
        {
            T res = default_query;
            for (i += size / 2; i > 0; i >>= 1) res = f(res, tree[i]);
            return res;
        }

        T _query(int l, int r)
        {
            T res1 = default_query, res2 = default_query;
            for (l += size / 2, r += size / 2; l <= r; l >>= 1, r >>= 1)
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
    SegTree_iter(const vector<T> &v, T default_query = 0) : default_query(std::move(default_query))
        {
            arr = v;
            height = (int)ceil(log2(v.size()));
            size = (1 << (height + 1));
            tree.resize(size + 1, default_query);
            for (int i = 0; i < arr.size(); i++) tree[size / 2 + i] = arr[i];
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

    template<typename T = int,
            typename Func = plus<T>,
            typename Inv_Func = minus<T>,
            T Identity = 0>
class FenWickTree
    {
        private:
    Func f;
        Inv_Func inv_f;

        vector<T> tree;
        int size;

        void _update(int x, T val)
        {
            for (int i = x; i < size; i += (i & -i))
            {
                tree[i] = f(tree[i], val);
            }
        }

        T _query(int x)
        {
            T res = Identity;
            for (int i = x; i > 0; i -= (i & -i))
            {
                res = f(res, tree[i]);
            }
            return res;
        }

        public:
    // Empty tree constructor
    FenWickTree(std::size_t size) : size(size)
        { // NOLINT
            tree.resize(size, Identity);
        }

        FenWickTree(const vector<T> &v) {
        size = (int) v.size();
        tree.resize(size);
        for (int i = 1; i<size; i++) {
            _update(i, v[i]);
    }
}

void update(int x, T val)
{
    _update(x, val);
}

T query(int i)
{
    return _query(i);
}

T query(int left, int right)
{
    if (left > right) return Identity;
    return inv_f(_query(right), _query(left - 1));
}
}; // class FenWickTree

pair<vector<int>, vector<int>> EulerTour(const graph<int>& g)
{
    int n = (int)g.size() - 1;
    vector<int> S(n +1), T(n + 1);

vector<bool> visited(n + 1, false);
function < void(int, int &) > dfs = [&](int u, int & d)-> void
{
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

int32_t main()
{
    fastIO;
    int n, m;
    cin >> n >> m;
    graph<int> g(n +1);
for (int i = 1; i <= n; i++)
{
    int u;
    cin >> u;
    if (u == -1) continue;
    g[u].emplace_back(i, 1);
}

auto[S, T] = EulerTour(g);

FenWickTree rsq1(n + 1), rsq2(n + 1);

bool flag = false;
for (int i = 0; i < m; i++)
{
    int cmd, id, w;
    cin >> cmd;
    if (cmd == 1)
    {
        cin >> id >> w;
        if (flag)
        {
            rsq1.update(S[id], w);
        }
        else
        {
            rsq2.update(S[id], w);
            rsq2.update(T[id] + 1, -w);
        }
    }
    else if (cmd == 2)
    {
        cin >> id;
        int res1 = rsq1.query(S[id], T[id]);
        int res2 = rsq2.query(S[id]);
        cout << res1 + res2 << endl;
    }
    else
    {
        flag ^= 1;
    }
}

return 0;
}
#endif
}
