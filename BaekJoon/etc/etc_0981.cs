using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 20
이름 : 배성훈
내용 : ABC 배열 놀이
    문제번호 : 25574번

    구현, dp, 브루트포스, 두 포인터 문제다
    중앙을 기준으로 정답을 찾는다

    그러면 왼쪽과 오른쪽 포인터를 쓰게되고
    왼쪽의 최댓값, 최솟값
    오른쪽의 최댓값, 최솟값을 dp에 저장한다

    그리고 모든 경우를 곱해서 최댓값을 찾는다
    이후 중앙을 오른쪽으로 한칸씩 이동시킨다

    그러면 왼쪽, 중앙, 오른쪽인 경우 1개를 찾는다
    이걸 6번하면 정답이 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0981
    {

        static void Main981(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            long INF = -1_000_000_000_000_000_000L;

            long ret;
            int n, a, b, c;
            long[] arr;
            long[] minL, maxL, minR, maxR;

            Solve();
            void Solve()
            {

                Init();

                int test = ReadInt();

                while(test-- > 0)
                {

                    Input();

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void FillL(int _len)
            {

                minL[_len] = arr[_len] - arr[0];
                maxL[_len] = arr[_len] - arr[0];

                for (int i = _len + 1; i <= n; i++)
                {

                    minL[i] = Math.Min(minL[i - 1], arr[i] - arr[i - _len]);
                    maxL[i] = Math.Max(maxL[i - 1], arr[i] - arr[i - _len]);
                }
            }

            void FillR(int _len)
            {

                minR[n - _len + 1] = arr[n] - arr[n - _len];
                maxR[n - _len + 1] = arr[n] - arr[n - _len];

                for (int i = n - _len; i > 0; i--)
                {

                    minR[i] = Math.Min(minR[i + 1], arr[i + _len - 1] - arr[i - 1]);
                    maxR[i] = Math.Max(maxR[i + 1], arr[i + _len - 1] - arr[i - 1]);
                }
            }

            void GetRet()
            {

                ret = INF;
                FillL(a);
                FillR(c);

                ChkRet(a, b, c);
                FillR(b);

                ChkRet(a, c, b);

                FillL(c);
                ChkRet(c, a, b);

                FillR(a);
                ChkRet(c, b, a);

                FillL(b);
                ChkRet(b, c, a);

                FillR(c);
                ChkRet(b, a, c);

                sw.Write($"{ret}\n");
            }

            void ChkRet(int _l, int _m, int _r)
            {

                int e = n - _m - _r + 1;
                for (int i = _l + 1; i <= e; i++)
                {

                    long mid = arr[i + _m - 1] - arr[i - 1];
                    ret = Math.Max(ret, mid * maxL[i - 1] * maxR[i + _m]);
                    ret = Math.Max(ret, mid * minL[i - 1] * maxR[i + _m]);
                    ret = Math.Max(ret, mid * maxL[i - 1] * minR[i + _m]);
                    ret = Math.Max(ret, mid * minL[i - 1] * minR[i + _m]);
                }
            }

            void Input()
            {

                n = ReadInt();
                a = ReadInt();
                b = ReadInt();
                c = ReadInt();

                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt() + arr[i - 1];
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int LEN = 100_000 + 1;

                arr = new long[LEN];
                minL = new long[LEN];
                minR = new long[LEN];
                maxL = new long[LEN];
                maxR = new long[LEN];
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool positive = c != '-';
                int ret = positive ? c - '0' : 0;

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return positive ? ret : -ret;
            }
        }
    }

#if other
// #include <iostream>
// #define MIN -1000000000000000000
// #define MAXN 100000
using namespace std;

void init();
void solve(int a, int b, int c);

long long int ans;
long long int arr[MAXN];
long long int prefix_sum[MAXN];
long long int suffix_sum[MAXN];
long long int prefix_value[3][MAXN][2];
long long int suffix_value[3][MAXN][2];
int n;
int len[3];

int main() {
    ios_base::sync_with_stdio(false);
    cin.tie(NULL);
    cout.tie(NULL);

    int T;
    cin >> T;
    while (T) {
        T--;
        init();

        solve(0, 1, 2); // A - B - C
        solve(0, 2, 1); // A - C - B
        solve(1, 0, 2); // B - A - C
        solve(1, 2, 0); // B - C - A
        solve(2, 0, 1); // C - A - B
        solve(2, 1, 0); // C - B - A

        cout << ans << "\n";
    }
}

void init() {
    ans = MIN;
    cin >> n >> len[0] >> len[1] >> len[2];
    for (int i = 0; i < n; i++) 
        cin >> arr[i];
    
    prefix_sum[0] = arr[0];
    suffix_sum[0] = arr[n - 1];
    for (int i = 1; i < n; i++) {
        prefix_sum[i] = prefix_sum[i - 1] + arr[i];
        suffix_sum[i] = suffix_sum[i - 1] + arr[n - 1 - i];
    }

    for (int idx = 0; idx < 3; idx++) {
        prefix_value[idx][0][0] = prefix_sum[len[idx] - 1];
        prefix_value[idx][0][1] = prefix_sum[len[idx] - 1];
        suffix_value[idx][0][0] = suffix_sum[len[idx] - 1];
        suffix_value[idx][0][1] = suffix_sum[len[idx] - 1];

        for (int i = 1; i <= n - len[idx]; i++) {
            long long int tmp;

            prefix_value[idx][i][0] = prefix_value[idx][i - 1][0];
            prefix_value[idx][i][1] = prefix_value[idx][i - 1][1];
            tmp = prefix_sum[i + len[idx] - 1] - prefix_sum[i - 1];
            if (tmp > prefix_value[idx][i][0])
                prefix_value[idx][i][0] = tmp;
            if (tmp < prefix_value[idx][i][1])
                prefix_value[idx][i][1] = tmp;

            suffix_value[idx][i][0] = suffix_value[idx][i - 1][0];
            suffix_value[idx][i][1] = suffix_value[idx][i - 1][1];
            tmp = suffix_sum[i + len[idx] - 1] - suffix_sum[i - 1];
            if (tmp > suffix_value[idx][i][0])
                suffix_value[idx][i][0] = tmp;
            if (tmp < suffix_value[idx][i][1])
                suffix_value[idx][i][1] = tmp;
        }
    }
}

void solve(int a, int b, int c) {
    int start = len[a];
    int end = n - len[b] - len[c];

    for (int idx = start; idx <= end; idx++) {
        long long int vb = prefix_sum[idx + len[b] - 1] - prefix_sum[idx - 1];
        int idx_a = idx - len[a];
        int idx_c = n - (idx + len[b] + len[c]);

        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < 2; j++) {
                long long int tmp = vb * prefix_value[a][idx_a][i] * suffix_value[c][idx_c][j];

                if (tmp > ans)
                    ans = tmp;
            }
        }
    }
}
#elif other2
// #include<bits/stdc++.h>
using namespace std;
typedef long long lg;
typedef long double ld;
// #define INF 1'000'000'000'000'000'000
// #define ff first
// #define ss second

