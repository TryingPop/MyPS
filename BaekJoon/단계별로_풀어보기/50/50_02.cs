using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 14
이름 : 배성훈
내용 : 컨닝 2
    문제번호 : 11014번

    이분 매칭 문제다
    이분 매칭으로 해결했다
    처음에 짝수 인덱스에 대해 -, / 방향으로만 간선을 연결해서 한 번 틀렸다
    조금 더 고민해보니, -, / 방향은 짝수 인덱스에서 컨닝 매칭할 때의 경우만 보장하고
    홀수 인덱스에 대해 매칭은 고려하지 않음을 떠올릴 수 있었다
    그래서 -, /, \ 방향으로 간선을 연결하니 이상없이 통과했다

    질문 게시판을 보는 중 다른 사람을 보니 똑같이 틀린 사람이 있었다;
    이 분의 예제를 빌려오면
        ...
        ...
        x.x
    -, /는 5가 나오고, 정답은 4가 나와야한다

    원리 부분은 해당 댓글쪽을 보면 된다
    https://www.acmicpc.net/board/view/143934

    /// 위 참고글 내용
    이 문제를 완전히 이해하려면 이분매칭만으로는 부족하고 두 가지 그래프 이론 지식이 추가로 필요합니다.

    우선 첫째로, 이 문제에서 구해야 하는 값은 사실 최대 매칭 크기가 아닙니다. 그래프의 독립 집합이라는 개념이 있는데, 이렇게 정의합니다.

    모든 정점의 집합 V의 어떤 부분집합 U가 있고, U에서 정점 2개를 어떻게 고르든지 그 사이에 간선이 없다면, U는 그 그래프의 독립 집합이다.
    11014번 문제에서 각 자리를 정점, 두 자리의 컨닝 가능성을 간선으로 정의했습니다. 그러므로 컨닝 가능한 학생들이 없도록 자리들을 선택했다면 위 정의에 따라 그 자리들은 독립 집합이 됩니다. 우리는 그러면서 최대한 많은 자리를 선택하고 싶기에, 우리의 목표는 최대 독립 집합의 크기입니다.

    여기서 한 가지 정리가 필요합니다. 갈라이(Gallai)의 정리라고 부르며, 내용은 이렇습니다.

    고립된 정점(간선이 1개도 달리지 않은 정점)이 없는 그래프에서 최대 독립 집합의 크기와 최소 정점 커버의 크기를 더하면 정점의 총 개수와 같다.
    정점 커버는 우리말로 정점 덮개라고도 합니다. 이 문제를 맞히는 데만 관심이 있다면 정점 커버의 의미는 몰라도 되지만, 정의를 써보자면 이렇습니다.

    모든 정점의 집합 V의 어떤 부분집합 U가 있고, 그래프에서 어떤 간선을 고르든지 그 간선의 두 끝점 중 적어도 하나가 U에 있다면, U는 그 그래프의 정점 커버이다.
    한 가지 정리가 더 필요합니다. 쾨닉(Konig)의 정리라고 부르며, 내용은 이렇습니다.

    이분 그래프에서 최대 매칭의 크기와 최소 정점 커버의 크기는 같다.
    이 모든 내용을 11014번 문제에 적용해 봅시다. 고립된 정점에 대해서 생각을 해야 되긴 하는데, 만약에 없다고 칩시다. 그러면 위에 나온 정리 2개를 모두 사용할 수 있으며, 조합하면 (정점의 총 개수) - (최대 매칭의 크기)를 구하면 된다는 결론이 나옵니다.

    예제 1에서는 모든 자리가 앉을 수 있어서 고립된 정점이 없습니다. 그래서 최대 매칭의 크기는 2이고 전체 자리가 6개니 6-2=4가 정답이 됩니다.

    이제 고립된 정점이 있을 때를 생각해봅시다. 그러러면 앉을 수 없는 자리를 어떻게 처리할지 기준을 세워야 합니다. 저는 컨닝 가능한 위치이면서 둘 다 앉을 수 있을 때만 간선이라고 정해 보겠습니다. 그러면 앉을 수 없는 자리는 고립되는 게 확실하니 일단 제외합시다(정점의 총 개수에서 차감). 그런데 앉을 수 있는 자리도 고립될 수 있습니다(예제 2나 3처럼). 그래서 앉을 수 있는 자리는 4가지 컨닝 방향 중에 앉을 수 있는 자리가 하나라도 있는지 확인을 해야 한다고 생각하실 수 있습니다. 하지만 그럴 필요가 없습니다.

    앉을 수 있는데 고립된 자리가 T개 있다고 해봅시다. 그 T개를 제외하면 갈라이의 정리를 쓸 수 있으니, 그 T개를 제외하고 풀었을 때 정답이 X라고 해봅시다. 그러면 그 T개를 제외하지 않았을 때의 정답은 X+T가 돼야 합니다(앉을 수 있고 고립된 자리는 무조건 학생을 앉힐 수 있으니까). 우리는 X+T만 알면 됩니다.

    그런데 X가 어떻게 나왔을 거냐면,

    X = (앉을 수 있는 자리의 개수) - T - (T개를 제외했을 때 최대 매칭의 크기)
    입니다. T를 이항하면,

    X + T = (앉을 수 있는 자리의 개수) - (T개를 제외했을 때 최대 매칭의 크기)
    이렇게 됩니다. 그런데 제외한 T개는 다 고립된 자리라서 제외하든 말든 최대 매칭 크기가 같습니다. 한마디로 고립을 생각하지 말고 앉을 수 있는 자리 전부에 대해서 이분 매칭을 돌려도 된다는 뜻입니다.

    최종 정리하면,

    앉을 수 있는 자리 -> 정점
    문제에서 말한 4가지 방향에 해당하고, 양쪽 다 앉을 수 있는 자리일 때 -> 간선
    정답 = (앉을 수 있는 자리의 개수) - (최대 매칭의 크기)
