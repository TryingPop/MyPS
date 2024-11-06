using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 16
이름 : 배성훈
내용 : 수영장 사장님
    문제번호 : 15730번

    구현, BFS 문제다
    etc_0697의 방법을 그대로 했다
    즉, 층을 하나씩 줄여가면서 높이를 갱신했다
    이렇게 구하니 1.2초 걸렸다

    빠른 사람들 풀이를 보니, 한번에 최대한 물을 줄인다
*/

namespace BaekJoon.etc
{
    internal class etc_0698
    {

        static void Main698(string[] args)
        {

            StreamReader sr;

            int[][] board;
            int row, col;

            int[][] water;
            bool[][] visit;
            Queue<(int r, int c)> q;
            int[] height;

            int[] dirR;
            int[] dirC;

            Solve();

            void Solve()
            {

                Input();

                SetBFS();

                for (int i = 0; i < height.Length; i++)
                {

                    BFS(i, (i & 1) == 0);
                }

                Console.WriteLine(GetRet());
            }

            int GetRet()
            {

                int ret = 0;
                for (int r = 1; r <= row; r++)
                {

                    for (int c = 1; c <= col; c++)
                    {

                        ret += water[r][c] - board[r][c];
                    }
                }

                return ret;
            }

            void SetBFS()
            {

                q = new(row * col);

                water = new int[row + 2][];
                visit = new bool[row + 2][];

                for (int r = 0; r < row + 2; r++)
                {

                    water[r] = new int[col + 2];
                    visit[r] = new bool[col + 2];
                    Array.Fill(water[r], height[0]);
                }

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };
            }

