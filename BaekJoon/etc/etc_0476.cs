using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 7
이름 : 배성훈
내용 : 선물할인
    문제번호 : 25947번

	그리디 알고리즘, 정렬, 누적합 문제다
	처음에는 이중 포문으로 제출했는데 이게 통과되었다(2416ms... 걸렸다 이게 왜 ?)
	이후에 다른 사람 풀이를 보고 슬라이딩 윈도우 기법으로 다시 풀었다
	그리고 덧셈연산으로 진행하니 오버플로우로 두 번 틀렸다

	아이디어는 다음과 같다
	먼저 정렬하고 세일 없이 최대한 구매한다
	이후에 최대 k개까지 할인 적용한다
	그리고 다음껄 할인하고 가장 싼 것에 적용했던 할인을 푼다
	이제 해당물품을 살 수 있는지 확인한다

	이렇게 진행해서 최대한 살 수 있는데까지 이상없이 88ms에 통과했다
    다른 사람 풀이를 보니 이분 탐색으로 세일을 해서 푸는 사람도 있었다
*/

namespace BaekJoon.etc
{
    internal class etc_0476
    {

        static void Main476(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int n = ReadInt();
            long r = ReadInt();
            int k = ReadInt();

            int[] arr = new int[n];
            bool[] sale = new bool[n];
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
            }

            sr.Close();

            Array.Sort(arr);

            // 세일 없이 최대한 살 수 있는 경우를 찾는다
			int maxIdx = n;
			for (int i = 0; i < n; i++)
			{

				if (r - arr[i] < 0)
				{

					maxIdx = i;
					break;
				}

				r -= arr[i];
			}

            // 세일 없이 다 사는 경우면 탈출
			if (maxIdx == n) 
			{ 
				
				Console.WriteLine(n);
				return;
			}

            // 그리디하게 현재꺼 최대한 세일해버린다
			for (int i = 1; i <= k; i++)
			{

				if (maxIdx - i < 0) break;

				r += arr[maxIdx - i] / 2;
			}

            // 슬라이딩 윈도우 기법으로 k개 할인을 옮겨간다
			int ret = n;
			for (int i = maxIdx; i < n; i++)
			{

				r += arr[i] / 2;
				if (i - k >= 0) r -= arr[i - k] / 2;

                // 해도 못사면 탈출
				if (r - arr[i] < 0)
				{

					ret = i;
					break;
				}
				r -= arr[i];
			}

			Console.WriteLine(ret);

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c!= '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }

#if Too_Slow
	
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int n = ReadInt();
            long r = ReadInt();
            int k = ReadInt();

            int[] arr = new int[n];
            bool[] sale = new bool[n];
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
            }

            sr.Close();

            Array.Sort(arr);

            long curVal = 0;
            int curIdx = 0;
            int remSale = k;
            int ret = 0;

            while(true)
            {

                if (curIdx >= n)
                {

                    break;
                }

                if (curVal + arr[curIdx] <= r)
                {

                    curVal += arr[curIdx];
                    ret++;
                    curIdx++;
                    continue;
                }

                if (remSale > 0)
                {

                    remSale--;
                    int saleIdx = curIdx;
                    while (saleIdx >= 0 && sale[saleIdx])
                    {

                        saleIdx--;
                    }
                    if (saleIdx >= 0) 
                    { 

                        r += arr[saleIdx] / 2; 
                        sale[saleIdx] = true;
                    }
                    else remSale = 0;
                    continue;
                }

                break;
            }

            Console.WriteLine(ret);

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c!= '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
#endif
        }
    }
#if other
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Arrays;
import java.util.StringTokenizer;

public class Main {
	/*
	 * 내가 사는 물건중에 가장 비싼것을 할인받아야 한다.
	 * 정렬로 작은것부터 사는데, 할인받을 수 있는 n개를 무조건 할인해서 값을 가져본다.
	 * 그 값이 예산을 넘을때의 전칸의 인덱스가 정답
	 */
	public static void main(String[] args) throws IOException {
		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		StringTokenizer st = new StringTokenizer(br.readLine());
		int n = Integer.parseInt(st.nextToken()); // 선물
		int b = Integer.parseInt(st.nextToken()); // 예산
		int a = Integer.parseInt(st.nextToken()); // 할인품목
		int[] arr = new int[n + 1];
		st = new StringTokenizer(br.readLine());
		for (int i = 1; i <= n; i++) {
			arr[i] = Integer.parseInt(st.nextToken());
		}
		Arrays.sort(arr);
		int ans = -1;
		int[] dp = new int[100001];
		dp[0] = 0;
		for (int i = 1; i <= a; i++) {
			dp[i] = dp[i - 1] + (arr[i] / 2);
			if (dp[i] > b) {
				ans = i - 1;
				break;
			}
		}
		if (ans == -1) {
			for (int i = a + 1; i < 100000; i++) {
				if (i > n) { // 예산이 남았는데 선물은 없는 경우.
					ans = i - 1;
					break;
				}
				dp[i] = dp[i - 1] + (arr[i] / 2);
				dp[i] += arr[i - a] / 2;
				if (dp[i] > b || i > n) {
					ans = i - 1;
					break;
				}
			}
		}
		System.out.println(ans);
	}
}

#elif other2
using System;

public class Program
{
    static void Main()
    {
        int[] nba = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        int n = nba[0], b = nba[1], a = nba[2];
        int[] price = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        Array.Sort(price);
        int left = 1, right = n;
        while (left <= right)
        {
            int mid = (left + right) / 2;
            int sum = 0;
            for (int i = mid - 1, j = a; i >= 0; i--, j--)
            {
                sum += j > 0 ? price[i] / 2 : price[i];
                if (sum > b)
                    break;
            }
            if (sum > b)
                right = mid - 1;
            else
                left = mid + 1;
        }
        Console.Write(right);
    }
}
#endif
}
