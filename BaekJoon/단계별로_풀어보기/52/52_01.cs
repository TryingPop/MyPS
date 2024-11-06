using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 29
이름 : 배성훈
내용 : 열혈강호 5
    문제번호 : 11408번

    최대 유량, 최소 비용 최대 유량 문제다
    MCMF, 최소 비용 최대 유량을 찾아야한다
    찾아보니 SPFA (shortest path faster algorithm)을 쓴다고 한다

    코드는 해당 사이트 참고했다
    https://kibbomi.tistory.com/48

    반면 아이디어는 여기를 참고했다
    https://m.blog.naver.com/kks227/220810623254

    아이디어는 다음과 같다
    MF로 최대 이분 매칭을 찾는다
    그리고 이분 매칭에서 벨만 포드 방법으로 최소 비용으로 이어간다
*/

namespace BaekJoon._52
{
    internal class _52_01
    {

        static void Main1(string[] args)
        {

            int INF = 10_000_000;

            StreamReader sr;
            int n, m;

            int[,] c, f, d;
            Queue<int> q;
            List<int>[] line;
            int source, sink;
            int[] dis, before;
            bool[] inQ;

            int ret1, ret2;
            Solve();

            void Solve()
            {

                Input();

                Init();

                MCMF();

                Console.Write($"{ret1}\n{ret2}");
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                source = 0;
                sink = n + m + 1;
                line = new List<int>[sink + 1];
                for (int i = 0; i <= sink; i++)
                {

                    line[i] = new();
                }

                c = new int[sink + 1, sink + 1];        // MF 에서 최대 유량 - 1
                f = new int[sink + 1, sink + 1];        // MF 에서 현재 흐르는 유량
                d = new int[sink + 1, sink + 1];        // MC 에 쓰이는 실제 거리 

                for (int i = 1; i <= n; i++)
                {

                    int len = ReadInt();

                    for (int j = 0; j < len; j++)
                    {

                        int dst = n + ReadInt();
                        int dis = ReadInt();

                        line[i].Add(dst);
                        line[dst].Add(i);
                        c[i, dst] = 1;                  // 이분 매칭용

                        d[i, dst] = dis;
                        d[dst, i] = -dis;               // 역방향에서는 -거리로 가정한다
                                                        // 에드먼드 카프 알고리즘으로 MF를 구하기에
                                                        // 최적화 경로를 찾을 때 역방향 탐색을 한다
                    }
                }

                sr.Close();
            }

            void Init()
            {

                q = new(sink + 1);

                for (int i = 1; i <= n; i++)
                {

                    line[source].Add(i);
                    line[i].Add(source);

                    c[source, i] = 1;
                }

                for (int i = n + 1; i <= n + m; i++)
                {

                    line[i].Add(sink);
                    line[sink].Add(i);

                    c[i, sink] = 1;
                }

                dis = new int[sink + 1];
                before = new int[sink + 1];
                inQ = new bool[sink + 1];
            }

            void MCMF()
            {

                ret1 = 0;
                ret2 = 0;
                while (true)
                {

                    Array.Fill(before, -1);
                    Array.Fill(dis, INF);
                    Array.Fill(inQ, false);
                    dis[source] = 0;

                    q.Enqueue(source);
                    inQ[source] = true;
                    before[source] = source;

                    while(q.Count > 0)
                    {

                        int node = q.Dequeue();

                        // 큐안에 있는지 확인
                        // 중복으로 넣는거 방지..?
                        inQ[node] = false;

                        for (int i = 0; i < line[node].Count; i++)
                        {

                            int next = line[node][i];
                            int nDis = d[node, next];

                            // 유량이 더 흐를 수 잇고, 최단 경로 갱신인지 확인
                            if (c[node, next] - f[node, next] > 0 && dis[next] > dis[node] + nDis)
                            {

                                dis[next] = dis[node] + nDis;
                                before[next] = node;

                                if (!inQ[next])
                                {

                                    inQ[next] = true;
                                    q.Enqueue(next);
                                }
                            }
                        }
                    }

                    // 유량이 흐르는 최단 경로가 더 이상 존재 X
                    if (before[sink] == -1) break;

                    int flow = INF;
                    for (int i = sink; i != source; i = before[i])
                    {

                        flow = Math.Min(flow, c[before[i], i] - f[before[i], i]);
                    }

                    for (int i = sink; i != source; i = before[i])
                    {

                        ret2 += flow * d[before[i], i];         // 찾은 경로 

                        f[before[i], i] += flow;
                        f[i, before[i]] -= flow;
                    }

                    ret1++;
                }
            }


            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' '&& c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// #nullable disable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public enum GraphNodeType
{
    Source,
    Sink,
    Man,
    Work,
}

public record struct GraphNode(GraphNodeType type, int arg);

public class Flow
{
    public int CurrFlow;
    public int MaxFlow;
    public int CostPerFlow;

    public Flow(int maxFlow, int costPerFlow)
    {
        MaxFlow = maxFlow;
        CostPerFlow = costPerFlow;
    }

    public bool CanFlow => CurrFlow < MaxFlow;

    public override string ToString() => $"{CurrFlow}/{MaxFlow}";
}

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var nm = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var (n, m) = (nm[0], nm[1]);

        var flowgraph = new Dictionary<GraphNode, Dictionary<GraphNode, Flow>>();

        var source = new GraphNode(GraphNodeType.Source, 0);
        var sink = new GraphNode(GraphNodeType.Sink, 0);

        for (var idx = 0; idx < n; idx++)
            AddFlow(flowgraph, source, new GraphNode(GraphNodeType.Man, idx), 0, 1);

        for (var idx = 0; idx < m; idx++)
            AddFlow(flowgraph, new GraphNode(GraphNodeType.Work, idx), sink, 0, 1);

        var rd = new Random();
        for (var idx = 0; idx < n; idx++)
        {
            var l = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            //var l = Enumerable
            //    .Range(1, m)
            //    .OrderBy(_ => rd.Next())
            //    .Take(rd.Next(0, 1 + m))
            //    .SelectMany(v => new[] { v, rd.Next(0, 100) })
            //    .Prepend(-1)
            //    .ToArray();

            for (var offset = 1; offset < l.Length; offset += 2)
            {
                var target = l[offset];
                var cost = l[offset + 1];

                AddFlow(flowgraph, new GraphNode(GraphNodeType.Man, idx), new GraphNode(GraphNodeType.Work, target - 1), cost, 1);
            }
        }

        var graphNodeToId = new Dictionary<GraphNode, int>();
        foreach (var node in flowgraph.Keys)
            graphNodeToId[node] = graphNodeToId.Count;

        var flowsum = 0L;
        var costsum = 0L;

        var flowgraphArr = new Flow[graphNodeToId.Count, graphNodeToId.Count];
        var graphArr = new List<int>[graphNodeToId.Count];

        for (var idx = 0; idx < graphNodeToId.Count; idx++)
            graphArr[idx] = new List<int>();

        foreach (var src in flowgraph.Keys)
            foreach (var (dst, flow) in flowgraph[src])
            {
                flowgraphArr[graphNodeToId[src], graphNodeToId[dst]] = flow;
                graphArr[graphNodeToId[src]].Add(graphNodeToId[dst]);
            }

        var q = new Queue<int>();
        var dist = new (int costSum, int prev)?[graphNodeToId.Count];
        var inq = new bool[graphNodeToId.Count];

        var sourceId = graphNodeToId[source];
        var sinkId = graphNodeToId[sink];

        while (true)
        {
            q.Clear();
            Array.Clear(dist);
            Array.Clear(inq);

            dist[sourceId] = (0, sourceId);
            inq[sourceId] = true;
            q.Enqueue(sourceId);

            while (q.TryDequeue(out var src))
            {
                inq[src] = false;

                foreach (var dst in graphArr[src])
                {
                    var flow = flowgraphArr[src, dst];

                    if (flow.CanFlow)
                    {
                        var newcost = dist[src].Value.costSum + flow.CostPerFlow;

                        if (dist[dst] == null || newcost < dist[dst].Value.costSum)
                        {
                            dist[dst] = (newcost, src);

                            if (!inq[dst])
                            {
                                inq[dst] = true;
                                q.Enqueue(dst);
                            }
                        }
                    }
                }
            }

            if (dist[sinkId] == null)
                break;

            var curr = sinkId;
            while (curr != sourceId)
            {
                var prev = dist[curr].Value.prev;
                flowgraphArr[prev, curr].CurrFlow++;
                flowgraphArr[curr, prev].CurrFlow--;

                costsum += flowgraphArr[prev, curr].CostPerFlow;
                curr = prev;
            }

            flowsum++;
        }

        sw.WriteLine(flowsum);
        sw.WriteLine(costsum);
    }

