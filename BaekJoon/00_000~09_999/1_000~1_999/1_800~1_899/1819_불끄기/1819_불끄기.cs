using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 11
이름 : 배성훈
내용 : 불끄기
    문제번호 : 1819번

    구현, dp, 비트마스킹 문제다.
    역추적을 복잡하게 구현했다;

    아이디어는 다음과 같다.
    t개의 상태만 기록한다.
    해당 상태에서 버튼을 누르는 경우와 누르지 않는 경우를 확인하고
    끝에 점을 제거한다. 켜져잇으면 1누적 없으면 1감소한다.
    이렇게 끝까지 l까지 확인해서 가장 작게 켜진 경우를 찾는다.
    l일 때는 나머지 켜진 갯수를 누적해준다.

    그리고 가장 적은 경우를 찾는다.
    이제 이전 경로들을 확인하며 언제 켜지는지 확인했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1178
    {

        static void Main1178(string[] args)
        {

            int INF = 1_000;
            int l, t;
            string bulb;
            int change;

            (int val, int prev)[][] dp;

            Solve();
            void Solve()
            {

                Input();

                SetDp();

                GetRet();
            }

            void GetRet()
            {

                int init = 0;
                for (int i = 0; i < t - 1; i++)
                {

                    if (bulb[i] == '1') init |= 1 << t;
                    init >>= 1;
                }

                dp[t - 1][init] = (0, -1);
                for (int i = t - 1; i < l; i++)
                {

                    for (int j = 0; j < dp[i].Length; j++)
                    {

                        if (dp[i][j].val == INF) continue;
                        int next = j >> 1;
                        if (bulb[i] == '1') next |= 1 << (t - 1);

                        if (dp[i][j].val + (next & 1) < dp[i + 1][next].val)
                        {

                            dp[i + 1][next].val = dp[i][j].val + (next & 1);
                            dp[i + 1][next].prev = j;
                        }

                        next ^= change;
                        if (dp[i][j].val + (next & 1) < dp[i + 1][next].val)
                        {

                            dp[i + 1][next].val = dp[i][j].val + (next & 1);
                            dp[i + 1][next].prev = j;
                        }
                    }
                }

                int min = INF;
                int chk = -1;
                for (int i = 0; i < dp[l].Length; i++)
                {

                    if (dp[l][i].val == INF) continue;
                    int cnt = 0;
                    for (int j = 0; j < t; j++)
                    {

                        if (((1 << j) & i) == 0) continue;
                        cnt++;
                    }

                    dp[l][i].val += cnt;
                    if (min < dp[l][i].val) continue;
                    min = dp[l][i].val;
                    chk = i;
                }

                for (int idx = l; chk != -1; idx--)
                {

                    dp[idx][chk].val = -1;
                    chk = dp[idx][chk].prev;
                }

                int[] btn = new int[l];
                int len = 0;
                for (int i = t - 1; i < l; i++)
                {

                    for (int j = 0; j < dp[i].Length; j++)
                    {

                        if (dp[i][j].val != -1) continue;
                        int next = j >> 1;
                        if (bulb[i] == '1') next |= 1 << (t - 1);
                        if (dp[i + 1][next].val == -1) break;
                        next ^= change;
                        if (dp[i + 1][next].val == -1)
                            btn[len++] = i - t + 2;
                    }
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                sw.Write($"{len}\n");
                for (int i = 0; i < len; i++)
                {

                    sw.Write($"{btn[i]}\n");
                }

                sw.Close();
            }

            void SetDp()
            {

                dp = new (int val, int prev)[l + 1][];

                for (int i = 0; i <= l; i++)
                {

                    dp[i] = new (int val, int prev)[1 << t];
                    Array.Fill(dp[i], (INF, -1));
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string[] temp = sr.ReadLine().Split();
                l = int.Parse(temp[0]);
                t = int.Parse(temp[1]);

                bulb = sr.ReadLine();
                string chk = sr.ReadLine();
                change = 0;
                for (int i = 0; i < t; i++)
                {

                    if (chk[i] == '0') continue;
                    change |= 1 << i;
                }

                sr.Close();
            }
        }
    }

#if other
// #include <cstdio>
// #include <algorithm>
// #include <vector>
// #include <stack>
// #include <queue>
// #include <utility>
// #include <cstring>

using namespace std;

int n,m;
char str[100],t[10];
int cache[100][1<<7],nxt[100][1<<7];

int DP(int idx,int s) {
	int &ret=cache[idx][s];
	if(ret!=-1) return ret;
	if(idx==n) return ret=0;

	int c=str[idx]-'0';
	for(int i=0;i<m-1 ;i++) {
		c ^= ((s>>i)&1)&(t[i+1]);
	}
	
	ret=DP(idx+1,(s<<1) & ((1<<m)-1))+c;
	nxt[idx][s]=0;
	
	if(idx+m-1<n) {
		int v=DP(idx+1,((s<<1) & ((1<<m)-1))|1)+(c^(t[0]-'0'));
		if(ret>v) {
			ret=v;
			nxt[idx][s]=1;
		}
	}
	return ret;
}
void trace(int idx,int s,int cnt) {
	if(idx==n) printf("%d\n",cnt);
	else {
		trace(idx+1,((s<<1) & ((1<<m)-1))|nxt[idx][s],cnt+nxt[idx][s]);
		if(nxt[idx][s]==1) printf("%d\n",idx+1);
	}
}

int main() {
	scanf("%d%d",&n,&m);
	scanf("%s%s",str,t);
	
	memset(cache,-1,sizeof(cache));
	DP(0,0);
	trace(0,0,0);
	
	return 0;
}
#endif
}