            void BFS(int _idx, bool _tf)
            {

                q.Enqueue((0, 0));

                while(q.Count > 0)
                {

                    (int r, int c) node = q.Dequeue();

                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];

                        if (ChkInvalidPos(nextR, nextC) || visit[nextR][nextC] == _tf) continue;
                        visit[nextR][nextC] = _tf;

                        if (board[nextR][nextC] < height[_idx])
                        {

                            water[nextR][nextC] = height[_idx + 1];
                            q.Enqueue((nextR, nextC));
                        }
                    }
                }
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row + 2 || _c >= col + 2) return true;
                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                board = new int[row + 2][];

                for (int r = 0; r < row + 2; r++)
                {

                    board[r] = new int[col + 2];
                }

                bool[] num = new bool[10_001];
                int len = 0;

                for (int r = 1; r <= row; r++)
                {

                    for (int c = 1; c <= col; c++)
                    {

                        int cur = ReadInt();
                        board[r][c] = cur;
                        if (num[cur]) continue;
                        num[cur] = true;
                        len++;
                    }
                }

                height = new int[len + 1];
                int idx = 0;
                for (int i = 10_000; i > 0; i--)
                {

                    if (!num[i]) continue;

                    height[idx++] = i;
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
using System.IO;
using System.Linq;

#nullable disable

public static class Program
{
    private static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var nm = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var (n, m) = (nm[0], nm[1]);

        var map = new int[n, m];
        var heights = new HashSet<int>();

        for (var y = 0; y < n; y++)
        {
            var l = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            heights.UnionWith(l);

            for (var x = 0; x < m; x++)
                map[y, x] = l[x];
        }

        var visited = new bool[n, m];
        var maxfill = new int[n, m];
        var q = new Queue<(int y, int x)>();
        var buff = new List<(int y, int x)>();

        var moveset = new[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

        foreach (var h in heights.OrderByDescending(v => v))
        {
            Array.Clear(visited);

            for (var y = 0; y < n; y++)
                for (var x = 0; x < m; x++)
                {
                    if (visited[y, x])
                        continue;

                    if (maxfill[y, x] != 0)
                        continue;

                    if (map[y, x] >= h)
                        continue;

                    buff.Clear();

                    var filled = true;
                    q.Enqueue((y, x));

                    while (q.TryDequeue(out var pos))
                    {
                        var (cy, cx) = pos;

                        if (visited[cy, cx])
                            continue;

                        visited[cy, cx] = true;
                        buff.Add((cy, cx));

                        if (cy == 0 || cx == 0 || cy == n - 1 || cx == m - 1)
                            filled = false;

                        foreach (var (dy, dx) in moveset)
                        {
                            var (ny, nx) = (cy + dy, cx + dx);

                            if (ny < 0 || nx < 0 || ny >= n || nx >= m)
                                continue;

                            if (visited[ny, nx])
                                continue;

                            if (map[ny, nx] >= h)
                                continue;

                            q.Enqueue((ny, nx));
                        }
                    }

                    if (!filled)
                        continue;

                    foreach (var (cy, cx) in buff)
                        maxfill[cy, cx] = h - map[cy, cx];
                }
        }

        var maxfillsum = 0L;
        for (var y = 0; y < n; y++)
            for (var x = 0; x < m; x++)
                maxfillsum += maxfill[y, x];

        sw.WriteLine(maxfillsum);
    }
}

#elif other2

import java.io.*;
import java.util.*;

public class Main {

    static int rowSZ, colSZ;
    static int[][] pool, dist;
    static boolean[][] visited;
    static int[] vr = {1, -1, 0, 0};
    static int[] vc = {0, 0, 1, -1};

    static int solve() {
        dist = new int[rowSZ + 2][colSZ + 2];
        visited = new boolean[rowSZ + 2][colSZ + 2];
        for (int i = 0; i < rowSZ + 2; i++) {
            Arrays.fill(dist[i], Integer.MAX_VALUE);
        }
        PriorityQueue<Point> que = new PriorityQueue<>();
        que.add(new Point(0, 0));
        dist[0][0] = 0;
        while (!que.isEmpty()) {
            Point cur = que.poll();
            if (visited[cur.r][cur.c]) continue;
            visited[cur.r][cur.c] = true;

            for (int i = 0; i < 4; i++) {
                int nr = cur.r + vr[i];
                int nc = cur.c + vc[i];
                if (checkBoundary(nr, nc)) {
                    int nd = Math.max(dist[cur.r][cur.c], pool[nr][nc]);
                    if (dist[nr][nc] > nd) {
                        dist[nr][nc] = nd;
                        que.add(new Point(nr, nc, nd));
                    }
                }
            }
        }
        return getAns();
    }

    private static int getAns() {
        int ret = 0;
        for (int i = 0; i < rowSZ + 2; i++) {
            for (int j = 0; j < colSZ + 2; j++) {
                ret += dist[i][j] - pool[i][j];
            }
        }
        return ret;
    }

    static boolean checkBoundary(int r, int c) {
        return r >= 0 && r < rowSZ + 2 && c >= 0 && c < colSZ + 2;
    }

    static class Point implements Comparable<Point> {
        int r, c, d;

        public Point(int r, int c) {
            this.r = r;
            this.c = c;
        }

        public Point(int r, int c, int d) {
            this.r = r;
            this.c = c;
            this.d = d;
        }

        @Override
        public String toString() {
            return "Point{" +
                    "r=" + r +
                    ", c=" + c +
                    ", w=" + d +
                    '}';
        }

        @Override
        public int compareTo(Point o) {
            return Integer.compare(d, o.d);
        }
    }

    static void showArr(int[][] arr) {
        for (int i = 0; i < arr.length; i++) {
            System.out.println(Arrays.toString(arr[i]));
        }
        System.out.println();
    }

    public static void main(String[] args) throws IOException {
        // System.out.println("===== input =====");
        // String fileName = "input/input1.txt";
        // BufferedReader br = new BufferedReader(new FileReader(fileName));
        // BufferedReader br2 = new BufferedReader(new FileReader(fileName));
        // String s;
        // while ((s = br2.readLine()) != null) {
        //     System.out.println(s);
        // }
        // System.out.println("===== output =====");
        // FastReader fr = new FastReader("input/input1.txt");
        FastReader fr = new FastReader();
        rowSZ = fr.nextInt();
        colSZ = fr.nextInt();
        pool = new int[rowSZ + 2][colSZ + 2];
        for (int i = 0; i < rowSZ; i++) {
            for (int j = 0; j < colSZ; j++) {
                pool[i + 1][j + 1] = fr.nextInt();
            }
        }
        System.out.println(solve());
    }

    static class FastReader {
        final private int BUFFER_SIZE = 1 << 16;
        private DataInputStream din;
        private byte[] buffer;
        private int bufferPointer, bytesRead;

        public FastReader() {
            din = new DataInputStream(System.in);
            buffer = new byte[BUFFER_SIZE];
            bufferPointer = bytesRead = 0;
        }

        public FastReader(String file_name) throws IOException {
            din = new DataInputStream(new FileInputStream(file_name));
            buffer = new byte[BUFFER_SIZE];
            bufferPointer = bytesRead = 0;
        }

        public String readLine() throws IOException {
            byte[] buf = new byte[64]; // line length
            int cnt = 0, c;
            while ((c = read()) != -1) {
                if (c == '\n')
                    break;
                buf[cnt++] = (byte) c;
            }
            return new String(buf, 0, cnt);
        }

        public int nextInt() throws IOException {
            int ret = 0;
            byte c = read();
            while (c <= ' ')
                c = read();
            boolean neg = (c == '-');
            if (neg)
                c = read();
            do {
                ret = ret * 10 + c - '0';
            } while ((c = read()) >= '0' && c <= '9');

            if (neg)
                return -ret;
            return ret;
        }

        public long nextLong() throws IOException {
            long ret = 0;
            byte c = read();
            while (c <= ' ')
                c = read();
            boolean neg = (c == '-');
            if (neg)
                c = read();
            do {
                ret = ret * 10 + c - '0';
            }
            while ((c = read()) >= '0' && c <= '9');
            if (neg)
                return -ret;
            return ret;
        }

        public double nextDouble() throws IOException {
            double ret = 0, div = 1;
            byte c = read();
            while (c <= ' ')
                c = read();
            boolean neg = (c == '-');
            if (neg)
                c = read();

            do {
                ret = ret * 10 + c - '0';
            }
            while ((c = read()) >= '0' && c <= '9');

            if (c == '.') {
                while ((c = read()) >= '0' && c <= '9') {
                    ret += (c - '0') / (div *= 10);
                }
            }

            if (neg)
                return -ret;
            return ret;
        }

        private void fillBuffer() throws IOException {
            bytesRead = din.read(buffer, bufferPointer = 0, BUFFER_SIZE);
            if (bytesRead == -1)
                buffer[0] = -1;
        }

        private byte read() throws IOException {
            if (bufferPointer == bytesRead)
                fillBuffer();
            return buffer[bufferPointer++];
        }

        public void close() throws IOException {
            if (din == null)
                return;
            din.close();
        }
    }
}

#elif other3

public class Main {
	static Reader in = new Reader();
	static int[][] map, water;
	static int N, M, INF, ans;
	static int[] dr = {-1,1,0,0};
	static int[] dc = {0,0,-1,1};
	
	public static void main(String[] args) throws Exception {
		N = in.nextInt();
		M = in.nextInt();
		
		INF = 10001;
		
		map = new int[N+2][M+2];
		water = new int[N+2][M+2];
		
		for(int r = 1; r <= N; r++) {
			for(int c = 1; c <= M; c++) {
				map[r][c] = in.nextInt();
				water[r][c] = INF;
			}
		}
		
		solve();
		
		for(int r = 1; r <= N; r++) {
			for(int c = 1; c <= M; c++) {
				ans += water[r][c] - map[r][c];
			}
		}
		
		System.out.println(ans);
	}
	
	static void solve() {
		while(true) {
			boolean flag = true;
			
			for(int r = 1; r <= N; r++) {
				for(int c = 1; c <= M; c++) {
					if(map[r][c] == water[r][c]) continue;
					
					int min = INF;
					
					for(int d = 0; d < 4; d++) {
						int nr = r + dr[d];
						int nc = c + dc[d];
						min = Math.min(min, water[nr][nc]);
					}
					
					if(water[r][c] > min) {
						water[r][c] = Math.max(map[r][c], min);
						flag = false;
					}
				}
			}
			
			if(flag) break;
		}
	}
	
	static class Reader {
		final int SIZE = 1 << 13;
		byte[] buffer = new byte[SIZE];
		int index, size;

		int nextInt() throws Exception {
			int n = 0;
			byte c;
			while ((c = read()) <= 32);
			do n = (n << 3) + (n << 1) + (c & 15);
			while (isNumber(c = read()));
			return n;
		}

		boolean isNumber(byte c) {
			return 47 < c && c < 58;
		}

		byte read() throws Exception {
			if (index == size) {
				size = System.in.read(buffer, index = 0, SIZE);
				if (size < 0)
					buffer[0] = -1;
			}
			return buffer[index++];
		}
	}
}
#endif
}
