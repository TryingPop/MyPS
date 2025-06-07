using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 5
이름 : 배성훈
내용 : *빛*영*우*
    문제번호 : 15807번

    누적합 문제다.
    누적합으로 접근이 안되 세그먼트 트리로 풀었다.
    우선 빛에 포함되는 좌표는 x + y의 값이 크고 동시에 x - y 의 값이 작아야 한다.
    그래서 좌표를 a = y - x, b = x + y로 바꾼다.
    그러면 영우가 출몰하는 곳의 a, b좌표를 c, d라 하면
    c보다 작은 값을 갖고 d보다 작은 값을 갖는 a, b좌표에 있는 스포트 라이트가 된다.

    이에 영우의 장소와 라이트를 한대 묶어 a를 기준으로 정렬하고, a가 같은경우 스포트라이트가 먼저오게 정렬한다.
    그리고 해당 배열을 하나씩 읽는데 스포트 라이트 b의 값을 세그먼트 트리에 누적되게 저장한다.
    영우가 나오는 장소가 나올 때는 해당 b보다 작거나 같은 스포트라이트의 갯수가 해당 장소를 비추는 스포트라이트의 갯수가 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1519
    {

        static void Main1519(string[] args)
        {

            int OFFSET = 3_000;

            int n, m;
            List<(int a, int b, int op)> queries;
            int[] seg;
            int bias;

            Input();

            SetSeg();

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int[] ret = new int[m];

                for (int i = 0; i < queries.Count; i++)
                {

                    if (queries[i].op == -1)
                        Update(queries[i].b);
                    else
                    {

                        ret[queries[i].op] = GetVal(queries[i].b);
                    }
                }

                for (int i = 0; i < m; i++)
                {

                    sw.Write($"{ret[i]}\n");
                }
            }

            void Update(int _chk)
            {

                int idx = _chk | bias;
                seg[idx]++;

                while (idx > 1)
                {

                    idx >>= 1;
                    seg[idx]++;
                }
            }

            int GetVal(int _val)
            {

                int l = bias;
                int r = bias | _val;

                int ret = 0;
                while (l < r)
                {

                    if ((r & 1) == 0) ret += seg[r--];
                    r >>= 1;
                    l >>= 1;
                }

                if (l == r) ret += seg[l];
                return ret;
            }

            void SetSeg()
            {

                seg = new int[(1 << 14) + 1];
                bias = 1 << 13;
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                queries = new(200_000);
                for (int i = 0; i < n; i++)
                {

                    int x = ReadInt();
                    int y = ReadInt();
                    queries.Add((y - x, x + y + OFFSET, -1));
                }

                m = ReadInt();
                for (int i = 0; i < m; i++)
                {

                    int x = ReadInt();
                    int y = ReadInt();

                    queries.Add((y - x, x + y + OFFSET, i));
                }

                queries.Sort((x, y) =>
                {

                    int ret = x.a.CompareTo(y.a);
                    if (ret == 0) ret = x.op.CompareTo(y.op);
                    return ret;
                });

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == ' ' || c == '\n') return true;
                        bool positive = c != '-';
                        ret = positive ? c - '0' : 0;

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        ret = positive ? ret : -ret;
                        return false;
                    }
                }
            }
        }
    }

#if other
using System;
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
        var mapsize = 6030;
        var map = new int[mapsize, mapsize];

        while (n-- > 0)
        {
            var xy = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var (y, x) = Transform(xy[1], xy[0]);

            map[y, x]++;
        }

        for (var y = 0; y < mapsize; y++)
            for (var x = 1; x < mapsize; x++)
                map[y, x] += map[y, x - 1];

        for (var y = 1; y < mapsize; y++)
            for (var x = 0; x < mapsize; x++)
                map[y, x] += map[y - 1, x];

        var m = Int32.Parse(sr.ReadLine());
        while (m-- > 0)
        {
            var xy = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var (y, x) = Transform(xy[1], xy[0]);
            sw.WriteLine(map[y, x]);
        }
    }

    public static (int y, int x) Transform(int y, int x)
    {
        return (y - x + 3010, y + x + 3010);
    }
}

#endif
}
