using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 2
이름 : 배성훈
내용 : 효율적인 해킹
    문제번호 : 1325번

    BFS, DFS 문제다
    타잔 알고리즘을 이용해 SCC로 만든 뒤에 SCC간에 간선을 다시 분배하고 방문 탐색을 했다
    처음에는 모든 그룹을 갱신하면서 탐색하니 5.5초가 나왔다
    SCC 간선도 직접 이어주기에 위상정렬해서 degree가 0인 부분만 탐색하니 4초대로 줄었다
    그런데, 다른 사람풀이를 보니 그냥 BFS랑 별 차이없어 안좋은 코드같다..;

    코사라주로도 접근시도해봤으나, 간선 잇는건 같아보여서 역방향, 정방향 간선 + 스택으로
    메모리만 더 먹을거 같아 해당 방법은 안썼다
*/

namespace BaekJoon.etc
{
    internal class etc_0672
    {

        static void Main672(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            HashSet<int>[] line;
            HashSet<int>[] gLine;
            bool[] visit;
            int[] id;
            int[] groups;
            int[] cnt;
            int gLen = 0;
            int idLen = 0;

            Stack<int> s;
            int n;
            int max = 0;
            int[] dp;
            Solve();

            void Solve()
            {

                Input();
                SCC();

                visit = new bool[gLen + 1];
                gLine = new HashSet<int>[gLen + 1];
                dp = new int[gLen + 1];
                for (int i = 1; i <= gLen; i++)
                {

                    gLine[i] = new();
                }

                int[] degree = new int[gLen + 1];
                for (int i = 1; i <= n; i++)
                {

                    int f = groups[i];
                    foreach (int next in line[i])
                    {

                        int b = groups[next];
                        if (f == b || gLine[f].Contains(b)) continue;
                        gLine[f].Add(b);
                        degree[b]++;
                    }
                }

                for (int i = 1; i <= gLen; i++)
                {

                    if (degree[i] != 0) continue;
                    Array.Fill(visit, false, 1, gLen);
                    int cur = DFS(i);
                    dp[i] = cur;
                    max = max < cur ? cur : max;
                }

                Output();
            }
            
            void Output()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 1; i <= n; i++)
                {

                    int g = groups[i];
                    if (dp[g] == max) sw.Write($"{i} ");
                }

                sw.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);

                n = ReadInt();
                int m = ReadInt();

                line = new HashSet<int>[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    line[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    line[b].Add(f);
                }

                sr.Close();
            }

            void SCC()
            {

                cnt = new int[n + 1];
                groups = new int[n + 1];
                id = new int[n + 1];
                Array.Fill(id, -1);
                s = new(n);

                for (int i = 1; i <= n; i++)
                {

                    Tarjan(i);
                }
            }

            int DFS(int _n)
            {

                int ret = cnt[_n];

                foreach (int next in gLine[_n])
                {

                    if (visit[next]) continue;
                    visit[next] = true;
                    ret += DFS(next);
                }

                return ret;
            }

