using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 29
이름 : 배성훈
내용 : 비트와 가희
    문제번호 : 16153번

    해당 사이트를 참고했다
    https://blog.naver.com/rdd573/221360289763

    아직은 플레티넘 난이도는 비계 영역이라 풀이를 봐도 dp가 무엇인지 이해가 잘 안된다;
    중간에서 만나기 아이디어가 어떻게 쓰였는지 확인하기 위해 따라 풀었다

    83%에서 DFS_BIT 부분에서 bitIdx = -1로 가서 여러 번 틀렸다
    추후에 다시 풀어봐야겠다
*/

namespace BaekJoon.etc
{
    internal class etc_0134
    {

        static void Main134(string[] args)
        {

            string[] temp = Console.ReadLine().Split(' ');

            int len = int.Parse(temp[0]);
            int a = int.Parse(temp[1]);
            int b = int.Parse(temp[2]);

            int[] bit = new int[31];
            int chk = 0;

            for (int i = 0; i < len; i++)
            {

                int t = int.Parse(Console.ReadLine());

                bit[t] = 1;
                chk |= 1 << t;
            }

            int ret = 0;
            if (a > 10_000)
            {

                // a가 충분히 크면 a의 배수에 한해서 완전 탐색
                // i를 long으로 해줘야한다 안하면 23%에서 오버플로우로 틀리는거 같다
                for (long i = a; i <= b; i += a)
                {

                    if ((i & (long)chk) == (long)chk) ret++;
                }

                Console.WriteLine(ret);
                return;
            }
            else if (len == 0)
            {

                // 꼭 넣어야할 비트가 없는 경우
                ret = b / a;
                Console.WriteLine(ret);
                return;
            }

            // 1 << 0 부터 1 << idx 까지 누적해서 더한값
            int[] max = new int[31];
            // 1 << 0 부터 1 << idx까지 포함된 것들을 더한 값
            int[] min = new int[31];

            max[0] = 1 << 0;
            min[0] = (1 << 0) * bit[0];
            for (int i = 1; i < 31; i++)
            {

                max[i] = 1 << i;
                max[i] += max[i - 1];

                min[i] = (1 << i) * bit[i];
                min[i] += min[i - 1];
            }

            // bitIdx를 포함한?
            int[][] dp = new int[31][];
            for (int i = 0; i < 31; i++)
            {

                dp[i] = new int[10_001];
                Array.Fill(dp[i], -1);
            }

            ret += DFS_BIT(30, 0, min, max, a, b, dp, bit);

            Console.WriteLine(ret);
        }

        static int DFS_BIT(int _bitIdx, int _curVal, int[] _min, int[] _max, int _a, int _b, int[][] _dp, int[] _bit)
        {

            if (_bitIdx == -1)
            {

                if (_curVal > _b) return 0;
                return DFS_DP(_bitIdx, _curVal % _a, _dp, _a, _bit);
            }
            // b이하인 bitIdx 를 포함한 값은 없다
            // long은 오버플로우 방지!
            if (_curVal + (long)_min[_bitIdx] > _b) return 0;
            // 최대 값이 b이하이므로 메모 가능
            // long은 오버플로우 방지
            if (_curVal + (long)_max[_bitIdx] <= _b) return DFS_DP(_bitIdx, _curVal % _a, _dp, _a, _bit);

            // bitIdx의 값을 추가해서 계산 한다
            int sum = DFS_BIT(_bitIdx - 1, _curVal + (1 << _bitIdx), _min, _max, _a, _b, _dp, _bit);
            // bitIdx의 값을 제외할 수 있으면 제외하고 연산한다
            if (_bit[_bitIdx] == 0) sum += DFS_BIT(_bitIdx - 1, _curVal, _min, _max, _a, _b, _dp, _bit);

            return sum;
        }

