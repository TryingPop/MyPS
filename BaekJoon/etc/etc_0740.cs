using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 29
이름 : 배성훈
내용 : 소금과 후추 (Large)
    문제번호 : 14603번

    세그먼트 트리, 슬라이딩 윈도우 문제다
    일반적인 세그먼트 트리를 만들고 저장과 n번째 위치한 값을 읽는 함수를 만들었다
    그리고 값 저장과 읽는것은 ㄹ자 경로로 슬라이딩 윈도우 기법을 사용했다
    이렇게 제출하니 1.2초대에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0740
    {

        static void Main740(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int row, col;
            int start, end;
            int w;

            int median;
            int[] seg;
            int[][] board;

            int[][] ret;

            Solve();

            void Solve()
            {

                Input();

                Init();

                SetRet();

                Output();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                start = 1;
                end = ReadInt();
                w = ReadInt();

                board = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = ReadInt();
                    }
                }

                ret = new int[row + 1 - w][];
                for (int r = 0; r < row + 1 - w; r++)
                {

                    ret[r] = new int[col + 1 - w];
                }

                median = (w * w) / 2;
                median++;
                int log = (int)Math.Ceiling(Math.Log2(end)) + 1;
                seg = new int[1 << log];
                sr.Close();
            }

            void Init()
            {

                for (int r = 0; r < w - 1; r++)
                {

                    for (int c = 0; c < w; c++)
                    {

                        int val = board[r][c];
                        Update(start, end, 1, val);
                    }
                }
            }

            void SetRet()
            {

                int chk = 0;
                while (chk + w <= row)
                {

                    // 아랫줄 추가
                    for (int c = 0; c < w; c++)
                    {

                        Update(start, end, 1, board[chk + w - 1][c]);
                    }

                    ret[chk][0] = GetVal(start, end, median);
                    for (int c = w; c < col; c++)
                    {

                        for (int r = 0; r < w; r++)
                        {

                            // 왼쪽 제거, 오른쪽 추가
                            Update(start, end, -1, board[chk + r][c - w]);
                            Update(start, end, 1, board[chk + r][c]);
                        }

                        ret[chk][c - w + 1] = GetVal(start, end, median);
                    }

                    // 윗줄 제거
                    for (int c = 0; c < w; c++)
                    {

                        Update(start, end, -1, board[chk][col - 1 - c]);
                    }

                    chk++;
                    if (chk + w > row) break;

                    // 아랫줄 추가
                    for (int c = 0; c < w; c++)
                    {

                        Update(start, end, 1, board[chk + w - 1][col - 1 - c]);
                    }

                    ret[chk][col - w] = GetVal(start, end, median);
                    for (int c = col - 1; c >= w; c--)
                    {

                        for (int r = 0; r < w; r++)
                        {

                            // 오른쪽은 제거, 왼쪽은 추가
                            Update(start, end, -1, board[chk + r][c]);
                            Update(start, end, 1, board[chk + r][c - w]);
                        }

                        ret[chk][c - w] = GetVal(start, end, median);
                    }

                    // 윗줄 제거
                    for (int c = 0; c < w; c++)
                    {

                        Update(start, end, -1, board[chk][c]);
                    }

                    chk++;
                }
            }

            void Output()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                for (int r = 0; r < row - w + 1; r++)
                {

                    for (int c = 0; c < col - w + 1; c++)
                    {

                        sw.Write($"{ret[r][c]} ");
                    }

                    sw.Write('\n');
                }

                sw.Close();
            }

            void Update(int _s, int _e, int _add, int _val, int _idx = 0)
            {

                if (_s == _e)
                {

                    seg[_idx] += _add;
                    return;
                }

                int mid = (_s + _e) / 2;
                if (mid < _val) Update(mid + 1, _e, _add, _val, _idx * 2 + 2);
                else Update(_s, mid, _add, _val, _idx * 2 + 1);

                seg[_idx] = seg[_idx * 2 + 2] + seg[_idx * 2 + 1];
            }

            int GetVal(int _s, int _e, int _chk, int _idx = 0)
            {

                if (_s == _e) return _s;

                int l = _idx * 2 + 1;
                int r = _idx * 2 + 2;

                int mid = (_s + _e) / 2;

                int ret;
                if (seg[l] < _chk) ret = GetVal(mid + 1, _e, _chk - seg[l], r);
                else ret = GetVal(_s, mid, _chk, l);

                return ret;
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
// #include <cstdio>
// #include <iostream>
// #include <vector>
// #define SZ(x) ((int)x.size())
using namespace std;
typedef long long ll;
typedef long double ld;
struct SegmentTree {
	int n, V[1<<18], OFF = 1<<17;
	SegmentTree(int n) : n(n) {}
	void update(int p, int x) {
		p += OFF;
		while(p > 1) V[p] += x, p >>= 1;
	}
	int kth(int h, int k) {
		if(h >= OFF) return h - OFF;
		if(V[h*2] >= k) return kth(h*2, k);
		return kth(h*2+1, k - V[h*2]);
	}
	inline int kth(int k) {
		return kth(1, k);
	}
};
int n, m, k, w, a[310][310], b[310][310];
int main() {
	scanf("%d%d%d%d", &n, &m, &k, &w);
	SegmentTree st(k+2);
	for(int i=0; i<n; i++)
		for(int j=0; j<m; j++)
			scanf("%d", &a[i][j]), a[i][j]++;
	for(int i=0; i<w; i++) for(int j=0; j<w; j++)
		st.update(a[i][j], 1);
	for(int i=0; i<n-w+1; i++) {
		if(i & 1) {
			for(int j=0; j<w; j++) {
				st.update(a[i-1][m-1-j], -1);
				st.update(a[i+w-1][m-1-j], 1);
			}
			b[i][m-w] = st.kth(w*w/2+1);
			for(int j=m-w-1; j>=0; j--) {
				for(int l=0; l<w; l++) {
					st.update(a[i+l][j+w], -1);
					st.update(a[i+l][j], 1);
				}
				b[i][j] = st.kth(w*w/2+1);
			}
		} else {
			if(i) for(int j=0; j<w; j++) {
				st.update(a[i-1][j], -1);
				st.update(a[i+w-1][j], 1);
			}
			b[i][0] = st.kth(w*w/2+1);
			for(int j=1; j<m-w+1; j++) {
				for(int l=0; l<w; l++) {
					st.update(a[i+l][j-1], -1);
					st.update(a[i+l][j+w-1], 1);
				}
				b[i][j] = st.kth(w*w/2+1);
			}
		}
	}
	for(int i=0; i<n-w+1; i++) {
		for(int j=0; j<m-w+1; j++) printf("%d ", b[i][j] - 1);
		puts("");
	}
	return 0;
}
#elif other2
// #include<iostream>
// #include<vector>

using namespace std;

int M, N, K, W;
int a[301][301], ret[301][301], tree[100002];

int get(int arg) {
	arg += 1;
	int ret = 0;
	while (arg > 0) {
		ret += tree[arg];
		arg = arg & (arg - 1);
	}
	return ret;
}

void add(int idx, int val) {
	idx += 1;
	while (idx <= K) {
		tree[idx] += val;
		idx += (idx & (-idx));
	}
}

int mid() {
	int s, e, m, t;
	t = (W * W + 1) / 2, s = 1, e = K;
	while (s < e) {
		m = (s + e) / 2;
		int temp = get(m);
		if (get(m) >= t) e = m;
		else s = m + 1;
	}
	return s;
}

int main() {
    scanf("%d%d%d%d", &N, &M, &K, &W);
	for (int i = 1; i <= N; i++) for (int j = 1; j <= M; j++) scanf("%d", &a[i][j]);
	for (int i = 0; i < W; i++) for (int j = 1; j <= W; j++) add(a[i][j], 1);

	int j, d;
	for (int i = 1; i <= N - W + 1; i++) {
		d = 2 * (i & 1) - 1;
		for (int k = 1 + (~i & 1) * (M - W); k <= W + (~i & 1) * (M - W); k++) {
			add(a[i - 1][k], -1);
			add(a[i + W - 1][k], 1);
		}

		j = (i & 1) ? 1 : M - W + 1;
		ret[i][j] = mid();
		j += d;

		while (1 <= j && j <= M - W + 1) {
			for (int k = i; k < i + W; k++) {
				add(a[k][j + (W - 1) * (i & 1)], 1);
				add(a[k][j + (W - 1) * (~i & 1) - d], -1);
			}
			ret[i][j] = mid();
			j += d;
		}
	}
	for (int i = 1; i <= N - W + 1; i++) {
		for (int j = 1; j <= M - W + 1; j++) printf("%d ", ret[i][j]);
		printf("\n");
	}

    return 0;
}
#elif other3
import java.io.*;
import java.math.BigDecimal;
import java.math.BigInteger;
import java.math.MathContext;
import java.util.*;
import java.lang.*;

public class Main {
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        StringTokenizer stk = new StringTokenizer(br.readLine());
        int n = Integer.parseInt(stk.nextToken());
        int m = Integer.parseInt(stk.nextToken());
        int k = Integer.parseInt(stk.nextToken());
        int w = Integer.parseInt(stk.nextToken());
        int[][] map = new int[n][m];
        for(int i=0;i<n;i++){
            stk =new StringTokenizer(br.readLine());
            for(int j=0;j<m;j++){
                map[i][j] = Integer.parseInt(stk.nextToken());
            }
        }
        StringBuilder sb =new StringBuilder();
        SegmentTree st = new SegmentTree(k+1);
        for(int i=0;i<=n-w;i++){
            st.tree = new int[st.s*2];
            for(int j=0;j<=m-w;j++){
                if(j!=0){
                    for(int p=i;p<i+w;p++){
                        st.update(map[p][j-1]+1,-1);
                        st.update(map[p][j+w-1]+1,1);
                    }
                }
                else{
                    for(int p=i;p<i+w;p++){
                        for(int q=j;q<j+w;q++){
                            st.update(map[p][q]+1,1);
                        }
                    }
                }
                sb.append((st.getMidIndex(w*w)-1)+" ");
            }
            sb.append('\n');
        }
        System.out.print(sb);
    }
}
class SegmentTree {
    int[] tree;
    int s;

