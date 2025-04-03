using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 1
이름 : 배성훈
내용 : 햄최몇?
    문제번호 : 19645번

    dp, 배낭 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1504
    {

        static void Main1504(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = int.Parse(sr.ReadLine());
            int[] arr = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);

            int sum = arr.Sum();
            bool[][] dp = new bool[sum + 1][];
            for (int i = 0; i < dp.Length; i++)
            {

                dp[i] = new bool[sum + 1];
            }

            // 배낭 dp
            dp[0][0] = true;
            sum = 0;
            for (int i = 0; i < n; i++)
            {

                sum += arr[i];
                for (int j = sum; j >= 0; j--)
                {

                    for (int k = sum; k >= 0; k--) 
                    {

                        // j, k가 가능하다면 추가한다.
                        if (!dp[j][k]) continue;
                        dp[j + arr[i]][k] = true;
                        dp[j][k + arr[i]] = true;
                    }
                }
            }

            int ret = 0;
            for (int i = 0; i <= sum; i++)
            {

                for (int j = 0; j <= sum; j++)
                {

                    if (!dp[i][j]) continue;
                    int chk = Math.Min(sum - (i + j), Math.Min(i, j));
                    ret = Math.Max(ret, chk);
                }
            }

            Console.Write(ret);
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;

int N, sum;
vector<int> cnt(51);

bool numbers_available(int a, int b, vector<int> &cnt) {
    if (b == 0)
        return true;

    if (a > 0) {
        for (int i = min(a, 50); i > 0; i--) {
            if (cnt[i] > 0) {
                cnt[i]--;
                if (numbers_available(a - i, b, cnt)) {
                    return true;
                }
                cnt[i]++;
            }
        }
    }
    else if (b > 0) {
        for (int i = min(b, 50); i > 0; i--) {
            if (cnt[i] > 0) {
                cnt[i]--;
                if (numbers_available(a, b - i, cnt)) {
                    return true;
                }
                cnt[i]++;
            }
        }
    }

    return false;
}

int solve() {
    for (int i = sum / 3; i > 0; i--) {
        int rem = sum - i;
        for (int j = i; j <= rem - j; j++) {
            auto ncnt = cnt;
            if (numbers_available(i, j, ncnt)) {
                return i;
            }
        }
    }
    return 0;
}

int main() {
    cin.tie(0) -> sync_with_stdio(0);
    cin >> N;
    for (int i = 0; i < N; i++) {
        int x;
        cin >> x;
        sum += x;
        cnt[x]++;
    }
    
    cout << solve();
}
#endif
}