*/

namespace BaekJoon._50
{
    internal class _50_02
    {

        static void Main2(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int row, col;
            int ret;

            int[] match;
            bool[] visit;

            int[][] board;
            List<int>[] line;
            int len;


            Solve();

            void Solve()
            {

                Init();

                int test = ReadInt();

                while(test-- > 0)
                {

                    Input();

                    LinkLine();

                    Array.Fill(match, -1, 0, len);

                    for (int i = 0; i < len; i++)
                    {

                        Array.Fill(visit, false, 0, len);
                        if (DFS(i)) ret--;
                    }

                    for (int i = 0; i < len; i++)
                    {

                        line[i].Clear();
                    }

                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            bool DFS(int _n)
            {

                for (int i = 0; i < line[_n].Count; i++)
                {

                    int next = line[_n][i];
                    if (visit[next]) continue;
                    visit[next] = true;

                    if (match[next] == -1 || DFS(match[next]))
                    {

                        match[next] = _n;
                        return true;
                    }
                }

                return false;
            }

            void LinkLine()
            {

                len = 0;
                for (int c = 0; c < col; c += 2)
                {

                    len += row;
                    if (board[0][c] == '.')
                    {

                        int idx = PosToInt(0, c);
                        if (c != 0) 
                        { 
                            
                            if (board[0][c - 1] == '.') line[idx].Add(PosToInt(0, c - 1)); 
                            if (1 < row && board[1][c - 1] == '.') line[idx].Add(PosToInt(1, c - 1));
                        }
                        if (c < col - 1) 
                        { 
                            
                            if (board[0][c + 1] == '.') line[idx].Add(PosToInt(0, c + 1));
                            if (1 < row && board[1][c + 1] == '.') line[idx].Add(PosToInt(1, c + 1));
                        }
                    }

                    for (int r = 1; r < row - 1; r++)
                    {

                        if (board[r][c] == 'x') continue;

                        int idx = PosToInt(r, c);

                        if (c != 0)
                        {

                            if (board[r - 1][c - 1] == '.') line[idx].Add(PosToInt(r - 1, c - 1));
                            if (board[r][c - 1] == '.') line[idx].Add(PosToInt(r, c - 1));
                            if (board[r + 1][c - 1] == '.') line[idx].Add(PosToInt(r + 1, c - 1));
                        }

                        if (c < col - 1)
                        {

                            if (board[r - 1][c + 1] == '.') line[idx].Add(PosToInt(r - 1, c + 1));
                            if (board[r][c + 1] == '.') line[idx].Add(PosToInt(r, c + 1));
                            if (board[r + 1][c + 1] == '.') line[idx].Add(PosToInt(r + 1, c + 1));
                        }
                    }

                    if (1 < row && board[row - 1][c] == '.')
                    {

                        int idx = PosToInt(row - 1, c);
                        if (c != 0)
                        {

                            if (board[row - 2][c - 1] == '.') line[idx].Add(PosToInt(row - 2, c - 1));
                            if (board[row - 1][c - 1] == '.') line[idx].Add(PosToInt(row - 1, c - 1));
                        }

                        if (c < col - 1)
                        {

                            if (board[row - 2][c + 1] == '.') line[idx].Add(PosToInt(row - 2, c + 1));
                            if (board[row - 1][c + 1] == '.') line[idx].Add(PosToInt(row - 1, c + 1));
                        }
                    }
                }
            }

            void Input()
            {

                row = ReadInt();
                col = ReadInt();
                ret = row * col;

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        int cur = sr.Read();
                        board[r][c] = cur;
                        if (cur == 'x') ret--;
                    }

                    if (sr.Read() == '\r') sr.Read();
                }
            }

            int PosToInt(int _r, int _c)
            {

                _c /= 2;
                return row * _c + _r;
            }
            
            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput());

