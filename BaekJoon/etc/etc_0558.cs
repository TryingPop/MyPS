using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 17
이름 : 배성훈
내용 : 회의실 배정 3
    문제번호 : 19622번

    dp 문제다
    앞 뒤가 겹치므로, 건너 뛰어야한다
    그래서 계단 오르기처럼 그리디하게 접근해서 풀었다
    다만, 값을 구하고 이후에 뒤로 미루는 연산을 해서
    1, 2를 비교해야하는데 0, 1을 비교해서 1번 틀렸다;

    이후에 1, 2로 수정하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0558
    {

        static void Main558(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
            int n = ReadInt();

            long[] arr = new long[4];

            Solve();
            sr.Close();

            void Solve()
            {


                for (int i = 0; i < n; i++)
                {

                    ReadInt();
                    ReadInt();
                    int cur = ReadInt();
                    arr[0] = Math.Max(arr[2], arr[3]) + cur;
                    for (int j = 3; j >= 1; j--)
                    {

                        arr[j] = arr[j - 1];
                    }
                }

                long ret = arr[1] < arr[2] ? arr[2] : arr[1];
                Console.WriteLine(ret);
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
#if other
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.StringTokenizer;

public class Main {
    static int MAX_LEN = 100000;

    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        int n = Integer.parseInt(br.readLine());
        int[] v = new int[MAX_LEN];
        for (int i = 0; i < n; i++) {
            StringTokenizer st = new StringTokenizer(br.readLine(), " ");
            st.nextToken();
            st.nextToken();
            v[i] = Integer.parseInt(st.nextToken());
        }

        int[][] dp = new int[n][2];
        dp[0][1] = v[0];
        for (int i = 1; i < n; i++) {
            dp[i][0] = max(dp[i - 1][0], dp[i - 1][1]);
            dp[i][1] = dp[i - 1][0] + v[i];
        }

        System.out.println(max(dp[n - 1][0], dp[n - 1][1]));
    }

    static int max(int a, int b) {
        return a > b ? a : b;
    }
}
#elif other2
// #include<bits/stdc++.h>
// #include<unordered_map>
using namespace std;
typedef long long ll;
typedef pair<int, int> PI;
typedef pair<ll, ll> PL;
typedef pair<int, pair<int, int>> PII;
typedef pair<ll, pair<ll, ll>> PLL;
// #define endl '\n'
// #define INF 2e9
// #define LINF 2e15
// #define MOD 1000000007

int n, p1, p2, p3, dp[2][100001];
vector<PII> v;

int func(int state, int cur) {
	if (cur >= n)return 0;
	int& ret = dp[state][cur];
	if (ret != -1)return ret;
	ret = max(ret, func(state, cur + 1));
	ret = max(ret, func(!state, cur + 2) + v[cur].second.second);
	return ret;
}

int main() {
	ios::sync_with_stdio(false);
	cin.tie(NULL); cout.tie(NULL);
	cin >> n;
	memset(dp, -1, sizeof(dp));
	for (int i = 0; i < n; i++) {
		cin >> p1 >> p2 >> p3;
		v.push_back({ p1,{p2,p3} });
	}
	cout << func(0, 0) << endl;
	return 0;
}
#endif
}
