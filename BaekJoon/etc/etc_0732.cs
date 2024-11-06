using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 27
이름 : 배성훈
내용 : 인구 이동
    문제번호 : 16234번

    구현, 시뮬레이션, BFS 문제다
    연합국 찾는건 BFS로 만들기 힘들어 DFS로 구현하니 쉽게되어 DFS로 구현했다
    그리고 연합국에 대해 계산은 BFS로 했다
    이렇게 제출하니 192ms로 통과되었다
*/

namespace BaekJoon.etc
{
    internal class etc_0732
    {

        static void Main732(string[] args)
        {

            StreamReader sr;
            int[][] board;
            int size;

            int min, max;

            Queue<(int r, int c)> q;

            bool[][] visit;
            int[] dirR, dirC;

            Solve();

            void Solve()
            {

                Input();

                int ret = GetRet();

                Console.WriteLine(ret);
            }

            int GetRet()
            {

                int ret = 0;
                while (true)
                {

                    bool stop = true;

                    for (int r = 0; r < size; r++)
                    {

                        for (int c = 0; c < size; c++)
                        {

                            if (visit[r][c]) continue;
                            // 연결
                            int total = DFS(r, c);

                            if (q.Count == 1)
                            {

                                q.Clear();
                                continue;
                            }

                            // 이동
                            stop = false;
                            BFS(total);
                        }
                    }

                    if (stop) break;

                    ret++;
                    for (int r = 0; r < size; r++)
                    {

                        for (int c = 0; c< size; c++)
                        {

                            visit[r][c] = false;
                        }
                    }
                }

                return ret;
            }

            int DFS(int _r, int _c)
            {

                visit[_r][_c] = true;
                q.Enqueue((_r, _c));

                int cur = board[_r][_c];
                int ret = board[_r][_c];
                for (int i = 0; i < 4; i++)
                {

                    int nextR = _r + dirR[i];
                    int nextC = _c + dirC[i];

                    if (ChkInvalidPos(nextR, nextC) || visit[nextR][nextC]) continue;
                    int next = board[nextR][nextC];

                    if (ChkDiff(cur, next)) ret += DFS(nextR, nextC);
                }

                return ret;
            }

            void BFS(int total)
            {

                int change = total / q.Count;
                while(q.Count > 0)
                {

                    var node = q.Dequeue();
                    board[node.r][node.c] = change;
                }
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _c >= size || _r >= size) return true;
                return false;
            }

