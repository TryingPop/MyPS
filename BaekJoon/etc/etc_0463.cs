using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 6
이름 : 배성훈
내용 : 소트 게임
    문제번호 : 1327번

    BFS, 해시를 이용한 집합과 맵 문제다
    BFS 를 이용해 최소 턴을 찾고
    중복 방문 방지는 해시셋을 이용해 막았다
    현재 상태는 int로 저장했다
*/

namespace BaekJoon.etc
{
    internal class etc_0463
    {

        static void Main463(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput());
            int n = ReadInt();
            int k = ReadInt();

            int find = 0;
            for (int i = 1; i <= n; i++)
            {

                find = find * 10 + i;
            }

            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
            }

            sr.Close();

            Queue<(int num, int turn)> q = new(20_000);
            HashSet<int> set = new(20_000);

            int s = ArrToInt(arr);
            q.Enqueue((s, 0));
            int ret = -1;
            int[] calc = new int[n];
            set.Add(s);

            while(q.Count > 0)
            {

                var node = q.Dequeue();
                if (node.num == find)
                {

                    ret = node.turn;
                    q.Clear();
                }

                IntToArr(node.num);
                for (int i = 0; i <= n - k; i++)
                {

                    GetRotate(i);
                    int next = ArrToInt(calc);
                    if (set.Contains(next)) continue;
                    set.Add(next);

                    q.Enqueue((next, node.turn + 1));
                }
            }

            Console.WriteLine(ret);

            void GetRotate(int _n)
            {

                if (n - k < _n) return;

                for (int i = 0; i < _n; i++)
                {

                    calc[i] = arr[i];
                }

                for (int i = 0; i < k; i++)
                {

                    calc[_n + i] = arr[_n + k - 1 - i];
                }

                for (int i = _n + k; i < n; i++)
                {

                    calc[i] = arr[i];
                }

                return;
            }

            void IntToArr(int _n)
            {

                for (int i = n - 1; i >= 0; i--)
                {

                    arr[i] = _n % 10;
                    _n /= 10;
                }
            }

