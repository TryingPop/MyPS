using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 2
이름 : 배성훈
내용 : 용액 2
    문제번호 : 32178번

    그리디, 정렬, 누적합 문제다
    아이디어는 다음과 같다
    연속된 누적합 중 0에 가까운 것을 구해야한다
    누적합들을 정렬한 뒤 인접한거끼리 빼주면
    연속된 누적합이 보장되고
    이 중 0과 가장 가까운 것을 제출하니 통과했다

    다만, 음수를 제출해야 하는데 절댓값으로 제출해 2번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_1019
    {

        static void Main1019(string[] args)
        {

            StreamReader sr;

            int n;
            (long val, int idx)[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Array.Sort(arr, (x, y) => x.val.CompareTo(y.val));

                long diff = 10_000_000_000_000_000, ret1 = 0;
                int ret2 = 0, ret3 = 0;

                for (int i = 1; i <= n; i++)
                {

                    long chk = arr[i].val - arr[i - 1].val;
                    if (diff <= chk) continue;

                    diff = chk;
                    if (arr[i].idx < arr[i - 1].idx) ret1 = -chk;
                    else ret1 = chk;
                    int min = arr[i].idx < arr[i - 1].idx ? arr[i].idx : arr[i - 1].idx;
                    int max = arr[i].idx < arr[i - 1].idx ? arr[i - 1].idx : arr[i].idx;

                    ret2 = min + 1;
                    ret3 = max;
                }

                Console.Write($"{ret1}\n{ret2} {ret3}");
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                arr = new (long val, int idx)[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    arr[i] = (ReadInt() + arr[i - 1].val, i);
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool positive = c != '-';
                int ret = positive ? c - '0' : 0;

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return positive ? ret : -ret;
            }
        }
    }

#if other
// #include <bits/stdc++.h>
// #include <unordered_set>
// #define MOD 1000000007
using namespace std;
typedef long long ll;
typedef pair<int, int> pii;
typedef pair<ll, ll> pll;
typedef tuple<int, int, int>ti;
const char* yes = "Yes";
const char* no = "No";

int main(void)
{
	cin.tie(0)->ios::sync_with_stdio(0);
	int n = 0;
	cin >> n;
	vector<ll>v(n + 1, 0);
	vector<pll>prefix(n + 1, make_pair(0, 0));
	for (int i = 1; i <= n; ++i)
	{
		cin >> v[i];
		prefix[i] = make_pair(prefix[i - 1].first + v[i], i);
	}
	sort(prefix.begin(), prefix.end());
	ll temp = LLONG_MAX;
	ll left = 0, right = 0;
	for (int i = 1; i <= n; ++i)
	{
		ll dif = abs(prefix[i].first - prefix[i - 1].first);
		if (dif < temp)
		{
			temp = dif;
			left = min(prefix[i].second, prefix[i - 1].second);
			right = max(prefix[i].second, prefix[i - 1].second) - 1;
		}
	}
	ll ans = 0;
	left++, right++;
	for (int i = left; i <= right; ++i)
	{
		ans += v[i];
	}
	cout << ans << '\n';
	cout << left<< ' ' << right;
	return 0;
}
#elif other2
// #include <bits/stdc++.h>

using namespace std;
typedef long long ll;
typedef long double ld;


class Code1342D1 {
public:
    int N, a, b;
    ll S, ans = LLONG_MAX;
    pair<ll, int> sum[1'000'003] = {};


    void solve() {
        ios::sync_with_stdio(false);
        cin.tie(nullptr);


        cin >> N;
        sum[0] = {0, 0};
        for (int i = 1; i <= N; i++) {
            cin >> S;
            sum[i].first = sum[i - 1].first + S;
            sum[i].second = i;
        }

        sort(sum, sum + N + 1);

        for (int i = 0; i < N; i++) {
            if (abs(ans) > abs(sum[i].first - sum[i + 1].first)) {
                if (sum[i].second > sum[i + 1].second) {
                    ans = sum[i].first - sum[i + 1].first;
                } else {
                    ans = sum[i + 1].first - sum[i].first;
                }
                a = min(sum[i].second, sum[i + 1].second);
                b = max(sum[i].second, sum[i + 1].second);
            }
        }


        cout << ans << "\n" << a + 1 << " " << b;
    }
};


int main() {
    Code1342D1 code;
    code.solve();
    return 0;
}
#endif
}
