using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. -
이름 : 배성훈
내용 : Missing Piece 2001
    문제번호 : 9985번

    2024. 7. 18 에 해결 완료
    5개월 걸렸다;
    중간에서 만나기, 브루트포스, 해시 문제다

    아이디어는 다음과 같다
    최대 15번 이동하기에 한 번에 이동은 4^15(> 10억)경우의 수로 불가능하다
    중간에서 만나기를 이용하면 시작지점에서 7 ~ 8번,
    끝에서 7 ~ 8번으로

    4^8 ~ 4^9 (< 30만) 으로 경우의 수가 엄청나게 줄어든다
    이동한 경로를 해싱해서 저장해야 한다

    유튜브 강의로만 해싱을 들었지 직접 구현하려니 어려워서
    챗 gpt에 물어보고 해당 코드를 참고해서 구현했다
    아래 두 해싱은 통과된다

    다만, 보드의 숫자를 하나씩 읽을려고 했으나
    입력 예제에서 \n? 이나 공백이 추가되어 예제부터 안되기에
    비싼 Split 을 썼다
*/

namespace BaekJoon.etc
{
    internal class etc_0112
    {

        static StreamReader sr;
        static StreamWriter sw;

        struct MyArr
        {

            int[] arr;
            int len;
            int r, c;
            int hashCode;

            int xToIdx => r * size + c;
            public int HashCode => hashCode;


            public static int size;

            public void Init()
            {

                arr = new int[100];
            }

            public void Set()
            {

                len = size * size;
                int idx = 0;
                for (int i = 0; i < size; i++)
                {

                    string[] temp = sr.ReadLine().Split();

                    for (int j = 0; j < temp.Length; j++)
                    {

                        if (temp[j] == string.Empty) continue;

                        if (temp[j][0] == 'X')
                        {

                            r = idx / size;
                            c = idx % size;
                            arr[idx++] = 0;
                        }
                        else arr[idx++] = int.Parse(temp[j]);
                    }
                }

                SetHash();
            }

            public bool Move(ref MyArr _next, int _op)
            {

                _next.r = r;
                _next.c = c;

                if (_op == 1)
                {

                    _next.c++;
                    if (_next.c == size) return true;
                }
                else if (_op == 2)
                {

                    _next.c--;
                    if (_next.c == -1) return true;
                }
                else if (_op == 3)
                {

                    _next.r++;
                    if (_next.r == size) return true;
                }
                else
                {

                    _next.r--;
                    if (_next.r == -1) return true;
                }

                _next.len = len;
                for (int i = 0; i < len; i++)
                {

                    _next.arr[i] = arr[i];
                }

                _next.Swap(xToIdx, _next.xToIdx);
                _next.SetHash();
                return false;
            }

            void SetHash()
            {

                hashCode = 0;
                for (int i = 0; i < len; i++)
                {

                    hashCode = hashCode * 31 + arr[i];
                }

                /*
                // HashCode를 이용한 해시
                HashCode curHash = new();
                for (int i = 0; i < len; i++)
                {

                    curHash.Add(arr[i]);
                }

                hashCode = curHash.ToHashCode();
                */
            }

            void Swap(int _idx1, int _idx2)
            {

                int temp = arr[_idx1];
                arr[_idx1] = arr[_idx2];
                arr[_idx2] = temp;
            }
        }

