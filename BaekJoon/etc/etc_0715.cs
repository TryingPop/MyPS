using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 22
이름 : 배성훈
내용 : 영과일 학회방
    문제번호 : 16726번

    이분 매칭 문제다
    호프크로프트 카프 알고리즘으로 해결했다

    아이디어는 다음과 같다
    1x1로 놓을 수 있는 칸을 모두 찾는다
    그리고 1x2(2x1도 포함)로 대체 가능한지 확인한다
    1x2로 대체 가능하면 2개가 합쳐지는 것이므로 1개씩 뺀다
    1x2는 홀짝 칸에 간선을 연결해 배치할 수 있는지 확인했다
*/

namespace BaekJoon.etc
{
    internal class etc_0715
    {

        static void Main715(string[] args)
        {

            int INF = 10_000;
            StreamReader sr;
            
            int row, col;
            int[][] board;

            int len1;
            int len2;

            List<int>[] line;
            int ret;

            int[] A;
            int[] B;
            int[] lvl;

            bool[] visit;
            Queue<int> q;

            Solve();

            void Solve()
            {

                Input();

                LinkLine();

                while (true)
                {

                    BFS();

                    int match = 0;
                    for (int i = 0; i < len1; i++)
                    {

                        if (!visit[i] && DFS(i)) match++;
                    }

                    if (match == 0) break;
                    ret -= match;
                }

                Console.WriteLine(ret);
            }

            void BFS()
            {

                for (int i = 0; i < len1; i++)
                {

                    if (!visit[i])
                    {

                        lvl[i] = 0;
                        q.Enqueue(i);
                    }
                    else lvl[i] = INF;
                }

                while (q.Count > 0)
                {

                    int a = q.Dequeue();

                    for (int i = 0; i < line[a].Count; i++)
                    {

                        int b = line[a][i];
                        if (B[b] != -1 && lvl[B[b]] == INF)
                        {

                            lvl[B[b]] = lvl[a] + 1;
                            q.Enqueue(B[b]);
                        }
                    }
                }
            }

            bool DFS(int _a)
            {

                for (int i = 0; i < line[_a].Count; i++)
                {

                    int b = line[_a][i];

                    if (B[b] == -1 || lvl[B[b]] == lvl[_a] + 1 && DFS(B[b]))
                    {

                        visit[_a] = true;
                        A[_a] = b;
                        B[b] = _a;
                        return true;
                    }
                }

                return false;
            }

            void LinkLine()
            {

                line = new List<int>[len1];

                for (int i = 0; i < len1; i++)
                {

                    line[i] = new();
                }

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c] == -1 || (r + c) % 2 == 1) continue;

                        int f = board[r][c];
                        if (r < row - 1 && board[r + 1][c] != -1) line[f].Add(board[r + 1][c]);
                        if (0 < r && board[r - 1][c] != -1) line[f].Add(board[r - 1][c]);

                        if (c < col - 1 && board[r][c + 1] != -1) line[f].Add(board[r][c + 1]);
                        if (0 < c && board[r][c - 1] != -1) line[f].Add(board[r][c - 1]);
                    }
                }

                visit = new bool[len1];
                A = new int[len1];
                B = new int[len2];
                lvl = new int[len1];

                Array.Fill(A, -1);
                Array.Fill(B, -1);

                q = new(len1);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                board = new int[row][];

                len1 = 0;
                len2 = 0;

                ret = 0;

                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        int cur = sr.Read();
                        if (cur == '.')
                        {

                            ret++;
                            if ((r + c) % 2 == 0) board[r][c] = len1++;
                            else board[r][c] = len2++;
                        }
                        else board[r][c] = -1;
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
// #include <cstdio>
// #include <vector>
// #include <algorithm>
using namespace std;

vector<vector<int>> graph(2501);
vector<bool> visited(2501, false), tile(2501, false);
int B[2501];
char m[50][51];
int N, M;
int match;

bool range(int x, int y)
{
    return 0<=x && x<M && 0<=y && y<N;
}

int num(int x, int y)
{
    return x+y*M+1;
}

void connect(int x1, int y1, int x2, int y2)
{
    if(range(x2, y2) && m[y2][x2] != 'X')
        graph[num(x1, y1)].push_back(num(x2, y2));
}

bool dfs(int a)
{
    visited[a] = true;

    for(int b: graph[a])
    {
        if(!B[b] || (!visited[B[b]] && dfs(B[b])))
        {
            B[b] = a;
            return true;
        }
    }

    return false;
}

