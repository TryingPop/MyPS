using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 26
이름 : 배성훈
내용 : 풍선 터뜨리기
    문제번호 : 2346번

    덱을 쓰는 문제인데, 값의 범위가 1 ~ 1000으로 순차적으로 놓여서 세그먼트 트리로 풀었다
    현재 자리에서 다음꺼 빼야하는 부분에서 잘못 로직을 설정해서 한 번 틀렸다

    만약 인덱스 순서가 무작위로 i번째 값을 빼야한다면, 
    리스트를 사용해 해당 인덱스의 원소를 뺄 것이다
*/

namespace BaekJoon.etc
{
    internal class etc_0099
    {

        static void Main99(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);
            int[] jump = new int[len + 1];

            for (int i = 1; i <= len; i++)
            {

                jump[i] = ReadInt(sr);
            }

            sr.Close();

            int log = (int)Math.Ceiling(Math.Log2(len)) + 1;
            int[] seg = new int[1 << log];

            for (int i = 1; i <= len; i++)
            {

                Update(seg, 1, len, i);
            }

            using (StreamWriter sw = new(Console.OpenStandardOutput()))
            {

                int cur = 1;
                int curLen = len;
                for (int i = 0; i < len; i++)
                {

                    int get = GetValue(seg, 1, len, cur);
                    curLen--;
                    cur += jump[get];
                    if (jump[get] > 0) cur--;

                    while(cur <= 0 || cur > curLen)
                    {

                        if (cur <= 0) cur += curLen;
                        else cur -= curLen;
                        if (curLen == 0) break;
                    }

                    sw.Write(get);
                    sw.Write(' ');
                }
            }

            sr.Close();
        }

        static int GetValue(int[] _seg, int _start, int _end, int _getVal, int _idx = 1)
        {

            if (_start == _end)
            {

                _seg[_idx - 1] = 0;
                return _start;
            }
            int ret = 0;
            int mid = (_start + _end) / 2;
            if (_getVal <= _seg[2 * _idx - 1]) ret += GetValue(_seg, _start, mid, _getVal, _idx * 2);
            else ret += GetValue(_seg, mid + 1, _end, _getVal - _seg[2 * _idx - 1], _idx * 2 + 1);

            _seg[_idx - 1] = _seg[_idx * 2 - 1] + _seg[_idx * 2];
            return ret;
        }

        static void Update(int[] _seg, int _start, int _end, int _addIdx, int _idx = 1)
        {

            if (_start == _end)
            {

                _seg[_idx - 1] = 1;
                return;
            }

            int mid = (_start + _end) / 2;

            if (_addIdx > mid) Update(_seg, mid + 1, _end, _addIdx, _idx * 2 + 1);
            else Update(_seg, _start, mid, _addIdx, _idx * 2);

            _seg[_idx - 1] = _seg[_idx * 2 - 1] + _seg[_idx * 2];
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            bool plus = true;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
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
#if other
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Study.BaekJoon
{

    // 풍선 터뜨리기

    internal class Problem2346Solver
    {
        static List<(int, int)> deque = new List<(int, int)>();
        static StringBuilder sb = new StringBuilder();

        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            string[] M = Console.ReadLine().Split(' ');
            
            for (int i = 0; i < N; i++)
            {
                deque.Add((i + 1, int.Parse(M[i])));
            }

            int idx = 0;
            while (deque.Count != 0)
            {
                int cnt = deque[idx].Item2;
                sb.Append($"{deque[idx].Item1} ");
                deque.RemoveAt(idx);

                if (deque.Count == 0)
                {
                    break;
                }

                if (cnt > 0)
                {
                    idx = (idx + cnt - 1) % deque.Count;
                }
                else
                {
                    idx = (idx + cnt) % deque.Count;
                    if (idx < 0) idx += deque.Count;
                }
            }

            Console.WriteLine(sb.ToString());
        }
    }
}

#elif other2
namespace Baekjoon;

using System.Text;

public class Program
{
    private static readonly StreamReader _sr = new(new BufferedStream(Console.OpenStandardInput()));
    private static readonly StreamWriter _sw = new(new BufferedStream(Console.OpenStandardOutput()));
    private static readonly StringBuilder _ret = new StringBuilder();

    private static void Main(string[] args)
    {
        var balloonCnt = ScanSignedInt();
        var balloonPaper = new int[balloonCnt + 1];
        for (int i = 1; i <= balloonCnt; i++)
        {
            var num = ScanSignedInt();
            balloonPaper[i] = num;
        }
        Deque deque = new(balloonCnt);
        for (int i = 0; i < balloonCnt; i++)
        {
            var balloon = PopAndMove();
            Append(balloon);
        }
        _ret.Length--;

        _sw.Write(_ret);
        _sr.Close();
        _sw.Close();

        int PopAndMove()
        {
            var ret = deque.PopFront();
            var paper = balloonPaper[ret];
            if (deque.Count == 0) return ret;
            if (paper > 0)
            {
                paper--;
                paper %= deque.Count;
                for (int i = 0; i < paper; i++)
                {
                    var front = deque.PopFront();
                    deque.PushBack(front);
                }
            }
            else
            {
                paper *= -1;
                paper %= deque.Count;
                for (int i = 0; i < paper; i++)
                {
                    var front = deque.PopBack();
                    deque.PushFront(front);
                }
            }
            return ret;
        }
    }

    private static void Append(int v)
    {
        _ret.Append(v).Append(' ');
    }

    static int ScanSignedInt()
    {
        int c = _sr.Read(), n = 0;
        if (c == '-')
            while (!((c = _sr.Read()) is ' ' or '\n' or -1))
            {
                if (c == '\r')
                {
                    _sr.Read();
                    break;
                }
                n = 10 * n - c + '0';
            }
        else
        {
            n = c - '0';
            while (!((c = _sr.Read()) is ' ' or '\n' or -1))
            {
                if (c == '\r')
                {
                    _sr.Read();
                    break;
                }
                n = 10 * n + c - '0';
            }
        }
        return n;
    }
}

internal class Deque
{
    readonly int[] _array = new int[2_000];
    int _headIndex, _tailIndex;

    public Deque(int cnt)
    {
        for (int i = 1; i <= cnt; i++)
        {
            _array[i] = i;
        }
        _headIndex = 0;
        _tailIndex = cnt + 1;
    }

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
