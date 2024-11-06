using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 25
이름 : 배성훈
내용 : 생존자 (Small, Large)
    문제번호 : 12429, 12430번

    그리디, 정렬, 배낭 문제다
    Small은 DFS로 탐색 방법으로 풀 수 있다
    그디리하게 풀었다

    아이디어는 다음과 같다
    유통기한 + 생존일이 큰 것을 나중에 먹는게 좋다
    음식 2개가 있을 때, 유통기한을 각각 p1, p2, 생존을 s1, s2라 하자
    그러면 오래 살기 위해 두 음식을 먹을 순서를 4! 경우로 나눠서 보았을 때,
    p1 + s1 < p2 + s2 => 1을 먼저 먹는게 좋음을 알 수 있다
        4!이지만, p1 < p2라는 고정된 경우로 해서 12경우로 했다
        p1 < p2 < s1 < s2, p1 < s1 < p2 < s2, s1 < p1 < p2 < s2, ..... 
    
    해당 방법으로 정렬하고, 2^N을 배낭 문제로 봐서 N * P(최대 굶는일자)로 해서 최대 일수를 계산했다
*/

namespace BaekJoon.etc
{
    internal class etc_0618
    {

        static void Main618(string[] args)
        {

            int MAXP = 100_000;
            int MAXS = 1_000;
            int N = 1_000;

            StreamReader sr;
            StreamWriter sw;

            int test;
            int n;
            (int p, int s)[] arr;
            bool[] dp = new bool[MAXP + MAXS + 1];

            Solve();

            void Solve()
            {

                Input();
                Comparer<(int p, int s)> comp = Comparer<(int p, int s)>.Create((x, y) => (x.s + x.p).CompareTo(y.s + y.p));

                for (int t = 1; t <= test; t++)
                {

                    Init();

                    Array.Sort(arr, 0, n, comp);
                    Array.Fill(dp, false);
                    dp[0] = true;
                    for (int i = 0; i < n; i++)
                    {

                        for (int j = arr[i].p; j >= 0; j--)
                        {

                            if (!dp[j]) continue;
                            int next = j + arr[i].s;
                            dp[next] = true;
                        }
                    }

                    for (int i = MAXP + MAXS; i >= 0; i--)
                    {

                        if (!dp[i]) continue;
                        sw.Write($"Case #{t}: {i}\n");
                        break;
                    }
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                n = ReadInt();
                for (int i = 0; i < n; i++)
                {

                    arr[i] = (ReadInt(), ReadInt());
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                test = ReadInt();
                arr = new (int p, int s)[N];
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
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.Collections;
import java.util.StringTokenizer;

public class Main {

    static int n, answer, time;
    static int[][] foods;
    static boolean[] visited;
    static ArrayList<Food> list;

    static class Food implements Comparable<Food>{
        int p, s;

        public Food (int p, int s) {
            this.p = p;
            this.s = s;
        }

        @Override
        public int compareTo(Food o) {
            if(this.s == o.s){
                if(this.p < o.s){
                    return -1;
                }
            }
            return o.s - this.s;
        }
    }

    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        int testCase = Integer.parseInt(br.readLine());
        for (int t = 1; t <= testCase; t++) {
            n = Integer.parseInt(br.readLine());
            foods = new int[n][2];
            visited = new boolean[n];
            list = new ArrayList<>();
            answer = 0;
            time = 0;

            for (int i = 0; i < n; i++) {
                StringTokenizer st = new StringTokenizer(br.readLine());
                    foods[i][0] = Integer.parseInt(st.nextToken());
                    foods[i][1] = Integer.parseInt(st.nextToken());
                    list.add(new Food(foods[i][0], foods[i][1]));

            }

            Collections.sort(list);

            solve();

            System.out.println("Case #" + t + ": " + answer);
        }
    }

    public static void solve() {

        answer = Math.max(answer, time);

        for (int i = 0; i < list.size(); i++) {
            if (!visited[i] && list.get(i).p >= time){
                visited[i] = true;
                time += list.get(i).s;
                solve();
                time -= list.get(i).s;
                visited[i] = false;
            }
        }

    }
}

#elif other2
import sys
from itertools import permutations
from collections import deque
input = sys.stdin.readline

t = int(input())

def dfs(time):
    global survive
    survive = max(survive,time)

    for next_food in range(len(foods)):
        if not check[next_food] and foods[next_food][0]>=time:
            check[next_food] = 1
            dfs(time+foods[next_food][1])
            check[next_food] = 0

for i in range(t):
    n = int(input())
    foods = []
    check = [0]*n
    survive = 0
    for j in range(n):
        p,s = map(int,input().split())
        foods.append((p,s))
    for pivot_food in range(len(foods)):
        check[pivot_food] = 1
        dfs(foods[pivot_food][1])
        check[pivot_food] = 0
    
    print(f"Case #{i+1}: {survive}")
#elif other3
// #include <cstdio>
// #include <cstdlib>
// #define NMAX 11
// #define PMAX 1003
int N;
int P[NMAX][2];
int D[PMAX];
int Answer;

int comp(const void * arg1, const void * arg2)
{
	int * a = (int*) arg1;
	int * b = (int*) arg2;
	return (a[0]+a[1]) - (b[0]+b[1]);
}
int main(int argc, char** argv) {
	int test_case, Tcase;
	
	scanf("%d",&Tcase);
	for(test_case = 1 ; test_case <= Tcase ; test_case++)
	{
		scanf("%d",&N);
		for(int i = 0 ; i < N ; i++)
			scanf("%d%d",&P[i][0],&P[i][1]);
		qsort(P,N,sizeof(int)*2,comp);
		Answer = 0;
		for(int i = 0 ; i < PMAX ; i++) D[i] = N+1;
		D[0] = -1;
		for(int i = 0 ; i < N ; i++)
			for(int j = P[i][0] ; j >= 0; j--)
				if(D[j] < i)D[j+P[i][1]] = i;
		for(int i = PMAX-1 ; i >= 0 ; i--)
		    if(D[i] < N+1) {Answer = i;break;}
		printf("Case #%d: %d\n",test_case,Answer);
	}
	return 0;
}
#elif other4
using System;
using System.Text;

public class Program
{
    struct Food : IComparable<Food>
    {
        public int p, s;
        public Food(int p, int s)
        {
            this.p = p;
            this.s = s;
        }
        public int CompareTo(Food other)
        {
            return (p + s).CompareTo(other.p + other.s);
        }
    }
    static void Main()
    {
        int t = int.Parse(Console.ReadLine());
        StringBuilder sb = new();
        for (int i = 1; i <= t; i++)
        {
            int n = int.Parse(Console.ReadLine());
            Food[] foods = new Food[n];
            for (int j = 0; j < n; j++)
            {
                string[] food = Console.ReadLine().Split(' ');
                foods[j] = new(int.Parse(food[0]), int.Parse(food[1]));
            }
            Array.Sort(foods);
            bool[] dp = new bool[1000001];
            dp[0] = true;
            int answer = int.MinValue;
            for (int j = 0; j < n; j++)
            {
                for (int k = foods[j].p; k >= 0; k--)
                {
                    if (dp[k])
                    {
                        dp[k + foods[j].s] = true;
                        answer = Math.Max(answer, k + foods[j].s);
                    }
                }
            }
            sb.Append($"Case #{i}: {answer}");
            if (i + 1 <= t)
                sb.Append('\n');
        }
        Console.Write(sb.ToString());
    }
}
#elif other5
// #include <bits/stdc++.h>
using namespace std;
typedef long long lint;
typedef pair<int, int> pi;
const int mod = 1e9 + 7;
const int MAXN = 1005;

int n;
pi a[1005];  

bool cmp(pi a, pi b){  
	return a.first + a.second < b.first + b.second;  
}  

int solve(){
	scanf("%d",&n);  
	for(int i=0; i<n; i++){  
		scanf("%d %d",&a[i].first,&a[i].second);  
	}
	sort(a, a+n, cmp);
	bitset<101001> dp, msk;
	dp[0] = 1;
	msk[0] = 1;
	int pnt = 0;
	for(int i=0; i<n; i++){  
		while(pnt < a[i].first + a[i].second){
			pnt++;
			msk[pnt] = 1;
		}
		dp |= (dp << a[i].second);
		dp &= msk;
	}  
	for(int i=101000; i>=0; i--){
		if(dp[i]) return i;
	}
}  


int main(){
	int tc;
	cin >> tc;
	for(int i=1; i<=tc; i++){
		printf("Case #%d: %d\n", i, solve());
	}
}

#endif
}
