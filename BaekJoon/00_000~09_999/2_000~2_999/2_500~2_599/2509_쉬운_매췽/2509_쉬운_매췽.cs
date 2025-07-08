using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 8
이름 : 배성훈
내용 : 쉬운 매췽
    문제번호 : 2509번

    브루트포스, 누적 합 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1753
    {

        static void Main1753(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int q = ReadInt();
            int t, p1, p2;
            int m1, m2;

            int[] tArr, p1Arr, p2Arr;
            (int s, int e)[] m1Arr, m2Arr;
            int[] cnt;

            Init();

            while (q-- > 0)
            {

                Input();

                Matching(p1Arr, p1, m1Arr, ref m1);
                Matching(p2Arr, p2, m2Arr, ref m2);

                // 이제 3번째, 4번째 조건 찾기
                int s = 0;
                for (int i = 0; i < m1; i++)
                {

                    int end = m1Arr[i].e;

                    for (int j = s; j < m2; j++)
                    {

                        // 두 포인터
                        if (end >= m2Arr[j].s) s++;
                        else
                        {

                            // 구간 확인
                            int n = tArr[m2Arr[j].s - 1] - tArr[end];
                            // 갯수 추가
                            cnt[n]++;
                        }
                    }
                }

                // 이제 가장 큰 경우의 수를 포함한 가장 작은 값 찾기
                int m3 = 0;    // 값
                int m4 = 0;    // 갯수
                for (int i = 1; i < cnt.Length; i++)
                {

                    if (m4 < cnt[i])
                    {

                        m3 = i;
                        m4 = cnt[i];
                    }

                    cnt[i] = 0;
                }

                sw.Write($"{m1} {m2} {m3} {m4}\n");
            }

            void Matching(int[] _pArr, int _p, (int s, int e)[] _mArr, ref int _m)
            {

                // 매췽 하는 찾고, 매췽이 되는 시작 지점과 끝지점을 _mArr에 저장
                _m = 0;
                for (int i = 1; i <= t; i++)
                {

                    int cur = i;
                    int prev = i - 1;
                    // 매칭하면 시작 지점과 끝 지점을 넣는다.
                    if (Find()) _mArr[_m++] = (i, prev);

                    bool Find()
                    {

                        // 매칭 여부 확인
                        for (int j = 0; j < _p; j++)
                        {

                            int find = _pArr[j];

                            int val = tArr[cur] - tArr[prev];

                            while (cur < t && val < find)
                            {

                                cur++;
                                val = tArr[cur] - tArr[prev];
                            }

                            if (val == find)
                            {

                                prev = cur;
                                cur = prev + 1;
                            }
                            else return false;
                        }

                        return true;
                    }
                }
            }

            void Init()
            {

                // 사용할 배열 사이즈 설정
                tArr = new int[5_002];

                p1Arr = new int[600];
                p2Arr = new int[600];

                m1Arr = new (int s, int e)[5_000];
                m2Arr = new (int s, int e)[5_000];

                cnt = new int[11_001];
            }

            void Input()
            {

                t = ReadInt();

                for (int i = 1; i <= t; i++)
                {

                    tArr[i] = ReadInt() + tArr[i - 1];
                }

                p1 = ReadInt();
                for (int i = 0; i < p1; i++)
                {

                    p1Arr[i] = ReadInt();
                }

                p2 = ReadInt();
                for (int i = 0; i < p2; i++)
                {

                    p2Arr[i] = ReadInt();
                }

                m1 = 0;
                m2 = 0;
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
#if other
// #include <bits/stdc++.h>

using namespace std;

typedef long long ll;

int t;
int n, arr[5002];
int m1, s1[602];
int m2, s2[602];
int pfs[5002];

bool chk1[5002], chk2[5002];
int counter[15002];

int main(){
    scanf("%d", &t);
    while(t--){
        scanf("%d", &n); for(int i=1; i<=n; i++) scanf("%d", &arr[i]), pfs[i] = pfs[i-1] + arr[i];
        pfs[n+1] = 0;
        scanf("%d", &m1); for(int i=1; i<=m1; i++) scanf("%d", &s1[i]);
        scanf("%d", &m2); for(int i=1; i<=m2; i++) scanf("%d", &s2[i]);

        memset(chk1, 0, sizeof(chk1));
        memset(chk2, 0, sizeof(chk2));
        memset(counter, 0, sizeof(counter));

        int ans = 0;
        for(int i=1; i<=n; i++){
            int pnt = i, sum = 0;
            bool good = 1;
            for(int j=m1; j>=1; j--){
                while(pnt > 0 && sum < s1[j]) sum += arr[pnt--];
                if(sum != s1[j]){
                    good = 0;
                    break;
                }
                sum = 0;
            }
            chk1[i] = good;
            if(good) ans++;
        }
        printf("%d ", ans);

        ans = 0;
        for(int i=1; i<=n; i++){
            int pnt = i, sum = 0;
            bool good = 1;
            for(int j=1; j<=m2; j++){
                while(pnt <= n && sum < s2[j]) sum += arr[pnt++];
                if(sum != s2[j]){
                    good = 0;
                    break;
                }
                sum = 0;
            }
            chk2[i] = good;
            if(good) ans++;
        }
        printf("%d ", ans);

        for(int i=1; i<=n; i++){
            if(!chk1[i]) continue;
            for(int j=i+2; j<=n; j++){
                if(!chk2[j]) continue;
                assert(1 <= pfs[j-1] - pfs[i] && pfs[j-1] - pfs[i] <= 15000);
                counter[pfs[j-1] - pfs[i]]++;
            }
        }

        int maxWeight = -1, maxNum = -1;
        for(int i=1; i<=15000; i++){
            if(maxWeight < counter[i]) maxWeight = counter[i], maxNum = i;
        }

        printf("%d %d\n", maxNum, maxWeight);
    }
}
#elif other2
// #include <stdio.h>  
// #include <algorithm>  
// #include <assert.h>
// #include <bitset>
// #include <cmath>  
// #include <complex>  
// #include <deque>  
// #include <functional>  
// #include <iostream>  
// #include <limits.h>  
// #include <map>  
// #include <math.h>  
// #include <queue>  
// #include <set>  
// #include <stdlib.h>  
// #include <string.h>  
// #include <string>  
// #include <time.h>  
// #include <unordered_map>  
// #include <unordered_set>  
// #include <vector>
// #pragma warning(disable:4996)
// #pragma comment(linker, "/STACK:336777216")
using namespace std;
// #define mp make_pair
// #define Fi first
// #define Se second
// #define pb(x) push_back(x)
// #define szz(x) ((int)(x).size())
// #define rep(i, n) for(int i=0;i<n;i++)
// #define all(x) (x).begin(), (x).end()
// #define ldb ldouble  
typedef tuple<int, int, int> t3;
typedef long long ll;
typedef unsigned long long ull;
typedef double db;
typedef long double ldb;
typedef pair <int, int> pii;
typedef pair <ll, ll> pll;
typedef pair <ll, int> pli;
typedef pair <db, db> pdd;
int IT_MAX = 1 << 20;
const ll MOD = 100003;
const int INF = 0x3f3f3f3f;
const ll LL_INF = 0x3f3f3f3f3f3f3f3f;
const db PI = acos(-1);
const db ERR = 1e-10;

int T[5050];
int sum[5050];

vector <int> Vl[2];
vector <int> Va[2];

int cnt[30050];
int main() {
	int TC;
	scanf("%d", &TC);
	while (TC--) {
		int N, i, j, k;
		scanf("%d", &N);
		for (i = 1; i <= N; i++) scanf("%d", &T[i]);
		for (i = 0; i < 2; i++) {
			int t, t2;
			scanf("%d", &t);
			while (t--) {
				scanf("%d", &t2);
				Vl[i].push_back(t2);
			}
		}
		for (i = 1; i <= N; i++) sum[i] = sum[i - 1] + T[i];

		for (i = 0; i < 2; i++) {
			for (j = 1; j <= N; j++) {
				int p = 0, s = 0;
				for (k = j; k <= N && p < Vl[i].size(); k++) {
					s += T[k];
					if (s == Vl[i][p]) {
						p++;
						s = 0;
					}
					else if (s > Vl[i][p]) break;
				}
				if (p == Vl[i].size()) {
					if (i == 0) Va[0].push_back(k);
					else Va[1].push_back(j);
				}
			}
		}

		printf("%d %d ", (int)Va[0].size(), Va[1].size());
		for (auto it1 : Va[0]) for (auto it2 : Va[1]) if (it1 <= it2) cnt[sum[it2 - 1] - sum[it1 - 1]]++;

		int mx = 1;
		for (i = 2; i <= 30000; i++) if (cnt[i] > cnt[mx]) mx = i;
		printf("%d %d\n", mx, cnt[mx]);

		Vl[0].clear();
		Vl[1].clear();
		Va[0].clear();
		Va[1].clear();
		memset(cnt, 0, sizeof(cnt));
	}
	return 0;
}
#endif
}
