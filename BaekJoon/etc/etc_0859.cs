using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 2
이름 : 배성훈
내용 : 가희의 수열놀이 (Small, Large)
    문제번호 : 17162번, 17163번

    자료구조, 세그먼트 트리, 스택, 우선순위 큐 문제다
    아이디어는 다음과 같다
    세그먼트 트리로 mod 값들이 모두 담겨 있는지 확인하고 최소값을 관리했다
    그리고 각 나머지에 따른 이전값들을 알아야한다
    이는 배열을 따로 둬서 이전 값들과 나머지를 저장해 해결했다

    앞의 문제는 스택을 이용했으나 여기는 세그먼트 트리를 이용했다
    속도는 해당 방법이 20% 가까이 빠르다
*/

namespace BaekJoon.etc
{
    internal class etc_0859
    {

        static void Main859(string[] args)
        {

            int EMPTY = -1;

            string NO = "-1\n";

            StreamReader sr;
            StreamWriter sw;

            (int val, int back)[] arr;
            int[] seg;
            bool onlyNo;
            int n, mod;
            int len;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                for (int i = 0; i < n; i++)
                {

                    int op = ReadInt();

                    if (op == 1)
                    {

                        int num = ReadInt();

                        if (onlyNo) continue;
                        num %= mod;
                        num++;

                        int b = GetVal(1, mod, num);
                        Update(1, mod, num, len);
                        arr[len++] = (num, b);
                    }
                    else if (op == 2)
                    {

                        if (onlyNo || len == 0) continue;
                        len--;
                        Update(1, mod, arr[len].val, arr[len].back);
                    }
                    else
                    {

                        if (onlyNo || seg[0] == EMPTY) sw.Write(NO);
                        else sw.Write($"{len - seg[0]}\n");
                    }
                }

                sr.Close();
                sw.Close();
            }

            int GetVal(int _s, int _e, int _chk, int _idx = 0)
            {

                if (_s == _e) return seg[_idx];

                int mid = (_s + _e) >> 1;

                if (mid < _chk) return GetVal(mid + 1, _e, _chk, _idx * 2 + 2);
                else return GetVal(_s, mid, _chk, _idx * 2 + 1);
            }

            void Update(int _s, int _e, int _chk, int _val, int _idx = 0)
            {

                if (_s == _e)
                {

                    seg[_idx] = _val;
                    return;
                }

                int mid = (_s + _e) >> 1;

                if (mid < _chk) Update(mid + 1, _e, _chk, _val, _idx * 2 + 2);
                else Update(_s, mid, _chk, _val, _idx * 2 + 1);

                int l = seg[_idx * 2 + 1];
                int r = seg[_idx * 2 + 2];
                if (l == EMPTY || r == EMPTY) seg[_idx] = EMPTY;
                else seg[_idx] = Math.Min(l, r);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = ReadInt();
                mod = ReadInt();

                len = 0;

                onlyNo = n <= mod;
                if (onlyNo) 
                {

                    arr = new (int val, int back)[0];
                    seg = new int[0];
                    return; 
                }

                arr = new (int val, int back)[n];
                int log = (int)Math.Ceiling(Math.Log2(n)) + 1;
                seg = new int[1 << log];

                Array.Fill(seg, EMPTY);
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
using ProblemSolving.Templates.SegmentTree.Impl;
using ProblemSolving.Templates.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
namespace ProblemSolving.Templates.SegmentTree.Impl {}
namespace ProblemSolving.Templates.Utility {}
namespace System {}
namespace System.Collections.Generic {}
namespace System.IO {}
namespace System.Linq {}
namespace System.Numerics {}

#nullable disable

public class TrieNode
{
    public List<(int ch, TrieNode node)> Child = new List<(int, TrieNode)>();
}

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        Solve(sr, sw);
    }

