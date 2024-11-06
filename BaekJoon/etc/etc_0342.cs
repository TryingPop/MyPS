using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 24
이름 : 배성훈
내용 : 급상승
    문제번호 : 23978번

    이분 탐색 문제다
    오버플로우로 한 번 틀렸다

    주된 아이디어는 다음과 같다
    설정할 값을 찾는데 이분 탐색을 썼다
    그러면 해당 값을 넣었을 때, 조건을 만족하면 값을 줄여본다
    불만족하면 값을 늘린다

    값은 시그마 형식이고 이분 탐색 한 번에 많으면 100만번 가까이 탐색해야하기에 
    누적합 계산 공식을 써서 했다

    아래 코드로 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0342
    {

        static void Main342(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            long k = ReadLong();

            int[] day = new int[n + 1];
            for (int i = 0; i < n; i++)
            {

                day[i] = ReadInt();
            }

            sr.Close();

            long l = 0;
            // 처음에 무턱대로 k로하다가
            // k가 10^18 까지 들어오고,
            // 누적합은 k^2까지 연산하기에
            // 40억만 넘어도 long에서 연산 중에
            // 16 * 10^18이므로 오버플로우가 뜬다
            // 이를 간과해서 k를 넣다 오버플로우로 1번 틀렸다
            // 이후는 20억에는 연산중에 많아야 4 * 10^18이고, 이는 long 범위 안이다
            // 그리고 연산이 끝났을 때, 2 * 10^18내외이므로 10^18보다는 확실히 크기에
            // k의 모든 범위를 이상없이 탐색할 수 있다
            long r = 2_000_000_000;

            while (l <= r)
            {

                long mid = (l + r) / 2;

                long calc = 0;
                for (int i = 0; i < n; i++)
                {

                    calc += CalcMoney(day[i], day[i + 1], mid);
                }

                if (calc < k) l = mid + 1;
                else r = mid - 1;
            }

            Console.WriteLine(r + 1);

            long CalcMoney(int _s, int _e, long _k)
            {

                long start = _k;
                long len = _k;
                if (_e != 0)
                {

                    len = _e - _s;
                    len = _k < len ? _k : len;
                }

                long end = start - len + 1;
                long ret = (start + end) * len;
                ret /= 2;
                return ret;
            }

            long ReadLong()
            {

                int c;
                long ret = 0;

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }

            int ReadInt()
            {

                int c;
                int ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
public class Main {
	public static void main(String[] args) throws Exception {
		StringBuilder sb = new StringBuilder();
		int N = (int) readLong();
		long K = readLong();
		long[] arr = new long[N];
		for (int i = 0; i < N; i++) {
			arr[i] = readLong();
		}
		long s = 1;
		long e = 2000000000L;
		long m = 0;
		while (s != e) {
			m = (s + e) >> 1;
			
			long res = (m * (m + 1)) >> 1;
			for (int i = 1; i < N; i++) {
				long day = arr[i] - arr[i - 1];

				if (day >= m) {
					res += (m * (m + 1)) >> 1;
				} else {
					long t = m - day;
					res += (m * (m + 1)) >> 1;
					res -= (t * (t + 1)) >> 1;
				}

				if (res >= K) {
					break;
				}
			}

			if (res >= K) {
				e = m;
			} else {
				s = m + 1;
			}
		}

		System.out.println(e);
	}

	public static long readLong() throws Exception {
		long val = 0;
		long c = System.in.read();
		while (c <= ' ') {
			c = System.in.read();
		}
		boolean flag = (c == '-');
		if (flag)
			c = System.in.read();
		do {
			val = 10 * val + c - 48;
		} while ((c = System.in.read()) >= 48 && c <= 57);

		if (flag)
			return -val;
		return val;
	}
}
#endif
}
