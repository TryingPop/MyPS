using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 5
이름 : 배성훈
내용 : Mowing the Lawn
    문제번호 : 5977번

    dp, 덱, 덱을 이용한 구간 최댓값 트릭 문제다
    아이디어는 다음과 같다
    먼저 일반적인 dp로 점화식을 세워 보면
    dp[i] : i번째까지 최대값
    arr[i] : 누적합 배열

    그러면 
    dp[i] = max(dp[j - 1] + arr[i] - arr[j])
    여기서 j는 i - k <= j < i이다
    그리고 식을 다시 세워보면,
    dp[i] = arr[i] + max(dp[j - 1] - arr[j])
    혹은, dp[i] = arr[i] - min(arr[j] - dp[j - 1])
    로 볼 수 있다

    max를 보면, i - k <= j < i 범위에서의 dp[j] - 1 - arr[j]의 최대값이 된다
    아니면 부호를 바꿔서 최솟값으로 찾아도 상관없다!
*/

namespace BaekJoon._48
{
    internal class _48_09
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
        static void Main9(string[] args)
        {

            StreamReader sr;
            int n, k;

            long[] arr;
            long[] dp;
            MyDeque<int> deque;

            Solve();

            void Solve()
            {

                Input();

                for (int i = 1; i <= n; i++)
                {

                    arr[i] = arr[i - 1] + ReadInt();
                }

                for (int i = 1; i <= n; i++)
                {

                    // i - k - 1이하의 인덱스는 삭제
                    while (!deque.Empty && deque.Front < i - k)
                    {

                        deque.PopFront();
                    }
                    
                    // 최대값을 찾는데 현재보다 작은건 삭제
                    while(!deque.Empty && GetMax(deque.Back) <= GetMax(i))
                    {

                        deque.PopBack();
                    }

                    deque.PushBack(i);
                    dp[i] = arr[i] + GetMax(deque.Front);

                    if (i <= k) dp[i] = Math.Max(dp[i], arr[i]);
                }

                Console.WriteLine(dp[n]);
            }

            long GetMax(int _idx)
            {

                return dp[_idx - 1] - arr[_idx];
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 4);
                n = ReadInt();
                k = ReadInt();

                arr = new long[n + 1];
                dp = new long[n + 1];

                // 2칸 뛰어넘기 고려
                deque = new(k + 2);
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
using System.IO;
using System.Text;
using System;
using System.Collections.Generic;
class Programs
{
    static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), Encoding.Default);
    static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), Encoding.Default);
    class Deque<T>
    {
        public int l=-1, r=-1;
        public T[] arr;
        public int Count = 0;
        public int size;
        public Deque(int size=100001)
        {
            arr = new T[size];
          this.size = size;
        }
        public void PushFront(T a)
        {
            if(l==-1)
            {
                l = r = 0;
            }
            else
            {
                l--;
                if(l<0)
                {
                    l = size -1;
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
                    r=0;
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
        int Max = 5000001;
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
    static long[] dp;

    static void Main(String[] args)
    {
        string[] str = sr.ReadLine().Split();
        int n = int.Parse(str[0]);
        int k = int.Parse(str[1]);
        long[] sum = new long[n+1];
        long[] arr = new long[n + 1];
        dp = new long[n+1];
        for (int i = 1; i <= n; i++)
        {
         arr[i]=   long.Parse(sr.ReadLine());
            sum[i] = arr[i] + sum[i - 1];
        }
        Deque<KeyValuePair<long, long>> deque = new Deque<KeyValuePair<long, long>>();
        deque.PushBack(new KeyValuePair<long, long>(0, 0));
        for (int i = 1; i <= n; i++)
        {

            while (deque.Count>0&&deque.Front().Value < i - k)
            {
                deque.PopFront();
            }
            while (deque.Count > 0 && deque.Back().Key >=   sum[i]- dp[i - 1]) //전체에서 그전dp값을 빼주면  그 구간합을 이룰 때 빼준 값이 나온다. 이 값이 작아질 수록 최대값을 구할 수있다.
            {
                deque.PopBack();
            }
            dp[i] =  sum[i]- deque.Front().Key; //전체 구간 부분합에서 dequeu에 들어간 최소값을 뺀 값이 현재 dp[i]의 최대값
            deque.PushBack(new KeyValuePair<long, long>(sum[i]-dp[i-1], i));
          dp[i] = Math.Max(dp[i], dp[i - 1]);//현재 구간합을 선택했을 경우와 안했을 경우 안했을 경우는 그전 dp가 최대값이다.
        }
        sw.Write(dp[n]);
        sw.Dispose();
    }
}

#elif other2
import java.util.ArrayDeque;
import java.util.Deque;

public class Main {
	public static long[] arr;
	public static long[] sum;
	public static long[] dp;
    public static void main(String[] args) throws Exception {
        int N = (int) readLong();
        int K = (int) readLong();
        arr = new long[N+1];
        sum = new long[N+1];
        dp = new long[N+1];
        for (int i = 1; i <= N; i++) {
            arr[i] = readLong();
        }

        for (int i = 1; i <= N; i++) {
            sum[i] = sum[i-1] + arr[i];
        }
        Deque<Integer> q = new ArrayDeque<>();
        for (int i = 1; i <= N; i++) {
            if(!q.isEmpty() && q.peekFirst() < i - K){
                q.pollFirst();
            }
            while(!q.isEmpty() && max(q.peekLast()) <= max(i)){
                q.pollLast();
            }
            q.offerLast(i);
            
            if(i <= K){
                dp[i] = sum[i];
            } else{
            	dp[i] = sum[i] + max(q.peekFirst());
            }
        }
        System.out.println(dp[N]);
    }
    
    public static long max(int idx) {
    	return dp[idx - 1] - sum[idx];
    }
    
    public static long readLong() throws Exception {
        long val = 0;
        long c = System.in.read();
        while (c <= ' ') {
            c = System.in.read();
        }
        boolean flag = (c == '-');
        if (flag)
            c = System.in.read();
        do {
            val = 10 * val + c - 48;
        } while ((c = System.in.read()) >= 48 && c <= 57);

        if (flag)
            return -val;
        return val;
    }
}
#endif
}
