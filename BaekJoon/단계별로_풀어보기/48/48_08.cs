using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

/*
날짜 : 2024. 5. 5
이름 : 배성훈
내용 : 최솟값 찾기
    문제번호 : 11003번

    덱, 우선순위 큐 문제다
    이전에는 우선순위 큐로 1.5초?에 걸쳐 해결했었다

    덱에 인덱스를 저장하는데,
    인덱스가 범위를 벗어나는 애들을 매번 확인해서 앞에서부터 빼준다
    그리고 최솟값을 찾기에 현재값보다 큰 애들을 뒤에서부터 뺀다
    그리고 현재 값을 뒤에 넣으면, 덱에 맨 앞에는 최소값 인덱스가 담기게된다
*/

namespace BaekJoon._48
{
    internal class _48_08
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

                // if (count <= 0) throw new Exception("원소 없어용!");
                count--;
                T ret = arr[tail];
                tail = tail == 0 ? capacity - 1 : tail - 1;
                return ret;
            }

            public T PopFront()
            {

                // if (count <= 0) throw new Exception("원소 없어용!");
                count--;
                T ret = arr[head];
                head = head == capacity - 1 ? 0 : head + 1;
                return ret;
            }

            public T Front()
            {

                // if (count == 0) throw new Exception("원소 없어용!");
                return arr[head];
            }
            public T Back()
            {

                // if (count == 0) throw new Exception("원소 없어용!");
                return arr[tail];
            }
        }

        static void Main8(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;
            int len;
            int n;
            MyDeque<int> deque;
            int[] arr;

            Solve();
            void Solve()
            {

                Init();

                for (int i = 0; i < len; i++)
                {

                    arr[i] = ReadInt();
                    while (!deque.Empty && deque.Front() <= i - n)
                    { 
                        
                        deque.PopFront(); 
                    }

                    while (!deque.Empty && arr[deque.Back()] >= arr[i])
                    {

                        deque.PopBack();
                    }

                    deque.PushBack(i);
                    sw.Write($"{arr[deque.Front()]} ");
                }

                sr.Close();
                sw.Close();
            }


            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 16);

                len = ReadInt();
                n = ReadInt();

                deque = new(n);
                arr = new int[len];
            }

            int ReadInt()
            {

                int c = sr.Read();
                if (c == '\r') c = sr.Read();
                if (c == -1 || c == ' ' || c == '\n') return 0;

                int ret = 0;
                bool plus = c != '-';
                if (plus) ret = c - '0';

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }
}
