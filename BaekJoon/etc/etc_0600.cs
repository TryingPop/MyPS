using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

/*
날짜 : 2024. 4. 22
이름 : 배성훈
내용 : 탈옥
    문제번호 : 9376번

    0-1 BFS, 최단경로, 다익스트라 문제다

    아이디어는 다음과 같다
    3명의 죄수를 각각 밖으로 가는 BFS 탐색을 한다
    이제 3명이 모두 밖에 나왔을 때 경우 좌물쇠를 연 횟수를 확인한다
    -> 둘 다 좌물쇠를 열고 나온 경우가 아니면 여기서 최소값을 갖는다!
    
        *****           *****           *#***
        .$*$.           *$$..           *$*$.
        *****           *****           *****
    해당 경우들의 최소 좌물쇠 연 횟수를 찾아낸다
    마지막 경우에서 왼쪽 한쪽에 문이 몇개 있던 상관없다

    이제 둘 다 좌물쇠를 열고 나오는 경우만 남는다
    이 경우는 좌물쇠들 중 최소로 연 경우만 찾아주면 된다
    이는 상근이가 오픈한 좌물쇠에 한해서만 조사하면 된다
    죄수의 탈출이 보장되기에 상근이가 열 수 있는 좌물쇠는 
    다른 죄수들도 열 수 있는 좌물쇠가 된다!

    중앙에 열지도 못하는데 좌물쇠가 있는 경우
        .#.***....
        #$#*#*....
        .#.***..$.
    이 경우 중앙에 있는 좌물쇠는 못 연다
    합이 0이다 그런데 왼쪽에 죄수를 보면 1회는 열어야한다
    
    해당 좌물쇠의 여는데 여태까지 연 좌물쇠의 합이 맵에 담기므로,
    해당 좌물쇠에 합을 구해서 최소가 되는게 모여서 갔을 때 가장 적게 연게 보장된다
    
        *#****          *****       *******
        *$**$*          *$#$*       *$#.#$*
        ****#*          *#***       ***#***

    양쪽 죄수가 적어도 하나의 문을 열고 나오는 경우 여기서 찾아진다
    (경로가 다른 경우는 위에서도 찾아지지만 좌물쇠에서도 찾아진다)
    이런 경우가 좌물쇠의 합에 최소가 담긴다
    최소가 되는 좌물쇠에 3명이 모여서 가므로 2개를 빼주면 된다

    아래 코드에서는 초기값이 1이므로 -3을 더 해줘야한다
    최소 좌물쇠만 찾으면 되는데, 그냥 모든 좌물쇠 위치를 넣어 탐색해도 된다
    2N, 덱을 쓰면 NlogN이라 N이 커지면 전부 탐색하는 2N이 오히려 빨라진다

    이렇게 제출하니 92ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0600
    {

        static void Main600(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            Queue<(int r, int c)> prisoner;
            Queue<(int r, int c)> q1;
            Queue<(int r, int c)> q2;
            Queue<(int r, int c)> locked;
            int test;

            int row;
            int col;
            int[][][] board;

            int[] dirR;
            int[] dirC;

            Solve();

            void Solve()
            {

                Init();

                while(test-- > 0)
                {

                    Input();
                    BFS();
                    Output();
                }

                sr.Close();
                sw.Close();
            }

            void Output()
            {

                int ret = 100_000;
                ret = board[0][0][0] + board[1][0][0] + board[2][0][0] - 3;

                while(locked.Count > 0)
                {

                    var node = locked.Dequeue();

                    if (board[0][node.r][node.c] == -1) continue;
                    int calc = board[0][node.r][node.c] + board[1][node.r][node.c] + board[2][node.r][node.c] - 5;
                    ret = calc < ret ? calc : ret;
                }

                sw.Write($"{ret}\n");
            }

            void BFS()
            {

                int chk = -1;
                while(prisoner.Count > 0)
                {

                    q1.Enqueue(prisoner.Dequeue());
                    chk++;

                    int move = 0;
                    while(q1.Count > 0)
                    {

                        move++;
                        while (q1.Count > 0)
                        {

                            (int r, int c) node = q1.Dequeue();

                            for (int i = 0; i < 4; i++)
                            {

                                int nextR = node.r + dirR[i];
                                int nextC = node.c + dirC[i];
                                if (ChkInvalidPos(nextR, nextC) || board[chk][nextR][nextC] > 0) continue;
                                if (board[chk][nextR][nextC] == -2) continue;

                                if (board[chk][nextR][nextC] == -1)
                                {

                                    board[chk][nextR][nextC] = move + 1;
                                    q2.Enqueue((nextR, nextC));
                                    continue;
                                }

                                board[chk][nextR][nextC] = move;
                                q1.Enqueue((nextR, nextC));
                            }
                        }

                        var temp = q1;
                        q1 = q2;
                        q2 = temp;
                    }
                }
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _r >= row + 2 || _c < 0 || _c >= col + 2) return true;
                return false;
            }

            void Init()
            {

                sr = new(new BufferedStream(Console.OpenStandardInput()));
                sw = new(new BufferedStream(Console.OpenStandardOutput()));

                prisoner = new(2);
                q1 = new(102 * 102);
                q2 = new(102 * 102);
                locked = new(100 * 100);

                board = new int[3][][];

                for (int i = 0; i < 3; i++)
                {

                    board[i] = new int[102][];
                    for (int j = 0; j < 102; j++)
                    {

                        board[i][j] = new int[102];
                    }
                }

                test = ReadInt();

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };
            }

            void Input()
            {

                row = ReadInt();
                col = ReadInt();

                prisoner.Enqueue((0, 0));

                for (int c = 0; c < col + 2; c++)
                {

                    board[0][0][c] = 0;
                    board[1][0][c] = 0;
                    board[2][0][c] = 0;
                }

                for (int r = 1; r <= row; r++)
                {

                    board[0][r][0] = 0;
                    board[1][r][0] = 0;
                    board[2][r][0] = 0;

                    for (int c = 1; c <= col; c++)
                    {

                        int cur = sr.Read();
                        if (cur == '#') 
                        { 
                            
                            board[0][r][c] = -1;
                            board[1][r][c] = -1;
                            board[2][r][c] = -1;

                            locked.Enqueue((r, c));
                        }
                        else if (cur == '$')
                        {

                            board[0][r][c] = 0;
                            board[1][r][c] = 0;
                            board[2][r][c] = 0;
                            prisoner.Enqueue((r, c));
                        }
                        else if (cur == '*')
                        {

                            board[0][r][c] = -2;
                            board[1][r][c] = -2;
                            board[2][r][c] = -2;
                        }
                        else
                        {

                            board[0][r][c] = 0;
                            board[1][r][c] = 0;
                            board[2][r][c] = 0;
                        }
                    }

                    if (sr.Read() == '\r') sr.Read();

                    board[0][r][col + 1] = 0;
                    board[1][r][col + 1] = 0;
                    board[2][r][col + 1] = 0;
                }

                for (int c = 0; c < col + 2; c++)
                {

                    board[0][row + 1][c] = 0;
                    board[1][row + 1][c] = 0;
                    board[2][row + 1][c] = 0;
                }
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
/*

아래 같이 가둬지는 경우(4, 4) 조심 
1
7 7
***#***
*.....*
*.***.*
*#*.*#*
*.***.*
*.$*$.*
*******
=> 답 : 3
 

감옥 내로만 이동하는 경우보다
문을 열고 밖으로 가서 다시 들어오는 경우 조심
1
3 5
// *****
// #$*$#
*****
=> 답 : 2

priorityQueue를 써야하는 이유.............
1
3 6
// **#***
// #$##$#
// ******
=> 답 : 2

1
100 100

 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApplication1
{
    public class Node : IComparable
    {
        public int x { get; set; }
        public int y { get; set; }
        public int cost { get; set; }
        public Node(int _x, int _y, int _cost)
        {
            x = _x; y = _y; cost = _cost;
        }

        public int CompareTo(object obj)
        {
            if ((obj is Node) == false) return 0;
            return cost.CompareTo((obj as Node).cost);
        }
    }

    public class PriorityQueue<T>
    {
        List<Node> nowList;

        public PriorityQueue()
        {
            nowList = new List<Node>();
        }

        public void Enqueue(Node data)
        {
            int count = nowList.Count;
            int index = -1;
            if (count < 1)
            {
                nowList.Add(data);
                return;
            }

            for (int i = count; --i > -1;)
            {
                if (nowList[i].CompareTo(data) < 0)
                {
                    index = i + 1;
                    break;
                }
            }

            if (index.Equals(-1))
                index = 0;

            nowList.Insert(index, data);
        }

        public Node Dequeue()
        {
            Node g = nowList[0];
            nowList.RemoveAt(0);
            return g;
        }

        public int count
        {
            get
            {
                return nowList.Count;
            }
        }
    }

    class Program
    {
        static int MIN, minX, minY;
        static int T, h, w;
        static string[,] arr = null;
        static int[,] sum = null;
        static bool[,] visit = null;
        static int[] xPos = { 0, 1, 0, -1 }; // top, right, bottom, left
        static int[] yPos = { -1, 0, 1, 0 };
        static StringBuilder sb = new StringBuilder();
        static PriorityQueue<Node> queue = null;
        
        static void Main(string[] args)
        {
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            sw.AutoFlush = true;

            // input
            T = Convert.ToInt32(sr.ReadLine());
            for (int t = 0; t < T; t++)
            {
                List<Node> list = new List<Node>();
                queue = new PriorityQueue<Node>();
                MIN = int.MaxValue;
                string[] tmp = sr.ReadLine().Split(' ');
                h = Convert.ToInt32(tmp[0]);
                w = Convert.ToInt32(tmp[1]);
                arr = new string[h + 2, w + 2];
                sum = new int[h + 2, w + 2];
                for (int i = 1; i <= h; i++)
                {
                    string input = sr.ReadLine();
                    for (int j = 1; j <= w; j++)
                    {
                        arr[i, j] = input.Substring(j - 1, 1);
                        if (arr[i, j] == "$")
                            list.Add(new Node(j, i, 0));
                    }
                }
                list.Add(new Node(0, 0, 0)); // 상근

                for (int l = 0; l < list.Count; l++)
                {
                    visit = new bool[h + 2, w + 2];
                    visit[list[l].y, list[l].x] = true;
                    queue.Enqueue(new Node(list[l].x, list[l].y, list[l].cost));
                    BFS();
                }

                GetMIN(); // calc MIN
                sb.Append(MIN + Environment.NewLine);
            }

            sw.WriteLine(sb.ToString());
        }

        private static void GetMIN()
        {
            MIN = int.MaxValue;
            for (int i = 1; i <= h; i++)
            {
                for (int j = 1; j <= w; j++)
                {
                    if (arr[i, j] == "*") continue;
                    //if (arr[i, j] == "$") continue;

                    if (arr[i, j] == "#")
                        sum[i, j] = sum[i, j] - 2;

                    if (visit[i, j] == true && MIN > sum[i, j])
                    {
                        MIN = sum[i, j];
                        minX = j;
                        minY = i;
                    }
                }
            }
        }

        private static void BFS()
        {
            while (queue.count > 0)
            {
                Node node = queue.Dequeue();

                int nextX, nextY, nextCount;
                for (int i = 0; i < 4; i++)
                {
                    nextX = node.x + xPos[i];
                    nextY = node.y + yPos[i];
                    nextCount = node.cost;

                    if (nextX < 0 || nextX > w + 1 || nextY < 0 || nextY > h + 1) continue;
                    if (visit[nextY, nextX] == true) continue;
                    if (arr[nextY, nextX] == "*") continue;
                    if (arr[nextY, nextX] == "#")
                        nextCount++;
                    
                    visit[nextY, nextX] = true;
                    sum[nextY, nextX] = sum[nextY, nextX] + nextCount;
                    queue.Enqueue(new Node(nextX, nextY, nextCount));
                }
            }
        }
    }
}

#elif other2
import java.io.*;
import java.util.*;

public class Main {
    private static char[][] a;
    private static int H;
    private static int W;
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(System.out));
        int T = stoi(br.readLine());
        for (int t = 0; t < T; t++) {
            StringTokenizer st = new StringTokenizer(br.readLine());
            H = stoi(st.nextToken());
            W = stoi(st.nextToken());
            a = new char[H + 2][W + 2];
            int x1, y1, x2, y2;
            x1 = y1 = x2 = y2 = -1;
            for (int i = 0; i < H + 2; i++) {
                char[] chars = new char[H + 1];
                if (i != 0 && i != H + 1) {
                    chars = br.readLine().toCharArray();
                }

                for (int j = 0; j < W + 2; j++) {
                    if (i == 0 || j == 0 || i == H + 1 || j == W + 1) {
                        a[i][j] = '.';
                    } else {
                        a[i][j] = chars[j - 1];
                        if (a[i][j] == '$') {
                            if (x1 == -1 && y1 == -1) {
                                x1 = j;
                                y1 = i;
                            } else {
                                x2 = j;
                                y2 = i;
                            }
                        }
                    }
                }
            }

            int[][] d0 = bfs(0, 0);
            int[][] d1 = bfs(x1, y1);
            int[][] d2 = bfs(x2, y2);
            int answer = (H + 2) * (W + 2);
            for (int i = 0; i < H + 2; i++) {
                for (int j = 0; j < W + 2; j++) {
                    if (a[i][j] == '*') {
                        continue;
                    }

                    if (d0[i][j] == -1 || d1[i][j] == -1 || d2[i][j] == -1) {
                        continue;
                    }

                    int cur = d0[i][j] + d1[i][j] + d2[i][j];
                    if (a[i][j] == '#') {
                        cur -= 2;
                    }

                    answer = Math.min(answer, cur);
                }
            }

            bw.write(String.valueOf(answer));
            if (t != T - 1) {
                bw.write("\n");
            }
        }

        bw.flush();
        br.close();
        bw.close();
    }
    
    private static int stoi(String s) {
        return Integer.parseInt(s);
    }

    private static class Pair {
        int x;
        int y;
        public Pair(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }

    private static int[] xa = new int[]{1, 0, -1, 0};
    private static int[] ya = new int[]{0, 1, 0, -1};
    private static int[][] bfs(int x, int y) {
        int[][] dist = new int[H + 2][W + 2];
        for (int i = 0; i < H + 2; i++) {
            Arrays.fill(dist[i], -1);
        }

        ArrayDeque<Pair> deque = new ArrayDeque<>();
        deque.add(new Pair(x, y));
        dist[y][x] = 0;
        while (!deque.isEmpty()) {
            Pair pair = deque.pollFirst();
            for (int i = 0 ; i < 4; i++) {
                int nx = pair.x + xa[i];
                int ny = pair.y + ya[i];
                if (nx < 0 || ny < 0 || H + 2 <= ny || W + 2 <= nx) {
                    continue;
                }

                if (dist[ny][nx] != -1) {
                    continue;
                }

                if (a[ny][nx] == '*') {
                    continue;
                }

                if (a[ny][nx] == '#') {
                    dist[ny][nx] = dist[pair.y][pair.x] + 1;
                    deque.addLast(new Pair(nx, ny));
                } else {
                    dist[ny][nx] = dist[pair.y][pair.x];
                    deque.addFirst(new Pair(nx, ny));
                }
            }
        }

        return dist;
    }
}
#elif other3
from collections import deque
import sys
input = sys.stdin.readline

def bfs(x, y):
// # 방문 체크
    visited = [[-1] * (w+2) for _ in range(h+2)]
// # 지나온 문의 최소 개수 저장
    visited[x][y] = 0

    deq = deque()
    deq.append((x, y))
    
    while deq:
        x, y = deq.popleft()

        for k in range(4):
            nx = x + di[k]
            ny = y + dj[k]

            if 0 <= nx < h+2 and 0 <= ny < w+2 and prison[nx][ny] != "*" and visited[nx][ny] == -1:
// # 방문 체크 지나온 문 최소 개수
                visited[nx][ny] = visited[x][y]
// # 문 만났을 때
                if prison[nx][ny] == "#":
                    visited[nx][ny] += 1
// # 문만난 길은 뒤에 탐색
                    deq.append((nx, ny))
                else:
// # 문 안만난 길부터 탐색
                    deq.appendleft((nx, ny))

    return visited


t = int(input())
di = [0, 1, 0, -1]
dj = [1, 0, -1, 0]

for _ in range(t):
    h, w = map(int, input().split())
// # 감옥 (출구가 여러 개일 때 밖에서 들어오는 탐색을 한번에 쉽게 찾기 위해 이동 가능한 길로 겉을 감싼다.)
// # (아니면 각 출구에서 일일이 계산 해야할 듯 하다.)
// # 윗면 감싸기
    prison = ['.' * (w + 2)]
// # 죄수 위치: 탈출 최소 문 위치
    prisoners = []
// # 감옥 만들기
    for i in range(h):
        row = input()
        for j in range(w):
// # 죄수 위치
            if row[j] == "$":
                prisoners.append((i+1, j+1))
// # 좌우 감싸기 (감옥 좌우로 이동 가능한 길 추가)
        prison.append('.' + row + '.')
// # 아랫면 감싸기
    prison.append('.' * (w + 2))
// # 출구에서 부터 안으로 탐색
    sg = bfs(0, 0)
// # 죄수1
    p1 = bfs(prisoners[0][0], prisoners[0][1])
// # 죄수2
    p2 = bfs(prisoners[1][0], prisoners[1][1])

    minV = 10001
// # 죄수1, 죄수2, 상근 이가 만났을 때 가장 적은 문을 지나는 부분을 찾으면 된다.
    for r in range(1, h+1):
        for c in range(1, w+1):
            if sg[r][c] != -1 and p1[r][c] != -1 and p2[r][c] != -1:
// # 문에서 만났을 때 (문에서 3명이 다 같이 만나니까 2명의 문 개수는 빼기)
                if prison[r][c] == "#":
                    minV = min(minV, sg[r][c] + p1[r][c] + p2[r][c] - 2)
                else:
                    minV = min(minV, sg[r][c] + p1[r][c] + p2[r][c])
    print(minV)
#elif other4
// #include <cstdio>
// #include <cstring>
// #include <queue>
using namespace std;
typedef struct point {
	int x, y;
	point(int x, int y) :x(x), y(y) {};
}point;
int main() {
	short a_cost[102][102], b_cost[102][102], s_cost[102][102];
	char ary[102][102];
	int dx[] = { 0, 0, 1, -1 },
		dy[] = { 1, -1, 0, 0 };
	queue<point> que[2];
	int n, h, w;
	scanf("%d", &n);
	while (n--) {
		scanf("%d%d", &h, &w);
		memset(ary, '.', w + 2);
		int ax = -1, ay, bx, by;
		for (int i = 1; i <= h; i++) {
			ary[i][0] = '.';
			scanf("%s", ary[i] + 1);
			for (int j = 1; j <= w; j++) {
				if (ary[i][j] == '$') {
					if (ax == -1)
						ax = i, ay = j;
					else
						bx = i, by = j;
				}
			}
			ary[i][w + 1] = '.';
		}
		memset(ary[h + 1], '.', w + 2);

		bool flag;
		short depth = 0;
		memset(a_cost, -1, sizeof a_cost);
		a_cost[ax][ay] = 0;
		que[flag = 0].emplace(ax, ay);
		while (!que[0].empty() || !que[1].empty()) {
			if (que[flag].empty())
				depth++, flag = !flag;
			int x = que[flag].front().x, y = que[flag].front().y;
			que[flag].pop();
			for (int d = 0; d < 4; d++) {
				int nx = x + dx[d], ny = y + dy[d];
				if (nx < 0 || h + 1 < nx || ny < 0 || w + 1 < ny || ary[nx][ny] == '*') continue;
				if (a_cost[nx][ny] == -1) {
					a_cost[nx][ny] = depth;
					if (ary[nx][ny] == '#')
						que[!flag].emplace(nx, ny);
					else
						que[flag].emplace(nx, ny);
				}
			}
		}

		depth = 0;
		memset(b_cost, -1, sizeof b_cost);
		b_cost[bx][by] = 0;
		que[flag = 0].emplace(bx, by);
		while (!que[0].empty() || !que[1].empty()) {
			if (que[flag].empty())
				depth++, flag = !flag;
			int x = que[flag].front().x, y = que[flag].front().y;
			que[flag].pop();
			for (int d = 0; d < 4; d++) {
				int nx = x + dx[d], ny = y + dy[d];
				if (nx < 0 || h + 1 < nx || ny < 0 || w + 1 < ny || ary[nx][ny] == '*') continue;
				if (b_cost[nx][ny] == -1) {
					b_cost[nx][ny] = depth;
					if (ary[nx][ny] == '#')
						que[!flag].emplace(nx, ny);
					else
						que[flag].emplace(nx, ny);
				}
			}
		}

		depth = 0;
		memset(s_cost, -1, sizeof s_cost);
		s_cost[0][0] = 0;
		que[flag = 0].emplace(0, 0);
		while (!que[0].empty() || !que[1].empty()) {
			if (que[flag].empty())
				depth++, flag = !flag;
			int x = que[flag].front().x, y = que[flag].front().y;
			que[flag].pop();
			for (int d = 0; d < 4; d++) {
				int nx = x + dx[d], ny = y + dy[d];
				if (nx < 0 || h + 1 < nx || ny < 0 || w + 1 < ny || ary[nx][ny] == '*') continue;
				if (s_cost[nx][ny] == -1) {
					s_cost[nx][ny] = depth;
					if (ary[nx][ny] == '#')
						que[!flag].emplace(nx, ny);
					else
						que[flag].emplace(nx, ny);
				}
			}
		}

		int ans = a_cost[0][0] + b_cost[0][0] + s_cost[0][0];
		for (int i = 1; i <= h; i++)
			for (int j = 1; j <= w; j++) {
				if (s_cost[i][j] != -1) {
					ans = min(ans, (ary[i][j] == '#') + s_cost[i][j] + a_cost[i][j] + b_cost[i][j]);
				}
			}
		printf("%d\n", ans);
	}
}
#endif
}
