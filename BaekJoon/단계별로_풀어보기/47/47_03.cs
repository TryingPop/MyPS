using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 7
이름 : 배성훈
내용 : 북서풍
    문제번호 : 5419번

    문제에서 찾을건 다음과 같다
    각 점 A에 대해 고등학교에서 배운 데카르트 좌표평면에서 A를 기준으로 오른쪽 아래 방향에 점이 몇 개인지 세어야한다

    예를들어 A(1, 1),   B (-4, -2), C(5, -1), D(3, 5)이라하면

                                        D(3, 5)
                            A(1, 1)-------------------------
                               |                C(5, -1)
            B(-4, -2)          |

        와같이 점들이 분포하게 될 것이다    
        A를 기준으로 보면, 오른쪽 아래 점은 C 1개만 존재한다!
        이를 A, B, C, D 모든 경우를 세어서 합친게 결과가 된다

        즉, A - 1개, B - 0개, C - 0개, D - 1개
        해당 예시의 경우 총 2개이다

    이를 어떻게 세어야 할 것인가 찾아야한다

    그냥 각각의 점에서 y보다 작은걸 센다면 N^2의 시간이 걸리고
    입력 값이 75,000개에 여러 케이스가 있어 N^2은 시간초과가 나온다
    NlogN의 시간대로 줄어야한다

    x, y의 입력 범위가 -10억 ~ 10억이고, 오른쪽 아래를 어떻게 찾아갈지 감이 전혀 안왔다
    그래서 다른 사람의 풀이 과정을 참고해 코드를 작성했다

    주된 아이디어는 다음과 같다
    오른쪽 아래를 찾는 것이기에 x가 큰 값을 앞으로 오게하고, x가 같은 경우 y가 작은 값을 앞으로 오게 해야한다
    (주어진 점들을 양 끝점로 간선을 만들었을 때 |, \, - 형태가 나오는 간선을 찾는것과 일치하기에 꼭 해당 방법으로만 풀리는 것은 아니다!)
    이후 y 값에 따라 몇 번째로 큰가를 나타내는 세그먼트 트리를 만들고 카운트하는 기법이었다

    예를들어 A(1, 1),   B (-4, -2), C(5, -1), D(3, 5)이라하면

                                        D(3, 5)
                            A(1, 1)
                                                C(5, -1)
            B(-4, -2)          

        에서는 C -> D -> A -> B 순서가 되게 정렬하고 
        C -> D -> A -> B 순으로 탐색을 진행한다

        C부터 시작한다 y가 -1보다 작은 값들을 센다 초기이므로 0개이고,
        이제 -1을 dp에 저장한다 그러면 dp에는 -1이 하나 있다

        이후 D의 y인 5보다 작은 값이 있는지 dp에서 조사한다
        그러면, 앞의 -1 <= 5인 -1이 한개 존재한다 그러면 결과에 1개를 추가한다
        그리고, 5를 dp에 넣는다 그러면 dp에는 -1, 5가 있다
        
        이후에 A의 y인 1보다 작은 값이 dp에 몇 개 있는지 조사한다
        그러면 5 > 1이므로 5는 안되고, -1 <= 1인 -1이 1개 존재한다 그러면 결과에 1개를 추가한다
        그러면 결과는 1 + 1 = 2가된다
        그리고, 1을 dp에 넣는다 그러면 에dp에는 -1, 5, 1이 있다

        이제 마지막으로 B의 y인 -2보다 작은 값이 몇 개 있는지 dp에서 센다
        0개이다 그리고 -2를 dp에 넣는다, dp에는 -1, 5, 1, -2가 들어간다
    이렇게 찾아간다

    여기서 dp를 세그먼트 트리로 만들기 위해 스케일링? y값을 좌표 압축을 실행한다
    예를들어 A(1, 1),   B (-4, -2), C(5, -1), D(3, 5)이라하면 y의 범위는 -2 ~ 5이다
        이를 제일 작은 것을 1으로 하고 1씩 늘려나간다(제일 작은 것을 원하는 숫자로 시작해도 된다, 편의상 1로 했다)
        그러면 A(1, 3), B(-4, 1), C(5, 2), D(3, 4)로 된다 -2 ~ 5가 1 ~ 4로 줄어들었다
        이는 y로 오름차순 정렬해서 값을 부여하면 된다
        
    그리고 세그먼트 트리로 만들면 -10억에서 10억 범위는 입력 값의 범위가 1 ~ 75_000으로 줄어든다

    또한 여러 케이스를 다루어서 필요한 길이만큼만 정렬해야 하기에 따로 정렬하는 메서드를 작성했다
    여기서는 힙 정렬을 이용했다

    820ms로 통과했다
    다만 Main에 end 변수의 위치를 잘못 둬서 인덱스 에러 2번과, struct로 컴파일에러 2번 일으켰다
