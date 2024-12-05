using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 5
이름 : 배성훈
내용 : Dance Dance Revolution
    문제번호 : 2342번

    dp문제다.
    dp[i][j][k] = val를 다음과 같이 설정하면 된다.
    i번째 왼발이 j에 있고, 오른발이 k에 있는 최소 힘을 담으면 된다.
    여기서 0은 중앙이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1156
    {

        static void Main1156(string[] args)
        {

            int INF = 1_000_000;
            int[][] move;
            StreamReader sr;
            
            int[][][] dp;
            Solve();
            void Solve()
            {

                Init();

                GetRet();
            }

            void GetRet()
            {

                dp[0][0][0] = 0;

                int num;
                while ((num = ReadInt()) != 0)
                {

                    for (int i = 0; i < 5; i++)
                    {

                        GetMin(num, i);
                        GetMin(i, num);
                    }

                    for (int i = 0; i < 5; i++)
                    {

                        for (int j = 0; j < 5; j++)
                        {

                            dp[0][i][j] = dp[1][i][j];
                            dp[1][i][j] = INF;
                        }
                    }
                }

                int ret = INF;
                for (int i = 0; i < 5; i++)
                {

                    for (int j = 0; j < 5; j++)
                    {

                        if (dp[0][i][j] == INF) continue;
                        ret = Math.Min(ret, dp[0][i][j]);
                    }
                }

                Console.Write(ret);
                void GetMin(int _i, int _j)
                {

                    int m = INF;
                    for (int i = 0; i < 5; i++)
                    {

                        m = Math.Min(m, dp[0][_i][i] + move[i][_j]);
                        m = Math.Min(m, dp[0][i][_j] + move[i][_i]);
                    }

                    dp[1][_i][_j] = m;
                }
            }

            int ReadInt()
            {

                int c, ret = 0;

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                dp = new int[2][][];
                for (int i = 0; i < 2; i++)
                {

                    dp[i] = new int[5][];
                    for (int j = 0; j < 5; j++)
                    {

                        dp[i][j] = new int[5];
                        Array.Fill(dp[i][j], INF);
                    }
                }

                move = new int[5][];
                for (int i = 0; i < 5; i++)
                {

                    move[i] = new int[5];
                }

                move[0][0] = INF;
                move[0][1] = 2;
                move[0][2] = 2;
                move[0][3] = 2;
                move[0][4] = 2;

                move[1][0] = INF;
                move[1][1] = 1;
                move[1][2] = 3;
                move[1][3] = 4;
                move[1][4] = 3;

                move[2][0] = INF;
                move[2][1] = 3;
                move[2][2] = 1;
                move[2][3] = 3;
                move[2][4] = 4;

                move[3][0] = INF;
                move[3][1] = 4;
                move[3][2] = 3;
                move[3][3] = 1;
                move[3][4] = 3;

                move[4][0] = INF;
                move[4][1] = 3;
                move[4][2] = 4;
                move[4][3] = 3;
                move[4][4] = 1;
            }
        }
    }

#if other
// #include <cstdio>
// #include <algorithm>

int f(int p, int x) {
	return p!=x ? p ? abs(p-x)==2 ? 4 : 3 : 2 : 1;
}
int main() {
	int a[5]{};
	a[1]=a[2]=a[3]=a[4]=1e9;
	for(int p=0, x; x=getchar_unlocked()-48; p=x) {
		getchar_unlocked();
		int m=1e9, w=f(p, x);
		for(int i=0; i<5; i++) m=std::min(m, a[i]+f(i, x)), a[i]+=w;
		a[p]=m;
	}
	printf("%d", std::min({a[0], a[1], a[2], a[3], a[4]}));
}
#endif
}
