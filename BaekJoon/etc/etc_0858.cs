using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 2
이름 : 배성훈
내용 : 가희와 수열놀이 (Small)
    문제번호 : 17162번

    스택, 자료구조, 수학 문제다
    mod의 범위가 작아 스택으로 저장하고 일일히 최소값을 확인하며 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0858
    {

        static void Main858(string[] args)
        {

            string NO = "-1\n";
            StreamReader sr;
            StreamWriter sw;

            int n, mod;
            int[] arr;
            Stack<int>[] stack;
            int match;
            int len;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                len = 0;
                match = 0;

                for (int i = 0; i < n; i++)
                {

                    int op = ReadInt();

                    if (op == 1)
                    {

                        int num = ReadInt() % mod;
                        arr[len] = num;

                        stack[num].Push(len);
                        if (stack[num].Count == 1) match++;
                        len++;
                    }
                    else if (op == 2)
                    {

                        if (len == 0) continue;
                        len--;
                        int num = arr[len];
                        stack[num].Pop();
                        if (stack[num].Count == 0) match--;
                    }
                    else
                    {

                        if (match != mod) 
                        { 
                            
                            sw.Write(NO);
                            continue;
                        }

                        int min = len;
                        for (int j = 0; j < mod; j++)
                        {

                            if (stack[j].Peek() < min) min = stack[j].Peek();
                        }

                        sw.Write($"{len - min}\n");
                    }
                }

                sr.Close();
                sw.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = ReadInt();
                mod = ReadInt();

                arr = new int[n];
                stack = new Stack<int>[mod];
                for (int i = 0; i < mod; i++)
                {

                    stack[i] = new();
                }
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
        if (mod > 2 * qc)
        {
            SolveOversize(sr, sw, qc);
            return;
        }

        var list = new List<int>();
        var st = new Stack<int>[mod];
        for (var idx = 0; idx < mod; idx++)
            st[idx] = new Stack<int>();

        var minseg = new MinSegTieLeft((int)mod);
        minseg.Init(-1);

        while (qc-- > 0)
        {
            var q = sr.ReadLine().Split(' ').Select(Int64.Parse).ToArray();

            if (q[0] == 1)
            {
                var num = (int)(q[1] % mod);
                list.Add(num);
                st[num].Push(list.Count - 1);
                minseg.Update(num, st[num].Peek());
            }
            else if (q[0] == 2)
            {
                if (list.Any())
                {
                    var last = list[^1];
                    list.RemoveAt(list.Count - 1);
                    st[last].Pop();

                    if (st[last].Count == 0)
                        minseg.Update(last, -1);
                    else
                        minseg.Update(last, st[last].Peek());
                }
            }
            else
            {
                var (_, min) = minseg.Range(0, minseg.Size);
                if (min == -1)
                    sw.WriteLine(-1);
                else
                    sw.WriteLine(list.Count - min);
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
    public class MinSegTieLeft : SemigroupSegTree<(int idx, long val), long>
    {
        public MinSegTieLeft(int size) : base(size)
        {
        }

        public void Init(long init)
        {
            for (var idx = 0; idx < _leafMask; idx++)
                _tree[_leafMask | idx] = (idx, init);

            for (var idx = _leafMask - 1; idx > 0; idx--)
                _tree[idx] = Merge(_tree[2 * idx], _tree[2 * idx + 1]);
        }
        public void Init(IList<long> init)
        {
            for (var idx = 0; idx < init.Count; idx++)
                _tree[_leafMask | idx] = (idx, init[idx]);

            for (var idx = _leafMask - 1; idx > 0; idx--)
                _tree[idx] = Merge(_tree[2 * idx], _tree[2 * idx + 1]);
        }

        protected override (int idx, long val) Merge((int idx, long val) l, (int idx, long val) r)
        {
            return l.val <= r.val ? l : r;
        }

        protected override (int idx, long val) UpdateElement((int idx, long val) elem, long update)
        {
            return (elem.idx, update);
        }
    }
}


namespace ProblemSolving.Templates.SegmentTree
{
    public abstract class SemigroupSegTree<TElement, TUpdate>
        where TElement : struct
        where TUpdate : struct
    {
        protected TElement[] _tree;
        protected int _leafMask;

        public int Size { get; private set; }

        private List<int> _lefts;
        private List<int> _rights;

        public SemigroupSegTree(int size)
        {
            _leafMask = (int)BitOperations.RoundUpToPowerOf2((uint)size);
            var treeSize = _leafMask << 1;

            _lefts = new List<int>();
            _rights = new List<int>();

            _tree = new TElement[treeSize];
            Size = size;
        }

        public TElement ElementAt(int idx) => _tree[_leafMask | idx];

        public void Init(IList<TElement> init)
        {
            for (var idx = 0; idx < init.Count; idx++)
                _tree[_leafMask | idx] = init[idx];

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

        protected abstract TElement UpdateElement(TElement elem, TUpdate update);
        protected abstract TElement Merge(TElement l, TElement r);
    }
}

// This is source code merged w/ template
// Timestamp: 2024-07-06 10:31:07 UTC+9
#elif other2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs17162
{
    class Program
    {
        public static Stack<int> series;
        public static Stack<int>[] stack;
        public static int Q;
        public static int MOD;

        public static StringBuilder sb;

        static void Main(string[] args)
        {
            sb = new StringBuilder();


            int[] line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            Q = line[0];
            MOD = line[1];

            series = new Stack<int>();
            stack = new Stack<int>[MOD];
            for (int i = 0; i < MOD; i++) stack[i] = new Stack<int>();

            for(int query = 0; query < Q; query++)
            {
                line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                switch (line[0])
                {
                    case 1:
                        Q1(line[1]);
                        break;
                    case 2:
                        Q2();
                        break;
                    case 3:
                        Q3();
                        break;
                }
            }

            Console.Write(sb);
        }

        static void Q1(int num)
        {
            series.Push(num);
            stack[num % MOD].Push(series.Count);
        }

        static void Q2()
        {
            if (series.Count == 0)
                return;

            int num = series.Pop();
            stack[num % MOD].Pop();
        }

        static void Q3()
        {
            if (series.Count < MOD)
            {
                //Console.WriteLine(-1);
                sb.AppendLine("-1");
                return;
            }

            int min = int.MaxValue;
            int cur = int.MaxValue;
            for (int i = 0; i < MOD; i++)
            {
                if (stack[i].Count == 0)
                {
                    //Console.WriteLine(-1);
                    sb.AppendLine("-1");
                    return;
                }
                cur = stack[i].Peek();
                if (min > cur)
                    min = cur;
            }

            //Console.WriteLine(series.Count - min + 1);
            sb.AppendFormat("{0}\n", series.Count - min + 1);
        }
    }
}

#elif other3
// #include<iostream>
using namespace std;
int main(){
    ios::sync_with_stdio(0);cin.tie(0);cout.tie(0);
	int q,mod,arr[200001]={0,},s=0,e=0,cnt=0,chk[101];
	cin>>q>>mod;
	while(q--){
		int a,b;
        cin>>a;
		if(a==1){
            cin>>arr[e];
			arr[e]%=mod;
			if(!chk[arr[e++]]++) ++cnt;
			while(cnt==mod&&s<e&&chk[arr[s]]>1) 
				--chk[arr[s++]];
		}
		else if(a==2){
			if(e!=0){
				if(!--chk[arr[--e]]) --cnt;
				while(cnt!=mod&&s>0) cnt+=!chk[arr[--s]]++;
			}
		}
		else{
			cout<<(cnt==mod?e-s:-1)<<'\n';
		}
	}
}
#endif
}
