using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 14
이름 : 배성훈
내용 : Evacuation Plan
    문제번호 : 3522번

    dp 문제다.
    dp[i][j] = val를
    정렬된 i번째 사람을 선택했고,
    j번째 방공호까지 최소 1명을 채운상태이다.
    그리고 val를 최소 비용을 담는다.

    여기서 번째 이므로 시작 번호는 1번이다!

    그러면 다음과 같은 점화식을 얻는다.
    dp[i][j] = min(dp[i - 1][j - 1], dp[i - 1][j]) + (i번째 사람을 j번째 방공호로 보내는 비용)
*/

namespace BaekJoon.etc
{
    internal class etc_1701
    {

        static void Main1701(string[] args)
        {

            int n, m;
            (int pos, int idx)[] src, dst;

            Input();

            GetRet();
            void GetRet()
            {

                long INF = 1_000_000_000_000_000_000;
                Array.Sort(src, (x, y) => x.pos.CompareTo(y.pos));
                Array.Sort(dst, (x, y) => x.pos.CompareTo(y.pos));

                // dp[i][j] = val
                // 정렬된 i 번째 사람을 선택
                // 정렬된 j 번째 방공호까지 최소 1명이 채워진 경우
                // val는 최소 비용
                long[][] dp = new long[n + 1][];
                for (int i = 0; i <= n; i++)
                {

                    dp[i] = new long[m + 1];
                    Array.Fill(dp[i], INF);
                }

                dp[0][0] = 0;
                for (int i = 1; i <= m; i++)
                {

                    for (int j = 1; j <= i; j++)
                    {

                        dp[i][j] = GetDis(i - 1, j - 1) + Math.Min(dp[i - 1][j - 1], dp[i - 1][j]);
                    }
                }

                for (int i = m + 1; i <= n; i++)
                {

                    for (int j = 1; j <= m; j++)
                    {

                        dp[i][j] = GetDis(i - 1, j - 1) + Math.Min(dp[i - 1][j - 1], dp[i - 1][j]);
                    }
                }

                int[] ret = new int[n + 1];
                ret[GetSrcIdx(n)] = GetDstIdx(m);
                int end = m;
                for (int i = n - 1; i > 0; i--)
                {

                    if (dp[i][end] >= dp[i][end - 1]) end--;
                    ret[GetSrcIdx(i)] = GetDstIdx(end);
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                sw.Write($"{dp[n][m]}\n");

                for (int i = 0; i < n; i++)
                {

                    sw.Write($"{ret[i]} ");
                }

                int GetDstIdx(int _i)
                    => dst[_i - 1].idx;

                int GetSrcIdx(int _i)
                    => src[_i - 1].idx;

                long GetDis(int _sIdx, int _dIdx)
                    => Math.Abs(src[_sIdx].pos - dst[_dIdx].pos);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                src = new (int pos, int idx)[n];
                for (int i = 0; i < n; i++)
                {

                    src[i] = (ReadInt(), i);
                }

                m = ReadInt();
                dst = new (int pos, int idx)[m];
                for (int i = 0; i < m; i++)
                {

                    dst[i] = (ReadInt(), i + 1);
                }

                int ReadInt()
                {

                    int ret = 0;
                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
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
    }

#if other
// #include <iostream>
// #include <algorithm>
// #include <bitset> 
using namespace std;
using ll = long long;

int main(void){
    ios::sync_with_stdio(0); cin.tie(0);
    int n; cin >> n;
    bitset<4096*4096> sel;
    pair<int,int> a[4005];
    for (int i = 0; i < n; i++){
        int v; cin >> v;
        a[i] = {v,i};
    }
    sort(a,a+n);

    int m; cin >> m;
    pair<int,int> b[4005];
    for (int i = 0; i < m; i++){
        int v; cin >> v;
        b[i] = {v, i};
    }
    sort(b, b+m);

    ll dp[4005];
    fill_n(dp, 4005, 0);
    for (int i = 0; i < n; i++){
        auto[t_pos, t_id] = a[i];
        ll up_left = 0;
        for (int j = 0; j < m && j <= i; j++){
            auto[s_pos, s_id] = b[j];
            ll up = dp[j];
            int sel_up_left = 0;
            if (j == 0){
                dp[j] = dp[0] + abs(t_pos - s_pos);
                sel_up_left = 0;
            }
            else if (j == i){
                dp[j] = up_left + abs(t_pos - s_pos);
                sel_up_left = 1;
            }
            else {
                dp[j] = min(up, up_left) + abs(t_pos - s_pos);
                sel_up_left = up_left < up;
            }
            sel.set(i*4096 + j, sel_up_left);

            up_left = up;
        }
    }
    ll ans = dp[m-1];
    cout << ans << "\n";
    int ts[4005];
    int ct = n - 1, cs = m - 1;
    while (ct >=0){
        ts[a[ct].second] = b[cs].second;
        if (sel[ct*4096 + cs]){
            cs--;
        }
        ct--;
    }
    for (int i = 0; i < n; i++)
        cout << ts[i]+1 << " ";
    cout << "\n";
}

#endif
}
