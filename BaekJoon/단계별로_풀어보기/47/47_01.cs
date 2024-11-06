using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 6
이름 : 배성훈
내용 : 선 긋기
    문제번호 : 2170번

    스위핑 알고리즘에 관한 문제이다
    N^2의 시간을 가진 문제를, 정렬 후 nlogn으로 해결하는 방법을 스위핑 알고리즘이라 하는거 같다
    https://lubiksss.github.io/boj/BOJ_Sweeping/

    다차원 배열로하니 시간도 많이 먹고 속도도 1.2초로 느리다
    이를 튜플로 수정시 30% 이상의 성능 향상이 일어났다, 메모리는 2배 이상!
    1212ms -> 876ms
    
    가장 빠른 사람은 ICompareTo를 상속받아서 해결했다
    앞의 트리는 사용되지 않는다;
*/

namespace BaekJoon._47
{
    internal class _47_01
    {
        
        static void Main1(string[] args)
        {
            
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            // int[][] arr = new int[len][];
            (int start, int end)[] arr = new (int start, int end)[len];

            for (int i = 0; i < len; i++)
            {

                // arr[i] = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
                string[] temp = sr.ReadLine().Split(' ');
                arr[i].start = int.Parse(temp[0]);
                arr[i].end = int.Parse(temp[1]);
            }

            sr.Close();

            Array.Sort(arr, (x, y) =>
            {

                // int result = x[0].CompareTo(y[0]);
                // if (result == 0) result = y[1].CompareTo(x[1]);
                int result = x.start.CompareTo(y.start);
                if (result == 0) result = y.end.CompareTo(x.end);

                return result;
            });

            // int start = arr[0][0];
            // int end = arr[0][1];

            int start = arr[0].start;
            int end = arr[0].end;

            int result = end - start;

            for (int i = 1; i < len; i++)
            {

                // if (end <= arr[i][0])
                if (end <= arr[i].start)
                {

                    // start = arr[i][0];
                    // end = arr[i][1];

                    start = arr[i].start;
                    end = arr[i].end;
                    result += end - start;
                }
                // else if (end < arr[i][1])
                else if (end < arr[i].end)
                {

                    // result += arr[i][1] - end;
                    // end = arr[i][1];

                    result += arr[i].end - end;
                    end = arr[i].end;
                }
            }

            Console.WriteLine(result);
        }
    }

#if other
namespace Baekjoon;
public class Tree
{
    readonly int[] _tree = new int[1 << 18];
    readonly int _length;
    public int Count => _tree[1];

    public Tree(int nodeCount)
    {
        _length = nodeCount;
        Init(0, _length - 1, 1);
    }

    private int Init(int start, int end, int node)
    {
        if (start == end)
            return _tree[node] = 1;
        var mid = (start + end) / 2;
        int l = Init(start, mid, 2 * node),
            r = Init(mid + 1, end, 2 * node + 1);
        return _tree[node] = l + r;
    }

    internal int Pop(int x) => Pop(0, _length - 1, 1, x);

    private int Pop(int start, int end, int node, int index)
    {
        _tree[node]--;
        if (start == end)
            return start + 1;
        var mid = (start + end) / 2;
        var lChild = _tree[2 * node];
        if (index < lChild)
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
        int n = ScanSignedInt(sr);
        var input = new Point[n];
        for (int i = 0; i < n; i++)
        {
            int x = ScanSignedInt(sr), y = ScanSignedInt(sr);
            input[i] = new Point(x, y);
        }
        Array.Sort(input, 0, input.Length);
        var last = input[0].End;
        var ret = last - input[0].Start;
        for (int i = 1; i < n; i++)
        {
            var curPoint = input[i];
            int start = curPoint.Start, end = curPoint.End;
            if (end <= last)
                continue;
            if (last <= start)
            {
                ret += end - start;
            }
            else
            {
                ret += end - last;
            }
            last = end;
        }
        Console.Write(ret);
    }

    static int ScanSignedInt(StreamReader sr)
    {
        int c = sr.Read(), n = 0;
        if (c == '-')
            while (!((c = sr.Read()) is ' ' or '\n' or -1))
            {
                if (c == '\r')
                {
                    sr.Read();
                    break;
                }
                n = 10 * n - c + '0';
            }
        else
        {
            n = c - '0';
            while (!((c = sr.Read()) is ' ' or '\n' or -1))
            {
                if (c == '\r')
                {
                    sr.Read();
                    break;
                }
                n = 10 * n + c - '0';
            }
        }
        return n;
    }
}

internal record struct Point(int Start, int End) : IComparable<Point>
{
    public readonly int CompareTo(Point other)
    {
        var ret = Start - other.Start;
        if (ret != 0) return ret;
        return other.End - End;
    }
}
#endif
}