    public SegmentTree(int n) {
        for (s = 1; s < n; s *= 2);
        tree = new int[s*2];
    }
    void update(int index,int x){
        int l = index +s - 1;
        tree[l] += x;
        l/=2;
        while(l>=1){
            tree[l] = tree[l*2]+tree[l*2+1];
            l/=2;
        }
    }
    int getMidIndex(int size){
        int mid = (size+1)/2;
        int l = 1;
        do{
            int left = tree[l*2];
            int right = tree[l*2+1];
            //   System.out.println(left+" "+right);
            if(left>=mid){
                l = l*2;
                //   System.out.println("왼쪽이동 "+l);
            }
            else{
                mid = mid - left;
                //   System.out.println("오른쪽이동 "+ l);
                l = l*2 + 1;
            }
        }while(l < s );
        return l - s + 1;
    }
}
#elif other4
class FenwickTree:
    def __init__(self, n):
        self.v = [0] * (n + 1)

    def sum(self, pos):
        pos += 1
        ret = 0
        while pos > 0:
            ret += self.v[pos]
            pos &= pos - 1

        return ret

    def add(self, pos, val):
        pos += 1
        while pos < len(self.v):
            self.v[pos] += val
            pos += pos & -pos


def get_mid(y, x):
    l = 0
    r = K

    while l + 1 < r:
        m = (l + r) >> 1
        if ft.sum(m) <= half:
            l = m
        else:
            r = m

    ans[y][x] = r


