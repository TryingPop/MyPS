using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 23
이름 : 배성훈
내용 : 규칙적인 보스돌이
    문제번호 : 29792번

    배낭 문제다.
    먼저 각 캐릭터가 벌 수 있는 최대 메소를 찾으면 된다.

    시간이 900초이고, 보스는 13마리를 넘지 않기에
    벌 수 있는 최대 메소는 배낭 형식으로 찾아주면 된다.

    그리고 n명 중 m명을 골라 최대가 되게 구현하니 시간초과 났다.
    그래서 dp로 접근했다. dp[i][j] = val를 i번호까지 선택했고 j 명을 선택했을 때 최댓값 val로 dp를 설정했다.
    그리고 dp에서 최댓값을 찾으면 된다.

    조금 지나 생각해보니 돈을 많이 버는 m명을 택하면 되겠네 생각했다.
    그래서 그냥 정렬해서 m명을 택했다.

    다른 사람의 풀이를 보니 모든 사람의 돈을 확인할 필요가 없었다...
    그리디로 딜이 높으면 벌 수 있는 메소가 더 많음은 자명하다.
    그리고 마찬가지로 돈의 양은 양수이므로 돈의 양이 큰 사람 m명을 택하면 최대가 보장된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1569
    {

        static void Main1569(string[] args)
        {

            int n, m, k;
            long[] dmg;
            int[] money;
            (long hp, int money)[] boss;
            Input();

            SetArr();

            GetRet();

            void GetRet()
            {

#if SLOW_SLOW
                int[] cur = new int[m + 1], next = new int[m + 1];

                for (int i = 0; i < n; i++)
                {

                    for (int j = 1; j <= m; j++)
                    {

                        next[j] = Math.Max(cur[j], cur[j - 1] + money[i]);
                    }

                    for (int j = 0; j <= m; j++)
                    {

                        cur[j] = next[j];
                    }
                }

                int ret = 0;

                for (int j = 0; j <= m; j++)
                {

                    ret = Math.Max(ret, cur[j]);
                }
#else

                Array.Sort(money, (x, y) => y.CompareTo(x));

                int ret = 0;
                for (int i = 0; i < m; i++)
                {

                    ret += money[i];
                }
#endif

                Console.Write(ret);
            }

            void SetArr()
            {

                int MAX_TIME = 900;
                int[] time = new int[MAX_TIME + 1];
                Array.Fill(time, -1);

                money = new int[n];
                for (int i = 0; i < n; i++)
                {

                    time[0] = 0;

                    for (int j = 0; j < k; j++)
                    {

                        long t = boss[j].hp / dmg[i];
                        if (boss[j].hp % dmg[i] > 0) t++;

                        if (t > MAX_TIME) continue;

                        for (long s = MAX_TIME - t; s >= 0; s--)
                        {

                            if (time[s] == -1) continue;
                            time[s + t] = Math.Max(time[s + t], time[s] + boss[j].money);
                        }
                    }

                    int max = 0;
                    for (int j = 0; j <= MAX_TIME; j++)
                    {

                        max = Math.Max(max, time[j]);
                        time[j] = -1;
                    }

                    money[i] = max;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();
                k = ReadInt();

                dmg = new long[n];
                for (int i = 0; i < n; i++)
                {

                    dmg[i] = ReadLong();
                }

                boss = new (long hp, int money)[k];
                for (int i = 0; i < k; i++)
                {

                    boss[i] = (ReadLong(), ReadInt());
                }

                long ReadLong()
                {

                    long ret = 0;

                    while (TryReadLong()) ;
                    return ret;

                    bool TryReadLong()
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
// #include <stdio.h>
// #include <string.h>
// #include <vector>
// #include <algorithm>
// #define TMAX 900
using namespace std;

typedef long long INT;

int N, M, K;
INT d, p, q;
vector< INT > D, P, Q;

INT cur, curMax, ret;
INT dp[TMAX+1];

int main() {
    scanf("%d %d %d", &N, &M, &K);
    for(int i=1;i<=N;i++) {
        scanf("%lld", &d);
        D.push_back(d);
    }
    for(int i=1;i<=K;i++) {
        scanf("%lld %lld", &p, &q);

        P.push_back(p);
        Q.push_back(q);
    }

    sort(D.rbegin(), D.rend());

    for(int i=0;i<M;i++) {
        curMax = 0;
        memset(dp, 0, sizeof(dp));

        for(int j=0;j<K;j++) {
            cur = (P[j] + D[i]-1) / D[i];
            for(int k=TMAX;k>=cur;k--) {
                dp[k] = max(dp[k], dp[k-cur] + Q[j]);

                curMax = max(curMax, dp[k]);
            }
        }

        ret += curMax;
    }

    printf("%lld", ret);
}
#endif
}
