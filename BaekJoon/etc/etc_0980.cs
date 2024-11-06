using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 20
이름 : 배성훈
내용 : Judge’s Mistake
    문제번호 : 16109번

    그래프 이론, 그리디 알고리즘 문제다
    MST로 만드는 간선들만 찾고 제출하니 3번 틀렸다
    이후 간선에 해당하는 노드들 부터 계산하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0980
    {

        static void Main980(string[] args)
        {

            StreamReader sr;
            int n, m;
            int[] cnt;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int chk = (m << 1) - n;
                for (int i = n; i >= 1; i--)
                {

                    cnt[i]--;

                    int min = Math.Min(cnt[i], chk);
                    chk -= min;
                    cnt[i] -= min;
                }

                int k = n - 1;
                long ret = 0;

                for (long i = 1; i <= 100_000; i++)
                {

                    int min = Math.Min(k, cnt[i]);
                    k -= min;
                    ret += min * i;

                    if (k == 0) break;
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                cnt = new int[100_001];

                int len = 3 * m;
                for (int i = 0; i < len; i++)
                {

                    int num = ReadInt();
                    cnt[num]++;
                }

                sr.Close();
            }

            bool TryReadInt(out int _ret)
            {

                _ret = 0;
                int c = sr.Read();
                if (c == ' ' || c == '\n' || c == -1) return true;

                _ret = c - '0';
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    _ret = _ret * 10 + c - '0';
                }

                return false;
            }

            int ReadInt()
            {

                int ret;

                while (TryReadInt(out ret)) { };
                return ret;
            }
        }
    }

#if other
// #include <bits/stdc++.h>

using namespace std;
typedef long long ll;
typedef pair<int, int> pii;
typedef pair<ll, ll> pll;

int main() {
    ios::sync_with_stdio(0);
    cin.tie(0);

    int a,b;
    cin>>a>>b;

    int k[100001];
    memset(k,0,sizeof(k));
    for(int i=0; i<3*b; i++){
        int x;
        cin>>x;
        k[x]++;
    }
    for(int i=1; i<=a; i++) k[i]--;
    int n=0,v=a;
    while(n<2*b-a){
        while(k[v]==0) v--;
        k[v]--,n++;
    }
    ll ans=0;
    v=1,n=0;
    while(n<a-1){
        while(k[v]==0) v++;
        ans+=v;
        k[v]--;
        n++;
    }
    cout<<ans;
}
#endif
}
