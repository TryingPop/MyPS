using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 4
이름 : 배성훈
내용 : 구간 합 구하기
    문제번호 : 2042번

    세그먼트 트리

    오버플로우 에러가 자꾸 뜬다;
    처음에는 스택 오버플로우인줄 알았는데, 
    곰곰히 생각해보니 자료 변형에서 일어나는 에러였다;

    입력 범위가 -2 ^ 63 ~ 2^63 - 1인데, 순간 정수형인 줄 알았다
    그래서 temp부분을 int형으로 잡고 통으로 int.Parse... 해버렸다;
    추후 string[] 으로 temp를 변형하고 특정 부분만 long.Parse로 바꿨다
    
    그러니 이상없이 수정되었다

    풀이 아이디어는 기존 배열을 세그먼트 트리로 변형한 것이다
    세그먼트 트리는 자식이 많아야 2명인 트리이다
    그리고 저장하는 값은 자식이 없는 노드 즉, 리프에 기존 배열의 값들을 저장한다
    그리고 부모노드는 두 자식노드의 합을 저장한다

    그래서 기존 배열이 변형된다면,
    해당 노드와 연관된 부모, 자식 노드들이 모두 변형되어야 한다
    
        예를들어 1, 2, 3, 4, 5가 잇을때 이를 세그먼트 트리로 만들면 다음과 같다
                        15   ( 3 + 12 )
                   /         \
                3  (1 + 2)      12  ( 3 + 9 )
              /   \           /    \
           '1'     '2'      '3'     9  ( 4 + 5 )
                                   / \
                                 '4' '5'

        여기서 3번째 3의 값이 6으로 변형된다면
                        18   ( 3 + 15 )
                   /         \
                3  (1 + 2)      15  ( 6 + 9 )
              /   \           /    \
            1       2       '6'     9  ( 4 + 5 )
                                   / \
                                  4   5 

        다음과 같이 변형되어야 한다

    그리고 세그먼트 트리에는 기존 배열의 시작인덱스와 끝 인덱스의 저장정보도 있어야하는데,
        위의 예제 변형된 세그먼트 트리에서 
        18에는 1 ~ 5번째 원소들의 합
        3에는 1 ~ 2번째 원소들의 합
        12에는 3 ~ 5번째 원소들의 합
        ... 정보가 담겨져 있어야한다!

    이는 저장 로직을 이용하면 세그먼트 트리의 배열 인덱스 정보로 찾아낼 수 있다
        그래서 위 예제 중 변형된 세그먼트 트리 2 ~ 5의 부분합을 구한다면,
            2 + 15 = 17
        적은 수에서는 차이가 적으나, 많아야 2 * 최대깊이 만큼의 연산만 하면 원하는 부분 구간합을 모두 구할 수 있다
*/

namespace BaekJoon._46
{
    internal class _46_01
    {

        static void Main1(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            // 1번 부터 시작
            long[] nums = new long[info[0] + 1];
            // info[0] == 1인 경우 - inf 나온다
            int log = info[0] != 1 ? (int)Math.Log2(info[0] - 1) + 2 : 1;
            int size = 1 << log;
            long[] seg = new long[size];

            for (int i = 1; i < info[0] + 1; i++)
            {

                nums[i] = long.Parse(sr.ReadLine());
            }

            SetArr(seg, nums, 1, info[0], 1);

            int len = info[1] + info[2];

            for (int i = 0; i < len; i++)
            {

                // 해당 코드로 해서 Overflow를 봤다
                // int[] temp = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
                string[] temp = sr.ReadLine().Split(' ');

                int temp0 = int.Parse(temp[0]);
                int temp1 = int.Parse(temp[1]);
                
                if (temp0 == 1) 
                {

                    long change = long.Parse(temp[2]);
                    long diff = change;
                    diff -= nums[temp1];
                    nums[temp1] = change;
                    ChangeArr(seg, 1, info[0], diff, temp1);
                    continue;
                }

                int temp2 = int.Parse(temp[2]);
                long result = Sum(seg, 1, info[0], temp1, temp2);
                sw.WriteLine(result);
            }

            sw.Close();
            sr.Close();
        }

        static void SetArr(long[] _seg, long[] _nums, int _start, int _end, int _idx = 1)
        {

            // 맨 밑에 노드에 기존 배열 정보 저장
            if (_start == _end)
            {

                _seg[_idx - 1] = _nums[_start];
                return;
            }
            
            // 분할 -> 리프까지 간다
            int mid = (_start + _end) / 2;
            SetArr(_seg, _nums, _start, mid, 2 * _idx);
            SetArr(_seg, _nums, mid + 1, _end, 2 * _idx + 1);

            // 정복 -> 자식 노드들의 합을 현재 노드로
            _seg[_idx - 1] = _seg[2 * _idx - 1] + _seg[2 * _idx];
        }

