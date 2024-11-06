using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 7
이름 : 성훈
내용 : 스위치
    문제번호 : 1395번

    느리게 갱신되는 세그먼트 트리 문제다
    스위치 전원바꾸기는 XOR 연산을 통해 해결했다
*/

namespace BaekJoon._53
{
    internal class _53_05
    {

        static void Main5(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            (int n, int lazy)[] seg;
            int n;

            Solve();
            void Solve()
            {

                Init();

                int len = ReadInt();

                for (int i = 0; i < len; i++)
                {

                    int op = ReadInt();
                    int f = ReadInt();
                    int b = ReadInt();

                    if (op == 0)
                    {

                        Update(1, n, f, b);
                    }
                    else
                    {

                        int ret = GetVal(1, n, f, b);
                        sw.Write($"{ret}\n");
                    }
                }

                sr.Close();
                sw.Close();
            }

            void LazyUpdate(int _s, int _e, int _idx)
            {

                int lazy = seg[_idx].lazy;
                if (lazy == 0) return;
                seg[_idx].lazy = 0;
                seg[_idx].n = (_e - _s + 1) - seg[_idx].n;

                if (_s == _e) return;
                seg[_idx * 2 + 1].lazy ^= lazy;
                seg[_idx * 2 + 2].lazy ^= lazy;
            }

            int GetVal(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                LazyUpdate(_s, _e, _idx);

                if (_chkS <= _s && _e <= _chkE) return seg[_idx].n;

                if (_e < _chkS || _chkE < _s) return 0;

                int mid = (_s + _e) >> 1;

                return GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1)
                    + GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2);
            }

