using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 21
이름 : 배성훈
내용 : 보석 도둑
    문제번호 : 1202번

    그리디, 정렬, 우선순위 큐 문제다
    아이디어가 안떠올라 다른 사람 풀이를 봤다
    주된 아이디어는 다음과 같다

    가방이 주어지면 가방의 무게보다 작은 보석들을 쓸어담는다
    그리고, 해당 보석 중 가장 값어치 있는 것을 찾고 해당 가방에 넣는다

    그래서 넣는 과정을 한 번에 해결하기 위해
    가방과 보석을 무게에 따라 정렬하고,
    이제 가방에 무게 이하의 보석들을 확인하며 중복안되게 넣어준다
    이후 가방 이하 무게의 보석들을 모두 넣었다면
    이제 최고 가치의 보석을 꺼내 해당 가방에 넣는다
    이렇게 제출하니 312ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0308
    {

        static void Main308(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int m = ReadInt(sr);

            Jewel[] jewel = new Jewel[n];

            for (int i = 0; i < n; i++)
            {

                jewel[i].Set(ReadInt(sr), ReadInt(sr));
            }

            int[] bag = new int[m];
            for (int i = 0; i < m; i++)
            {

                bag[i] = ReadInt(sr);
            }

            sr.Close();

            Array.Sort(jewel);
            Array.Sort(bag);

            PriorityQueue<int, int> q = new(n, Comparer<int>.Create((x, y) => y.CompareTo(x)));

            int idx = 0;
            long ret = 0;
            for (int i = 0; i < m; i++)
            {

                int cur = bag[i];
                while (idx < n && jewel[idx].w <= cur)
                {

                    int val = jewel[idx++].v;
                    q.Enqueue(val, val);
                }

                if (q.Count > 0)
                {

                    ret += q.Dequeue();
                }
            }

            Console.WriteLine(ret);
        }

        struct Jewel : IComparable<Jewel>
        {

            public int w;
            public int v;

            public void Set(int _w, int _v)
            {

                w = _w;
                v = _v;
            }

            public int CompareTo(Jewel other)
            {

                return w.CompareTo(other.w);
            }
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
using System;
using System.Collections.Generic;
using System.IO;

using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
int n = ScanInt(), k = ScanInt();

var jewels = new Jewel[n];
for (int i = 0; i < n; i++)
    jewels[i] = new(ScanInt(), ScanInt());
Array.Sort(jewels, (x, y) => x.Mass - y.Mass);

var bags = new int[k];
for (int i = 0; i < k; i++)
    bags[i] = ScanInt();
Array.Sort(bags);

long sum = 0;
var jIndex = 0;
var heap = new PriorityQueue<int, int>();
foreach (var c in bags)
{
    while (jIndex < n && jewels[jIndex].Mass <= c)
    {
        heap.Enqueue(jewels[jIndex].Value, -jewels[jIndex].Value);
        jIndex++;
    }

    if (heap.Count > 0)
        sum += heap.Dequeue();
}
Console.Write(sum);

int ScanInt()
{
    int c, n = 0;
    while (!((c = sr.Read()) is ' ' or '\n'))
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
record struct Jewel(int Mass, int Value);
#endif
}
