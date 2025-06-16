using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 16
이름 : 배성훈
내용 : 진수 정렬 (Hard)
    문제번호 : 32287번

    브루트포스, 조합론 문제다.
    m개 중 n개를 선택하는 방법은 m H n = m + n - 1 C n이다

    n과 m은 10 을 넘지 못하므로 커봐야 19C10이다.
    이는 200만을 넘지 못한다.

    그래서 브루트포스로 가능한 조합의 갯수를 찾는다.
    각 경우 i가 선택된 갯수를 chk[i]라 하자.
    그러면 각 경우는 n! / (∏chk[i]!)가지가 있다.

    이렇게 find 조합의 보다 앞에 있는 것들의 갯수를 찾는다.
    이제 find 조합에 대해 find 숫자가 자신보다 앞에 있는 것의 갯수를 찾는다.
    
    가능한 경우는 10!이하이다.
    그래서 브루트포스로 수를 만들면서 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1706
    {

        static void Main(string[] args)
        {

            // 32287번
            long MAX = 10;

            int n, m;
            string findStr;

            Input();

            GetRet();

            void GetRet()
            {

                int[] cnt;
                int[] chk;
                long[] fac;

                long ret = 0;
                long find;

                SetArr();

                DFS_PREV(m - 1, n);

                RevParse();

                DFS_POP(n);

                Console.Write(ret);

                void RevParse()
                {

                    find = 0;
                    for (int i = n - 1; i >= 0; i--)
                    {

                        find = find * 10 + findStr[i] - '0';
                    }
                }

                void DFS_POP(int _dep, long _val = 0)
                {

                    if (_dep == 0)
                    {

                        if (_val < find) ret++;
                        return;
                    }

                    _val *= 10;
                    for (int i = 0; i < m; i++)
                    {

                        if (cnt[i] == 0) continue;
                        cnt[i]--;

                        DFS_POP(_dep - 1, _val + i);
                        cnt[i]++;
                    }
                }

                void DFS_PREV(int _dep, int _cnt, bool _isSmall = false)
                {

                    if (_dep == 0)
                    {

                        chk[_dep] = _cnt;
                        if (_isSmall) ret += Cnt();
                        return;
                    }

                    int e = _isSmall ? _cnt : cnt[_dep];

                    for (int i = 0; i < e; i++, chk[_dep]++)
                    {

                        DFS_PREV(_dep - 1, _cnt - i, true);
                    }

                    DFS_PREV(_dep - 1, _cnt - e, _isSmall | false);

                    chk[_dep] = 0;
                }

                long Cnt()
                {

                    long ret = fac[n];
                    for (int i = 0; i < m; i++)
                    {

                        ret /= fac[chk[i]];
                    }

                    return ret;
                }

                void SetArr()
                {

                    cnt = new int[m];
                    chk = new int[m];
                    fac = new long[MAX + 1];

                    for (int i = 0; i < n; i++)
                    {

                        cnt[findStr[i] - '0']++;
                    }

                    fac[0] = 1;
                    for (int i = 1; i <= MAX; i++)
                    {

                        fac[i] = i * fac[i - 1];
                    }
                }
            }

            void Input()
            {

                string input = Console.ReadLine();
                string[] temp = input.Split();

                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);

                findStr = Console.ReadLine();
            }
        }
    }

#if other
// #include <bits/stdc++.h>

using namespace std;

typedef long long ll;

typedef pair<int, int> pii;

typedef pair<ll, ll> pll;

ll ans = 0, a, b, fact[11], g;

int ch[10];

string s;

void rec(int x, int f) {

  if (x == 0)

    return;

  ll c = 0, r = 1;

  for (char u : s) {

    if (u - '0' == x)

      c++;

    if (u - '0' <= x)

      r *= x;

  }

  for (int i = 0; i < c; i++)

    ans += g / fact[i] / fact[f - i] * r, r /= x;

  g /= fact[c];

  rec(x - 1, f - c);

}

void r(int x){

  if(x==a) return;

  for(int i=0; i<s[x]-'0'; i++){

    if(ch[i]){

      ch[i]--;

      ll t=fact[a-x-1];

      for(int j=0; j<10; j++) t/=fact[ch[j]];

      ans+=t;

      ch[i]++;

    }

  }

  ch[s[x]-'0']--;

  r(x+1);

}

int main() {

  fact[0] = 1;

  for (int i = 1; i <= 10; i++)

    fact[i] = fact[i - 1] * i;

  cin >> a >> b >> s;

  for(char u:s) ch[u-'0']++;

  reverse(s.begin(), s.end());

  g = fact[a];

  rec(b - 1, a);

  r(0);

  cout << ans;

}
#elif other2
// #include <stdio.h>
// #include <string.h>
// #include <algorithm>

long long int fact[110];
int count[110];
char x[110];
long long int check[110][110];
long long int func(int s, int t)
{
    if(t<0) return 0;
    if(s==0) return 1;
    if(check[s][t]!=-1) return check[s][t];

    long long int ans = 0;
    for(int i=0;i<=s;i++)
    {
        long long int val = func(s-i,t-1);
        val *= (fact[s]/fact[s-i]);
        val /= fact[i];
        ans += val;
    }
    return check[s][t] = ans;
}

int main()
{
    fact[0] = 1;
    for(int i=1;i<=11;i++) fact[i] = i*fact[i-1];
    for(int i=0;i<=100;i++) for(int j=0;j<=100;j++) check[i][j] = -1;

    int a,b;
    scanf("%d%d",&a,&b);
    scanf("%s",x+1);
    for(int i=1;i<=a;i++) count[x[i]-'0']++;

    int t = 0;
    long long int s = 1, ans = 0;
    for(int i=b-1;i>=1;i--)
    {
        for(int j=0;j<count[i];j++)
        {
            long long int val = func(a-t-j,i); // (a-t-j)!/n_1!...n_i!, sum n_i = a-t-j
            val *= (fact[a-t]/fact[a-t-j]);
            val /= fact[j];
            val *= s;
            ans += val;
        }
        s *= (fact[a-t]/fact[a-t-count[i]]);
        s /= fact[count[i]];
        t += count[i];
    }
    std::reverse(x+1,x+a+1);

    for(int i=1;i<=a;i++)
    {
        int val = x[i]-'0';
        for(int j=0;j<val;j++)
        {
            if(count[j]>0)
            {
                count[j]--;
                long long int val = fact[a-i];
                for(int k=0;k<b;k++) val /= fact[count[k]];
                ans += val;
                count[j]++;
            }
        }
        count[val]--;
    }

    printf("%lld",ans);
}
#endif
}
