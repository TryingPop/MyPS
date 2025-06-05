using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 5
이름 : 배성훈
내용 : 신촌 통폐합 계획
    문제번호 : 31423번

    연결리스트 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1677
    {

        static void Main1677(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);


            int n = ReadInt();
            string[] str = new string[n + 1];
            for (int i = 1; i <= n; i++)
            {

                str[i] = sr.ReadLine();
            }

            (int idx, int tail, int next, int len)[] node = new (int idx, int tail, int next, int len)[n + 1];
            for (int i = 1; i <= n; i++)
            {

                node[i] = (i, i, 0, 1);
            }

            for (int i = 1; i < n; i++)
            {

                int f = ReadInt();
                int t = ReadInt();

                Connect(f, t);
            }

            int idx = 0;
            for (int i = 1; i <= n; i++)
            {

                if (node[i].len == n)
                {

                    idx = i;
                    break;
                }
            }

            for (int i = 0; i < n; i++)
            {

                sw.Write(str[idx]);
                idx = node[idx].next;
            }

            void Connect(int _f, int _t)
            {

                int tail = node[_f].tail;
                node[tail].next = _t;
                node[_f].tail = node[_t].tail;
                node[_f].len += node[_t].len;
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

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
using System.IO;
using System.Linq;

// #nullable disable

public class Link
{
    public int Idx;
    public Link Prev;
    public Link Next;

    public Link(int idx)
    {
        Idx = idx;
    }

    public override string ToString()
    {
        return Idx.ToString();
    }
}

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var names = new string[n];
        var links = new (Link head, Link tail)[n];

        for (var idx = 0; idx < n; idx++)
        {
            names[idx] = sr.ReadLine();

            var l = new Link(idx);
            links[idx] = new(l, l);
        }

        var nonEmpty = Enumerable.Range(0, n).ToHashSet();

        for (var idx = 0; idx < n - 1; idx++)
        {
            var e = sr.ReadLine().Split(' ').Select(s => Int32.Parse(s) - 1).ToArray();

            var src = e[0];
            var dst = e[1];

            links[src].tail.Next = links[dst].head;
            links[src].tail.Next.Prev = links[src].tail;

            links[src] = (links[src].head, links[dst].tail);
            links[dst] = (null, null);
        }

        var root = links.Single(p => p.head != null).head;
        while (root != null)
        {
            sw.Write(names[root.Idx]);
            root = root.Next;
        }
    }
}

#endif
}
