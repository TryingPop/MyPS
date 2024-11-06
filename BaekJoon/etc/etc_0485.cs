using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 8
이름 : 배성훈
내용 : 동작 그만. 밑장 빼기냐?
    문제번호 : 20159번

    누적합 문제다
    dp를 이용해 그리디하게 풀었다
    처음 틀렸을 때 밑장 빼기를 맨 밑에꺼 빼는거 아닌가? 의문을 가져 찾아봤고 문제 설명에는 없었다;
    질문 게시판에 다른 사람 설명글에 맨 밑에거 뺀다고 나와있었다;
    다른 사람이 남긴 테스트 케이스를 보고 직접 비교해본 결과 상대턴에 밑장빼는 경우를 고려안해서 틀림을 알았다
    그래서 이를 수정하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0485
    {

        static void Main485(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();

            int[] arr = new int[n];

            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
            }
            sr.Close();

            int len = n / 2;
            int[,] dp = new int[len, 2];

            dp[0, 0] = arr[0];
            dp[0, 1] = arr[n - 1];
            for (int i = 1; i < len; i++)
            {

                dp[i, 0] = dp[i - 1, 0] + arr[i * 2];
                int chk1 = dp[i - 1, 1] + arr[i * 2 - 1];       // 이전에 밑장 빼고 온 경우
                int chk2 = dp[i - 1, 0] + arr[i * 2 - 1];       // 이전에 상대턴에 밑장 빼는 경우
                int chk3 = dp[i - 1, 0] + arr[n - 1];           // 이번턴에 내가 밑장 빼는 경우
                int chk = chk1 < chk2 ? chk2 : chk1;
                chk = chk3 < chk ? chk : chk3;
                dp[i, 1] = chk;
            }

            int ret = dp[len - 1, 0] < dp[len - 1, 1] ? dp[len - 1, 1] : dp[len - 1, 0]; 
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
import java.util.*;
import java.io.*;

/*
    완전탐색을 할경우엔 N이 너무 크기 때문에 힘들다.
    밑장을 뺴는 순간, 홀수번째가 아닌 짝수번째 카드를 뽑을 수 있음을 알 수 있다.
    따라서 누적합을 이용하면 빠른 시간내에 구할 수 있다.
 */
public class Main {
    public static int[] odds, evens;

    public static void main(String[] args) throws Exception {
        int N = read();
        odds = new int[N/2+1];
        evens = new int[N/2+1];
        for (int i = 1; i <= N/2; i++) {
            odds[i] = odds[i-1] + read();
            evens[i] = evens[i-1] + read();
        }

        int ans = 0;

        int lastEvenCard = evens[N/2] - evens[N/2-1];
        for (int i = 0; i <= N/2; i++) {
            int odd = odds[i];
            int even = evens[N/2] - evens[i]; // 내차례때 밑장을 뺴는 것
            if (i != 0) {
                even = Math.max(even, evens[N/2] - evens[i-1] -lastEvenCard); // 상대차례 때 밑장을 뺴는 것
            }

            int sum = odd+even;
            if (ans < sum)
                ans = sum;
        }
        System.out.println(ans);
    }

    public static int read() throws Exception{
        int n = 0;
        int cur;
        boolean isNumber = false;
        boolean isNegative = false;
        while(true){
            cur = System.in.read();
            if(cur == '-'){
                isNegative = true;
            }
            else if(cur <= 32){
                if(isNumber){
                    return isNegative ? -n : n;
                }
            }
            else{
                isNumber = true;
                n = (n<<3) + (n<<1) + (cur&15);
            }
        }
    }

}
#endif
}
