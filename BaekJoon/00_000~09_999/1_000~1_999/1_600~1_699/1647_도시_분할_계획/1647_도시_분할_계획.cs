using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 25
이름 : 배성훈
내용 : 도시 분할 계획
    문제번호 : 1647번

    최소 신장 트리를 쓰는 문제
    두 개의 마을로 분할해야한다

    마을을 분할할 때는 각 분리된 마을 안에 집들이 서로 연결되도록 분할해야 한다
    즉, 분할된 마을은 MST로 만들어야한다

    그래서 전체에 대해서 MST를 만들고
    가장 비싼 경로를 끊어버리니 이상없이 통과했다

    크루스칼로 MST를 찾았는데, 쓴 자료구조는 PriorityQueue였다
    그런데 다른 사람 풀이를 보니 구지 PriorityQueue를 쓸 필요가 없이 
    배열로 저장하고 정렬해서 해결해도 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0090
    {

        static void Main90(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int m = ReadInt(sr);

            int[] group = new int[n + 1];
            for (int i = 1; i <= n; i++)
            {

                group[i] = i;
            }

            // 경로 val로 정렬
            PriorityQueue<(int from, int to, int val), int> q = new(m);

            for (int i = 0; i < m; i++)
            {

                int from = ReadInt(sr);
                int to = ReadInt(sr);
                int val = ReadInt(sr);

                q.Enqueue((from, to, val), val);
            }

            // 크루스칼 알고리즘
            Stack<int> s = new Stack<int>(n);
            int ret = 0;
            int max = -1;
            while(q.Count > 0)
            {

                var node = q.Dequeue();

                int f = Find(group, node.from, s);
                int t = Find(group, node.to, s);

                if (f == t) continue;

                ret += node.val;
                if (max < node.val) max = node.val;
                group[f] = t;
            }
            ret -= max;

            Console.WriteLine(ret);
        }

        static int Find(int[] _group, int _chk, Stack<int> _calc)
        {

            while(_chk != _group[_chk])
            {

                _calc.Push(_chk);
                _chk = _group[_chk];
            }

            while(_calc.Count > 0)
            {

                _group[_calc.Pop()] = _chk;
            }

            return _chk;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
internal class Program
{
    private static void Main(string[] args)
    {
        using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

        int n = ScanInt(sr);
        if (n == 2)
        {
            Console.Write(0);
            return;
        }
        int m = ScanInt(sr);
        var edges = new ValueTuple<int, int, int>[m];
        for (int i = 0; i < m; i++)
        {
            edges[i] = (ScanInt(sr), ScanInt(sr), ScanInt(sr));
        }
        Array.Sort(edges, (x, y) => x.Item3 - y.Item3);

        var link = new int[n + 1];
        for (int i = 1; i <= n; i++)
        {
            link[i] = i;
        }

        var costSum = 0;
        var lastWay = n - 2;
        foreach ((var a, var b, var c) in edges)
        {
            if (GetRoot(a) == GetRoot(b))
                continue;
            Union(a, b);
            costSum += c;
            if (--lastWay == 0)
                break;
        }

        Console.Write(costSum);

        void Union(int a, int b)
        {
            int rootA = GetRoot(a), rootB = GetRoot(b);
            int rootMin = Math.Min(rootA, rootB), rootMax = Math.Max(rootA, rootB);
            link[rootMax] = rootMin;
        }

        int GetRoot(int index)
        {
            ref var linked = ref link[index];
            if (linked == index)
                return index;
            return linked = GetRoot(linked);
        }


    }

    static int ScanInt(StreamReader sr)
    {
        int c, n = 0;
        while (!((c = sr.Read()) is ' ' or '\n' or -1))
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            n = 10 * n + c - '0';
        }
        return n;
    }
}
#endif
}
