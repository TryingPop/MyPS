using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 19
이름 : 배성훈
내용 : Secret Code
    문제번호 : 9994번

    문자열, dp, 브루트포스, 분할 정복, 재귀 문제다
    분할 정복을 이용해 풀었다

    아이디어는 다음과 같다
    생성되는 과정을 보면
    ABC는 전체 문자열이라 보고, 
    AB는 맨 뒤의 원소를 1개 뺀 부분 문자열,
    BC는 맨 앞의 원소를 1개 뺀 부분 문자열을 간단히 표현한거라 할 때

    ABC + AB, ABC + BC, AB + ABC, BC + ABC 형태가 있다
    만들어진 길이를 보면 기존 ABC의 길이를 len이라 하면 2 * len - 1이므로
    홀수다 즉, 짝수는 만들 수 없다!

    그래서 짝수인 경우나 더 쪼갤 수 없는 길이가 1인 경우 탈출을 하는 코드를 작성한다
    이후 4가지 경우 중 만족하는 쪽으로 분할하고 카운팅해간다

    문자열의 최대 길이가 100이고 한 번당 길이가 절반으로 줄어들기에
    깊이는 7번 이상을 못 내려간다 깊이당 4번 연산이 있으니 최대 4^7 = 2^14번 연산을 한다
*/

namespace BaekJoon.etc
{
    internal class etc_0978
    {

        static void Main978(string[] args)
        {

            StreamReader sr;
            string str;

            Solve();
            void Solve()
            {

                Input();

                int ret = DFS(0, str.Length);

                // 0번 연산도 1회로 세었기에 빼준다
                Console.Write(ret - 1);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                str = sr.ReadLine().Trim();

                sr.Close();
            }

            bool ChkSubStr(int _s1, int _s2, int _len)
            {

                /*
                 
                부분 문자열이 같은지 확인 
                */
                for (int i = 0; i < _len; i++)
                {

                    if (str[_s1 + i] == str[_s2 + i]) continue;
                    return false;
                }

                return true;
            }

            int DFS(int _s, int _len)
            {

                int ret = 1;

                // 짝수인 경우는 암호가 생성되는 형식을 보면
                // len - 1  + len 이므로 짝수는 생성 불가능하다
                // 1인 경우도 더 쪼갤 수 없어 종료
                if (_len % 2 == 0 || _len == 1) return ret;

                int len = _len >> 1;

                // AB + ABC 경우인지 확인
                if (ChkSubStr(_s, _s + len, len)) ret += DFS(_s + len, len + 1);

                // ABC + AB, BC + ABC 경우인지 확인
                if (ChkSubStr(_s, _s + len + 1, len))
                {

                    // ABC + AB 경우 확인
                    ret += DFS(_s, len + 1);
                    // BC + ABC 경우 확인
                    ret += DFS(_s + len, len + 1);
                }

                // ABC + BC 경우 확인
                if (ChkSubStr(_s + 1, _s + len + 1, len)) ret += DFS(_s, len + 1);

                return ret;
            }
        }
    }

#if other
// #include <cstdio>
// #include <memory.h>
// #include <cstring>
// #include <vector>
// #include <deque>
// #include <queue>
// #include <algorithm>
// #include <cmath>
// #include <functional>
// #include <set>
// #include <map>
// #include <unordered_map>
// #include <unordered_set>
// #include <bitset>
// #define sz(x) (int)(x).size()
// #define all(x) (x).begin(), (x).end()
using namespace std;

typedef unsigned long long ull;
typedef long long ll;
typedef pair<int, int> pii;
typedef pair<ll, ll> pll;
typedef pair<ll, int> pli;
const ll MOD = 1e9 + 7;

priority_queue<int, vector<int>, greater<int> > pq;
vector<int> v; queue<int> q; deque<int> dq;

char s[100];
int dp[100][100];
int len;

int go(int l, int r) {
	if (l == 0 && r == len - 1) return 1;
	if (dp[l][r] != -1) return dp[l][r];

	int sum = 0, ll = r - l;
	if (l - ll >= 0) {
		if (strncmp(s + (l - ll), s + l, ll) == 0) sum += go(l - ll, r);
		if (strncmp(s + (l - ll), s + l + 1, ll) == 0) sum += go(l - ll, r);
	}
	if (r + ll < len) {
		if (strncmp(s + l, s + r + 1, ll) == 0) sum += go(l, r + ll);
		if (strncmp(s + l + 1, s + r + 1, ll) == 0) sum += go(l, r + ll);
	}

	return dp[l][r] = sum;
}

int main() {
	int sum = 0;
	scanf("%s", s);
	len = strlen(s);

	memset(dp, -1, sizeof(dp));
	for (int i = 1; i < len - 2; i++) {
		for (int j = 0; j + i < len; j++)
			sum += go(j, j + i);
	}
	printf("%d", sum);
}
#endif
}
