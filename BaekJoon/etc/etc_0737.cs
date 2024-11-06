using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 29
이름 : 배성훈
내용 : 어린 왕자
    문제번호 : 1004번

    수학, 기하학 문제다

    아이디어는 간단하다
    원안에 시작점과 끝점이 함께 포함되거나 안되는 경우 진입, 탈출은 필요없다
    이외는 진입, 탈출이 1회 필요하다
*/

namespace BaekJoon.etc
{
    internal class etc_0737
    {

        static void Main737(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int sx, sy;
            int ex, ey;

            Solve();

            void Solve()
            {

                Init();

                int test = ReadInt();
                while(test-- > 0)
                {

                    Input();

                    int ret = 0;
                    int len = ReadInt();
                    for (int i = 0; i < len; i++)
                    {

                        ret += Chk();
                    }

                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            }

            void Input()
            {

                sx = ReadInt();
                sy = ReadInt();

                ex = ReadInt();
                ey = ReadInt();
            }

            int Chk()
            {

                int x = ReadInt();
                int y = ReadInt();

                int r = ReadInt();

                bool chk1 = IsIn(sx, sy, x, y, r);
                bool chk2 = IsIn(ex, ey, x, y, r);

                return chk1 == chk2 ? 0 : 1;
            }

            bool IsIn(int _x1, int _y1, int _x2, int _y2, int _r)
            {

                int x = _x1 - _x2;
                x *= x;

                int y = _y1 - _y2;
                y *= y;
                return x + y < _r * _r;
            }

            int ReadInt()
            {

                int c = sr.Read();

                if (c == -1) return 0;
                bool plus = c != '-';

                int ret = plus ? c - '0' : 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
var reader = new Reader();

var t = reader.NextInt();
using (var writer = new StreamWriter(Console.OpenStandardOutput()))
while (t-- > 0)
{
    var (sX, sY) = (reader.NextInt(), reader.NextInt());
    var (eX, eY) = (reader.NextInt(), reader.NextInt());

    int n = reader.NextInt();
    int cnt = 0;
    while (n-- > 0)
    {
        var (x, y, r) = (reader.NextInt(), reader.NextInt(), reader.NextInt());
        bool sI = Math.Sqrt((x - sX) * (x - sX) + (y - sY) * (y - sY)) < r;
        bool eI = Math.Sqrt((x - eX) * (x - eX) + (y - eY) * (y - eY)) < r;

        if (sI ^ eI)
            cnt++;
    }

    writer.WriteLine(cnt);
}

class DisjointSet
{
    private int size;
    private int[] parent;
    private int[] setSize;
    private int[] setValue;

    public DisjointSet(int capacity, int[] values)
    {
        this.size = capacity;
        this.parent = new int[capacity];
        this.setSize = new int[capacity];
        this.setValue = new int[capacity];

        for (int i = 0; i < size; i++)
        {
            this.parent[i] = i;
            this.setSize[i] = 1;
            this.setValue[i] = values[i];
        }
    }

    public int Find(int node)
    {
        if (node == this.parent[node])
            return node;

        var result = this.Find(parent[node]);
        parent[node] = result;
        setSize[node] = setSize[result];
        setValue[node] = setValue[result];

        return result;
    }

    public void Union(int n, int m)
    {
        int nRep = this.Find(n);
        int mRep = this.Find(m);

        if (nRep != mRep)
        {
            int size = this.setSize[nRep] + this.setSize[mRep];
            setSize[nRep] = size;
            setSize[mRep] = size;

            int value = this.setValue[nRep] + this.setValue[mRep];
            setValue[nRep] = value;
            setValue[mRep] = value;
        }

        parent[nRep] = mRep;
    }

    public int GetSetSize(int n) => setSize[this.Find(n)];

    public int GetSetValue(int n) => setValue[this.Find(n)];
}

class Reader
{
    StreamReader reader;

    public Reader()
    {
        BufferedStream stream = new(Console.OpenStandardInput());
        reader = new(stream);
    }

    public int NextInt()
    {
        bool negative = false;
        bool reading = false;

        int value = 0;
        while (true)
        {
            int c = reader.Read();

            if (reading == false && c == '-')
            {
                negative = true;
                reading = true;
                continue;
            }

            if ('0' <= c && c <= '9')
            {
                value = value * 10 + (c - '0');
                reading = true;
                continue;
            }

            if (reading == true)
                break;
        }

        return negative ? -value : value;
    }
}
#elif other2
using System;
using System.IO;
using static System.Console;

StreamWriter sw = new(new BufferedStream(OpenStandardOutput()));
var inp = new int[4];
var t = int.Parse(ReadLine());
while (t-- > 0)
{
    var line = ReadLine();
    var last = 0;
    var index = 0;
    for (int i = 0; i < line.Length; i++)
    {
        if (line[i] == ' ')
        {
            inp[index++] = int.Parse(line.AsSpan(last, i-last));
            last = i + 1;
        }
    }
    inp[index] = int.Parse(line.AsSpan(last, line.Length - last));

    (var x1, var y1, var x2, var y2) = (inp[0], inp[1], inp[2], inp[3]);
    var result = 0;
    var n = int.Parse(ReadLine());
    while (n-- > 0)
    {
        line = ReadLine();
        index = 0;
        last = 0;
        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] == ' ')
            {
                inp[index++] = int.Parse(line.AsSpan(last, i-last));
                last = i + 1;
            }
        }
        inp[index] = int.Parse(line.AsSpan(last, line.Length - last));

        var crsq = inp[2] * inp[2];
        (var xd1, var yd1) = (inp[0] - x1, inp[1] - y1);
        (var xd2, var yd2) = (inp[0] - x2, inp[1] - y2);
        if (xd1 * xd1 + yd1 * yd1 < crsq ^
            xd2 * xd2 + yd2 * yd2 < crsq)
            result++;
    }
    sw.WriteLine(result);
}
sw.Close();
#endif
}