            void Update(int _s , int _e, int _chkS, int _chkE, int _idx = 0)
            {

                LazyUpdate(_s, _e, _idx);

                if (_chkS <= _s && _e <= _chkE)
                {

                    seg[_idx].n = (_e - _s + 1) - seg[_idx].n;
                    if (_s != _e)
                    {

                        seg[_idx * 2 + 1].lazy ^= 1;
                        seg[_idx * 2 + 2].lazy ^= 1;
                    }

                    return;
                }

                if (_e < _chkS || _chkE < _s) return;

                int mid = (_s + _e) >> 1;

                Update(_s, mid, _chkS, _chkE, _idx * 2 + 1);
                Update(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2);

                seg[_idx].n = seg[_idx * 2 + 1].n + seg[_idx * 2 + 2].n;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = ReadInt();

                int log = (int)Math.Ceiling(Math.Log2(n)) + 1;
                seg = new (int n, int lazy)[1 << log];
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
var reader = new Reader();
var (n, m) = (reader.NextInt(), reader.NextInt());

var st = new LazySegmentTree(n);

using (var writer = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
while (m-- > 0)
{
    var (o, s, t) = (reader.NextInt(), reader.NextInt(), reader.NextInt());
    if (o == 0)
        st.UpdateRange(s - 1, t - 1);
    else
    {
        writer.WriteLine(st.Query(s - 1, t - 1));
    }
}

class LazySegmentTree
{
    int _count;
    int _size;
    int _sizeHalf;
    int[] _tree;
    int[] _lazy;
    int[] _sectionSize;

    public LazySegmentTree(int count)
    {
        int height = (int)Math.Ceiling(Math.Log2(count)) + 1;

        _count = count;
        _size = 1 << height;
        _sizeHalf = _size >> 1;
        _tree = new int[_size];
        _lazy = new int[_size];
        _sectionSize = new int[_size];

        CacheSectionSize(1, 0, _count - 1);
    }

    private void CacheSectionSize(int node, int start, int end)
    {
        if (start == end)
        {
            _sectionSize[node] = 1;
            return;
        }

        int mid = (start + end) / 2;
        CacheSectionSize(node << 1, start, mid);
        CacheSectionSize(node << 1 | 1, mid + 1, end);
        _sectionSize[node] = _sectionSize[node << 1] + _sectionSize[node << 1 | 1];
    }

    private void DoLazyUpdate(int node, int start, int end)
    {
        if (_lazy[node] == 0)
            return;

        _tree[node] = _lazy[node] % 2 == 0 ? _tree[node] : (_sectionSize[node] - _tree[node]);
        if (start != end) // Not a leaf node
        {
            _lazy[node << 1] += _lazy[node];
            _lazy[node << 1 | 1] += _lazy[node];
        }

        _lazy[node] = 0;
    }

    public int Query(int left, int right) => DoQuery(1, 0, _count - 1, left, right);

    private int DoQuery(int node, int start, int end, int left, int right)
    {
        DoLazyUpdate(node, start, end);

        // Invalid Range
        if (right < start || end < left)
            return 0;

        // In Range
        if (left <= start && end <= right)
            return _tree[node];

        // Partially In Range
        int mid = (start + end) / 2;
        int lr = DoQuery(node << 1, start, mid, left, right);
        int rr = DoQuery(node << 1 | 1, mid + 1, end, left, right);

        return lr + rr;
    }

    public void UpdateRange(int left, int right) => DoUpdateRange(1, 0, _count - 1, left, right);

    private void DoUpdateRange(int node, int start, int end, int left, int right)
    {
        DoLazyUpdate(node, start, end);
    
        // Invalid Range
        if (right < start || end < left)
            return;

        // In Range
        if (left <= start && end <= right)
        {
            _tree[node] = _sectionSize[node] - _tree[node];

            if (start != end) // Not a leaf node
            {
                _lazy[node << 1]++;
                _lazy[node << 1 | 1]++;
            }

            return;
        }

        // Partially In Range
        int mid = (start + end) / 2;
        DoUpdateRange(node << 1, start, mid, left, right);
        DoUpdateRange(node << 1 | 1, mid + 1, end, left, right);
        _tree[node] = _tree[node << 1] + _tree[node << 1 | 1];
    }
}

class Reader{StreamReader R;public Reader()=>R=new(new BufferedStream(Console.OpenStandardInput()));
public int NextInt(){var(v,n,r)=(0,false,false);while(true){int c=R.Read();if((r,c)==(false,'-')){(n,r)=(true,true);continue;}if('0'<=c&&c<='9'){(v,r)=(v*10+(c-'0'),true);continue;}if(r==true)break;}return n?-v:v;}
}
#elif other2
using System;
using System.Text;

class Program {
    private static readonly TreeNode[] Tree = new TreeNode[400004];

    struct TreeNode {
        public int Count;
        public bool Lazy;
    }

    static void Main() {
        int n, m;
        string[] input = Console.ReadLine().Split();
        n = int.Parse(input[0]);
        m = int.Parse(input[1]);

        var sb = new StringBuilder();
        for (int i = 0; i < m; i++) {
            input = Console.ReadLine().Split();
            int a = int.Parse(input[0]);
            int b = int.Parse(input[1]);
            int c = int.Parse(input[2]);

            if (b > c) {
                (b, c) = (c, b);
            }

            if (a == 0) {
                UpdateLazy(1, n, b, c, 1);
            }
            else {
                sb.AppendLine(Sum(1, n, b, c, 1).ToString());
            }
        }
        
        Console.WriteLine(sb);
    }

    private static int Sum(int s, int e, int l, int r, int index) {
        PropagateLazyIfExist(index, s, e);

        if (s > r || e < l) {
            return 0;
        }

        if (s >= l && e <= r) {
            return Tree[index].Count;
        }
        
        return Sum(s, (s + e) / 2, l, r, index * 2) + Sum((s + e) / 2 + 1, e, l, r, index * 2 + 1);
    }
    
    private static void UpdateLazy(int s, int e, int l, int r, int index) {
        PropagateLazyIfExist(index, s, e);
        
        if (s > r || e < l) return;
        
        if (s >= l && e <= r) {
            Tree[index].Lazy = true;
            PropagateLazyIfExist(index, s, e);
            return;
        }

        UpdateLazy(s, (s + e) / 2, l, r, index * 2);
        UpdateLazy((s + e) / 2 + 1, e, l, r, index * 2 + 1);
        Tree[index].Count = Tree[index * 2].Count + Tree[index * 2 + 1].Count;
    }

    private static void PropagateLazyIfExist(int index, int s, int e) {
        if (!Tree[index].Lazy) return;
        
        Tree[index].Count = e - s + 1 - Tree[index].Count;

        if (s != e) {
            Tree[index * 2].Lazy = !Tree[index * 2].Lazy;
            Tree[index * 2 + 1].Lazy = !Tree[index * 2 + 1].Lazy;
        }

        Tree[index].Lazy = false;
    }
}
#elif other3
using System;
using System.Text;

public class Program
{
    static void Main()
    {
        string[] nm = Console.ReadLine().Split(' ');
        int n = int.Parse(nm[0]), m = int.Parse(nm[1]);
        StringBuilder sb = new();
        for (int i = 0; i < m; i++)
        {
            int[] ost = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int o = ost[0], s = ost[1], t = ost[2];
            if (o == 0)
                Update(1, n, 1, s, t);
            else
                sb.Append(Query(1, n, 1, s, t)).Append('\n');
        }
        sb.Remove(sb.Length - 1, 1);
        Console.Write(sb.ToString());
    }
    static int[] tree = new int[1 << 18];
    static bool[] lazy = new bool[1 << 18];
    static void Update(int start, int end, int node, int left, int right)
    {
        Propagation(start, end, node);
        if (start > right || end < left)
            return;
        if (start >= left && end <= right)
        {
            lazy[node] = true;
            Propagation(start, end, node);
            return;
        }
        if (start == end)
            return;
        int mid = (start + end) / 2;
        Update(start, mid, node * 2, left, right);
        Update(mid + 1, end, node * 2 + 1, left, right);
        tree[node] = tree[node * 2] + tree[node * 2 + 1];
    }
    static void Propagation(int start, int end, int node)
    {
        if (lazy[node])
        {
            if (start != end)
            {
                lazy[node * 2] ^= true;
                lazy[node * 2 + 1] ^= true;
            }
            tree[node] = end - start + 1 - tree[node];
            lazy[node] = false;
        }
    }
    static int Query(int start, int end, int node, int left, int right)
    {
        Propagation(start, end, node);
        if (start > right || end < left)
            return 0;
        if (start >= left && end <= right)
            return tree[node];
        int mid = (start + end) / 2;
        return Query(start, mid, node * 2, left, right) + Query(mid + 1, end, node * 2 + 1, left, right);
    }
}
#endif
}
