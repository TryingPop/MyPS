using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 20
이름 : 배성훈
내용 : 크림 파스타
    문제번호 : 25214번

    dp, 그리디 알고리즘 문제다
    그리디하게 풀었다

    앞의 원소와 차이가 최대가 되게 저장해야하므로
    최대 차이와 최소값만 저장했다
    그리고 현재 값과 최소값의 차이가 최대 차이보다 클 경우 갱신한다
    이렇게 제출하니 이상없이 통과했다
*/
namespace BaekJoon.etc
{
    internal class etc_0301
    {

        static void Main301(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int min = ReadInt(sr);
            int diff = 0;
            int[] ret = new int[n];

            for (int i = 1; i < n; i++)
            {

                int cur = ReadInt(sr);

                // 최소값 갱신
                min = cur < min ? cur : min;
                // 현재 차이 확인
                int chkDiff = cur - min;
                // 최대 차이보다 큰 경우 갱신
                if (chkDiff > diff) diff = chkDiff;
                ret[i] = diff;
            }
            sr.Close();

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 0; i < n; i++)
                {

                    sw.Write(ret[i]);
                    sw.Write(' ');
                }
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
#if other
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.StringTokenizer;

public class Main {

    private static final String SPACE = " ";

    private static int[] A;
    private static int[] dp;

    private static int N;

    public static void main(String[] args) throws Exception {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        N = Integer.parseInt(br.readLine());

        StringTokenizer st = new StringTokenizer(br.readLine());
        A = new int[N];
        dp = new int[N];

        for(int i = 0; i < N; i++) {
            A[i] = Integer.parseInt(st.nextToken());
            dp[i] = -1;
        }

        bottomUp();

        StringBuilder sb = new StringBuilder();
        for(int d: dp) {
            sb.append(d).append(SPACE);
        }

        System.out.println(sb);
    }

    private static void bottomUp() {
        int min = A[0];
        dp[0] = 0;

        for(int i = 1; i < N; i++) {
            dp[i] = Math.max(dp[i - 1], A[i] - min);
            min = Math.min(A[i], min);
        }
    }
}

#endif
}