                match = new int[3200];
                visit = new bool[3200];

                board = new int[80][];
                for (int i = 0; i < 80; i++)
                {

                    board[i] = new int[80];
                }

                line = new List<int>[3200];
                for (int i = 0; i < line.Length; i++)
                {

                    line[i] = new(4);
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c =sr.Read()) != -1 && c != ' ' && c != '\n')
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

#nullable disable

public enum GraphNodeType
{
    Source,
    Sink,
    Seat,
}

public record struct GraphNode(GraphNodeType type, int y, int x);

public class Flow
{
    public int CurrFlow;
    public int MaxFlow;

    public bool CanFlow => CurrFlow < MaxFlow;

    public override string ToString() => $"{CurrFlow}/{MaxFlow}";
}

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var t = Int32.Parse(sr.ReadLine());
        while (t-- > 0)
        {
            var nm = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var n = nm[0];
            var m = nm[1];

            var sourceNode = new GraphNode(GraphNodeType.Source, 0, 0);
            var sinkNode = new GraphNode(GraphNodeType.Sink, 0, 0);

            var map = new string[n];
            for (var idx = 0; idx < n; idx++)
                map[idx] = sr.ReadLine();

            var nodeIdMap = new Dictionary<GraphNode, int>();
            nodeIdMap.Add(sourceNode, nodeIdMap.Count);
            nodeIdMap.Add(sinkNode, nodeIdMap.Count);

            for (var y = 0; y < n; y++)
                for (var x = 0; x < m; x++)
                    if (map[y][x] == '.')
                        nodeIdMap.Add(new GraphNode(GraphNodeType.Seat, y, x), nodeIdMap.Count);

            var flowgraph = new Dictionary<int, Dictionary<int, Flow>>();
            var graph = new List<int>[nodeIdMap.Count];

            for (var idx = 0; idx < graph.Length; idx++)
                graph[idx] = new List<int>();

            for (var y = 0; y < n; y++)
                for (var x = 0; x < m; x += 2)
                {
                    if (map[y][x] == 'x')
                        continue;

                    var node = nodeIdMap[new GraphNode(GraphNodeType.Seat, y, x)];

                    foreach (var dx in new[] { -1, 1 })
                        foreach (var dy in new[] { -1, 0, 1 })
                        {
                            var targetNode = new GraphNode(GraphNodeType.Seat, y + dy, x + dx);
                            if (!nodeIdMap.ContainsKey(targetNode))
                                continue;

                            var target = nodeIdMap[targetNode];

                            EnsureExists(flowgraph, node, target);

                            flowgraph[node][target] ??= new Flow();
                            flowgraph[target][node] ??= new Flow();

                            graph[node].Add(target);
                            graph[target].Add(node);
                            flowgraph[node][target].MaxFlow++;
                        }
                }

            for (var y = 0; y < n; y++)
                for (var x = 0; x < m; x++)
                {
                    if (map[y][x] == 'x')
                        continue;

                    var target = nodeIdMap[new GraphNode(GraphNodeType.Seat, y, x)];
                    if (x % 2 == 0)
                    {
                        var source = nodeIdMap[sourceNode];

                        EnsureExists(flowgraph, source, target);

                        graph[source].Add(target);
                        graph[target].Add(source);
                        flowgraph[source][target].MaxFlow++;
                    }
                    else
                    {
                        var sink = nodeIdMap[sinkNode];

                        EnsureExists(flowgraph, target, sink);

                        graph[target].Add(sink);
                        graph[sink].Add(target);
                        flowgraph[target][sink].MaxFlow++;
                    }
                }

            var maxflow = Dinic(nodeIdMap.Count, flowgraph, graph, nodeIdMap[sourceNode], nodeIdMap[sinkNode]);
            var seatCount = map.Sum(l => l.Count(ch => ch == '.'));

            sw.WriteLine(seatCount - maxflow);
        }
    }

    private static void EnsureExists(Dictionary<int, Dictionary<int, Flow>> flowgraph, int src, int dst)
    {
        if (!flowgraph.ContainsKey(src))
            flowgraph.Add(src, new Dictionary<int, Flow>());

        if (!flowgraph.ContainsKey(dst))
            flowgraph.Add(dst, new Dictionary<int, Flow>());

        flowgraph[src][dst] = flowgraph[src].GetValueOrDefault(dst) ?? new Flow();
        flowgraph[dst][src] = flowgraph[dst].GetValueOrDefault(src) ?? new Flow();
    }

    private static int Dinic(int n, Dictionary<int, Dictionary<int, Flow>> flowgraph, List<int>[] graph, int source, int sink)
    {
        var levelgraph = new int?[n];
        var isDeadEnd = new bool[n];

        var maxflow = 0;
        while (true)
        {
            Array.Clear(levelgraph);
            Array.Clear(isDeadEnd);

            var isChanged = false;
            BuildLevelGraph(flowgraph, graph, levelgraph, source, sink);

            while (true)
            {
                var f = MakeFlow(flowgraph, graph, levelgraph, isDeadEnd, source, sink, Int32.MaxValue);
                if (f == 0)
                    break;

                maxflow += f;
                isChanged = true;
            }

            if (!isChanged)
                break;
        }

        return maxflow;
    }

    private static void BuildLevelGraph(Dictionary<int, Dictionary<int, Flow>> flowgraph, List<int>[] graph, int?[] levelgraph, int source, int sink)
    {
        var q = new Queue<(int pos, int level)>();
        q.Enqueue((source, 0));

        while (q.TryDequeue(out var s))
        {
            var (pos, level) = s;

            if (levelgraph[pos].HasValue)
                continue;

            levelgraph[pos] = level;

            if (pos == sink)
                continue;

            foreach (var dst in graph[pos])
            {
                if (!flowgraph[pos][dst].CanFlow)
                    continue;

                if (levelgraph[dst].HasValue)
                    continue;

                q.Enqueue((dst, 1 + level));
            }
        }
    }

    private static int MakeFlow(Dictionary<int, Dictionary<int, Flow>> flowgraph, List<int>[] graph, int?[] levelgraph, bool[] isDeadEnd, int pos, int sink, int currFlow)
    {
        if (pos == sink)
            return currFlow;

        foreach (var dst in graph[pos])
        {
            if (isDeadEnd[dst])
                continue;

            var forward = flowgraph[pos][dst];
            if (!forward.CanFlow)
                continue;

            if (!levelgraph[pos].HasValue || !levelgraph[dst].HasValue || levelgraph[pos] >= levelgraph[dst])
                continue;

            var f = MakeFlow(flowgraph, graph, levelgraph, isDeadEnd, dst, sink, Math.Min(forward.MaxFlow - forward.CurrFlow, currFlow));
            if (f == 0)
                continue;

            var reverse = flowgraph[dst][pos];
            forward.CurrFlow += f;
            reverse.CurrFlow -= f;

            return f;
        }

        isDeadEnd[pos] = true;
        return 0;
    }
}

