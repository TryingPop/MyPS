using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 11
이름 : 배성훈
내용 : 단어 게임
    문제번호 : 1856번

    dp 문제다.
    문자열의 길이 l이 최대 600이고, 들어오는 문자의 갯수 w가 최대 300개 이므로
    O(l^2 * w)의 방법이 유효하다.

    dp[i] = val를 i번째 문자열을 택했을 때, 가장 작은 제외 갯수로 한다.
    그러면 점화식은 모든 w에 대해 문자를 하나씩 제거하며 w가 존재하면
    w의 길이를 k, 제거된 부분을 l이라 하면, dp[i] = min(dp[i - k - l] + l)임을 알 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1267
    {

        static void Main1267(string[] args)
        {

            string str;
            string[] word;

            int w, l;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int[] dp = new int[l + 1];

                for (int i = 0; i < l; i++)
                {

                    dp[i + 1] = dp[i] + 1;
                    for (int j = 0; j < w; j++)
                    {

                        int idx = word[j].Length - 1;
                        int pop = 0;
                        for (int k = i; k >= 0; k--)
                        {

                            if (word[j][idx] != str[k]) 
                            {

                                pop++;
                                continue; 
                            }
                            idx--;

                            if (idx >= 0) continue;
                            dp[i + 1] = Math.Min(dp[i + 1], dp[k] + pop);
                            break;
                        }
                    }
                }

                Console.Write(dp[l]);
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string[] input = sr.ReadLine().Split();
                w = int.Parse(input[0]);
                l = int.Parse(input[1]);

                str = sr.ReadLine();
                word = new string[w];
                for (int i = 0; i < w; i++)
                {

                    word[i] = sr.ReadLine();
                }

                sr.Close();
            }
        }
    }

#if other
// #include<cstdio>
// #include<cstring>
// #include<vector>
// #include<queue>
// #include<algorithm>
using namespace std;
const int MAX_N = 300;
vector<pair<int, int>> c[MAX_N];
int l, dp[MAX_N];
int rec(int p) {
	if (p == l) return 0;
	int& ret = dp[p];
	if (ret != -1) return ret;
	ret = 1e9;
	for (auto& v : c[p])
		ret = min(ret, rec(v.first) + v.second);
	return ret = min(ret, rec(p + 1) + 1);
}
int main() {
	char d[26], s[MAX_N + 1];
	int w, i, j, k, nxt[MAX_N][26], idx, last;
	queue<int> q[26][26];
	memset(nxt, -1, sizeof nxt);
	memset(dp, -1, sizeof dp);
	scanf("%d%d\n%s", &w, &l, s);
	for (i = 0; s[i]; i++) {
		k = s[i] - 'a';
		for (j = 0; j < 26; j++) {
			while (!q[j][k].empty()) {
				nxt[q[j][k].front()][k] = i;
				q[j][k].pop();
			}
			q[k][j].push(i);
		}
	}

	for (i = 0; i < w; i++) {
		scanf("%s", d);
		for (j = 0; j < l; j++) if (d[0] == s[j]) {
			last = j;
			for (k = 1; d[k]; k++) {
				idx = nxt[last][d[k] - 'a'];
				if (idx == -1) break;
				last = idx;
			}
			if (!d[k])
				c[j].push_back({ last + 1, last + 1 - j - k });
		}
	}
	printf("%d", rec(0));
	return 0;
}
#endif
}
