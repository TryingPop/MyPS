using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 8
이름 : 배성훈
내용 : 정수
    문제번호 : 1040번

    dp, 그리디, 비트마스킹 문제다.
    아이디어는 다음과 같다.
    큰 자리부터 숫자를 크게 0 ~ 9까지 올려본다.
    그렇게 끝자리까지 가서 k개의 숫자를 사용했는지 확인한다.
    k개의 숫자를 사용했고 기존 수보다 크다면 종료한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1144
    {

        static void Main1144(string[] args)
        {

            
            int MAX = 19;
            long n;
            int k;
            long[][][] dp;
            long[] tenPow;
            int[] num;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = long.Parse(temp[0]) - 1;
                k = int.Parse(temp[1]);
                dp = new long[MAX + 1][][];
                for (int i = 0; i <= MAX; i++)
                {

                    dp[i] = new long[1 << 10][];
                    int len = 1 << 10;
                    for (int j = 0; j < len; j++)
                    {

                        dp[i][j] = new long[2];
                        Array.Fill(dp[i][j], -1);
                    }
                }

                // tenPow[i] = 10^(idx - 1)이다.
                tenPow = new long[MAX + 1];
                tenPow[1] = 1;
                for (int i = 2; i <= MAX; i++)
                {

                    tenPow[i] = tenPow[i - 1] * 10;
                }

                // n의 숫자
                num = new int[MAX + 1];
                long chk = n;
                for (int i = 1; i <= MAX; i++)
                {

                    num[i] = (int)(chk % 10);
                    chk /= 10;
                }
            }

            void GetRet()
            {

                Console.Write(DFS(MAX, 0, false, true));

                long DFS(int _digit, int _state, bool _isBig, bool _readZero)
                {
                    ref long ret = ref dp[_digit][_state][_isBig ? 1 : 0];  
                    if (ret != -1) return ret;

                    if (_digit == 0)
                    {

                        // 모든 수를 채워넣었고, k개 사용했는지 확인 한다.
                        if (!_isBig) return ret = -2;
                        int cnt = 0;

                        for (int i = 0; i < 10; i++)
                        {

                            if ((_state & (1 << i)) > 0) cnt++;
                        }

                        if (cnt != k) return ret = -2;
                        else return ret = 0;
                    }

                    ret = 0;
                    if (_readZero && num[_digit] == 0)
                    {

                        long next = DFS(_digit - 1, _state, _isBig, _readZero);
                        // 채워 넣을 수 있는 경우다.
                        if (next >= 0) return ret = next;
                    }

                    // 읽기 시작하는 자리 설정
                    int s;
                    if (_isBig) s = 0;
                    else if (num[_digit] == 0 && _readZero) s = 1;
                    else s = num[_digit];
                    for (int i = s; i < 10; i++)
                    {

                        long next = DFS(_digit - 1, _state | (1 << i), _isBig || (i > num[_digit]), _readZero && i == 0);
                        // 채워넣을 수 있는 경우 반환
                        if (next >= 0)
                            return ret = next + tenPow[_digit] * i;
                    }

                    return ret = -2;
                } 
            }
        }
    }

#if other
// #include <stdio.h>

typedef unsigned long long LL;

LL N; int K;
LL P[20], ans = 18446744073709551615LL;

void solve(int i, LL n, LL fn) {
	bool D[10] = { 0, }; int C = 0;
	while (fn) {
		if (D[fn % 10] == 0) D[fn % 10] = 1, C++;
		fn /= 10;
	}
	if (C > K) return;
	if (C == K) {
		for (int f = 0; f < 10; f++) {
			if (D[f]) {
				for (int k = i - 1; k >= 0; k--) n += f * P[k];
				break;
			}
		}
	}
	else if (K - C <= i) {
		int add = K - C;
		if (D[0] == 0) add--;
		int p = 1;
		while (add >= 1) {
			while (p <= 9 && D[p] == 1) p++;
			n += (p++) * P[--add];
		}
	}
	else return;
	if (ans > n) ans = n;
}

int main() {
	scanf("%llu%d", &N, &K);
	int L = 0; LL x = N;
	P[0] = 1;
	for (int i = 1; i <= 19; i++) P[i] = P[i - 1] * 10LL;
	while (x) x /= 10, L++;
	int lim = (L > K - 1) ? L : (K - 1);
	for (int i = -1; i <= lim; i++) {
		LL n, fn;
		if (i < 0) solve(i, N, N);
		else {
			for (int y = N / P[i] % 10 + 1; y <= 9; y++) {
				n = ((N / P[i]) / 10 * 10 + y) * P[i];
				fn = n / P[i];
				solve(i, n, fn);
			}
		}
	}
	printf("%llu", ans);
}
#elif other2
// #include <stdio.h>
// #include <string.h>
// #include <stdlib.h>

char str[20], made[21];
int k, len, it=0;

char search(int vst, int index, int diff, int changed){
if(index==len){
printf("%s\n", made);
exit(0);
}
for(int i=(changed?0:str[index]-48);i<10;i++){
if(!(vst&1<<i)&&diff==k){
changed=1;
continue;
}else if(len-index==k-diff&&vst&1<<i){
changed=1;
continue;
}
made[index]=i+48;
if(vst&1<<i)
search(vst, index+1, diff, changed);
else
search(vst|1<<i, index+1, diff+1, changed);
if(!changed)
changed=1;
}
return -1;
}

void error(){
str[0]='1';
if(k>len)
len=k;
else
len++;
for(int i=1;i<=len;i++)
str[i]='0';
search(1, 0, 1, 0);
exit(0);
}

int main(){
scanf("%s%d", str, &k);
len=strlen(str);
if(len<k)
error();
if(search(0, 0, 0, 0)==-1)
error();
return 0;
}
#endif
}
