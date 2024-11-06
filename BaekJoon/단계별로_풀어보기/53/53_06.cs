using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 9
이름 : 배성훈
내용 : Circuits
    문제번호 : 16357번

    좌표 압축, 느리게 갱신되는 세그먼트 트리, 스위핑 문제다
    처음은 세그먼트 트리 2개를 이용해 해결했고 이후, 세그먼트 트리 한 개는 불필요해보여 지웠다

    아이디어는 다음과 같다
    입력 범위다 -10000만 ~ 1000만이고 좌표값은 결과에 영향을 안미치기에
    좌표 재설정을 할겸 좌표압축을 했다 O( N log N )

    그리고 한개만 찾는다면 누적합으로 O(N) 으로 찾을 수 있지만 두 개의 선분이다
    음 가장 긴거하고 그다음에 긴거 선택하면 되는거 아니야? 라고 
    그리디 하게 접근할까 생각했지만 금방 반례가 떠올라 취소했다
        |   |   |
        |   |       |   |           
                    |   |   |
    보면 2번째 줄에 4개가 만나는 곳이 있다
    두 번째 줄에 긋는 경우 어느 다른 곳을 그어도 5개다
    그러나 맨 위와 맨아래를 그어야 최대값 6이 나온다

    해당 반례를 보고 그러면 가장자리에 하나 놓되(고정)
    다른 하나는 가장 많은 곳에 놓아 두 합의 최대값이 전체 최대값이 되지 않을까? 했고 
    따져보니 맞는거 같아 해당 방법을 채택했다

    끝점만 탐색해도 최대값이 보장되기에 중간지점은 탐색안하고 끝점만 탐색한다
    또한 첫 번째 선은 아래에서 위로 1칸씩 올라가게 하고 두 번째 선은 첫 번째 선보다 위에 있게해도
    결과값이 변하지 않기에 해당 방법으로 했다!

    그래서 누적합배열로 처음에 선을 그었을 때 만나는 최대 직사각형의 개수를 찾았다
    (처음에는 값비싼 세그먼트 트리로 찾았다; 이후 중복됨을 알고 중간 연산에 사용하는 누적합 배열을 썼다)

    이제 두 번째 줄을 그을 때 필요한 느리게 갱신되는 세그먼트 트리가 필요하다
    i를 포함하는 모든 선을 지워야한다 그리고 두 번째 선은 i보다 항상 위에 있으니
    i선 보다 위에 있는 사각형만 살린다 겹치거나 아래 있는 사각형은 지워낸다
    그리고 i선 보다 위에 있는 선 중 가장 사각형이 많이 겹치는 곳을 찾아야한다
    그래서 세그먼트 트리는 각 j에 선을 그었을 때 만나는 사각형의 개수를 담기게 했다
    그리고 구간에는 최대 값이 오게 설정했다
    그러면 해당 세그먼트 트리 0번 값은 i보다 위에 있는 값 중 직선을 그었을 때 가장 많은 사각형을 지나는 값을 갖게된다

    이렇게 아래 선과 만나는 값 + 위 선과 만나는 최대값을 비교값으로 두고
    비교값이 최대가 되는 것을 결과로 하니 이상없이 통과했다

    다만, 누적합을 구하는데 있어 끝점에서 바로 값을 빼버려 한 번 틀렸고,
    다음으로 탐색과정을 잘못 구현해 한 번 더 틀렸다;
    이후 368ms에 이상없이 통과했다

    해당 풀이 아이디어를 발견하는데 시간이 오래 걸렸다;
*/

namespace BaekJoon._53
{
    internal class _53_06
    {

