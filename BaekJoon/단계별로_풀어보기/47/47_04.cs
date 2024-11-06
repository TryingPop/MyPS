using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 8
이름 : 배성훈
내용 : 여우가 정보섬에 올라온 이유
    문제번호 : 17131번

    N^2의 시간대 풀이는 보자마자 떠올랐어도 N log N 방법은 안떠올랐다
    N^2은 각원소에 대해 스택을 이용한 방법이다

    세그먼트 트리를 써야하는데, 떠오르지 않았다
    고민했음에도 아이디어가 안떠올라 다른 사람 아이디어를 봤다

    확인하니 47_04에서 오른쪽에서 왼쪽으로 진행하면서 아래 있는 원소를 파악하는 것처럼
    여기서는 양방향으로 확인하고 좌측, 오른쪽 값을 곱해줘야한다

    왼쪽 탐색에서 오른쪽 값을 넣는 방법을 몰라서 왼쪽 탐색 후 오른쪽 탐색을 또 했다 즉, 두 번 탐색했다
    그리고 오른쪽 탐색에서 왼쪽에 탐색한 값을 꺼내 곱하면서 결과값을 채워넣었다

    주의할건 다음과 같다
    x1 < x2 < x3 && y1 > y2 < y3가 되어야 별로 인식하므로
    왼쪽 탐색이나 오른쪽 탐색 중 적어도 한군데는 y값을 채우는데 x값에 맞춰 조치를 취해줘야한다

    왼쪽 탐색에서는 y를 오름차순 정렬했기에 x가 같고 y가 큰 경우는 발견되지 않는다
    오른쪽 탐색에서는 y가 내림차순이므로 x가 같은데 y가 큰 값의 개수를 인식할 수 있다
    그래서 앞번 x값을 기록하고 x값이 변할 때, 여태까지 저장한 y값을 한 번에 저장하는 아이디어를 사용했다
    y를 수정하는데에는 순서가 상관없어 Stack을 써서 했다

    그리고 결과를 제출하니 이상없이 통과했다
    시간은 400ms이다
*/

namespace BaekJoon._47
{
    internal class _47_04
    {

        static void Main4(string[] args)
        {

            // y 입력값의 범위
            int MAX = 400_000;
            // y값 양수로 보정
            // start end를 써서 이진 탐색(분할쪽)을 하려면 양수가 되게 수정해야한다
            // 그냥 사용하면 무한루프에 빠질 수 있다
            int ADD = 200_000;

            // 세그먼트 트리
            int log = (int)Math.Ceiling(Math.Log2(MAX + 1)) + 1;
            int[] seg = new int[1 << log];

            // 결과 계산용
            int r = 1_000_000_007;

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());
            Pos[] pos = new Pos[len];

            // 좌표 입력 받기
            for (int i = 0; i < len; i++)
            {

                string[] temp = sr.ReadLine().Split(' ');

                pos[i].x = int.Parse(temp[0]);
                pos[i].y = int.Parse(temp[1]) + ADD;
            }

            sr.Close();

            // 정렬
            // CompareTo 메서드에 맞춰 오름차순 정렬해준다
            Array.Sort(pos);

            // 왼쪽 계산값들 저장용
            Stack<int> left = new Stack<int>(len);

            for (int i = 0; i < len; i++)
            {

                int cnt = GetValue(seg, 0, MAX, pos[i].y);
                left.Push(cnt);
                Update(seg, 0, MAX, pos[i].y);
            }

            // 오른쪽 계산을 위해 세그먼트 트리 초기화
            Array.Fill(seg, 0);

            // 결과값 10억 + 7로 나누지만 곱해주는 연산 중에 곱할 때는 int 범위를 아득히 넘어서므로
            // 그냥 long으로 했다
            long result = 0;

            // 오른쪽 탐색에서 x 값이 변할 때 y를 넣어줄 생각이다
            // 그렇지 않으면 역순 탐색에서 y가 내림차순이기에
            // x가 같은데 y가 높은 경우를 세어버려 경우의 수가 많아진다
            int curX = pos[len - 1].x;
            Stack<int> calc = new Stack<int>(len - 1);

            for (int i = len - 1; i>= 0; i--)
            {

                // x값이 변해서 이전에 모아둔 y값들을 이때 넣어준다
                if (pos[i].x != curX)
                {

                    curX = pos[i].x;
                    while (calc.Count > 0)
                    {

                        int y = calc.Pop();
                        Update(seg, 0, MAX, y);
                    }
                }

                // 계산
                long cnt = GetValue(seg, 0, MAX, pos[i].y);
                result += cnt * left.Pop();
                result %= r;
                calc.Push(pos[i].y);
            }

            Console.WriteLine(result);
        }