        static void ChangeArr(long[] _seg, int _start, int _end, long _diff, int _changeIdx, int _idx = 1)
        {

            // if (_changeIdx < _start || _changeIdx > _end) return;
            // 값 변경에 변화 받는 노드
            _seg[_idx - 1] += _diff;

            if (_start == _end) return;
            int mid = (_start + _end) / 2;

            // ChangeArr(_seg, _start, mid, _diff, _changeIdx, 2 * _idx);
            // ChangeArr(_seg, mid + 1, _end, _diff, _changeIdx, 2 * _idx + 1);
            // 양방향이 아닌 필요한 노드만! 재귀
            if (_changeIdx <= mid) ChangeArr(_seg, _start, mid, _diff, _changeIdx, 2 * _idx);
            else ChangeArr(_seg, mid + 1, _end, _diff, _changeIdx, 2 * _idx + 1);
        }

        // 부분 구간 합 구하기
        static long Sum(long[] _seg, int _start, int _end, int _chkStart, int _chkEnd, int _idx = 1)
        {

            // 확인하는 범위에 포함되면 해당 노드 값 바로 반환하고 더 탐색안한다
            if (_start >= _chkStart && _end <= _chkEnd) return _seg[_idx - 1];
            // 겹치는 구간이 없는 경우 바로 탈출!
            else if (_chkEnd < _start || _end < _chkStart) return 0;

            // 겹치는 구간이 있고 포함이 안된 경우면 포함이 될때까지 반으로 짜른다
            int mid = (_start + _end) / 2;
            long l = Sum(_seg, _start, mid, _chkStart, _chkEnd, 2 * _idx);
            long r = Sum(_seg, mid + 1, _end, _chkStart, _chkEnd, 2 * _idx + 1);
            return l + r;
        }
    }

#if other1
var reader = new Reader();

int n = reader.NextInt();
int m = reader.NextInt() + reader.NextInt();

var array = new long[n];
for (int i = 0; i < n; i++)
    array[i] = reader.NextLong();

var decomposed = new SqrtDecomposition(array);
using (var w = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
while (m-- > 0)
{
    if (reader.NextInt() == 1)
        decomposed.Update(reader.NextInt() - 1, reader.NextLong());
    else
        w.WriteLine(decomposed.Query(reader.NextInt() - 1, reader.NextInt() - 1));
}

class SqrtDecomposition
{
    int blockSize;
    long[] array;
    long[] block;

    public SqrtDecomposition(long[] array)
    {
        this.blockSize = (int)Math.Sqrt(array.Length);
        this.array = array;
        this.block = new long[(int)Math.Ceiling((double)array.Length / this.blockSize)];

        int blockIndex = -1;
        for (int i = 0; i < array.Length; i++)
        {
            if (i % this.blockSize == 0)
                blockIndex++;
            
            this.block[blockIndex] += array[i];
        }
    }

    public void Update(int index, long value)
    {
        array[index] = value;

        int group = index / blockSize;
        int start = group * blockSize;
        int end = Math.Min(array.Length, start + blockSize);
        block[group] = 0;
        for (int i = start; i < end; i++)
            block[group] += array[i];
    }

    public long Query(int left, int right)
    {
        int groupL = left / blockSize;
        int groupR = right / blockSize;

        long sum = 0;
        if (groupL == groupR)
        {
            for (int i = left; i <= right; i++)
                sum += array[i];
        }
        else
        {
            // left - end of groupL
            int groupLEnd = groupL * blockSize + blockSize;
            for (int i = left; i < groupLEnd; i++)
                sum += array[i];

            // Groups between groupL and groupR
            for (int i = groupL + 1; i < groupR; i++)
                sum += block[i];

            // start of groupR - right
            int groupRStart = groupR * blockSize;
            for (int i = groupRStart; i <= right; i++)
                sum += array[i];
        }

        return sum;
    }
}

class Reader
{
    StreamReader reader;

    public Reader()
    {
        BufferedStream stream = new(Console.OpenStandardInput());
        reader = new(stream);
    }

    public int NextInt()
    {
        bool negative = false;
        bool reading = false;

        int value = 0;
        while (true)
        {
            int c = reader.Read();

            if (reading == false && c == '-')
            {
                negative = true;
                reading = true;
                continue;
            }

            if ('0' <= c && c <= '9')
            {
                value = value * 10 + (c - '0');
                reading = true;
                continue;
            }

            if (reading == true)
                break;
        }

        return negative ? -value : value;
    }

    public long NextLong()
    {
        bool negative = false;
        bool reading = false;

        long value = 0;
        while (true)
        {
            int c = reader.Read();

            if (reading == false && c == '-')
            {
                negative = true;
                reading = true;
                continue;
            }

            if ('0' <= c && c <= '9')
            {
                value = value * 10 + (c - '0');
                reading = true;
                continue;
            }

            if (reading == true)
                break;
        }

        return negative ? -value : value;
    }
}
#elif other2
using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
int n = ScanInt(), m = ScanInt(), k = ScanInt();
var nums = new long[n];
for (int i = 0; i < n; i++)
    nums[i] = ScanLong();

const int TreeMax = 1_048_576 * 2;

var tree = new long[TreeMax];
Init(1, n, 1);

using var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
for (int i = 0; i < m + k; i++)
{
    var isUpdateOperation = sr.Read() == '1';
    sr.Read();
    var b = ScanInt();
    long c = ScanLong();
    if (isUpdateOperation)
    {
        Update(1, n, 1, b, c - Sum(1, n, 1, b, b));
    }
    else
    {
        var sum = Sum(1, n, 1, b, (int)c);
        sw.WriteLine(sum);
    }
}

long Init(int start, int end, int node)
{
    long ret;
    if (start == end) ret = nums[start - 1];
    else
    {
        var mid = (start + end) / 2;
        ret =
            Init(start, mid, node * 2) +
            Init(mid + 1, end, node * 2 + 1);
    }
    return tree[node] = ret;
}

long Sum(int start, int end, int node, int left, int right)
{
    long ret;
    if (left > end || right < start)
        ret = 0;
    else if (left <= start && end <= right)
        ret = tree[node];
    else
    {
        var mid = (start + end) / 2;
        ret = Sum(start, mid, node * 2, left, right) + Sum(mid + 1, end, node * 2 + 1, left, right);
    }
    return ret;
}

void Update(int start, int end, int node, int index, long diff)
{
    if (index < start || index > end) return;

    tree[node] += diff;
    if (start == end) return;
    var mid = (start + end) / 2;
    Update(start, mid, node * 2, index, diff);
    Update(mid + 1, end, node * 2 + 1, index, diff);
}

int ScanInt()
{
    int c = sr.Read(), n = 0;
    if (c == '-')
        while (!((c = sr.Read()) is ' ' or '\n'))
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            n = 10 * n - (c - '0');
        }
    else
    {
        n = c - '0';
        while (!((c = sr.Read()) is ' ' or '\n'))
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            n = 10 * n + (c - '0');
        }
    }
    return n;
}