        static void Main6(string[] args)
        {

            StreamReader sr;
            int[] y;
            (int n, int lazy)[] seg;
            int n;
            (int m, int M)[] pos;
            int[] sum;
            int START, END;

            Solve();

            void Solve()
            {

                Input();

                ChangeY();

                SetSeg();

                int ret = 0;
                int idx = 0;
                for (int i = 0; i < 2 * n; i++)
                {

                    int down = sum[i];

                    while (idx < n && pos[idx].m <= i)
                    {

                        Update(START, END, pos[idx].m, pos[idx].M, -1);
                        idx++;
                    }

                    int up = seg[0].n;

                    ret = up + down <= ret ? ret : up + down;
                }

                Console.WriteLine(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                y = new int[n * 2];
                pos = new (int m, int M)[n];

                for (int i = 0; i < n; i++)
                {

                    ReadInt();
                    int M = ReadInt();
                    ReadInt();
                    int m = ReadInt();

                    y[i * 2] = m;
                    y[i * 2 + 1] = M;

                    pos[i].m = m;
                    pos[i].M = M;
                }

                sr.Close();
            }

            void ChangeY()
            {

                Array.Sort(y);

                for (int i = 0; i < n; i++)
                {

                    pos[i].m = BinarySearch(pos[i].m);
                    pos[i].M = BinarySearch(pos[i].M);
                }

                Array.Sort(pos, (x, y) => x.m.CompareTo(y.m));
            }

            int BinarySearch(int _val)
            {

                int l = 0;
                int r = y.Length - 1;

                while (l <= r)
                {

                    int mid = (l + r) >> 1;
                    if (y[mid] < _val) l = mid + 1;
                    else r = mid - 1;
                }

                return r + 1;
            }

            void SetSeg()
            {

                int log = (int)Math.Ceiling(Math.Log2(2 * n + 1)) + 1;

                seg = new (int n, int lazy)[1 << log];

                START = 0;
                END = 2 * n - 1;

                sum = new int[2 * n + 1];

                for (int i = 0; i < n; i++)
                {

                    sum[pos[i].m]++;
                    sum[pos[i].M + 1]--;
                }

                int s = 0;
                for (int i = 0; i < sum.Length; i++)
                {

                    s += sum[i];
                    sum[i] = s;
                }

                for (int i = START; i <= END; i++)
                {

                    Update(START, END, i, i, sum[i]);
                }
            }

            void LazyUpdate(int _s, int _e, int _idx)
            {

                int lazy = seg[_idx].lazy;
                if (lazy == 0) return;
                seg[_idx].lazy = 0;

                seg[_idx].n += lazy;
                if (_s == _e) return;

                seg[_idx * 2 + 1].lazy += lazy;
                seg[_idx * 2 + 2].lazy += lazy;
            }

            void Update(int _s, int _e, int _chkS, int _chkE, int _add, int _idx = 0)
            {

                LazyUpdate(_s, _e, _idx);
                if (_chkS <= _s && _e <= _chkE)
                {

                    seg[_idx].n += _add;
                    if (_s != _e)
                    {

                        seg[_idx * 2 + 1].lazy += _add;
                        seg[_idx * 2 + 2].lazy += _add;
                    }

                    return;
                }

                if (_e < _chkS || _chkE < _s) return;

                int mid = (_s + _e) >> 1;

                Update(_s, mid, _chkS, _chkE, _add, _idx * 2 + 1);
                Update(mid + 1, _e, _chkS, _chkE, _add, _idx * 2 + 2);

                seg[_idx].n = Math.Max(seg[_idx * 2 + 1].n, seg[_idx * 2 + 2].n);
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool plus = c != '-';
                int ret = plus ? c - '0' : 0;
                
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
// #include <bits/stdc++.h>
// #define gibon ios::sync_with_stdio(false); cin.tie(0);
// #define fir first
// #define sec second
// #define pii pair<int, int>
// #define pll pair<ll, ll>
// #define pdd pair<long double, long double>
// #pragma GCC optimize("O3")
// #pragma GCC optimize("Ofast")
// #pragma GCC optimize("unroll-loops")
typedef long long ll;
using namespace std;
int dx[4]={0, 1, 0, -1}, dy[4]={1, 0, -1 , 0};
const int mxN=100020;
const int mxM=90;
const ll MOD=1000000007;
const ll INF=100000000000001;
int N, M;
pii A[mxN];
int B[2*mxN], S[2*mxN];
int cur;
int ans;
vector <int> coor;
int seg[8*mxN], lazy[8*mxN];
bool cmp1(pii a, pii b)
{
    return a.sec<b.sec;
}
void input()
{
    cin >> N;
    for(int i=0;i<N;i++)
    {
        int a, b, c, d;
        cin >> a >> b >> c >> d;
        A[i]={d, b};
        coor.push_back(d);
        coor.push_back(b);
    }
}
void upd(int idx, int s, int e, int str, int fin)
{
    if(str>e || s>fin)  return;
    if(str<=s && e<=fin)
    {
        seg[idx]++;
        lazy[idx]++;
        return;
    }
    seg[2*idx]+=lazy[idx];   seg[2*idx+1]+=lazy[idx];
    lazy[2*idx]+=lazy[idx]; lazy[2*idx+1]+=lazy[idx];
    lazy[idx]=0;
    int mid=(s+e)/2;
    upd(2*idx, s, mid, str, fin);
    upd(2*idx+1, mid+1, e, str, fin);
    seg[idx]=max(seg[2*idx], seg[2*idx+1]);
}
int main()
{
    gibon
    input();
    sort(coor.begin(), coor.end());
    coor.erase(unique(coor.begin(), coor.end()), coor.end());
    M=coor.size();
    for(int i=0;i<N;i++)
    {
        A[i].fir = lower_bound(coor.begin(), coor.end(), A[i].fir)-coor.begin()+1;
        A[i].sec = lower_bound(coor.begin(), coor.end(), A[i].sec)-coor.begin()+1;
    }
    sort(A, A+N, cmp1);
    for(int i=0;i<N;i++)
    {
        B[A[i].fir]++;
        B[A[i].sec+1]--;
    }
    for(int i=1;i<=M;i++)   S[i]=S[i-1]+B[i];
    for(int i=1;i<=M;i++)
    {
        ans=max(ans, S[i]+seg[1]);
        while(cur!=N && A[cur].sec<=i)
        {
            upd(1, 1, M, A[cur].fir, A[cur].sec);
            cur++;
        }
    }
    cout << ans;
}

#elif other2
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.util.*;
import java.math.*;
import java.text.DecimalFormat;

public class Main {
	
	static int[] tr, ar, la;
	static int[][] node;
	static int st, en, co = 0;
	static ArrayList<Integer>[] tree;
	static ArrayList<int[]> pos = new ArrayList<>();
	static int[][] ar2;
	static Comparator<int[]> cm = new Comparator<int[]>() {
		@Override
		public int compare(int[] o1, int[] o2) {
			if (o1[0] < o2[0]) return -1;
			if (o1[0] > o2[0]) return 1;
			return 0;
		}
	};
	
	public static void main(String[] args) throws IOException {
		BufferedReader rr = new BufferedReader(new InputStreamReader(System.in));
		BufferedWriter ww = new BufferedWriter(new OutputStreamWriter(System.out));
		int n = Integer.parseInt(rr.readLine());
		ar2 = new int[n][2];
		for (int i = 0; i < n; i++) {
			StringTokenizer st = new StringTokenizer(rr.readLine());
			st.nextToken();
			int[] c1 = {Integer.parseInt(st.nextToken()), i, 1};
			st.nextToken();
			int[] c2 = {Integer.parseInt(st.nextToken()), i, 0};
			pos.add(c1); pos.add(c2);
		}
		pab();
		pos.clear();
		tr = new int[4*co]; la = new int[2*co];
		ArrayList<Integer>[] ina = new ArrayList[co+1], oua = new ArrayList[co+1];
		for (int i = 0; i <= co; i++) {
			ina[i] = new ArrayList<>(); oua[i] = new ArrayList<>();
		}
		st = 1; en = co;
		for (int i = 0; i < n; i++) {
			ina[ar2[i][0]].add(i); oua[ar2[i][1]].add(i);
			change(ar2[i][0], ar2[i][1], 1, st, en, 1);
		}
		int ic = 0, max = 2;
		for (int i = 1; i <= co; i++) {
			for (int c : ina[i]) {
				change(ar2[c][0], ar2[c][1], -1, st, en, 1);
				ic++;
			}
			int cur = tr[1] + ic;
			if (max < cur) max = cur;
			for (int c : oua[i]) {
				change(ar2[c][0], ar2[c][1], 1, st, en, 1);
				ic--;
			}
		}
		ww.write(Integer.toString(max));
		ww.flush();
		ww.close();
	}
	
	static void pab() { // pos to ar
		Collections.sort(pos, cm);
		co = 0; 
		int bf = -1000000000;
		for(int[] c : pos) {
			if (c[0] > bf) {
				co++; bf = c[0];
			}
			ar2[c[1]][c[2]] = co;
		}
	}
	
	static void tam(int i) {
		co++;
		node[i][0] = co;
		for (int c : tree[i]) {
			tam(c);
		}
		node[i][1] = co;
	}
	
	static void in(int i, int j, int n) { // 0 <= i, j < N
		if (i == j) {
			tr[n] = ar[i-1];
			return;
		}
		int mid = (i+j)/2;
		in(i, mid, 2*n); in(mid+1, j, 2*n+1);
		tr[n] = tr[2*n] + tr[2*n+1];
	}
	
	static void change(int a, int b, int c, int i, int j, int n) {
		if (i == j) {
			tr[n] += c;
			return;
		}
		if (i == a && j == b) {
			la[n] += c;
			tr[n] = Math.max(tr[2*n], tr[2*n+1]) + la[n];
			return;
		}
		int mid = (i+j)/2;
		if (b <= mid) change(a, b, c, i, mid, 2*n);
		else if (a > mid) change(a, b, c, mid+1, j, 2*n+1);
		else {
			change(a, mid, c, i, mid, 2*n);
			change(mid+1, b, c, mid+1, j, 2*n+1);
		}
		tr[n] = Math.max(tr[2*n], tr[2*n+1]) + la[n];
	}
	
	static long out(int a, int b, int i, int j, int n) {
		if (i == a && j == b) return tr[n] + la[n] * (b - a + 1);
		int mid = (i+j)/2;
		long r = la[n] * (b-a+1);
		if (b <= mid) r += out(a, b, i, mid, 2*n);
		else if (a > mid) r += out(a, b, mid+1, j, 2*n+1);
		else {
			r += out(a, mid, i, mid, 2*n) + out(mid+1, b, mid+1, j, 2*n+1);
		}
		return r;
	}
}

#endif
}