        static void Main112(string[] args)
        {

            string YES = "SOLVABLE WITHIN ";
            string NO = "NOT SOLVABLE WITHIN ";
            string LAST = " MOVES";

            int MAX;
            int n, l, r;
            int ret;
            Dictionary<int, int> visit;
            MyArr[] myArr;

            Queue<int> q;
            int[] turn;

            string str;
            Solve();

            void Solve()
            {

                Init();

                bool isFirst = true;
                str = sr.ReadLine();
                while (str != string.Empty && str != null)
                {

                    if (isFirst) isFirst = false;
                    else sw.Write("\n\n");
                    Set();

                    Get();

                    sr.ReadLine();
                    str = sr.ReadLine();
                }

                sw.Close();
                sr.Close();
            }

            void Set()
            {

                string[] temp = str.Split();

                MyArr.size = int.Parse(temp[1]);
                n = int.Parse(temp[2]);
                r = n / 2;
                l = n - r;

                myArr[0].Set();

                visit[myArr[0].HashCode] = 0;

                SetBFS();
            }

            void SetBFS()
            {

                q.Enqueue(0);

                while (q.Count > 0)
                {

                    int node = q.Dequeue();
                    if (turn[node] == l) continue;

                    for (int i = 1; i <= 4; i++)
                    {

                        int next = node * 4 + i;
                        if (myArr[node].Move(ref myArr[next], i)) continue;

                        int hash = myArr[next].HashCode;
                        if (visit.ContainsKey(hash)) continue;
                        visit[hash] = turn[next];
                        q.Enqueue(next);
                    }
                }
            }

            void Get()
            {

                myArr[0].Set();
                int hash = myArr[0].HashCode;
                if (visit.ContainsKey(hash)) ret = visit[hash];
                else ret = MAX;

                GetDFS();

                if (ret == MAX)
                {

                    sw.Write(NO);
                    sw.Write(n);
                }
                else
                {

                    sw.Write(YES);
                    sw.Write(ret);
                }

                sw.Write(LAST);

                visit.Clear();
            }

            void GetDFS(int _depth = 0, int _cur = 0)
            {

                if (_depth == r) return;

                int next = 4 * _cur + 1;
                for (int i = 0; i < 4; i++, next++)
                {

                    if (myArr[_cur].Move(ref myArr[next], i)) continue;

                    int hash = myArr[next].HashCode;
                    if (visit.ContainsKey(hash)) ret = Math.Min(ret, 1 + _depth + visit[hash]);

                    GetDFS(_depth + 1, next);
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int add = 1;
                int end = 0;
                MAX = 1;
                for (int i = 1; i <= 8; i++)
                {

                    add *= 4;
                    end = MAX;
                    MAX += add;
                }

                visit = new(MAX);
                myArr = new MyArr[MAX];
                for (int i = 0; i < MAX; i++)
                {

                    myArr[i].Init();
                }

                q = new(MAX);
                turn = new int[MAX];

                for (int i = 0; i < end; i++)
                {

                    int next = i * 4 + 1;
                    for (int j = 0; j < 4; j++)
                    {

                        turn[next++] = turn[i] + 1;
                    }
                }
            }
        }
    }

#if other
// #include <cstdio>
// #include <cstring>
// #include <ctime>
using namespace std;

typedef unsigned long long ull;
inline int abs(const int& a) { return a < 0 ? -a : a; }
inline int min(const int& a, const int& b) { return a < b ? a : b; }
const int dr[] = {0,-1,0,1};
const int dc[] = {1,0,-1,0};
int N,L,lim,nlim,p[101],fi[101],ifi[101],X,Xi,PUZZLE,sumo,sumf,gans;
char chr[500];

inline int md_all() {
    int ans = 0;
    for(int i = 0; i < PUZZLE; i++) {
        int ord_r = ifi[p[i]] / N;
        int ord_c = ifi[p[i]] % N;
        ans += abs(ord_r - i/N) + abs(ord_c - i%N);
    }
    return ans;
}

inline int md_partial(const int& rx, const int& cx, const int& r, const int& c) {
    int ord_r = ifi[p[N*r+c]] / N;
    int ord_c = ifi[p[N*r+c]] % N;
    return -(abs(ord_r - r) + abs(ord_c - c)) + abs(ord_r - rx) + abs(ord_c - cx);
}

inline bool bound(const int& r, const int& c) {
    return (0 <= r && r < N && 0 <= c && c < N);
}

inline bool check() {
    for(int i = 0; i < PUZZLE; i++) if(p[i] != fi[i]) return false;
    return true;
}

inline void swap(const int& r1, const int& c1, const int& r2, const int& c2) {
    int temp = p[N*r2+c2];
    p[N*r2+c2] = p[N*r1+c1];
    p[N*r1+c1] = temp;
}

inline int check_num(int p) {
    int ans[3], i = 0, ip = p, ret = 0;
    while(chr[p] != ' ' && chr[p] != '\n' && chr[p] != '\0') {
        if(chr[p] == 'X') return N*N;
        ans[i] = chr[p]-'0';
        ++p, ++i;
    }
    for(i = 0; i < p-ip; i++) {
        ret += ans[i];
        if(i == p-ip-1) break;
        ret *= 10;
    }
    return ret;
}

inline int digit_cal(int num) {
    if(num / 10 == 0) return 1;
    if(num / 100 == 0) return 2;
    return 3;
}

bool dfs(int g, int h, int xr, int xc) {
    if(g+h > lim) {
        nlim = min(nlim, g+h);
        return false;
    }
    if(check()) {
        gans = min(gans,g);
        return true;
    }
    for(int d = 0; d < 4; d++) {
        int nr = xr + dr[d];
        int nc = xc + dc[d];
        if(bound(nr,nc)) {
            int mhp = md_partial(xr,xc,nr,nc);
            swap(xr,xc,nr,nc);
            if(dfs(g+1,h+mhp,nr,nc)) return true;
            swap(nr,nc,xr,xc);
        }
    }
    return false;
}

bool check_loop() {
    if(chr[0] != 'S') return false;
    if(chr[1] != 'T') return false;
    if(chr[2] != 'A') return false;
    if(chr[3] != 'R') return false;
    if(chr[4] != 'T') return false;
    return true;
}

int IDA_star() {
    lim = md_all();
    gans = 2e9;
    while(true) {
        nlim = 2e9;
        if(dfs(0,md_all(),Xi/N,Xi%N)) return gans;
        if(nlim == 2e9) return -1;
        lim = nlim;
        if(lim > L) break;
    }
    return -1;
}

int main(int argc, const char * argv[]) {
    while(scanf("%s",chr)) {
        if(!check_loop()) break;
        scanf("%d %d\n",&N,&L);
        X = N*N-1;
        PUZZLE = N*N;
        for(int i = 0; i < N; i++) {
            scanf(" %[^\n]",chr);
            int point = 0;
            for(int j = 0; j < N; j++) {
                int num = check_num(point);
                p[N*i+j] = num-1;
                if(num != N*N) point += digit_cal(num) + 1;
                else {
                    point += 2;
                    Xi = N*i+j;
                }
            }
        }
        for(int i = 0; i < N; i++) {
            scanf(" %[^\n]",chr);
            int point = 0;
            for(int j = 0; j < N; j++) {
                int num = check_num(point);
                fi[N*i+j] = num-1;
                if(num != N*N) point += digit_cal(num) + 1;
                else point += 2;
            }
        }
        for(int i = 0; i < PUZZLE; i++) ifi[fi[i]] = i;
        int ans = IDA_star();
        if(ans != -1 && ans <= L) printf("SOLVABLE WITHIN %d MOVES\n",ans);
        else printf("NOT SOLVABLE WITHIN %d MOVES\n",L);
        printf("\n");
        scanf("%s",chr);
    }
    return 0;
}
#elif other2
// #include <stdio.h>
// #include <vector>
// #include <algorithm>
// #include <memory.h>
// #include <queue>
// #include <iostream>
// #include <map>
// #include <queue>
// #include <unordered_map>
// #include <time.h>
// #include <math.h>
// #define debug(x) printf("(%s): %d", #x, x)
// #define MAX(a,b) ((a)>(b)?(a):(b))
// #define MIN(a,b) ((a)<(b)?(a):(b))
// #define ABS(a) ((a)>0?(a):-(a))
// #define rep(x, start, end) for(int x=start;i < end; i++)
//const int MOD = 1e9 + 7;
const long long inf = 1e18 + 7;
using namespace std;

const int HASH_TABLE_SIZE = 100007;


int nSize;
int gridSize;
int n, x;

struct Node {
	Node* next;
	int data[100];
	int val;
	bool operator==(Node& rhs) {
		for (int i = 0; i < gridSize; i++) {
			if (rhs.data[i] != data[i]) return false;
		}
		return true;
	}
	Node& operator=(Node& rhs) {
		for (int i = 0; i < gridSize; i++) data[i] = rhs.data[i];
		return *this;
	}
}N[13000];


struct Item {
	Node node;
	int preDir;
	int step;
	int xp;
};
Item q[20000];
int bck = 0;
Node st;
Node fi;

int dy[4] = {0, 1, -1, 0 };
int dx[4] = {1, 0, 0, -1 };

// 00
// 01
// 10
// 11
unsigned int getHash(Node* node) {
	unsigned int h = 5381;
	for (int i = 0; i < gridSize; i++) {
		h = node->data[i] * 5381 + h * 33;
	}
	return h;
}
struct Hash {
	Node* hashTable[HASH_TABLE_SIZE];
	void init() {
		for (int i = 0; i < HASH_TABLE_SIZE; i++) hashTable[i] = 0;
	}
	Node* find(Node* node) {
		auto h = getHash(node) % HASH_TABLE_SIZE;
		for (auto iter = hashTable[h]; iter; iter = iter->next) {
			if (*iter == *node) return iter;
		}
		return nullptr;
	}
	void add(Node* node) {
		auto h = getHash(node) % HASH_TABLE_SIZE;
		node->next = hashTable[h];
		hashTable[h] = node;
	}
}H;
char buff[8];

void solve() {
	H.init();
	scanf("%d %d", &n, &x);
	nSize = bck = 0;
	gridSize = n * n;
	
	int xsp = -1, xep = -1;
	for (int i = 0; i < gridSize; i++) {
		char c;
		scanf(" %c", &c);
		if (c == 'X') {
			st.data[i] = 0;
			xsp = i;
		}
		else {
			int val = 0;
			while (c != ' ' && c != '\n') {
				val = val * 10 + c - '0';
				scanf("%c", &c);
			}
			st.data[i] = val;
		}
	}
	for (int i = 0; i < gridSize; i++) {
		char c;
		scanf(" %c", &c);
		if (c == 'X') {
			fi.data[i] = 0;
			xep = i;
		}
		else {
			int val = 0;
			while (c != ' ' && c != '\n') {
				val = val * 10 + c - '0';
				scanf("%c", &c);
			}
			fi.data[i] = val;
		}
	}
	scanf("%s", buff);

	int targetNum = x / 2;
	int baseNum = x - targetNum;

	q[bck].node = st;
	q[bck].step = 0;
	
	N[nSize] = q[bck].node;
	N[nSize].val = 0;
	H.add(&N[nSize++]);
	q[bck].xp = xsp;
	q[bck++].preDir = -1;

	int fnt = 0;
	while (fnt < bck) {
		Item& cur = q[fnt++];
		if (cur.step == baseNum) break;
		
		int y = cur.xp / n;
		int x = cur.xp % n;
		for (int i = 0; i < 4; i++) {
			if (cur.preDir != -1 && (cur.preDir ^ 0x3) == i) continue;

			int ny = y + dy[i];
			int nx = x + dx[i];
			if (ny < 0 || ny >= n || nx < 0 || nx >= n) continue;

			q[bck].node = cur.node;
			q[bck].step = cur.step + 1;
			q[bck].preDir = i;
			q[bck].xp = ny * n + nx;
			swap(q[bck].node.data[q[bck].xp], q[bck].node.data[cur.xp]);
			if (H.find(&q[bck].node) != nullptr) continue;

			N[nSize] = q[bck].node;
			N[nSize].val = q[bck].step;
			H.add(&N[nSize++]);
			bck++;
		}
	}

	fnt = bck = 0;

	q[bck].node = fi;
	q[bck].step = 0;

	auto iter = H.find(&q[bck].node);
	int answer = 404;
	if (iter) {
		answer = min(answer, iter->val);
	}
	q[bck].xp = xep;
	q[bck++].preDir = -1;

	while (fnt < bck) {
		Item& cur = q[fnt++];
		if (cur.step == targetNum) break;

		int y = cur.xp / n;
		int x = cur.xp % n;
		for (int i = 0; i < 4; i++) {
			if (cur.preDir != -1 && (cur.preDir ^ 0x3) == i) continue;

			int ny = y + dy[i];
			int nx = x + dx[i];
			if (ny < 0 || ny >= n || nx < 0 || nx >= n) continue;

			q[bck].node = cur.node;
			q[bck].step = cur.step + 1;
			q[bck].preDir = i;
			q[bck].xp = ny * n + nx;
			swap(q[bck].node.data[q[bck].xp], q[bck].node.data[cur.xp]);

			auto iter = H.find(&q[bck].node);
			if (iter) {
				answer = min(answer, iter->val + q[bck].step);
			}
			bck++;
		}
	}

	if (answer == 404) {
		printf("NOT SOLVABLE WITHIN %d MOVES\n\n", x);
	}
	else {
		printf("SOLVABLE WITHIN %d MOVES\n\n", answer);
	}
}
int main() {
	int testCase = 10;
	//scanf("%d", &testCase);
	// cin >> testCase;
	for (int i = 1; i; i++) {
		if (scanf("%s", buff) != EOF) solve();
		else break;
	}
}



#elif other3
// # 플래티넘 2

import sys

input = sys.stdin.readline
DX, DY = [-1, 1, 0, 0], [0, 0, -1, 1]


def find_x(board):
    for i in range(len(board)):
        for j in range(len(board)):
            if board[i][j] == "X":
                return i, j


def convert_to_string(board):
    return "".join(["".join(line) for line in board])


def dfs(x, y, depth, limit, board, movements):
    if depth > limit:
        return

