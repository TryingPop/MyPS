using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 21
이름 : 배성훈
내용 : 나무 위의 빗물
    문제번호 : 17073번

    수학, 트리 문제다
    정점의 개수를 찾는게 문제다

    BFS 탐색으로 리프를 찾았다
    그런데 리프 찾는데 더 좋은 방법이 있다
    이어진 간선의 개수를 세어서 리프를 찾는 것이다
*/

namespace BaekJoon.etc
{
    internal class etc_0309
    {

        static void Main309(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int w = ReadInt(sr);

#if first
            List<int>[] lines = new List<int>[n + 1];
            for (int i = 1; i <= n; i++)
            {

                lines[i] = new();
            }

            for (int i = 0; i < n - 1; i++)
            {

                int f = ReadInt(sr);
                int b = ReadInt(sr);

                lines[f].Add(b);
                lines[b].Add(f);
            }

            sr.Close();

            Queue<int> q = new(n);
            bool[] p = new bool[n + 1];
            q.Enqueue(1);
            p[1] = true;
            int cnt = 0;
            while(q.Count > 0)
            {

                // 정점에서 시작해서 자식들을 세어가며 찾는다
                var node = q.Dequeue();
                bool isLeaf = true;
                for (int i = 0; i < lines[node].Count; i++)
                {

                    int next = lines[node][i];
                    if (p[next]) continue;
                    p[next] = true;
                    isLeaf = false;
                    q.Enqueue(next);
                }

                if (isLeaf) cnt++;
            }

            if (cnt == 0) cnt = 1;

            double ret = w / (double)cnt;

            Console.WriteLine(ret);
#else

            int[] conn = new int[n + 1];
            for (int i = 1; i < n; i++)
            {

                int f = ReadInt(sr);
                int b = ReadInt(sr);

                // 간선의 개수를 센다
                conn[f]++;
                conn[b]++;
            }

            sr.Close();

            double cnt = 0;
            for (int i = 2; i <= n; i++)
            {

                if (conn[i] == 1) cnt++;
            }

            double ret = w / cnt;
            Console.WriteLine(ret);
#endif
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
def main():
    s = open(0, 'rb').read().split()
    N = int(s[0])
    W = int(s[1])
    a = bytearray(b'\x02') * N
    a[0] = 0
    for i in range(2, N * 2):
        a[int(s[i]) - 1] >>= 1
    print(W / a.count(1))
if __name__ == "__main__":
    main()
#elif other2
#nullable disable

using System;
using System.IO;
using System.Linq;

public static class FastIO
{
    private static bool IsWhitespace(int ch)
    {
        return ch == '\t' || ch == '\r' || ch == '\n' || ch == ' ';
    }

    public static int ReadPositiveInt(this StreamReader sr)
    {
        var v = sr.Read();
        while (IsWhitespace(v))
            v = sr.Read();

        var abs = v - '0';

        while (true)
        {
            v = sr.Read();

            if (v == -1 || IsWhitespace(v))
                break;
            else
                abs = abs * 10 + (v - '0');
        }

        return abs;
    }

    public static void WritePositiveInt(this StreamWriter sw, int value)
    {
        if (value == 0)
        {
            sw.Write('0');
            sw.Write('\n');
            return;
        }

        var divisor = 1;
        var valcopy = value;

        while (valcopy > 0)
        {
            valcopy /= 10;
            divisor *= 10;
        }

        divisor /= 10;

        while (divisor != 0)
        {
            var digit = (value / divisor) % 10;
            sw.Write((Char)('0' + digit));
            divisor /= 10;
        }

        sw.Write('\n');
    }
}

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        //var nw = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        //var n = nw[0];
        //var w = nw[1];
        var n = sr.ReadPositiveInt();
        var w = sr.ReadPositiveInt();

        var edgeCount = new int[1 + n];

        for (var idx = 0; idx < n - 1; idx++)
        {
            var src = sr.ReadPositiveInt();
            var dst = sr.ReadPositiveInt();

            edgeCount[src]++;
            edgeCount[dst]++;
        }

        var leafNodeCount = edgeCount.Skip(2).Count(v => v == 1);
        sw.WriteLine((double)w / leafNodeCount);
    }
}

#endif
}
