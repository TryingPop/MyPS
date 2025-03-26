using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 24
이름 : 배성훈
내용 : 마피아
    문제번호 : 1079번

    브루트포스 문제다.
    낮에 죽는 인원은 고정되어져 있다.
    밤에 한명씩 선택하며 최댓값을 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1460
    {

        static void Main1460(string[] args)
        {

            // 1079 - 마피아
            int n;
            int[] guilty;
            int[][] board;
            int mafia;

            Input();

            GetRet();
            void GetRet()
            {

                bool[] isDead = new bool[n];

                int ret = DFS(n);
                Console.Write(ret);

                int DFS(int _n)
                {

                    if (_n == 0) return 0;

                    int ret = 0;
                    if ((_n & 1) == 0)
                    {

                        for (int i = 0; i < n; i++)
                        {

                            if (isDead[i] || i == mafia) continue;
                            isDead[i] = true;
                            
                            for (int j = 0; j < n; j++)
                            {

                                if (isDead[j]) continue;
                                guilty[j] += board[i][j];
                            }
                            ret = Math.Max(ret, DFS(_n - 1));

                            for (int j = 0; j < n; j++)
                            {

                                if (isDead[j]) continue;
                                guilty[j] -= board[i][j];
                            }
                            isDead[i] = false;
                        }

                        ret++;
                    }
                    else
                    {

                        int max = -1_234;
                        int kill = -1;
                        for (int i = 0; i < n; i++)
                        {

                            if (isDead[i]) continue;
                            if (guilty[i] <= max) continue;
                            max = guilty[i];
                            kill = i;
                        }

                        isDead[kill] = true;
                        if (!isDead[mafia]) ret = DFS(_n - 1);
                        isDead[kill] = false;
                    }

                    return ret;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());
                guilty = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
                board = new int[n][];

                for (int i = 0; i < n; i++)
                {

                    board[i] = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
                }

                mafia = int.Parse(sr.ReadLine());
            }
        }
    }

#if other
// #include <stdio.h>
// #include <stdbool.h>
// #define NP 16
bool bAlive[NP];
int nSuspicion[NP], nMatrix[NP][NP], nMaxNights, N, nMafia;
void GetInput(){
	char sInput[1805];
	int ip = 0;
	fread(sInput, sizeof(char), 1800, stdin);
	while(sInput[ip] > 47) N = N * 10 + sInput[ip++] - 48;
	ip++;
	for(int i = 0;i < N;i++) {
		while(sInput[ip] > 47) nSuspicion[i] = nSuspicion[i] * 10 + sInput[ip++] - 48;
		ip++;
	}
	for(int i = 0;i < N;i++) {
		bAlive[i] = true;
		for(int j = 0; j < N;j++) {
			int sign = 1;
			if(sInput[ip] == '-') ip++, sign = -1;
			while(sInput[ip] > 47) nMatrix[i][j] = nMatrix[i][j] * 10 + sInput[ip++] - 48;
			nMatrix[i][j] *= sign;
			ip++;
		}
	}
	while(sInput[ip] > 47) nMafia = nMafia * 10 + sInput[ip++] - 48;
	return;
}
int PicktheSuspiciousOne(){
	int nOne = -1, sus = 0;
	for(int i = 0;i < N;i++) if(nSuspicion[i] > sus && bAlive[i]) nOne = i, sus = nSuspicion[i];
	return nOne;
}
void DFS_aNightandaDay(int days){
	int d1;
    if(nMaxNights == N/2) return;
	if(nMaxNights < days) nMaxNights = days;
	for(int i = 0;i < N;i++) {
		if(i == nMafia || !bAlive[i] ) continue;
		bAlive[i] = false;
		for(int j = 0;j < N;j++) nSuspicion[j] += nMatrix[i][j];
		d1 = PicktheSuspiciousOne();
		bAlive[d1] = false;
		if(bAlive[nMafia]) DFS_aNightandaDay(1 + days);
		bAlive[d1] = true;
		bAlive[i] = true;
		for(int j = 0;j < N;j++) nSuspicion[j] -= nMatrix[i][j];
	}
	return;
}
int main() {
	GetInput();
	if(N & 1) bAlive[PicktheSuspiciousOne()] = false;
	if(bAlive[nMafia]) DFS_aNightandaDay(1);
	printf("%d", nMaxNights);
}
#endif
}
