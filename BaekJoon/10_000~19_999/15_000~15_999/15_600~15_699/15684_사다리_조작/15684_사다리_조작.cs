using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 2
이름 : 배성훈
내용 : 사다리 조작
    문제번호 : 15684번

    브루트포스, 백트래킹 문제다.
    사다리를 최대한 놓을 수 있는 갯수를 0개에서 3개까지 늘리면서
    놓을 수 있는 자리에 놓는다.
    그리고 가능하면 해당 갯수를 출력하고 종료하는식으로 구현했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1509
    {

        static void Main1509(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = ReadInt();
            int m = ReadInt();
            int h = ReadInt();

            int[][] line = new int[h][];

            for (int i = 0; i < h; i++)
            {

                line[i] = new int[n];
            }

            for (int i = 0; i < m; i++)
            {

                int y = ReadInt() - 1;
                int x = ReadInt();

                line[y][x] = -1;
                line[y][x - 1] = 1;
            }

            for (int i = 0; i <= 3; i++)
            {

                if (DFS(i))
                {

                    Console.Write(i);
                    return;
                }
            }

            Console.Write(-1);

            bool DFS(int _cnt, int _prevX = 1, int _prevY = 0)
            {

                if (_cnt == 0)
                    return ChkSame();

                for (int y = _prevY; y < h; y++)
                {

                    for (int x = y == _prevY ? _prevX : 1; x < n; x++)
                    {

                        if (line[y][x] != 0 || line[y][x - 1] != 0) continue;
                        line[y][x - 1] = 1;
                        line[y][x] = -1;

                        if (DFS(_cnt - 1, x, y)) return true;

                        line[y][x - 1] = 0;
                        line[y][x] = 0;
                    }
                }

                return false;
            }

            bool ChkSame()
            {

                for (int i = 0; i < n; i++)
                {

                    int x = i;
                    for (int j = 0; j < h; j++)
                    {

                        x += line[j][x];
                    }

                    if (x != i) return false;
                }

                return true;
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

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }

#if other
// #include<cstdio>
int N, M, H, ans=4;
bool A[31][11];
bool chk(){
	for(int i=1;i<=N;++i){
		int t=i;
		for(int j=1;j<=H;++j){
			if(A[j][t])	++t;
			else if(A[j][t-1])	--t;
		}
		if(t!=i)	return false;
	}
	return true;
}
void sol(int cnt, int g){
	if(ans<4)	return;
	if(cnt==g){
		if(chk())	ans=ans>g?g:ans;
		return;
	}
	for(int j=1;j<N;++j){
		for(int i=1;i<=H;++i){
			if(A[i][j] || A[i][j+1] || A[i][j-1])	continue;
			A[i][j]=1;
			sol(cnt+1, g);
			A[i][j]=0;
			while(i<=H && !A[i][j-1] && !A[i][j+1])	++i;
		}
	}
}
int main(){
	scanf("%d %d %d", &N, &M, &H);
	while(M--){
		int a, b;
		scanf("%d %d", &a, &b);
		A[a][b]=1;
	}
	for(int i=0;i<4;++i){
		sol(0, i);
		if(ans!=4){
			printf("%d\n", ans);
			break;
		}
	}
	if(ans==4)	printf("-1\n");
	return 0;
}
#endif
}
