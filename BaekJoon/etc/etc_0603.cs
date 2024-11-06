using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 24
이름 : 배성훈
내용 : 최애 정하기
    문제번호 : 17481번

	자료구조, 해시, 이분 매칭 문제다
	딕셔너리 자료구조로 문자열을 숫자로 변경하고
	이분매칭을 해결 했다
*/

namespace BaekJoon.etc
{
    internal class etc_0603
    {

        static void Main603(string[] args)
        {

            StreamReader sr;
            int[][] line;
            int n, m;
            bool[] visit;
            int[] match;

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

                if (n == ret) Console.Write("YES");
                else Console.Write($"NO\n{ret}");
            }

            bool DFS(int _n)
            {

                for (int i = 0; i < line[_n].Length; i++)
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

            void Input()
            {

                sr = new(new BufferedStream(Console.OpenStandardInput()));
                n = ReadInt();

                m = ReadInt();
                int idx = 1;

                Dictionary<string, int> sTi = new(m);

                for (int i = 0; i < m; i++)
                {

                    string str = sr.ReadLine();
                    sTi[str] = idx++;
                }

                line = new int[n + 1][];

                for (int i = 1; i <= n; i++)
                {

                    int size = ReadInt();
                    line[i] = new int[size];
                    string[] temp = sr.ReadLine().Split();
                    for (int j = 0; j < size; j++)
                    {

                        line[i][j] = sTi[temp[j]];
                    }
                }

                visit = new bool[m + 1];
                match = new int[m + 1];
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
import java.io.*;
import java.util.*;

public class Main {
	private static int N, M;
	private static int[] B;
	private static boolean[] visited;

	private static ArrayList<Integer>[] edge;

	private static boolean dfs(int u) {
		visited[u] = true;
		for (int v : edge[u]) {
			if (B[v] == 0 || !visited[B[v]] && dfs(B[v])) {
				B[v] = u;
				return true;
			}
		}
		return false;
	}

	public static void main(String[] args) {
		FastIO io = new FastIO();
		N = io.nextInt();
		M = io.nextInt();

		HashMap<String, Integer> map = new HashMap<>();
		for (int i = 1; i <= M; i++) {
			map.put(io.nextString(), i);
		}

		edge = new ArrayList[N + 1];
		for (int i = 1; i <= N; i++) {
			edge[i] = new ArrayList<>();
		}

		int tmp;
		for (int i = 1; i <= N; i++) {
			tmp = io.nextInt();
			for (int j = 0; j < tmp; j++) {
				edge[i].add(map.get(io.nextString()));
			}
		}

		B = new int[M + 1];
		visited = new boolean[N + 1];

		int ans = 0;
		for (int i = 1; i <= N; i++) {
			if (dfs(i)) ans++;
			Arrays.fill(visited, false);
		}

		if (ans == N) {
			io.print("YES");
		} else {
			io.println("NO");
			io.print(ans);
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

			this.inbuffer = new byte[10000];
			this.inbufferpointer = 0;
			this.bytesread = 0;
			this.outbuffer = new byte[100];
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

#elif other2
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.StringTokenizer;

public class Main {
	private final static int NONE = 0;
	
	private static BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
	private static StringBuilder sb = new StringBuilder();
	private static StringTokenizer tokens;

	private static int N;	//	친구 수
	private static int M;	//	멤버 수
	private static Map<String, Integer> memberName = new HashMap<>();
	
	private static boolean[] visited;	//	방문 상태
	private static int[] oshi;			//	oshi[i] : i번째 멤버를 최애하는 친구 번호
	
	private static List<Integer>[] graph;	//	그래프
	
	private static int maxOshi;	//	겹치지 않는 최애 수
	
	public static void main(String[] args) throws IOException {
		tokens = new StringTokenizer(br.readLine());
		
		N = Integer.parseInt(tokens.nextToken());
		M = Integer.parseInt(tokens.nextToken());
	
		for(int i = 1; i <= M; i++) {
			String member = br.readLine();
			memberName.put(member, i);
		}
		
		visited = new boolean[M + 1];
		oshi = new int[M + 1];
		
		graph = new ArrayList[N + 1];
		for(int i = 1; i <= N; i++)
			graph[i] = new ArrayList<>();
		
		for(int i = 1; i <= N; i++) {
			tokens = new StringTokenizer(br.readLine());
			int cnt = Integer.parseInt(tokens.nextToken());
			
			for(int j = 1; j <= cnt; j++) {
				String member = tokens.nextToken();
				
				graph[i].add(memberName.get(member));
			}
		}
		
		for(int i = 1; i <= N; i++) {
			Arrays.fill(visited, false);
			
			if(dfs(i))
				maxOshi++;
		}
		
		System.out.println(maxOshi == N ? "YES" : "NO\n" + maxOshi);
	} //	main-end
	
	private static boolean dfs(int friend) {
		for(int member : graph[friend]) {	//	friend 친구가 최애하는 멤버들
			
			if(!visited[member]) {	//	member를 아직 확인 안했을 경우
				visited[member] = true;	//	member를 확인함
				
				if(oshi[member] == NONE || dfs(oshi[member])) {	//	member를 최애하는 아이가 없거나, 있어도 다른 멤버를 최애할 수 있으면
					oshi[member] = friend;	//	member는 friend가 최애함
					return true;
				}
			}
		}
		
		return false;	//	friend는 누구도 최애할 수 없음
	}
} //	Main-class-end
#endif
}
