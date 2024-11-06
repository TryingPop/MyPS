using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 6
이름 : 배성훈
내용 : 연세워터파크
    문제번호 : 15678번

    덱, dp, 덱을 이용한 dp문제다
    덱(deque)과 그리디, dp를 이용해 풀었다

    아이디어는 다음과 같다
    dp[i] = max(max(dp[j]), 0)
    형태다 j는 i - k <= j < i 까지인 인덱스이다
    그래서 dp[j]의 최대값을 찾는데 덱을 이용했다

    그런데, 48_09, 48_08처럼 앞에서 뒷부분을 빼고 싶었으나,
    구현에 어려움을 겪어 여기서는 먼저 정답을 계산하고, 해당 값보다 작은 것들을 빼는 연산을 후처리 했다
*/

namespace BaekJoon._48
{
    internal class _48_10
    {

        public class MyDeque<T>
        {

            private T[] arr;
            private int capacity;
            private int count;

            private int head;
            private int tail;

            public int Count => count;

            public bool Empty => count == 0;

            public MyDeque(int _capacity)
            {

                capacity = _capacity;
                arr = new T[capacity];
                count = 0;
            }

            public void PushFront(T _add)
            {

                if (count >= capacity) return;

                if (count == 0)
                {

                    head = 0;
                    tail = 0;
                    arr[head] = _add;
                    count = 1;
                    return;
                }
                count++;
                head = head == 0 ? capacity - 1 : head - 1;
                arr[head] = _add;
            }

            public void PushBack(T _add)
            {

                if (count >= capacity) return;

                if (count == 0)
                {

                    head = 0;
                    tail = 0;
                    arr[head] = _add;
                    count = 1;
                    return;
                }

                count++;
                tail = tail == capacity - 1 ? 0 : tail + 1;
                arr[tail] = _add;
            }

            public T PopBack()
            {

                count--;
                T ret = arr[tail];
                tail = tail == 0 ? capacity - 1 : tail - 1;
                return ret;
            }

            public T PopFront()
            {

                count--;
                T ret = arr[head];
                head = head == capacity - 1 ? 0 : head + 1;
                return ret;
            }

            public T Front => arr[head];

            public T Back => arr[tail];

        }

        static void Main10(string[] args)
        {

            StreamReader sr;
            int n, k;
            MyDeque<int> deque;
            long[] dp;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();
                deque = new(k + 1);
                dp = new long[n];

                long ret = -1_000_000_000;

                for (int i = 0; i < n; i++)
                {

                    long cur = ReadInt();

                    // 그리디 아이디어!
                    // 시작 지점이 아무 곳이나 가능하므로 0과 비교해야한다!
                    dp[i] = Math.Max(cur + dp[deque.Front], cur);
                    // 언제든지 끝낼 수 있어, 매번 최대값 확인!
                    ret = ret < dp[i] ? dp[i] : ret;

                    // i - k ~ i - 1중에 가장 큰 값을 확인하는데
                    // 후처리이므로 다음 인덱스에 대해 생각해야한다
                    // 그래서 i - k + 1 ~ i 번까지만 살린다
                    while (!deque.Empty && deque.Front <= i - k)
                    {

                        deque.PopFront();
                    }

                    // 이제 최대값을 앞에 담기게 해야하므로 현재와 비교해서 가장 큰 값을 앞에 담게 한다
                    while(!deque.Empty && dp[deque.Back] < dp[i])
                    {

                        deque.PopBack();
                    }

                    deque.PushBack(i);

                }

                Console.WriteLine(ret);
            }

