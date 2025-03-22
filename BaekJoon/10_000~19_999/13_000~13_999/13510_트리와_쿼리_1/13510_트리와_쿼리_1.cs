using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 5
이름 : 배성훈
내용 : 트리와 쿼리 1
    문제번호 : 13510번

    해비 라이트 분할(HLD) 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1248
    {

        static void Main1248(string[] args)
        {

            StreamReader sr;

            int S, E;

            int n;
            int[] child;
            List<(int dst, int dis)>[] edge;
            (int parent, int head, int dep, int num)[] chain;
            int[] seg;
            (int f, int t)[] edgeNum;

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
                    if (op == 1)
                    {

                        int idx = ReadInt();
                        int newDis = ReadInt();

                        // 목적지로 가는 큰쪽에 값을 갱신한다.
                        var e = edgeNum[idx];
                        int v = e.f;
                        if (chain[e.f].num < chain[e.t].num) v = e.t;
                        Update(S, E, chain[v].num, newDis);
                    }
                    else
                    {

                        int f = ReadInt();
                        int t = ReadInt();

                        if (chain[f].dep > chain[t].dep)
                        {

                            int temp = f;
                            f = t;
                            t = temp;
                        }

                        int ret = 0;

                        // 깊이 맞추기 (LCA)
                        while (chain[f].dep < chain[t].dep)
                        {

                            ret = Math.Max(ret, GetVal(S, E, chain[chain[t].head].num, chain[t].num));
                            t = chain[t].parent;
                        }

                        // head가 같아질때까지 부모로 이동
                        while (chain[f].head != chain[t].head)
                        {

                            ret = Math.Max(ret, GetVal(S, E, chain[chain[f].head].num, chain[f].num));
                            ret = Math.Max(ret, GetVal(S, E, chain[chain[t].head].num, chain[t].num));

                            f = chain[f].parent;
                            t = chain[t].parent;
                        }

                        if (chain[f].num > chain[t].num)
                        {

                            int temp = f;
                            f = t;
                            t = temp;
                        }

                        ret = Math.Max(ret, GetVal(S, E, chain[f].num + 1, chain[t].num));

                        sw.Write($"{ret}\n");
                    }
                }
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

                seg[_idx] = Math.Max(seg[_idx * 2 + 1], seg[_idx * 2 + 2]);
            }

            int GetVal(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                if (_e < _chkS || _chkE < _s) return 0;
                else if (_chkS <= _s && _e <= _chkE) return seg[_idx];

                int mid = (_s + _e) >> 1;
                return Math.Max(GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1), GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2));
            }

            void SetSeg()
            {

                int log = (int)(Math.Ceiling(Math.Log2(n) + 1e-9)) + 1;
                seg = new int[1 << log];

                S = 1;
                E = n;
            }

            void SetChain()
            {

                chain = new (int parent, int head, int dep, int num)[n + 1];
                int cnt = 1;
                chain[1].head = 1;
                DFS();

                void DFS(int _cur = 1, int _prev = 0, int _depth = 1)
                {

                    chain[_cur].num = cnt++;
                    chain[_cur].dep = _depth;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i].dst;
                        if (_prev == next) continue;

                        // 앞에서 가장 많은 자식들과 연결된 간선을 0번으로 옮겼다.
                        if (i == 0)
                        {

                            chain[next].head = chain[_cur].head;
                            chain[next].parent = chain[_cur].parent;

                            DFS(next, _cur, _depth);
                        }
                        else
                        {

                            chain[next].head = next;
                            chain[next].parent = _cur;

                            DFS(next, _cur, _depth + 1);
                        }

                        Update(S, E, chain[next].num, edge[_cur][i].dis);
                    }
                }
            }

            void SetChild()
            {

                child = new int[n + 1];
                DFS();

                int DFS(int _cur = 1, int _prev = 0)
                {

                    ref int ret = ref child[_cur];
                    ret = 1;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i].dst;
                        if (next == _prev) continue;

                        ret += DFS(next, _cur);

                        if (child[edge[_cur][0].dst] < child[next] 
                            || edge[_cur][0].dst == _prev)
                        {

                            var temp = edge[_cur][0];
                            edge[_cur][0] = edge[_cur][i];
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

                edge = new List<(int dst, int dis)>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                edgeNum = new (int f, int t)[n];
                for (int i = 1; i < n; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    int w = ReadInt();

                    if (t < f)
                    {

                        int temp = f;
                        f = t;
                        t = temp;
                    }

                    edge[f].Add((t, w));
                    edge[t].Add((f, w));
                    edgeNum[i] = (f, t);
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
                    if (c == '\n' || c == ' ') return true;

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
using System.Numerics;
using System.Runtime.CompilerServices;

#nullable disable

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

public sealed class MaxSeg
{
    private int[] _tree;
    private int _leafMask;

    public MaxSeg(int size)
    {
        var initSizeLog = 1 + BitOperations.Log2((uint)size - 1);

        _leafMask = 1 << initSizeLog;
        var treeSize = 2 * _leafMask;

        _tree = new int[treeSize];
    }

    internal void Init(List<int> init)
    {
        for (var idx = 0; idx < init.Count; idx++)
            _tree[_leafMask | idx] = init[idx];

        for (var idx = _leafMask - 1; idx > 0; idx--)
            _tree[idx] = Math.Max(_tree[2 * idx], _tree[2 * idx + 1]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Update(int index, int val)
    {
        var curr = _leafMask | index;
        _tree[curr] = val;
        curr >>= 1;

        while (curr != 0)
        {
            _tree[curr] = Math.Max(_tree[2 * curr], _tree[2 * curr + 1]);
            curr >>= 1;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Range(int stIncl, int edExcl)
    {
        var leftNode = _leafMask | stIncl;
        var rightNode = _leafMask | (edExcl - 1);

        var aggregated = 0;
        while (leftNode <= rightNode)
        {
            if ((leftNode & 1) == 1)
                aggregated = Math.Max(aggregated, _tree[leftNode++]);
            if ((rightNode & 1) == 0)
                aggregated = Math.Max(aggregated, _tree[rightNode--]);

            leftNode >>= 1;
            rightNode >>= 1;
        }

        return aggregated;
    }
}

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var graph = new List<(int dst, int cost, int idx)>[n];

        for (var idx = 0; idx < n; idx++)
            graph[idx] = new List<(int dst, int cost, int idx)>();

        for (var idx = 0; idx < n - 1; idx++)
        {
            var (x, y, c) = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            x--;
            y--;

            graph[x].Add((y, c, idx));
            graph[y].Add((x, c, idx));
        }

        var children = new List<(int dst, int cost)>[n];
        var depth = new int[n];
        var subtreeSize = new int[n];
        FillSubtreeSize(graph, children, depth, subtreeSize, 0);

        var nodeIdxToChild = new int[n - 1];
        for (var src = 0; src < n; src++)
            foreach (var (dst, _, idx) in graph[src])
                nodeIdxToChild[idx] = subtreeSize[src] < subtreeSize[dst] ? src : dst;

        var inlist = new bool[n];
        var hldinfo = new (int chainTop, int attachedTo, int mapPos)[n];
        var hldmap = new List<int>();
        HLD(children, subtreeSize, inlist, hldmap, hldinfo, 0, 0, 0);

        for (var src = 0; src < n; src++)
            foreach (var (dst, cost) in children[src])
                hldmap[hldinfo[dst].mapPos] = cost;

        var hldseg = new MaxSeg(n);
        hldseg.Init(hldmap);

        var qc = Int32.Parse(sr.ReadLine());
        while (qc-- > 0)
        {
            var q = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

            if (q[0] == 1)
            {
                var (_, nodeIdx, c) = q;
                nodeIdx--;

                var childIdx = nodeIdxToChild[nodeIdx];
                hldseg.Update(hldinfo[childIdx].mapPos, c);
            }
            else
            {
                var (_, u, v) = q;
                u--;
                v--;

                var (upos, vpos) = (u, v);
                var maxCost = 0;
                while (hldinfo[upos].chainTop != hldinfo[vpos].chainTop)
                {
                    var uinfo = hldinfo[upos];
                    var vinfo = hldinfo[vpos];

                    if (depth[uinfo.chainTop] > depth[vinfo.chainTop])
                    {
                        var stIncl = hldinfo[uinfo.chainTop].mapPos;
                        var edIncl = uinfo.mapPos;

                        maxCost = Math.Max(maxCost, hldseg.Range(stIncl, edIncl + 1));
                        upos = uinfo.attachedTo;
                    }
                    else
                    {
                        var stIncl = hldinfo[vinfo.chainTop].mapPos;
                        var edIncl = vinfo.mapPos;

                        maxCost = Math.Max(maxCost, hldseg.Range(stIncl, edIncl + 1));
                        vpos = vinfo.attachedTo;
                    }
                }

                if (upos != vpos)
                {
                    var uinfo = hldinfo[upos];
                    var vinfo = hldinfo[vpos];

                    var min = Math.Min(uinfo.mapPos, vinfo.mapPos);
                    var max = Math.Max(uinfo.mapPos, vinfo.mapPos);

                    maxCost = Math.Max(maxCost, hldseg.Range(min + 1, max + 1));
                }

                sw.WriteLine(maxCost);
            }
        }
    }

    private static void HLD(
        List<(int dst, int cost)>[] children, int[] subtreeSize, bool[] inlist,
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

        var heavy = children[curr].MaxBy(v => subtreeSize[v.dst]).dst;
        HLD(children, subtreeSize, inlist, hldmap, hldinfo, heavy, chainTop, attachedTo);

        foreach (var (light, _) in children[curr])
        {
            if (light == heavy)
                continue;

            HLD(children, subtreeSize, inlist, hldmap, hldinfo, light, light, curr);
        }
    }

    private static void FillSubtreeSize(
        List<(int dst, int cost, int idx)>[] graph,
        List<(int dst, int cost)>[] children, int[] depth, int[] subtreeSize, int curr)
    {
        children[curr] = new List<(int dst, int cost)>();

        var size = 1;

        foreach (var (child, cost, _) in graph[curr])
            if (children[child] == null)
            {
                depth[child] = 1 + depth[curr];
                FillSubtreeSize(graph, children, depth, subtreeSize, child);
                children[curr].Add((child, cost));
                size += subtreeSize[child];
            }

        subtreeSize[curr] = size;
    }
}

#endif
}