lg n,m,k,t,y,x;
lg ans = -INF;
lg arr[100005];
pair<lg,lg> aa[100005], cc[100005];

void f(vector<lg> v){
    lg tmpsum = 0;
    for(lg i=v[0];i<=n;i++){
        if(i == v[0]){
            for(lg j=1;j<=v[0];j++){
                tmpsum += arr[j];
            }
            aa[v[0]] = {tmpsum, tmpsum};
            continue;
        }
        else{
            tmpsum += arr[i];
            tmpsum -= arr[i - v[0]];
        }
        aa[i].ff = max(aa[i-1].ff, tmpsum);
        aa[i].ss = min(aa[i-1].ss, tmpsum);
    }

    tmpsum = 0;
    for(lg i=v[2];i<=n;i++){
        if(i == v[2]){
            for(lg j=1;j<=v[2];j++){
                tmpsum += arr[n-j+1];
            }
            cc[n-v[2]+1] = {tmpsum, tmpsum};
            continue;
        }
        else{
            tmpsum += arr[n-i+1];
            tmpsum -= arr[n-(i - v[2])+1];
        }
        cc[n-i+1].ff = max(cc[n-(i-1)+1].ff, tmpsum);
        cc[n-i+1].ss = min(cc[n-(i-1)+1].ss, tmpsum);
    }

    // for(lg i=1;i<=n;i++){
    //     cout << aa[i].ff << " " << aa[i].ss << "\n";
    // }


    tmpsum = 0;
    for(lg i=v[0]+1; i+v[1]+v[2]-1<=n;i++){
        //b의 시작 위치
        if(i == v[0]+1){
            for(lg j=0;j<v[1];j++){
                tmpsum += arr[i+j];
            }
        }
        else{
            tmpsum += arr[i+v[1]-1];
            tmpsum -= arr[i-1];
        }
        
        //4개 중 하나
        lg low0 = i-1;
        lg hi2 = i+v[1];
        ans = max(ans, tmpsum * aa[low0].ff * cc[hi2].ff);
        ans = max(ans, tmpsum * aa[low0].ss * cc[hi2].ff);
        ans = max(ans, tmpsum * aa[low0].ff * cc[hi2].ss);
        ans = max(ans, tmpsum * aa[low0].ss * cc[hi2].ss);
    }
}

int main(){
    ios_base::sync_with_stdio(0);
    cin.tie(0);

    cin >> t;
    while(t--){
        ans = -INF;

        cin >> n;
        vector<lg> abc;
        for(lg i=0;i<3;i++){
            lg a;
            cin >> a;
            abc.push_back(a);
        }
        sort(abc.begin(),abc.end());

        for(lg i=1;i<=n;i++){
            cin >> arr[i];
        }

        do{
            f(abc);
        }while(next_permutation(abc.begin(),abc.end()));

        cout << ans << "\n";
    }
}
#endif
}