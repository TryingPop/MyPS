using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 18
이름 : 배성훈
내용 : 업무 처리
    문제번호 : 14234번

    비트필드를 이용한 다이나믹 프로그래밍 문제다.
    dp[i][j] = val를 i일까지 일을 했고 해결한 문제 상황이 j일 때 가장 많이 해결한 문제 수를 val가 되게 설정한다.
    그러면 해당 날짜에 아직 풀지 않은 문제를 보며, 
*/

namespace BaekJoon.etc
{
    internal class etc_1872
    {

        static void Main1872(string[] args)
        {

            // 14234 - 업무처리
            int n;
            (int s, int e)[] problem;
            int[][] term;

            Input();

            GetRet();

            void GetRet()
            {

                int[][] dp = new int[102][];
                for (int i = 0; i <= 101; i++)
                {

                    dp[i] = new int[1 << n];
                    Array.Fill(dp[i], -1);
                }

                int ret = 0;
                dp[0][0] = 0;
                for (int day = 0; day <= 100; day++)
                {

                    for (int pState = 0; pState < dp[day].Length; pState++)
                    {

                        if (dp[day][pState] == -1) continue;
                        for (int pNum = 0; pNum < n; pNum++)
                        {

                            int curState = 1 << pNum;
                            // 이미 해결한 문제인지 혹은 해결할 수 없는 날짜인지 확인
                            if ((pState & curState) == curState
                                || ImpoDay(pNum, day)) continue;

                            // 문제 해결 시도..
                            int nextDay = day + term[pNum][day];
                            if (problem[pNum].e < nextDay) continue;
                            int nextState = pState | curState;
                            dp[nextDay][nextState] = Math.Max(dp[nextDay][nextState], dp[day][pState] + 1);
                        }

                        dp[day + 1][pState] = Math.Max(dp[day + 1][pState], dp[day][pState]);
                        ret = Math.Max(dp[day][pState], ret);
                    }
                }

                Console.Write(ret);

                bool ImpoDay(int pNum, int day)
                    => problem[pNum].e < day || day < problem[pNum].s;
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                problem = new (int s, int e)[n];
                term = new int[n][];

                for (int i = 0; i < n; i++)
                {

                    int a = ReadInt();
                    int b = ReadInt();
                    problem[i] = (a, b);
                    term[i] = new int[101];
                    Array.Fill(term[i], -1);
                    for (int j = a; j <= b; j++)
                    {

                        term[i][j] = ReadInt() - 1;
                    }
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
// #pragma GCC optimize("O3")
// # include<bits/stdc++.h>
using namespace std;
 
template<typename T>
ostream& operator<< (ostream& out, vector<T> v) {
   out<<"Vector: { ";
   for(T i : v) out << i <<" , ";
   out << "\b\b}"; //<<endl;
   return out;
}
template<typename T>
ostream& operator<< (ostream& out, set<T> v) {
   out<<"Set: { ";
   for(T i : v) out << i<<" , ";
   out << "\b\b}"<<endl;
   return out;
}
template<typename T>
ostream& operator<< (ostream& out, multiset<T> v) {
   out<<"Multiset: { ";
   for(T i : v) out << i<<" , ";
   out << "\b\b}"<<endl;
   return out;
}
template<typename T>
ostream& operator<< (ostream& out, pair<T,T> v) {
   out<<"("<<v.first<<','<<v.second<<") ";
   return out;
}
 
using ll = long long;
// # ifdef MY_LOCAL
static ifstream ciin("!input.txt");
// #define cin ciin
// #endif
    constexpr int MAX = 15, MAXB = 101, MAXT = 32768; // 2**15 = 32768
    const int dx[]{0,1,0,-1}, dy[]
{ 1,0,-1,0};
// #define int long long
vector<pair<int, int>> v[MAX];
int two[MAX + 1];
size_t N;
int dp[MAXT];
// ======================================
signed main()
{
    ios_base::sync_with_stdio(false), cin.tie(0), cout.tie(0);
    two[0] = 1;
    for (int i = 1; i <= MAX; ++i)
    {
        two[i] = two[i - 1] << 1;
    }
    cin >> N;
    for (int i = 0; i < N; ++i)
    {
        int a, b; cin >> a >> b;
        for (int j = a; j <= b; ++j)
        {
            int x; cin >> x;
            x += j - 1;
            // cout<<j<<' '<<x<<endl;
            if (x > b) continue;
            while (!v[i].empty() and v[i].back().second >= x) v[i].pop_back();
v[i].push_back({ j,x});
		}
		// cout<<"v["<<i<<"]=";
		// for(auto&[x,y]:v[i]){
		// 	cout<<x<<','<<y<<endl;
		// }
		// cout<<endl;
	}
	memset(dp, 0x3f, sizeof(int) * MAXT);
int inf = dp[0];
vector<int> dial[MAXB];
dp[0] = 0;
dial[0].push_back(0);
// dp[b] = 비트마스크를 b로 만들기 위해 필요한 최소 공간! 
for (int i = 0; i < MAXB; ++i)
{
    while (!dial[i].empty())
    {
        int b = dial[i].back();
        dial[i].pop_back();
        // cout<<"? "<<i<<' '<<b<<endl;
        if (dp[b] < i) continue;
        for (int j = 0; j < N; ++j)
        {
            if (b & two[j]) continue;
            auto it = upper_bound(v[j].begin(), v[j].end(), pair<int, int>{ i,0});
if (it != v[j].end())
{
    int nb = b | two[j], np = it->second;
    if (dp[nb] > np)
    {
        dp[nb] = np;
        dial[np].push_back(nb);
    }
}
			}
		}
	}
	int ans = 0;
for (int b = 1; b < two[N]; ++b)
{
    if (dp[b] < inf)
    {
        // cout<<"! "<<b<<endl;
        ans = max(ans, (int)__builtin_popcount(b));
    }
}
cout << ans;
}

#endif
}
