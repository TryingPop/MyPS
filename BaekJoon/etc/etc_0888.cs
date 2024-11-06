using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 19
이름 : 배성훈
내용 : 사다리 게임
    문제번호 : 2008번

    dp, 그래프 이론 문제다
    2차원 배열을 할당한 뒤 
    N^2 x M 시간에 해결했다

    dp를 다음과 같이 설정했다
    다음과 같이 사다리가 세팅되었다고 생각하자
    
        A   B   C   D
        |   |   |   |
             ---            (사다리 B - C를 잇는다)
        |   |   |   |
         ---                (사다리 A - B를 잇는다)
        |   |   |   |
                 ---        (사다리 C - D를 잇는다)
        |   |   |   |
        1   2   3   4

    사다리를 구분선으로 보면, 다음처럼 인덱스를 부여할 수 있다

        A   B   C   D
        |   |   |   |   -> 구간 0
             ---            (사다리 B - C를 잇는다)
        |   |   |   |   -> 구간 1
         ---                (사다리 A - B를 잇는다)
        |   |   |   |   -> 구간 2
                 ---        (사다리 C - D를 잇는다)
        |   |   |   |   -> 구간 3
        1   2   3   4

    이제 각 구간 i에 대해 시작지점 a에서 시작해
    각 번호 b로 가는 최소 비용을 dp에 저장한다

        dp[i][b] = (최소 비용)

    그러면 처음 구간 0은 사다리를 만들어 이어줘야 한다

    이후에 구간 1부터는 먼저 직선으로 내려온 경우로 초기값을 채워줬다
    여기서 사다리가 있으면 제거해서 | 이되게 한다
    이전 값을 이어받는다 다만 사다리가 있는 구간이면 끊는 값을 추가한다
    위 예제 구간 1에서 dp[1][2]의 경우를 보면
    B - C가 연결되는 사다리가 있으므로 해당 사다리를 끊는 비용을 추가한다
        dp[1][2] = dp[0][2] + x
    dp[1][3]역시도 마찬가지로 끊는 비용 추가해줘야한다
        dp[1][3] = dp[0][3] + x
    
    이후에 이제 다른 지점에서 해당 지점으로 오는 것을 찾는다
    이는 포문으로 찾는다
    그래서 사다리를 잇는데 만약 앞번 사다리를 활용하는 형식으로 잇는다
    위 예제로 구간 1에서 4에서 1으로 가는 비용을 생각할 때,
    4에서 1로 갈러면 사다리를 3개 이어야 하지만 B - C 사다리를 이용하면 2개만 이어도 된다
        dp[1][1] = Math.Min(dp[1][1], dp[0][4] + 2 * y)
    그리고 사다리를 이용안해도 되는 경우면
    구간 1에서 2에서 1로 사다리 활용이 불가능한 경우면,
        dp[1][1] = Math.Min(dp[1][1], dp[0][2] + y)
    가된다

    이렇게 찾으면 dp[m][b]에 최소 비용이 담기게 된다
    여기서는 순차적으로 진행되기에 2개의 배열을 번갈아 가며 썼다
*/

namespace BaekJoon.etc
{
    internal class etc_0888
    {

        static void Main888(string[] args)
        {

            StreamReader sr;

            int n, m, a, b, x, y;
            int[][] dp;

            Solve();
            void Solve()
            {

                Init();

                SetDp();

                GetRet();
            }

            void GetRet()
            {

                Console.Write(dp[0][b]);
            }

            void SetDp()
            {

                dp = new int[2][];
                for (int i = 0; i < 2; i++)
                {

                    dp[i] = new int[n];
                }

                for (int i = 0; i < n; i++)
                {

                    dp[0][i] = y * Math.Abs(i - a);
                }

                for (int i = 0; i < m; i++)
                {

                    int conn = ReadInt();
                    Swap();

                    for (int j = 0; j < n; j++)
                    {

                        dp[0][j] = dp[1][j];
                        if (j == conn || j + 1 == conn) dp[0][j] += x;

                        for (int k = 0; k < n; k++)
                        {

                            if (j == k) continue;
                            if ((j < conn && conn <= k) || (k < conn && conn <= j))
                                dp[0][j] = Math.Min(dp[0][j], dp[1][k] + (Math.Abs(j - k) - 1) * y);
                            else
                                dp[0][j] = Math.Min(dp[0][j], dp[1][k] + Math.Abs(j - k) * y);
                        }
                    }
                }

                sr.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();
                
                a = ReadInt() - 1;
                b = ReadInt() - 1;

                x = ReadInt(); 
                y = ReadInt();
            }

            void Swap()
            {

                int[] temp = dp[0];
                dp[0] = dp[1];
                dp[1] = temp;
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
// #include <stdio.h>
// #include <algorithm>
// #define mink min=std::min(min,k)

int x,y,n,m,a,b;
int arr[501],dp[110][510][3];
int f(int xx,int yy,int t)
{
    if(dp[xx][yy][t]!=-1)
        return dp[xx][yy][t];
    int k,min=987654321;
    if(yy==m)
    {
        if(xx==b)
            return 0;
        if(xx>1&&t!=2)
            k=f(xx-1,yy,1)+y,mink;
        if(xx<n&&t!=1)
            k=f(xx+1,yy,2)+y,mink;
        return min;
    }
    if(arr[yy]==xx||arr[yy]+1==xx)
    {
        k=f(arr[yy]==xx?xx+1:xx-1,yy+1,0),mink;
        k=f(xx,yy+1,0)+x,mink;
    }
    else
        k=f(xx,yy+1,0),mink;
    if(xx>1&&t!=2)
        k=f(xx-1,yy,1)+y,mink;
    if(xx<n&&t!=1)
        k=f(xx+1,yy,2)+y,mink;
    return dp[xx][yy][t]=min;
}
int main()
{
    int i,j;
    scanf("%d%d%d%d%d%d",&n,&m,&a,&b,&x,&y);
    for(i=0;i<m;i++)
        scanf("%d",&arr[i]);
    for(i=0;i<=n;i++)
        for(j=0;j<=m;j++)
            dp[i][j][0]=dp[i][j][1]=dp[i][j][2]=-1;
    printf("%d",f(a,0,0));
}
#elif other2
// #include<cstdio>
// #include<cstring>
// #include<algorithm>
using namespace std;
const int MAX_N = 100, MAX_M = 500;
int N, M, b, X, Y, dp[MAX_N][MAX_M], rn[MAX_M];
int rec(int col, int row) {
	if (col < 0 || col >= N) return 1e9;
	if (row == M)
		return Y * abs(col - b);
	int& ret = dp[col][row];
	if (ret != -1) return ret;
	ret = 1e9;
	int ladj = rn[row] == col - 1, radj = rn[row] == col;
	ret = min({ rec(col, row + 1) + (ladj || radj) * X, rec(col + 1, row) + Y, rec(col - 1, row) + Y });
	if (ladj)
		ret = min(ret, rec(col - 1, row + 1));
	else if (radj)
		ret = min(ret, rec(col + 1, row + 1));
	return ret;
}
int main() {
	memset(dp, -1, sizeof dp);
	int i, a;
	scanf("%d%d%d%d%d%d", &N, &M, &a, &b, &X, &Y);
	a--, b--;
	for (i = 0; i < M; i++) {
		scanf("%d", &rn[i]);
		rn[i]--;
	}
	printf("%d", rec(a, 0));
	return 0;
}
#endif
}