            int ArrToInt(int[] _arr)
            {

                int ret = 0;

                for (int i = 0;i < n; i++)
                {

                    ret = ret * 10 + _arr[i];
                }

                return ret;
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
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.util.ArrayDeque;
import java.util.Arrays;
import java.util.Queue;
import java.util.StringTokenizer;

public class Main {

	static int n, k, ans, cur;
	static boolean[] visit;
	static int[] arr;

	static Queue<Pair> q = new ArrayDeque<>();

	public static void main(String[] args) throws IOException {
		n = rstn(); k = rstn(); arr = ra();
		visit = new boolean[(1 << 25) - 1];

		for (int i = 0; i < n; ++i) {
			ans += (i << ((n - i - 1) * 3));
			cur += ((arr[n - i - 1] - 1) << (i * 3));
		}

		q.add(new Pair(cur, 0));
		visit[cur] = true;

		while (!q.isEmpty()) {
			Pair p = q.poll();

			if (p.x == ans) {
				System.out.println(p.y);
				return ;
			}

			for (int i = n - 1; i >= k - 1; --i) {
				int tempk = k;
				int nx = p.x;
				int index = i;
				while (tempk > 1) {
					int first = p.x & (7 << (index * 3));
					int second = p.x & (7 << ((index - (tempk - 1)) * 3));
					nx = nx & (~first);
					nx = nx & (~second);
					first = first >> ((tempk - 1) * 3);
					second = second << ((tempk - 1) * 3);

					nx = nx | first | second;
					tempk -= 2;
					index--;
				}
				if (visit[nx]) continue;
				visit[nx] = true;
				q.add(new Pair(nx, p.y + 1));
			}
		}

		System.out.println(-1);
	}
	static BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
	static BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(System.out));
	static StringBuilder sb = new StringBuilder();
	static StringTokenizer st;
	static int rn() throws IOException {return Integer.parseInt(br.readLine());}
	static void est() throws IOException {st = new StringTokenizer(br.readLine());}
	static int rstn() throws IOException {if(st==null||!st.hasMoreTokens()) est(); return Integer.parseInt(st.nextToken());}
	static int[] ra() throws IOException {return Arrays.stream(br.readLine().split(" ")).mapToInt(Integer::parseInt).toArray();}
	static int[] dx = {-1,0,1,0};
	static int[] dy = {0,-1,0,1};
	static boolean chk(int x, int y, int n, int m){return x<0 || y<0 || x>=n || y>=m;}
	static class Pair{int x,y;public Pair(int x, int y) {this.x = x;this.y = y;}}
	static class Triple{ int x,y,z;public Triple(int x, int y,int z) {this.x = x;this.y = y;this.z = z;}}
	static class Quad{ int w,x,y,z;public Quad(int w, int x, int y,int z) {this.w = w; this.x = x;this.y = y;this.z = z;}}
}
#elif other2
// #include <cstdio>
// #include <bitset>
// #include <deque>

const int maxN = 8;
const int maxStates = 1 << (3 * 8);

typedef std::deque<int> Queue;

int n, k;
std::bitset<maxStates> bs;

int encode(int s[maxN]) {
    int res = 0;
    for (int i=0; i<n; i++)
        res = (res << 3) + s[i];
    return res;
}

static inline void addQ(Queue &q, int i) {
    if (!bs[i]) {
        bs[i] = true;
        q.push_back(i);
    }
}

int main() {
    scanf("%d%d", &n, &k);

    int s[maxN], g[maxN];
    for (int i=0; i<n; i++) {
        scanf("%d", s+i);
        s[i]--;
        g[i] = i;
    }
    int encoded = encode(s);
    int goal = encode(g);

    int res = 0;
    bool found = true;
    if (encoded != goal) {
        found = false;

        Queue cur, next;
        addQ(cur, encoded);
        while (!found) {
            res++;
            while (!cur.empty()) {
                int e = cur.front();
                cur.pop_front();

                for (int i=n-1; i>=0; i--, e>>=3) s[i] = e & 7;
                for (int i=0; i<=n-k; i++) {
                    int t[maxN], low=i, high=i+k-1;
                    std::copy(s, s+n, t);
                    while (low < high)
                        std::swap(t[low++], t[high--]);
                    encoded = encode(t);
                    if (encoded == goal) {
                        found = true;
                        break;
                    }
                    addQ(next, encoded);
                }
            }
            cur.swap(next);
            if (cur.empty()) break;
        }
    }
    printf("%d\n", found? res: -1);
}

#elif other3
// #include <bits/stdc++.h>
// #define fastio ios::sync_with_stdio(0), cin.tie(0), cout.tie(0)
using namespace std;

int n, k, input, ans;
bitset<1 << 24> vis;

int reverseBit(int cur, int st) {
	int head = cur >> 3 * (k + st);
	int mid = cur >> 3 * st & ~(head << 3 * k);
	int tail = cur & ~(cur >> 3 * st << 3 * st);
	int temp = mid; mid = 0;
	for (int i = 0; i < k; i++) {
		mid <<= 3;
		mid |= temp & 7;
		temp >>= 3;
	}
	return (head << 3 * (k + st)) + (mid << 3 * st) + tail;
}

int main() {
	fastio;
	cin >> n >> k;
	for (int i = 0; i < n; i++) {
		int t; cin >> t;
		input <<= 3; input |= --t;
		ans <<= 3; ans |= i;
	}
	queue<pair<int, int>> Q;
	Q.push({ input, 0 }), vis[input] = true;
	while (!Q.empty()) {
		auto [cur, cost] = Q.front(); Q.pop();
		if (cur == ans) {
			cout << cost << '\n';
			return 0;
		}
		for (int i = 0; i < n - k + 1; i++) {
			int nxt = reverseBit(cur, i);
			if (!vis[nxt]) Q.push({ nxt, cost + 1 }), vis[nxt] = true;
		}
	}
	cout << -1 << '\n';
}
#endif
}
