using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 30
이름 : 배성훈
내용 : 배수 공사
    문제번호 : 15817번

    dp, 배낭문제다
    처음에는 여러 개이네 하고, 평벙한 배낭 2 문제처럼 풀다가 한 번 틀렸다
    해당 문제는 최대 만족도로 dp를 갱신하는 것이고 여기서는 만들 수 있는 경우의 수를 찾는 것이기에
    잘못된 접근 방법임을 인지했다
    그래서 그냥, 개수만큼 더해주는 식으로 제출하니 104ms에 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0665
    {

        static void Main665(string[] args)
        {

            StreamReader sr;
            int n, len;
            int[] dp;

            int[] weight;
            int[] cnt;
            Solve();

            void Solve()
            {

                Input();

                dp = new int[len + 1];
                dp[0] = 1;

                for (int i = 0; i < n; i++)
                {

                    int curW = weight[i];

                    for (int j = len; j >= 0; j--)
                    {

                        if (dp[j] == 0) continue;
                        for (int k = 1; k <= cnt[i]; k++)
                        {

                            int next = k * curW + j;
                            if (len < next) break;
                            dp[next] += dp[j];
                        }
                    }
                }

                Console.WriteLine(dp[len]);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                len = ReadInt();

                weight = new int[n];
                cnt = new int[n];

                for (int i = 0; i < n; i++)
                {

                    weight[i] = ReadInt();
                    cnt[i] = ReadInt();
                }

                sr.Close();
            }

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
    public static void main(String[] args) throws IOException {
        InputStreamReader isr = new InputStreamReader(System.in);
        BufferedReader br = new BufferedReader(isr);
        StringTokenizer st = new StringTokenizer(br.readLine());
        int N = Integer.parseInt(st.nextToken());
        int x = Integer.parseInt(st.nextToken());
        DrainageWork dw = new DrainageWork(x);
        for(int i = 0; i < N; i++) {
            st = new StringTokenizer(br.readLine());
            dw.inputPipe(Integer.parseInt(st.nextToken()),Integer.parseInt(st.nextToken()));
        }
        System.out.print(dw.getResult());
    }
}
class DrainageWork {
    int[] DP;
    int wantLength;
    public DrainageWork(int wantLength) {
        this.wantLength = wantLength;
        DP = new int[wantLength+1];
    }
    public void inputPipe(int pipeLength, int number) {
        int[] currentDP = new int[wantLength+1];
        for(int i = 0; i <= number; i++) {
            if(i * pipeLength <= wantLength) {
                currentDP[i * pipeLength] = 1;
            } else {
                break;
            }
        }
        for(int i = 1; i < wantLength+1; i++) {
            if(DP[i] > 0) {
                for(int j = 0; j <= number; j++) {
                    if(i + j*pipeLength <= wantLength) {
                        currentDP[i + j * pipeLength] += DP[i];
                    } else {
                        break;
                    }
                }
            }
        }
        DP = currentDP;
    }
    public int getResult() {
        return DP[wantLength];
    }
}
#elif other2
n,m=map(int,input().split())
g=[1]+[0 for _ in range(m)]
for i in range(n):
    a,b=map(int,input().split())
    for j in range(m-1,-1,-1):
        if g[j]==0:continue
        for k in range(1,b+1):
            if j+k*a<=m:
                g[j+k*a]+=g[j]
            else:break
print(g[m])
#elif other3
// #include <bits/stdc++.h>
using namespace std;
int dp[101][10001],arr[101][2],N,X;

int main()
{
	cin>>N>>X;
	for(int i=1;i<=N;i++) cin>>arr[i][0]>>arr[i][1];
	dp[0][0]=1;
	for(int i=1;i<=N;i++) {
		for(int k=0;k<arr[i][0];k++) {
			int sum=0,cnt=0;
			for(int j=k+arr[i][0];j<=X;j+=arr[i][0]) {
				sum+=dp[i-1][j-arr[i][0]];
				cnt++;
				if(cnt>arr[i][1]) sum-=dp[i-1][j-arr[i][0]*(arr[i][1]+1)];
				dp[i][j]=sum;
			}
		}
		for(int k=0;k<=X;k++)dp[i][k]+=dp[i-1][k];
	}
	cout<<dp[N][X];
}
#endif
}
