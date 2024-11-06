using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 18
이름 : 배성훈
내용 : N-Rook
    문제번호 : 1760번

	이분 매칭 문제다
	1은 공격 경로는 가능한데, 배치는 못하는 곳, 2는 벽으로 막힌 곳 앞의 룩 2개 문제를 합친 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0704
    {

        static void Main704(string[] args)
        {

            StreamReader sr;

            int[][][] board;
            int row, col;

            List<int>[] line;
            int[] match;
            bool[] visit;

            int len1, len2;

            Solve();

            void Solve()
            {

                Input();

                SetArea();
                LinkLine();

                int ret = 0;
                for (int i = 1; i <= len1; i++)
                {

                    Array.Fill(visit, false);
                    if (DFS(i)) ret++;
                }

                Console.WriteLine(ret);
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

            void LinkLine()
            {

                line = new List<int>[len1 + 1];
                for (int i = 1; i <= len1; i++)
                {

                    line[i] = new();
                }

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c][0] != 0) continue;
                        line[board[r][c][1]].Add(board[r][c][2]);
                    }
                }

                match = new int[len2 + 1];
                visit = new bool[len2 + 1];
            }

            void SetArea()
            {

                len1 = 0;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c][0] != 0) continue;
                        board[r][c][1] = ++len1;

                        for (int i = c + 1; i < col; i++)
                        {

                            if (board[r][i][0] == 2) break;
                            board[r][i][1] = len1;
                            c++;
                        }
                    }
                }

                len2 = 0;
                for (int c = 0; c < col; c++)
                {

                    for (int r = 0; r < row; r++)
                    {

                        if (board[r][c][0] != 0) continue;
                        board[r][c][2] = ++len2;

                        for (int i = r + 1; i < row; i++)
                        {

                            if (board[i][c][0] == 2) break;
                            board[i][c][2] = len2;
                            r++;
                        }
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                board = new int[row][][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col][];
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = new int[3];
                        board[r][c][0] = ReadInt();
                    }
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
// #include <string.h>
// #include <algorithm>
// #include <vector>
using namespace std;
bool ch[5000];
vector<int> map[5000];
int a[100][100], hor[100][100], ver[100][100], pred[5000];
bool recur(int v){
	if(v==-1)
		return 1;
	for(int i=0; i<map[v].size(); i++){
		if(!ch[map[v][i]]){
			ch[map[v][i]] = 1;
			if(recur(pred[map[v][i]])){
				pred[map[v][i]] = v;
				return 1;
			}
		}
	}
	return 0;
}
int main(){
	int n, m, i, j, thor=-1, tver=-1, ans=0;
	scanf("%d %d", &n, &m);
	for(i=0; i<n; i++){
		for(j=0; j<m; j++)
			scanf("%d", &a[i][j]);
	}
	for(i=0; i<n; i++){
		for(j=0; j<m; j++){
			if(j==0 || (a[i][j] == 2 && a[i][j-1] != 2))
				thor++;
			hor[i][j] = thor;
		}
	}
	for(j=0; j<m; j++){
		for(i=0; i<n; i++){
			if(i==0 || (a[i][j] == 2 && a[i-1][j] != 2))
				tver++;
			ver[i][j] = tver;
		}
	}
	for(i=0; i<n; i++){
		for(j=0; j<m; j++){
			if(a[i][j] == 0)
				map[hor[i][j]].push_back(ver[i][j]);
		}
	}
	memset(pred, -1, sizeof(pred));
	for(i=0; i<=thor; i++){
		memset(ch, 0, sizeof(ch));
		if(recur(i))
			ans++;
	}
	printf("%d\n", ans);
	return 0;
}

#elif other2
import java.io.*;
import java.util.*;

public class Main {
	private static int N, M;

	private static int[][] B;
	private static boolean[] visited;

	private static ArrayList<Edge>[] edge;

	private static class Edge {
		int row;
		int num;

		Edge(int r, int n) {
			row = r;
			num = n;
		}
	}

	private static boolean dfs(int u) {
		visited[u] = true;
		for (Edge v : edge[u]) {
			if (B[v.row][v.num] == 0 || !visited[B[v.row][v.num]] && dfs(B[v.row][v.num])) {
				B[v.row][v.num] = u;
				return true;
			}
		}
		return false;
	}

	public static void main(String[] args) {
		FastIO io = new FastIO();

		N = io.nextInt();
		M = io.nextInt();

		edge = new ArrayList[N * (M + 1) + 1];
		int edge_pointer = 0;

		int[] row = new int[M + 1];

		int shape;
		for (int i = 1; i <= N; i++) {
			edge[++edge_pointer] = new ArrayList<>();

			for (int j = 1; j <= M; j++) {
				shape = io.nextInt();
				if (shape == 0) {
					edge[edge_pointer].add(new Edge(j, row[j]));
				} else if (shape == 2) {
					edge[++edge_pointer] = new ArrayList<>();
					row[j]++;
				}
			}
		}

		B = new int[M + 1][N];
		visited = new boolean[edge_pointer + 1];

		int ans = 0;
		for (int i = 1; i <= edge_pointer; i++) {
			if (dfs(i)) ans++;
			Arrays.fill(visited, false);
		}

		io.print(ans);
		io.flushBuffer();
	}

	private static class FastIO {
		private static final int EOF = -1;

		private static final byte ASCII_dot = 46;
		private static final byte ASCII_minus = 45;
		private static final byte ASCII_space = 32;
		private static final byte ASCII_n = 10;
		private static final byte ASCII_0 = 48;
		private static final byte ASCII_9 = 57;

		private static final byte ASCII_A = 65;
		private static final byte ASCII_Z = 90;
		private static final byte ASCII_a = 97;
		private static final byte ASCII_z = 122;

		private final DataInputStream din;
		private final DataOutputStream dout;

		private byte[] inbuffer;
		private int inbufferpointer, bytesread;
		private byte[] outbuffer;
		private int outbufferpointer;

		private byte[] bytebuffer;

		private FastIO() {
			this.din = new DataInputStream(System.in);
			this.dout = new DataOutputStream(System.out);

			this.inbuffer = new byte[10000];
			this.inbufferpointer = 0;
			this.bytesread = 0;
			this.outbuffer = new byte[20];
			this.outbufferpointer = 0;

			this.bytebuffer = new byte[20];
		}

		private byte read() {
			if (inbufferpointer == bytesread)
				fillBuffer();
			return bytesread == EOF ? EOF : inbuffer[inbufferpointer++];
		}

		private void fillBuffer() {
			try {
				bytesread = din.read(inbuffer, inbufferpointer = 0, inbuffer.length);
			} catch (Exception e) {
				throw new RuntimeException(e);
			}
		}

		private void write(byte b) {
			if (outbufferpointer == outbuffer.length)
				flushBuffer();
			outbuffer[outbufferpointer++] = b;
		}

		private void flushBuffer() {
			if (outbufferpointer != 0) {
				try {
					dout.write(outbuffer, 0, outbufferpointer);
				} catch (Exception e) {
					throw new RuntimeException(e);
				}
				outbufferpointer = 0;
			}
		}

		private boolean hasNext() {
			return inbufferpointer == bytesread;
		}

		private char[] convertCharArray(byte[] arr, int length) {
			char[] ret = new char[length];
			for (int i = 0; i < length; i++) {
				ret[i] = (char) arr[i];
			}
			return ret;
		}

		private int nextInt() {
			boolean isMinus = false;
			byte b;

			while(isSpace(b = read()))
				;

			if (b == ASCII_minus) {
				isMinus = true;
				b = read();
			}

			int result = b - '0';
			while (isDigit(b = read()))
				result = result * 10 + b - '0';
			return isMinus ? -result : result;
		}

		private float nextFloat() {
			boolean isMinus = false;

			byte b;
			while(isSpace(b = read()))
				;

			if (b == ASCII_minus) {
				isMinus = true;
				b = read();
			}

			float result = b - '0';
			while (isDigit(b = read()))
				result = result * 10 + b - '0';

			if (b == ASCII_dot) {
				float div = 1;
				while (isDigit(b = read())) {
					result = result * 10 + b - '0';
					div *= 10;
				}
				result /= div;
			}
			return isMinus ? -result : result;
		}

		private char[] nextCharArray() {
			byte b;
			int index = 0;

			while (isChar(b = read()))
				bytebuffer[index++] = b;

			return convertCharArray(bytebuffer, index);
		}

		private String nextString() {
			StringBuilder sb = new StringBuilder();

			byte b;
			while (isChar(b = read())) {
				sb.append((char) b);
			}

			return sb.toString();
		}

		private void println() {
			write(ASCII_n);
		}

		private void print(String s) {
			for (int i = 0; i < s.length(); i++) {
				write((byte) s.charAt(i));
			}
		}

		private void println(String s) {
			print(s);
			write(ASCII_n);
		}

		private void print(int i) {
			if (i == 0) {
				write(ASCII_0);
			} else {
				if (i < 0) {
					write(ASCII_minus);
					i = -i;
				}
				int index = 0;
				while (i > 0) {
					bytebuffer[index++] = (byte) ((i % 10) + ASCII_0);
					i /= 10;
				}
				while (index-- > 0) {
					write(bytebuffer[index]);
				}
			}
		}

		private void println(int i) {
			print(i);
			write(ASCII_n);
		}

		private void printls(int i) {
			print(i);
			write(ASCII_space);
		}

		private void print(long i) {
			if (i == 0) {
				write(ASCII_0);
			} else {
				if (i < 0) {
					write(ASCII_minus);
					i = -i;
				}
				int index = 0;
				while (i > 0) {
					bytebuffer[index++] = (byte) ((i % 10) + ASCII_0);
					i /= 10;
				}
				while (index-- > 0) {
					write(bytebuffer[index]);
				}
			}
		}

		private void println(long i) {
			print(i);
			write(ASCII_n);
		}

		private void printls(long i) {
			print(i);
			write(ASCII_space);
		}

		private boolean isSpace(byte b) {
			return b <= ASCII_space && b > 0;
		}

		private boolean isDigit(byte b) {
			return b >= ASCII_0 && b <= ASCII_9;
		}

		private boolean isChar(byte b) {
			return (ASCII_A <= b && b <= ASCII_Z) || (ASCII_a <= b && b <= ASCII_z);
		}
	}
}

#endif
}