    key = convert_to_string(board)

    if key in movements and movements[key] <= depth:
        return

    movements[key] = min(movements.get(key, depth), depth)

    for i in range(4):
        nx, ny = x + DX[i], y + DY[i]

        if 0 <= nx < size and 0 <= ny < size:
            board[x][y], board[nx][ny] = board[nx][ny], board[x][y]
            dfs(nx, ny, depth + 1, limit, board, movements)
            board[x][y], board[nx][ny] = board[nx][ny], board[x][y]


while True:
    try:
        _, size, counts = input().split()
        size, counts = int(size), int(counts)

// # 시작 전 보드
        origin_board = [input().split() for _ in range(size)]
        sx, sy = find_x(origin_board)
        origin_movements = {}
        origin_movement_count = counts // 2

// # 완료 후 보드
        target_board = [input().split() for _ in range(size)]
        ex, ey = find_x(target_board)
        target_movements = {}
        target_movement_count = counts // 2 if counts % 2 == 0 else counts // 2 + 1

        _ = input()

// # 서로 counts의 반만큼 따로 움직여서 board가 동일해지는 구간의 최소 거리를 구함
        dfs(sx, sy, 0, origin_movement_count, origin_board, origin_movements)
        dfs(ex, ey, 0, target_movement_count, target_board, target_movements)

// # 딕셔너리에서 같은 모양의 board를 찾아 최소 거리를 더한 후, counts내로 들어왔는지 확인하고 결과 출력
        min_dist = 99999999

