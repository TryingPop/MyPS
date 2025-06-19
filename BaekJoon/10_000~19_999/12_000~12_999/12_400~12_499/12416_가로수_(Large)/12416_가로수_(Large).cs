using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 18
이름 : 배성훈
내용 : 가로수 (Small, Large)
    문제번호 : 12415번, 12416번

    그리디, 브루트포스 문제다.
    아이디어는 다음과 같다.
    M의 크기가 1000 이하이므로 2개씩 택해서 만들 수 있는 경우를 모두 조사해도 충분하다.
    그래서 받침대로 만들 수 있는 막대 길이를 모두 찾는다.
    이후 각 나무에 대해 만들 수 있는 막대기와 재료를 확인하며 가장 작은거부터 차례로 만든다.
    그리디로 작은거부터 만들어도 최소가 보장된다.
    이렇게 찾으면 각 케이스당 시간복잡도는 N + M^2 x log M이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1121
    {

        static void Main1121(string[] args)
        {

            int MAX = 1000;
            StreamReader sr;
            StreamWriter sw;
            int n, m, b;
            (int p, int q)[] stick;

            int chkLen, chkIdx;
            (int len, int a, int b)[] chkStick;
            Comparer<(int len, int a, int b)> comp;
            Solve();
            void Solve()
            {

                Init();

                int test = ReadInt();
                for (int t = 1; t <= test; t++)
                {

                    Input();

                    sw.Write($"Case #{t}: {GetRet()}\n");
                }

                sr.Close();
                sw.Close();
            }

            int GetRet()
            {

                int ret = 0;

                while (n > 0)
                {

                    int cnt;
                    while ((cnt = Chk()) == 0) { chkIdx++; }
                    if (cnt == -1) return -1;

                    cnt = Math.Min(n, cnt);
                    Use(chkStick[chkIdx].a, cnt);
                    Use(chkStick[chkIdx].b, cnt);

                    ret += chkStick[chkIdx].len * cnt;
                    n -= cnt;
                }

                return ret;

                int Chk()
                {

                    if (chkLen == chkIdx) return -1;

                    int i = chkStick[chkIdx].a;
                    int j = chkStick[chkIdx].b;
                    if (i == j) return stick[i].q >> 1;
                    int ret = stick[i].q;
                    if (j != -1) ret = Math.Min(ret, stick[j].q);

                    return ret;
                }

                void Use(int _i, int _cnt)
                {

                    if (_i == -1) return;
                    stick[_i].q -= _cnt;
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                stick = new (int p, int q)[MAX];

                chkStick = new (int len, int a, int b)[MAX + ((MAX) * (MAX + 1)) / 2];

                comp = Comparer<(int len, int a, int b)>.Create((x, y) => x.len.CompareTo(y.len));
            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();
                b = ReadInt();

                chkLen = 0;

                for (int i = 0; i < m; i++)
                {

                    stick[i] = (ReadInt(), ReadInt());
                    if (stick[i].p < b) continue;
                    chkStick[chkLen++] = (stick[i].p, i, -1);
                }

                for (int i = 0; i < m; i++)
                {

                    for (int j = i; j < m; j++)
                    {

                        int chk = stick[i].p + stick[j].p;
                        if (chk < b) continue;
                        chkStick[chkLen++] = (chk, i, j);
                    }
                }

                Array.Sort(chkStick, 0, chkLen, comp);

                chkIdx = 0;
                for (int i = 0; i < chkLen; i++)
                {

                    if (b <= chkStick[i].len) break;
                    chkIdx++;
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

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
// #include<iostream>
// #include<map>
// #include<vector>
// #include<algorithm>
using namespace std;
void sv(int num) {
	int n, m, li;
	cin >> n >> m >> li;
	vector<int>ans(20001);
	map<int, int>mp;
	while (m--) {
		int a, b;
		cin >> a >> b;
		if (a < li) {
			mp[a] = b;
			//맵에 a의 수량 저장
		}
		else
			ans[a] = b;
	}
	long long sum = 0;
	map<int, int>::iterator iter;
	while (!mp.empty()) {
		iter = mp.begin();
		int x = iter->first;//지지하는 힘의 크기
		int xc = iter->second;//x_count
		mp.erase(iter);
		int y = li - x;//필요한 최솟값
		if (x >= y) {
			ans[x + x] += xc / 2;
			xc %= 2;
			if (!xc) continue;
		}
end:
		iter = mp.lower_bound(y);
		if (iter == mp.end()) continue;
		int k = min(iter->second, xc);
		if (iter->second == k) {
			xc -= k;
			ans[x + iter->first] += k;
			mp.erase(iter);
			if (xc) goto end;
		}
		else if (xc == k) {
			ans[x + iter->first] += k;
			mp[iter->first] -= k;
		}
	}
	for (int i = li; i < 20001; i++) {
		if (ans[i]) {
			if (ans[i] < n) {
				n -= ans[i];
				sum += (long long)i * (long long)ans[i];
			}
			else {
				sum += (long long)i * (long long)n;
				n = 0;
				break;
			}
		}
	}
	cout << "Case #" << num << ": ";
	if (n) cout << "-1\n";
	else cout << sum << "\n";
}
int main() {
	ios_base::sync_with_stdio(0);
	cin.tie(0);
	cout.tie(0);
	int t;
	cin >> t;
	for (int i = 1; i <= t; i++)
		sv(i);
}
#elif other2
// #include <bits/stdc++.h>
using namespace std;
    typedef long long ll;
    typedef pair<int, int> pii;

    void solve()
    {
        int n, m, b;
        cin >> n >> m >> b;
        multiset<pii> ms;
        map<int, int> mp;
        for (int i = 0; i < m; ++i)
        {
            int p, q;
            cin >> p >> q;
            if (p < b) ms.insert({ p, q });
        else mp[p] += q;
    }

    while (!ms.empty()) {
        auto it = ms.begin();
    auto[p, q] = * it;
    ms.erase(it);
        if (2* p < b) {
        auto it2 = ms.lower_bound({ b - p, 0 });
            if (it2 == ms.end()) continue;
        auto [p2, q2] = *it2;
        ms.erase(it2);
        mp [p+p2] += min(q, q2);
            if (q != q2) ms.insert({ (q > q2 ? p : p2), abs(q - q2) });
    } else {
            if (q >= 2) {
                mp[2 * p] += q/2;
                q %= 2;
            }
if (q == 1)
{
    auto it2 = ms.lower_bound({ b - p, 0 });
    if (it2 == ms.end()) continue;
    auto[p2, q2] = *it2;
    ms.erase(it2);
    mp[p + p2] += 1;
    if (q2 > 1) ms.insert({ p2, q2 - 1 });
}
        }
    }
    
    pair<ll, ll> ans = { 0, 0 };
for (auto[p, q]: mp)
{
    if (ans.first + q >= n)
    {
        ans.second += (ll)p * (n - ans.first);
        ans.first = n;
        break;
    }
    else
    {
        ans.second += (ll)p * q;
        ans.first += q;
    }
}
cout << (ans.first >= n ? ans.second : -1) << '\n';
}

int main()
{
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);

    int cases;
    cin >> cases;
    for (int it = 1; it <= cases; ++it)
    {
        cout << "Case #" << it << ": ";
        solve();
    }
}
#endif
}
