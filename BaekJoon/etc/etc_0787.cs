using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 3
이름 : 배성훈
내용 : 준영이
    문제번호 : 31443번

    그리디, 수학 문제다
    1 * 1 블록으로 나눴을 때, 나머지가 0 또는 2이면
    2 * 1로 하나 자르고 나머지를 3 * 1로 최대한 잘라주면 최대가 된다
    그리고 1인경우면 전체가 1일 때는 그냥 1개 반환한다
    이외는 2 * 1을 2개 해주고 나머지는 3 * 1로 최대한 잘라준다
    이렇게 자른 조각이 최대가된다고 추론했다

    그래서 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0787
    {

        static void Main787(string[] args)
        {

            int[] info;
            int MOD = 1_000_000_007;

            Solve();

            void Solve()
            {

                info = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

                long ret;
                ret = GetRet(info[0] * info[1]);

                Console.Write(ret % MOD);
            }

            long GetRet(int _len)
            {

                if (_len == 1) return 1;
                long ret = 1;
                int two, three;
                three = _len / 3;

                int r = _len % 3;
                if (r == 0) two = 0;
                else if (r == 2) two = 1;
                else
                {

                    three--;
                    two = 2;
                }

                ret = GetPow(3, three) * GetPow(2, two);
                return ret;
            }

            long GetPow(long _a, int _exp)
            {

                long ret = 1;
                while(_exp > 0)
                {

                    if ((_exp & 1) == 1) ret = (ret * _a) % MOD;

                    _a = (_a * _a) % MOD;
                    _exp >>= 1;
                }

                return ret;
            }
        }
    }

#if other
// #include <stdio.h>
int main() {
    long long int a, b;
    scanf("%lld %lld", &a, &b);
    if(a*b==1) printf("1");
    else{
        long long int two, three, ans=1;
        if(a*b%3==0) two=0;
        if(a*b%3==1) two=2;
        if(a*b%3==2) two=1;
        three=(a*b-two*2)/3;
        for(int i=0; i<three; i++){
            ans*=3;
            ans%=1000000007;
        }
        for(int i=0; i<two; i++){
            ans*=2;
            ans%=1000000007;
        }
        printf("%lld", ans);
    }
}
#elif other2
import java.io.*;
import java.util.*;

public class Main {
	public static final int MOD = 1000000007;

	public static void main(String[] args) throws IOException {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		StringTokenizer st = new StringTokenizer(br.readLine());
		int N = Integer.parseInt(st.nextToken());
		int M = Integer.parseInt(st.nextToken());

		if (N == 1 && M == 1) {
			System.out.println(1);
			br.close();
			return;
		}
		
		int threeCnt_1 = (N / 3) * M + (N % 3) * (M / 3);
		int twoCnt_1 = 0;
		if (M % 3 == 1) {
			if (N % 3 == 1) {
				threeCnt_1 -= (N % 3);
				twoCnt_1 += (N % 3) * 2;
			} else if (N % 3 == 2) {
				twoCnt_1 += 1;
			}
		} else if (M % 3 == 2) {
			twoCnt_1 += (N % 3);
		}

		int threeCnt_2 = (M / 3) * N + (M % 3) * (N / 3);
		int twoCnt_2 = 0;
		if (N % 3 == 1) {
			if (M % 3 == 1) {
				threeCnt_2 -= (M % 3);
				twoCnt_2 += (M % 3) * 2;
			} else if (M % 3 == 2) {
				twoCnt_2 += 1;
			}
		} else if (N % 3 == 2) {
			twoCnt_2 += (M % 3);
		}

		int threeCnt = 0;
		int twoCnt = 0;
		if (threeCnt_1 > threeCnt_2) {
			threeCnt = threeCnt_1;
			twoCnt = twoCnt_1;
		} else if (threeCnt_1 < threeCnt_2) {
			threeCnt = threeCnt_2;
			twoCnt = twoCnt_2;
		} else {
			threeCnt = threeCnt_1;
			twoCnt = Math.max(twoCnt_1, twoCnt_2);
		}

		long answer = 1;
		for (int i = 0; i < threeCnt; i++) {
			answer *= 3;
			answer %= MOD;
		}
		for (int i = 0; i < twoCnt; i++) {
			answer *= 2;
			answer %= MOD;
		}

		System.out.println(answer);
		br.close();
	}
}
#endif
}
