using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 5
이름 : 배성훈
내용 : 덱 2
    문제번호 : 28279번

    덱 문제다
    48_08을 해결하기 위해 먼저 푼 문제다
    직접 두 포인터 알고리즘을 이용해 덱을 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0678
    {

        public class MyDeque
        {

            private int[] arr;
            private int capacity;
            private int count;

            private int head;
            private int tail;

            public int Count => count;
            public MyDeque(int _capacity)
            {

                capacity = _capacity;
                arr = new int[capacity];
                count = 0;
            }

            public void PushFront(int _add)
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

            public void PushBack(int _add)
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

            public int PopBack()
            {

                if (count <= 0) return -1;
                count--;
                int ret = arr[tail];
                tail = tail == 0 ? capacity - 1 : tail - 1;
                return ret;
            }

            public int PopFront()
            {

                if (count <= 0) return -1;
                count--;
                int ret = arr[head];
                head = head == capacity - 1 ? 0 : head + 1;
                return ret;
            }

            public int Front() 
            {

                if (count == 0) return -1;
                return arr[head];
            }
            public int Back()
            {

                if (count == 0) return -1;
                return arr[tail];
            }
        }

        static void Main678(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;
            int n;
            MyDeque deque;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 8);
                n = ReadInt();

                deque = new(n);
                for (int i = 0; i < n; i++)
                {

                    int f = ReadInt();
                    int b;
                    switch (f)
                    {

                        case 1:
                            b = ReadInt();
                            deque.PushFront(b);
                            break;

                        case 2:
                            b = ReadInt();
                            deque.PushBack(b);
                            break;

                        case 3:
                            b = deque.PopFront();
                            sw.Write($"{b}\n");
                            break;

                        case 4:
                            b = deque.PopBack();
                            sw.Write($"{b}\n");
                            break;

                        case 5:
                            sw.Write($"{deque.Count}\n");
                            break;

                        case 6:
                            b = deque.Count == 0 ? 1 : 0;
                            sw.Write($"{b}\n");
                            break;

                        case 7:
                            b = deque.Front();
                            sw.Write($"{b}\n");
                            break;

                        case 8:
                            b = deque.Back();
                            sw.Write($"{b}\n");
                            break;
                    }
                }

                sr.Close();
                sw.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;

                while((c= sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
namespace Baekjoon;

using System;
using System.Text;

public class Program
{
    private static readonly StreamReader _sr = new(new BufferedStream(Console.OpenStandardInput()));
    private static readonly StreamWriter _sw = new(new BufferedStream(Console.OpenStandardOutput()));
    private static readonly StringBuilder _ret = new StringBuilder();

    private static void Main(string[] args)
    {
        var cmdCount = ScanInt();
        Deque deque = new();
        for (int i = 0; i < cmdCount; i++)
        {
            var cmdType = ScanInt();
            int arg;
            switch (cmdType)
            {
                case 1:
                    arg = ScanInt();
                    deque.PushFront(arg);
                    break;
                case 2:
                    arg = ScanInt();
                    deque.PushBack(arg);
                    break;
                case 3:
                    Append(deque.PopFront());
                    break;
                case 4:
                    Append(deque.PopBack());
                    break;
                case 5:
                    Append(deque.Count);
                    break;
                case 6:
                    Append(deque.Count == 0 ? 1 : 0);
                    break;
                case 7:
                    Append(deque.PeekFront());
                    break;
                case 8:
                    Append(deque.PeekBack());
                    break;
            }
        }
        _sw.Write(_ret);
        _sr.Close();
        _sw.Close();
    }

    private static void Append(int v)
    {
        _ret.Append(v).Append('\n');
    }

    static int ScanInt()
    {
        int c, n = 0;
        while (!((c = _sr.Read()) is ' ' or '\n' or -1))
        {
            if (c == '\r')
            {
                _sr.Read();
                break;
            }
            n = 10 * n + c - '0';
        }
        return n;
    }
}

internal class Deque
{
    readonly int[] _array = new int[1_000_000];
    int _headIndex = 0, _tailIndex = 1;
    public int Count
    {
        get
        {
            var diff = _tailIndex - _headIndex - 1;
            if (diff < 0) diff += _array.Length;
            return diff;
        }
    }

    internal int PeekBack()
    {
        var ret = -1;
        if (Count > 0)
        {
            ret = _array[Sub1ByCycle(_tailIndex)];
        }
        return ret;
    }

    internal int PeekFront()
    {
        var ret = -1;
        if (Count > 0)
        {
            ret = _array[Add1ByCycle(_headIndex)];
        }
        return ret;
    }

    internal int PopBack()
    {
        var ret = -1;
        if (Count > 0)
        {
            ret = _array[_tailIndex = Sub1ByCycle(_tailIndex)];
        }
        return ret;
    }

    internal int PopFront()
    {
        var ret = -1;
        if (Count > 0)
        {
            ret = _array[_headIndex = Add1ByCycle(_headIndex)];
        }
        return ret;
    }

    internal void PushBack(int arg)
    {
        _array[_tailIndex] = arg;
        _tailIndex = Add1ByCycle(_tailIndex);
    }

    internal void PushFront(int arg)
    {
        _array[_headIndex] = arg;
        _headIndex = Sub1ByCycle(_headIndex);
    }

    int Add1ByCycle(int value)
    {
        if (++value >= _array.Length)
            value -= _array.Length;
        return value;
    }

    int Sub1ByCycle(int value)
    {
        if (--value < 0)
            value += _array.Length;
        return value;
    }
}
#endif
}
