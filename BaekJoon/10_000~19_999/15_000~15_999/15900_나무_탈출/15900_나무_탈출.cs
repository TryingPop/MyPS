using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 23
이름 : 배성훈
내용 : 나무탈출
    문제번호 : 15900번

    트리, DFS 문제다
    DFS로 하고 싶었으나 리프에서 시작하는 DFS는 떠오르지 않아서
    정점에서 시작하는 BFS로 먼저 풀었다

    리프에서 시작하는 경우 한번에 완전탐색이 불가능하기 때문이다
    풀고나서 정점에서 시작하면 DFS가 되겠네라 떠올렸고,
    DFS로도 풀어봤으나 메모리만 더 먹을뿐, 시간 개선은 안된다

        시간      - BFS : 568ms,       DFS : 576ms
        메모리    - BFS : 62292KB,     DFS : 71364KB

    리프 판정은 연결된 간선의 개수로 했다
*/

namespace BaekJoon.etc
{
    internal class etc_0336
    {

        static void Main336(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 1024 * 1024);

            int n = ReadInt();

            int[] conn = new int[n + 1];
            int[] dp = new int[n + 1];
            List<int>[] lines = new List<int>[n + 1];
            for (int i = 1; i <= n; i++)
            {

                lines[i] = new();
            }

            for (int i = 0; i < n - 1; i++)
            {

                int f = ReadInt();
                int b = ReadInt();

                conn[f]++;
                conn[b]++;

                lines[f].Add(b);
                lines[b].Add(f);
            }

            sr.Close();

            Queue<int> q = new(n);
            q.Enqueue(1);
            dp[1] = 1;
            while (q.Count > 0)
            {

                int node = q.Dequeue();

                for (int i = 0; i < lines[node].Count; i++)
                {

                    int next = lines[node][i];
                    if (dp[next] != 0) continue;
                    dp[next] = dp[node] + 1;
                    q.Enqueue(next);
                }
            }

            int sum = 0;
            for (int i = 2; i <= n; i++)
            {

                if (conn[i] > 1) continue;
                sum += dp[i] - 1;
                sum %= 2;
            }

            if (sum == 1) Console.WriteLine("Yes");
            else Console.WriteLine("No");

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
using System.Text;

public class Program
{
    private static int N;
    public static List<int>[] connections;
    public static bool[] passed;

    public static void Main(string[] args)
    {
        N = int.Parse(Console.ReadLine());
        passed = new bool[N + 1];
        connections = new List<int>[N + 1];
        for(int i = 1; i <= N; i++)
        {
            connections[i] = new List<int>();
        }

        for (int i = 0; i < N - 1; i++)
        {
            string[] s = Console.ReadLine().Split();
            int x = int.Parse(s[0]);
            int y = int.Parse(s[1]);

            connections[x].Add(y);
            connections[y].Add(x);
        }

        DFS(1, 0);

        if (total % 2 == 0)
            Console.WriteLine("No");
        else
            Console.WriteLine("Yes");
    }

    private static int total;
    private static void DFS(int x, int depth)
    {
        passed[x] = true;


        List<int> con = connections[x];
        foreach (int i in con)
        {
            if (passed[i])
                continue;

            DFS(i, depth + 1);
        }

        if (con.Count == 1)
        {
            // Leaf node
            total += depth;
        }

        passed[x] = false;
    }
}

#endif
}
