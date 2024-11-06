using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 17
이름 : 배성훈
내용 : 열혈강호 2
    문제번호 : 11376번

    이분 매칭 문제다
    아이디어는 다음과 같다
    1명이 최대 2번의 일을 할 수 있으므로, 
    2명으로 분열시켜 이분매칭을 했다 시간은 3초대이다
    그리고 자기자신인 경우 돌아가는 현상을 취소시키니 1.5초대로 줄었다

    여담으로,
    다른 사람 풀이를 살펴보니, 호프크로프트-카프 알고리즘 (Hopcroft-Karp Algorithm) 을 썼고
    https://gazelle-and-cs.tistory.com/35
    에 내용이 있다

    가중치가 없는 그래프에서 가장 큰 매칭을 찾는 방법이고,
    시간은 O(n^2.5)이다 장점은 bipartite graph(이분 매치)이 아니어도 적용된다!

    해당 내용은 숙제(핀, 메모)로 남기고, 현재는 이분매치를 공부 중이므로
    당장은 넘긴다
*/

namespace BaekJoon.etc
{
    internal class etc_0556
    {

        static void Main556(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            int m = ReadInt();
            int[][] line = new int[n + 1][];

            int[] match = new int[m + 1];
            bool[] visit = new bool[m + 1];

            Solve();

            sr.Close();

            void Solve()
            {

                for (int i = 1; i <= n; i++)
                {

                    int len = ReadInt();
                    line[i] = new int[len];
                    for (int j = 0; j < len; j++)
                    {

                        line[i][j] = ReadInt();
                    }
                }

                int ret = 0;
                for (int i = 1; i <= 2 * n; i++)
                {

                    Array.Fill(visit, false);
                    if (DFS(i)) ret++;
                }

                Console.WriteLine(ret);
            }

            bool DFS(int _n)
            {

                int chk = (_n + 1) / 2;
                for (int i = 0; i < line[chk].Length; i++)
                {

                    int next = line[chk][i];
                    if (visit[next]) continue;
                    visit[next] = true;

                    // 자기의 일인가? 확인
                    // 내 일인 경우 끊으니 3초 -> 1.5초대로 확 줄었다
                    int chkNext = match[next];
                    if (((chkNext + 1) / 2) == chk) continue;

                    if (chkNext == 0 || DFS(match[next]))
                    {

                        match[next] = _n;
                        return true;
                    }
                }

                return false;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1  && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }

        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public enum GraphNodeType
{
    Source,
    Sink,
    Person,
    Book,
}

public record struct GraphNode(GraphNodeType NodeType, int Value);
public class Flow
{
    public int CurrFlow;
    public int MaxFlow;

    public bool CanFlow => CurrFlow < MaxFlow;

    public Flow(int currFlow, int maxFlow)
    {
        CurrFlow = currFlow;
        MaxFlow = maxFlow;
    }

    public override string ToString()
    {
        return $"{CurrFlow}/{MaxFlow}";
    }
}

public static class Program
{
    private static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var nm = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var n = nm[0];
        var m = nm[1];

        var graph = new Dictionary<GraphNode, Dictionary<GraphNode, Flow>>();
        var source = new GraphNode(GraphNodeType.Source, 0);
        var sink = new GraphNode(GraphNodeType.Sink, 0);

        for (var personId = 0; personId < n; personId++)
        {
            var person = new GraphNode(GraphNodeType.Person, personId);

            graph[source] = graph.GetValueOrDefault(source) ?? new Dictionary<GraphNode, Flow>();
            graph[source][person] = new Flow(0, 2);

            graph[person] = graph.GetValueOrDefault(person) ?? new Dictionary<GraphNode, Flow>();
            graph[person][source] = new Flow(0, 0);
        }

        for (var bookId = 1; bookId <= m; bookId++)
        {
            var book = new GraphNode(GraphNodeType.Book, bookId);

            graph[book] = graph.GetValueOrDefault(book) ?? new Dictionary<GraphNode, Flow>();
            graph[book][sink] = new Flow(0, 1);

            graph[sink] = graph.GetValueOrDefault(sink) ?? new Dictionary<GraphNode, Flow>();
            graph[sink][book] = new Flow(0, 0);
        }

        for (var personId = 0; personId < n; personId++)
        {
            var arr = sr.ReadLine().Split(' ').Select(Int32.Parse).Skip(1).ToArray();

            var person = new GraphNode(GraphNodeType.Person, personId);
            foreach (var bookId in arr)
            {
                var book = new GraphNode(GraphNodeType.Book, bookId);

                graph[person][book] = new Flow(0, 1);
                graph[book][person] = new Flow(0, 0);
            }
        }

        var maxflow = Dinic(graph, source, sink);
        sw.WriteLine(maxflow);
    }

    private static int Dinic(Dictionary<GraphNode, Dictionary<GraphNode, Flow>> graph, GraphNode source, GraphNode sink)
    {
        var maxflow = 0;
        var levelGraph = graph.ToDictionary(v => v.Key, _ => default(int?));
        var isDeadEnd = graph.ToDictionary(v => v.Key, _ => false);

        while (true)
        {
            var isMaxFlowChanged = false;

            foreach (var k in graph.Keys)
            {
                levelGraph[k] = default;
                isDeadEnd[k] = false;
            }

            BuildLevelGraph(graph, levelGraph, source, sink);

            while (true)
            {
                var flow = FindFlow(graph, levelGraph, isDeadEnd, source, sink, Int32.MaxValue);
                if (flow == 0)
                    break;

                isMaxFlowChanged = true;
                maxflow += flow;
            }

            if (!isMaxFlowChanged)
                break;
        }

        return maxflow;
    }

    private static int FindFlow(
        Dictionary<GraphNode, Dictionary<GraphNode, Flow>> graph,
        Dictionary<GraphNode, int?> levelGraph,
        Dictionary<GraphNode, bool> isDeadEnd,
        GraphNode currentNode,
        GraphNode sink,
        int minflow)
    {
        if (currentNode == sink)
            return minflow;

        foreach (var (dst, flow) in graph[currentNode])
        {
            if (!flow.CanFlow)
                continue;

            if (isDeadEnd[dst])
                continue;

            if (!levelGraph[currentNode].HasValue || !levelGraph[dst].HasValue)
                continue;

            if (levelGraph[currentNode].Value >= levelGraph[dst].Value)
                continue;

            var remainflow = flow.MaxFlow - flow.CurrFlow;
            var f = FindFlow(graph, levelGraph, isDeadEnd, dst, sink, Math.Min(remainflow, minflow));

            if (f != 0)
            {
                var ghost = graph[dst][currentNode];

                flow.CurrFlow += f;
                ghost.CurrFlow -= f;

                return f;
            }
        }

        isDeadEnd[currentNode] = true;
        return 0;
    }

    private static void BuildLevelGraph(Dictionary<GraphNode, Dictionary<GraphNode, Flow>> graph, Dictionary<GraphNode, int?> levelGraph, GraphNode source, GraphNode sink)
    {
        var q = new Queue<(GraphNode node, int level)>();
        q.Enqueue((source, 0));

        while (q.TryDequeue(out var s))
        {
            var (node, level) = s;

            if (levelGraph[node].HasValue)
                continue;

            levelGraph[node] = level;

            if (node == sink)
                continue;

            foreach (var (dst, flow) in graph[node])
                if (flow.CanFlow && !levelGraph[dst].HasValue)
                    q.Enqueue((dst, 1 + level));
        }
    }
}

#elif other2
using static IO;
public class IO{
public static IO Cin=new();
public static StreamReader reader=new(Console.OpenStandardInput());
public static StreamWriter writer=new(Console.OpenStandardOutput());
public static implicit operator string(IO _)=>reader.ReadLine();
public static implicit operator int(IO _)=>int.Parse(reader.ReadLine());
public static implicit operator string[](IO _)=>reader.ReadLine().Split();
public static implicit operator int[](IO _)=>Array.ConvertAll(reader.ReadLine().Split(),int.Parse);
public static implicit operator (int,int)(IO _){int[] a=Cin;return(a[0],a[1]);}
public static implicit operator (int,int,int)(IO _){int[] a=Cin;return(a[0],a[1],a[2]);}
public void Deconstruct(out int a,out int b){(int,int) r=Cin;(a,b)=r;}
public void Deconstruct(out int a,out int b,out int c){(int,int,int) r=Cin;(a,b,c)=r;}
public static object? Cout{set{writer.Write(value);}}
public static object? Coutln{set{writer.WriteLine(value);}}
public static void Main() {Program.Coding();writer.Flush();}
}
class Program {
    public static void Coding() {
        (int employee, int work) = Cin;
        //직원별 가능한 일 목록
        int[][] workable = new int[employee][];
        for(int i=0;i<employee;i++) {
            int[] input = Cin;
            workable[i] = new int[input[0]];
            for(int x=0;x<input[0];x++) {
                workable[i][x] = input[x+1]-1;
            }
        }
        //이분 매칭
        int?[] reserve = new int?[work];
        bool[] visited;
        bool dfs(int me,int exclude) {
            if (visited[me]) return false;
            visited[me] = true;
            foreach(var target in workable[me]) {
                if (target == exclude) continue;
                //남이 예약한 일이라면
                if (reserve[target] is int other) {
                    //근데 내꺼임?
                    if (other == me) continue; //그럼 다른거 찾아야지
                    //양보 ㄱㄴ?
                    if (dfs(other,target)) {
                        //ㄳ
                        reserve[target] = me;
                        return true;
                    }
                    //실패시 다른 일 찾아보기
                } else {
                    //아무도 안한 일이면
                    reserve[target] = me; //개꿀
                    return true;
                }
            }
            return false;
        }

        for(int x=0;x<employee;x++) {
            visited = new bool[employee];
            dfs(x,-1);
            visited = new bool[employee];
            dfs(x,-1);
        }
        Cout = reserve.Count(x => x is not null);
    }
}
#elif other3
using System;
using System.Collections.Generic;
class MainApp
{
    static List<List<int>> list = new List<List<int>>();
    static int[] d = new int[1001];
    static bool[] visited = new bool[1001];
    static int N;
    static bool DFS(int x)
    {
        for (int i = 0; i < list[x].Count; i++)
        {
            int y = list[x][i];
            if (visited[y]) continue;
            visited[y] = true;
            if (d[y] == 0 || DFS(d[y]))
            {
                d[y] = x;
                return true;
            }
        }
        return false;
    }
    static void Main(string[] args)
    {
        string[] Input = Console.ReadLine().Split();
        N = int.Parse(Input[0]);
        for (int i = 0; i < 1001; i++)
        {
            list.Add(new List<int>());
        }
        for (int i = 1; i <= N; i++)
        {
            string[] input = Console.ReadLine().Split();
            int A = int.Parse(input[0]);
            for (int j = 1; j <= A; j++)
            {
                list[i].Add(int.Parse(input[j]));
            }
        }
        int Count = 0;
        for (int j = 0; j < 2; j++)
        {
            for (int i = 1; i <= N; i++)
            {
                visited = new bool[1001];
                if (DFS(i)) Count++;
            }
        }
        Console.WriteLine(Count);
    }
}
#elif other4
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;
using System.Data;

namespace Algorithm
{
    class Program
    {
        static List<List<int>> graph;
        static int[] d;
        static bool[] vis;
        static bool DFS(int x)
        {
            for(int i = 0; i < graph[x].Count; i++)
            {
                int tmp = graph[x][i];
                if(vis[tmp])
                    continue;
                vis[tmp] = true;
                if(d[tmp] == 0 || DFS(d[tmp]))
                {
                    d[tmp] = x;
                    return true;
                }
            }
            return false;
        }
        static void Main()
        {
            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            
            string[] nm = sr.ReadLine().Split();
            int n = int.Parse(nm[0]);
            int m = int.Parse(nm[1]);
            graph = new List<List<int>>(){new List<int>()};
            d = new int[m+1];
            vis = new bool[m+1];
            int count = 0;
            for(int i = 0; i < n; i++)
            {
                List<int> input = Array.ConvertAll(sr.ReadLine().Split(), int.Parse).ToList();
                input.RemoveAt(0);
                graph.Add(input.ToList());
            }
            for(int i = 1; i <= n; i++)
            {
                vis = new bool[m+1];
                if(DFS(i))
                    count++;
                vis = new bool[m+1];
                if(DFS(i))
                    count++;
            }
            sb.Append(count);
            sw.WriteLine(sb);
            sr.Close();
            sw.Close(); 
        }
        static int LIS(List<int> seq)
        {
            List<double> lis = new List<double>();
            for(int i = 0; i< seq.Count(); i++)
            {
                int index = lis.BinarySearch(seq[i]);
                if(index < 0)
                    index = Math.Abs(index)-1;
                /*
                else
                {
                    lis[index]-=0.5f;
                    index++;
                }
                */
                if(index >= lis.Count())
                    lis.Add(seq[i]);
                else
                    lis[index] = seq[i];
            }
            return lis.Count();
        }
    }
}
#endif
}
