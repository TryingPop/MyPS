using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 13
이름 : 배성훈
내용 : 고장난 시계
    문제번호 : 21981번

    수학, 정수론, 중국인의 나머지 정리, 확장 유클리드 호제법 문제다
    음수 처리와 오버플로우로 총 5번, 인덱스 에러로 1번 틀렸다
    
    하루 86400초이므로 Ax + By = gcd(A, B) 합동식에서 오버플로우가 일어난다!
    실제로 예제 
        1
        3
        15:19:59 16:07:49 15:44:54
        966 392 667
    에서 30억 넘어가면서 오버플로우 난다!

    그리고, 서브테스크 2번 보면, n <= 50_000이라, 5만 배열로 했는데 인덱스 에러 떴다!
    7만으로 고치니 이상없어졌다

    아이디어는 다음과 같다
    먼저 0번 시계를 기준으로 i번 시계와 만나는 처음시간 s와 주기 t를 찾는다 (이를 찾는데 확장된 유클리드 알고리즘이 쓰인다)
    그러면 해당 합동식 x = si (mod ti)가되고 이를 i = 1, ..., n - 1번까지 진행한다
    그러면 연립 일차 합동식을 얻는다 이후에 이 연립 일차 합동식의 해를 찾는다 (여기에 나머지 정리가 쓰였다)
    이를 순차적으로 진행했다

    만약 해가 없다면 0, 해가 있다면 해의 주기t를 하루로 나눠 제출했다 (해를 찾는데 확장된 유클리드 알고리즘이 쓰인다)
    이러니 900ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0520
    {

        static void Main520(string[] args)
        {

            int MOD = 86_400;
            string NO = "0\n";
            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new(Console.OpenStandardOutput());

            int test = ReadInt();

            long[] time = new long[70_000];
            long[] d = new long[70_000];

            while (test-- > 0)
            {

                int n = ReadInt();
                for (int i = 0; i < n; i++)
                {

                    time[i] = ReadTime();
                }

                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt() % MOD;
                    cur = cur < 0 ? MOD + cur : cur;
                    d[i] = cur;
                }

                bool impo = false;

                long ret1 = 0;
                long ret2 = 1;
                for (int i = 1; i < n; i++)
                {

                    if (ChkInvalidCalc1(i, out long s, out long t))
                    {

                        impo = true;
                        break;
                    }

                    if (ChkInvalidCalc2(ret1, ret2, s, t, out ret1, out ret2))
                    {

                        impo = true;
                        break;
                    }
                }

                if (impo || ret1 == -1 || ret2 == -1)
                {

                    sw.Write(NO);
                    continue;
                }

                long ret = MOD / ret2;
                sw.Write($"{ret}\n");
            }

            sr.Close();
            sw.Close();

            bool ChkInvalidCalc2(long _s1, long _t1, long _s2, long _t2, out long _s, out long _t)
            {

                long gcd = GetGCD(_t1, _t2, out long iA, out long iB);

                if ((_s1 - _s2) % gcd != 0)
                {

                    _s = -1;
                    _t = -1;
                    return true;
                }

                _t = (_t1 * _t2 / gcd);
                _s = (_t1 * iA * ((_s2 - _s1) / gcd)) + _s1;
                _s %= _t;
                _s = _s < 0 ? _s + _t : _s;
                return false;
            }

            bool ChkInvalidCalc1(int _idx, out long s, out long t)
            {

                long diff1 = d[_idx] - d[0];
                long diff2 = time[0] - time[_idx];

                diff1 = diff1 < 0 ? diff1 + MOD : diff1;
                diff2 = diff2 < 0 ? diff2 + MOD : diff2;

                long gcd;
                long f = 1, b = 1;
                if (diff1 == 0) gcd = MOD;
                else gcd = GetGCD(diff1, MOD, out f, out b);

                if ((diff1 == 0 && diff2 != 0) || (diff2 % gcd != 0))
                {

                    s = -1;
                    t = -1;
                    return true;
                }

                t = MOD / gcd;
                s = (f * (diff2 / gcd)) % t;
                s = s < 0 ? s + t : s;

                return false;
            }
            long GetGCD(long _a, long _b, out long _iA, out long _iB)
            {

                long s1 = 1;
                long s2 = 0;

                long t1 = 0;
                long t2 = 1;

                long q, temp;
                while (_b > 0)
                {

                    temp = _a % _b;
                    q = (_a - temp) / _b;

                    _a = _b;
                    _b = temp;

                    temp = -q * s2 + s1;
                    s1 = s2;
                    s2 = temp;

                    temp = -q * t2 + t1;
                    t1 = t2;
                    t2 = temp;
                }

                _iA = s1;
                _iB = t1;
                return _a;
            }

            int ReadTime()
            {

                int h = sr.Read() - '0';
                h = h * 10 + sr.Read() - '0';

                sr.Read();

                int m = sr.Read() - '0';
                m = m * 10 + sr.Read() - '0';

                sr.Read();

                int s = sr.Read() - '0';
                s = s * 10 + sr.Read() - '0';

                if (sr.Read() == '\r') sr.Read();
                return 3600 * h + 60 * m + s;
            }

            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if (c == '-')
                    {

                        plus = false;
                        continue;
                    }

                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }

