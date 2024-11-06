using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 26
이름 : 배성훈
내용 : 조 나누기
    문제번호 : 9496번

    이분 매칭 문제다
    아이디어는 다음과 같다
    1 - 2, 2 - 3, 3 - 1을 이분 매칭해본다
    이 중에 가장 작은게 빠질 인원이다

    1 - 2가 가장 작다고 하자 그러면
    1 - 2학년을 같은 조에 넣고, 3학년을 다른 조에 둔다
    이러면 가장 작은게 된다!

    호프크로프트 카프 알고리즘을 조금만 참고하고 작성해서 그런지
    이분 매칭 구현으로 5번 틀렸다;

    BFS를 구현해놓고 안썼다;(이걸로 2번), &&연산자가 있어야 하는 부분이 ||연산자가 있었다;
    그리고, 기존 배열을 그대로 이용하기에 간선 체크, 수정전 초기에 team을 배열 2차원 배열로 했었는데
    이를 제대로 수정안해 인덱스에러 떴다;
*/

namespace BaekJoon.etc
{
    internal class etc_0728
    {

        static void Main728(string[] args)
        {

            int INF = 10_000;
            StreamReader sr;
            int n;
            int[] team;

            Queue<int> q;
            bool[][] line;

            int[] A, B, lvl;
            bool[] visit;
            Solve();

            void Solve()
            {

                Input();

                int min = 10_000;
                min = Math.Min(min, Matching(0, 1));
                min = Math.Min(min, Matching(1, 2));
                min = Math.Min(min, Matching(2, 0));

                Console.WriteLine(n - min);
            }

            int Matching(int _t1, int _t2)
            {

                Array.Fill(A, -1);
                Array.Fill(B, -1);
                Array.Fill(visit, false);

                int ret = 0;
                while (true)
                {

                    BFS(_t1, _t2);

                    int match = 0;

                    for (int i = 0; i < n; i++)
                    {

                        if (team[i] == _t1 && !visit[i] && DFS(i, _t2)) match++;
                    }

                    if (match == 0) break;
                    ret += match;
                }

                return ret;
            }

            void BFS(int _t1, int _t2)
            {

                for (int i = 0; i < n; i++)
                {

                    if (team[i] != _t1) continue;
                    if (!visit[i])
                    {

                        lvl[i] = 0;
                        q.Enqueue(i);
                    }
                    else lvl[i] = INF;
                }

                while (q.Count > 0)
                {

                    int a = q.Dequeue();

                    for (int i = 0; i < n; i++)
                    {

                        if (team[i] != _t2 || !line[a][i]) continue;

                        if (B[i] != -1 && lvl[B[i]] == INF)
                        {

                            lvl[B[i]] = lvl[a] + 1;
                            q.Enqueue(B[i]);
                        }
                    }
                }
            }

            bool DFS(int _a, int _t2)
            {

                for (int i = 0; i < n; i++)
                {

                    if (team[i] != _t2 || !line[_a][i]) continue;

                    if (B[i] == -1 || lvl[B[i]] == lvl[_a] + 1 && DFS(B[i], _t2))
                    {

                        visit[_a] = true;
                        A[_a] = i;
                        B[i] = _a;
                        return true;
                    }
                }

                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());

                team = new int[n];
                for (int i = 0; i < n; i++)
                {

                    team[i] = sr.Read() - '1';
                }

                if (sr.Read() == '\r') sr.Read();

                line = new bool[n][];
                for (int i = 0; i < n; i++)
                {

                    line[i] = new bool[n];
                    for (int j = 0; j < n; j++)
                    {

                        line[i][j] = sr.Read() == 'Y';
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                q = new(n);
                visit = new bool[n];
                A = new int[n];
                B = new int[n];
                lvl = new int[n];
                sr.Close();
            }
        }
    }

#if other
// #include <iostream>
// #include <fstream>

using namespace std;

constexpr char ENDL = '\n';
constexpr char SP   = ' ';

inline void ioInit(void)
{
	ios_base::sync_with_stdio(false);
	cin.tie(NULL);	cout.tie(NULL);
// #ifdef JH_DEBUG
	static ifstream in( "9496_input.txt");
	cin.rdbuf( in.rdbuf());
// #endif
}

///////////////////////////
// #include <algorithm>
constexpr int MAXSTUDENT = 51;
constexpr int NGRADE = 3;

typedef long long ll;

int N, grade[MAXSTUDENT];

ll gradeMask[3];
ll hateMask[MAXSTUDENT];

inline int myMax(int x, int y)
{
    return (x > y) ? x : y;
}

inline int myMax(int x, int y, int z)
{
    return myMax(x, myMax(y, z));
}

void dPrintBin(ll val, int bits)
{
    while (bits--)
    {
        cout << ((val & (1ll << bits)) ? 1 : 0);
	}
	cout << ENDL;
}

void readData(void)
{
    char G[MAXSTUDENT];
    char rel[MAXSTUDENT];


    gradeMask[0] = gradeMask[1] = gradeMask[2] = 0;

    cin >> N >> G;
    for (int i = 0; i < N; i++)
    {
        grade[i] = G[i] - '1';
        gradeMask[0] <<= 1; gradeMask[1] <<= 1; gradeMask[2] <<= 1;
        gradeMask[grade[i]] |= 1;

        cin >> rel;
        hateMask[i] = 0;
        for (int j = 0; j < N; j++)
        {
            hateMask[i] <<= 1;
            if (rel[j] == 'Y') hateMask[i] |= 1;
        }
    }
}

int cntBits(long long val)
{
    int cnt = 0;

    while (val != 0)
    {
        if ((val & 1) != 0) cnt++;
        val >>= 1;
    }

    return cnt;
}

int getMaxPeople(int g1, int g2)
{
    int ret = cntBits(gradeMask[g1]);

    for (int i = 0; i < N; i++)
    {
        if (grade[i] == g2)
        {
            ll mask = gradeMask[g1] & ~hateMask[i];
            int cnt = cntBits(mask);

            for (int j = 0; j < N; j++)
            {
                if (grade[j] == g2 && (mask & hateMask[j]) == 0) cnt++;
            }
            ret = myMax(ret, cnt);
        }
    }

    return ret;
}

int main(void)
{
    ioInit();

    readData();
    cout << myMax(
        cntBits(gradeMask[0]) + myMax(getMaxPeople(1, 2), getMaxPeople(2, 1)),
        cntBits(gradeMask[1]) + myMax(getMaxPeople(0, 2), getMaxPeople(2, 0)),
        cntBits(gradeMask[2]) + myMax(getMaxPeople(0, 1), getMaxPeople(1, 0))
    ) << ENDL;


    return 0;
}
#elif other2
/ /# BOJ 9496
import sys
input = sys.stdin.readline

def dfs(x,b):
    if visited[x]:
        return 0
    visited[x] = 1
    for y in range(N):
        if students[y] == b and E[x][y] == 1: # 대상학년일 경우만
            if match[y] == -1 or dfs(match[y],b):
                match[y] = x
                return 1
    return 0
    
N = int(input())
students = [int(i) for i in input().rstrip()]
E = [ [*map(lambda x:0 if x=='N' else 1,input().rstrip())]for _ in range(N) ]

cnts = []
for a,b in (1,2), (2,3), (3,1): # 서로 다른 학년에 대해 이분매칭
    match = [-1]*N # N의 일부분(각학년)이지만 어차피 결과에는 영향 없으므로 그대로
    cnt = 0
    for x in range(N):
        visited = [0]*N
        if students[x] == a:
            cnt += 1 if dfs(x,b) else 0
    cnts.append(cnt)
print(N-min(cnts))
#elif other3
import java.util.*;
import java.io.*;

class Main {
	