    private static void AddFlow(
        Dictionary<GraphNode, Dictionary<GraphNode, Flow>> flowgraph,
        GraphNode src,
        GraphNode dst,
        int cost,
        int flow)
    {
        if (!flowgraph.ContainsKey(src))
            flowgraph.Add(src, new Dictionary<GraphNode, Flow>());
        if (!flowgraph.ContainsKey(dst))
            flowgraph.Add(dst, new Dictionary<GraphNode, Flow>());

        flowgraph[src][dst] = new Flow(flow, cost);
        flowgraph[dst][src] = new Flow(0, -cost);
    }
}
#elif other2
using System;
using System.Collections.Generic;
using System.Linq;

namespace 열혈강호_5 {
    class Edge {
        public int Source { get; }
        public int Destination { get; }
        public int Flow { get; private set; }
        public int Cost { get; private set; }
        public int Capacity { get; private set; }
        public Edge Opposite { get; set; }

        public int Spare => Capacity - Flow;

        public Edge(int src, int dst, int cost, int capa) {
            Source = src;
            Destination = dst;
            Flow = 0;
            Cost = cost;
            Capacity = capa;
        }

        public void AddFlow(int add) {
            Flow += add;
            if (Opposite != null) {
                Opposite.Flow -= add;
            }
        }
    }

