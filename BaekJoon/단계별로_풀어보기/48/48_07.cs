using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 3
이름 : 배성훈
내용 : 두부장수 장홍준
    문제번호 : 1657번

    dp, 비트마스킹, 비트필드를 이용한 dp 문제다
    48_06의 내용을 이용해 최대 값이 담기게 dp를 변형해 풀었다
    dp 역할만 바꼈을 뿐 탐색 방법은 앞과 같다

    이렇게 푸니 160ms에 통과했다
    그런데 다른 사람의 풀이를 보니 MCMF (최대 최소 유량 으로 풀 수 있다)
    몇 단원 안남았으니, 나중에 네트워크 플로우 단원하게 되면 비슷한 문제로 다시 풀어봐야겠다
*/

namespace BaekJoon._48
{
    internal class _48_07
    {

        static void Main7(string[] args)
        {

            int[][] value = new int[6][];
            value[0] = new int[6] { 10, 8, 7, 5, 0, 1 };
            value[1] = new int[6] { 8, 6, 4, 3, 0, 1 };
            value[2] = new int[6] { 7, 4, 3, 2, 0, 1 };
            value[3] = new int[6] { 5, 3, 2, 2, 0, 1 };
            value[4] = new int[6];
            value[5] = new int[6] { 1, 1, 1, 1, 0, 0 };

            StreamReader sr;
            int row, col;
            int[][] board;
            int[][] dp;

            Solve();
            void Solve()
            {

                Input();
                dp = new int[row * col][];
                for (int i = 0; i < row * col; i++)
                {

                    dp[i] = new int[1 << col];
                    Array.Fill(dp[i], -1);
                }

                Console.WriteLine(DFS(0, 0));
            }

            int DFS(int _idx, int _state)
            {

                if (_idx >= row * col) return 0;

                int ret = dp[_idx][_state];
                if (ret != -1) return ret;
                ret = 0;

                ret = DFS(_idx + 1, _state >> 1);
                if ((_state & 1) == 0)
                {


                    int r = _idx / col;
                    int c = _idx % col;

                    if (c != col - 1 && (_state & 2) == 0)
                    {

                        int add = value[board[r][c]][board[r][c + 1]];
                        int chk = DFS(_idx + 2, _state >> 2) + add;
                        ret = ret < chk ? chk : ret;
                    }

                    if (r != row - 1)
                    {

                        int add = value[board[r][c]][board[r + 1][c]];
                        int chk = DFS(_idx + 1, (_state >> 1) | (1 << (col - 1))) + add;
                        ret = ret < chk ? chk : ret;
                    }
                }

                dp[_idx][_state] = ret;
                return ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 1024 * 16);
                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = sr.Read() - 'A';
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                sr.Close();
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MCMFImplementation {
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

    public record Line(int Axis, int Start, int End, int Weight);

    class Program {
        static int[,] priceTable = {
            { 10, 8, 7, 5, 0, 1 },
            {  8, 6, 4, 3, 0, 1 },
            {  7, 4, 3, 2, 0, 1 },
            {  5, 3, 2, 2, 0, 1 },
            {  0, 0, 0, 0, 0, 0 },
            {  1, 1, 1, 1, 0, 0 },
        };

        static void Main(string[] args) {
            var tokens = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var (n, m) = (tokens[0], tokens[1]);

            int vertexCount = n*m+3;
            int src = vertexCount-3;
            int srcNeck = vertexCount-2;
            int dst = vertexCount-1;
            Graph graph = new Graph(vertexCount);

            string[] map = new string[n];
            for (int i = 0; i < n; ++i) {
                map[i] = Console.ReadLine();
            }

            for (int i = 0; i < n; ++i) {
                for (int j = 0; j < m; ++j) {
                    char c1 = map[i][j], c2;
                    int n1 = i*m+j, n2;
                    bool even = (i+j) % 2 == 0;

                    if (j+1 < m) {
                        c2 = map[i][j+1];
                        n2 = i*m+j+1;
                        if (even) {
                            graph.AddEdge(n1, n2, -priceTable[c1-'A', c2-'A'], 1);
                        } else {
                            graph.AddEdge(n2, n1, -priceTable[c1-'A', c2-'A'], 1);
                        }
                    }

                    if (i+1 < n) {
                        c2 = map[i+1][j];
                        n2 = (i+1)*m+j;
                        if (even) {
                            graph.AddEdge(n1, n2, -priceTable[c1-'A', c2-'A'], 1);
                        } else {
                            graph.AddEdge(n2, n1, -priceTable[c1-'A', c2-'A'], 1);
                        }
                    }

                    if (even) {
                        graph.AddEdge(srcNeck, n1, 0, 1);
                    } else {
                        graph.AddEdge(n1, dst, 0, 1);
                    }
                }
            }

            graph.AddEdge(src, srcNeck, 0, n*m/2);
            graph.AddEdge(srcNeck, dst, 0, Graph.Inf);

            Console.WriteLine(-graph.GetMinCostMaxFlow(src, dst).TotalCost);
        }
    }
}

#elif other2
using System.IO;
using System.Text;
using System;

class Programs
{
    static StreamReader sr = new StreamReader(Console.OpenStandardInput(), Encoding.Default);
    static StreamWriter sw = new StreamWriter(Console.OpenStandardOutput(), Encoding.Default);
    static int n, m;
    static int[,] rank = { {10, 8,7,5,1 }, { 8, 6, 4, 3, 1 }, { 7, 4, 3, 2, 1 }, { 5, 3, 2, 2, 1 }, { 1, 1, 1, 1, 0 }};
    static int[,] dp = new int[14 * 14, 1 << 14];
    static int[] arr;
    //예
    /*
2 3
AAF
FAA
     */
    static int DFS(int num,int visit)
    {
        //똑같이 격자판 채우기처럼 1차배열의 끝에 도달하면.. 이번에는 꽉채우는게 아니기 때문에 마지막 두부가 체크가 안되어 있을수도 있다.
        if(num==n*m-1)
        {
            //가로인경우 1x2일때 시작부분에서만 등급을 매겨서 초기화하기 때문에 2번째에서는 등급을 매겨서 리턴할 필요없이 0만 리턴하면 될 듯함.
            return 0;
        }
        //타일을 만들 때 이미 등급을 부여함.
        if(dp[num,visit]!=-1)
        {
            return dp[num, visit];//처음 방문하지 않았다면 해당하는 값을 리턴함
        }
        dp[num, visit] = 0;//방문 초기화
        int next = visit & 1;
        int horizontal = 0, vertical = 0;
        if(next==1)
        {
            //이미 체크되었기 때문에 다음 번호로 진행
            dp[num, visit] += DFS(num + 1, visit >> 1);//그전번호를 지운다.
        }
        else
        {
            //최대합이기 때문에 비교연산으로 최대값만 리턴해야됨.
            //가로 진행이 가능한 경우 3
            next = 3;
            if(num%m+1<m&&(visit&next)==0)//체크가 안된 경우만
            {
                horizontal = rank[arr[num], arr[num + 1]];
               horizontal += DFS(num + 1, (visit | next) >> 1);
            }
            //아래로 진행이 가능한 경우
            next = (1 << m) | 1;
            if(num/m+1<n&&(visit&next)==0)
            {
                vertical = rank[arr[num], arr[num + m]];
               vertical += DFS(num + 1, (visit | next) >> 1);
            }
            //비우고 넘어감
            dp[num, visit] += DFS(num + 1, visit >> 1);
        }
        return dp[num, visit]=Math.Max(Math.Max(horizontal,vertical),dp[num,visit]);
    }
    static void Main(String[] args)
    {
      string[] str= sr.ReadLine().Split();
        n = int.Parse(str[0]);
        m = int.Parse(str[1]);
        for (int i = 0; i < n*m; i++)
        {
            for (int j = 0; j < (1<<14); j++)
            {
                dp[i, j] = -1;
            }
        }
        arr = new int[n*m];
        for (int i = 0; i < n; i++)
        {
           string s = sr.ReadLine();
            for (int j = 0; j < m; j++)
            {
                if(s[j]=='F')
                {
                    arr[j + m * i] = 4;
                }
                else
                {
            arr[j+m*i] = Convert.ToInt32(s[j]-'A');

                }
            }
        }
        //이건 근데 목적이 2x1로 가득채우는 게 아니라 중간에 1이 껴있을 수도 있음
        //채울 때 1개를 버리는 것도 포함해서 완전 탐색하면 될 듯?
      sw.Write(DFS(0, 0));
        sw.Dispose();
    }
}

#elif other3
import static java.lang.Integer.*;

import java.io.*;
import java.util.*;

public class Main {

    static int N,M,length , source , sink , networkFlow;
    static int[] dx = {-1,1,0,0} , dy = {0,0,-1,1};
    static ArrayList<Integer>[] graph;
    static int[][] capacity , flow , cost;
    static char[][] dubu;
    static int[] distance,pre;
    static boolean[] visited;

    static int transferDubu(int x1 , int y1 , int x2 , int y2) {
        if (dubu[x1][y1]=='A' && dubu[x2][y2]=='A') return 10;
        if ( (dubu[x1][y1]=='A' && dubu[x2][y2]=='B') || (dubu[x1][y1]=='B' && dubu[x2][y2]=='A') ) return 8;
        if ( (dubu[x1][y1]=='A' && dubu[x2][y2]=='C') || (dubu[x1][y1]=='C' && dubu[x2][y2]=='A') ) return 7;
        if ( (dubu[x1][y1]=='A' && dubu[x2][y2]=='D') || (dubu[x1][y1]=='D' && dubu[x2][y2]=='A') ) return 5;
        if ( (dubu[x1][y1]=='A' && dubu[x2][y2]=='F') || (dubu[x1][y1]=='F' && dubu[x2][y2]=='A') ) return 1;


        if (dubu[x1][y1]=='B' && dubu[x2][y2]=='B') return 6;
        if ( (dubu[x1][y1]=='B' && dubu[x2][y2]=='C') || (dubu[x1][y1]=='C' && dubu[x2][y2]=='B') ) return 4;
        if ( (dubu[x1][y1]=='B' && dubu[x2][y2]=='D') || (dubu[x1][y1]=='D' && dubu[x2][y2]=='B') ) return 3;
        if ( (dubu[x1][y1]=='B' && dubu[x2][y2]=='F') || (dubu[x1][y1]=='F' && dubu[x2][y2]=='B') ) return 1;

        if (dubu[x1][y1]=='C' && dubu[x2][y2]=='C') return 3;
        if ( (dubu[x1][y1]=='C' && dubu[x2][y2]=='D') || (dubu[x1][y1]=='D' && dubu[x2][y2]=='C') ) return 2;
        if ( (dubu[x1][y1]=='C' && dubu[x2][y2]=='F') || (dubu[x1][y1]=='F' && dubu[x2][y2]=='C') ) return 1;

        if (dubu[x1][y1]=='D' && dubu[x2][y2]=='D') return 2;
        if ( (dubu[x1][y1]=='D' && dubu[x2][y2]=='F') || (dubu[x1][y1]=='F' && dubu[x2][y2]=='D') ) return 1;

        return 0;
    }
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(System.out));
        StringTokenizer st = new StringTokenizer(br.readLine());
        /**
         * 0 : source
         * odd: groupA
         * even : group B
         * 0<odd,even<=N*N;
         * N*N+1 : sink
         *
         */

        N = parseInt(st.nextToken());
        M = parseInt(st.nextToken());
        length = N*M+2;
        source = 0;
        sink = N*M+1;
        dubu = new char[N+1][M+1];
        graph = new ArrayList[length];
        capacity = new int[length][length];
        cost = new int[length][length];
        flow = new int[length][length];
        distance = new int[length];
        visited = new boolean[length];
        pre = new int[length];

        for (int i = 0 ; i < length;  i++) {
            graph[i] = new ArrayList<>();
        }


        int count = 1;
        int[][] number = new int[N+1][M+1];
        for (int i = 0 ; i < N ; i++) {
            String line = br.readLine();
            for (int j = 0 ; j < M ; j++) {
                dubu[i][j] = line.charAt(j);
                number[i][j] = count;
                count++;
            }
        }


        /**
         * source -> group A
         */
        for (int i = 0 ; i < N ; i++) {
            for (int j = 0 ; j < M ; j++) {
                if ((i+j)%2==0) {
                    graph[source].add(number[i][j]);
                    graph[number[i][j]].add(source);
                    capacity[source][number[i][j]] = 1;
                }
            }
        }

        /**
         * group A - > group B
         */
        for (int i = 0 ; i < N ; i++) {
            for (int j = 0; j < M ; j++) {
                if ((i+j)%2==0) {
                    for (int k = 0 ; k < 4 ; k++) {
                        int nx = i+dx[k];
                        int ny = j+dy[k];
                        if (nx>=0 && nx<N && ny>=0 && ny<M) {
                            graph[number[i][j]].add(number[nx][ny]);
                            graph[number[nx][ny]].add(number[i][j]);
                            capacity[number[i][j]][number[nx][ny]] = 1;
                            cost[number[i][j]][number[nx][ny]] = -transferDubu(i,j,nx,ny);
                            cost[number[nx][ny]][number[i][j]] = transferDubu(i,j,nx,ny);
                            // 최대비용 최대유량이기 때문에 cost 를 반대로 더해줌
                        }
                    }
                }

            }
        }

        /**
         * group B - > sink
         */
        for (int i = 0 ; i < N ; i++) {
            for (int j = 0 ; j < M ; j++) {
                if ((i+j)%2==1) {
                    graph[number[i][j]].add(sink);
                    graph[sink].add(number[i][j]);
                    capacity[number[i][j]][sink] = 1;
                }
            }
        }

        bw.write(Integer.toString(MCMF()));
        bw.flush();
    }

