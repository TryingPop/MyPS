using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 30
이름 : 배성훈
내용 : 토렌트
    문제번호 : 9577번

	이분 매칭 문제다
	시간에따라 데이터를 매칭시켜 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0664
    {

        static void Main664(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int test;

            int[] match;
            bool[] visit;
            HashSet<int>[] line;

            Solve();

            void Solve()
            {

                Input();

                while(test-- > 0)
                {

                    int n = ReadInt();
                    for (int i = 0; i <= 100; i++)
                    {

                        line[i].Clear();
                        match[i] = 0;
                    }

                    int m = ReadInt();
                    for (int i = 0; i < m; i++)
                    {

                        int s = ReadInt();
                        int e = ReadInt();

                        int len = ReadInt();
                        for (int j = 0; j < len; j++)
                        {

                            int add = ReadInt();
                            for (int t = s + 1; t <= e; t++)
                            {

                                line[t].Add(add);
                            }
                        }
                    }

                    int data = 0;
                    int ret = -1;
                    for (int i = 1; i <= 100; i++)
                    {

                        Array.Fill(visit, false, 1, n);
                        if (DFS(i))
                        {

                            data++;
                            if (data == n) 
                            {

                                ret = i;
                                break; 
                            }
                        }
                    }

                    sw.WriteLine(ret);
                }

                sr.Close();
                sw.Close();
            }

            bool DFS(int _n)
            {

                foreach(int next in line[_n])
                {

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

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                test = ReadInt();

                match = new int[101];
                visit = new bool[101];

                line = new HashSet<int>[101];
                for (int i = 0; i <= 100; i++)
                {

                    line[i] = new(100);
                }
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
import java.io.*;
import java.util.*;

public class Main {
	private static int N, M, t1, t2, a, q, ans, cnt;
	private static int[] A = new int[101];
	private static int[] B = new int[101];
	private static boolean[] visited = new boolean[101];

	private static boolean[][] possible = new boolean[101][101];
	private static ArrayList<Integer>[] edge = new ArrayList[101];

	private static boolean dfs(int u) {
		visited[u] = true;
		for (int v : edge[u]) {
			if (B[v] == -1 || !visited[B[v]] && dfs(B[v])) {
				A[u] = v;
				B[v] = u;
				return true;
			}
		}
		return false;
	}

	public static void main(String[] args) {
		FastIO io = new FastIO();

		int T = io.nextInt();
		for (int t = 0; t < T; t++) {
			Arrays.fill(A, -1);
			Arrays.fill(B, -1);
			Arrays.fill(visited, false);
			for (int i = 0; i <= 100; i++) {
				Arrays.fill(possible[i], false);
			}

			N = io.nextInt();
			M = io.nextInt();

			for (int i = 0; i < M; i++) {
				t1 = io.nextInt();
				t2 = io.nextInt();
				a = io.nextInt();
				for (int j = 0; j < a; j++) {
					q = io.nextInt();
					for (int k = t1; k < t2; k++) {
						possible[k][q] = true;
					}
				}
			}

			if (t == 0) {
				for (int i = 0; i <= 100; i++) {
					edge[i] = new ArrayList<>();
				}
			} else {
				for (int i = 0; i <= 100; i++) {
					edge[i].clear();
				}
			}


			for (int i = 0; i <= 100; i++) {
				for (int j = 1; j <= N; j++) {
					if (possible[i][j]) edge[i].add(j);
				}
			}

			ans = -1;
			cnt = 0;
			for (int i = 0; i <= 100; i++) {
				if (dfs(i)) {
					if (N == ++cnt) {
						ans = i + 1;
						break;
					}
				}
				Arrays.fill(visited, false);
			}

			io.println(ans);
		}
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

			this.inbuffer = new byte[65536];
			this.inbufferpointer = 0;
			this.bytesread = 0;
			this.outbuffer = new byte[65536];
			this.outbufferpointer = 0;

			this.bytebuffer = new byte[100];
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
			byte b;
			while(isSpace(b = read()))
				;
			int result = b - '0';
			while (isDigit(b = read()))
				result = result * 10 + b - '0';
			return result;
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

		private char[] nextLine() {
			byte b;
			int index = 0;

			while ((b = read()) != ASCII_n)
				bytebuffer[index++] = b;

			return convertCharArray(bytebuffer, index);
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

		private void println() {
			write(ASCII_n);
		}

		private void printls(int i) {
			print(i);
			write(ASCII_space);
		}

		private boolean isSpace(byte b) {
			return b <= ASCII_space && b >= 0;
		}

		private boolean isDigit(byte b) {
			return b >= ASCII_0 && b <= ASCII_9;
		}

		private boolean isChar(byte b) {
			return (ASCII_A <= b && b <= ASCII_Z) || (ASCII_a <= b && b <= ASCII_z);
		}
	}
}

#elif other2
def dfs(a):
    visited[a]=True
    for b in adj[a]:
        if B[b]==-1: B[b]=a; return 1
    for b in adj[a]:
        if not(visited[B[b]]) and dfs(B[b]): B[b]=a; return 1
    return 0

import sys
input=sys.stdin.readline
for _ in range(int(input())):
    N,M=map(int,input().split())
    adj = [set()for _ in range(100)]
    match=0
    for _ in range(M):
        t1,t2,_,*q=list(map(int,input().split()))
        for i in range(t1,t2): adj[i]|=set(q)
    B=[-1]*(N+1)
    for i in range(100):
        visited=[0]*100
        match+=dfs(i)
        if match==N:print(i+1);break
    else:print(-1)
#elif other3
// #include <iostream>
// #include <vector>
// #include <algorithm>
// #define FAST ios_base::sync_with_stdio(false); cin.tie(0); cout.tie(0);

using namespace std;

const int MAXN = 101;
vector<vector<int>> g_graph;
bool g_visited[MAXN] = { false, };
int g_matched[MAXN] = { 0, };
int g_maxtime = -1;

bool dfs(int from)
{
	for (int to : g_graph[from]) {
		if (g_visited[to]) continue;
		else g_visited[to] = true;
		if (g_matched[to] == 0 || dfs(g_matched[to])) {
			g_matched[to] = from;
			if(to > g_maxtime) g_maxtime = to;
			return true;
		}
	}
	return false;
}

void doTest()
{
	int n, m;
	cin >> n >> m;
	g_graph.resize(n + 1);
	int t1, t2, a, b;
	bool connected[MAXN][MAXN];
	fill(&connected[0][0], &connected[MAXN - 1][MAXN], false);
	for(int i = 0; i < m; i++) {
		cin >> t1 >> t2;
		cin >> a;
		for(int j = 0; j < a; j++) {
			cin >> b;
			for(int k = t1 + 1; k <= t2; k++)
				connected[b][k] = true;
		}
	}

	for(int i = 1; i < n + 1; i++)
		for(int j = 0; j < MAXN; j++)
			if(connected[i][j]) g_graph[i].push_back(j);

	fill(g_matched, g_matched + MAXN, 0);
	g_maxtime = -1;
	int count = 0;
	for(int i = 1; i < n + 1; i++) {
		fill(g_visited, g_visited + MAXN, false);
		if(dfs(i)) count++;
	}
	cout << (count == n ? g_maxtime : -1) << "\n";
	g_graph.clear();
}

int main()
{
	FAST;
	int T;
	cin >> T;
	for(int i = 0; i < T; i++)
		doTest();
}
#endif
}
