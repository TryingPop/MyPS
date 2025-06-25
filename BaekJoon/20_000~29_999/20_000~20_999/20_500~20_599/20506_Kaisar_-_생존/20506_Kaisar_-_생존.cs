using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 30
이름 : 배성훈
내용 : Kaisar - 생존
    문제번호 : 20506번

    트리, dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1658
    {

        static void Main1658(string[] args)
        {

            int n;
            List<int>[] edge;
            int root;

            Input();

            GetRet();

            void GetRet()
            {

                int[] child = new int[n + 1];
                long[] cnt = new long[n + 1];

                SetChild(root);

                DFS(root);

                long o = 0L, e = 0L;
                bool addOdd = true;

                for (int i = 1; i <= n; i++)
                {

                    long add = (cnt[i] / 2) * i;
                    o += add;
                    e += add;

                    if (cnt[i] % 2 == 1)
                    {

                        if (addOdd) o += i;
                        else e += i;
                        addOdd = !addOdd;
                    }
                }

                Console.Write($"{e} {o}");

                void DFS(int _cur)
                {

                    ref long ret = ref cnt[_cur];
                    if (ret != 0) return;
                    ret = child[_cur];

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        DFS(next);

                        cnt[_cur] += 1L * child[next] * (child[_cur] - child[next]);
                    }
                }

                int SetChild(int _cur)
                {


                    ref int ret = ref child[_cur];
                    if (ret != 0) return ret;
                    ret = 1;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        ret += SetChild(edge[_cur][i]);
                    }

                    return ret;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                edge = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                root = 0;
                for (int i = 1; i <= n; i++)
                {

                    int p = ReadInt();
                    if (p == 0) root = i;
                    else
                    {

                        edge[p].Add(i);
                    }
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
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
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// #nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var parent = sr.ReadLine().Split(' ').Select(Int32.Parse).Prepend(0).ToArray();
        var children = parent
            .Select((v, idx) => (v, idx))
            .GroupBy(p => p.v)
            .ToDictionary(g => g.Key, g => g.Select(p => p.idx).ToArray());

        var descendantCount = new long[1 + n];
        for (var idx = 1; idx <= n; idx++)
        {
            var curr = idx;
            while (curr != 0)
            {
                descendantCount[curr]++;
                curr = parent[curr];
            }
        }

        var lcaPairCount = new long[1 + n];
        for (var idx = 1; idx <= n; idx++)
        {
            var count = 2 * descendantCount[idx] - 1;
            var c = children.GetValueOrDefault(idx);

            if (c != null)
            {
                for (var c1 = 0; c1 < c.Length; c1++)
                    for (var c2 = 1 + c1; c2 < c.Length; c2++)
                        count += 2 * descendantCount[c[c1]] * descendantCount[c[c2]];
            }

            lcaPairCount[idx] = count;
        }

        var evenSum = 0L;
        var oddSum = 0L;
        var isOdd = false;

        for (var idx = 1; idx <= n; idx++)
        {
            var v = lcaPairCount[idx];
            if (isOdd)
            {
                oddSum += idx;
                v--;
                isOdd = false;
            }

            oddSum += idx * (v / 2);
            evenSum += idx * (v / 2);

            if (v % 2 == 1)
            {
                evenSum += idx;
                isOdd = true;
            }
        }

        sw.WriteLine($"{oddSum} {evenSum}");
    }
}

#endif
}