long ScanLong()
{
    int c = sr.Read(); long n = 0;
    if (c == '-')
        while (!((c = sr.Read()) is ' ' or '\n'))
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            n = 10 * n - (c - '0');
        }
    else
    {
        n = c - '0';
        while (!((c = sr.Read()) is ' ' or '\n'))
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            n = 10 * n + (c - '0');
        }
    }
    return n;
}
#elif other3
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static long[] num;
        static long[] tree = new long[6000001];
        public static void Main(string[] args)
        {
            StreamReader input = new StreamReader(
                new BufferedStream(Console.OpenStandardInput()));
            StreamWriter output = new StreamWriter(
                new BufferedStream(Console.OpenStandardOutput()));
            StringBuilder sb = new StringBuilder();
            int[] arr = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
            int n = arr[0]; int m = arr[1]; int k = arr[2];
            num = new long[n];
            for (int i = 0; i < n; i++)
            {
                num[i] = long.Parse(input.ReadLine());
            }
            init(0, n-1, 1);
            for(int i = 0; i < m+k; i++)
            {
                string[] temp = input.ReadLine().Split(' ');
                int b = int.Parse(temp[1]);
                long c = long.Parse(temp[2]);
                if (temp[0] == "1")
                {
                    update(0, n-1, 1, b-1, c - num[b-1]);
                    num[b - 1] = c;
                }
                else
                {
                    sb.Append($"{sum(0, n-1, 1, b-1, (int)c-1)}\n");
                }
            }
            output.Write(sb);

            input.Close();
            output.Close();
        }
        static long init(int start,int end,int index)
        {
            if(start == end)
            {
                tree[index] = num[start];
                return tree[index];
            }
            int mid = (start + end) / 2;
            tree[index] = init(start, mid, index * 2) + init(mid + 1, end, index * 2 + 1);
            return tree[index];
        }
        static long sum(int start,int end,int index,int left,int right)
        {
            if (end < left || start > right) return 0;
            if (left <= start && end <= right) return tree[index];
            int mid = (start + end) / 2;
            return sum(start, mid, index * 2, left, right) + sum(mid + 1, end, index * 2 + 1, left, right);
        }
        static void update(int start,int end,int index,int where,long value)
        {
            if (where < start || where > end) return;
            tree[index] += value;
            if (start == end) return;
            int mid = (start + end) / 2;
            update(start,mid,index * 2, where, value);
            update(mid + 1, end, index * 2 + 1, where, value);
        }
    }
}
#endif
}