#if other
//#include <bits/stdc++.h>

using namespace std;

typedef long long ll;

// #define MOD (60*60*24)

vector<pair<ll, ll>> normalize(vector<pair<ll, ll>> &timers)
{
    unordered_map<ll, ll> m;
    vector<pair<ll, ll>> ret;

    for (ll i = 0; i < timers.size(); i++)
    {
        ll A, B;
        tie(A, B) = timers[i];

        auto it = m.find(A);
        if (it == m.end())
        {
            m[A] = B;
            ret.push_back(make_pair(A, B));
        }
        else
        {
            if (it->second != B)
                return vector<pair<ll, ll>>();
        }
    }

    return ret;
}

int main(void)
{
    cin.tie(0);
    ios::sync_with_stdio(0);

    ll T;
    cin >> T;

    while (T--)
    {
        ll N;
        cin >> N;

        /*
         * y = Ax + B
         * A: 속도
         * B: offset
         */
        vector<pair<ll, ll>> timers(N);

        for (ll i = 0; i < N; i++)
        {
            ll h, m, s;
            string str;
            cin >> str;

            h = (str[0]-'0')*10 + (str[1]-'0');
            m = (str[3]-'0')*10 + (str[4]-'0');
            s = (str[6]-'0')*10 + (str[7]-'0');

            timers[i].second = h*60*60 + m*60 + s;
        }

        for (ll i = 0; i < N; i++)
        {
            cin >> timers[i].first;
            timers[i].first %= MOD;
            if (timers[i].first < 0)
                timers[i].first += MOD;
        }

        vector<pair<ll, ll>> normtimers = normalize(timers);

        if (normtimers.size() == 0)
        {
            cout << 0 << "\n";
            continue;
        }

        if (normtimers.size() == 1)
        {
            if (N == 1)
                cout << "0\n";
            else
                cout << MOD << "\n";

            continue;
        }

        ll ans = 0;

        for (ll x = 0; x < MOD; x++)
        {
            bool ok = true;

            ll y0 = (normtimers[0].first*x + normtimers[0].second) % MOD;
            if (y0 < 0)
                y0 += MOD;

            for (ll i = 1; i < normtimers.size(); i++)
            {
                ll yi = (normtimers[i].first*x + normtimers[i].second) % MOD;
                if (yi < 0)
                    yi += MOD;

                if (y0 != yi)
                {
                    ok = false;
                    break;
                }
            }

            if (ok)
                ans++;
        }

        cout << ans << "\n";
    }

    return 0;
}
#elif other2
// #include <bits/stdc++.h>
using namespace std;

const int LM = 70004;
const int MOD = 86400;

int N;
int x[LM], d[LM];

int gcd(int n, int m){
    if(!n) return abs(m);
    if(!m) return abs(n);
    return __gcd(abs(n), abs(m));
}

void solve(){
    scanf("%d", &N);
    for(int i=1;i<=N;i++){
        int a,b,c;
        scanf("%d:%d:%d",&a,&b,&c);
        x[i] = a*3600 + b*60+c;
    }
    for(int i=1;i<=N;i++){
        scanf("%d", d+i); d[i] = (d[i]%MOD+MOD)%MOD;
    }
    if(N == 2){
        if(d[1] == d[2]){
            printf("%d\n", x[1]==x[2] ? MOD : 0);
        }
        else{
            int g = gcd(d[2]-d[1], MOD);
            if(abs(x[2]-x[1]) % g) puts("0");
            else printf("%d\n", g);
        }
        return;
    }
    for(int i=1;i<=N-2;i++){
        if((1LL*(d[i+2]-d[i+1])*(x[i+1]-x[i])%MOD+MOD)%MOD != (1LL*(d[i+1]-d[i])*(x[i+2]-x[i+1])%MOD+MOD)%MOD){
            puts("0");
            return;
        }
    }
    int len = MOD;
    for(int i=1;i<N;i++){
        len = gcd(len, (d[i+1]-d[i]+MOD)%MOD);
    }
    printf("%d\n", len);
}

int main(){
    int T;
    scanf("%d", &T);
    while(T--) solve();
    return 0;
}
#elif other3
def secs(x):
    return int(x[0]) * 3600 + int(x[1]) * 60 + int(x[2])


for i in range(int(input())):
    n = int(input())
    t = [secs(_.split(sep=':')) for _ in input().split()]
    d = [int(_) % 86400 for _ in input().split()]

    appeared = [-1] * 86400
    unique = list()
    cnt = 0

    for j in range(n):
        if appeared[d[j]] == -1:
            appeared[d[j]] = t[j]
            unique.append((t[j], d[j]))
        elif appeared[d[j]] != t[j]:
            break
    else:
        for j in range(1, 86400 + 1):
            base = (unique[0][0] + j * unique[0][1]) % 86400
            for k in range(1, len(unique)):
                target = (unique[k][0] + j * unique[k][1]) % 86400
                if base != target:
                    break
            else:
                cnt += 1
    print(cnt)
#endif
}