        static int DFS_DP(int _bitIdx, int _mo, int[][] _dp, int _a, int[] _bit)
        {

            if (_bitIdx == -1)
            {

                // 나눠 떨어지는 경우 a로 1
                if (_mo == 0) return 1;
                // a로 안나눠 떨어진다
                return 0;
            }

            if (_dp[_bitIdx][_mo] != -1) return _dp[_bitIdx][_mo];

            // bitIdx 추가한 경우로 간다
            int sum = DFS_DP(_bitIdx - 1, (_mo + (1 << _bitIdx)) % _a, _dp, _a, _bit);
            // bitIdx를 제외할 수 있으면 제외하고 경우의 수를 찾는다
            if (_bit[_bitIdx] == 0) sum += DFS_DP(_bitIdx - 1, _mo, _dp, _a, _bit);

            return _dp[_bitIdx][_mo] = sum;
        }
    }

#if other
// #include <stdio.h>

int main() {
	int mod[31][4196] = {0, }, mc=1;
	int range[31], bit[32];
	int n, a, b, i, j, k, x=0, ans=0;
	long long s;
	
	scanf("%d%d%d", &n, &a, &b);
	while (n--) {
		scanf("%d", &i);
		x += 1 << i;
	}
	
	if (x == 0) {
		ans = b / a;
	} else if (a > 4196) {
		for (i=0, k=a; i<b/a; i++, k+=a)
			ans += (x & k) == x;
	} else {
		range[0] = x;
		mod[0][x % a]++;
		bit[0] = 0;
		
		for (i=0; i<31; i++) {
			if ((x & (1 << i)) == 0) {
				x += 1 << i;
				range[mc] = x;
				bit[mc] = i;
				k = (1 << i) % a;
				
				for (j=0; j<a; j++) {
					mod[mc][j] += mod[mc-1][j];
					mod[mc][(j + k) % a] += mod[mc-1][j];
				}
				mc++;
			}
		}
		bit[mc] = i;
		
		for (i=mc-1, s=0; i>=0; i--) {
			if (b >= range[i] + s) {
				ans += mod[i][(a - s % a) % a];
				s += 1LL << (bit[i+1]);
			}
		}
	}
	
	printf("%d", ans);
}
#elif other2
// #include <bits/stdc++.h>
using namespace std;

const int N = 31, K = 205;

int n, a, b, c, r, d[N][K][2];

int main(){
    scanf("%d%d%d", &n, &a, &b);
    if(!n){ printf("%d\n", b / a); return 0; }
    for(int x; n--; ){ scanf("%d", &x); c |= (1 << x); }
    if(b < c){ puts("0"); return 0; }
    if(a >= K){
        for(long long i = a; i <= b; i += a) if((i & c) == c) r++;
        printf("%d\n", r);
        return 0;
    }
    int i; for(i = N - 1; !(b & (1 << i)); i--);
    d[i][(1 << i) % a][1] = 1;
    if(!(c & (1 << i))) d[i][0][0] = 1;
    for(i--; i >= 0; i--){
        for(int j = 0; j < a; j++){
            if(c & (1 << i)){
                if(b & (1 << i)) d[i][(j + (1 << i)) % a][1] += d[i + 1][j][1];
                d[i][(j + (1 << i)) % a][0] += d[i + 1][j][0];
            }
            else{
                d[i][(j + (b & (1 << i))) % a][1] += d[i + 1][j][1];
                d[i][(j + (1 << i)) % a][0] += d[i + 1][j][0];
                d[i][j][0] += d[i + 1][j][0];
                if(b & (1 << i)) d[i][j][0] += d[i + 1][j][1];
            }
        }
    }
    printf("%d\n", d[0][0][0] + d[0][0][1]);
}
#elif other3
// 이분이 중간에서 만나기 아이디어!
// #include<bits/stdc++.h>
using namespace std;

int ans = 0;
int cnt[1 << 15];

int main(){
    int n, a, b;
    scanf("%d%d%d", &n, &a, &b);
    int one = 0, two = 0;
    for(int i=0;i<n;i++){
        int x;
        scanf("%d", &x);
        if(x < 15) two += (1 << x);
        else one += (1 << (x-15));
    }
    for(int i=0;i<(1 << 15);i++){
        if((two & i) == two) cnt[i] = 1;
        if(i >= a) cnt[i] += cnt[i-a];
    }
    for(int i=0;i<(1 << 16);i++){
        if(b < i * (1 << 15)) break;
        int ss = i * (1 << 15);
        int ee = min(ss + ((1 << 15) - 1), b);
        ss = max(ss, 1);
        int gae = (ee / a) - ((ss-1)/a);
        if(gae > 0 && (i & one) == one){
            ans += cnt[((ee / a) * a) % (1 << 15)];
            if(i == 0 && two == 0) ans--;
        }
    }
    printf("%d\n", ans);
}


#endif
}
