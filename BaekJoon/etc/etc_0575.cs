using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 19
이름 : 배성훈
내용 : 게임 개발
    문제번호 : 1516번

    위상 정렬, dp, 방향 비순환 그래프 문제다
    위상 정렬로 풀었다

    아이디어는 다음과 같다
    동시에 여러 건물을 지을 수 있으므로, 선행 건물 중 큰 값 + 자신을 짓는데 큰 값을 결과로 제출하면 된다
    선행 건물의 최대값은 add에 연산을 통해 찾고, 자신을 짓는데 걸리는 시간은 time에 입력받았다
*/

namespace BaekJoon.etc
{
    internal class etc_0575
    {

        static void Main575(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()));

            int n;
            Queue<int> q;

            int[] time;
            int[] degree;
            int[] add;
            List<int>[] line;

            Solve();

            sr.Close();
            sw.Close();

            void Solve()
            {

                Input();

                while(q.Count > 0)
                {

                    var node = q.Dequeue();

                    for (int i = 0; i < line[node].Count; i++)
                    {

                        int next = line[node][i];
                        degree[next]--;
                        add[next] = Math.Max(add[next], add[node] + time[node]);

                        if (degree[next] == 0) q.Enqueue(next);
                    }
                }

                for (int i = 1; i <= n; i++)
                {

                    sw.Write($"{time[i] + add[i]}\n");
                }
            }

            void Input()
            {

                n = ReadInt();

                q = new(n);

                time = new int[n + 1];
                degree = new int[n + 1];
                add = new int[n + 1];

                line = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    line[i] = new();
                }

                for (int i = 1; i <= n; i++)
                {

                    time[i] = ReadInt();
                    int cur = ReadInt();
                    while(cur != -1)
                    {

                        line[cur].Add(i);
                        degree[i]++;
                        cur = ReadInt();
                    }

                    if (degree[i] == 0) q.Enqueue(i);
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;
                while((c= sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if (c == '-')
                    {

                        plus = false;
                        continue;
                    }
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
var reader = new Reader();

int n = reader.NextInt();
var dura = new int[n + 1];
var req = new bool[n + 1, n + 1];
for (int i = 1; i <= n; i++)
{
    dura[i] = reader.NextInt();

    var p = -1;
    while ((p = reader.NextInt()) != -1)
        req[i, p] = true;
}

var duras = new int[n + 1];
Array.Fill(duras, -1);
for (int i = 1; i <= n; i++)
    GetCost(i);

Console.Write(string.Join("\n", duras.Skip(1)));

int GetCost(int num)
{
    if (duras[num] != -1)
        return duras[num];

    int max = 0;
    for (int i = 1; i <= n; i++)
        if (req[num, i] == true)
            max = Math.Max(max, GetCost(i));
    
    duras[num] = dura[num] + max;
    return duras[num];
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
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        int n;
        int[] timeCosts;
        List<int>[] successors;
        int[] predecessorMaxes;
        int[] requiredNums;
        Stack<int> stack = new();
        using (var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput())))
        {
            n = ScanSignedInt(sr);
            timeCosts = new int[n];
            successors = new List<int>[n];
            requiredNums = new int[n];
            predecessorMaxes = new int[n];
            for (int i = 0; i < n; i++)
                successors[i] = new();

            for (int i = 0; i < n; i++)
            {
                var num = ScanSignedInt(sr);
                timeCosts[i] = num;
                while ((num = ScanSignedInt(sr)) != -1)
                {
                    successors[num - 1].Add(i);
                    requiredNums[i]++;
                }
                if (requiredNums[i] == 0)
                    stack.Push(i);
            }
        }

        var wholeTimeCosts = new int[n];
        while (stack.TryPop(out var item))
        {
            var wholeCost = wholeTimeCosts[item] = timeCosts[item] + predecessorMaxes[item];
            foreach (var o in successors[item])
            {
                if (predecessorMaxes[o] < wholeCost)
                    predecessorMaxes[o] = wholeCost;
                if (--requiredNums[o] == 0)
                    stack.Push(o);
            }
        }

        var ret = new StringBuilder();
        ret.AppendJoin('\n', wholeTimeCosts);
        using (var sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            sw.Write(ret);
    }

    static int ScanSignedInt(StreamReader sr)
    {
        int c = sr.Read(), n = 0;
        if (c == '-')
            while (!((c = sr.Read()) is ' ' or '\n'))
            {
                if (c == '\r')
                {
                    sr.Read();
                    break;
                }
                n = 10 * n - c + '0';
            }
        else
        {
            n = c - '0';
            while (!((c = sr.Read()) is ' ' or '\n'))
            {
                if (c == '\r')
                {
                    sr.Read();
                    break;
                }
                n = 10 * n + c - '0';
            }
        }
        return n;
    }
}
#elif other3
using System;
using System.Text;
using System.Collections.Generic;
class MainApp
{
    static int[] indegree;
    static int[] Time;
    static int[] TimePlus;
    static StringBuilder sb = new StringBuilder();
    static List<List<int>> list = new List<List<int>>();
    static void Topology()
    {
        var queue = new Queue<int>();
        for (int i = 1; i < indegree.Length; i++)
        {
            if (indegree[i] == 0)
                queue.Enqueue(i);
        }
        while (queue.Count > 0)
        {
            int now = queue.Dequeue();
            for (int i = 0; i < list[now].Count; i++)
            {
                TimePlus[list[now][i]] = Math.Max(TimePlus[list[now][i]], Time[now]);
                if (--indegree[list[now][i]] == 0)
                {
                    queue.Enqueue(list[now][i]);
                    Time[list[now][i]] += TimePlus[list[now][i]];
                }
            }
        }
    }
    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        indegree = new int[N + 1];
        Time = new int[N + 1];
        TimePlus = new int[N + 1];
        for (int i = 0; i < N + 1; i++)
        {
            list.Add(new List<int>());
        }
        for (int i = 1; i < N + 1; i++)
        {
            string[] Input = Console.ReadLine().Split();
            Time[i] = int.Parse(Input[0]);
            for (int j = 1; j < Input.Length - 1; j++)
            {
                list[int.Parse(Input[j])].Add(i);
                indegree[i]++;
            }
        }
        Topology();
        for (int i = 1; i < Time.Length; i++)
        {
            sb.AppendLine((Time[i]).ToString());
        }
        Console.Write(sb);
    }
}
#endif
}