    static int MCMF() {
        int check=0,ans=0;
        while (true) {
            Queue<Integer> Q = new LinkedList<>();
            Q.add(source);
            Arrays.fill(visited, false);
            Arrays.fill(pre , -1);
            Arrays.fill(distance, MAX_VALUE);
            distance[source] = 0;
            visited[source] = true;

            while (!Q.isEmpty()) {
                int node = Q.poll();
                visited[node] = false;
                for (Integer nextNode : graph[node]) {
                    if (capacity[node][nextNode] - flow[node][nextNode] > 0
                        && distance[node] + cost[node][nextNode] < distance[nextNode]) {
                        distance[nextNode] = distance[node] + cost[node][nextNode];
                        pre[nextNode] = node;
                        if (!visited[nextNode]) {
                            visited[nextNode] = true;
                            Q.add(nextNode);
                        }
                    }
                }
            }

            if (pre[sink] == -1) break;

            networkFlow = MAX_VALUE;

            for (int i = sink ; i!=source ; i=pre[i]) {
                networkFlow = Math.min(networkFlow, capacity[pre[i]][i] - flow[pre[i]][i]);
            }

            for (int i = sink;  i!=source ; i=pre[i]) {
                check += networkFlow*cost[pre[i]][i];
                flow[pre[i]][i] += networkFlow;
                flow[i][pre[i]] -= networkFlow;
            }
            if (check<=ans) {
                ans=check;
            } else {
                break;
            }
        }
        return -ans; // 최대비용 최대유량
    }
}

/*
4
CAAC
AAAA
AAAA
AAAA

ans = 700
 */

#endif
}
