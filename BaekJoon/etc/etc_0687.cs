using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 12
이름 : 배성훈
내용 : ACM Craft
    문제번호 : 1005번

    dp, 위상정렬 문제다
    초기화 문제로 54%에서 2번 틀렸다
    모든 건물을 지을 수 있다고 했으므로 간선에 사이클은 존재안한다!

    우선 순위 큐를 이용해 정렬해 풀었는데, 범위가 작아 필수는 아니다
    매번 시간을 큰 값으로 갱신만 해주면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0687
    {

        static void Main687(string[] args)
        {

            int LEN = 1_000;

            StreamReader sr;
            StreamWriter sw;

            int[] time;
            int[] degree;
            int[] ret;

            List<int>[] line;

            int n, m;
            int find;

            PriorityQueue<int, int> q;

            Solve();

            void Solve()
            {

                Init();
                int test = ReadInt();

                while(test-- > 0)
                {

                    Input();

                    for (int i = 1; i <= n; i++)
                    {

                        if (degree[i] != 0) continue;
                        ret[i] = time[i];
                        q.Enqueue(i, ret[i]);
                    }

                    while(q.Count > 0)
                    {

                        int node = q.Dequeue();
                        for (int i = 0; i < line[node].Count; i++)
                        {

                            int next = line[node][i];
                            degree[next]--;
                            if (degree[next] != 0) continue;

                            ret[next] = ret[node] + time[next];
                            q.Enqueue(next, ret[next]);
                        }
                    }

                    sw.Write($"{ret[find]}\n");
                }

                sr.Close();
                sw.Close();
            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();

                for (int i = 1; i <= n; i++)
                {

                    time[i] = ReadInt();
                    line[i].Clear();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    line[f].Add(b);
                    degree[b]++;
                }

                find = ReadInt();
            }
            
            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                time = new int[LEN + 1];
                degree = new int[LEN + 1];
                ret = new int[LEN + 1];

                line = new List<int>[LEN + 1];
                for (int i = 1; i <= LEN; i++)
                {

                    line[i] = new();
                }

                q = new(LEN);
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
int t = reader.NextInt();

int n = 0;
bool[,] req;
int[] dura;
int[] temp;

using (var w = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
while (t-- > 0)
{
    n = reader.NextInt();
    int k = reader.NextInt();
    dura = new int[n + 1];
    for (int i = 1; i <= n; i++)
        dura[i] = reader.NextInt();

    req = new bool[n + 1, n + 1];
    while (k-- > 0)
    {
        var (x, y) = (reader.NextInt(), reader.NextInt());
        req[y, x] = true;
    }

    var target = reader.NextInt();
    temp = new int[n + 1];
    Array.Fill(temp, -1);
    w.WriteLine(GetCost(target));
}

int GetCost(int num)
{
    if (temp[num] != -1)
        return temp[num];

    int max = 0;
    for (int i = 1; i <= n; i++)
        if (req[num, i] == true)
            max = Math.Max(max, GetCost(i));
    
    temp[num] = dura[num] + max;
    return temp[num];
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

    public long NextLong()
    {
        bool negative = false;
        bool reading = false;

        long value = 0;
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
using System.Collections.Generic;
using System.IO;

using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
using var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
var t = ScanInt();
for (int i = 0; i < t; i++)
{
    int n = ScanInt(), k = ScanInt();
    var costs = new int[n + 1];
    for (int j = 1; j <= n; j++)
    {
        costs[j] = ScanInt();
    }

    var requirements = new List<int>[n + 1];
    for (int j = 1; j <= n; j++)
    {
        requirements[j] = new();
    }
    for (int j = 0; j < k; j++)
    {
        int require = ScanInt(), achievement = ScanInt();
        requirements[achievement].Add(require);
    }
    var w = ScanInt();
    var totalCosts = new int[n + 1];
    for (int j = 1; j < totalCosts.Length; j++)
    {
        totalCosts[j] = -1;
    }
    var ret = GetCost(w);
    sw.WriteLine(ret);

    int GetCost(int item)
    {
        ref var output = ref totalCosts[item];
        if (output == -1)
        {
            var max = 0;
            foreach (var o in requirements[item])
                max = Math.Max(GetCost(o), max);
            output = max + costs[item];
        }
        return output;
    }
}

int ScanInt()
{
    int c, n = 0;
    while (!((c = sr.Read()) is ' ' or '\n' or -1))
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
#endif
}
