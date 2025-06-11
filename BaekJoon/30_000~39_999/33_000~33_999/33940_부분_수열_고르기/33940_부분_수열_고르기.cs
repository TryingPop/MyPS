using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 11
이름 : 배성훈
내용 : 부분 수열 고르기
    문제번호 : 33940번

    수학, 해 구성하기 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1694
    {

        static void Main1694(string[] args)
        {

            int n;
            long a, d, m;

            Input();

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                if (m == a)
                {

                    sw.Write($"1\n{a}");
                    return;
                }
                else if (m < a)
                {

                    sw.Write(-1);
                    return;
                }

                long[] sums = new long[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    sums[i] = (i - 1) + sums[i - 1];
                }

                int len = GetLen();

                sw.Write($"{len}\n");

                if (len <= 0) return;

                long dSum = (m - len-- * a) / d;
                
                for (int i = 1; i <= n; i++)
                {

                    // 현재거 선택
                    long curSum = dSum - (i - 1);
                    long min = sums[i + len] - sums[i];
                    long max = sums[n] - sums[n - len];
                    if (curSum < min || max < curSum) continue;

                    // 선택 가능하니 선택
                    dSum -= (i - 1);
                    sw.Write($"{a + (i - 1) * d} ");
                    if (len-- == 0) break;
                }

                int GetLen()
                {

                    // 부분 수열 길이 찾기
                    long dSum = m;      // d들의 총합이 된다.
                    int ret = -1;
                    // i개 원소 선택
                    for (int i = 1; i <= n; i++)
                    {

                        // i * a 만큼 빼준다.
                        dSum -= a;
                        // dSum 은 ?d 들의 합이므로 d로 나눠떨어져야 한다.
                        // 나눠떨어지지 않는 경우면 불가능한 경우다.
                        // 
                        // dSum <= 0이면 a들로만 이루어진다는말과 같다.
                        // 즉, m = ∑a형태이다. 여기서 m = a는 앞에서 반례처리
                        // 이외는 불가능하다.
                        if (dSum <= 0 || dSum % d != 0) continue;

                        long cnt = dSum / d;
                        // 갯수가 최소 최대 범위를 벗어나면 불가능하다.
                        if (cnt < sums[i] || sums[n] - sums[n - i] < cnt) continue;

                        ret = Math.Max(i, ret);
                    }

                    return ret;
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                a = long.Parse(temp[1]);
                d = long.Parse(temp[2]);
                m = long.Parse(temp[3]);
            }
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;
using ll=long long;
using pii=pair<int,int>;
using pll=pair<ll,ll>;
// #define fi first
// #define se second
// #define all(x) x.begin(),x.end()
const int INF=1e9;
const ll LINF=4e18;

ll sum(ll s,ll e){ return e*(e+1)/2-s*(s-1)/2; }

void TESTCASE() {
    ll N,a,d,M; cin>>N>>a>>d>>M;
    ll ans=-1;
    for(int i=1;i<=N;i++){
        ll t=M-a*i;
        if(t<0||t%d!=0) continue;
        t/=d;
        if(sum(0,i-1)<=t&&t<=sum(N-i,N-1)) ans=i;
    }
    cout<<ans;
    if(ans==-1) return;
    ll target=(M-a*ans)/d-sum(0,ans-1);
    vector<int> v(ans);
    for(int i=0;i<ans;i++) v[i]=i;
    for(int i=ans-1;i>=0;i--){
        int t=min(N-ans,target);
        v[i]+=t;
        target-=t;
    }
    cout<<'\n';
    for(auto i:v) cout<<a+i*d<<'\n';
}

int main() {
    ios_base::sync_with_stdio(0); cin.tie(0);
    int T=1; //cin>>T;
    for (int tc=1;tc<=T;tc++) {
        TESTCASE();
    }
    return 0;
}
#endif
}