	static int n, ans, B[], grade[];
	static List<Integer> adj[];
	static boolean[] v;
	
	static boolean dfs(int a, int e) {
		v[a] = true;
		for(int b : adj[a]) {
			if(grade[b] != e) continue;
			if(B[b] == -1 || !v[B[b]] && dfs(B[b], e)) {
				B[b] = a;
				return true;
			}
		}
		return false;
	}
	public static void main(String[] args) throws Exception {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		n = Integer.parseInt(br.readLine());
		B = new int[n];
		v = new boolean[n];
		grade = new int[n];
		adj = new ArrayList[n];
		
		char[] temp = br.readLine().toCharArray();
		for(int i=0; i<n; i++)
			grade[i] = temp[i]-'1';

		for(int i=0; i<n; i++) {
			temp = br.readLine().toCharArray();
			adj[i] = new ArrayList<>();
			for(int j=0; j<n; j++)
				if(temp[j] == 'Y')
					adj[i].add(j);
		}
		for(int s=0; s<3; s++) {
			int cnt = n, e = (s+1)%3;
			Arrays.fill(B, -1);
			for(int i=0; i<n; i++) {
				if(grade[i] == s) {
					Arrays.fill(v, false);
					if(dfs(i, e)) cnt--;
				}
			}
			ans = Math.max(ans, cnt);
		}
		System.out.println(ans);
	}
}
#endif
}
