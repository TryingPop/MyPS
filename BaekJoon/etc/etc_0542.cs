using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 16
이름 : 배성훈
내용 : 최소값 찾기
    문제번호 : 11003번

    우선순위 큐 문제다
    dp단원에 왜 있는지 모르겠다
    힌트도 보면 우선순위 큐만 있고 dp는 없다
*/

namespace BaekJoon.etc
{
    internal class etc_0542
    {

        static void Main542(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 32);
            StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 65536 * 16);
            PriorityQueue<(int n, int i), int> q = new(1_250_000);

            Solve();

            sr.Close();
            sw.Close();

            void Solve()
            {

                int n = ReadInt();
                int r = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    q.Enqueue((cur, i), cur);

                    while (q.Count > 0)
                    {

                        bool chk = (i - q.Peek().i) < r;
                        if (chk) break;
                        q.Dequeue();
                    }

                    sw.Write($"{q.Peek().n} ");
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;

                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    if (c == '-')
                    {

                        plus = false;
                        continue;
                    }

                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
var reader = new Reader();
var (n, l) = (reader.NextInt(), reader.NextInt());

var dq = new PhonyDeque<(int n, int i)>(n);
using (var w = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
for (int i = 1; i <= n; i++)
{
    var num = reader.NextInt();
    
    while (dq.Count > 0 && dq.PeekBack().n > num)
        dq.PopBack();

    dq.PushBack((num, i));

    while (dq.PeekFront().i <= i - l)
        dq.PopFront();
    
    w.Write(dq.PeekFront().n + " ");
}

class PhonyDeque<T>
{
    private T[] _buffer;
    private int _front;
    private int _back;

    public int Capacity;

    public int Count => _back - _front + 1;

    public PhonyDeque(int capacity)
    {
        Capacity = capacity;
        _buffer = new T[capacity * 2 + 1];
        _front = capacity;
        _back = capacity - 1;
    }

    public T PeekBack() => _buffer[_back];
    public T PeekFront() => _buffer[_front];

    public void PushBack(T value) => _buffer[++_back] = value;
    public void PushFront(T value) => _buffer[_front--] = value;

    public T PopBack() => _buffer[_back--];
    public T PopFront() => _buffer[++_front];
}

class Reader{StreamReader R;public Reader()=>R=new(new BufferedStream(Console.OpenStandardInput()));
public int NextInt(){var(v,n,r)=(0,false,false);while(true){int c=R.Read();if((r,c)==(false,'-')){(n,r)=(true,true);continue;}if('0'<=c&&c<='9'){(v,r)=(v*10+(c-'0'),true);continue;}if(r==true)break;}return n?-v:v;}
}
#elif other2
using System;
using System.IO;
using System.Runtime.CompilerServices;

#nullable disable

public class Deque<T>
{
    public bool IsEmpty => Count == 0;
    public int Count => _count;

    public T Front => _arr[_stIncl];
    public T Back => _edExcl == 0 ? _arr[_arr.Length - 1] : _arr[_edExcl - 1];

    private int _stIncl;
    private int _edExcl;
    private int _count;

    private T[] _arr;

    public Deque()
    {
        _arr = new T[1024];
    }

    public void PushFront(T v)
    {
        EnsureSize(1 + _count);
        _count++;

        _stIncl--;
        if (_stIncl == -1)
            _stIncl = _arr.Length - 1;

        _arr[_stIncl] = v;
    }
    public T PopFront()
    {
        if (_count == 0)
            throw new InvalidOperationException();

        _count--;

        var val = _arr[_stIncl];

        _stIncl++;
        if (_stIncl == _arr.Length)
            _stIncl = 0;

        return val;
    }
    public void PushBack(T v)
    {
        EnsureSize(1 + _count);
        _count++;

        _arr[_edExcl] = v;

        _edExcl++;
        if (_edExcl == _arr.Length)
            _edExcl = 0;
    }
    public T PopBack()
    {
        if (_count == 0)
            throw new InvalidOperationException();

        _count--;

        _edExcl--;
        if (_edExcl == -1)
            _edExcl += _arr.Length;

        return _arr[_edExcl];
    }

    private void EnsureSize(int size)
    {
        if (1 + _arr.Length == size)
        {
            var newarr = new T[2 * _arr.Length];

            Array.Copy(_arr, _stIncl, newarr, 0, _arr.Length - _stIncl);
            Array.Copy(_arr, 0, newarr, _arr.Length - _stIncl, _stIncl);

            _stIncl = 0;
            _edExcl = _arr.Length;

            _arr = newarr;
        }
    }
}

public class Program
{
    public static void Main()
    {
        //using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        //using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        using var io = new FastIO();

        var n = io.ReadInt();
        var l = io.ReadInt();

        var deq = new Deque<(int val, int idx)>();
        for (var idx = 0; idx < n; idx++)
        {
            var val = io.ReadInt();

            while (deq.Count != 0 && deq.Back.val > val)
                deq.PopBack();

            while (deq.Count != 0 && deq.Front.idx <= idx - l)
                deq.PopFront();

            deq.PushBack((val, idx));

            io.WriteInt(deq.Front.val);
            io.WriteByte((Byte)' ');
        }
    }
}

public sealed class FastIO : IDisposable
{
    private Byte[] _readBuffer;
    private int _readIndex;
    private int _readLength;
    private Stream _readStream;

    private Byte[] _writeBuffer;
    private int _writeIndex;
    private Stream _writeStream;

    public FastIO()
    {
        _readBuffer = new Byte[65536];
        _readStream = Console.OpenStandardInput(_readBuffer.Length);
        _readLength = _readStream.Read(_readBuffer, 0, _readBuffer.Length);

        _writeBuffer = new Byte[65536];
        _writeStream = Console.OpenStandardOutput(_writeBuffer.Length);
    }

    private static bool IsWhitespace(int ch)
    {
        return ch == '\n' || ch == ' ' || ch == '\r';
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private byte ReadByte()
    {
        // End of stream
        if (_readLength == 0)
            return 0;

        var val = _readBuffer[_readIndex++];
        if (_readIndex == _readLength)
        {
            _readIndex = 0;
            _readLength = _readStream.Read(_readBuffer, 0, _readBuffer.Length);
        }

        if (_readLength == 0)
            return 0;

        return val;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteByte(byte b)
    {
        _writeBuffer[_writeIndex++] = b;

        if (_writeIndex == _writeBuffer.Length)
        {
            _writeStream.Write(_writeBuffer, 0, _writeIndex);
            _writeIndex = 0;
        }
    }

    public int ReadInt()
    {
        var v = ReadByte();
        while (IsWhitespace(v))
            v = ReadByte();

        if (v == '-')
            return -ReadInt();

        var abs = v - '0';

        while (true)
        {
            v = ReadByte();

            if (v == 0 || IsWhitespace(v))
                break;
            else
                abs = abs * 10 + (v - '0');
        }

        return abs;
    }

    public void WriteInt(int value)
    {
        if (value < 0)
        {
            WriteByte((byte)'-');
            WriteInt(-value);
            return;
        }
        if (value == 0)
        {
            WriteByte((byte)'0');
            return;
        }

        var divisor = 1L;
        var valcopy = value;

        while (valcopy > 0)
        {
            valcopy /= 10;
            divisor *= 10;
        }

        divisor /= 10;

        while (divisor != 0)
        {
            var digit = (value / divisor) % 10;
            WriteByte((byte)('0' + digit));
            divisor /= 10;
        }
    }

    public void Dispose()
    {
        _writeStream.Write(_writeBuffer, 0, _writeIndex);
    }
}

#elif other3
using System.Text;
using Item = System.ValueTuple<int, int>;
class Program
{
    static void Main(string[] args)
    {
        using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        using var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        var chunk = new StringBuilder(12 * 101);

        int n = ScanInt(sr), k = ScanInt(sr);
        var deque = new Deque(k);
        Span<char> outBuffer = stackalloc char[11];
        for (int i = 0; i < n; i++)
        {
            var num = ScanInt(sr);
            (var value, var index) = deque.Peek();
            if (i - k == index)
                deque.Dequeue();
            deque.Push((num, i));
            value = deque.Peek().Item1;
            value.TryFormat(outBuffer, out int charsWritten);
            chunk.Append(outBuffer[..charsWritten]);
            chunk.Append(' ');
            if (chunk.Length >= 12 * 100)
            {
                sw.Write(chunk);
                chunk.Clear();
            }
        }
        sw.Write(chunk);
    }

    static int ScanInt(StreamReader sr)
    {
        int c = sr.Read(), n = 0;
        if (c == '-')
            while (!((c = sr.Read()) is ' ' or '\n' or -1))
            {
                //if (c == '\r')
                //{
                //    sr.Read();
                //    break;
                //}
                n = 10 * n - c + '0';
            }
        else
        {
            n = c - '0';
            while (!((c = sr.Read()) is ' ' or '\n' or -1))
            {
                //if (c == '\r')
                //{
                //    sr.Read();
                //    break;
                //}
                n = 10 * n + c - '0';
            }
        }
        return n;
    }

    struct Deque
    {
        public Item[] _array;
        int _start = 0;
        int _count = 0;

        public Deque(int capacity)
        {
            _array = new Item[capacity];
        }

        public void Push(Item item)
        {
            int l = 0, r = _count;
            while (l < r)
            {
                int mid = (l + r) / 2;
                if (FrontIsBigger(item, GetByIndex(mid)))
                    l = mid + 1;
                else
                    r = mid;
            }

            _count = l + 1;
            GetByIndex(l) = item;
        }

        public Item Peek()
        {
            return GetByIndex(0);
        }

        public void Dequeue()
        {
            _count--;
            _start = (_start + 1) % _array.Length;
        }

        static bool FrontIsBigger(Item a, Item b)
        {
            (int aValue, int aIndex) = a;
            (int bValue, int bIndex) = b;
            if (aValue != bValue)
                return aValue > bValue;
            return aIndex < bIndex;
        }

        ref Item GetByIndex(int index)
        {
            return ref _array[(_start + index) % _array.Length];
        }
    }
}
#elif other4
// #![no_main]

fn solve() {
    let mut reader = Reader::new(1 << 24);
    let mut writer = Writer::new(1 << 20);
    let n = reader.u32() as usize;
    let l = reader.u32() as usize;
    let mut a = vec![(0i32, 0i32); n];
    let a = &mut a[..n];
    let mut front = 0;
    let mut back = 0;
    for i in 0..n {
        reader.try_refill(16);
        let x = reader.i32();
        if front < back && unsafe { a.get_unchecked(front).1 } as usize + l <= i {
            front += 1;
        }
        while front < back && unsafe { a.get_unchecked(back - 1).0 } >= x {
            back -= 1;
        }
        unsafe { *a.get_unchecked_mut(back) = (x, i as i32) };
        back += 1;
        writer.i32(unsafe { a.get_unchecked(front).0 });
        writer.byte(b' ');
    }
}

// #[no_mangle]
unsafe fn main() -> i32 {
    solve();
    0
}

struct Reader {
    begin: *const u8,
    cur: *const u8,
    end: *const u8,
    goff: usize,
    cap: usize,
}
impl Reader {
    fn new(capacity: usize) -> Self {
        let ptr;
        unsafe {
            std::arch::asm!(
                "syscall",
                inout("rax") 9usize => ptr,
                in("rdi") 0,
                in("rsi") capacity,
                in("rdx") 3,
                in("r10") 2,
                in("r8") 0,
                in("r9") 0,
                out("rcx") _,
                out("r11") _,
                options(nomem,preserves_flags)
            );
        }
        Self {
            begin: ptr,
            end: unsafe { ptr.add(capacity) },
            cur: ptr,
            goff: 0,
            cap: capacity,
        }
    }
    fn try_refill(&mut self, readahead: usize) {
        if unsafe { self.cur.add(readahead) } <= self.end {
            return;
        }
        self.goff += unsafe { self.cur.offset_from(self.begin) } as usize;
        let add = self.goff & 4095;
        self.goff &= !4095;
        let ptr;
        unsafe {
            std::arch::asm!(
                "syscall",
                inout("rax") 9usize => ptr,
                in("rdi") self.begin,
                in("rsi") self.cap,
                in("rdx") 3,
                in("r10") 18,
                in("r8") 0,
                in("r9") self.goff,
                out("rcx") _,
                out("r11") _,
                options(nomem,preserves_flags)
            );
        }
        self.begin = ptr;
        self.cur = unsafe { self.begin.add(add) };
        self.end = unsafe { self.begin.add(self.cap) };
    }
    fn i32(&mut self) -> i32 {
        let sign = unsafe { self.cur.read() } == b'-';
        (if sign {
            self.cur = unsafe { self.cur.add(1) };
            self.u32().wrapping_neg()
        } else {
            self.u32()
        }) as i32
    }
    fn u32(&mut self) -> u32 {
        let mut c = unsafe { self.cur.cast::<u64>().read_unaligned() };
        let m = !c & 0x1010101010101010;
        let len = m.trailing_zeros() >> 3;
        c <<= (8 - len) << 3;
        c = (c & 0x0F0F0F0F0F0F0F0F).wrapping_mul(2561) >> 8;
        c = (c & 0x00FF00FF00FF00FF).wrapping_mul(6553601) >> 16;
        c = (c & 0x0000FFFF0000FFFF).wrapping_mul(42949672960001) >> 32;
        self.cur = unsafe { self.cur.add(len as usize) };
        if len == 8 {
            if unsafe { self.cur.read() } & 0x10 != 0 {
                c *= 10;
                c += (unsafe { self.cur.read() } - b'0') as u64;
                self.cur = unsafe { self.cur.add(1) };
            }
            if unsafe { self.cur.read() } & 0x10 != 0 {
                c *= 10;
                c += (unsafe { self.cur.read() } - b'0') as u64;
                self.cur = unsafe { self.cur.add(1) };
            }
        }
        self.cur = unsafe { self.cur.add(1) };
        c as u32
    }
}
struct Writer {
    buf: Vec<u8>,
    off: usize,
}
impl Drop for Writer {
    fn drop(&mut self) {
        self.flush();
    }
}
// #[repr(align(16))]
struct B128([u8; 16]);
// #[target_feature(enable = "avx2")]
unsafe fn cvt8(out: &mut B128, n: u32) -> usize {
    use std::arch::x86_64::*;
    let x = _mm_cvtsi32_si128(n as i32);
    let div_10000 = _mm_set1_epi32(0xd1b71759u32 as i32);
    let mul_10000_merge = _mm_set1_epi32(55536);
    let div_var = _mm_setr_epi16(
        8389,
        5243,
        13108,
        0x8000u16 as i16,
        8389,
        5243,
        13108,
        0x8000u16 as i16,
    );
    let shift_var = _mm_setr_epi16(
        1 << 7,
        1 << 11,
        1 << 13,
        (1 << 15) as i16,
        1 << 7,
        1 << 11,
        1 << 13,
        (1 << 15) as i16,
    );
    let mul_10 = _mm_set1_epi16(10);
    let ascii0 = _mm_set1_epi8(48);
    let x_div_10000 = _mm_srli_epi64::<45>(_mm_mul_epu32(x, div_10000));
    let y = _mm_add_epi32(x, _mm_mul_epu32(x_div_10000, mul_10000_merge));
    let t0 = _mm_slli_epi16::<2>(_mm_shuffle_epi32::<5>(_mm_unpacklo_epi16(y, y)));
    let t1 = _mm_mulhi_epu16(t0, div_var);
    let t2 = _mm_mulhi_epu16(t1, shift_var);
    let t3 = _mm_slli_epi64::<16>(t2);
    let t4 = _mm_mullo_epi16(t3, mul_10);
    let t5 = _mm_sub_epi16(t2, t4);
    let t6 = _mm_packus_epi16(_mm_setzero_si128(), t5);
    let mask = _mm_movemask_epi8(_mm_cmpeq_epi8(t6, _mm_setzero_si128()));
    let offset = (mask & !0x8000).trailing_ones() as usize;
    let ascii = _mm_add_epi8(t6, ascii0);
    _mm_store_si128(out.0.as_mut_ptr().cast(), ascii);
    offset
}
impl Writer {
    fn new(capacity: usize) -> Self {
        Self {
            buf: vec![0; capacity],
            off: 0,
        }
    }
    fn flush(&mut self) {
        unsafe {
            std::arch::asm!(
                "syscall",
                inout("rax") 1 => _,
                in("rdi") 1,
                in("rsi") self.buf.as_ptr(),
                in("rdx") self.off,
                out("rcx") _,
                out("r11") _,
                options(readonly,preserves_flags)
            )
        }
        self.off = 0;
    }
    fn try_flush(&mut self, readahead: usize) {
        if self.off + readahead > self.buf.len() {
            self.flush();
        }
    }
    fn byte(&mut self, b: u8) {
        self.try_flush(1);
        self.buf[self.off] = b;
        self.off += 1;
    }
    fn i32(&mut self, n: i32) {
        if n < 0 {
            self.byte(b'-');
            self.u32((n as u32).wrapping_neg());
        } else {
            self.u32(n as u32);
        }
    }
    fn u32(&mut self, n: u32) {
        let mut b128 = B128([0u8; 16]);
        let mut off;
        if n >= 100_000_000 {
            self.try_flush(10);
            let mut hi = n / 100_000_000;
            let lo = n % 100_000_000;
            unsafe { cvt8(&mut b128, lo) };
            off = 8;
            off -= 1;
            b128.0[off] = (hi % 10) as u8 + b'0';
            if hi >= 10 {
                off -= 1;
                hi /= 10;
                b128.0[off] = hi as u8 + b'0';
            }
        } else {
            self.try_flush(8);
            off = unsafe { cvt8(&mut b128, n) };
        }
        let len = 16 - off;
        self.buf[self.off..self.off + len].copy_from_slice(&b128.0[off..]);
        self.off += len;
    }
}


#elif other5
// #include <bits/stdc++.h>
using namespace std;

// #define pb emplace_back
// #define all(x) x.begin(), x.end()
// #define ff first
// #define ss second
// #define LLINF 0x3f3f3f3f3f3f3f3f
// #define INF 0x3f3f3f3f
// #define uniq(x) sort(all(x)), x.resize(unique(all(x))-x.begin());
// #define sz(x) (int)x.size()
// #define pw(x) (1LL<<x)

using pii = pair<int, int>;
using ll = long long;
using ld = long double;
const ll MOD = 1e9 + 7;
const ld PI = acos(-1.0);

// #pragma GCC optimize("O3")
// #pragma GCC optimize("Ofast")
// #pragma GCC optimize("unroll-loops")
// #pragma GCC target("avx,avx2")

static struct FastInput {
    static constexpr int BUF_SIZE = 1 << 20;
    char buf[BUF_SIZE];
    size_t chars_read = 0;
    size_t buf_pos = 0;
    FILE *in = stdin;
    char cur = 0;

    inline char get_char() {
        if (buf_pos >= chars_read) {
            chars_read = fread(buf, 1, BUF_SIZE, in);
            buf_pos = 0;
            buf[0] = (chars_read == 0 ? -1 : buf[0]);
        }
        return cur = buf[buf_pos++];
    }

    inline void tie(int) {}

    inline explicit operator bool() {
        return cur != -1;
    }

    inline static bool is_blank(char c) {
        return c <= ' ';
    }

    inline bool skip_blanks() {
        while (is_blank(cur) && cur != -1) {
            get_char();
        }
        return cur != -1;
    }

    inline FastInput& operator>>(char& c) {
        skip_blanks();
        c = cur;
        return *this;
    }

    inline FastInput& operator>>(string& s) {
        if (skip_blanks()) {
            s.clear();
            do {
                s += cur;
            } while (!is_blank(get_char()));
        }
        return *this;
    }

template <typename T>
    inline FastInput& read_integer(T& n) {
// unsafe, doesn't check that characters are actually digits
        n = 0;
        if (skip_blanks()) {
            int sign = +1;
            if (cur == '-') {
                sign = -1;
                get_char();
            }
            do {
                n += n + (n << 3) + cur - '0';
            } while (!is_blank(get_char()));
            n *= sign;
        }
        return *this;
    }

template <typename T>
    inline typename enable_if<is_integral<T>::value, FastInput&>::type operator>>(T& n) {
        return read_integer(n);
    }

// #if !defined(_WIN32) || defined(_WIN64)
    inline FastInput& operator>>(__int128& n) {
        return read_integer(n);
    }
// #endif

template <typename T>
    inline typename enable_if<is_floating_point<T>::value, FastInput&>::type operator>>(T& n) {
// not sure if really fast, for compatibility only
        n = 0;
        if (skip_blanks()) {
            string s;
            (*this) >> s;
            sscanf(s.c_str(), "%lf", &n);
        }
        return *this;
    }
} fast_input;

// #define cin fast_input

static struct FastOutput {
    static constexpr int BUF_SIZE = 1 << 20;
    char buf[BUF_SIZE];
    size_t buf_pos = 0;
    static constexpr int TMP_SIZE = 1 << 20;
    char tmp[TMP_SIZE];
    FILE *out = stdout;

    inline void put_char(char c) {
        buf[buf_pos++] = c;
        if (buf_pos == BUF_SIZE) {
            fwrite(buf, 1, buf_pos, out);
            buf_pos = 0;
        }
    }

    ~FastOutput() {
        fwrite(buf, 1, buf_pos, out);
    }

    inline FastOutput& operator<<(char c) {
        put_char(c);
        return *this;
    }

    inline FastOutput& operator<<(const char* s) {
        while (*s) {
            put_char(*s++);
        }
        return *this;
    }

    inline FastOutput& operator<<(const string& s) {
        for (int i = 0; i < (int) s.size(); i++) {
            put_char(s[i]);
        }
        return *this;
    }

template <typename T>
    inline char* integer_to_string(T n) {
// beware of TMP_SIZE
        char* p = tmp + TMP_SIZE - 1;
        if (n == 0) {
            *--p = '0';
        } else {
            bool is_negative = false;
            if (n < 0) {
                is_negative = true;
                n = -n;
            }
            while (n > 0) {
                *--p = (char) ('0' + n % 10);
                n /= 10;
            }
            if (is_negative) {
                *--p = '-';
            }
        }
        return p;
    }

template <typename T>
    inline typename enable_if<is_integral<T>::value, char*>::type stringify(T n) {
        return integer_to_string(n);
    }

// #if !defined(_WIN32) || defined(_WIN64)
    inline char* stringify(__int128 n) {
        return integer_to_string(n);
    }
// #endif

template <typename T>
    inline typename enable_if<is_floating_point<T>::value, char*>::type stringify(T n) {
        sprintf(tmp, "%.17f", n);
        return tmp;
    }

template <typename T>
    inline FastOutput& operator<<(const T& n) {
        auto p = stringify(n);
        for (; *p != 0; p++) {
            put_char(*p);
        }
        return *this;
    }
} fast_output;

// #define cout fast_output

const int N = 5000005;
int n,l,a[N];

int main() {
    ios_base::sync_with_stdio(0); cin.tie(0);
    cin >> n >> l;
    deque<int> dq;
    for(int i=1 ; i<=n ; i++) cin >> a[i];
    for(int i=1 ; i<=n ; i++) {
        while(sz(dq)) {
            if(dq.front() < i-l+1) dq.pop_front();
            else break;
        }
        while(1) {
            if(!sz(dq) || a[dq.back()] <= a[i]) {
                dq.push_back(i);
                break;
            }
            dq.pop_back();
        }
        cout << a[dq.front()] << ' ';
    }
    cout << '\n';
}
#endif
}
