using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 15
이름 : 배성훈
내용 : 카드 놓기
    문제번호 : 18115번

    자료구조 덱 문제이다
    덱 자료구조를 만드는게 아닌 핵심만 빼온 두 포인터로 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0238
    {

        static void Main238(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            int[] ret = new int[n];

            int ptr1 = 0;
            int ptr2 = 1;
            int ptr3 = n - 1;

            int curCard = n;
            for (int i = 0; i < n; i++)
            {

                int order = ReadInt(sr);

                if (order == 1)
                {

                    ret[ptr1] = curCard--;

                    ptr1 = ptr2;
                    ptr2++;
                }
                else if (order == 2)
                {

                    ret[ptr2] = curCard--;

                    ptr2++;
                }
                else
                {

                    ret[ptr3] = curCard--;
                    ptr3--;
                }
            }

            sr.Close();

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 0; i < n; i++)
                {

                    sw.Write(ret[i]);
                    sw.Write(' ');
                }
            }
        }
        
        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while ((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace cs18115
{
    internal class Program
    {
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StringBuilder sb = new StringBuilder();

        static void Main(string[] args)
        {
            int N = int.Parse(sr.ReadLine());
            int[] line = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);

            LooongCat cat = new LooongCat(N);
            for(int i = N-1; i >= 0; i--)
            {
                if (line[i] == 1) cat.One(N - i);
                else if (line[i] == 2) cat.Two(N - i);
                else if (line[i] == 3) cat.Three(N - i);
            }

            foreach(var num in cat.Early())
            {
                sb.Append(num).Append(' ');
            }

            sw.Write(sb);
            sw.Close();
            sr.Close();
        }
    }

    internal class LooongCat
    {

        int size;
        int[] mount;
        int head;
        int tail;

        internal LooongCat(int len)
        {
            size = len + 2;
            mount = new int[size];
            head = 1;
            tail = 0;
        }

        public void One(int num)
        {
            head = (head + size - 1) % size;
            mount[head] = num;
        }

        public void Two(int num)
        {
            int tt = mount[head];
            mount[head] = num;
            One(tt);
        }

        public void Three(int num)
        {
            tail = (tail + 1) % size;
            mount[tail] = num;
        }

        public int[] Early()
        {
            int[] early = new int[size - 2];
            for(int i = 0; i < size-2; i++)
            {
                early[i] = mount[(head + i) % size];
            }
            return early;
        }
    }
}
#elif other2

#nullable disable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public sealed class Deque<T>
{
    public bool IsEmpty => Count == 0;
    public int Count => _count;

    public T Front => _arr[_stIncl];
    public T Back => _arr[(_edExcl - 1 + _arr.Length) % _arr.Length];

    private int _stIncl;
    private int _edExcl;
    private int _count;

    private T[] _arr;

    public Deque()
    {
        _arr = new T[64];
    }

    public void PushFront(T v)
    {
        EnsureSize(1 + _count);
        _count++;

        _stIncl = (_stIncl - 1 + _arr.Length) % _arr.Length;
        _arr[_stIncl] = v;
    }
    public T PopFront()
    {
        if (_count == 0)
            throw new InvalidOperationException();

        _count--;

        var val = _arr[_stIncl];
        _stIncl = (_stIncl + 1) % _arr.Length;

        return val;
    }
    public void PushBack(T v)
    {
        EnsureSize(1 + _count);
        _count++;

        _arr[_edExcl] = v;
        _edExcl = (_edExcl + 1) % _arr.Length;
    }
    public T PopBack()
    {
        if (_count == 0)
            throw new InvalidOperationException();

        _count--;

        _edExcl = (_edExcl + _arr.Length - 1) % _arr.Length;
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

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var d = new Deque<int>();

        // backside is topside
        for (var idx = 0; idx < n; idx++)
            d.PushBack(n - idx);

        var placed = new List<int>();
        var seq = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        foreach (var v in seq)
        {
            if (v == 1)
            {
                placed.Add(d.PopBack());
            }
            if (v == 2)
            {
                var tmp = d.PopBack();
                placed.Add(d.PopBack());
                d.PushBack(tmp);
            }
            if (v == 3)
            {
                placed.Add(d.PopFront());
            }
        }

        var mapping = placed
            .Select((v, idx) => (v, idx))
            .ToDictionary(v => v.v, v => n - v.idx);

        for (var idx = 1; idx <= n; idx++)
            sw.Write($"{mapping[idx]} ");
    }
}

#endif
}
