using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 3
이름 : 배성훈
내용 : 시리얼 넘버
    문제번호 : 5444번

    dp, 배낭문제, 슬라이딩 윈도우 문제다.
    경우의 수가 500 x 10만 = 5천만 이고 시간이 3초이므로 
    배낭 형태로 시도했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1092
    {

        static void Main1092(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n, m;
            int[] arr;
            int[] curCnt, nextCnt;

            Solve();
            void Solve()
            {

                Init();

                int t = ReadInt();

                for (int i = 0; i < t; i++)
                {

                    Input();
                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                curCnt[0] = 0;
                nextCnt[0] = 0;
                for (int i = 0; i < n; i++)
                {

                    for (int j = 0; j < m; j++)
                    {

                        if (curCnt[j] == -1) continue;
                        int next = (arr[i] + j) % m;
                        nextCnt[next] = Math.Max(curCnt[j] + 1, nextCnt[next]);
                    }

                    for (int j = 0; j < m; j++)
                    {

                        curCnt[j] = nextCnt[j];
                    }
                }

                sw.Write($"{curCnt[0]}\n");
            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                Array.Fill(curCnt, -1, 0, m);
                Array.Fill(nextCnt, -1, 0, m);
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;

                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                arr = new int[500];
                curCnt = new int[100_000];
                nextCnt = new int[100_000];
            }
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;
// #define max_n 501
// #define max_m 100001

vector<int> x(max_n);
int dp[2][max_m];

int main() {
	
	ios::sync_with_stdio(false);
	cin.tie(0);
	
	int t, n, m;
	cin>>t;
	while(t--){
		cin>>n>>m;
		for(int i=1; i<=n; i++){
			cin>>x[i];
		}
		memset(dp, -1, sizeof(dp));
		dp[1][x[1]%m]=1;
		
		for(int i=2; i<=n; i++){
			int mod= x[i]%m; 
			for(int j=0; j<m; j++){
				
				int pMod= (j<mod)? j-mod+m: j-mod;
				
				if(i%2==0){
					
					if(dp[1][j]!=-1) dp[0][j]= dp[1][j];
					if(dp[1][pMod]!=-1) dp[0][j]= max(dp[0][j], dp[1][pMod]+1);	
				}
				else{
					if(dp[0][j]!=-1) dp[1][j]= dp[0][j];
					if(dp[0][pMod]!=-1) dp[1][j]= max(dp[1][j], dp[0][pMod]+1);
				}
				
			}
		}
		cout<<dp[n%2][0]<<"\n";
	}
	
	return 0;
}
#endif
}
