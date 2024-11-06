using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 22
이름 : 배성훈
내용 : 공장
    문제번호 : 7578번

    세그먼트 트리 문제다
    세그먼트 트리로 풀었다
    아이디어는 다음과 같다
    선에 대해 위쪽에 있는 것을 u, 아래쪽에 있는 것을 d라 하자
    이제 두 선 a, b가 교차한다는 것은 
    각 선분에 대해 어느 위에 점이 같은 경우는 없고 마찬가지로 아랫점이 같은 경우도 없기에
    아래에서 비교적 오른쪽에 있는 것을 a라 다시 명명할 수 있고   
    a.u < b.u이고 b.d < a.d이 성립할 때 두 선 a, b는 교차한다고 할 수 있다
    
    그래서 위쪽은 읽으면서 몇 번째인지 인덱스를 기록한다
    그리고, 아래쪽을 순차적으로 진행하는데 두 선을 비교한다
    그러면 위쪽 값만 확인해주면 된다

    아래쪽을 진행하면서 위쪽에 사용되었다고 값을 기록해주면
    현재에 대해 기록된 값 중 오른쪽에 있는 것이 교차하는 것이 되고
    왼쪽 전체에서 왼쪽에 기록된만큼 빼면 교차하는 것이 된다

    해당 과정을 2번(정답에 해당하는 값가져오기, 기록)에 할 수 있으나, 이해를 위해 3 번(왼쪽 값 가져오기, 오른쪽 값 가져오기, 기록)에 나눠서 했다
*/

namespace BaekJoon.etc
{
    internal class etc_0428
    {

        static void Main428(string[] args)
        {

            StreamReader sr;
            int[] nTi;
            int len;

            int[] seg;

            Solve();

            void Solve()
            {

                Init();

                long ret = 0;
                for (int i = 0; i < len; i++)
                {

                    int n = ReadInt();
                    int idx = nTi[n];
                    if (idx == -1) continue;

                    ret += GetVal(0, len - 1, idx, len - 1);
                    ret += idx - GetVal(0, len - 1, 0, idx);

                    Update(0, len - 1, idx);
                }

                sr.Close();

                Console.WriteLine(ret / 2);
            }

            void Init()
            {

                nTi = new int[1_000_001];
                Array.Fill(nTi, -1);

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                len = ReadInt();

                int log = (int)Math.Ceiling(Math.Log2(len)) + 1;
                seg = new int[1 << log];

                for (int i = 0; i < len; i++)
                {

                    int n = ReadInt();
                    nTi[n] = i;
                }
            }

            int GetVal(int _s, int _e, int _fs, int _fe, int _idx = 0)
            {

                if (_fs <= _s && _e <= _fe) return seg[_idx];
                else if (_e < _fs || _fe < _s) return 0;

                int mid = (_s + _e) / 2;

                int ret = GetVal(_s, mid, _fs, _fe, _idx * 2 + 1);
                ret += GetVal(mid + 1, _e, _fs, _fe, _idx * 2 + 2);

                return ret;
            }

            void Update(int _s, int _e, int _setIdx, int _idx = 0)
            {

                if (_s == _e)
                {

                    seg[_idx]++;
                    return;
                }

                int mid = (_s + _e) / 2;
                if (_setIdx <= mid) Update(_s, mid, _setIdx, _idx * 2 + 1);
                else Update(mid + 1, _e, _setIdx, _idx * 2 + 2);

                seg[_idx]++;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr .Read()) != -1 && c != ' ' && c != '\n')
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
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var src = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var dst = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

        var reindex = new Dictionary<int, int>();
        foreach (var v in src)
            reindex[v] = reindex.Count;

        var arr = dst.Select(v => reindex[v]).ToArray();
        var (inv, _) = CountInversion(arr);

        sw.WriteLine(inv);
    }

    private static (long inversion, int[] sorted) CountInversion(int[] arr)
    {
        if (arr.Length == 1)
            return (0, arr);

        var mid = arr.Length / 2;
        var (leftinv, left) = CountInversion(arr.Take(mid).ToArray());
        var (rightinv, right) = CountInversion(arr.Skip(mid).ToArray());

        var sorted = new int[arr.Length];

        var lidx = 0;
        var lcount = mid;
        var ridx = 0;
        var rcount = arr.Length - mid;

        var inv = leftinv + rightinv;

        while (lidx < lcount && ridx < rcount)
        {
            if (left[lidx] < right[ridx])
            {
                sorted[lidx + ridx] = left[lidx];
                lidx++;
            }
            else
            {
                sorted[lidx + ridx] = right[ridx];
                ridx++;
                inv += lcount - lidx;
            }
        }

        while (lidx < lcount)
        {
            sorted[lidx + ridx] = left[lidx];
            lidx++;
        }
        while (ridx < rcount)
        {
            sorted[lidx + ridx] = right[ridx];
            ridx++;
            inv += lcount - lidx;
        }

        return (inv, sorted);
    }
}