#elif other2
import static java.lang.Integer.*;

import java.io.*;
import java.util.*;

public class Main {

    static int C,N,M;
    static boolean[] visited;
    static int[] match;
    static ArrayList<Integer>[] edge;
    static char[][] graph;
    static char[] line;
    static int[][] numbers;
    static int[] dx = {1,0,-1,1,0,-1};
    static int[] dy = {-1, -1, -1, 1, 1, 1};
    static int ans;

    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(System.out));

        StringTokenizer st;
        C = parseInt(br.readLine());

        while (C-- > 0) {

            st = new StringTokenizer(br.readLine());
            N = parseInt(st.nextToken());
            M = parseInt(st.nextToken());
            graph = new char[N+1][M+1];
            numbers = new int[N+1][M+1];

            for (int i = 0 ; i < N ; i ++) {
                line = br.readLine().toCharArray();
                for (int j = 0 ; j < M ; j++) {
                    graph[i][j] = line[j];
                }
            }

            int count = 1;
            for (int i = 0 ; i < N ; i++) {
                for (int j = 0 ; j < M ; j++ ) {
                    if (graph[i][j]=='.') {
                        numbers[i][j] = count;
                        count++;
                    }
                }
            }


            match = new int[N*M+1];
            visited = new boolean[N * M + 1];

            edge = new ArrayList[N*M+2];
            for (int i = 0 ; i <=N*M+1 ; i++) {
                edge[i] = new ArrayList<>();
            }

            for (int i = 0 ; i < N ; i ++) {
                for (int j = 0; j < M ;  j++) {
                    if (j%2!=0) continue;
                    if (graph[i][j] == 'x') continue; // 소문자 x임

                    for (int k = 0 ; k < 6 ; k++) {
                        int nx = i + dx[k];
                        int ny = j + dy[k];

                        if (nx>=0 && nx<N && ny>=0 && ny<M && graph[nx][ny]=='.') {
                            edge[numbers[i][j]].add(numbers[nx][ny]);
                        }
                    }
                }
            }

            ans = 0;
            for (int i = 1 ; i <= count ; i++) {
                Arrays.fill(visited, false);
                if (dfs(i)) {
                    ans++;
                }
            }
            bw.write(Integer.toString(count-ans-1));
            bw.newLine();
        }

        bw.flush();
    }

    private static boolean dfs(int node) {
        for (int i = 0 ; i < edge[node].size() ; i++) {
            int nextNode = edge[node].get(i);

            if (visited[nextNode]) {
                continue;
            }

            visited[nextNode] = true;

            if (match[nextNode] == 0 || dfs(match[nextNode])) {
                match[nextNode] = node;
                return true;
            }
        }
        return false;
    }
}