            int ReadInt()
            {

                int c = sr.Read();
                if (c == '\r') c = sr.Read();
                if (c == -1 || c == ' ' || c == '\n') return 0;
                bool plus = c != '-';
                int ret;
                if (plus) ret = c - '0';
                else ret = 0;

                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
using System;
using System.IO;
using System.Linq;

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
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var nd = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var (n, d) = (nd[0], nd[1]);

        var k = sr.ReadLine().Split(' ').Select(Int64.Parse).ToArray();
        var dp = new long[n];
        var deque = new Deque<(int idx, long dpvalue)>();

        for (var idx = k.Length - 1; idx >= 0; idx--)
        {
            while (deque.Count != 0 && deque.Front.idx - idx > d)
                deque.PopFront();

            if (deque.Count == 0)
                dp[idx] = k[idx];
            else
                dp[idx] = Math.Max(k[idx], k[idx] + deque.Front.dpvalue);

            while (deque.Count != 0 && deque.Back.dpvalue < dp[idx])
                deque.PopBack();

            deque.PushBack((idx, dp[idx]));
        }

        sw.WriteLine(dp.Max());
    }
}

#elif other2
using System.IO;
using System.Text;
using System;
using System.Collections.Generic;
class Programs
{
    static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), Encoding.Default);
    static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), Encoding.Default);
    static long[] arr;
    class Deque<T>
    {
        public int l = -1, r = -1;
        public T[] arr;
        public int Count = 0;
        public int size;
        public Deque(int size = 200001)
        {
            arr = new T[size];
            this.size = size;
        }
        public void PushFront(T a)
        {
            if (l == -1)
            {
                l = r = 0;
            }
            else
            {
                l--;
                if (l < 0)
                {
                    l = size - 1;
                }
            }
            arr[l] = a;
            Count++;
        }
        public void PopFront()
        {
            l++;
            if (l >= size)
            {
                l = 0;
            }
            Count--;
        }

        public void PushBack(T a)
        {
            if (r == -1)
            {
                l = r = 0;
            }
            else
            {
                r++;
                if (r >= size)
                {
                    r = 0;
                }
            }
            arr[r] = a;
            Count++;
        }
        public void PopBack()
        {
            r--;
            if (r < 0)
            {
                r = size - 1;
            }
            Count--;
        }
        public T Front()
        {
            return arr[l];
        }
        public T Back()
        {
            return arr[r];
        }
    }
    class DeQueue<T>
    {
        int Max = 200001;
        private T[] arr;
        private int l = -1, r = 0;
        public int Count = 0;
        public DeQueue()
        {
            arr = new T[Max];
        }
        //잘 기억이 안나네 ㅡㅡ;
        public void PushFront(T n)
        {
            if (l == -1)
            {
                r = l = 0;
            }
            else
            {
                l--;
                if (l < 0)
                {
                    l = Max - 1;
                }
            }
            arr[l] = n;
            Count++;
        }
        public void PushBack(T n)
        {
            if (l == -1)
            {
                l = r = 0;
            }
            else
            {
                r++;
                if (r >= Max)
                {
                    r = 0;
                }
            }
            arr[r] = n;
            Count++;
        }
        public void PopFront()
        {
            if (Count == 0)
            {
                return;
            }
            l++;
            if (l >= Max)
            {
                l = 0;
            }
            Count--;
        }
        public void PopBack()
        {
            if (Count == 0)
            {
                return;
            }
            r--;
            if (r < 0)
            {
                r = Max - 1;
            }
            Count--;
        }
        public T Front()
        {
            if (Count == 0)
            {
                return default;
            }
            return arr[l];
        }
        public T Back()
        {
            if (Count == 0)
            {
                return default;
            }
            return arr[r];
        }
    }
    static Deque<KeyValuePair<int, long>> dq = new Deque<KeyValuePair<int, long>>();
    static void Main(String[] args)
    {
        string[] str = sr.ReadLine().Split();
        int n = int.Parse(str[0]);
        int d = int.Parse(str[1]);
        str = sr.ReadLine().Split();
        arr = new long[n];
       long answer = -100000000;
        for (int i = 0; i < n; i++)
        {
            arr[i] = long.Parse(str[i]);
            while (dq.Count > 0 &&i- dq.Front().Key > d)
                {
                dq.PopFront();
            }
            if(dq.Count>0)
            {
              arr[i]=  Math.Max(dq.Front().Value + arr[i], arr[i]);
            }

            answer = Math.Max(answer,arr[i]);
            while (dq.Count > 0 &&dq.Back().Value<arr[i])
            {
                dq.PopBack();
            }
                dq.PushBack(new KeyValuePair<int,long>(i, arr[i]));
        }
        //어렵다 18;;풀이보자.
        sw.Write(answer);
        sw.Dispose();
    }
    
}

#elif other3
// #include<bits/stdc++.h>
using namespace std;
typedef long long ll;
typedef pair<int, int> pii;
typedef pair<ll, ll> pll;
// #define xx first
// #define yy second

int n, m;
ll a;
deque<pll> Q;
int main() {
    ios_base::sync_with_stdio(0);
    cin.tie(0);

    ll ans = -1987654321;
    cin >> n >> m;
    for (int i = 0; i < n; ++i) {
        cin >> a;
        if (!Q.empty() && Q.front().xx > 0)
            a += Q.front().xx;
        ans = max(ans, a);
        while (!Q.empty()) {
            if (Q.back().xx <= a) Q.pop_back();
            else break;
        }
        while (!Q.empty()) {
            if (Q.front().yy < i - m + 1) Q.pop_front();
            else break;
        }
        Q.emplace_back(a, i);
    }
    cout << ans;
}
#endif
}