        static void Update(int[] _seg, int _start, int _end, int _changeIdx, int _idx = 1)
        {

            if (_start == _end)
            {

                _seg[_idx - 1]++;
                return;
            }

            int mid = (_start + _end) / 2;
            if (_changeIdx > mid) Update(_seg, mid + 1, _end, _changeIdx, _idx * 2 + 1);
            else Update(_seg, _start, mid, _changeIdx, _idx * 2);

            _seg[_idx - 1] = _seg[_idx * 2 - 1] + _seg[_idx * 2];
        }

        // chk보다 큰거 다 출력한다!?
        static int GetValue(int[] _seg, int _start, int _end, int _chk, int _idx = 1)
        {

            if (_start > _chk) return _seg[_idx - 1];
            else if (_seg[_idx - 1] == 0 || _end <= _chk) return 0;

            int mid = (_start + _end) / 2;

            int l = 0, r = 0;
            if (mid > _chk) l = GetValue(_seg, _start, mid, _chk, _idx * 2);
            r = GetValue(_seg, mid + 1, _end, _chk,_idx * 2 + 1);

            return l + r;
        }

        struct Pos : IComparable<Pos>
        {

            public int x;
            public int y;

            // Array.Sort의 기준이된다
            public int CompareTo(Pos _other)
            {

                int ret = x - _other.x;
                if (ret == 0) ret = y - _other.y;

                return ret;
            }
        }
    }

#if other
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

class Program
{
    static StreamReader sr = new StreamReader(Console.OpenStandardInput(), Encoding.Default);
    static StreamWriter sw = new StreamWriter(Console.OpenStandardOutput(), Encoding.Default);
    static List<long> xx = new List<long>();
    static List<long> yy = new List<long>();
    static Dictionary<long, long> dicX = new Dictionary<long, long>(),dicY=new Dictionary<long, long>();
    static KeyValuePair<long,long>[] arr;
    static long[] node;
    static long mod = 1000000007;
    static int CMP(KeyValuePair<long,long> a,KeyValuePair<long,long> b)
    {
        if(a.Key.Equals(b.Key))
        {
            return b.Value.CompareTo(a.Value);
        }
        return a.Key.CompareTo(b.Key);
    }
    static long SegmentTreeSum(long s,long e, long l,long r,long parent)
    {
        if(s>r||e<l)
        {
            return 0;
        }
        if(l<=s&&e<=r)
        {
            return node[parent]%mod;
        }
        long mid = (s + e) / 2;
        long left = parent * 2 + 1;
        long right = parent * 2 + 2;
        return (SegmentTreeSum(s, mid, l, r, left)%mod + SegmentTreeSum(mid + 1, e, l, r, right)%mod)%mod;
    }
    static void SegmentTreeUpdate(long s,long e,long idx,long parent)
    {
        if(idx<s||idx>e)
        {
            return;
        }
        if(s.Equals(e))
        {
            node[parent]++;
            node[parent] %= mod;
            return;
        }
        long mid = (s + e) / 2;
        long left = parent * 2 + 1;
        long right = parent * 2 + 2;
        SegmentTreeUpdate(s, mid, idx, left);
        SegmentTreeUpdate(mid+1, e, idx, right);
        node[parent] = (node[left] + node[right])%mod;
    }
    static void Main(string[] args)
    {
        int n = int.Parse(sr.ReadLine());
        arr = new KeyValuePair<long, long>[n];
        node = new long[n * 4];
        for (int i = 0; i < n; i++)
        {
        string[] str = sr.ReadLine().Split();
            long x = long.Parse(str[0]);
            xx.Add(x);
            long y = long.Parse(str[1]);
            yy.Add(y);
            arr[i] = new KeyValuePair<long, long>(x, y);
        }
        xx.Sort();
        yy.Sort();
        int ix = 0,iy=0;
        for (int i = 0; i < n; i++)
        {
            if (!dicX.ContainsKey(xx[i]))
            {
                dicX.Add(xx[i], ix++); 
            }
            if(!dicY.ContainsKey(yy[i]))
            {
                dicY.Add(yy[i], iy++);
            }
        }
        for (int i = 0; i < n; i++)
        {
            arr[i] = new KeyValuePair<long, long>(dicX[arr[i].Key], dicY[arr[i].Value]);
        }
        Array.Sort(arr, CMP);
        List<long>[] list = new List<long>[dicY.Count];
        for (int i = 0; i < n; i++)
        {
            long _y = arr[i].Value;
            if(list[_y]==null)
            {
            list[_y] = new List<long>();
            }
            list[_y].Add(arr[i].Key);
        }
        long answer = 0;
        for (int i = dicY.Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < list[i].Count; j++)
            {
                long currentX = list[i][j];
             long a=   SegmentTreeSum(0, n - 1, 0, currentX - 1, 0);//좌측대각선 갯수
                long b = SegmentTreeSum(0, n - 1, currentX + 1, n - 1, 0);//우측값
                answer += (a * b) % mod;
                answer %= mod;
            }
            for (int j = 0; j < list[i].Count; j++)
            {
                SegmentTreeUpdate(0, n - 1, list[i][j], 0);
            }
        }
        sw.Write(answer);
        sw.Close();
    }
}
#endif
}
