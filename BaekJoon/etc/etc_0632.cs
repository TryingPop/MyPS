using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 27
이름 : 배성훈
내용 : 학급비 낭비하기
    문제번호 : 14498번

    이분 매칭 문제다
    양립할 수 없는 사람을 최대한 매칭시킨다
    그러면 양립할 수 없는 집단의 최소수가 나온다
    해당 의견을 무시하는게 모순 없는 경우의 최대값이 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0632
    {

        static void Main632(string[] args)
        {

            StreamReader sr;
            int[] match;
            bool[] visit;

            List<int>[] line;
            int n;

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

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                ReadInt();
                ReadInt();

                n = ReadInt();
                line = new List<int>[n + 1];
                match = new int[n + 1];
                visit = new bool[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    line[i] = new();
                }

                int[] arr1 = new int[n + 1];
                int[] arr2 = new int[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();
                    int type = ReadInt();

                    if (type == 0) b = -b;
                    else f = -f;

                    arr1[i] = f;
                    arr2[i] = b;
                }

                for (int i = 1; i < n; i++)
                {

                    for (int j = i + 1; j <= n; j++)
                    {

                        if (arr1[i] + arr1[j] == 0 || arr2[i] + arr2[j] == 0)
                        {

                            if (arr1[i] > 0) line[i].Add(j);
                            else line[j].Add(i);
                        }
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
import java.io.*;
import java.util.*;
import java.util.LinkedList;

public class Main {

    /**
     * * Stub for PS.
     */

    static StringTokenizer st;
    static StringBuilder sb;
    static BufferedReader br;

    static ArrayList<Integer> arr[];
    static int check[];
    static int b[];
    static Entry entry[];

    static boolean dfs(int i){
        if (check[i] == 1)
            return false;
        check[i] = 1;
        for (int next : arr[i]){
            if (b[next] == 0 || dfs(b[next])){
                b[next] = i;
                return true;
            }
        }
        return false;
    }

    public static void main(String[] args) throws IOException {
        br = new BufferedReader(new InputStreamReader(System.in));
        sb = new StringBuilder();

        st = new StringTokenizer(br.readLine());
        int n = Integer.parseInt(st.nextToken());
        int m = Integer.parseInt(st.nextToken());
        int k = Integer.parseInt(st.nextToken());
        entry = new Entry[k+1];
        arr = new ArrayList[k+1];
        check = new int[k+1];
        b = new int[k+1];
        for (int i = 1; i <= k; i++) {
            arr[i] = new ArrayList<>();
            st = new StringTokenizer(br.readLine());
            entry[i] = new Entry(Integer.parseInt(st.nextToken()), Integer.parseInt(st.nextToken()),
                    Integer.parseInt(st.nextToken()));
        }

        for (int i = 1; i < k; i++) {
            for (int j = i+1; j < k+1; j++) {
                if (entry[i].c != entry[j].c && (entry[i].n == entry[j].n || entry[i].m == entry[j].m)){
                    if (entry[i].c == 0)
                        arr[i].add(j);
                    else
                        arr[j].add(i);
                }
            }
        }
        int flow = 0;
        for (int i = 1; i <=k ; i++) {
            Arrays.fill(check, -1);
            if (dfs(i))
                flow++;
        }
        System.out.print(flow);

    }
    static class Entry{
        int n;
        int m;
        int c;

        public Entry(int n, int m, int c) {
            this.n = n;
            this.m = m;
            this.c = c;
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
n,m,k=map(int,input().split())
bubble=[]; chicken=[]
for _ in range(k):
    ni,mi,ci=map(int,input().split())
    if ci: chicken.append((ni,mi))
    else: bubble.append((ni,mi))

N=len(bubble); M=len(chicken)
adj = [[]for _ in range(N)]
for i in range(N):
    for j in range(M):
        if bubble[i][0]==chicken[j][0] or bubble[i][1]==chicken[j][1]: adj[i].append(j)

match=0
B=[-1]*M
for i in range(N):
    visited=[0]*N
    match+=dfs(i)
    if match==M: break
print(match)
#elif other3
// #include <bits/stdc++.h>

using namespace std;

const int max_cnt = 512;
int n, m, k, A[max_cnt + 5], B[max_cnt + 5];
bool visited[max_cnt + 5];
vector<vector<int> > graph;
vector<pair<int, int> > cinfo, dinfo;

void solve(void);
bool dfs(int p);
int bmatch(void);

int main(void) {
	solve();
	return 0;
}

void solve(void) {
	scanf("%d%d%d", &n, &m, &k);
	int i, j, a, b, c;

	// printf("passed here\n");

	for (i = 0; i < k; i++) {
		scanf("%d%d%d", &a, &b, &c);
		if (!c) cinfo.push_back(make_pair(a, b));
		else dinfo.push_back(make_pair(b, a));
	}

	graph.resize((int)cinfo.size());

	// printf("passed here\n");
	for (i = 0; i < (int)cinfo.size(); i++) {
		for (j = 0; j < (int)dinfo.size(); j++) {
			if (cinfo[i].second == dinfo[j].first || cinfo[i].first == dinfo[j].second) {
				graph[i].push_back(j);
			}
		}
	}

	// printf("passed here\n");

	printf("%d\n", bmatch());
}

int bmatch(void) {
	int cnt = (int)graph.size();
	memset(A, -1, (cnt + 1) * sizeof(int));
	memset(B, -1, ((int)dinfo.size() + 1) * sizeof(int));

	int ret = 0;
	for (int i = 0; i < cnt; i++) {
		if (A[i] == -1) {
			memset(visited, false, (cnt + 1) * sizeof(bool));
			if (dfs(i)) ret++;
		}
	}
	
	return ret;
}

bool dfs(int p) {
	visited[p] = true;
	for (auto k : graph[p]) {
		if (B[k] == -1 || (!visited[B[k]] && dfs(B[k]))) {
			A[p] = k;
			B[k] = p;
			return true;
		}
	}
	return false;
}

#endif
}
