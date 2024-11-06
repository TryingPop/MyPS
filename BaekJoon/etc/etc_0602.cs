using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 23
이름 : 배성훈
내용 : 사랑의 큐피드
    문제번호 : 30976번

    이분 매칭 문제다
    경우의 수가 300, 300이므로, 완전탐색 300 * 300 = 90_000으로
    간선을 놓고 이분 매칭을 진행해서 풀었다
    초과, 미만 부분을 이상, 이하로 해서 3번 틀렸다
    그리고 인덱스 에러로 1번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0602
    {

        static void Main602(string[] args)
        {

            StreamReader sr;

            int n, m;
            int[] match;
            bool[] visit;

            List<int>[] line;

            Solve();

            void Solve()
            {

                Input();

                int ret = 0;
                for (int i = 1; i <= n; i++)
                {

                    Array.Fill(visit, false);
                    if (DFS(i)) ret++;
                }

                Console.WriteLine(ret);
            }

            void Input()
            {

                sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
                n = ReadInt();
                m = ReadInt();
                int[] girl = new int[n + 1];
                line = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    girl[i] = ReadInt();
                    line[i] = new();
                }

                int[] boy = new int[m + 1];
                for (int i = 1; i <= m; i++)
                {

                    boy[i] = ReadInt();
                }

                int[] want = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {
                    want[i] = ReadInt();
                }

                for (int b = 1; b <= m; b++)
                {

                    int cur = ReadInt();

                    for (int g = 1; g <= n; g++)
                    {

                        if (boy[b] >= want[g] || girl[g] <= cur) continue;
                        line[g].Add(b);
                    }
                }

                match = new int[m + 1];
                visit = new bool[m + 1];

                sr.Close();
            }

            bool DFS(int _n)
            {

                for (int i = 0; i < line[_n].Count; i++)
                {

                    int next = line[_n][i];
                    if (visit[next]) continue;
                    visit[next] = true;

                    if (match[next] == 0 || DFS(match[next]))
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
def DFS(nx):
    for mx in G[nx]:
        if not check[mx]:
            check[mx] = 1
            if V[mx] == -1 or DFS(V[mx]):
                V[mx] = nx
                return True
    return False

N, M = map(int, input().split())
G = [[] for _ in range(N + 1)]
g = list(map(int, input().split()))
b = list(map(int, input().split()))
l = list(map(int, input().split()))
u = list(map(int, input().split()))
for i in range(N):
    for j in range(M):
        if b[j] < l[i] and g[i] > u[j]:
            G[i].append(j)
V = [-1] * (M + 1)
ans = 0
for i in range(N):
    check = [0] * M
    if DFS(i):
        ans += 1
print(ans)
#elif other2
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.*;

public class Main {
    static boolean[] visit;
    static int[] connect;
    static ArrayDeque<Integer>[] list;

    static boolean match(int n) {
        if (visit[n])
            return false;
        visit[n] = true;
        Iterator<Integer> I = list[n].iterator();
        int v;
        while (I.hasNext()) {
            v = I.next();
            if (connect[v] == 0 || match(connect[v])) {
                connect[v] = n;
                return true;
            }
        }
        return false;
    }

    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        StringTokenizer st = new StringTokenizer(br.readLine());
        int N = Integer.parseInt(st.nextToken()), M = Integer.parseInt(st.nextToken());
        int[] G = new int[N + 1], B = new int[M + 1], L = new int[N + 1], U = new int[M + 1];
        list = new ArrayDeque[N + 1];
        for (int i = 1; i <= N; i++)
            list[i] = new ArrayDeque<>();
        st = new StringTokenizer(br.readLine());
        for (int i = 1; i <= N; i++)
            G[i] = Integer.parseInt(st.nextToken());
        st = new StringTokenizer(br.readLine());
        for (int i = 1; i <= M; i++)
            B[i] = Integer.parseInt(st.nextToken());
        st = new StringTokenizer(br.readLine());
        for (int i = 1; i <= N; i++)
            L[i] = Integer.parseInt(st.nextToken());
        st = new StringTokenizer(br.readLine());
        for (int i = 1; i <= M; i++)
            U[i] = Integer.parseInt(st.nextToken());
        for (int i = 1; i <= N; i++)
            for (int j = 1; j <= M; j++)
                if (G[i] > U[j] && B[j] < L[i])
                    list[i].addLast(j);
        int ans = 0;
        connect = new int[M + 1];
        for (int i = 1; i <= N; i++) {
            visit = new boolean[N + 1];
            if (match(i))
                ans++;
        }
        System.out.println(ans);
    }
}
#endif
}