*/

namespace BaekJoon._47
{
    internal class _47_03
    {

        static void Main3(string[] args)
        {

            int MAX = 75_000;
            int log = (int)Math.Ceiling(Math.Log2(MAX)) + 1;
            int[] seg = new int[1 << log];

            Pos[] pos = new Pos[MAX];

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = int.Parse(sr.ReadLine());

    
            for (int t = 0; t < test; t++)
            {

                int len = int.Parse(sr.ReadLine());

                for (int i = 0; i < len; i++)
                {

                    string[] temp = sr.ReadLine().Split(' ');

                    pos[i].x = int.Parse(temp[0]);
                    pos[i].y = int.Parse(temp[1]);
                }

                int end = 0;

                {

                    // 가질 수 있는 최소값보다 1작게 했다
                    // end = 1부터 한다면
                    // y 오름차순 정렬 후 pos[0].y로 하면 된다
                    int cur = -1_000_000_001;

                    // 이번 경우에 입력된 0 ~ len -1 번 인덱스까지만
                    // y 오름차순 정렬
                    HeapSort1(pos, len);

                    // 좌표 압축
                    for (int i = 0; i < len; i++)
                    {

                        // 다른 경우 즉, 증가한 경우다
                        if (cur != pos[i].y)
                        {

                            // end 추가하고 다음 값을 비교하기 위해 현재 값을 저장
                            end++;
                            cur = pos[i].y;
                        }
                        
                        // 해당 순서 부여
                        pos[i].y = end;
                    }

                    // x가 큰 값이 앞으로 오게 했다
                    // x가 같다면 y가 작은게 앞에 오게 했다
                    // 마찬가지로 이번 케이스에 입력된 0 ~ len - 1번 인덱스까지만 정렬한다
                    HeapSort2(pos, len);

                    // 세그먼트 트리 초기화
                    Init(seg, 1, end);
                }

                // 결과의 최대가 75_000 * 75_000 / 2 - 75_000 > int.MaxValue 이므로 long으로 잡았다
                long result = 0;

                for (int i = 0; i < len; i++)
                {

                    // 작은 점들을 세어간다
                    result += GetArr(seg, 1, end, pos[i].y);
                    Update(seg, 1, end, pos[i].y);
                }

                sw.WriteLine(result);
            }
            sr.Close();
            sw.Close();

        }

        struct Pos : IComparable<Pos>
        {

            // 좌표 자료구조 x, y
            public int x;
            public int y;

            // 비교 방법
            public int CompareTo(Pos other)
            {

                // return => ret
                // x 내림차순
                int ret = other.x - x;
                // 오름 차순
                if (ret == 0) ret = y - other.y;

                return ret;
            }
        }

        static void Init(int[] _seg, int _start, int _end, int _idx = 1)
        {

            _seg[_idx - 1] = 0;
            if(_start == _end) return;

            int mid = (_start + _end) / 2;
            Init(_seg, _start, mid, _idx * 2);
            Init(_seg, mid + 1, _end, _idx * 2 + 1);
        }