        for board, dist in origin_movements.items():
            if board not in target_movements:
                continue

            min_dist = min(min_dist, dist + target_movements[board])

        if min_dist == 99999999:
            print(f"NOT SOLVABLE WITHIN {counts} MOVES\n")
        else:
            print(f"SOLVABLE WITHIN {min_dist} MOVES\n")

    except ValueError:
        break

#elif other4
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Set;
import java.util.StringTokenizer;

public class Main {
	public static int[][] dir = {{1,0},{0,1},{-1,0},{0,-1}};
	
	public static class node {
		ArrayList<Integer> al ;
		int y=0,x=0;
		node(){};
		node(int size){
			this.al = new ArrayList<>(size*size);
			for(int i=0;i<size*size;i++) {
				al.add(-1);
			}
		}
	}
	
	public static void main(String[] args) throws Exception {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		String str = null;
		StringBuilder sb = new StringBuilder();
		while( (str =br.readLine()) != null) {
			String[] sstr = str.split(" ");
			int M = Integer.parseInt(sstr[1]);
			int N = Integer.parseInt(sstr[2]);
			node map[] = new node[2]; 
			for(int i=0;i<2;i++) {
				map[i] = new node(M);
				for(int y=0;y<M;y++) {
					StringTokenizer st = new StringTokenizer(br.readLine());
					for(int x=0;x<M;x++) {
						String now = st.nextToken();
						if(now.equals("X")) {
							int index = y*M+x;
							map[i].y=y;
							map[i].x=x;
							map[i].al.remove(index);
							map[i].al.add(index,0);
						} else {
							int index = y*M+x;
							map[i].al.remove(index);
							map[i].al.add(index,Integer.parseInt(now));
						}
					}
				}
			}
		
			String end = br.readLine();
			HashMap<ArrayList<Integer>, Integer> hm[] = new HashMap[2];
			hm[0] = new HashMap<>();
			hm[1] = new HashMap<>();
			for(int i =0 ; i < 2 ; i ++) {
				ArrayDeque<node> dq = new ArrayDeque<>();
				hm[i].put(map[i].al, 0);
				dq.add(map[i]);
				for(int j =0; j<(N/2 + (N%2 & i));j++) {
					int size = dq.size();
					while(size>0) {
						size--;
						node now = dq.poll();
						for(int k=0;k<4;k++) {
							int nexty = now.y+dir[k][0];
							int nextx = now.x+dir[k][1];
							if(nexty>=0&&nextx>=0&&nexty<M&&nextx<M) {
								int pos = now.y*M+now.x;
								int npos = nexty*M+nextx;
								
								Collections.swap(now.al, pos, npos);
								if(!hm[i].containsKey(now.al)) {
									hm[i].put((ArrayList<Integer>) now.al.clone(), j+1);
									node next = new node(M);
									next.x = nextx;
									next.y = nexty;
									next.al = (ArrayList<Integer>) now.al.clone();
									dq.add(next);
								}
								Collections.swap(now.al, npos, pos);
							}
						}
					}
				}
			}
			
		
			int answer = 987654321;
			Set<ArrayList<Integer>> keys = hm[0].keySet();
			for (ArrayList<Integer> key : keys) {
				if(hm[1].containsKey(key)) {
					int res = hm[1].get(key)+ hm[0].get(key);
					answer = answer > res ? res : answer; 
				}
			}
			if(answer <=N) {
				sb.append("SOLVABLE WITHIN "+answer+" MOVES"+"\n\n");
			} else {
				sb.append("NOT SOLVABLE WITHIN "+N+" MOVES"+"\n\n");
			}
			
			
		}System.out.print(sb);
	}

}

#endif
}
#if study
        static void Main112(string[] args)
        {

            int[] array1 = new int[100];
            int[] array2 = new int[100];
            int[] array3 = new int[100];

            // 예시 데이터를 배열에 채워 넣습니다.
            for (int i = 0; i < 100; i++)
            {
                array1[i] = i;
                array2[i] = i; // array2는 array1과 동일한 값
                array3[i] = 100 - i; // array3은 array1과 다른 값
            }

            HashSet<int[]> set1 = new HashSet<int[]>(new IntArrayComparer());

            set1.Add(array1);
            set1.Add(array2); // array2는 array1과 동일한 값이므로 추가되지 않음
            set1.Add(array3); // array3은 array1과 다른 값이므로 추가됨

            Console.WriteLine($"HashSet contains {set1.Count} unique arrays.");

            HashSet<CustomHashIntArray> set2 = new HashSet<CustomHashIntArray>();

            CustomHashIntArray arrayWithHash1 = new CustomHashIntArray(array1);
            CustomHashIntArray arrayWithHash2 = new CustomHashIntArray(array2);
            CustomHashIntArray arrayWithHash3 = new CustomHashIntArray(array3);

            set2.Add(arrayWithHash1);
            set2.Add(arrayWithHash2); // array2는 array1과 동일한 값이므로 추가되지 않음
            set2.Add(arrayWithHash3); // array3은 array1과 다른 값이므로 추가됨

            Console.WriteLine($"HashSet contains {set2.Count} unique arrays.");
        }