int main()
{
    scanf("%d %d", &N, &M);

    for(int y=0; y<N; y++)
        scanf("%s", m[y]);

    bool chess = true;

    for(int y=0; y<N; y++)
    {
        for(int x=0; x<M; x++)
        {
            if(chess && m[y][x] != 'X')
            {
                connect(x, y, x-1, y);
                connect(x, y, x+1, y);
                connect(x, y, x, y-1);
                connect(x, y, x, y+1);
            }

            if(m[y][x] == 'X')
                tile[num(x, y)] = true;

            chess = !chess;
        }

        if(M%2 == 0)
            chess = !chess;
    }

    chess = true;

    for(int y=0; y<N; y++)
    {
        for(int x=0; x<M; x++)
        {
            if(chess && m[y][x] != 'X')
            {
                fill(visited.begin(), visited.end(), false);

                if(dfs(num(x, y)))
                    match++;
            }

            chess = !chess;
        }

        if(M%2 == 0)
            chess = !chess;
    }

    for(int b=1; b<=N*M; b++)
    {
        if(B[b])
        {
            tile[b] = true;
            tile[B[b]] = true;
        }
    }

    for(int i=1; i<=N*M; i++)
        if(!tile[i])
            match++;

    printf("%d", match);

    return 0;
}

#elif other2
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.StringTokenizer;

public class Main {
    static ArrayList<Integer>[] list;
    static boolean[] visit;
    static int[] connect;

    static boolean match(int u) {
        if (visit[u])
            return false;
        visit[u] = true;
        int v;
        Iterator<Integer> I = list[u].iterator();
        while (I.hasNext()) {
            v = I.next();
            if (connect[v] == 0 || match(connect[v])) {
                connect[v] = u;
                return true;
            }
        }
        return false;
    }

    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader((new InputStreamReader(System.in)));
        StringTokenizer st = new StringTokenizer(br.readLine());
        int N = Integer.parseInt(st.nextToken());
        int M = Integer.parseInt(st.nextToken());
        char[][] map = new char[N + 2][M + 2];
        for (int i = 0; i <= N + 1; i++)
            map[i][0] = map[i][M + 1] = 'X';
        for (int j = 0; j <= M + 1; j++)
            map[0][j] = map[N + 1][j] = 'X';
        int[][] odd = new int[N + 2][M + 2];
        int[][] even = new int[N + 2][M + 2];
        int O = 0, E = 0;
        String s;
        for (int i = 1; i <= N; i++) {
            s = br.readLine();
            for (int j = 1; j <= M; j++) {
                map[i][j] = s.charAt(j - 1);
                if (map[i][j] == '.') {
                    if ((i + j) % 2 != 0)
                        odd[i][j] = ++O;
                    else
                        even[i][j] = ++E;
                }
            }
        }
        list = new ArrayList[O + 1];
        for (int i = 1; i <= O; i++)
            list[i] = new ArrayList<>();
        for (int i = 1; i <= N; i++)
            for (int j = 1; j <= M; j++) {
                if ((i + j) % 2 != 0 && odd[i][j] > 0) {
                    if (even[i - 1][j] > 0)
                        list[odd[i][j]].add(even[i - 1][j]);
                    if (even[i + 1][j] > 0)
                        list[odd[i][j]].add(even[i + 1][j]);
                    if (even[i][j - 1] > 0)
                        list[odd[i][j]].add(even[i][j - 1]);
                    if (even[i][j + 1] > 0)
                        list[odd[i][j]].add(even[i][j + 1]);
                }
            }
        int ans = 0;
        visit = new boolean[O + 1];
        connect = new int[E + 1];
        for (int i = 1; i <= O; i++) {
            for (int j = 1; j <= O; j++)
                visit[j] = false;
            if (match(i))
                ans++;
        }
        System.out.println((O + E - ans * 2) + ans);
    }
}
#elif other3
def dfs(x):
    visited[x]=1
    for nx in path[x]:
        if odd[nx]==-1 or (not visited[odd[nx]] and dfs(odd[nx])):
            even[x]=nx
            odd[nx]=x
            return 1
    return 0
n,m=map(int,input().split())
D=[input() for _ in range(n)]
Num=[[-1]*m for _ in range(n)]
res=0
a,b=0,0
A,B=[],[]
for i in range(n*m):
    x,y=i//m,i%m
    if D[x][y]=='.':
        if (x+y)%2==0:
            Num[x][y]=a;a+=1
            A.append([x,y])
        else:
            Num[x][y]=b;b+=1
            B.append([x,y])
        res+=1
path=[[] for _ in range(a)]
dx,dy=[0,0,1,-1],[1,-1,0,0]
for i in range(a):
    x,y=A[i]
    for j in range(4):
        nx,ny=x+dx[j],y+dy[j]
        if 0<=nx<n and 0<=ny<m and D[nx][ny]=='.':
            path[i].append(Num[nx][ny])
even=[-1]*a
odd=[-1]*b
for i in range(a):
    if even[i]==-1:
        visited=[0]*a
        if dfs(i):res-=1
print(res)
#endif
}
