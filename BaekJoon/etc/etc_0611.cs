using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 25
이름 : 배성훈
내용 : 초등 수학
    문제번호 : 11670

    이분 매칭 문제다
	연산결과를 미리 찾고 해당 결과로 이분 매칭시켰다
*/

namespace BaekJoon.etc
{
    internal class etc_0611
    {

        static void Main611(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n;
            (int f, int b, int op1, int op2, int op3)[] arr;

            List<int> match;
            bool[] visit;

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

                char[] op = { '+', '-', '*' };
                if (ret != n) sw.Write("impossible");
                else
                {

                    for (int i = 1; i <= n; i++)
                    {


                        int o = -1;
                        o = match[arr[i].op1] == i ? 0 : o;
                        o = match[arr[i].op2] == i ? 1 : o;
                        o = match[arr[i].op3] == i ? 2 : o;

                        long calc;
                        if (o == 0) calc = arr[i].f + arr[i].b;
                        else if (o == 1) calc = arr[i].f - arr[i].b;
                        else
                        {

                            calc = arr[i].f;
                            calc *= arr[i].b;
                        }

                        sw.Write($"{arr[i].f} {op[o]} {arr[i].b} = {calc}\n");
                    }
                }

                sw.Close();
            }

            bool DFS(int _n)
            {

                int next = arr[_n].op1;
                if (!visit[next])
                {

                    visit[next] = true;

                    if (match[next] == 0 || DFS(match[next]))
                    {

                        match[next] = _n;
                        return true;
                    }
                }

                next = arr[_n].op2;
                if (!visit[next])
                {

                    visit[next] = true;

                    if (match[next] == 0 || DFS(match[next]))
                    {

                        match[next] = _n;
                        return true;
                    }
                }

                next = arr[_n].op3;
                if (!visit[next])
                {

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
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                n = ReadInt();

                match = new(3 * n) { 0 };
                Dictionary<long, int> dic = new(3 * n);
                arr = new (int f, int b, int op1, int op2, int op3)[n + 1];

                int idx = 0;
                for (int i = 1; i <= n; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();
                    int op1, op2, op3;
                    if (dic.ContainsKey(f + b)) op1 = dic[f + b];
                    else
                    {

                        op1 = idx;
                        dic[f + b] = idx++;
                        match.Add(0);
                    }

                    if (dic.ContainsKey(f - b)) op2 = dic[f - b];
                    else
                    {

                        op2 = idx;
                        dic[f - b] = idx++;
                        match.Add(0);
                    }

                    long mul = f;
                    mul *= b;
                    if (dic.ContainsKey(mul)) op3 = dic[mul];
                    else
                    {

                        op3 = idx;
                        dic[mul] = idx++;
                        match.Add(0);
                    }

                    arr[i] = (f, b, op1, op2, op3);
                }

                visit = new bool[match.Count];
                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if (c == '-')
                    {

                        plus = false;
                        continue;
                    }
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
import java.io.*;
import java.util.*;

public class Main {
	private static int N;
	private static int[] A;
	private static int[] B;
	private static boolean[] visited;

	private static long[][] data;
	private static ArrayList<Integer>[] edge;

	private static class Edge implements Comparable<Edge> {
		long result;
		int idx;

		Edge(long result, int idx) {
			this.result = result;
			this.idx = idx;
		}

		@Override
		public int compareTo(Edge o) {
			if (result > o.result) {
				return 1;
			} else if (result < o.result) {
				return -1;
			} else {
				return 0;
			}
		}
	}

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

		ArrayList<Edge> arr = new ArrayList<>();

		data = new long[N + 1][2];
		for (int i = 1; i <= N; i++) {
			data[i][0] = io.nextInt();
			data[i][1] = io.nextInt();
			arr.add(new Edge(data[i][0] + data[i][1], i));
			arr.add(new Edge(data[i][0] - data[i][1], i));
			arr.add(new Edge(data[i][0] * data[i][1], i));
		}

		Collections.sort(arr);
		edge = new ArrayList[N + 1];
		for (int i = 1; i <= N; i++) {
			edge[i] = new ArrayList<>();
		}

		long[] result = new long[N * 3 + 1];
		int idx = 1;

		result[idx] = arr.get(0).result;
		edge[arr.get(0).idx].add(idx);

		for (int i = 1; i < N * 3; i++) {
			if (arr.get(i).result != result[idx]) {
				result[++idx] = arr.get(i).result;
			}

			edge[arr.get(i).idx].add(idx);
		}

		A = new int[N + 1];
		B = new int[N * 3 + 1];
		visited = new boolean[N + 1];

		boolean impossible = false;
		for (int i = 1; i <= N; i++) {
			if (!dfs(i)) {
				impossible = true;
				break;
			}
			Arrays.fill(visited, false);
		}

		if (impossible) {
			io.print("impossible");
		} else {
			for (int i = 1; i <= N; i++) {
				io.print(data[i][0]);
				if (result[A[i]] == data[i][0] + data[i][1]) {
					io.print(" + ");
				} else if (result[A[i]] == data[i][0] - data[i][1]) {
					io.print(" - ");
				} else {
					io.print(" * ");
				}
				io.print(data[i][1]);
				io.print(" = ");
				io.println(result[A[i]]);
			}
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

		private char[] nextLine() {
			byte b;
			int index = 0;

			while ((b = read()) != ASCII_n)
				bytebuffer[index++] = b;

			return convertCharArray(bytebuffer, index);
		}

		private void println() {
			write(ASCII_n);
		}

		private void print(String s) {
			for (int i = 0; i < s.length(); i++) {
				write((byte) s.charAt(i));
			}
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
from sys import stdin, setrecursionlimit
input = stdin.readline
setrecursionlimit(2600)


def solve():
	n = int(input())
	operandList = list()
	targetList = list()
	for _ in range(n):
		a, b = map(int, input().split())
		operandList += (a, b),
		targetList += {a + b, a - b, a * b},
	
	def bimatch(node):
		if visit[node]:
			return False
		else:
			visit[node] = 1
		
		for target in targetList[node]:
			if matched[target] == -1 or \
					bimatch(matched[target]):
				matched[target] = node
				return True
		else:
			return False
	
	matched = dict()
	for target in targetList:
		for value in target:
			matched[value] = -1
	
	for node in range(n):
		visit = [0] * n
		bimatch(node)
	
	valueSet = set()
	for value in matched:
		if matched[value] >= 0:
			valueSet.add(value)
	
	if len(valueSet) < n:
		print("impossible")
	else:
		for node in range(n):
			a, b = operandList[node]
			
			if a + b in valueSet and matched[a + b] == node:
				print(f"{a} + {b} = {a + b}")
			elif a - b in valueSet and matched[a - b] == node:
				print(f"{a} - {b} = {a - b}")
			else:  # a * b
				print(f"{a} * {b} = {a * b}")


solve()

#elif other3
// #include<cstdio>
// #include<cstdlib>
// #include<cstring>
// #include<algorithm>
// #include<vector>
// #include<map>
using namespace std;

long long arr[2501][2];
map<long long, int> ma;
bool check[2501];
int ans[2501];

bool dfs(int root)
{
	if (check[root])
		return false;
	check[root] = true;
	
	int match, temp;

	temp = arr[root][0] + arr[root][1];
	if (ma.find(temp) == ma.end())
	{
		ma.insert({ temp, root });
		ans[root] = 1;
		return true;
	}
	else if(dfs(ma[temp]))
	{
		ma[temp] = root;
		ans[root] = 1;
		return true;
	}

	temp = arr[root][0] - arr[root][1];
	if (ma.find(temp) == ma.end())
	{
		ma.insert({ temp, root });
		ans[root] = 2;
		return true;
	}
	else if (dfs(ma[temp]))
	{
		ma[temp] = root;
		ans[root] = 2;
		return true;
	}

	temp = arr[root][0] * arr[root][1];
	if (ma.find(temp) == ma.end())
	{
		ma.insert({ temp, root });
		ans[root] = 3;
		return true;
	}
	else if (dfs(ma[temp]))
	{
		ma[temp] = root;
		ans[root] = 3;
		return true;
	}
	return false;
}
bool matching(int n)
{
	for (int i = 1; i <= n; i++)
	{
		memset(check, false, sizeof(check));
		if (!dfs(i))
			return false;
	}
	return true;
}

int main()
{
	int n;
	scanf("%d", &n);
	for (int i = 1; i <= n; i++)
		scanf("%lld %lld", &arr[i][0], &arr[i][1]);
	if (matching(n))
	{
		for (int i = 1; i <= n; i++)
		{
			if (ans[i] == 1)
				printf("%lld + %lld = %lld\n", arr[i][0], arr[i][1], arr[i][0] + arr[i][1]);
			else if (ans[i] == 2)
				printf("%lld - %lld = %lld\n", arr[i][0], arr[i][1], arr[i][0] - arr[i][1]);
			else
				printf("%lld * %lld = %lld\n", arr[i][0], arr[i][1], arr[i][0] * arr[i][1]);
		}
	}
	else
		printf("impossible\n");
	return 0;
}
#endif
}
