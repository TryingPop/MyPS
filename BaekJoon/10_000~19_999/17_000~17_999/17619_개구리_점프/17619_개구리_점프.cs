using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 16
이름 : 배성훈
내용 : 개구리 점프
    문제번호 : 17619번

    정렬, 유니온 파인드 문제다.
    개구리 점프 방식을 보면 x 간격이 겹치는 두 통나무는 서로 이동이 가능하다.
    그래서 이동이 가능한 그룹으로 분할한다.
    (추이, 대칭, 교환이 성립)
    그래서 x 시작으로 정렬한 뒤 끝을 비교해 이어주며 그룹을 지정했다.
    
    이후 두 통나무의 그룹이 같은지 확인해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1341
    {
        static void Main1341(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n, m;
            PriorityQueue<(int idx, int s, int e), int> pq;
            int[] group;

            Solve();
            void Solve()
            {

                Input();

                SetGroup();

                GetRet();
            }

            void GetRet()
            {

                string YES = "1\n";
                string NO = "0\n";

                while (m-- > 0)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    if (group[f] == group[t]) sw.Write(YES);
                    else sw.Write(NO);
                }
            }

            void SetGroup()
            {

                group = new int[n + 1];

                int end = -1;
                int g = 0;
                while (pq.Count > 0)
                {

                    var node = pq.Dequeue();

                    if (node.s <= end)
                    {

                        group[node.idx] = g;
                        end = Math.Max(node.e, end);
                    }
                    else
                    {

                        group[node.idx] = ++g;
                        end = node.e;
                    }
                }


            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();
                pq = new(n);

                for (int i = 1; i <= n; i++)
                {

                    int x1 = ReadInt();
                    int x2 = ReadInt();
                    int y = ReadInt();

                    pq.Enqueue((i, x1, x2), x1);
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

public class UnionFind
{
    private int[] _set;
    private int[] _rank;
    private List<int> _buffer;

    public UnionFind(int n)
    {
        _set = new int[n];
        _rank = new int[n];
        _buffer = new List<int>(n);

        for (var idx = 0; idx < n; idx++)
        {
            _set[idx] = idx;
            _rank[idx] = 1;
        }
    }

    public int Find(int v)
    {
        var root = _set[v];
        if (root == _set[root])
            return root;

        _buffer.Clear();
        do
        {
            _buffer.Add(root);
            root = _set[root];
        }
        while (root != _set[root]);

        foreach (var val in _buffer)
            _set[val] = root;

        return root;
    }

    public void Union(int l, int r)
    {
        var leftRoot = Find(l);
        var rightRoot = Find(r);

        if (leftRoot == rightRoot)
            return;

        if (_rank[leftRoot] < _rank[rightRoot])
        {
            _set[leftRoot] = rightRoot;
            _rank[rightRoot] += _rank[leftRoot];
        }
        else
        {
            _set[rightRoot] = leftRoot;
            _rank[leftRoot] += _rank[rightRoot];
        }
    }
}

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var nq = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var (n, q) = (nq[0], nq[1]);

        var lines = new (int idx, int minx, int maxx)[n];

        for (var idx = 0; idx < n; idx++)
        {
            var xxyy = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

            var minx = Math.Min(xxyy[0], xxyy[1]);
            var maxx = Math.Max(xxyy[0], xxyy[1]);

            lines[idx] = (idx, minx, maxx);
        }

        var uf = new UnionFind(n);
        var queue = new Queue<(int maxpos, int idx)>();

        foreach (var (idx, min, max) in lines.OrderBy(v => v.minx))
        {
            while (queue.Any() && queue.Peek().maxpos < min)
                queue.Dequeue();

            if (queue.Any())
                uf.Union(queue.Peek().idx, idx);

            queue.Enqueue((max, idx));
        }

        while (q-- > 0)
        {
            var query = sr.ReadLine().Split(' ').Select(s => Int32.Parse(s) - 1).ToArray();
            sw.WriteLine(uf.Find(query[0]) == uf.Find(query[1]) ? 1 : 0);
        }
    }
}

#endif
}
