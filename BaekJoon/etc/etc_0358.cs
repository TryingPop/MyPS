using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 27
이름 : 배성훈
내용 : 영화감독 숌 2, 영화감독 숌 3
    문제번호 : 27438번, 27441번

    dp 문제다
    
    10_05에 있는 영화감독 숌 1의 코드가 잘못되었다 정확히는 점화식을 잘못 세웠다
    해당 코드는 1만 이하에서만 유효할뿐, 빈 공간이 8자리를 출력하는 경우 확실히 틀린다
    해당 코드는 arr[i] = Math.Pow(10, i), arr[i] += 9 * arr[i - j] << j 를 1부터 i까지로 잡혀져 있다
    실제로는 j를 1부터 최대 3까지만 잡아야한다

    자리수 찾는건 쉽게 되었으나, 이를 영화감독 슘 숫자로 바꾸는데 시간이 오래 걸렸다
    다른 사람 풀이를 보면 dp로 했으나 두 포인터를 활용한 분할 정복 아이디어로 구현했다
    다른 사람 풀이가 더 편해보인다

    시간은 68ms로 이상없이 통과했다
    영화감독 숌은 int로 바꿔서 하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0358
    {

        static void Main358(string[] args)
        {

            // 문자열의 자리수가 된다

            // 전처리 작업
            long[] arr = new long[18];

            for (int i = 0; i < arr.Length; i++)
            {

                arr[i] = (long)Math.Pow(10, i);
                for (int j = 1; j <= Math.Min(3, i); j++)
                {

                    arr[i] += 9 * arr[i - j];
                }
            }

            // 입력
            long find = long.Parse(Console.ReadLine());
            int len = 0;
            for (int i = 0; i < arr.Length; i++)
            {

                // 정답 문자의 길이 찾기
                if (find > arr[i]) len++;
                else break;
            }

            // 정답 출력용
            int[] ret = new int[len + 3];

            // 현재 값넣을 ret의 idx 위치
            int curLen = 0;

            // 기준값
            long std = find - 1;

            // 왼쪽 하한
            long l = 0;
            // 오른쪽 상한
            long r = arr[len] - 1;

            // 6의 연결된 개수
            int conn = 0;

            while (curLen < len + 3)
            {

                if (curLen == len)
                {

                    // 666 앞에 숫자를 다 찾은 경우
                    for (int i = 0; i < 3 - conn; i++)
                    {

                        // 666이 되게 채운다
                        ret[curLen++] = 6;
                    }
                    break;
                }

                // 왼쪽 차이
                long lDiff = std - l;
                // 오른쪽 차이
                long rDiff = r - std;

                // 왼쪽부터 확인
                // 앞자리가 0 ~ 5을 찾는다
                long lChk = lDiff / arr[len - curLen - 1];

                if (lChk < 6)
                {

                    // 앞이 5이하인 경우 바로 넣고 왼쪽값 오른쪽으로 당긴다
                    // 오른쪽, 왼쪽 끝도 당긴다
                    l += lChk * arr[len - curLen - 1];
                    r = l + arr[len - curLen - 1] - 1;
                    ret[curLen++] = (int)lChk;
                    // 6이 아니므로 6의 연속이 0개로 초기화
                    conn = 0;
                    continue;
                }

                // 666 앞에 값이 6이상이므로 6까지 옮긴다
                l += 6 * arr[len - curLen - 1];
                // 앞의 문자가 7, 8, 9인지 확인
                long rChk = rDiff / arr[len - curLen - 1];
                if (rChk < 3)
                {

                    // 앞의 문자가 7, 8, 9인 경우 오른쪽과, 왼쪽 범위를 줄인다
                    r -= rChk * arr[len - curLen - 1];
                    l = r - arr[len - curLen - 1] + 1;
                    ret[curLen++] = 9 - (int)rChk;
                    conn = 0;
                    continue;
                }

                // 앞이 6인 경우
                // 6을 입력하고 다음 범위로 간다
                r -= 3 * arr[len - curLen - 1];
                ret[curLen++] = 6;
                // 6의 길이 추가
                conn++;

                // 앞에 666이 나오면 다 찾은 경우다
                if (conn == 3) break;
            }

            // 666 뒷부분을 채우기
            while (curLen < len + 3)
            {

                // 666뒷부분은 남은 값이 된다
                long diff = std - l;
                for (int i = len + 2; i >= curLen; i--)
                {

                    ret[i] = (int)(diff % 10);
                    diff /= 10;
                    if (diff == 0) break;
                }

                curLen = len + 3;
            }

            // 출력
            StreamWriter sw = new(Console.OpenStandardOutput());
            for (int i =0; i< ret.Length; i++)
            {

                sw.Write(ret[i]);
            }

            sw.Close();
        }
    }

