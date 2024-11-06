using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 22
이름 : 배성훈
내용 : 판치기
    문제번호 : 23085번

    BFS 문제다
    구현을 잘못해서 한번에 무조건 k개를 못옮겨 1번 틀렸다;
    이후 이부분을 수정하니 이상없이 통과했다

    아이디어는 다음과 같다
    돌아간 T의 개수를 숫자로 변경한 뒤,
    BFS로 n개가 되는지 확인하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0769
    {

        static void Main769(string[] args)
        {

            StreamReader sr;;

            int n, k;
            bool[] visit;
            int[] turn;
            Queue<int> q;
            int total;

            Solve();

            void Solve()
            {

                Input();

                BFS();

                int ret = turn[n];
                Console.Write(ret);
            }

            void BFS()
            {

                q = new Queue<int>(n);
                Array.Fill(turn, -1);
                turn[total] = 0;
                visit[total] = true;
                q.Enqueue(total);

                while(q.Count > 0)
                {

                    int node = q.Dequeue();
                    int head = n - node;

                    int s = Math.Min(k, node);
                    int e = Math.Min(k, head);

                    for (int i = -s; i <= 0; i++)
                    {

                        int other = i + k;
                        if (e < other) continue;
                        int add = i + other;
                        int next = node + add;

                        if (visit[next]) continue;
                        visit[next] = true;
                        turn[next] = turn[node] + 1;
                        q.Enqueue(next);
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();
                turn = new int[n + 1];
                visit = new bool[n + 1];
                total = 0;
                for (int i = 0; i < n; i++)
                {

                    if (sr.Read() == 'H') continue;
                    total++;
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

class Main {

	public static void main(String[] args) throws IOException {

		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		StringTokenizer st = new StringTokenizer(br.readLine());
		int N = Integer.parseInt(st.nextToken());
		int K = Integer.parseInt(st.nextToken());
		int initialBackCnt = 0;

		for (int i = 0; i < N; i++) {
			if (br.read() == 'T') {
				initialBackCnt++;
			}
		}

		// BFS
		Queue<Integer> que = new LinkedList<>();
		int[] processed = new int[N + 1];
		int cur;
		int backCnt;
		int minBackFlip;
		int maxBackFlip;

		for (int i = 0; i <= N; i++) {
			processed[i] = -1;
		}

		que.add(initialBackCnt);
		processed[initialBackCnt] = 0;

		while (!que.isEmpty()) {
			cur = que.remove();

			if (cur == N) {
				break;
			}

			minBackFlip = Math.max(K - N + cur, 0); // 뒤집을 수 있는 뒷면의 최소 개수
			maxBackFlip = Math.min(cur, K); // 뒤집을 수 있는 뒷면의 최대 개수

			for (int i = minBackFlip; i <= maxBackFlip; i++) {
				backCnt = cur - i + Math.max((K - i), 0);

				if (processed[backCnt] > -1) {
					continue;
				}

				que.add(backCnt);
				processed[backCnt] = processed[cur] + 1;
			}
		}

		System.out.println(processed[N]);
		br.close();

	}

}
#elif other2
def bfs():
    q = []
    q.append(k)
    cnt = 0

    while q:
        cnt += 1
        for nows in range(len(q)):
            now = q.pop(0)

            for i in range(now+1):
                if m-i <= n-now:
                    nxt = now - (i - (m-i))
                    

                    if 0 <= nxt <= n:
                        if lens[nxt] == - 1:
                        
                            q.append(nxt)
                            lens[nxt] = cnt


n,m = map(int,input().split())
strs = input()
lens = [-1 for _ in range(n+1)]

k = 0

for i in range(n):
    if strs[i] == "T":
        k += 1

lens[k] = 0

bfs()

print(lens[n])

#elif other3
// #include <stdio.h>
// #include <queue>
// #include <algorithm>
using namespace std;

int main(){
    int n, k;
    char s[3010];
    scanf("%d %d", &n, &k);
    scanf("%s", s);
    int cnt = 0;
    for(int i = 0; i < n; i++){
        if(s[i] == 'T') cnt++;
    }
    int check[3010];
    for(int i = 0; i <= n; i++) check[i] = -1;
    check[cnt] = 0;
    queue<int> q;
    q.push(cnt);
    while(!q.empty()){
        int top = q.front();
        q.pop();
        for(int i = max(0, k - (n - top)); i <= min(k, top); i++){
            int next = top + k - 2 * i;
            if(check[next] == -1){
                check[next] = check[top] + 1;
                q.push(next);
            }
        }
    }
    printf("%d", check[n]);
}
#endif
}