M, N, K, W = map(int, input().split())
half = W * W // 2
A = tuple(tuple(map(int, input().split())) for _ in range(M))
ft = FenwickTree(K + 1)

for i in range(W):
    for j in range(W):
        ft.add(A[i][j], 1)

ans = [[0] * (N - W + 1) for _ in range(M - W + 1)]

y = 0
x = 0
get_mid(y, x)

while x < N - W:
// # 왼쪽 한 줄 제거, 오른쪽 한 줄 추가
    for k in range(W):
        ft.add(A[y + k][x], -1)

    for k in range(W):
        ft.add(A[y + k][x + W], 1)

    x += 1
    get_mid(0, x)

while y < M - W:
// # 위쪽 한 줄 제거, 아래쪽 한 줄 추가
    for k in range(W):
        ft.add(A[y][x + k], -1)

    for k in range(W):
        ft.add(A[y + W][x + k], 1)

    y += 1
    get_mid(y, x)

    if y & 1:
        while x > 0:
// # 오른쪽 한 줄 제거, 왼쪽 한 줄 추가
            x -= 1
            for k in range(W):
                ft.add(A[y + k][x + W], -1)

            for k in range(W):
                ft.add(A[y + k][x], 1)

            get_mid(y, x)
    else:
        while x < N - W:
// # 왼쪽 한 줄 제거, 오른쪽 한 줄 추가
            for k in range(W):
                ft.add(A[y + k][x], -1)

            for k in range(W):
                ft.add(A[y + k][x + W], 1)
            x += 1
            get_mid(y, x)

for i in range(len(ans)):
    print(' '.join(map(str, ans[i])))

#endif
}
