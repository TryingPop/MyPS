using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 20
이름 : 배성훈
내용 : 123456789 찾기
    문제번호 : 12105번

    dp, 문자열 문제다.
    매칭하는 인덱스를 kmp를 통해 찾고
    경우의 수는 dp를 이용해 세었다.

    dp로 세는 방법은 해당 수를 선택하지 않는 경우를 누적하고,
    해당 수를 선택하는 경우를 추가로 더해주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1208
    {

        static void Main1208(string[] args)
        {

            int MOD = 1_000_000_007;
            string s, p;
            long[][,,,] dp;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                List<int> matches = KMP(s, p);

                dp = new long[2][,,,];
                int[] cnt = new int[4];

                for (int i = 0; i < dp.Length; i++)
                {

                    dp[i] = new long[4, 3, 2, 2];
                }

                dp[0][0, 0, 0, 0] = 1;
                for (int i = 0; i < matches.Count; i++)
                {

                    int cur = matches[i] + 1;
                    SetCnt(cur);

                    for (int two = 0; two <= 3; two++)
                    {

                        for (int three = 0; three <= 2; three++)
                        {

                            for (int five = 0; five <= 1; five++)
                            {

                                for (int seven = 0; seven <= 1; seven++)
                                {

                                    dp[1][two, three, five, seven] = (dp[1][two, three, five, seven] + dp[0][two, three, five, seven]) % MOD;

                                    int nTwo = two + cnt[0];
                                    if (3 < nTwo) nTwo = 3;
                                    int nThree = three + cnt[1];
                                    if (2 < nThree) nThree = 2;
                                    int nFive = five + cnt[2];
                                    if (1 < nFive) nFive = 1;
                                    int nSeven = seven + cnt[3];
                                    if (1 < nSeven) nSeven = 1;

                                    dp[1][nTwo, nThree, nFive, nSeven] = (dp[1][nTwo, nThree, nFive, nSeven] + dp[0][two, three, five, seven]) % MOD;
                                }
                            }
                        }
                    }

                    for (int two = 0; two <= 3; two++)
                    {

                        for (int three = 0; three <= 2; three++)
                        {

                            for (int five = 0; five <= 1; five++)
                            {

                                for (int seven = 0; seven <= 1; seven++)
                                {

                                    dp[0][two, three, five, seven] = dp[1][two, three, five, seven];
                                    dp[1][two, three, five, seven] = 0;
                                }
                            }
                        }
                    }
                }

                Console.Write(dp[0][3, 2, 1, 1]);
                void SetCnt(int _num)
                {

                    Cnt(2, 0, 3);
                    Cnt(3, 1, 2);
                    Cnt(5, 2, 1);
                    Cnt(7, 3, 1);

                    void Cnt(int _div, int _idx, int _inf)
                    {

                        cnt[_idx] = 0;
                        while (_num % _div == 0 
                            && cnt[_idx] < _inf)
                        {

                            cnt[_idx]++;
                            _num /= _div;
                        }
                    }
                }
            }

            int[] SetPattern(string _pattern)
            {

                int backPos = 0;
                int[] jump = new int[_pattern.Length];

                for (int curPos = 1; curPos < _pattern.Length; curPos++)
                {

                    while (backPos > 0 && _pattern[backPos] != _pattern[curPos])
                    {

                        backPos = jump[backPos - 1];
                    }

                    if (_pattern[backPos] == _pattern[curPos]) jump[curPos] = ++backPos;
                }

                return jump;
            }

            List<int> KMP(string _text, string _pattern)
            {

                int match = 0;
                List<int> ret = new(_text.Length);
                int[] jump = SetPattern(_pattern);

                for (int i = 0; i < _text.Length; i++)
                {

                    while (match > 0 && _text[i] != _pattern[match])
                    {

                        match = jump[match - 1];
                    }

                    if (_text[i] == _pattern[match])
                    {

                        match++;

                        if (match == _pattern.Length)
                        {

                            // text에 pattern 문자가 있는 경우 
                            // 여기 부분을 조절하면 된다
                            ret.Add(i - match + 1);
                            match = jump[match - 1];
                        }
                    }
                }

                return ret;
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                p = sr.ReadLine();
                s = sr.ReadLine();

                sr.Close();
            }
        }
    }