/*
1
2 3
x.x
xxx

 */
#elif other3
def dfs(a):
    visited[a]=True
    for b in adj[a]:
        if B[b]==-1: B[b]=a; return 1
    for b in adj[a]:
        if not(visited[B[b]]) and dfs(B[b]): B[b]=a; return 1
    return 0

import sys
input=sys.stdin.readline
MAX=2000
for _ in range(int(input())):
    N,M=map(int,input().split())
    desk=[list(input().rstrip())for _ in range(N)]
    node=0
    for j in range(1,M,2):
        for i in range(N):
            if desk[i][j]=='.': desk[i][j]=node; node+=1
    odd=node
    adj=[]
    for j in range(0,M,2):
        for i in range(N):
            if desk[i][j]=='.':
                tmp=[]
                for dx,dy in((-1,-1),(0,-1),(1,-1),(-1,1),(0,1),(1,1)):
                    nx,ny=i+dx,j+dy
                    if 0<=nx<N and 0<=ny<M and desk[nx][ny]!='x': tmp.append(desk[nx][ny])
                adj.append(tmp)

    even=len(adj)
    match=0
    B=[-1]*odd
    for i in range(even):
        visited=[0]*even
        match+=dfs(i)
        if match==odd:break
    print(odd+even-match)
#elif other4
// #include <iostream>
// #include <algorithm>
// #include <vector>
using namespace std;

int pnt[3201], xs[] = { 1,0,-1,-1,0,1 }, ys[] = { 1,1,1,-1,-1,-1 };
vector<int> l[3201];
int p[80][80];
bool visited[3201];

bool dfs(int x) {
	for (int i = 0;i < l[x].size();i++) {
		int k = l[x][i];
		if (visited[k]) continue;
		visited[k] = true;

		if (pnt[k] == 0 || dfs(pnt[k])) {
			pnt[k] = x;
			return true;
		}
	}

	return false;
}

int main() {
	cin.tie(NULL);
	cout.tie(NULL);
	cin.sync_with_stdio(false);

	int t, n, m;
	char c;
	cin >> t;

	while (t != 0) {
		int cnt = 0, ls = 0, rs = 0;
		cin >> n >> m;
		for (int i = 0;i < n;i++) {
			for (int j = 0;j < m;j++) {
				cin >> c;
				if (c == '.') {
					if (j % 2) p[i][j] = ++rs;
					else p[i][j] = ++ls;
				}
				else p[i][j] = 0;
			}
		}

		for (int i = 0;i < n;i++) {
			for (int j = 0;j < m;j += 2) {
				if (p[i][j]) {
					for (int k = 0;k < 6;k++) {
						int x = i + xs[k], y = j + ys[k];
						if (0 <= x && x < n && 0 <= y && y < m && p[x][y]) l[p[i][j]].push_back(p[x][y]);
					}
				}
			}
		}


		for (int i = 1;i <= ls;i++) {
			for (int j = 1;j <= rs;j++) visited[j] = false;
			if (dfs(i)) cnt++;
		}

		for (int i = 1;i <= ls;i++) l[i].clear();
		for (int i = 1;i <= rs;i++) pnt[i] = 0;

		cout << ls + rs - cnt << "\n";
		t--;
	}
}
#endif
}
