using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 22
이름 : 배성훈
내용 : 들쥐의 탈출
    문제번호 : 2191번

	이분매칭 문제다
	조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0596
    {

        static void Main596(string[] args)
        {

            StreamReader sr;

            int n, m;
            List<int>[] line;
            int[] match;
            bool[] visit;

            Solve();

            void Solve()
            {

                Input();

                int ret = n;
                for (int i = 1; i <= n; i++)
                {

                    Array.Fill(visit, false);
                    if (DFS(i)) ret--;
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

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 8);

                n = ReadInt();
                m = ReadInt();
                int s = ReadInt();
                int v = ReadInt();

                (float x, float y)[] mouse = new (float x, float y)[n + 1];
                line = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    mouse[i] = (ReadFloat(), ReadFloat());
                    line[i] = new();
                }

                (float x, float y)[] tunnel = new (float x, float y)[m + 1];

                match = new int[m + 1];
                visit = new bool[m + 1];

                for (int i = 1; i <= m; i++)
                {

                    tunnel[i] = (ReadFloat(), ReadFloat());
                }

                for (int i = 1; i <= n; i++)
                {

                    for (int j = 1; j <= m; j++)
                    {

                        if (ChkDisConnect(mouse[i], tunnel[j], v * s)) continue;
                        line[i].Add(j);
                    }
                }

                sr.Close();
            }

            bool ChkDisConnect((float x, float y) _mouse, (float x, float y) _tunnel, double _max)
            {

                double diffX = _mouse.x - _tunnel.x;
                double diffY = _mouse.y - _tunnel.y;

                diffX *= diffX;
                diffY *= diffY;

                _max *= _max;

                return _max < diffX + diffY;
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

            float ReadFloat()
            {

                int c;
                float ret = 0f;
                bool plus = true;
                bool isFloat = false;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if (c == '-')
                    {

                        plus = false;
                        continue;
                    }
                    else if (c == '.')
                    {

                        isFloat = true;
                        break;
                    }
                    ret = ret * 10 + c - '0';
                }

                if (isFloat)
                {

                    int add = 0;
                    float div = 1.0f;
                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        add = add * 10 + c - '0';
                        div *= 10;
                    }

                    ret += (add / div);
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
import java.io.*;
import java.util.*;

public class Main {
	private static int N, M, S, V;
	private static int[] A;
	private static int[] B;
	private static boolean[] visited;

	private static ArrayList<Integer>[] edge;

	private static boolean dfs(int u) {
		visited[u] = true;
		for (int v : edge[u]) {
			if (B[v] == 0 || !visited[B[v]] && dfs(B[v])) {
				A[u] = v;
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
		S = io.nextInt();
		V = io.nextInt();

		float[][] loc_mouse = new float[N + 1][2];
		for (int i = 1; i <= N; i++) {
			loc_mouse[i][0] = io.nextFloat();
			loc_mouse[i][1] = io.nextFloat();
		}

		edge = new ArrayList[N + 1];
		for (int i = 1; i <= N; i++) {
			edge[i] = new ArrayList<>();
		}

		float max = S * S * V * V;
		float loc_x, loc_y;
		for (int j = 1; j <= M; j++) {
			loc_x = io.nextFloat();
			loc_y = io.nextFloat();

			for (int i = 1; i <= N; i++) {
				if ((loc_x - loc_mouse[i][0]) * (loc_x - loc_mouse[i][0])
					+ (loc_y - loc_mouse[i][1]) * (loc_y - loc_mouse[i][1])
					<= max) {
					edge[i].add(j);
				}
			}
		}

		A = new int[N + 1];
		B = new int[M + 1];
		visited = new boolean[N + 1];

		int ans = N;
		for (int i = 1; i <= N; i++) {
			if (dfs(i)) ans--;
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
def DFS(i):
  if T[i]:return 0
  T[i]=1
  for j in E[i]:
    if H[j]==-1:H[j]=i;return 1
  for j in E[i]:
    if DFS(H[j]):H[j]=i;return 1
  return 0
import sys
l,*rl=sys.stdin.readlines()
N,M,S,V=map(int,l.split());L=(S*V)**2
rl=[[*map(float,l.split())] for l in rl]
E=[0]*N
for i,(a,b) in enumerate(rl[:N]):
  E[i]=[]
  for j,(x,y) in enumerate(rl[N:]):
    if(a-x)**2+(b-y)**2-L<=0:E[i].append(j)
H=[-1]*M;R=N
for i in range(N):T=[0]*N;R-=DFS(i)
print(R)
#endif
}