    public static void Solve(StreamReader sr, StreamWriter sw)
    {
        var (qc, mod) = sr.ReadLine().Split(' ').Select(Int64.Parse).ToArray();
        if (mod > qc)
        {
            SolveOversize(sr, sw, qc);
            return;
        }

        var st = new Stack<(int prev, int val)>();
        var minseg = new MinSegTieLeft((int)mod);
        minseg.Init(-1);

        while (qc-- > 0)
        {
            var q = sr.ReadLine().Split(' ').Select(Int64.Parse).ToArray();

            if (q[0] == 1)
            {
                var num = (int)(q[1] % mod);
                var segv = (int)minseg.ElementAt(num).val;
                st.Push((segv, num));
                minseg.Update(num, st.Count - 1);
            }
            else if (q[0] == 2)
            {
                if (st.Any())
                {
                    var (prev, num) = st.Pop();
                    minseg.Update(num, prev);
                }
            }
            else
            {
                var (_, min) = minseg.AllRange;
                if (min == -1)
                    sw.WriteLine(-1);
                else
                    sw.WriteLine(st.Count - min);
            }
        }
    }

    private static void SolveOversize(StreamReader sr, StreamWriter sw, long qc)
    {
        while (qc-- > 0)
        {
            var q = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            if (q[0] == 3)
                sw.WriteLine(-1);
        }
    }
}

namespace ProblemSolving.Templates.Utility
{
    public static class DeconstructHelper
    {
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2) => (v1, v2) = (arr[0], arr[1]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3) => (v1, v2, v3) = (arr[0], arr[1], arr[2]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4) => (v1, v2, v3, v4) = (arr[0], arr[1], arr[2], arr[3]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5) => (v1, v2, v3, v4, v5) = (arr[0], arr[1], arr[2], arr[3], arr[4]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6) => (v1, v2, v3, v4, v5, v6) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6, out T v7) => (v1, v2, v3, v4, v5, v6, v7) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6]);
        public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6, out T v7, out T v8) => (v1, v2, v3, v4, v5, v6, v7, v8) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6], arr[7]);
    }
}


namespace ProblemSolving.Templates.SegmentTree.Impl
{
    public class MinSegTieLeft : MonoidSegTree<(int idx, long val), long>
    {
        public MinSegTieLeft(int size) : base(size)
        {
        }

        public void Init(long init)
        {
            var id = Identity();
            for (var idx = 0; idx < Size; idx++)
                _tree[_leafMask | idx] = (idx, init);
            for (var idx = Size; idx < _leafMask; idx++)
                _tree[_leafMask | idx] = id;

            for (var idx = _leafMask - 1; idx > 0; idx--)
                _tree[idx] = Merge(_tree[2 * idx], _tree[2 * idx + 1]);
        }
        public void Init(IList<long> init)
        {
            var id = Identity();
            for (var idx = 0; idx < init.Count; idx++)
                _tree[_leafMask | idx] = (idx, init[idx]);
            for (var idx = init.Count; idx < _leafMask; idx++)
                _tree[_leafMask | idx] = id;

            for (var idx = _leafMask - 1; idx > 0; idx--)
                _tree[idx] = Merge(_tree[2 * idx], _tree[2 * idx + 1]);
        }

        protected override (int idx, long val) Identity() => (-1, Int64.MaxValue);
        protected override (int idx, long val) Merge((int idx, long val) l, (int idx, long val) r) => l.val <= r.val ? l : r;
        protected override (int idx, long val) UpdateElement((int idx, long val) elem, long update) => (elem.idx, update);
    }
}