#if other
// #include <bits/stdc++.h>
// #define fastio cin.tie(0)->sync_with_stdio(0)
using namespace std;

constexpr int mod = 1e9 + 7;

int main() {
	fastio;
	string a, b; cin >> a >> b;

	auto get_fail = [](const string& s) {
		vector f(s.size(), 0);
		for (int i = 1, j = 0; i < s.size(); i++) {
			while (j && s[i] != s[j]) j = f[j - 1];
			if (s[i] == s[j]) f[i] = ++j;
		}
		return f;
	};

	// return i s.t. s.substr(i, t.size()) == t
	auto kmp = [&](const string& s, const string& t) {
		vector ret(0, 0), f = get_fail(t);
		for (int i = 0, j = 0; i < s.size(); i++) {
			while (j && s[i] != t[j]) j = f[j - 1];
			if (s[i] == t[j] && ++j == t.size()) {
				ret.push_back(i - t.size() + 1);
				j = f[j - 1];
			}
		}
		return ret;
	};

	// return max k s.t. p^k | n
	auto expo = [](int n, int p) {
		int ret = 0;
		while (n % p == 0) ret++, n /= p;
		return ret;
	};

	vector dp(48, 0); dp[0] = 1;
	for (int x : kmp(b, a)) {
		int c2 = expo(++x, 2);
		int c3 = expo(x, 3);
		int c5 = expo(x, 5);
		int c7 = expo(x, 7);
		for (int i = 47; i >= 0; i--) {
			int i2 = min(i / 12 + c2, 3);
			int i3 = min(i % 12 / 4 + c3, 2);
			int i5 = min(i % 4 / 2 + c5, 1);
			int i7 = min(i % 2 + c7, 1);
			int j = i2 * 12 + i3 * 4 + i5 * 2 + i7;
			dp[j] += dp[i];
			if (dp[j] >= mod) dp[j] -= mod;
		}
	}
	cout << dp.back() << '\n';
}
#elif other2
// #include <iostream>
// #include <vector>
// #include <string>
using namespace std;

int main(){
    ios::sync_with_stdio(false), cin.tie(NULL), cout.tie(NULL);
    constexpr int mod = 1000000007;
    constexpr int e2 = 3, e3 = 2, e5 = 1, e7 = 1;
    string S, P;
    cin >> S >> P;
    int n = S.size(), m = P.size();
    vector<int> fail(n);
    for(int i = 1, j = 0; i < n; i++){
        while(j && S[i] != S[j])
            j = fail[j - 1];
        if(S[i] == S[j])
            fail[i] = ++j;
    }
    vector<vector<vector<vector<int>>>> dp(e2 + 1, vector<vector<vector<int>>>(e3 + 1, vector<vector<int>>(e5 + 1, vector<int>(e7 + 1))));
    dp[0][0][0][0] = 1;
    for(int i = 0, j = 0; i < m; i++){
        while(j && P[i] != S[j])
            j = fail[j - 1];
        if(P[i] == S[j]){
            j++;
            if(j == n){
                int idx = i - n + 2;
                int d2 = 0, d3 = 0, d5 = 0, d7 = 0;
                while(idx % 2 == 0){
                    d2++;
                    idx /= 2;
                }
                while(idx % 3 == 0){
                    d3++;
                    idx /= 3;
                }
                while(idx % 5 == 0){
                    d5++;
                    idx /= 5;
                }
                while(idx % 7 == 0){
                    d7++;
                    idx /= 7;
                }
                for(int p = e2; p >= 0; p--){
                    for(int q = e3; q >= 0; q--){
                        for(int r = e5; r >= 0; r--){
                            for(int s = e7; s >=0; s--){
                                int& D = dp[min(p + d2, e2)][min(q + d3, e3)][min(r + d5, e5)][min(s + d7, e7)];
                                D = (D + dp[p][q][r][s]) % mod;
                            }
                        }
                    }
                }
                j = fail[j - 1];
            }
        }
    }
    cout << dp[e2][e3][e5][e7];
    return 0;
}
#endif
}
