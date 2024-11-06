using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 5
이름 : 배성훈
내용 : 디지털 비디오 디스크(DVDs)
    문제번호 : 9345번

    1 - 1이므로 i부터 j까지 있는지 확인하는 것을 min - max로 풀었다

    예를 들어  5개에서 1 ~ 5가 있는지 확인한다면
    순서 상관없이 1, 2, 3, 4, 5가 있어야한다

    만약 1, 2, 3, 4, 5 중에 하나가 빠진다면, 2가 아닌 다른 숫자가 있다고 하자
    그러면 1 - 1이 관계이므로 2 자리에 1, 2, 3, 4, 5 이외에 다른 숫자가 와야한다!
    
    예를 들어 0이 왓을 경우
    0, 1, 3, 4, 5 가 된다 이때의 min은 0, max는 5이다

    1, 2, 3, 4, 5의 min은 1, max는 5이고

    1 != 0으로 다른 값을 갖는다

    반면 2자리에 1보다 작은 경우 0 이외에는 5보다 큰 수가 오게 될 것이다
    7이 온다고 하면
    1, 3, 4, 5, 7이고 min = 1, max = 7
    5 != 7으로 max에서 다른 값을 갖게된다

    그리고 서로 다른 5개 배열의 min과 max가 1, 5로 서로 같다면 
    순서를 무시하면 가능한 경우는 1, 2, 3, 4, 5 뿐이다!

    이러한 방법으로 접근해서 푸니 이상없이 통과했다
*/

namespace BaekJoon._46
{
    internal class _46_05
    {

        static void Main5(string[] args)
        {

            string Y = "YES";
            string N = "NO";

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = int.Parse(sr.ReadLine());
            int[] nums = new int[100_000];
            // 2 ^ 18 = 26만
            int size = 1 << 18;
            (int min, int max)[] seg = new (int min, int max)[size];

            for (int t = 0; t < test; t++)
            {

                int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                for (int i = 0; i < info[0]; i++)
                {

                    nums[i] = i;
                }

                SetArr(seg, nums, 0, info[0] - 1);

                // 민 맥스로 판별하자!
                // 어차피 중복 X
                for (int i = 0; i < info[1]; i++)
                {

                    int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

                    if (temp[0] == 0)
                    {

                        // 바꾼다
                        Swap(nums, temp[1], temp[2]);
                        ChangeArr(seg, 0, info[0] - 1, temp[1], nums[temp[1]]);
                        ChangeArr(seg, 0, info[0] - 1, temp[2], nums[temp[2]]);
                    }
                    else
                    {

                        // 시리즈 있는지 확인하기
                        var chk = GetArr(seg, 0, info[0] - 1, temp[1], temp[2]);

                        if (chk.min == temp[1] && chk.max == temp[2]) sw.WriteLine(Y);
                        else sw.WriteLine(N);
                    }
                }

                sw.Flush();
            }

            sr.Close();
            sw.Close();
        }

        static void Swap(int[] _arr, int _idx1, int _idx2)
        {

            int temp = _arr[_idx1];
            _arr[_idx1] = _arr[_idx2];
            _arr[_idx2] = temp;
        }

        static void SetArr((int min, int max)[] _seg, int[] _nums, int _start, int _end, int _idx = 1)
        {

            if (_start == _end)
            {

                _seg[_idx - 1].min = _start;
                _seg[_idx - 1].max = _end;
                return;
            }

            int mid = (_start + _end) / 2;

            SetArr(_seg, _nums, _start, mid, _idx * 2);
            SetArr(_seg, _nums, mid + 1, _end, _idx * 2 + 1);

            _seg[_idx - 1].min = Math.Min(_seg[_idx * 2 - 1].min, _seg[_idx * 2].min);
            _seg[_idx - 1].max = Math.Max(_seg[_idx * 2 - 1].max, _seg[_idx * 2].max);
        }

