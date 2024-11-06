using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 28
이름 : 배성훈
내용 : 소금과 후추 (small)
    문제번호 : 14602번

    구현, 정렬, 슬라이딩 윈도우 문제이다
    최악의 경우 900 * 225 < 300만이므로
    슬라이딩 윈도우 없이 매번 정렬해주고 갱신해주면서 풀었다

    large에서 슬라이딩 윈도우와 세그먼트 트리를 요구하기에 해당 문제에서 해볼 생각이다
    다른 사람 풀이를 보니 C에서 구현한 사람이 있다
    나중에 Large 풀어볼 때 막히면 참고해야겠다
*/

namespace BaekJoon.etc
{
    internal class etc_0380
    {

        static void Main380(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int row = ReadInt();
            int col = ReadInt();

            int[,] board = new int[row, col];

            int k = ReadInt();

            int w = ReadInt();

            int[] chk = new int[w * w];

            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    board[r, c] = ReadInt();
                }
            }

            sr.Close();

            int mid = (w * w) / 2;
            int[,] ret = new int[row - w + 1, col - w + 1];

            for (int r = 0; r < row - w + 1; r++)
            {

                for (int c = 0; c < col - w + 1; c++)
                {

                    for (int i = 0; i < w; i++)
                    {

                        for (int j = 0; j < w; j++)
                        {

                            chk[w * i + j] = board[r + i, c + j];
                        }
                    }

                    Array.Sort(chk);
                    ret[r, c] = chk[mid];
                }
            }

            using (StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 0; i < row - w + 1; i++)
                {

                    for (int j = 0; j < col - w + 1; j++)
                    {

                        sw.Write($"{ret[i, j]} ");
                    }
                    sw.Write('\n');
                }
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
// #include <cstring>
// #include <cmath>
// #include <algorithm>
// #include <vector>
using namespace std;
int N, M, L, K;
int arr[301][301];
vector<vector<int>> ans;
struct Segment {
	vector<int> tree;
	int size;
	Segment() {}
	Segment(int N) :size(N + 1) { tree.resize(4 * (N + 1), 0); }
	int update(int idx, int val, int node, int nl, int nr) {
		if (idx < nl || idx > nr) return tree[node];
		if (nl == nr) return tree[node] += val;
		int mid = (nl + nr) >> 1;
		return tree[node] = update(idx, val, node * 2, nl, mid) + update(idx, val, node * 2 + 1, mid + 1, nr);
	}
	void update(int idx, int val) {
		update(idx, val, 1, 0, size - 1);
	}
	int query(int val, int node, int nl, int nr) {
		if (nl == nr) return nl;
		int mid = (nl + nr) >> 1;
		if (tree[node * 2] >= val) return query(val, node * 2, nl, mid);
		else return query(val - tree[node * 2], node * 2 + 1, mid + 1, nr);
	}
	int query(int val) {
		return query(val, 1, 0, size - 1);
	}
};
int main() {
	scanf("%d%d%d%d", &N, &M, &L, &K);
	Segment seg(L);
	for (int n = 0;n < N;n++) for (int m = 0;m < M;m++) scanf("%d", &arr[n][m]);

	for (int n = 0;n < K;n++) for (int m = 0;m < K;m++)seg.update(arr[n][m], 1);
	for (int n = 0;n < N - K + 1;n++) {
		if (!(n & 1)) {		// ->
			vector<int> tmp;
			if (n != 0)
				for (int m = 0;m < K;m++) seg.update(arr[n - 1][m], -1), seg.update(arr[n + K - 1][m], 1);
			for (int m = 0;m < M - K + 1;m++) {
				if (m == 0) {}
				else {
					for (int y = n;y < n + K;y++) seg.update(arr[y][m - 1], -1);
					for (int y = n;y < n + K;y++) seg.update(arr[y][m + K - 1], 1);
				}
				tmp.push_back(seg.query((K*K + 1) / 2));
			}
			ans.push_back(tmp);
		}
		else {			// <-
			vector<int> tmp;
			for (int m = M - K;m < M;m++) seg.update(arr[n - 1][m], -1), seg.update(arr[n + K - 1][m], 1);
			for (int m = M - K;m >= 0;m--) {
				if (m == M - K) {}
				else {
					for (int y = n;y < n + K;y++) seg.update(arr[y][m + K], -1);
					for (int y = n;y < n + K;y++) seg.update(arr[y][m], 1);
				}
				tmp.push_back(seg.query((K*K + 1) / 2));
			}
			reverse(tmp.begin(), tmp.end());
			ans.push_back(tmp);
		}
	}

	for (auto &n : ans) {
		for (auto &m : n) printf("%d ", m);
		printf("\n");
	}
	return 0;
}
#endif
}
