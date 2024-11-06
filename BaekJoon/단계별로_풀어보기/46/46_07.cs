using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 6
이름 : 배성훈
내용 : 데이터 구조
    문제번호 : 12899번

    사탕상자(46_09)와 중앙값(46_08) 문제와 같은 로직을 썼다
    그러니 무난하게 통과한다

    다만 여기서는 1개씩 넣고 1개씩 빼기에 addValue 변수를 따로 안쓴다
*/

namespace BaekJoon._46
{
    internal class _46_07
    {

        static void Main7(string[] args)
        {

            int MAX = 2_000_000;
            // 실상 200만 1을 넣어야하는데, 여기서는 그냥 200만을 넣는다 어차피 결과는 같다!
            int log = (int)Math.Ceiling(Math.Log2(MAX)) + 1;
            int[] seg = new int[1 << log];

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            int test = int.Parse(sr.ReadLine());

            for (int t = 0; t < test; t++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);

                if (temp[0] == 1)
                {

                    Update(seg, 1, MAX, temp[1]);
                }
                else
                {

                    int result = GetValue(seg, 1, MAX, temp[1]);
                    sw.WriteLine(result);
                }
            }

            sr.Close();
            sw.Close();
        }

        // update
        static void Update(int[] _seg, int _start, int _end, int _chkIdx, int _idx = 1)
        {

            if (_start == _end)
            {

                _seg[_idx - 1] += 1;
                return;
            }

            int mid = (_start + _end) / 2;
            if (_chkIdx > mid) Update(_seg, mid + 1, _end, _chkIdx, 2 * _idx + 1);
            else Update(_seg, _start, mid, _chkIdx, 2 * _idx);

            _seg[_idx - 1] = _seg[_idx * 2 - 1] + _seg[_idx * 2];
        }

        static int GetValue(int[] _seg, int _start, int _end, int _getIdx, int _idx = 1)
        {

            _seg[_idx - 1] -= 1;
            if (_start == _end) return _start;

            int mid = (_start + _end) / 2;
            int result;

            int l = _seg[2 * _idx - 1];

            if (_getIdx > l) result = GetValue(_seg, mid + 1, _end, _getIdx - l, _idx * 2 + 1);
            else result = GetValue(_seg, _start, mid, _getIdx, _idx * 2);

            return result;
        }
    }

#if other
namespace Baekjoon;

using System;
using System.Text;

public class Tree
{
    readonly int[] _tree = new int[1 << 22];
    const int Length = 2_000_001;

    internal void Add(int x) => Add(0, Length - 1, 1, x);

    private void Add(int start, int end, int node, int index)
    {
        _tree[node]++;
        if (start == end)
            return;
        var mid = (start + end) / 2;
        if (index <= mid)
            Add(start, mid, 2 * node, index);
        else
            Add(mid + 1, end, 2 * node + 1, index);
    }

    internal int Pop(int x) => Pop(0, Length - 1, 1, x);

    private int Pop(int start, int end, int node, int index)
    {
        _tree[node]--;
        if (start == end)
            return start;
        var mid = (start + end) / 2;
        var lChild = _tree[2 * node];
        if (index <= lChild)
            return Pop(start, mid, 2 * node, index);
        else
            return Pop(mid + 1, end, 2 * node + 1, index - lChild);
    }
}

public class Program
{
    private static void Main(string[] args)
    {
        using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        var queryCount = ScanInt(sr);
        var tree = new Tree();
        var sb = new StringBuilder();
        while (queryCount-- > 0)
        {
            int operation = ScanInt(sr), x = ScanInt(sr);
            if (operation == 1) tree.Add(x);
            else sb.Append(tree.Pop(x)).Append('\n');
        }
        Console.Write(sb);
    }

    static int ScanInt(StreamReader sr)
    {
        int c, n = 0;
        while (!((c = sr.Read()) is ' ' or '\n' or -1))
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            n = 10 * n + c - '0';
        }
        return n;
    }
}
#endif
}