        static void Update(int[] _seg, int _start, int _end, int _changeIdx, int _idx = 1)
        {

            if (_start == _end)
            {

                // add + 1
                _seg[_idx - 1] += 1;
                return;
            }

            int mid = (_start + _end) / 2;
            if (_changeIdx > mid) Update(_seg, mid + 1, _end, _changeIdx, _idx * 2 + 1);
            else Update(_seg, _start, mid, _changeIdx, _idx * 2);

            _seg[_idx - 1] = _seg[_idx * 2 - 1] + _seg[_idx * 2];
        }

        // chk보다 작은 수 다 가져온다!
        static int GetArr(int[] _seg, int _start, int _end, int _chk, int _idx = 1)
        {

            if (_end <= _chk) return _seg[_idx - 1];
            else if (_start > _chk) return 0;

            int mid = (_start + _end) / 2;
            int l = GetArr(_seg, _start, mid, _chk, _idx * 2);
            int r = GetArr(_seg, mid + 1, _end, _chk, _idx * 2 + 1);

            return l + r;
        }

        static void HeapSort1(Pos[] _pos, int _len)
        {

            ///
            /// 0 ~ len - 1번까지 y 오름차순 정렬해준다!
            ///

            for (int i = 1; i < _len; i++)
            {

                int cur = i;

                while (cur > 0)
                {

                    int p = (cur - 1) / 2;

                    // y가 큰 것을 부모로 보낼 것이다
                    // 즉, y가 큰 것을 맨 뒤로 보낼 생각!
                    if (_pos[cur].y > _pos[p].y)
                    {

                        Swap(_pos, cur, p);
                        cur = p;
                    }
                    else break;
                }
            }

            Swap(_pos, 0, _len - 1);

            for (int i = _len - 2; i >= 1; i--)
            {

                int cur = 0;
                while (true)
                {

                    int left = cur * 2 + 1;
                    int right = cur * 2 + 2;

                    if (right <= i && _pos[left].y < _pos[right].y && _pos[cur].y < _pos[right].y)
                    {

                        Swap(_pos, cur, right);
                        cur = right;
                    }
                    else if (left <= i && _pos[cur].y < _pos[left].y)
                    {

                        Swap(_pos, cur, left);
                        cur = left;
                    }
                    else break;
                }

                Swap(_pos, 0, i);
            }
        }
        
        static void HeapSort2(Pos[] _pos, int _len)
        {

            ///
            /// x 내림차순을 최우선으로 하고
            /// x가 같은 경우 y 오름차순
            /// 

            for (int i = 1; i < _len; i++)
            {

                int cur = i;

                while (cur > 0)
                {

                    int p = (cur - 1) / 2;

                    // x가 작고, x가 같은 경우 y가 큰 것을 뒤로 보낼 생각이다
                    if (_pos[p].CompareTo(_pos[cur]) < 0)
                    {

                        Swap(_pos, cur, p);
                        cur = p;
                    }
                    else break;
                }
            }

            Swap(_pos, 0, _len - 1);

            for (int i = _len - 2; i >= 1; i--)
            {

                int cur = 0;
                while (true)
                {

                    int left = cur * 2 + 1;
                    int right = cur * 2 + 2;

                    // left와 right비교
                    // right가 작은 경우 !
                    // right.x < left.x
                    if (right <= i && _pos[left].CompareTo(_pos[right]) < 0 && _pos[cur].CompareTo(_pos[right]) < 0)
                    {

                        Swap(_pos, cur, right);
                        cur = right;
                    }
                    else if (left <= i && _pos[cur].CompareTo(_pos[left]) < 0)
                    {

                        Swap(_pos, cur, left);
                        cur = left;
                    }
                    else break;
                }

                Swap(_pos, 0, i);
            }
        }

        static void Swap(Pos[] _pos, int _idx1, int _idx2)
        {

            var temp = _pos[_idx1];
            _pos[_idx1] = _pos[_idx2];
            _pos[_idx2] = temp;
        }
    }

#if other

namespace Baekjoon;
using System.Text;

public class Tree
{
    readonly uint[] _tree;
    int _count;

