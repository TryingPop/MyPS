using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 21
이름 : 배성훈
내용 : Flight Turbulence
    문제번호 : 17848번

    구현, 그래프 이론 문제다
    원하는 자리에 앉아 있으면 이동 안해도 된다
    
    아이디어는 다음과 같다
    m번째 자리는 비워져 있다

    몇 명의 사람이 이동해야 m번 의자를 원하는 사람이 나오는지 확인하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0897
    {

        static void Main897(string[] args)
        {

            StreamReader sr;
            int n, m;
            int[] arr;
            bool[] set;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                // 초기 자기자신 이동
                int ret = 1;

                int chk = m;
                // m 번 의자를 원하는 사람?
                while (arr[chk] != m)
                {

                    // 원하지 않으므로 해당 사람 이동시킨다
                    ret++;
                    chk = arr[chk];
                }

                // 만약 자기자신이 원하는 자리에 앉아 있으면
                // 이동 자체를 할 필요가 없다
                if (ret == 1) ret = 0;
                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt() - 1;

                arr = new int[n];
                set = new bool[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt() - 1;
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
#if other
// #include <stdio.h>

int a[256];

int main(void) {
    int n, m, t, res = 0;
    scanf("%d %d", &n, &m);
    for (int i = 1; i <= n; i++) {
        scanf("%d", &a[i]);
    }
    while (a[m] != m) {
        t = a[m];
        a[m] = m;
        m = t;
        res++;
    }
    printf("%d", res);
    return 0;
}
#elif other2
// #include <bits/stdc++.h>
using namespace std;

int main()
{
    ios::sync_with_stdio(0); cin.tie(0);
    int n,m; cin >> n >> m;
    vector<int> v(n+1);
    for(int i=1;i<=n;i++) cin >> v[i];
    int cnt=0;
    while(v[m]!=m) swap(m,v[m]), ++cnt;
    cout << cnt;
    return 0;
}
#endif
}