            int Tarjan(int _n)
            {

                if (id[_n] != -1) return -1;
                id[_n] = idLen++;
                s.Push(_n);

                int parent = id[_n];
                int ret = _n;
                
                foreach (int next in line[_n])
                {

                    if (id[next] != -1)
                    {

                        if (groups[next] == 0 && id[next] < id[ret]) ret = next;
                        continue;
                    }

                    int chk = Tarjan(next);

                    if (id[chk] < id[ret]) ret = chk;
                }

                if (ret == _n)
                {

                    gLen++;
                    while(s.Count > 0)
                    {

                        int next = s.Pop();
                        groups[next] = gLen;
                        cnt[gLen]++;
                        if (next == _n) break;
                    }
                }

                return ret;
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
public static class PS
{
    private static int n;
    private static List<int>[] nodes;

    static PS()
    {
        string[] input;
        int m, node1, node2;

        input = Console.ReadLine().Split();
        n = int.Parse(input[0]);
        m = int.Parse(input[1]);

        nodes = new List<int>[n + 1];

        for (int i = 1; i <= n; i++)
        {
            nodes[i] = new();
        }

        for (int i = 1; i <= m; i++)
        {
            input = Console.ReadLine().Split();
            node1 = int.Parse(input[0]);
            node2 = int.Parse(input[1]);

            nodes[node2].Add(node1);
        }
    }

    public static void Main()
    {
        List<int> ans = new();

        int max = 0;
        int temp;

        for (int i = 1; i <= n; i++)
        {
            temp = BFS(i);

            if (temp > max)
            {
                max = temp;
                ans.Clear();
                ans.Add(i);
            }
            else if (temp == max)
            {
                ans.Add(i);
            }
        }
        
        Console.Write(String.Join(' ', ans));
    }

    private static int BFS(int start)
    {
        Queue<int> queue = new();
        bool[] visited = new bool[n + 1];

        queue.Enqueue(start);
        visited[start] = true;
        
        int cur;
        int cnt = 0;

        while (queue.Count > 0)
        {
            cur = queue.Dequeue();
            cnt++;

            foreach (int neighbor in nodes[cur])
            {
                if (!visited[neighbor])
                {
                    queue.Enqueue(neighbor);
                    visited[neighbor] = true;
                }
            }
        }

        return cnt;
    }
}
#elif other2
public static class PS
{
    private static int n;
    private static List<int>[] nodes;

    static PS()
    {
        string[] input;
        int m, node1, node2;

        input = Console.ReadLine().Split();
        n = int.Parse(input[0]);
        m = int.Parse(input[1]);

        nodes = new List<int>[n + 1];

        for (int i = 1; i <= n; i++)
        {
            nodes[i] = new();
        }

        for (int i = 1; i <= m; i++)
        {
            input = Console.ReadLine().Split();
            node1 = int.Parse(input[0]);
            node2 = int.Parse(input[1]);

            nodes[node2].Add(node1);
        }
    }

    public static void Main()
    {
        List<int> ans = new();

        int max = 0;
        int temp;

        for (int i = 1; i <= n; i++)
        {
            temp = BFS(i);

            if (temp > max)
            {
                max = temp;
                ans.Clear();
                ans.Add(i);
            }
            else if (temp == max)
            {
                ans.Add(i);
            }
        }
        
        Console.Write(String.Join(' ', ans));
    }

    private static int BFS(int start)
    {
        Queue<int> queue = new();
        bool[] visited = new bool[n + 1];

        queue.Enqueue(start);
        visited[start] = true;
        
        int cur;
        int cnt = 0;

        while (queue.Count > 0)
        {
            cur = queue.Dequeue();
            cnt++;

            foreach (int neighbor in nodes[cur])
            {
                if (!visited[neighbor])
                {
                    queue.Enqueue(neighbor);
                    visited[neighbor] = true;
                }
            }
        }

        return cnt;
    }
}
#elif other3
// #include <cstdio>
// #include <vector>
// #include <stack>
// #include <algorithm>

std::vector<int> c[10001], SCC[10001];
std::stack<int> s;
int cid[10001], id, nsid[10001], sid, cntsid[10001], D[10001];
bool isChild[10001], visited[10001];

inline int dfs(int node) {
    cid[node] = ++id;
    s.push(node);

    int ret = cid[node];
    for (int next : c[node]) {
        if (!cid[next]) ret = std::min(ret, dfs(next));
        else if (!nsid[next]) ret = std::min(ret, cid[next]);
    }

    if (ret == cid[node]) {
        sid++;
        while (true) {
            int t = s.top();
            s.pop();
            nsid[t] = sid;
            cntsid[sid]++;
            if (cid[t] == ret) break;
        }
    }
    return ret;
}

inline int dfs2(int idx) {
    visited[idx] = true;
    int ret = cntsid[idx];
    for (int cost : SCC[idx]) {
        if (!visited[cost]) ret += dfs2(cost);
    }
    return ret;
}

int main(void) {
    int N, M, A, B; scanf("%d %d", &N, &M);
    while (M--) {
        scanf("%d %d", &A, &B);
        c[B].push_back(A);
    }

    for (int i = 1; i <= N; i++) {
        if (!cid[i]) dfs(i);
    }

    for (int i = 1; i <= N; i++) {
        for (int next : c[i]) {
            if (nsid[i] != nsid[next]) SCC[nsid[i]].push_back(nsid[next]);
        }
    }

    for (int i = 1; i <= sid; i++) {
        std::sort(SCC[i].begin(), SCC[i].end());
        SCC[i].erase(std::unique(SCC[i].begin(), SCC[i].end()), SCC[i].end());

        for (int node : SCC[i]) isChild[node] = true;
    }

    int imax = 0;
    for (int i = 1; i <= sid; i++) {
        for (int j = 1; j <= sid; j++) visited[j] = false;
        if (!isChild[i]) {
            imax = std::max(imax, D[i] = dfs2(i));
        }
    }

    for (int i = 1; i <= N; i++) {
        if (D[nsid[i]] == imax) printf("%d ", i);
    }
}
#endif
}
