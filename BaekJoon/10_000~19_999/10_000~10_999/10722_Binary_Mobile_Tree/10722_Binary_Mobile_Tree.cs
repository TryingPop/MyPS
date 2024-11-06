using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 10
이름 : 배성훈
내용 : Binary Mobile Tree
    문제번호 : 10722번

    트리, 트리 dp, 물리학 문제다
    범위 계산을 잘못해 여러 번 틀렸다

    막대는 10만개가 올 수 있고 추의 최대 무게가 10만이므로
    메달릴 수 있는 추의 무게는 int 범위를 벗어날 수 있다
    이진 트리 형태면 5만 x 10만 > int.MaxValue이다
*/

namespace BaekJoon.etc
{
    internal class etc_1044
    {

        static void Main1044(string[] args)
        {

            int MAX = 100_000;
            StreamReader sr;
            StreamWriter sw;
            
            int n;
            long[] wL, wR;
            int[] lengths;
            double[] eL, eR;

            Solve();
            void Solve()
            {

                Init();

                int t = ReadInt();

                while (t-- > 0)
                {

                    Input();

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                WeightsDFS(1);
                double ret = eR[1] - eL[1];

                sw.Write($"{ret:0.000000000}\n");
            }

            void Input()
            {

                n = ReadInt();
                for (int i = 1; i <= n; i++)
                {

                    eL[i] = 0;
                    eR[i] = 0;
                }

                for (int i = 1; i <= n; i++)
                {

                    int len = ReadInt();
                    int l = ReadInt();
                    int r = ReadInt();

                    lengths[i] = len;
                    wL[i] = l;
                    wR[i] = r;
                }
            }

            long WeightsDFS(long _cur)
            {

                long lidx = wL[_cur] > 0 ? 0 : -wL[_cur];
                long ridx = wR[_cur] > 0 ? 0 : -wR[_cur];

                if (0 < lidx) wL[_cur] = WeightsDFS(lidx);
                if (0 < ridx) wR[_cur] = WeightsDFS(ridx);

                double l = (-1.0 * wR[_cur] * lengths[_cur]) / (wL[_cur] + wR[_cur]);
                double r = (1.0 * wL[_cur] * lengths[_cur]) / (wL[_cur] + wR[_cur]);

                eL[_cur] = Math.Min(l + eL[lidx], r + eL[ridx]);
                eR[_cur] = Math.Max(l + eR[lidx], r + eR[ridx]);

                return wL[_cur] + wR[_cur];
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                wL = new long[MAX + 1];
                wR = new long[MAX + 1];
                lengths = new int[MAX + 1];
                eL = new double[MAX + 1];
                eR = new double[MAX + 1];
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
                    if (c == -1 || c == ' ' || c == '\n') return true;

                    bool positive = c != '-';
                    ret = positive ? c - '0' : 0;

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    ret = positive ? ret : -ret;

                    return false;
                }
            }
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;

// #define endl '\n'

// #define PRECISION 12

// #define fr first
// #define sc second

using ll = long long;
using ld = long double;

typedef pair<ll,ll> pll;
typedef pair<int,int> pii;

const ll MOD = 1e9+7;
const ll INF = 4'500'000'000'000'000'000;

int T; ld ansl = 0; ld ansr = 0;

// 0 = left, 1 = right
ll dfs(ll cur, int dir, vector<ll>& l, vector<ll>& r, vector<ll>& len, vector<pll> &dp){
    ll node = abs(cur);
    if(cur > 0){
        return cur;
    }
    if(cur == -1){
        ll cl = dfs(l[node], 0, l, r, len, dp);
        ll cr = dfs(r[node], 1, l, r, len, dp);

        dp[node] = {cl , cr};
        return cl + cr;
    }
    else{
        ll cl = dfs(l[node], dir, l, r, len, dp);
        ll cr = dfs(r[node], dir, l, r, len, dp);
        ld x = (ld)(cr*len[node])/(cl+cr);

        dp[node] = {cl , cr};
        return cl + cr;
    }
}

void ddfs(int cur, ld cst, vector<ll>& l, vector<ll>& r, vector<ll>& len, vector<pll> &dp){
    int node = abs(cur);
    if(cur > 0) return;
    ll cl = dp[node].fr, cr = dp[node].sc;
    ld x = (ld)(cr*len[node])/(cl+cr);
    ld y = (ld)len[node] - x;
    ansl = max(ansl, cst+x); ansr = min(ansr, cst-y);
    ddfs(l[node], cst + x, l, r, len, dp);
    ddfs(r[node], cst - y, l, r, len, dp);
}

int main(){
    ios::sync_with_stdio(0);
    cin.tie(0);
    cout.setf(ios::fixed); cout.precision(PRECISION);

    cin >> T;
    while(T--){
        int n;
        cin >> n;
        vector<ll>left(n+1), right(n+1), len(n+1);
        vector<pll>dp(n+1, {0, 0});
        for(int i=1; i<=n; i++){
            cin >> len[i] >> left[i] >> right[i];
        }
        ansl = ansr = 0;
        dfs(-1, -1, left, right, len, dp);
        ddfs(-1, 0.0, left, right, len, dp);
        // cout << ansl << ' ' <<ansr << endl;
        cout << ansl - ansr << endl;
    }
}

#endif
}