        public class IntArrayComparer : IEqualityComparer<int[]>
        {
            public bool Equals(int[] x, int[] y)
            {
                if (x.Length != y.Length) return false;
                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] != y[i]) return false;
                }
                return true;
            }

            public int GetHashCode(int[] array)
            {
                HashCode hash = new HashCode();
                foreach (int element in array)
                {
                    hash.Add(element);
                }
                return hash.ToHashCode();
            }
        }

        public class CustomHashIntArray
        {
            private readonly int[] _array;
            private readonly int _hashCode;

            public CustomHashIntArray(int[] array)
            {
                _array = array;
                _hashCode = ComputeHashCode(array);
            }

            private static int ComputeHashCode(int[] array)
            {
                int hash = 17;
                foreach (int element in array)
                {
                    // 커스텀 해시 함수: 요소를 31과 곱하고 XOR 연산을 통해 결합
                    hash = hash * 31 + element;
                }
                return hash;
            }

            public override bool Equals(object obj)
            {
                if (obj is CustomHashIntArray other)
                {
                    if (_array.Length != other._array.Length) return false;
                    for (int i = 0; i < _array.Length; i++)
                    {
                        if (_array[i] != other._array[i]) return false;
                    }
                    return true;
                }
                return false;
            }

            public override int GetHashCode()
            {
                return _hashCode;
            }
        }
#endif