#elif other2       
// #pragma GCC target("avx2")
// #include "bits/stdc++.h"
// #define pb push_back
// #define endl '\n'

using namespace std;
using ll = long long;
const int di[4] = {0, -1, 0, 1}, dj[4] = {-1, 0, 1, 0};

const int N = 505050;
const int b = 4, B = (1 << b); // cache line size (in integers, not bytes)

// the height of the tree over an n-element array
constexpr int height(int n) {
    return (n <= B ? 1 : height(n / B) + 1);
}

// where the h-th layer starts
constexpr int offset(int h) {
    int s = 0, n = N;
    while (h--) {
        n = (n + B - 1) / B;
        s += n * B;
    }
    return s;
}

constexpr int H = height(N);
alignas(64) int t[offset(H)]; // an array for storing nodes

struct Precalc {
    alignas(64) int mask[B][B];

    constexpr Precalc() : mask{} {
        for (int k = 0; k < B; k++)
            for (int i = 0; i < B; i++)
                mask[k][i] = (i > k ? -1 : 0);
    }
};

constexpr Precalc T;
typedef int vec __attribute__ (( vector_size(32) ));

constexpr int round(int k) {
    return k & ~(B - 1); // = k / B * B
}

void add(int k, int x) {
    vec v = x + vec{};
    for (int h = 0; h < H; h++) {
        auto a = (vec*) &t[offset(h) + round(k)];
        auto m = (vec*) T.mask[k % B];
        for (int i = 0; i < B / 8; i++)
            a[i] += v & m[i];
        k >>= b;
    }
}

int sum(int k) {
    int s = 0;
    for (int h = 0; h < H; h++)
        s += t[offset(h) + (k >> (h * b))];
    return s;
}

// #include <unistd.h>
// #include <sys/stat.h>
// #include <sys/mman.h>
template<typename T>
struct FASTIO {
    char *p,O[2000000],*d;
    void set() {
        struct stat st;fstat(0,&st);d=O;
        p=(char*)mmap(0,st.st_size,1,1,0,0);
    }
    ~FASTIO() {
        write(1,O,d-O);
    }
    inline T get() {
        T x=0;bool e;p+=e=*p=='-';
        for(char c=*p++;c&16;x=10*x+(c&15),c=*p++);
        return e?-x:x;
    }
    inline void put(T x) {
        if(x<0) *d++='-', x=-x;
        char t[16],*q=t;
        do *q++=x%10|48; while(x/=10);
        do *d++=*--q; while(q!=t);
        *d++=10;
    }
};

FASTIO<ll> IO;

int main() {
    ios::sync_with_stdio(0); cin.tie(0);
//    freopen("input.txt", "r", stdin);

    IO.set();
    int n = IO.get();
    int a[1010101], b[505050];
    for(int i = 1, x; i <= n; i++) a[IO.get()] = i;
    for(int i = 1; i <= n; i++) b[i] = a[IO.get()];
    ll ans = 0;
    for(int i = n, x; i > 0; i--) {
        ans += sum(b[i]);
        add(b[i], 1);
    }
    cout << ans << endl;
}

#elif other3
// #pragma GCC optimize("O3")
// #pragma GCC target("avx2")
// #include <bits/stdc++.h>

// #define IBUF_SIZE 1 << 18
// #define OBUF_SIZE 1 << 6

using namespace std;

struct Buffer {
  size_t size, maxSize;
  char *buf, *end, *p;

  Buffer(int sz) {
    maxSize = size = sz;
    buf = (char*)malloc(sz);
    end = buf + sz;
    memset(buf, 0x00, sz);
  }
};

struct IBuffer: Buffer {
  bool isEOF;
  IBuffer(int sz): Buffer(sz) { __load(); }
  
  bool __load() {
    size = fread(p = buf, 1, maxSize, stdin);
    end = buf + size;
    return !(isEOF = (size == 0));
  }
  
  char __read() {
    if (p == end) if (!__load()) return '\0';
    return *p++;
  }

  inline char __read_unsafe() {
    return *p++;
  }

  /* read int */
  template<class T, enable_if_t<is_integral<T>::value && !is_same<T, char>::value, T>* = nullptr>
  T read_unsafe() {
    T ret = 0;
    char ch;

    while (isspace(ch = *p++));

    bool neg = (is_signed<T>::value) && ch == '-' ? true : false;
    if (neg) ch = *p++;

    do {
      ret = ret * 10 + ch - '0';
    } while (isdigit(ch = *p++));

    return neg ? -ret : ret;
  }

