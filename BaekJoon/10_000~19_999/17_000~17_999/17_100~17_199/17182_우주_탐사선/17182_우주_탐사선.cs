using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 30
이름 : 배성훈
내용 : 우주 탐사선
    문제번호 : 17182번

    플로이드 워셜, 브루트포스 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1303
    {

        static void Main1303(string[] args)
        {

            int[][] fw;
            int n, k;

            Solve();
            void Solve()
            {

                Input();

                SetFW();

                GetRet();
            }

            void GetRet()
            {

                int INF = 1_000_000_000;
                int END = (1 << n) - 1;

                Console.Write(DFS(k, 1 << k));
                int DFS(int _cur, int _visit)
                {

                    if (_visit == END) return 0;

                    int ret = INF;

                    for (int next = 0; next < n; next++)
                    {

                        if ((_visit & (1 << next)) != 0) continue;
                        int chk = fw[_cur][next] + DFS(next, _visit | 1 << next);
                        ret = Math.Min(ret, chk);
                    }

                    return ret;
                }
            }

            void SetFW()
            {

                for (int mid = 0; mid < n; mid++)
                {

                    for (int start = 0; start < n; start++)
                    {

                        for (int end = 0; end < n; end++)
                        {

                            fw[start][end] = Math.Min(fw[start][end], fw[start][mid] + fw[mid][end]);
                        }
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                k = ReadInt();

                fw = new int[n][];
                for (int r = 0; r < n; r++)
                {

                    fw[r] = new int[n];
                    for (int c = 0; c < n; c++)
                    {

                        fw[r][c] = ReadInt();
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
            }
        }
    }

#if other
// #include<stdio.h>
// #include<string.h>
// #include<algorithm>
using namespace std;
int N, K, M, T[10][10], d[1024][10];
int sol(int v, int x){
	if(v==M)	return 0;
	if(d[v][x]>=0)	return d[v][x];
	int ret=1e6;
	for(int i=0;i<N;++i)
		if(!(v&(1<<i)))	ret=min(ret, sol(v|(1<<i), i)+T[x][i]);
	return d[v][x]=ret;
}
int main(){
	scanf("%d %d", &N, &K);
	for(int i=0;i<N;++i)for(int j=0;j<N;++j)	scanf("%d", &T[i][j]);
	for(int k=0;k<N;++k)for(int i=0;i<N;++i)for(int j=0;j<N;++j){
		T[i][j]=min(T[i][j], T[i][k]+T[k][j]);
	}
	M=(1<<N)-1;
	memset(d, -1, sizeof(d));
	printf("%d\n", sol(1<<K, K));
	return 0;
}
#endif
}
