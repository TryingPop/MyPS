using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 6
이름 : 배성훈
내용 : 요세푸스 문제 2
    문제번호 : 1168번

    세그먼트 트리로 i번째로 큰 값을 빠르게 찾게 했다
    해당 세그먼트 아이디어는 46_07 ~ 46_09와 같다

    그리고 i번째를 계속해서 찾아갔다
*/


namespace BaekJoon._46
{
    internal class _46_10
    {

        static void Main10(string[] args)
        {

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int[] info = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int log = (int)Math.Ceiling(Math.Log2(info[0])) + 1;
            int[] seg = new int[1 << log];
            Init(seg, 1, info[0]);

            int next = 0;
            sw.Write('<');
            for (int i = info[0]; i > 0; i--)
            {

                // 7 3
                // 3 -> 6 -> 2 -> 7 -> 5 -> 1 -> 4
                // 순으로 출력되어야한다
                // 범위 벗어나면 다시 1번부터
                next = (next + info[1]) % i;
                // 만약 0인경우는 끝자리로 맞춘다!
                next = next == 0 ? i : next;
                sw.Write(GetArr(seg, 1, info[0], next));
                if (i != 1)
                {

                    sw.Write(", ");
                }
                // 시작 지점 카운팅하기 위해 한칸 앞으로 땡긴다
                next--;
            }
            sw.Write('>');

            sw.Close();
        }

        static void Init(int[] _seg, int _start, int _end, int _idx = 1)
        {

            if (_start == _end)
            {
                
                // 1로 초기화
                _seg[_idx - 1] = 1;
                return;
            }
            int mid = (_start + _end) / 2;

            // Update는 기존에 특정 idx만 찾아갔는데, 여기 시작은 모든 idx에 1로 초기화해준다!
            Init(_seg, _start, mid, _idx * 2);
            Init(_seg, mid + 1, _end, _idx * 2 + 1);

            _seg[_idx - 1] = _seg[_idx * 2 - 1] + _seg[_idx * 2];
        }

        static int GetArr(int[] _seg, int _start, int _end, int _getIdx, int _idx = 1)
        {

            // 해당 노드에 탐색하면서 빼는 역할을 동시에 진행!
            _seg[_idx - 1] -= 1;

            if (_start == _end) return _start;

            int mid = (_start + _end) / 2;
            int l = _seg[_idx * 2 - 1];
            int result;

            if (_getIdx > l) result = GetArr(_seg, mid + 1, _end, _getIdx - l, _idx * 2 + 1);
            else result = GetArr(_seg, _start, mid, _getIdx, _idx * 2);

            return result;
        }
    }

#if other
namespace Baekjoon;
using System.Text;

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
        var split = Console.ReadLine()!.Split();
        int nodeCount = int.Parse(split[0]), term = int.Parse(split[1]);
        var tree = new Tree(nodeCount);
        int index = 0;
        using var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        var sb = new StringBuilder();
        sb.Append('<');
        for (int i = 0; i < nodeCount; i++)
        {
            index += term - 1;
            index %= tree.Count;
            var item = tree.Pop(index);
            sb.Append(item);
            sb.Append(", ");
        }
        sb.Length--;
        sb[^1] = '>';
        sw.Write(sb);
    }
}

#endif
}