    public record MCMF {
        public int Flowed { get; set; }
        public int TotalCost { get; set; }
    }

    class Graph {
        public static readonly int Inf = int.MaxValue/2;
        private List<Edge>[] graph;
        
        public Graph(int vertexCount) {
            graph = new List<Edge>[vertexCount];
            for (int i = 0; i < vertexCount; ++i) {
                graph[i] = new List<Edge>();
            }
        }

        public void AddEdge(int src, int dst, int cost, int capa) {
            Edge curr = new Edge(src, dst, cost, capa);
            Edge oppo = new Edge(dst, src, -cost, 0);
            curr.Opposite = oppo;
            oppo.Opposite = curr;

            graph[src].Add(curr);
            graph[dst].Add(oppo);
        }

        private List<Edge> FindAugPath(int src, int dst) {
            Edge[] prev = new Edge[graph.Length];
            int[] dist = new int[graph.Length];
            bool[] inQueue = new bool[graph.Length];
            Array.Fill(dist, Inf);
            Queue<int> q = new();

            dist[src] = 0;
            inQueue[src] = true;
            q.Enqueue(src);

            while (q.Count > 0) {
                int u = q.Dequeue();
                inQueue[u] = false;
                
                foreach (Edge e in graph[u]) {
                    if (e.Spare <= 0) {
                        continue;
                    }
                    int v = e.Destination;
                    if (dist[u] + e.Cost < dist[v]) {
                        dist[v] = dist[u] + e.Cost;
                        prev[v] = e;
                        if (!inQueue[v]) {
                            inQueue[v] = true;
                            q.Enqueue(v);
                        }
                    }
                }
            }

            if (prev[dst] == null) {
                return null;
            }

            List<Edge> path = new();
            for (Edge e = prev[dst]; e != null; e = prev[e.Source]) {
                path.Add(e);
            }

            return path;
        }

        public MCMF GetMinCostMaxFlow(int src, int dst) {
            MCMF result = new MCMF { Flowed = 0, TotalCost = 0 };
            List<Edge> path;

            while ((path = FindAugPath(src, dst)) != null) {
                int add = int.MaxValue;
                int costSum = 0;

                foreach (Edge e in path) {
                    add = Math.Min(add, e.Spare);
                }
                foreach (Edge e in path) {
                    e.AddFlow(add);
                    costSum += e.Cost;
                }

                result.Flowed += add;
                result.TotalCost += costSum * add;
            }

            return result;
        }
    }

