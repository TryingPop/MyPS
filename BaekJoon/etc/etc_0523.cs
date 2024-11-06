using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 13
이름 : 배성훈
내용 : 특정 거리의 도시 찾기
    문제번호 : 18352번

    다익스트라, BFS, 최단 경로 문제다
    우선 간선의 길이가 모두 1로 동일해 우선순위 큐가 아닌 
    일반 큐를 써서 풀어도 된다
    그래서 제출하니 이상없이 통과했다
    다만 케이스가 많아 480ms 걸린다
*/

namespace BaekJoon.etc
{
    internal class etc_0523
    {

        static void Main523(string[] args)
        {

            // 입력
            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 8);
            int n = ReadInt();
            int m = ReadInt();

            int k = ReadInt();
            int x = ReadInt();

            List<int>[] line = new List<int>[n + 1];

            for (int i = 1; i <= n; i++)
            {

                line[i] = new();
            }

            for (int i = 0; i < m; i++)
            {

                int f = ReadInt();
                int b = ReadInt();

                line[f].Add(b);
            }

            sr.Close();

            // BFS
            bool[] visit = new bool[n + 1];
            int[] dis = new int[n + 1];
            BFS(x);

            // 출력
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 65536);

            bool impo = true;
            for (int i = 1; i <= n; i++)
            {

                if (k != dis[i]) continue;
                impo = false;
                sw.Write(i);
                sw.Write('\n');
            }

            if (impo) sw.Write(-1);

            sw.Close();


            void BFS(int _s)
            {

                Queue<int> q = new(n);
                q.Enqueue(_s);
                visit[_s] = true;

                while (q.Count > 0)
                {

                    int node = q.Dequeue();

                    for (int i = 0; i < line[node].Count; i++)
                    {

                        int next = line[node][i];
                        if (visit[next]) continue;
                        visit[next] = true;
                        dis[next] = dis[node] + 1;
                        q.Enqueue(next);
                    }
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
var reader = new Reader();
int n = reader.NextInt();
int m = reader.NextInt();
int k = reader.NextInt();
int x = reader.NextInt();

var road = new List<int>[n + 1];
while (m-- > 0)
{
    var (a, b) = (reader.NextInt(), reader.NextInt());
    (road[a] ??= new List<int>()).Add(b);
}

var queue = new Queue<int>();
var dist = new int[n + 1];
Array.Fill(dist, -1);

queue.Enqueue(x);
dist[x] = 0;

var result = new List<int>();
while (queue.Count > 0)
{
    var c = queue.Dequeue();

    if (dist[c] == k)
    {
        result.Add(c);
        continue;
    }

    if (road[c] == null)
        continue;

    foreach (var r in road[c])
    {
        if (dist[r] != -1)
            continue;

        queue.Enqueue(r);
        dist[r] = dist[c] + 1;
    }
}

if (result.Count == 0)
{
    Console.Write(-1);
    return;
}

using (var writer = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
foreach (var d in result.OrderBy(x => x))
    writer.WriteLine(d);

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
int cityNum, roadNum, distance, start;
List<int>[] roads;
using (var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput())))
{
    cityNum = ScanInt();
    roadNum = ScanInt();
    distance = ScanInt();
    start = ScanInt();
    roads = new List<int>[cityNum + 1];
    for (int i = 1; i < roads.Length; i++)
        roads[i] = new();

    for (int i = 0; i < roadNum; i++)
    {
        int a = ScanInt(), b = ScanInt();
        roads[a].Add(b);
    }

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
}

var q = new Queue<int>();
{
    q.Enqueue(start);
    var visited = new bool[cityNum + 1];
    visited[start] = true;
    var newQ = new Queue<int>();
    var dist = 0;

    do
    {
        while (q.TryDequeue(out var o))
        {
            foreach (var dest in roads[o])
            {
                if (!visited[dest])
                {
                    visited[dest] = true;
                    newQ.Enqueue(dest);
                }
            }
        }

        (q, newQ) = (newQ, q);
    } while (++dist < distance && q.Count > 0);
}

if (q.Count > 0)
{
    using var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

    foreach (var o in q.OrderBy(o => o))
        sw.WriteLine(o);
}
else Console.Write("-1");
#endif
}
