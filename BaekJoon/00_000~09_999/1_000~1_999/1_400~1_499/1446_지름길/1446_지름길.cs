using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 22
이름 : 배성훈
내용 : 지름길
    문제번호 : 1446번

    다익스트라 문제다.
    다익스트라로 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1444
    {

        static void Main1444(string[] args)
        {

            int MAX = 10_000;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = ReadInt(), d = ReadInt();
            List<(int dst, int dis)>[] edge = new List<(int dst, int dis)>[MAX + 1];
            edge[0] = new(2 * n);
            int[] pos = new int[2 * n];

            for (int i = 0; i < n; i++)
            {

                int s = ReadInt();
                int e = ReadInt();
                int dis = ReadInt();

                pos[i * 2] = s;
                pos[i * 2 + 1] = e;

                if (e <= d)
                {

                    edge[s] ??= new();
                    edge[s].Add((e, dis)); 
                }
            }

            Array.Sort(pos);

            for (int i = 0; i < pos.Length; i++)
            {

                if (d <= pos[i]) continue;
                edge[0].Add((pos[i], pos[i]));
                edge[pos[i]] ??= new();
                edge[pos[i]].Add((d, d - pos[i]));

                for (int j = i + 1; j < pos.Length; j++)
                {

                    if (pos[j] <= pos[i] || d <= pos[j]) continue;
                    edge[pos[i]].Add((pos[j], pos[j] - pos[i]));
                }
            }
            
            PriorityQueue<(int dst, int dis), int> pq = new(36);
            int[] ret = new int[MAX + 1];
            Array.Fill(ret, -1);
            ret[0] = 0;
            pq.Enqueue((0, 0), 0);

            while (pq.Count > 0)
            {

                (int dst, int dis) node = pq.Dequeue();
                int cur = node.dst;
                if (ret[cur] < node.dis) continue;
                else if (cur == d) break;

                for (int i = 0; i < edge[cur].Count; i++)
                {

                    int next = edge[cur][i].dst;
                    int nDis = ret[cur] + edge[cur][i].dis;

                    if (ret[next] != -1 && ret[next] <= nDis) continue;
                    ret[next] = nDis;
                    pq.Enqueue((next, nDis), nDis);
                }
            }

            Console.Write(ret[d]);

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
namespace ShortCut
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(Console.OpenStandardInput());

            int[] inputs = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            int N = inputs[0];
            int D = inputs[1];

            List<int[]> shortCut = new List<int[]>();

            for (int i =0; i< N; i++)
            {
                shortCut.Add(Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse));
            }
            shortCut.Sort((x, y) => x[1].CompareTo(y[1]));

            int[] arr = new int[D + 1];

            int p = 1;
            int q = 0;

            while (shortCut[q][1] == 0) q++;

            while (p <= D)
            {
                arr[p] = arr[p - 1] + 1;
                while (q < shortCut.Count && shortCut[q][1] == p)
                {
                    arr[p] = Math.Min(arr[p], arr[shortCut[q][0]] + shortCut[q][2]);
                    q++;
                }
                p++;
            }

            Console.WriteLine(arr[D]);

        }
    }
}
#endif
}
