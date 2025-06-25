using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 23
이름 : 배성훈
내용 : 버스 노선 개편하기
    문제번호 : 23740번

    정렬, 스위핑 문제다.
    정렬하고 가장 앞서는거부터 끝을 비교하며 합치면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1568
    {

        static void Main1568(string[] args)
        {

            int n;
            (int s, int e, int c, bool isAlive)[] bus;

            Input();

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                Array.Sort(bus, (x, y) => x.s.CompareTo(y.s));

                ref (int s, int e, int c, bool isAlive) cur = ref bus[0];
                int ret1 = n;

                for (int i = 1; i < n; i++)
                {

                    if (bus[i].s <= cur.e)
                    {

                        bus[i].isAlive = false;
                        ret1--;

                        if (cur.e < bus[i].e) cur.e = bus[i].e;
                        if (bus[i].c < cur.c) cur.c = bus[i].c;
                    }
                    else cur = ref bus[i];
                }

                sw.Write($"{ret1}\n");
                for (int i = 0; i < n; i++)
                {

                    if (bus[i].isAlive) sw.Write($"{bus[i].s} {bus[i].e} {bus[i].c}\n");
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                bus = new (int s, int e, int c, bool isAlive)[n];

                for (int i = 0; i < n; i++)
                {

                    bus[i] = (ReadInt(), ReadInt(), ReadInt(), true);
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
    }

#if other
StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

int N = int.Parse(sr.ReadLine());
List<(int s, int e, int w)> lines = new();

for(int i=0; i<N; i++)
{
    int[] inputs = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
    lines.Add((inputs[0], inputs[1], inputs[2]));
}

lines.Sort();

int prev = 0;
int count = 0;
for (int i=1; i<N; i++)
{
    if(lines[prev].e < lines[i].s)
    {
        prev = i;
        count++;
        continue;
    }
    else
    {
        lines[prev] = (lines[prev].s, Math.Max(lines[prev].e, lines[i].e), Math.Min(lines[prev].w, lines[i].w));
        lines[i] = (-1, -1, -1);
    }
}
sw.WriteLine(count + 1);
for(int i=0; i<N; i++)
    if(lines[i].w >= 0)
        sw.WriteLine(lines[i].s + " " + lines[i].e + " " + lines[i].w);

sr.Close();
sw.Close();
#endif
}