  template<class T, enable_if_t<is_integral<T>::value && !is_same<T, char>::value, T>* = nullptr>
  T read() {
    if (p < end - 24) return read_unsafe<T>();
    T ret = 0;
    char ch;

    while (isspace(ch = __read()));

    bool neg = (is_signed<T>::value) && ch == '-' ? true : false;
    if (neg) ch = __read();

    do {
      ret = ret * 10 + ch - '0';
    } while (isdigit(ch = __read()));

    return neg ? -ret : ret;
  }

  /* read char */
  template<class T, enable_if_t<is_same<T, char>::value, T>* = nullptr>
  T read() {
    char ch;
    while (isspace(ch = __read()));
    return ch;
  }

  /* read string */
  template<class T, enable_if_t<is_same<T, string>::value, T>* = nullptr>
  T read() {
    T ret;
    char ch;
    
    while (isspace(ch = __read()));

    do {
      ret.push_back(ch);
    } while (isgraph(ch = __read()));
    
    return ret;
  }

  /* read floating points */
  template<class T, enable_if_t<is_floating_point<T>::value, T>* = nullptr>
  T read() {
    return (T)stold(read<string>());
  }
} I(IBUF_SIZE);

struct OBuffer: Buffer {
  OBuffer(int sz): Buffer(sz) { p = buf; }
  ~OBuffer() { __flush(); }
  
  void __flush() {
    fwrite(buf, 1, p - buf, stdout);
    p = buf;
  }
  
  void __write(char ch) {
    if (p == end) __flush();
    *p++ = ch;
  }

  inline void __write_unsafe(char ch) {
    *p++ = ch;
  }

  /* write unsigned int */
  template<class T, enable_if_t<is_integral<T>::value && is_unsigned<T>::value, T>* = nullptr>
  void write_unsafe(T src) {
    char stack[20], *ptr = stack;
    do {
      *ptr++ = (src % 10) + '0';
      src /= 10;
    } while (src > 0);
    while (ptr-- != stack) *p++ = *ptr;
  }

  template<class T, enable_if_t<is_integral<T>::value && is_unsigned<T>::value, T>* = nullptr>
  void write(T src) {
    if (p < end - 24) { write_unsafe(src); return; }
    char stack[20], *p = stack;
    do {
      *p++ = (src % 10) + '0';
      src /= 10;
    } while (src > 0);
    while (p-- != stack) __write(*p);
  }

  /* write signed int */
  template<class T, enable_if_t<is_integral<T>::value && is_signed<T>::value && !is_same<T, char>::value, T>* = nullptr>
  void write(T src) {
    if (src < 0) __write('-'), src = -src;
    write<make_unsigned_t<T>>(src);
  }

  /* write char */
  template<class T, enable_if_t<is_same<T, char>::value, T>* = nullptr>
  void write(T src) {
    __write(src);
  }

  /* write string */
  template<class T, enable_if_t<is_same<T, string>::value, T>* = nullptr>
  void write(T src) {
    for (char ch: src) __write(ch);
  }

  /* write floating points */
  template<class T, enable_if_t<is_floating_point<T>::value, T>* = nullptr>
  void write(T src) {
    write<string>(to_string(src));
  }
} O(OBUF_SIZE);

struct IVector { template <typename T> inline operator T() { return I.read<T>(); } } iv;
inline IVector& readAny() { return iv; }

template <class T> IBuffer& operator>>(IBuffer& ib, T& x) { x = ib.read<T>(); return ib; }
template <class T> OBuffer& operator<<(OBuffer& ob, T x) { ob.write<T>(x); return ob; }

/* section .text */
int N, idx[1000001], src[500000], tree[524289];

void doInput() {
  N = readAny();
  for (int i=0; i<N; i++) {
    int t = readAny();
    idx[t] = i+1;
  }
  for (int i=0; i<N; i++) {
    int t = readAny();
    src[i] = idx[t];
  }
}

void update(int idx, int delta) {
  while (idx <= 524288) {
    tree[idx] += delta;
    idx += (idx & -idx);
  }
}

int query(int x) {
  int R = 0;
  while (x > 0) {
    R += tree[x];
    x ^= (x & -x);
  }
  return R;
}

int64_t doSolve() {
  int64_t R = 0;
  for (int i=0; i<N; i++) {
    int p = src[i];
    R += (i - query(p));
    update(p, 1);
  }
  return R;
}

int main() {
  doInput();
  O << doSolve() << '\n';
  return 0;
}
#endif
}