    class Program {
        static void Main(string[] args) {
            var tokens = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var (n, m) = (tokens[0], tokens[1]);

            int vertexCount = n+m+3;
            Graph graph = new Graph(vertexCount);
            int src = vertexCount-2;
            int dst = vertexCount-1;

            for (int i = 1; i <= n; ++i) {
                tokens = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int count = tokens[0];
                graph.AddEdge(src, i, 0, 1);

                for (int j = 1; j < tokens.Length; j += 2) {
                    var (job, pay) = (tokens[j], tokens[j+1]);
                    graph.AddEdge(i, n+job, pay, 1);
                }
            }

            for (int i = 1; i <= m; ++i) {
                graph.AddEdge(n+i, dst, 0, 1);
            }

            MCMF result = graph.GetMinCostMaxFlow(src, dst);
            Console.WriteLine($"{result.Flowed}\n{result.TotalCost}");
        }
    }
}

#elif other3
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
class FlowLine
{
    public int Flow { get; set; } = 0;
    public int Limit { get; init; }
    public int Cost { get; init; }
    public int Remaining => Limit - Flow;
    public int Starting { get; init; }
    public int Destination { get; init; }
    public FlowLine Reverse { get; init; }

    public FlowLine(int start , int end , int limit , int cost=0 , FlowLine? reverse = null)
    {
        this.Starting = start;
        this.Destination = end;
        this.Limit = limit;
        this.Cost = cost;
        this.Reverse = reverse ?? new(end , start , 0 , -cost , this);
    }
}
class Program {
    public static void Coding() {
        (int employee_count,int work_count) = Cin;
        // 0~customer : 고객
        // customer ~ store : 상점
        // 일/직원/소스/싱크 순
        int source = employee_count+work_count;
        int sink = source+1;
        LinkedList<FlowLine>[] lines = new LinkedList<FlowLine>[sink+1];
        for(int i=0;i<=sink;i++) lines[i]=new();
        void Push(FlowLine line) {
            lines[line.Starting].AddLast(line);
            lines[line.Destination].AddLast(line.Reverse);
        }
        for(int worker=0;worker<employee_count;worker++) {
            int worker_index = worker + work_count;
            //소스 -> 직원
            FlowLine line = new(source,worker_index,1);
            Push(line);
            // 직원 -> 일
            int[] input = Cin;
            for(int i=1;i<input.Length;i+=2) {
                line = new(worker_index,input[i]-1,1,input[i+1]);
                Push(line);
            }
        }
        //일 -> 싱크
        for(int i=0;i<work_count;i++) {
            Push(new FlowLine(i,sink,1));
        }
        //mcmf
        Queue<int> queue = new();
        int result = 0, counting = 0;
        while(true) {
            int[] dist = new int[sink+1];
            Array.Fill(dist,int.MaxValue>>1);
            FlowLine?[] visited = new FlowLine[sink+1];
            bool[] exist_queue = new bool[sink+1];
            dist[source]=0;
            exist_queue[source]=true;
            queue.Clear();
            queue.Enqueue(source);

            while(queue.Count > 0) {
                int me = queue.Dequeue();
                exist_queue[me] = false;
                foreach(var line in lines[me]) {
                    int other = line.Destination;
                    int next_dist = dist[me]+line.Cost;
                    if (line.Remaining > 0 && next_dist < dist[other]) {
                        dist[other] = next_dist;
                        visited[other] = line;
                        if (exist_queue[other]) continue;
                        queue.Enqueue(other);
                        exist_queue[other] = true;
                    }
                }
            }

            if (visited[sink] is null) break;

            int flow = int.MaxValue;
            for(int p=sink;p!=source;p=visited[p].Starting) {
                flow = Math.Min(flow, visited[p].Remaining);
            }
            for(int p=sink;p!=source;p=visited[p].Starting) {
                result += flow * visited[p].Cost;
                visited[p].Flow += flow;
                visited[p].Reverse.Flow -= flow;
            }

            counting++;
        }

        Coutln = counting;
        Cout = result;
    }
}
#endif
}
