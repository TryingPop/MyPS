using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 31
이름 : 배성훈
내용 : 벌레컷
    문제번호 : 27651번

    이분탐색, 누적합, 두 포인터 문제다
    끝나고나서 힌트를 보니 힌트대로 풀었다

    아이디어는 다음과 같다
    머리, 몸통, 꼬리로 구분해야한다
    그리고 각 구간의 총합을 비교해야하기에 누적합인 sum 배열을 썼다
    배열의 크기가 10만까지이고, 범위는 100만까지이므로 long 자료형을 썼다

    그리고, 머리와 꼬리 경계를 두 포인터로 설정했다
    경계는 머리 값에 따라 머리 < 꼬리 가 되는 최소값을 꼬리 끝으로 했다
    이후에 꼬리 < 몸통이 되는 몸통의 수는 이분탐색으로 찾아갔다

    해당 아이디어로 제출하니 176ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0411
    {

        static void Main411(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();

            long[] sum = new long[n + 1];
            for (int i = 1; i <= n; i++)
            {

                // 누적합
                sum[i] = ReadInt();
                sum[i] += sum[i - 1];
            }

            sr.Close();

            long head;
            long body;
            long tail;

            int h = 1;
            int t = n - 1;

            long ret = 0;
            while(h < t)
            {

                head = sum[h];
                tail = sum[n] - sum[t];

                // 머리 < 꼬리인
                // 몸통과 꼬리 경계점 찾기
                while(head >= tail)
                {

                    tail = sum[n] - sum[--t];
                }

                int l = h;
                int r = t;

                // 몸통 개수 세기!
                while(l <= r)
                {

                    int mid = (l + r) / 2;

                    body = sum[mid] - sum[h];
                    tail = sum[n] - sum[mid];

                    // body 크기를 줄여보기!
                    if (tail < body) r = mid - 1;
                    // body 크기를 늘려야한다
                    else l = mid + 1;
                }

                int chk = t - r;
                // 몸통 개수가 0개인 경우
                // 뒤는 항상 0개이므로 탈출!
                if (chk <= 0) break;
                ret += chk;
                h++;
            }

            Console.WriteLine(ret);

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
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.StringTokenizer;

public class Main {
	static int N;
	static long[] A;
	static long[] pSum;

	private static long getRangePSum(int left, int right){
		return pSum[right] - pSum[left - 1];
	}
    
	private static int upperBound(int start, int end, long key){
		int ret = end;
		int lo = start - 1, hi = end;
		while(lo <= hi){
			int mid = (lo + hi) / 2;
			if(getRangePSum(start, mid) <= key){
				ret = mid;
				lo = mid + 1;
			}
			else hi = mid - 1;
		}
		return ret;
	}

	private static long solve() {
		long ret = 0;
		int left = 1, right = N - 1; // 머리: [1, left], 가슴: [left + 1, right], 배: [right + 1, N]
		while (left < right) {
			long headSum = getRangePSum(1, left);
			long midSum = getRangePSum(left + 1, right);
			long tailSum = getRangePSum(right + 1, N);
			if(midSum <= headSum || midSum <= tailSum) break;
            
			if(tailSum <= headSum) right--;
			else { // headSum은 고정, midSum > tailSum 개수 구하기
				long key = getRangePSum(left + 1, N) / 2;
				int index = upperBound(left + 1, N, key);
				if(index < right) ret += right - index;
				left++;
			}
		}
		return ret;
	}

	public static void main(String[] args) throws IOException {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		StringTokenizer st = new StringTokenizer(br.readLine());
		N = Integer.parseInt(st.nextToken());

		A = new long[N + 1];
		pSum = new long[N + 1];
		st = new StringTokenizer(br.readLine());
		for (int i = 1; i <= N; i++) {
			A[i] = Long.parseLong(st.nextToken());
			pSum[i] += pSum[i - 1] + A[i];
		}

		System.out.println(solve());
	}
}

#endif
}
