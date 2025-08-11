using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 10
이름 : 배성훈
내용 : 금민수의 합
    문제번호 : 1528번

    dp, BFS 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1815
    {

        static void Main1815(string[] args)
        {

            // 1528번 금민수
            int n = int.Parse(Console.ReadLine());

            Queue<int> q = new(n);
            int[] km = new int[1_000];
            int len = 0;

            int[] ret = new int[n + 1];
            int[] prev = new int[n + 1];
            Array.Fill(ret, -1);

            BFS_KM();

            BFS_RET();

            void BFS_RET()
            {

                q.Enqueue(0);
                ret[0] = 0;

                while (q.Count > 0)
                {

                    int cur = q.Dequeue();
                    
                    for (int i = 1; i < len; i++)
                    {

                        int next = cur + km[i];
                        if (next > n || ret[next] != -1) continue;
                        q.Enqueue(next);
                        ret[next] = ret[cur] + 1;
                        prev[next] = cur;
                    }
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                if (ret[n] <= 0)
                    sw.Write(-1);
                else
                {

                    int[] b = new int[ret[n]];
                    int l = 0;
                    for (int cur = n; cur != 0; cur = prev[cur])
                    {

                        b[l++] = cur - prev[cur];
                    }

                    Array.Sort(b);

                    for (int i = 0; i < l; i++)
                    {

                        sw.Write($"{b[i]} ");
                    }
                }
                
            }

            void BFS_KM()
            {

                q.Enqueue(0);

                while (q.Count > 0)
                {

                    int cur = q.Dequeue();
                    if (cur > n) break;
                    km[len++] = cur;

                    q.Enqueue(cur * 10 + 4);
                    q.Enqueue(cur * 10 + 7);
                }

                q.Clear();
            }
        }
    }
#if other
// #include <bits/stdc++.h>
using namespace std;

int main() {
    cin.tie(0) -> sync_with_stdio(0);
    
    int n;
    cin >> n; // <= 1e6

    vector<int> v = {4, 7};
    for (int s = 0, e = 2, _ = 5; _--;) {
        for (int i = s; i < e; i++) {
            v.push_back(v[i] * 10 + 4);
            v.push_back(v[i] * 10 + 7);
        }
        int num = e - s;
        tie(s, e) = pair<int, int>  {e, e + (e - s) * 2};
    }
    
    const int INF = 1e9;
    vector<int> dp(n + 1, INF);
    dp[0] = 0;

    for (auto num : v) {
        for (int i = num; i <= n; i++) dp[i] = min(dp[i], dp[i - num] + 1);
    }
    
    if (dp[n] == INF) {
        cout << -1;
        return 0;
    }
    
    assert(is_sorted(v.begin(), v.end()));
    for (auto num : v) {
        while (n && dp[n] == dp[n - num] + 1) { // 이미 해가 존재하는 경우라서 n이 양수이면 무조건 num이 존재할수밖에 없고 n-num>=0일수밖에 없음
            cout << num << " ";
            n -= num;
        }
    }

    return 0;
}
#endif
}