        static void ChangeArr((int min, int max)[] _seg, int _start, int _end, int _changeIdx, int _changeValue, int _idx = 1)
        {

            if (_start == _end)
            {

                _seg[_idx - 1].min = _changeValue;
                _seg[_idx - 1].max = _changeValue;
                return;
            }

            int mid = (_start + _end) / 2;

            if (_changeIdx > mid) ChangeArr(_seg, mid + 1, _end, _changeIdx, _changeValue, _idx * 2 + 1);
            else ChangeArr(_seg, _start, mid, _changeIdx, _changeValue, _idx * 2);

            _seg[_idx - 1].min = Math.Min(_seg[_idx * 2 - 1].min, _seg[_idx * 2].min);
            _seg[_idx - 1].max = Math.Max(_seg[_idx * 2 - 1].max, _seg[_idx * 2].max);
        }

        static (int min, int max) GetArr((int min, int max)[] _seg, int _start, int _end, int _chkStart, int _chkEnd, int _idx = 1)
        {

            if (_start >= _chkStart && _end <= _chkEnd) return _seg[_idx - 1];
            else if (_start > _chkEnd || _end < _chkStart) return (100_000 - 1, 0);

            int mid = (_start + _end) / 2;
            var l = GetArr(_seg, _start, mid, _chkStart, _chkEnd, _idx * 2);
            var r = GetArr(_seg, mid + 1, _end, _chkStart, _chkEnd, _idx * 2 + 1);

            return (Math.Min(l.min, r.min), Math.Max(l.max, r.max));
        }
    }


#if other

namespace Baekjoon;

using System;
using System.Text;

public record struct Node(int Min, int Max)
{
    public static readonly Node Default = new(int.MaxValue, int.MinValue);
    public readonly Node Merge(Node other) => new(
        Math.Min(Min, other.Min), Math.Max(Max, other.Max));
}

public class Tree
{
    readonly int[] _seq = new int[100_000];
    readonly Node[] _tree = new Node[1 << 18];
    int _size;

    public void SetElem(int index, int element)
    {
        _seq[index] = element;
        Set(0, _size - 1, 1, index, element);
    }

    public void Init(int size)
    {
        for (int i = 0; i < size; i++)
            _seq[i] = i;
        Init(0, size - 1, 1);
        _size = size;
    }

    private void Init(int start, int end, int node)
    {
        _tree[node] = new(start, end);
        if (start < end)
        {
            var mid = (start + end) / 2;
            Init(start, mid, 2 * node);
            Init(mid + 1, end, 2 * node + 1);
        }
    }

    private Node Set(int start, int end, int node, int index, int value)
    {
        if (index < start || end < index) return _tree[node];
        if (start == end) return _tree[node] = new(value, value);
        var mid = (start + end) / 2;
        var leftNode = Set(start, mid, 2 * node, index, value);
        var rightNode = Set(mid + 1, end, 2 * node + 1, index, value);
        return _tree[node] = leftNode.Merge(rightNode);
    }

    internal int GetElem(int b) => _seq[b];

    internal Node MinMax(int from, int to) => GetNode(0, _size - 1, 1, from, to);

    private Node GetNode(int start, int end, int node, int from, int to)
    {
        if (to < start || end < from) return Node.Default;
        if (from <= start && end <= to) return _tree[node];
        var mid = (start + end) / 2;
        Node left = GetNode(start, mid, 2 * node, from, to),
            right = GetNode(mid + 1, end, 2 * node + 1, from, to);
        return left.Merge(right);
    }
}

public class Program
{
    static Program()
    {
        using var sr = new StreamReader(
            new BufferedStream(Console.OpenStandardInput())
            );

    }

    private static void Main(string[] args)
    {
        using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        var t = ScanInt(sr);
        var sb = new StringBuilder();
        var tree = new Tree();
        while (t-- > 0)
        {
            int nodeCount = ScanInt(sr), changeCount = ScanInt(sr);
            tree.Init(nodeCount);
            while (changeCount-- > 0)
            {
                int oper = ScanInt(sr), a = ScanInt(sr), b = ScanInt(sr);
                if (oper == 0)
                {
                    int oriA = tree.GetElem(a), oriB = tree.GetElem(b);
                    tree.SetElem(a, oriB);
                    tree.SetElem(b, oriA);
                }
                else
                {
                    sb.AppendLine(new Node(a, b) == tree.MinMax(a, b) ? "YES" : "NO");
                }
            }
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