namespace ProblemSolving.Templates.SegmentTree
{
    public abstract class MonoidSegTree<TElement, TUpdate>
        where TElement : struct
        where TUpdate : struct
    {
        protected TElement[] _tree;
        protected int _leafMask;

        public int Size { get; private set; }

        private List<int> _lefts;
        private List<int> _rights;

        public MonoidSegTree(int size)
        {
            _leafMask = (int)BitOperations.RoundUpToPowerOf2((uint)size);
            var treeSize = _leafMask << 1;

            _lefts = new List<int>();
            _rights = new List<int>();

            _tree = new TElement[treeSize];
            Size = size;
        }

        public TElement AllRange => _tree[1];
        public TElement ElementAt(int idx) => _tree[_leafMask | idx];

        public void Init(IList<TElement> init)
        {
            var id = Identity();
            for (var idx = 0; idx < init.Count; idx++)
                _tree[_leafMask | idx] = init[idx];
            for (var idx = init.Count; idx < _leafMask; idx++)
                _tree[_leafMask | idx] = id;

            for (var idx = _leafMask - 1; idx > 0; idx--)
                _tree[idx] = Merge(_tree[2 * idx], _tree[2 * idx + 1]);
        }

        public void Update(int index, TUpdate val)
        {
            var curr = _leafMask | index;
            _tree[curr] = UpdateElement(_tree[curr], val);
            curr >>= 1;

            while (curr != 0)
            {
                _tree[curr] = Merge(_tree[2 * curr], _tree[2 * curr + 1]);
                curr >>= 1;
            }
        }

        public TElement Range(int stIncl, int edExcl)
        {
            if (stIncl >= _leafMask || edExcl > _leafMask)
                throw new ArgumentOutOfRangeException();

            var leftNode = _leafMask | stIncl;
            var rightNode = _leafMask | (edExcl - 1);

            _lefts.Clear();
            _rights.Clear();

            while (leftNode <= rightNode)
            {
                if ((leftNode & 1) == 1)
                    _lefts.Add(leftNode++);
                if ((rightNode & 1) == 0)
                    _rights.Add(rightNode--);

                leftNode >>= 1;
                rightNode >>= 1;
            }

            foreach (var idx in _rights.AsEnumerable().Reverse())
                _lefts.Add(idx);

            var aggregated = _tree[_lefts[0]];
            foreach (var idx in _lefts.Skip(1))
                aggregated = Merge(aggregated, _tree[idx]);

            return aggregated;
        }

        protected abstract TElement Identity();
        protected abstract TElement UpdateElement(TElement elem, TUpdate update);
        protected abstract TElement Merge(TElement l, TElement r);
    }
}

// This is source code merged w/ template
// Timestamp: 2024-07-06 10:48:24 UTC+9

#elif other2
// #include<cstdio>
using namespace std;
char buf[1<<17];
inline char read(){
	static int idx=1<<17;
	if(idx==1<<17){
		fread(buf, 1, 1<<17, stdin);
		idx=0;
	}
	return buf[idx++];
}
inline int readInt(){
	int sum=0;
	bool f=0;
	char cur=read();
	while(cur!='-' && (cur==10 || cur==32))	cur=read();
	if(cur=='-')	f=1, cur=read();
	while(cur>=48 && cur<=57){
		sum=sum*10+cur-48;
		cur=read();
	}
	if(f)	sum=-sum;
	return sum;
}
inline int min(int a, int b){return a<b?a:b;}
int A[1000001], p[1000001], seg[1<<21], base=1<<20;
void update(int i, int d){
	i+=base;
	seg[i]=d;
	for(i>>=1;i;i>>=1){
		int j=i*2;
		seg[i]=min(seg[j], seg[j+1]);
	}
}
int main(){
	int Q, mod, a, b, cnt=0, idx=0;
	Q=readInt();mod=readInt();
	if(mod>Q){
		while(Q--){
			a=readInt();
			if(a==1)	a=readInt();
			else if(a==3)	printf("-1\n");
		}
		return 0;
	}
	for(int i=1;i<base*2;++i)	seg[i]=1e9;
	while(Q--){
		a=readInt();
		if(a==1){
			b=readInt();
			A[++idx]=b=b%mod;
			if(seg[b+base]==1e9)	++cnt, p[idx]=0;
			else	p[idx]=seg[b+base];
			update(b, idx);
		}else if(a==2){
			if(idx){
				if(!p[idx])	--cnt, update(A[idx], 1e9);
				else	update(A[idx], p[idx]);
				--idx;
			}
		}else{
			if(cnt<mod)	printf("-1\n");
			else	printf("%d\n", idx+1-seg[1]);
		}
	}
	return 0;
}
#endif
}
