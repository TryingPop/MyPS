using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 8
이름 : 배성훈
내용 : 배열 정렬
   문제번호 : 28707번

    다익스트라 문제다.
    아이디어는 다음과 같다.
    배열을 하나의 노드로 봐야한다.
    다익스트라로 정렬된 배열로 갈 수 있는 최단 경로를 찾으면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1166
    {

        static void Main1166(string[] args)
        {

            int n, m;
            int[] arr;
            (int f, int t, int dis)[] edge;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                PriorityQueue<int, int> pq = new();
                HashSet<int> visit = new(50_000);
                Dictionary<int, int> ret = new(50_000);
                int num = ArrToInt();
                pq.Enqueue(num, 0);
                ret[num] = 0;

                Array.Sort(arr);
                int end = ArrToInt();

                while(pq.Count > 0)
                {

                    int node = pq.Dequeue();
                    if (node == end) break;
                    else if (visit.Contains(node)) continue;
                    visit.Add(node);
                    int cur = ret[node];
                    for (int i = 0; i < m; i++)
                    {

                        IntToArr(node);
                        Swap(edge[i].f, edge[i].t);
                        int next = ArrToInt();

                        int nDis = cur + edge[i].dis;
                        if (ret.ContainsKey(next) && ret[next] <= nDis) continue;
                        ret[next] = nDis;
                        pq.Enqueue(next, nDis);
                    }
                }

                if (ret.ContainsKey(end)) Console.Write(ret[end]);
                else Console.Write(-1);
            }

            void Swap(int _i, int _j)
            {

                int temp = arr[_i];
                arr[_i] = arr[_j];
                arr[_j] = temp;
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                m = ReadInt();
                edge = new (int f, int t, int dis)[m];
                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt() - 1;
                    int t = ReadInt() - 1;
                    int dis = ReadInt();

                    edge[i] = (f, t, dis);
                }

                sr.Close();

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

            void IntToArr(int _num)
            {

                int idx = -1;
                for (int i = 0; i < arr.Length; i++)
                {

                    int cur = 0;
                    for (int j = 0; j < 4; j++)
                    {

                        idx++;
                        if ((_num & (1 << idx)) == 0) continue;
                        cur |= 1 << j;
                    }

                    arr[i] = cur;
                }
            }

            int ArrToInt()
            {

                int hash = 0;
                int idx = -1;
                for (int i = 0; i < arr.Length; i++)
                {

                    for (int j = 0; j < 4; j++)
                    {

                        idx++;
                        if (((1 << j) & arr[i]) == 0) continue;
                        hash |= 1 << idx;
                    }
                }

                return hash;
            }
        }
    }

#if other
using ProblemSolving.Templates.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace ProblemSolving.Templates.Utility {}
namespace System {}
namespace System.Collections.Generic {}
namespace System.IO {}
namespace System.Linq {}

public class Arr : IEquatable<Arr>
{
    public int[] Data;

    public Arr(int[] arr)
    {
        Data = arr;
    }

    public Arr MakeSwap(int l, int r)
    {
        var arr = Data.Clone() as int[];
        (arr[l], arr[r]) = (arr[r], arr[l]);

        return new Arr(arr);
    }

    public bool IsSorted()
    {
        for (var idx = 0; idx < Data.Length - 1; idx++)
            if (Data[idx] > Data[idx + 1])
                return false;

        return true;
    }

    public bool Equals(Arr other)
    {
        return Data.SequenceEqual(other.Data);
    }
    public override bool Equals(object obj)
    {
        return obj is Arr arr && this.Equals(arr);
    }
    public override int GetHashCode()
    {
        var v = 0;
        foreach (var vv in Data)
            v = 11 * v + vv;

        return v;
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
        var v = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var init = new Arr(v);

        var m = Int32.Parse(sr.ReadLine());
        var ops = new (int l, int r, int cost)[m];
        for (var idx = 0; idx < m; idx++)
        {
            var (l, r, c) = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            ops[idx] = (l - 1, r - 1, c);
        }

        var dist = new Dictionary<Arr, long>();
        var pq = new PriorityQueue<Arr, long>();
        dist[init] = 0;
        pq.Enqueue(init, 0);

        while (pq.TryDequeue(out var arr, out var costSum))
        {
            if (dist[arr] != costSum)
                continue;

            if (arr.IsSorted())
            {
                sw.WriteLine(costSum);
                return;
            }

            foreach (var (l, r, c) in ops)
            {
                var next = arr.MakeSwap(l, r);
                if (!dist.TryGetValue(next, out var nextCost) || nextCost > costSum + c)
                {
                    dist[next] = costSum + c;
                    pq.Enqueue(next, dist[next]);
                }
            }
        }

        sw.WriteLine(-1);
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
#endif
}
