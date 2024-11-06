using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 3
이름 : 배성훈
내용 : N번째 큰 수
    문제번호 : 2075번

    우선순위 큐 문제다
    직접 구현한 우선순위 큐는 제대로 작동 X
    -> 원인은 부모 갈 때 -1을 안해서 생긴 문제였다;
    수정하니 이상없이 작동 잘한다;
*/

namespace BaekJoon.etc
{
    internal class etc_0673
    {

        static void Main673(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput());
            int n, k;
            n = ReadInt();
            // MyData pq = new MyData(n);
            PriorityQueue<int, int> q = new PriorityQueue<int, int>(n);

            for (int i = 0; i < n * n; i++)
            {

                int cur = ReadInt();

                if (q.Count < n) q.Enqueue(cur, cur);
                else if (q.Peek() < cur)
                {

                    q.Dequeue();
                    q.Enqueue(cur, cur);
                }
                /*
                if (pq.Count < n) pq.Enqueue(cur);
                else if (pq.Peek() < cur)
                {
                    
                    pq.Dequeue();
                    pq.Enqueue(cur);
                }
                */
            }

            Console.WriteLine(q.Peek());
            // Console.WriteLine(pq.Dequeue());

            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if (c == '-')
                    {

                        plus = false;
                        continue;
                    }
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
        /// <summary>
        /// 현재 의도한 기능이 작동 X
        /// </summary>
        class MyData
        {

            private int[] arr;
            private int count;

            public int Count => count;
            public MyData(int capacity = 1_000_000)
            {

                arr = new int[capacity];
            }

            public void Enqueue(int _val)
            {

                arr[count] = _val;

                Up(count);
                count++;
            }

            private void Up(int _idx)
            {

                while (_idx > 0)
                {

                    int parent = (_idx - 1) / 2;
                    if (arr[_idx] >= arr[parent]) break;

                    int temp = arr[_idx];
                    arr[_idx] = arr[parent];
                    arr[parent] = temp;
                    _idx = parent;
                }
            }

            void Down()
            {

                int cur = 0;
                while (true)
                {

                    int l = cur * 2 + 1;
                    int r = cur * 2 + 2;

                    if (r < count)
                    {

                        if (arr[l] >= arr[r])
                        {

                            int temp = arr[cur];
                            arr[cur] = arr[r];
                            arr[r] = temp;

                            cur = r;
                            continue;
                        }
                        else if (arr[l] < arr[r])
                        {

                            int temp = arr[cur];
                            arr[cur] = arr[l];
                            arr[l] = temp;

                            cur = l;
                            continue;
                        }
                    }
                    else if (r == count)
                    {

                        int temp = arr[cur];
                        arr[cur] = arr[l];
                        arr[l] = temp;

                        cur = l;
                        continue;
                    }

                    break;
                }

                if (cur < count - 1)
                {

                    int temp = arr[cur];
                    arr[cur] = arr[count - 1];
                    arr[count - 1] = temp;

                    Up(cur);
                }
                count--;
            }

            public void Clear()
            {

                count = 0;
            }

            public int Peek()
            {

                return arr[0];
            }

            public int Dequeue()
            {

                if (count == 0) return 0;
                int ret = arr[0];
                arr[0] = 0;

                Down();
                return ret;
            }
        }
    }

#if other
internal class Program
{
    private static void Main(string[] args)
    {
        PriorityQueue<int, int> heap;
        using (var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput())))
        {
            var n = ScanInt(sr);
            heap = new PriorityQueue<int, int>(n);
            for (int i = 0; i < n; i++)
            {
                var num = ScanInt(sr);
                heap.Enqueue(num, num);
            }
            var end = n * (n - 1);
            for (int i = 0; i < end; i++)
            {
                var num = ScanInt(sr);
                if (heap.Peek() < num)
                    heap.EnqueueDequeue(num, num);
            }
        }
        Console.Write(heap.Peek());
    }
    static int ScanInt(StreamReader sr)
    {
        int c = sr.Read(), n = 0;
        if (c == '-')
            while (!((c = sr.Read()) is ' ' or '\n'))
            {
                if (c == '\r')
                {
                    sr.Read();
                    break;
                }
                n = 10 * n - (c - '0');
            }
        else
        {
            n = c - '0';
            while (!((c = sr.Read()) is ' ' or '\n'))
            {
                if (c == '\r')
                {
                    sr.Read();
                    break;
                }
                n = 10 * n + (c - '0');
            }
        }
        return n;
    }
}
#endif
}
