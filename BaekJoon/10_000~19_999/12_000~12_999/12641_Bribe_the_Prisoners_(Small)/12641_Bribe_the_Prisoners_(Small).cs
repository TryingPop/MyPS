using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 14
이름 : 배성훈
내용 : Bribe the Prisoners (Small)
	문제번호 : 12641번

	구현, 브루트포스, 백트래킹 문제다.
	브루트포스로 답을 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1189
    {

        static void Main1189(string[] args)
        {

            int INF = 65_536;
            string HEAD = "Case #";
            string BODY = ": ";
            string TAIL = "\n";

            StreamReader sr;
            StreamWriter sw;

            int[] arr;
            bool[] visit;
            int p, q;

            Solve();
            void Solve()
            {

                Init();

                int t = ReadInt();

                for (int i = 1; i <= t; i++)
                {

                    Input();

                    int ret = GetRet();

                    sw.Write($"{HEAD}{i}{BODY}{ret}{TAIL}");
                }

                sr.Close();
                sw.Close();
            }

            int GetRet(int _depth = 0, int _ret = 0)
            {

                if (_depth == q) return _ret;
                int ret = INF;

                for (int i = 0; i < q; i++)
                {

                    int cur = arr[i];
                    if (visit[cur]) continue;
                    visit[cur] = true;
                    int cost = 0;

                    for (int j = cur - 1; j >= 1; j--)
                    {

                        if (visit[j]) break;
                        cost++;
                    }

                    for (int j = cur + 1; j <= p; j++)
                    {

                        if (visit[j]) break;
                        cost++;
                    }

                    ret = Math.Min(ret, GetRet(_depth + 1, _ret + cost));
                    visit[cur] = false;
                }

                return ret;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                arr = new int[101];
                visit = new bool[101];
            }

            void Input()
            {

                p = ReadInt();
                q = ReadInt();

                for (int i = 0; i < q; i++)
                {

                    arr[i] = ReadInt();
                }
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == ' ' || c == '\n') return true;
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }

#if other
// #include <cstdio>

// #define MAX_P		100

bool C[MAX_P + 2], V[MAX_P];
int P, Q, man[MAX_P];

int min(const int& a, const int& b) { return (a < b) ? a: b; }

int cl(const int& idx) {
	int r = 0, i;
	for (i = idx + 1; !C[i]; ++i) ++r;
	for (i = idx - 1; !C[i]; --i) ++r;
	return r;
}

int bt(const int& L, const int& S) {
	if (L == Q) return S;

	int r = 2501;
	for (int i = 0; i < Q; ++i) {
		if (!V[i]) {
			V[i] = true;
			C[man[i]] = true;
			r = min(r, bt(L + 1, S + cl(man[i])));
			C[man[i]] = false;
			V[i] = false;
		}
	}

	return r;
}

int main() {
	int T, t, i;

	C[0] = true;

	scanf("%d", &T);
	for (t = 1; t <= T; ++t) {
		scanf("%d %d", &P, &Q);
		for (i = 0; i < Q; ++i) scanf("%d", man + i);
		C[P + 1] = true;
		printf("Case #%d: %d\n", t, bt(0, 0));
		C[P + 1] = false;
	}

	return 0;
}
#elif other2
// #include <cstdio>
// #include <cstring>

int bribe(int dp[][110], int a[110], int i, int j){
	if(i + 1 >= j){
		return 0;
	}
	if(dp[i][j] != -1){
		return dp[i][j];
	}
	dp[i][j] = 1000000000;
	for(int k = i + 1; k <= j - 1; ++k){
		int cmp = bribe(dp, a, i, k) + bribe(dp, a, k, j) + a[j] - a[i] - 2;
		if(cmp < dp[i][j]){
			dp[i][j] = cmp;
		}
	}
	return dp[i][j];
}

int main(){
	int t;
	scanf("%d", &t);
	for(int a0 = 1; a0 <= t; ++a0){
		int p, q;
		scanf("%d %d", &p, &q);
		int a[110] = {};
		for(int i = 1; i <= q; ++i){
			scanf("%d", &a[i]);
		}
		a[q + 1] = p + 1;
		int dp[110][110];
		memset(dp, -1, sizeof(dp));
		printf("Case #%d: %d\n", a0, bribe(dp, a, 0, q + 1));
	}
	return 0;
}
#elif other3
// #include <iostream>
// #include <algorithm>
// #include <cmath>
// #include <array>
// #include <stack>
// #include <queue>
// #include <deque>
// #include <map>
// #include <set>
// #include <tuple>
// #include <string>
// #include <cstring>
using namespace std;
typedef long long ll;
typedef pair<ll, ll> pi;
void init()
{
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	cout.tie(nullptr);
}

bool prison[101];

// inclusive
int solve(int s, int e)
{
	if (s > e) return 0;
	int ans = 10000;
	bool isClean = true;
	for (int i = s; i <= e; i++)
	{
		// 석방 가능하다면
		if (prison[i])
		{
			isClean = false;
			ans = min(ans, solve(s, i - 1) + solve(i + 1, e) + e - s);
		}
	}
	if (isClean)
	{
		return 0;
	}
	return ans;
}

int main(void) {
	init();
	int n;
	cin >> n;
	for (int t = 1; t <= n; t++)
	{
		int p, q;
		cin >> p >> q;
		fill_n(prison, p + 1, false);
		for (int i = 0; i < q; i++)
		{
			int tmp;
			cin >> tmp;
			prison[tmp] = true;
		}
		cout << "Case #" << t << ": " << solve(1, p) << '\n';
	}
	return 0;
}
#endif
}