            bool ChkDiff(int _x, int _y)
            {

                int diff = _x - _y;
                diff = diff < 0 ? -diff : diff;

                if (min <= diff && diff <= max) return true;
                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                size = ReadInt();

                min = ReadInt();
                max = ReadInt();

                board = new int[size][];
                visit = new bool[size][];
                for (int r = 0; r < size; r++)
                {

                    board[r] = new int[size];
                    visit[r] = new bool[size];

                    for (int c = 0; c < size; c++)
                    {

                        board[r][c] = ReadInt();
                    }
                }

                q = new(size * size);
                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };

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
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace CSharp
{
    class Program
    {
        public static int n;
        public static int l;
        public static int r;
        public static int[,] array;
        public static bool[,] visited;
        public static int result = 0;
        static void Main(string[] args)
        {
            int[] inputted = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            n = inputted[0];
            l = inputted[1];
            r = inputted[2];
            array = new int[n, n];
            visited = new bool[n, n];
            for (int i = 0; i < n; i++)
            {
                int[] nums = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                for (int j = 0; j < nums.Length; j++)
                {
                    array[i, j] = nums[j];
                }
            }
            while (true)
            {
                int tmpResult = result;
                openCheck();
                if (tmpResult == result) break;
                visited = new bool[n, n];
            }
            Console.WriteLine(result);
        }
        static void openCheck()
        {
            bool flag = false;
            int[] dx = { -1, 0, 0, 1 };
            int[] dy = { 0, -1, 1, 0 };
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (!visited[i, j])
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            int newX = i + dx[k];
                            int newY = j + dy[k];
                            if (0 <= newX && newX < n && newY < n && 0 <= newY && !visited[newX, newY])
                            {
                                int dif = Math.Abs(array[i, j] - array[newX, newY]);
                                if (l <= dif && dif <= r) // 적중 케이스
                                {
                                    bfs(i, j);
                                    flag = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (flag) result += 1;
        }
        static void bfs(int i, int j)
        {
            int[] dx = { -1, 0, 0, 1 };
            int[] dy = { 0, -1, 1, 0 };
            Queue allCases = new Queue();
            Queue queue = new Queue();
            queue.Enqueue(new int[] { i, j });
            allCases.Enqueue(new int[] { i, j });
            int sum = array[i, j];
            visited[i, j] = true;
            while (queue.Count > 0)
            {
                int[] currentNode = queue.Dequeue() as int[];
                int cx = currentNode[0];
                int cy = currentNode[1];
                for (int k = 0; k < 4; k++)
                {
                    int newX = cx + dx[k];
                    int newY = cy + dy[k];
                    if (0 <= newX && newX < n && newY < n && 0 <= newY && !visited[newX, newY])
                    {
                        int dif = Math.Abs(array[cx, cy] - array[newX, newY]);
                        if (l <= dif && dif <= r) // 적중 케이스
                        {
                            allCases.Enqueue(new int[] { newX, newY });
                            queue.Enqueue(new int[] { newX, newY });
                            sum += array[newX, newY];
                            visited[newX, newY] = true;
                        }
                    }
                }
            }
            int value = (int)(sum / allCases.Count);
            foreach (int[] xy in allCases)
                array[xy[0], xy[1]] = value;
        }
    }
}
#elif other2
using System.ComponentModel;

int[] line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
int N = line[0];
int L = line[1];
int R = line[2];

Country[,] country = new Country[N, N];
for(int i = 0; i < N; i++)
{
    line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
    for (int j = 0; j < N; j++)
        country[i, j] = new Country(line[j]);
}
//////////////////////////////////////////////////////////////////
int[,] poptable = new int[N, N];
Changed();
int count = 0;
while (true)
{
    //Print();

    // 1단계: 국경을 연다
    Connect();

    // 2단계: 인구이동을 시작한다
    foreach (var item in country)
        Move(item);

    // 3단계: 국경을 닫는다.
    Disconnect();

    // 4단계: 이동이 있었는지 확인한다.
    if (!Changed())
        break;

    count++;
}

Console.WriteLine(count);
return;

void Print()
{
    for(int i = 0; i < N; i++)
    {
        for (int j = 0; j < N; j++)
            Console.Write(" {0,3} ", country[i, j].population);
        Console.WriteLine();
    }
    Console.WriteLine();
}

bool Changed()
{
    bool changed = false;
    for(int i = 0; i < N; i++)
    {
        for(int j = 0; j < N;j++)
        {
            if (poptable[i, j] != country[i, j].population)
            {
                changed = true;
                poptable[i, j] = country[i, j].population;
            }    
        }
    }
    return changed;
}

void Move(Country c)
{
    int sum = 0;
    int count = 0;
    if (!c.exp)
        Recursion(c, ref sum, ref count);
    if (count != 0)
        Recursion2(c, sum / count);
}

void Recursion2(Country c, int newpop)
{
    c.population = newpop;
    c.div = true;
    if (c.up != null && !c.up.div)
        Recursion2(c.up, newpop);
    if (c.down != null && !c.down.div)
        Recursion2(c.down, newpop);
    if (c.left != null && !c.left.div)
        Recursion2(c.left, newpop);
    if (c.right != null && !c.right.div)
        Recursion2(c.right, newpop);
}

void Recursion(Country c, ref int sum, ref int count)
{
    sum += c.population;
    count++;
    c.exp = true;
    if(c.up != null && !c.up.exp)
        Recursion(c.up, ref sum, ref count);
    if (c.down != null && !c.down.exp)
        Recursion(c.down, ref sum, ref count);
    if (c.left != null && !c.left.exp)
        Recursion(c.left, ref sum, ref count);
    if (c.right != null && !c.right.exp)
        Recursion(c.right, ref sum, ref count);
}

void Connect()
{
    for (int i = 0; i < N; i++)
    {
        for (int j = 0; j < N; j++)
        {
            if (i != 0 && Open(country[i, j], country[i - 1, j]))
                country[i, j].up = country[i - 1, j];
            if (i != N - 1 && Open(country[i, j], country[i + 1, j]))
                country[i, j].down = country[i + 1, j];
            if (j != 0 && Open(country[i, j], country[i, j - 1]))
                country[i, j].left = country[i, j - 1];
            if (j != N - 1 && Open(country[i, j], country[i, j + 1]))
                country[i, j].right = country[i, j + 1];
        }
    }
}

void Disconnect()
{
    for(int i = 0; i < N; i++)
        for(int j = 0; j < N; j++)
        {
            country[i, j].up = null;
            country[i, j].down = null;
            country[i, j].left = null;
            country[i, j].right = null;
            country[i, j].exp = false;
            country[i, j].div = false;
        }
}

bool Open(Country A, Country B)
{
    int del = A.population - B.population;
    if (del >= L && del <= R)
        return true;
    del *= -1;
    if (del >= L && del <= R)
        return true;
    return false;
}

class Country
{
    internal Country? up;
    internal Country? down;
    internal Country? left;
    internal Country? right;
    internal int population;
    internal bool exp;
    internal bool div;

    public Country(int population)
    {
        up = null;
        down = null;
        left = null;
        right = null;
        this.population = population;
        exp = false;
        div = false;
    }
}
#elif other3
// #include<cstdio>
// #include<cstring>
typedef struct point{
	int x, y;
}point;
int N, L, R, A[52][52], top, val, cnt, st_s;
int dx[4]={-1, 0, 1, 0}, dy[4]={0, 1, 0, -1};
bool visit[52][52];
point S[2500], st[2500];
void dfs(int x, int y){
	if(visit[x][y])	return;
	visit[x][y]=1;
	S[top++]={x, y};
	val+=A[x][y];
	for(int d=0;d<4;++d){
		int nx=x+dx[d], ny=y+dy[d], gap=A[nx][ny]-A[x][y];
		gap=gap<0?-gap:gap;
		if(A[nx][ny]!=-1 && !visit[nx][ny] && L<=gap && gap<=R)
			dfs(nx, ny);
	}
	if(top>1 && x==S[0].x && y==S[0].y){
		val/=top;
		for(int i=0;i<top;++i){
			A[S[i].x][S[i].y]=val;
			st[st_s++]={S[i].x, S[i].y};
		}
		cnt=1;
	}
}
int main(){
	scanf("%d %d %d", &N, &L, &R);
	for(int i=1;i<=N;++i)for(int j=1;j<=N;++j)	scanf("%d", &A[i][j]);
	for(int i=0;i<=N;++i){
		A[0][i]=A[i][0]=A[N+1][i]=A[i][N+1]=-1;
	}
	for(int i=1;i<=N;++i){
		for(int j=(i&1)+1;j<=N;j+=2){
			dfs(i, j);
			top=0, val=0;
		}
	}
	int ans=0;
	point tmp_st[2500];
	for(;cnt;++ans){
		memset(visit, 0, sizeof(visit));
		cnt=0;
		memcpy(tmp_st, st, sizeof(point)*st_s);
		int tmp=st_s;
		st_s=0;
		for(int i=0;i<tmp;++i){
			dfs(tmp_st[i].x, tmp_st[i].y);
			top=0, val=0;
		}
	}
	printf("%d\n", ans);
	return 0;
}
#endif
}