#if other

// #include <bits/stdc++.h>
// #define fastio cin.tie(0)->sync_with_stdio(0)
using namespace std;

using i64 = long long;

i64 T[20]{ 1 }, DB[20]{ 0,0,0,1,19,280,3700,45991,549739,6394870,72915400,818740081,9082453159,99766977760,1087013539000,11762766729271,126545925214279,1354561349342950,14435830371578500,153252438815221561 };

string Sol(int i, int j, i64 n) {
    if (i <= 3) {
        for (i64 val = 0; val < T[i]; val++) {
            string s = to_string(val);
            string cur = string(j, 54) + string(i - s.size(), 48) + s;
            if (cur.find("666") != string::npos) n--;
            if (!n) return cur.substr(j);
        }
    }
    if (j == 3) {
        string res = to_string(n - 1);
        return string(i - res.size(), 48) + res;
    }
    for (int c = 0; c < 10; c++) {
        i64 cnt = [&]() -> i64 {
            if (c != 6) return DB[i - 1];
            if (j == 0) return T[i - 3] + 9 * DB[i - 3] + 9 * DB[i - 2];
            if (j == 1) return T[i - 2] + 9 * DB[i - 2];
            if (j == 2) return T[i - 1];
        }();
        if (n <= cnt) return string(1, c | 48) + Sol(i - 1, c == 6 ? j + 1 : 0, n);
        n -= cnt;
    }
}

int main() {
    fastio;
    i64 n; cin >> n;
    for (int i = 1; i < 20; i++) T[i] = T[i - 1] * 10;
    auto res = Sol(20, 0, n);
    while (res[0] == 48) res.erase(res.begin());
    cout << res << '\n';
}
#elif other2

// #include <bits/stdc++.h>
using namespace std;
using lint = long long;
using pi = array<lint, 2>;
// #define sz(v) ((int)(v).size())
// #define all(v) (v).begin(), (v).end()

string s;
lint dp[35][4][4][4];

lint f(int n, int mtch, int consec, int mconsec) {
	mconsec = max(mconsec, consec);
	if (n == sz(s)) {
		return mconsec == 3;
	}
	if (~dp[n][mtch][consec][mconsec])
		return dp[n][mtch][consec][mconsec];
	lint ans = 0;
	for (int i = 0; i < 10; i++) {
		if (mtch && i > s[n] - '0')
			continue;
		if (i != 6) {
			ans += f(n + 1, mtch & (i == s[n] - '0'), 0, mconsec);
		} else {
			ans += f(n + 1, mtch & (i == s[n] - '0'), min(consec + 1, 3), mconsec);
		}
	}
	return dp[n][mtch][consec][mconsec] = ans;
}

int main() {
	ios_base::sync_with_stdio(0);
	cin.tie(0);
	cout.tie(0);
	lint n;
	cin >> n;
	__int128 l = 1, r = 1;
	for (int i = 0; i < 20; i++)
		r *= 10;
	while (l != r) {
		__int128 m = (l + r) / 2;
		s.clear();
		for (__int128 i = m; i; i /= 10) {
			s.push_back(i % 10 + '0');
		}
		reverse(all(s));
		memset(dp, -1, sizeof(dp));
		if (f(0, 1, 0, 0) < n)
			l = m + 1;
		else
			r = m;
	}
	__int128 m = (l + r) / 2;
	s.clear();
	for (__int128 i = m; i; i /= 10) {
		s.push_back(i % 10 + '0');
	}
	reverse(all(s));

	cout << s << "\n";
}
#elif other3

n=int(input())
anss = [0 for i in range(40)]
counts = [[[-1 for _ in range(2)] for _ in range(3)] for _ in range(40)]

def getCount(idx, conti, already):
	if conti >= 3:
		already = 1
	if already:
		return 10 ** idx
	if idx == 0:
		return 0
	if counts[idx][conti][already] >= 0:
		return counts[idx][conti][already]
	res = 0
	for digit in range(10):
		res += getCount(idx - 1, (conti + 1) if digit == 6 else 0, already)
	counts[idx][conti][already] = res
	return res

ans = 0
g_conti = 0
g_already = 0
for idx in range(39, 0, -1):
	for digit in range(10):
		c = getCount(idx - 1, (g_conti + 1) if digit == 6 else 0, g_already)
		if n <= c:
			g_conti = (g_conti + 1) if digit == 6 else 0
			g_already = 1 if g_conti >= 3 else g_already
			anss[idx] = digit
			break
		else:
			n -= c

for idx in range(39, 0, -1):
	ans *= 10
	ans += anss[idx]
print(ans)
#endif
}
