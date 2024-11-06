using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 6
이름 : 배성훈
내용 : 중앙값
    문제번호 : 1572번

    중앙값을 어떻게 찾지 하고 고민했다;
    해당 방법이 안떠올라 다른 사람 글을 참고했다

    주된 아이디어는 다음과 같다
    입력값의 범위가 0 ~ 65536 이므로 
    해당 범위의 숫자가 부분 수열에 n개 있다는 의미로 세그먼트 트리를 만든다

    예를 들어 만약 부분 수열이 3, 1, 2이면,
    해당 세그먼트 트리는
                    3                        
            1               2   
        0       1       1       1
        |       |       |       |
        0       1       2       3           << 리프노드
    위와 같은 형태가 된다
    맨 위의 3은 0 ~ 3 범위의 값이 부분 수열에 3개 있다는 의미이고
    3 밑의 줄의 1은 0 ~ 1 범위의 값이 부분 수열에 1개 있다는 의미이고
    2는 2 ~ 3 범위의 값이 부분 수열에 2개 있다는 말이다!
    또한 리프노드만 놓고 보면 수들은 오름차순으로 정렬된 상태가 된다!

    그리고 3, 1, 2로 만든 세그먼트 트리 에서 중앙값을 찾아보자
    중앙값은 정렬 했을 때 2번째가 된다

                    3
            1               2
        0       1       1       1

    맨 위 루트 노드에서 시작해서 좌우를 비교하며 좌측을 기준으로 2보다 작은지 확인한다
    좌측 자식 노드의 값이 2보다 크거나 같다면 왼쪽 노드로 가고, 좌측 자식 노드의 값이 2보다 작다면
    오른쪽 노드로 간다 여기서는 오른쪽으로 간다

    그런데 오른쪽으로 이동할 때에는, 
    앞의 1개가 빠지므로 (찾을 idx) - (좌측 자식 노드의 값)으로 찾을 idx를 갱신해줘야한다
    그래서 
                    3
            1              '2'
        0       1       1       1

    2로 이동하는데, 이제 찾을 인덱스는 1이다
    2의 왼쪽 자식 노드의 값 1이 찾을 인덱스 1보다 크거나 같으므로 왼쪽 노드로 간다
    왼쪽 노드 이동에서는 찾을 idx를 갱신하지 않는다
                    3
            1               2 
        0       1      '1'      1

    더 이상 자식이 없으므로 해당 노드가 가리키는 값 2를 출력한다
    3, 1, 2 -> 1, 2, 3이고 이때 2번째 원소는 2이다

    이를 코드로 쓰면 아래와 같다
*/

namespace BaekJoon._46
{
    internal class _46_08
    {

        static void Main8(string[] args)
        {

            int MAX = 65537;
            int log = (int)Math.Ceiling(Math.Log2(MAX)) + 1;
            int size = 1 << log;
            int[] seg = new int[size];

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            int[] nums = new int[info[0]];

            for (int i = 0; i < info[0]; i++)
            {

                nums[i] = int.Parse(sr.ReadLine());
            }

            sr.Close();

            for (int i = 0; i < info[1] - 1; i++)
            {

                ChangeArr(seg, 0, 65536, nums[i], 1);
            }

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            int chk = (info[1] + 1) / 2;
            long result = 0;
            for (int i = info[1] - 1; i < info[0]; i++)
            {

                ChangeArr(seg, 0, 65536, nums[i], 1);
                result += GetArr(seg, 0, 65536, chk);
                ChangeArr(seg, 0, 65536, nums[i - info[1] + 1], -1);
            }

            sw.Write(result);
            sw.Close();
        }

        static void ChangeArr(int[] _seg, int _start, int _end, int _changeIdx, int _addValue, int _idx = 1)
        {

            if (_start == _end)
            {

                // 중복될 수도 있어서 +=연산
                _seg[_idx - 1] += _addValue;
                return;
            }

            int mid = (_start + _end) / 2;

            // 해당되는 방향으로만 재귀함수 호출
            if (_changeIdx > mid) ChangeArr(_seg, mid + 1, _end, _changeIdx, _addValue, _idx * 2 + 1);
            else ChangeArr(_seg, _start, mid, _changeIdx, _addValue, _idx * 2);

            _seg[_idx - 1] = _seg[_idx * 2 - 1] + _seg[_idx * 2];
        }

        static int GetArr(int[] _seg, int _start, int _end, int _chk, int _idx = 1)
        {

            // 리프노드의 값 반환
            if (_start == _end) return _start;

            int l = _seg[_idx * 2 - 1];

            int result;
            int mid = (_start + _end) / 2;
            
            // 왼쪽 노드가 찾는 인덱스보다 작으면 오른쪽 노드로 가야한다
            if (l < _chk) result = GetArr(_seg, mid + 1, _end, _chk - l, _idx * 2 + 1);
            // 왼쪽노드가 찾는 인덱스보다 크거나 같은 경우 왼쪽 노드 탐색
            else result = GetArr(_seg, _start, mid, _chk, _idx * 2);

            // 앞에서 찾은 리프노드를 반환한다
            return result;
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

#nullable disable

public class Seg
{
    private int _leafMask;
    private long[] _tree;

    public Seg(int n)
    {
        _leafMask = (int)BitOperations.RoundUpToPowerOf2((uint)n);
        _tree = new long[2 * _leafMask];
    }

    public void Update(int idx, long val)
    {
        var pos = _leafMask | idx;
        while (pos != 0)
        {
            _tree[pos] += val;
            pos = pos / 2;
        }
    }

    public long Range(int stIncl, int edExcl)
    {
        var q = new Queue<(int pos, int stIncl, int edExcl)>();
        q.Enqueue((1, 0, _leafMask));

        var sum = 0L;

        while (q.TryDequeue(out var s))
        {
            if (s.edExcl <= stIncl || edExcl <= s.stIncl)
                continue;

            if (stIncl <= s.stIncl && s.edExcl <= edExcl)
            {
                sum += _tree[s.pos];
                continue;
            }

            var mid = (s.stIncl + s.edExcl) / 2;

            if (stIncl < mid)
                q.Enqueue((2 * s.pos, s.stIncl, mid));
            if (mid < edExcl)
                q.Enqueue((2 * s.pos + 1, mid, s.edExcl));
        }

        return sum;
    }
}

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var seg = new Seg(65536 + 1);

        var nk = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var n = nk[0];
        var k = nk[1];
        var medianIndex = (1 + k) / 2;

        var arr = new int[n];
        for (var idx = 0; idx < n; idx++)
            arr[idx] = Int32.Parse(sr.ReadLine());

        var min = arr.Min();
        var max = arr.Max();

        var sum = 0L;
        for (var idx = 0; idx < n; idx++)
        {
            if (idx - k >= 0)
                seg.Update(arr[idx - k], -1);

            seg.Update(arr[idx], 1);

            if (idx >= k - 1)
            {
                // find med
                var st = min;
                var ed = 1 + max;

                while (st < ed)
                {
                    var mid = (st + ed) / 2;
                    var elemCount = seg.Range(0, 1 + mid);

                    if (elemCount < medianIndex)
                        st = mid + 1;
                    else
                        ed = mid;
                }

                sum += ed;
            }
        }

        sw.WriteLine(sum);
    }
}
#endif
}
