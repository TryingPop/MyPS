using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 18
이름 : 배성훈
내용 : Python 문법
    문제번호 : 15731번

    누적합, dp 문제다.
    dp[i][j] = val를 j번째 들여쓰기한 곳에 f를 배치할 수 있는 경우의 수이다.
    그리고 i = 0은 현재이고, i = 1이면 다음이다.

    f가 들어오면 f의 위치는 1칸 이동해야 한다.
    그래서 dp[1][j + 1] = dp[0][j]이다.

    e가 들어오면 f가 올 수 있는 위치를 보자.
        f
            e
    인 경우를 보자.
    다음 f의 경우
        f
            e
            f
    이거나
        f
            e
        f
    의 경우처럼 될 수 있다.
    즉 이전의 f의 위치에올 수 있다.

    그래서 k <= j에 대해 dp[1][k] += dp[0][j]임을 알 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1874
    {

        static void Main1874(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int MOD = 1_000_000_007;
            string str = sr.ReadLine();
            int[][] dp = new int[2][];
            dp[0] = new int[str.Length + 1];
            dp[1] = new int[str.Length + 1];

            dp[0][0] = 1;
            for (int i = 0; i < str.Length; i++)
            {

                if (str[i] == 'f')
                {

                    // 들여쓰기이므로 1칸씩 늘여서 저장
                    for (int j = 0; j < str.Length; j++)
                    {

                        dp[1][j + 1] = (dp[1][j + 1] + dp[0][j]) % MOD;
                    }
                }
                else
                {

                    // 실행문 배치
                    // 들여쓰기 구간에 누적됨을 알 수 있다.
                    int sum = 0;
                    for (int j = str.Length; j >= 0; j--)
                    {

                        sum = (sum + dp[0][j]) % MOD;
                        dp[1][j] = sum;
                    }
                }

                for (int j = 0; j <= str.Length; j++)
                {

                    dp[0][j] = dp[1][j];
                    dp[1][j] = 0;
                }
            }

            int ret = dp[0][0];

            Console.Write(ret);
        }
    }

#if other
// #include <stdio.h>
// #include <string.h>

int arr[5050];
int flag;
int cnt[5050];
int top;

int main(){
    char str[5050];

    scanf("%s",str);

    int len=strlen(str);

    for(int i=0;i<len;i++){
        if(str[i]=='f') arr[i]=1;
        else arr[i]=0;
    }

    cnt[0]=1;
    top=1;

    for(int i=0;i<len;i++){
        if(flag){
            for(int j=top-1;j>=0;j--){
                cnt[j+1]=cnt[j];
            }
            cnt[0]=0;
            top++;
        }
        else{
            int sum=0;
            for(int j=top-1;j>=0;j--){
                sum+=cnt[j];
                sum%=1000000007;
                cnt[j]=sum;
            }
        }
        flag=arr[i];

        /*for(int j=0;j<top;j++){
            printf("%d ",cnt[j]);
        }
        printf("\n");*/
    }

    int sum=0;

    for(int i=0;i<top;i++){
        sum+=cnt[i];
        sum%=1000000007;
    }
    printf("%d",sum);

    return 0;
}

#endif
}
