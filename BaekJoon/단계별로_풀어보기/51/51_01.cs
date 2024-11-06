using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 15
이름 : 배성훈
내용 : 도시 왕복하기 1
    문제번호 : 17412번

    최대 유량 문제다
    유튜브를 보고, 네트워크 플로우 알고리즘을 따라 쳐봤다
    영상을 보고 쓴 알고리즘은 에드먼드 카프 알고리즘이다

    한번 지나간 간선은 다시 지나가면 안되므로 현재 흐르는 유량을 1로 설정했다
    1에서 2로흐르는 경로를 찾아 제출했다

    단방향 간선이라 명시되어서 단방향으로만 연결하니 91%쯤에서 틀렸다
    유량의 대칭성?에의해 유량이 음으로 바뀌면서 연산을 진행한다
*/

namespace BaekJoon._51
{
    internal class _51_01
    {

        static void Main1(string[] args)
        {

            int INF = 1_000_000_000;
            StreamReader sr;

            int n, m;

            int[,] c;
            int[,] f;
            int[] d;

            List<int>[] line;
            int ret = 0;

            Solve();

            void Solve()
            {

                Input();
                MaxFlow(1, 2);
                Console.WriteLine(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                c = new int[n + 1, n + 1];
                f = new int[n + 1, n + 1];
                d = new int[n + 1];
                line = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    line[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int from = ReadInt();
                    int to = ReadInt();

                    line[from].Add(to);
                    line[to].Add(from);
                    c[from, to] = 1;
                }
            }

            void MaxFlow(int _s, int _e)
            {

                while (true)
                {

                    Array.Fill(d, -1);
                    Queue<int> q = new(n);
                    q.Enqueue(_s);

                    while(q.Count > 0)
                    {

                        int cur = q.Dequeue();
                        
                        // 경로 이동
                        for (int i = 0; i < line[cur].Count; i++)
                        {

                            int next = line[cur][i];
                            // 더 흐를 유량이 있는지 확인
                            if (c[cur, next] - f[cur, next] > 0 && d[next] == -1)
                            {

                                q.Enqueue(next);
                                d[next] = cur;
                                if (next == _e) break;
                            }
                        }
                    }

                    // 모든 경로를 다 찾은 뒤에 탈출
                    if (d[_e] == -1) break;
                    int flow = INF;

                    // 거꾸로 최소 유량 탐색
                    for (int i = _e; i != _s; i = d[i])
                    {

                        flow = Math.Min(flow, c[d[i], i] - f[d[i], i]);
                    }

                    // 최소 유량만큼 추가
                    for (int i = _e; i != _s; i = d[i])
                    {

                        f[d[i], i] += flow;
                        f[i, d[i]] -= flow;
                    }

                    ret += flow;
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
class MainApp
{
    static int N;
    static int Count;
    static int[,] Cap;
    static int[,] Flow;
    static int[] visited;
    static List<List<int>> list = new List<List<int>>();
    static void MaxFlow(int start, int end)
    {
        while (true)
        {
            for (int i = 0; i < N + 1; i++)
            {
                visited[i] = -1;
            }
            var queue = new Queue<int>();
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                int x = queue.Dequeue();
                for (int i = 0; i < list[x].Count; i++)
                {
                    int y = list[x][i];
                    if (Cap[x, y] - Flow[x, y] > 0 && visited[y] == -1)
                    {
                        queue.Enqueue(y);
                        visited[y] = x;
                        if (y == end)
                        {
                            Count++;
                            break;
                        }
                    }
                }
            }
            if (visited[end] == -1) break;
            int flow = int.MaxValue;
            for (int i = end; i != start; i = visited[i])
            {
                flow = Math.Min(flow, Cap[visited[i], i] - Flow[visited[i], i]);
            }
            for (int i = end; i != start; i = visited[i])
            {
                Flow[visited[i], i]++;
                Flow[i, visited[i]]--;
            }
        }
    }
    static void Main(string[] args)
    {
        var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        string[] Input = sr.ReadLine().Split();
        N = int.Parse(Input[0]);
        int P = int.Parse(Input[1]);
        Cap = new int[N + 1, N + 1];
        Flow = new int[N + 1, N + 1];
        visited = new int[N + 1];
        for (int i = 0; i < N + 1; i++)
        {
            list.Add(new List<int>());
        }
        for (int i = 0; i < P; i++)
        {
            string[] input = sr.ReadLine().Split();
            int A = int.Parse(input[0]);
            int B = int.Parse(input[1]);
            list[A].Add(B);
            list[B].Add(A);
            Cap[A, B] = 1;
        }
        MaxFlow(1, 2);
        Console.WriteLine(Count);
        sr.Close();
    }
}
#elif other2
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Flow
{
    public int Dst;

    public int CurrFlow;
    public int MaxFlow;

    public Flow(int dst, int currFlow, int maxFlow)
    {
        Dst = dst;
        CurrFlow = currFlow;
        MaxFlow = maxFlow;
    }
}

public static class Program
{
    private static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var rd = new Random();

        var np = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var n = np[0];
        var p = np[1];

        var graph = new Dictionary<int, Dictionary<int, Flow>>();
        for (var idx = 1; idx <= n; idx++)
            graph[idx] = new Dictionary<int, Flow>();

        while (p-- > 0)
        {
            var l = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

            graph[l[0]][l[1]] = new Flow(l[1], 0, 1);
            graph[l[1]][l[0]] = new Flow(l[0], 0, 0);
        }

        var level = new int?[1 + n];
        var deadend = new bool[1 + n];

        var source = 1;
        var sink = 2;
        var maxflow = 0;

        while (true)
        {
            var anyFlowMade = false;

            Array.Clear(level);
            Array.Clear(deadend);

            BuildLevelGraph(graph, level, source, sink);

            while (true)
            {
                var newflow = TryFlow(graph, level, deadend, source, sink, Int32.MaxValue);
                if (newflow == 0)
                    break;

                anyFlowMade = true;
                maxflow += newflow;
            }

            if (!anyFlowMade)
                break;
        }

        sw.WriteLine(maxflow);
    }

    private static void BuildLevelGraph(Dictionary<int, Dictionary<int, Flow>> graph, int?[] level, int source, int sink)
    {
        var q = new Queue<(int pos, int level)>();
        q.Enqueue((source, 0));

        while (q.TryDequeue(out var s))
        {
            if (level[s.pos].HasValue)
                continue;

            level[s.pos] = s.level;

            if (s.pos == sink)
                continue;

            foreach (var f in graph[s.pos].Values)
                if (f.MaxFlow != f.CurrFlow && !level[f.Dst].HasValue)
                    q.Enqueue((f.Dst, s.level + 1));
        }
    }

    private static int TryFlow(Dictionary<int, Dictionary<int, Flow>> graph, int?[] level, bool[] deadend, int curr, int sink, int maxflowNow)
    {
        if (curr == sink)
            return maxflowNow;

        foreach (var f in graph[curr].Values)
        {
            if (f.MaxFlow == f.CurrFlow)
                continue;

            if (level[f.Dst].HasValue && level[curr].HasValue && level[curr].Value >= level[f.Dst].Value)
                continue;

            if (deadend[f.Dst])
                continue;

            var flow = TryFlow(graph, level, deadend, f.Dst, sink, Math.Min(f.MaxFlow - f.CurrFlow, maxflowNow));
            if (flow != 0)
            {
                f.CurrFlow += flow;
                graph[f.Dst][curr].CurrFlow -= flow;

                return flow;
            }
        }

        deadend[curr] = true;
        return 0;
    }
}

#elif other3
int[] input = Array.ConvertAll(Console.ReadLine().Split(),int.Parse);
int n = input[0]; //도시 개수
int m = input[1]; //단방향 길 개수

// lines[도시 번호] : 해당 도시와 연결된 간선 리스트
LinkedList<FlowLine>[] lines = new LinkedList<FlowLine>[n];
for(int i=0;i<n;i++) lines[i] = new(); //배열 초기화
//간선 추가해주는 함수
void Push(FlowLine l) {
    lines[l.Starting].AddLast(l); //시작점엔 정방향 간선을 추가
    lines[l.Destination].AddLast(l.Reverse); //도착점엔 역방향 간선을 추가
}

//모든 간선에 대해 입력받기
for(int i=0;i<m;i++) {
    input = Array.ConvertAll(Console.ReadLine().Split(),int.Parse);
    //번호는 0부터 시작하므로 -1을 해줌.
    int starting = input[0]-1; //출발점
    int destination = input[1]-1; //도착점
    // 이미 지나간 길을 또 갈순 없으므로, 용량은 1
    FlowLine line = new(starting,destination,1);
    Push(line);
}

const int source = 0; //출발점
const int sink = 1; //도착점
int answer = 0; //최대 유량 (마지막에 출력할 예정)
//애드몬드 카프 알고리즘
Queue<int> queue = new(); //BFS용 큐
while(true) {
    //방문체크 겸 역추적을 위해 이동한 간선을 저장하는 배열.
    // visited[정점] != null : 방문 여부
    FlowLine[]? visited = new FlowLine[n];
    visited[source] = new(0,0,0); //가짜 방문체크 (시작점으로 역행하는걸 방지)
    queue.Enqueue(source); //출발점을 큐에 삽입
    //BFS 시작
    while(queue.Count > 0) {
        //큐에서 꺼낸 정점과 연결된 모든 간선 찾아보기
        foreach(var line in lines[queue.Dequeue()]) {
            //이미 방문한적이 있거나, 더이상 흘릴 유량이 없다면, 건너뛰기
            if (visited[line.Destination] != null || line.Remaining <= 0) continue;
            //방문체크 (역추적을 위해 이동한 간선을 저장)
            visited[line.Destination] = line;
            queue.Enqueue(line.Destination); //도착점을 큐에 삽입 (BFS)
        }
    }
    //도착점에 도착하지 못했다면, 더이상 흘릴 유량이 없다는 것이므로 중단
    if (visited[sink] == null) break;

    //이동한 모든 간선을 되짚어서 흘릴수 있는 유량 구하기
    int flow = int.MaxValue;
    //싱크에서 다시 출발에서 visited 배열에 남긴 간선 정보를 이용해서 소스에 도달할때까지 역추적
    for(int point=sink;point!=source;point=visited[point].Starting) {
        //유량 최솟값 갱신
        flow = Math.Min(flow, visited[point].Remaining);
    }
    //이제 흘릴수 있는 유량을 적용
    for(int point=sink;point!=source;point=visited[point].Starting) {
        visited[point].Flow += flow;
        visited[point].Reverse.Flow -= flow; //역방향 간선에는 음의 유량을 흘려야함.
    }
    answer += flow; //최대 유량 추가
}
//정답 출력
Console.Write(answer);

class FlowLine {
    public int Starting {get; init;} //시작 지점
    public int Destination {get; init;} //도착 지점
    public int Limit {get; init;} //용량 (유량 제한)
    public int Flow = 0; //현재 흐르고 있는 유량
    public int Remaining => Limit - Flow; //더 흘릴수 있는 유량 (잔여 용량)
    public FlowLine Reverse {get; init;} //이 간선과 반대되는 반대편 간선
    public FlowLine(int start,int end,int limit,FlowLine? reverse = null) {
        Starting = start;
        Destination = end;
        Limit = limit;
        //이 간선을 추가하면, 반대편 간선도 자동으로 추가됨.
        this.Reverse = reverse ?? new(end,start,0,this);
    }
}
#endif
}
