using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 28
이름 : 배성훈
내용 : Sequence
    문제번호 : 13077번

    수학, 트리 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1648
    {

        static void Main1648(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t = ReadInt();

            while (t-- > 0)
            {

                int u = ReadInt();
                int d = ReadInt();

                int x = 0, y = 0;
                while (u != d)
                {

                    if (u > d)
                    {

                        u -= d;
                        x += 1 << y;
                    }
                    else d -= u;

                    y++;
                }

                long ret = (1L << y) + x;
                sw.Write($"{ret}\n");
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
                    ret = ret * 10 + c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n' && c != '/')
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
// #nullable disable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var t = Int32.Parse(sr.ReadLine());
        while (t-- > 0)
        {
            var npq = sr.ReadLine().Split(' ', '/').Select(Int32.Parse).ToArray();
            //var n = npq[0];
            var p = npq[0];
            var q = npq[1];

            var seq = new List<bool>();
            while (p != 1 || q != 1)
            {
                GetParent(p, q, out var pp, out var pq, out var isLeft);
                p = pp;
                q = pq;

                seq.Add(isLeft);
            }

            var index =
                1
                + ((1 << seq.Count) - 1)
                + (seq.AsEnumerable().Select((v, idx) => v ? 0 : (1 << idx)).Sum());

            sw.WriteLine(index);
        }
    }

    public static void GetParent(int p, int q, out int parentP, out int parentQ, out bool isLeftChild)
    {
        if (p > q)
        {
            parentP = p - q;
            parentQ = q;
            isLeftChild = false;
        }
        else
        {
            parentP = p;
            parentQ = q - p;
            isLeftChild = true;
        }
    }
}
#endif
}
