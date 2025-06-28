using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 5
이름 : 배성훈
내용 : 트리와 쿼리 3
    문제번호 : 13512번

    HLD 문제다.
    색상을 정하는데, 세그먼트 트리에 어떻게 저장하는지가 관건이다.
    아래 방법은 인덱스를 저장하고 루트와 가까운 것을 살려 저장했다.
    루트와 가까운 것은 새로 적용된 인덱스가 낮은 쪽 즉, 왼쪽에 있는 것이 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1251
    {

        static void Main1251(string[] args)
        {

            StreamReader sr;

            int n, bias;
            List<int>[] edge;
            int[] seg;
            (int num, int head, int parent, int dep)[] chain;

            Solve();
            void Solve()
            {

                Input();

                SetChild();

                SetSeg();

                SetChain();

                GetRet();
            }

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int m = ReadInt();

                while (m-- > 0)
                {

                    int op = ReadInt();
                    int t = ReadInt();

                    if (op == 1) Update(chain[t].num, t);
                    else
                    {

                        int ret = 0;

                        int f = 1;
                        int chk;
                        while (chain[f].dep < chain[t].dep)
                        {

                            chk = GetVal(chain[chain[t].head].num, chain[t].num);
                            if (chk > 0) ret = chk;
                            t = chain[t].parent;
                        }

                        chk = GetVal(1, chain[t].num);
                        if (chk > 0) ret = chk;

                        if (ret == 0) ret = -1;

                        sw.Write($"{ret}\n");
                    }
                }
            }

            void Update(int _chk, int _val)
            {

                int idx = bias | _chk;
                seg[idx] = seg[idx] == 0 ? _val : 0;

                idx >>= 1;
                for (; idx > 0; idx >>= 1)
                {

                    int l = idx << 1;
                    int r = (idx << 1) + 1;

                    seg[idx] = seg[l] > 0 ? seg[l] : seg[r];
                }
            }

            int GetVal(int _l, int _r)
            {

                int ret = 0;
                _l |= bias;
                _r |= bias;

                while (_l <= _r)
                {

                    if ((_l & 1) == 1)
                    {

                        if (seg[_l] != 0) return seg[_l];
                        _l++;
                    }

                    if (((~_r) & 1) == 1)
                    {

                        if (seg[_r] != 0) ret = seg[_r];
                        _r--;
                    }

                    _l >>= 1;
                    _r >>= 1;
                }

                return ret;
            }

            void SetSeg()
            {

                int log = (int)(Math.Ceiling(Math.Log2(n) + 1e-9));
                seg = new int[1 << (log + 1)];
                bias = 1 << log;
            }

            void SetChain()
            {

                chain = new (int num, int head, int parent, int dep)[n + 1];
                int cnt = 1;
                chain[1].head = 1;
                DFS();

                void DFS(int _cur = 1, int _prev = 0, int _dep = 1)
                {

                    chain[_cur].num = cnt++;
                    chain[_cur].dep = _dep;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (next == _prev) continue;

                        if (i == 0)
                        {

                            chain[next].head = chain[_cur].head;
                            chain[next].parent = chain[_cur].parent;

                            DFS(next, _cur, _dep);
                        }
                        else
                        {

                            chain[next].head = next;
                            chain[next].parent = _cur;

                            DFS(next, _cur, _dep + 1);
                        }
                    }
                }
            }

            void SetChild()
            {

                int[] child = new int[n + 1];
                DFS();

                int DFS(int _cur = 1, int _prev = 0)
                {

                    ref int ret = ref child[_cur];
                    ret = 1;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (next == _prev) continue;

                        ret += DFS(next, _cur);

                        if (child[edge[_cur][0]] < child[next] || edge[_cur][0] == _prev)
                        {

                            int temp = edge[_cur][0];
                            edge[_cur][0] = next;
                            edge[_cur][i] = temp;
                        }
                    }

                    return ret;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                edge = new List<int>[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 1; i < n; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    edge[f].Add(t);
                    edge[t].Add(f);
                }
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == ' ' || c == '\n') return true;

                    ret = c - '0';
                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }
#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// #nullable disable

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

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var graph = new List<int>[n];

        for (var idx = 0; idx < n; idx++)
            graph[idx] = new List<int>();

        for (var idx = 0; idx < n - 1; idx++)
        {
            var (x, y) = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            x--;
            y--;

            graph[x].Add(y);
            graph[y].Add(x);
        }

        var children = new List<int>[n];
        var depth = new int[n];
        var subtreeSize = new int[n];
        FillSubtreeSize(graph, children, depth, subtreeSize, 0);

        var inlist = new bool[n];
        var hldinfo = new (int chainTop, int attachedTo, int mapPos)[n];
        var hldmap = new List<int>();
        HLD(children, subtreeSize, inlist, hldmap, hldinfo, 0, 0, 0);

        var isBlack = new bool[n];
        var pqs = new Dictionary<int, PriorityQueue<int, int>>();

        foreach (var v in hldinfo.Select(info => info.chainTop).Distinct())
            pqs[v] = new PriorityQueue<int, int>();

        var qc = Int32.Parse(sr.ReadLine());
        while (qc-- > 0)
        {
            var q = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

            if (q[0] == 1)
            {
                var (_, nodeIdx) = q;
                nodeIdx--;

                isBlack[nodeIdx] ^= true;
                if (isBlack[nodeIdx])
                    pqs[hldinfo[nodeIdx].chainTop].Enqueue(nodeIdx, depth[nodeIdx]);
            }
            else
            {
                var (_, u) = q;
                u--;

                var minDepth = default(int?);

                while (u != 0)
                {
                    var info = hldinfo[u];
                    var pq = pqs[info.chainTop];

                    while (pq.TryPeek(out var e1, out _) && !isBlack[e1])
                        pq.Dequeue();

                    if (pq.TryPeek(out var e, out var d) && d <= depth[u])
                        minDepth = 1 + e;

                    u = info.attachedTo;
                }

                if (isBlack[u])
                    minDepth = 1 + u;

                sw.WriteLine(minDepth ?? -1);
            }
        }
    }

    private static void HLD(
        List<int>[] children, int[] subtreeSize, bool[] inlist,
        List<int> hldmap, (int chainTop, int attachedTo, int mapPos)[] hldinfo,
        int curr, int chainTop, int attachedTo)
    {
        if (inlist[curr])
            return;

        inlist[curr] = true;
        hldmap.Add(0);
        hldinfo[curr] = (chainTop, attachedTo, hldmap.Count - 1);

        if (children[curr].Count == 0)
            return;

        var heavy = children[curr].MaxBy(v => subtreeSize[v]);
        HLD(children, subtreeSize, inlist, hldmap, hldinfo, heavy, chainTop, attachedTo);

        foreach (var light in children[curr])
        {
            if (light == heavy)
                continue;

            HLD(children, subtreeSize, inlist, hldmap, hldinfo, light, light, curr);
        }
    }

    private static void FillSubtreeSize(List<int>[] graph, List<int>[] children, int[] depth, int[] subtreeSize, int curr)
    {
        children[curr] = new List<int>();

        var size = 1;

        foreach (var child in graph[curr])
            if (children[child] == null)
            {
                depth[child] = 1 + depth[curr];
                FillSubtreeSize(graph, children, depth, subtreeSize, child);
                children[curr].Add(child);
                size += subtreeSize[child];
            }

        subtreeSize[curr] = size;
    }
}

#endif
}
