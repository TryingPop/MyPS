using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 24
이름 : 배성훈
내용 : malloc
    문제번호 : 3217번

    구현, 연결리스트 문제다.
    메모리를 세그먼트 트리로 표현해 풀었다.
    세그먼트 트리로 풀시에 size만큼의 구간이 있는 가장 작은 l을 찾아야 한다.
    그러니 구간의 가장 큰 값이 중요하고 이에 left, right, total의 가장 큰 구간을 저장해야 했다.

    연결리스트 형태로도 풀 수 있다고 한다.
    https://zoosso.tistory.com/389
*/

namespace BaekJoon.etc
{
    internal class etc_1642
    {

        static void Main1642(string[] args)
        {

            int INF = 100_001;

            int S = 1;
            int E = 100_000;
            string FREE = "free";
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            Dictionary<string, int> vTi = new(1_000);
            (int s, int size)[] node = new (int s, int size)[1_000];

            (int l, int r, int t, int sum)[] seg;

            SetSeg();

            void SetSeg()
            {

                seg = new (int l, int r, int t, int sum)[1 << 18];
                for (int i = 1; i <= 100_000; i++)
                {

                    Update(S, E, i, i, false);
                }
            }

            int q = int.Parse(sr.ReadLine());
            int idx = 0;

            while (q-- > 0)
            {

                string var;
                int type, size;
                ReadOp();

                if (type == 1) Malloc();
                else if (type == 2) Free();
                else Print();

                int GetIdx()
                {

                    if (!vTi.ContainsKey(var)) vTi[var] = idx++;
                    return vTi[var];
                }

                void Malloc()
                {

                    int idx = GetIdx();
                    int start = GetVal(S, E, size);
                    if (start == INF || E < start + size - 1)
                    {

                        start = 0;
                        size = 0;
                    }
                    else Update(S, E, start, start + size - 1, true);

                    node[idx] = (start, size);
                }

                void Free()
                {

                    int idx = GetIdx();
                    if (node[idx].s == 0) return;

                    Update(S, E, node[idx].s, node[idx].s + node[idx].size - 1, false);
                    node[idx].s = 0;
                }

                void Print()
                {

                    int idx = GetIdx();
                    sw.Write($"{node[idx].s}\n");
                }

                void ReadOp()
                {

                    string[] input = sr.ReadLine().Split('=');

                    if (input.Length == 1)
                    {

                        string[] op = input[0].Split('(', ')');
                        var = op[1];

                        if (op[0] == FREE)
                        {

                            type = 2;
                            size = 0;
                        }
                        else
                        {

                            type = 3;
                            int idx = GetIdx();
                            size = node[idx].s;
                        }
                    }
                    else
                    {

                        type = 1;
                        var = input[0];
                        string[] op = input[1].Split('(', ')');
                        size = int.Parse(op[1]);
                    }
                }
            }

            int GetVal(int _s, int _e, int _chkSize, int _idx = 0)
            {

                if (_chkSize <= seg[_idx].l) return _s;
                else if (seg[_idx].t < _chkSize) return INF;

                int mid = (_s + _e) >> 1;
                int l = _idx * 2 + 1;
                int r = _idx * 2 + 2;
                int chk = GetVal(_s, mid, _chkSize, l);

                if (chk < INF) return chk;
                else if (_chkSize <= seg[l].r + seg[r].l) return mid - seg[l].r + 1;
                else return GetVal(mid + 1, _e, _chkSize, r);
            }

            void Update(int _s, int _e, int _chkS, int _chkE, bool _chk, int _idx = 0)
            {

                if (_chkS <= _s && _e <= _chkE)
                {

                    if (_chk) seg[_idx] = (0, 0, 0, 0);
                    else
                    {

                        int len = _e - _s + 1;
                        seg[_idx] = (len, len, len, len);
                    }

                    return;
                }
                else if (_e < _chkS || _chkE < _s) return;

                int mid = (_s + _e) >> 1;
                int l = _idx * 2 + 1;
                int r = _idx * 2 + 2;
                Update(_s, mid, _chkS, _chkE, _chk, l);
                Update(mid + 1, _e, _chkS, _chkE, _chk, r);

                seg[_idx].l = seg[l].l;
                if (0 < seg[l].l && seg[l].l == seg[l].r && seg[l].sum == seg[l].l) seg[_idx].l += seg[r].l;

                seg[_idx].r = seg[r].r;
                if (0 < seg[r].r && seg[r].r == seg[r].l && seg[r].r == seg[r].sum) seg[_idx].r += seg[l].r;

                seg[_idx].sum = seg[l].sum + seg[r].sum;
                seg[_idx].t = Math.Max(seg[l].t, seg[r].t);

                if (seg[_idx].t < seg[_idx].l)
                    seg[_idx].t = seg[_idx].l;

                if (seg[_idx].t < seg[l].r + seg[r].l)
                    seg[_idx].t = seg[l].r + seg[r].l;

                if (seg[_idx].t < seg[_idx].r)
                    seg[_idx].t = seg[_idx].r;
            }
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
namespace System {}
namespace System.Collections.Generic {}
namespace System.IO {}

#nullable disable

public class Bucket
{
    public int StIncl;
    public int EdExcl;

    public PriorityQueue<int, int> MaxSizePq;
    public int[] EmptySize;

    public Bucket(int stIncl, int edExcl)
    {
        StIncl = stIncl;
        EdExcl = edExcl;

        MaxSizePq = new PriorityQueue<int, int>();
        EmptySize = new int[edExcl - stIncl];
    }

    public void SetValue(int offset, int val)
    {
        EmptySize[offset] = val;
        MaxSizePq.Enqueue(offset, -val);
    }
    public void RemoveValue(int offset)
    {
        EmptySize[offset] = 0;
    }

    public bool CanAllocate(int size)
    {
        while (MaxSizePq.TryPeek(out var offset, out var s))
        {
            if (EmptySize[offset] != -s)
            {
                MaxSizePq.Dequeue();
                continue;
            }

            return -s >= size;
        }

        return false;
    }
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
        var n = Int32.Parse(sr.ReadLine());

        var variables = new Dictionary<string, LinkedListNode<(bool allocated, int pos, int size)>>();
        var ll = new LinkedList<(bool allocated, int pos, int size)>();
        ll.AddLast(new LinkedListNode<(bool allocated, int pos, int size)>((false, 1, 100000)));

        while (n-- > 0)
        {
            var s = sr.ReadLine().Trim();

            if (s.StartsWith("print("))
            {
                var name = s.Substring(6, s.Length - 8);
                var node = variables.GetValueOrDefault(name);

                if (node == null)
                    sw.WriteLine(0);
                else
                    sw.WriteLine(node.ValueRef.pos);
            }
            else if (s.StartsWith("free("))
            {
                var name = s.Substring(5, s.Length - 7);
                var node = variables.GetValueOrDefault(name);

                if (node != null)
                {
                    var prev = node.Previous;
                    var next = node.Next;

                    node.ValueRef.allocated = false;
                    // try merge
                    if (TryMerge(ll, prev))
                        TryMerge(ll, prev);
                    else
                        TryMerge(ll, node);
                }

                variables[name] = null;
            }
            else
            {
                var splitted = s.Split('=');
                var name = splitted[0];
                var size = Int32.Parse(splitted[1].Substring(7, splitted[1].Length - 9));

                var curr = ll.First;
                variables[name] = null;

                while (curr != null)
                {
                    if (!curr.ValueRef.allocated && curr.ValueRef.size >= size)
                    {
                        curr.ValueRef.allocated = true;
                        variables[name] = curr;

                        var remain = curr.ValueRef.size - size;
                        curr.ValueRef.size = size;
                        if (remain > 0)
                        {
                            var pos = curr.ValueRef.pos + size;
                            var newnode = new LinkedListNode<(bool allocated, int pos, int size)>((false, pos, remain));
                            ll.AddAfter(curr, newnode);
                        }

                        break;
                    }

                    curr = curr.Next;
                }
            }
        }
    }

    private static bool TryMerge(LinkedList<(bool allocated, int pos, int size)> ll, LinkedListNode<(bool allocated, int pos, int size)> node)
    {
        if (node == null)
            return false;

        var next = node.Next;
        if (next == null)
            return false;

        if (!node.ValueRef.allocated && !next.ValueRef.allocated)
        {
            node.ValueRef.size += next.ValueRef.size;
            ll.Remove(next);
            return true;
        }

        return false;
    }
}
#elif other2
// #include <bits/stdc++.h>
// #define fastio cin.tie(0)->sync_with_stdio(0)
using namespace std;

int main() {
	fastio;
	int q; cin >> q;

	list<pair<int, int>> L;
	vector M(456976, L.end());
	L.insert(L.end(), { 1, 1 });
	L.insert(L.end(), { 100'001, 100'001 });

	auto hash = [&](const string& s) {
		int ret = 0;
		for (int i = 0; i < 4; i++) ret = 26 * ret + s[i] - 97;
		return ret;
	};

	auto malloc = [&](int s, int sz) {
		M[s] = L.end();
		auto cur = L.begin();
		auto nxt = next(cur);
		while (nxt != L.end() && nxt->first - cur->second < sz) cur++, nxt++;
		if (nxt == L.end()) return;
		M[s] = L.insert(nxt, { cur->second, cur->second + sz });
	};

	auto free = [&](int s) {
		if (M[s] == L.end()) return;
		L.erase(M[s]);
		M[s] = L.end();
	};

	auto print = [&](int s) {
		if (M[s] == L.end()) cout << 0 << '\n';
		else cout << M[s]->first << '\n';
	};

	while (q--) {
		string s; cin >> s;
		if (s[4] == '=') malloc(hash(s.substr(0, 4)), stoi(s.substr(12, s.size() - 14)));
		else if (s[0] == 'f') free(hash(s.substr(5, 4)));
		else print(hash(s.substr(6, 4)));
	}
}
#endif
}
