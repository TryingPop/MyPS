using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 29
이름 : 배성훈
내용 : 단방향 링크 네트워크
    문제번호 : 3295번

	이분 매칭 문제다
	이분 매칭 유형으로 들어온 문제라 이분 매칭 어떻게 쓸지만 고민해서 비교적 쉽게 풀었다
	다만, 이분 매칭이 아닌 힌트없이 왔다면 힘들거 같다

	아이디어는 다음과 같다
	노드의 개수를 k라 하면 링인 경우 k점, 선형인 경우 k - 1점이다
	이를 다른 방향으로 보면, k는 간선의 개수와 일치한다
	그래서, 노드간 간선을 최대한 연결해주면 된다
	이렇게 이분매칭을 하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0653
    {

        static void Main653(string[] args)
        {

            int MAX = 1_000;
            StreamReader sr;
            StreamWriter sw;

            int n;
            List<int>[] line;
            int[] match;
            bool[] visit;
            Solve();

            void Solve()
            {

                Init();
                int test = ReadInt();
                while(test-- > 0)
                {

                    Input();

                    int ret = 0;
                    for (int i = 0; i < n; i++)
                    {

                        Array.Fill(visit, false, 0, n);
                        if (DFS(i)) ret++;
                    }

                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            bool DFS(int _n)
            {

                for (int i = 0; i < line[_n].Count; i++)
                {

                    int next = line[_n][i];
                    if (visit[next]) continue;
                    visit[next] = true;

                    if (match[next] == -1 || DFS(match[next]))
                    {

                        match[next] = _n;
                        return true;
                    }
                }
                return false;
            }

            void Input()
            {

                n = ReadInt();
                for (int i = 0; i < n; i++)
                {

                    line[i].Clear();
                    match[i] = -1;
                }

                int m = ReadInt();
                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();
                    line[f].Add(b);
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                line = new List<int>[MAX];
                for (int i = 0; i < MAX; i++)
                {

                    line[i] = new();
                }

                match = new int[MAX];
                visit = new bool[MAX];
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
// #define _CRT_SECURE_NO_WARNINGS
// #include <iostream>
// #include <vector>
// #include <string>
// #include <map>
// #include <queue>
// #include <stack>
// #include <utility>
// #include <algorithm>
// #include <cstring>
// #include <math.h>
// #include <set>
// #include <cassert>
// #include <bitset>
// #include <sstream>
// #include <cmath>
// #include <random>
// #include <numeric>
// #define MOD 1000000007
// #define BOUND 2000
// #define MININT -2147483647
// #define MAXINT 2147483647
// #define MAXN 500005
using namespace std;
using lld = long long;
using pii = pair<int, int>;
using pll = pair<lld, lld>;


int N, M, p[BOUND], q[BOUND], rn[BOUND];
bool used[BOUND];
vector<int> w[BOUND];

void bfsRank() {
	queue<int> Q;
	for (int i = 0; i < N; i++) {
		if (!used[i]) {
			rn[i] = 0;
			Q.push(i);
		}
		else rn[i] = 1e9;
	}
	while (!Q.empty()) {
		int now = Q.front();
		Q.pop();
		for (int tar : w[now]) {
			if (q[tar] != -1 && rn[q[tar]] == 1e9) {
				rn[q[tar]] = rn[now] + 1;
				Q.push(q[tar]);
			}
		}
	}
}

bool bipartite(int now) {
	for (int tar : w[now]) {
		if (q[tar] == -1 || (rn[q[tar]] == rn[now] + 1 && bipartite(q[tar]))) {
			used[now] = true;
			p[now] = tar;
			q[tar] = now;
			return true;
		}
	}
	return false;
}

int main() {
	ios::sync_with_stdio(false); cin.tie(NULL); cout.tie(NULL);

	int T;
	cin >> T;
	while (T--) {
		cin >> N >> M;
		for (int i = 0; i < M; i++) {
			int x, y;
			cin >> x >> y;
			w[x].push_back(1000 + y);
		}
		int res = 0;
		memset(p, -1, sizeof p);
		memset(q, -1, sizeof q);
		while (true) {
			bfsRank();
			int cnt = 0;
			for (int i = 0; i < N; i++) if (!used[i] && bipartite(i)) cnt++;
			if (!cnt) break;
			res += cnt;
		}
		cout << res << '\n';
		memset(used, false, sizeof used);
		for (int i = 0; i < N; i++) w[i].clear();
	}
	
	return 0;
}
#elif other2
import java.io.*;
import java.util.*;

class Main {

	static class Reader {
		
		final private int BUFFER_SIZE = 1<<16;
		private DataInputStream di;
		private byte[] buffer;
		private int bufferPointer, bytesRead;
		
		public Reader() {
			di = new DataInputStream(System.in);
			buffer = new byte[BUFFER_SIZE];
			bufferPointer = bytesRead = 0;
		}
		public Reader(String File_name) throws IOException {
			di = new DataInputStream(new FileInputStream(File_name));
			buffer = new byte[BUFFER_SIZE];
			bufferPointer = bytesRead = 0;
		}
		public int nextInt() throws IOException{
			int ret = 0;
			byte c = read();
			while(c <= ' ') c = read();
			boolean neg = (c == '-');
			if(neg) c = read();
			do ret = ret*10+c-'0';
			while((c=read())>='0' && c<='9');
			return neg ? -ret : ret;
		}
		private void fillBuffer() throws IOException {
			bytesRead = di.read(buffer, bufferPointer = 0, BUFFER_SIZE);
			if(bytesRead == -1) buffer[0] = -1;
		}
		private byte read() throws IOException {
			if(bufferPointer == bytesRead) fillBuffer();
			return buffer[bufferPointer++];
		}
		public void close() throws IOException {
			if(di == null) return;
			di.close();
		}
	}
	static int B[];
	static List<Integer> adj[];
	
	static boolean dfs(int a, boolean[] v) {
		v[a] = true;
		for(int b : adj[a]) {
			if(B[b] == -1 || !v[B[b]] && dfs(B[b], v)) {
				B[b] = a;
				return true;
			}
		}
		return false;
	}
	public static void main(String[] args) throws Exception {
		Reader rs = new Reader();
		BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(System.out));
		for(int T=rs.nextInt(); T-->0;) {
			int n = rs.nextInt(), m = rs.nextInt(), cnt = 0;
			B = new int[n];
			Arrays.fill(B, -1);
			adj = new ArrayList[n];
			
			for(int i=0; i<n; i++)
				adj[i] = new ArrayList<>();
			
			while(m-- > 0)
				adj[rs.nextInt()].add(rs.nextInt());
			
			for(int i=0; i<n; i++)
				if(dfs(i, new boolean[n]))
					cnt++;
			
			bw.write(cnt + "\n");
		}
		bw.close();
	}
}
#elif other3
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
    adj = [[] for _ in range(N)]
    for _ in range(M):
        a,b=map(int,input().split())
        adj[a].append(b)

    match=0
    B=[-1]*N
    for i in range(N):
        visited=[0]*N
        match+=dfs(i)
        if match==N: break
    print(match)
#endif
}