    public Tree(int max)
    {
        _tree = new uint[max];
    }

    internal void Init(ReadOnlySpan<uint> span)
    {
        _count = span.Length;
        Init(0, _count - 1, 1, span);
    }

    private uint Init(int start, int end, int node, ReadOnlySpan<uint> array)
    {
        _tree[node] = 0;
        if (start == end) return _tree[node] = array[start];
        var mid = (start + end) / 2;
        return _tree[node] = Init(start, mid, 2 * node, array) + Init(mid + 1, end, 2 * node + 1, array);
    }

    internal uint CountLessThanOrEquals(int curKey)
    {
        return Find(0, _count - 1, 1, curKey);
    }

    private uint Find(int start, int end, int node, int key)
    {
        if (_tree[node] == 0) return 0;
        if (end <= key)
            return _tree[node];
        var mid = (start + end) / 2;
        var ret = Find(start, mid, 2 * node, key);
        if (mid < key)
            ret += Find(mid + 1, end, 2 * node + 1, key);
        return ret;
    }

    internal void Remove(int curKey)
    {
        Remove(0, _count - 1, 1, curKey);
    }

    private void Remove(int start, int end, int node, int value)
    {
        _tree[node]--;
        if (start < end)
        {
            int mid = (start + end) / 2;
            if (value <= mid) Remove(start, mid, 2 * node, value);
            else Remove(mid + 1, end, 2 * node + 1, value);
        }
    }
}

public class Program
{
    const int Max = 75000;
    static readonly int[] _xs = new int[Max];
    static readonly Point[] _islands = new Point[Max];
    static readonly int[] _ys = new int[Max];
    static readonly uint[] _yCountByValue = new uint[Max];
    static readonly int[] _sortedY = new int[Max];
    static readonly Dictionary<int, int> _yDict = new(Max);
    static int _islandNum;
    static readonly Tree _tree = new(1 << 18);

    private static void Main(string[] args)
    {
        using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        var sb = new StringBuilder();
        int testCount = ScanSignedInt(sr);
        while (testCount-- > 0)
        {
            _islandNum = ScanSignedInt(sr);
            for (int i = 0; i < _islandNum; i++)
            {
                int x = ScanSignedInt(sr), y = ScanSignedInt(sr);
                _islands[i] = new(x, y);
                _xs[i] = x;
                _ys[i] = y;
            }
            var answer = GetAnswer();
            sb.Append(answer).Append('\n');
            _yDict.Clear();
        }
        Console.Write(sb);
    }

    static uint GetAnswer()
    {
        Array.Copy(_ys, _sortedY, _islandNum);
        Array.Sort(_sortedY, 0, _islandNum);
        var value = 0;
        for (int i = 0; i < _islandNum; i++)
        {
            var key = _sortedY[i];
            if (_yDict.TryAdd(key, value))
                value++;
        }

        Array.Sort(_islands, _ys, 0, _islandNum);
        for (int i = 0; i < _islandNum; i++)
        {
            var altValue = _yDict[_ys[i]];
            _ys[i] = altValue;
        }
        Array.Fill(_yCountByValue, 0u, 0, _islandNum);
        for (int i = 0; i < _islandNum; i++)
        {
            _yCountByValue[_ys[i]]++;
        }
        _tree.Init(new(_yCountByValue, 0, _yDict.Count));
        uint ret = 0;
        for (int i = 0; i < _islandNum; i++)
        {
            var curKey = _ys[i];
            _tree.Remove(curKey);
            ret += _tree.CountLessThanOrEquals(curKey);
        }
        return ret;
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

internal record struct Point(int X, int Y) : IComparable<Point>
{
    public readonly int CompareTo(Point other)
    {
        var ret = X - other.X;
        if (ret != 0) return ret;
        return other.Y - Y;
    }
}
#endif
}
