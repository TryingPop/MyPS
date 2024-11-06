using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 7
이름 : 배성훈
내용 : 왕복
    문제번호 : 18311번

    구현 문제다
    입력과 동시에 누적합을 구하고
    이분 탐색으로 어디에 속하는지 확인했다
*/

namespace BaekJoon.etc
{
    internal class etc_0473
    {

        static void Main473(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()));
            int n = (int)ReadLong();
            long k = ReadLong();

            long[] sum = new long[n + 1];

            for (int i = 1; i <= n; i++)
            {

                sum[i] = ReadLong();
                sum[i] += sum[i - 1];
            }

            sr.Close();

            int ret;
            
            // 여기서는 2k 미만의 값만 들어오기에 
            // 해당 방법으로 검색 가능
            // 다만, 2k 넘어가면 나눠줘야한다
            if (sum[n] <= k)
            {

                // 목표지점 가고 시작지점으로 돌아가는 경우
                k = 2 * sum[n] - k;
                
                int l = 0;
                int r = n;

                while(l <= r)
                {

                    int mid = (l + r) / 2;

                    // 걸치는 경우면 다음 지점을 향해야하기에
                    // 이전 인덱스를 계승해야한다
                    if (sum[mid] < k) l = mid + 1;
                    else r = mid - 1;
                }

                ret = r + 1;
                ret = ret < 0 ? 0 : ret;
            }
            else
            {

                // 목표지점 가는 경우
                int l = 0;
                int r = n;

                while(l <= r)
                {

                    int mid = (l + r) / 2;
                    // 걸치는 경우면 다음 인덱스를 향해야한다
                    if (sum[mid] <= k) l = mid + 1;
                    else r = mid - 1;
                }

                ret = l;
            }

            Console.WriteLine(ret);

            long ReadLong()
            {

                int c;
                long ret = 0;
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

    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        StringTokenizer st = new StringTokenizer(br.readLine());

        int n = Integer.parseInt(st.nextToken());
        long k = Long.parseLong(st.nextToken());
        int[] course = new int[n];
        st = new StringTokenizer(br.readLine());
        for (int i = 0; i < n; i++) {
            course[i] = Integer.parseInt(st.nextToken());
        }

        boolean flag = false;

        for (int i = 0; i < n; i++) {
            k -= course[i];
            if (k < 0) {
                System.out.println(i + 1);
                flag = true;
                break;
            }
        }

        if (!flag) {
            for (int i = n - 1; i >= 0; i--) {
                k -= course[i];
                if (k < 0) {
                    System.out.println(i + 1);
                    break;
                }
            }
        }
    }
}

#endif
}
