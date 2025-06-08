using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 13
이름 : 배성훈
내용 : Matrice
    문제번호 : 16703번

    dp 문제다.
    문제를 잘못 해석해서 한참을 고민했다.

    #girls
    ##areb
    #.#est
    #..#!!
    #####!

    예제를 보면
    #
    ##
    형태가 3개,(↘ 형태라 한다.)

    ##
    #
    형태가 1개,(↗)

     #
    ##
    형태가 1개, (↙)

    .
    ..
    형태가 1개, (↘)

    !!
     !
    형태가 1개, (↖)
    로 총 7개이다.

    내부가 모두 같아야 한다!

    #
    ##
    #.#
    #..#
    #####
    는 내부에 다른게 있으므로 카운팅하지 않는다!

    그래서 가능한 최대 크기로 찾으면 된다.
    그러면 최대 크기 -1이 개수가 된다.

    삼각형이 추가되는 경우를 보면 각 현재 점과 각 방향에 맞는 두 지점이 같은지 확인하고
    같으면 늘려가는 식으로 찾으면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1270
    {

        static void Main1270(string[] args)
        {

            int row, col;
            int[][] dp;
            int[][] board;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                // ↙, ↗, ↖, ↘ 형태의 삼각형
                // 여기서 방향은 가로 선분이 있는 곳을 표시한 것이다.
                int[] dirR1 = { 0, -1, -1, 0 }, dirC1 = { -1, 0, -1, -1 };
                int[] dirR2 = { -1, -1, -1, -1 }, dirC2 = { 0, 1, 0, -1 };
                long ret = 0;

                dp = new int[row + 2][];
                for (int r = 0; r <= row + 1; r++)
                {

                    dp[r] = new int[col + 2];
                }

                for (int i = 0; i < 4; i++)
                {

                    Chk(i);
                }

                Console.Write(ret);

                void Chk(int _dir)
                {

                    for (int i = 0; i < dp.Length; i++)
                    {

                        Array.Fill(dp[i], 1);
                    }

                    for (int r = 1; r <= row; r++)
                    {

                        for (int c = 1; c <= col; c++)
                        {

                            if (board[r + dirR1[_dir]][c + dirC1[_dir]] == board[r][c] && board[r][c] == board[r + dirR2[_dir]][c + dirC2[_dir]])
                            {

                                dp[r][c] = Math.Min(dp[r + dirR1[_dir]][c + dirC1[_dir]], dp[r + dirR2[_dir]][c + dirC2[_dir]]) + 1;
                                ret += dp[r][c] - 1;
                            }
                        }
                    }
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string[] size = sr.ReadLine().Split();
                row = int.Parse(size[0]);
                col = int.Parse(size[1]);

                board = new int[row + 2][];
                board[0] = new int[col + 2];
                for (int r = 1; r <= row; r++)
                {

                    board[r] = new int[col + 2];
                    string temp = sr.ReadLine();
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c + 1] = temp[c];
                    }
                }

                board[row + 1] = new int[col + 2];
                sr.Close();
            }
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;

// #define st first
// #define nd second
// #define pb push_back
// #define mp make_pair
// #define klar(v) memset(v, 0, sizeof(v))
// #define endl "\n"

typedef vector<int> vi;
typedef vector<pair<int, int>> vpii;
typedef vector<long long> vll;
typedef pair<int, int> pii;
typedef long long ll;
typedef pair<ll, ll> pll;

const int maxn = 1100;

char grid[maxn][maxn];
int Xa[] = {-1, 0, -1, -1}, Ya[] = {0, -1, -1, 0};
int Xb[] = {0, 1, 0, -1}, Yb[] = {-1, -1, -1, -1};
ll dp[maxn][maxn];

ll gemacht(char w, int n, int m, int k){
	ll ret = 0;
	//if(w != '#')return 0;
	//if(k != 0)return 0;
	klar(dp);
	for(int i = 1; i <= n; i++)
		for(int j = 1; j <= m; j++)
			if(grid[i][j] == w){
				dp[i][j] = min(dp[i+Ya[k]][j+Xa[k]], dp[i+Yb[k]][j+Xb[k]])+1;
				//cout << i << " " << j << " " << dp[i][j] << " " << dp[i+Yb[k]][j+Xb[k]] << " " << i+Yb[k] << " " << j+Xb[k] << endl;
			}
	//cout << endl;
	for(int i = 1; i <= n; i++)
		for(int j = 1; j <= m; j++)
			ret += max(dp[i][j]-1, 0LL);
	return ret;
}

int main()
{
	ios_base::sync_with_stdio(false);
	int n, m;
	cin >> n >> m;
	for(int i = 1; i <= n; i++)
		for(int j = 1; j <= m; j++)
			cin >> grid[i][j]; 
	ll ans = 0;
	for(int i = 33; i <= 126; i++)
		for(int j = 0; j < 4; j++)
			ans += gemacht(char(i), n, m, j);
	cout << ans << endl;
	return 0;
}
#elif other2
// #include <bits/stdc++.h>
using namespace std;
using ll = long long;
using ld = long double;

// #define mp make_pair
// #define x first
// #define y second

ll dp[1010][1010];

void solve() {

    int n, m;
    cin >> n >> m;

    vector<string> A(n);
    for (int i = 0; i < n; i++) {
        cin >> A[i];
    }

    ll ans = 0;
    for (int i = 1; i < n; i++) {
        for (int j = 1; j < m; j++) {
            if (A[i][j] == A[i - 1][j] && A[i][j] == A[i][j - 1]) {
                dp[i][j] = min(dp[i][j - 1], dp[i - 1][j]) + 1;
            }
            ans += dp[i][j];
        }
    }
    memset(dp, 0, sizeof(dp));
    for (int i = 1; i < n; i++) {
        for (int j = m - 2; j >= 0; j--) {
            if (A[i][j] == A[i - 1][j] && A[i][j] == A[i][j + 1]) {
                dp[i][j] = min(dp[i][j + 1], dp[i - 1][j]) + 1;
            }
            ans += dp[i][j];
        }
    }

    memset(dp, 0, sizeof(dp));
    for (int i = n - 2; i >= 0; i--) {
        for (int j = 1; j < m; j++) {
            if (A[i][j] == A[i + 1][j] && A[i][j] == A[i][j - 1]) {
                dp[i][j] = min(dp[i][j - 1], dp[i + 1][j]) + 1;
            }
            ans += dp[i][j];
        }
    }

    memset(dp, 0, sizeof(dp));
    for (int i = n - 2; i >= 0; i--) {
        for (int j = m - 2; j >= 0; j--) {
            if (A[i][j] == A[i + 1][j] && A[i][j] == A[i][j + 1]) {
                dp[i][j] = min(dp[i][j + 1], dp[i + 1][j]) + 1;
            }
            ans += dp[i][j];
        }
    }

    cout << ans;
}

int main() {
    ios::sync_with_stdio(false);
    cin.tie(0);
    cout.tie(NULL);

    solve();
    return 0;
}
#endif
}
