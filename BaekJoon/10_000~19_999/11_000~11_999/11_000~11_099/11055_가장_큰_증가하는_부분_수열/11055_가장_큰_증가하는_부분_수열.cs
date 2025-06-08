using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 15
이름 : 배성훈
내용 : 가장 큰 증가하는 부분 수열
    문제번호 : 11055번

    dp 문제다.
    크기가 1_000으로 작아 N^2의 방법을 택했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1338
    {

        static void Main1338(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();
            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
            }

            int[] max = new int[n];
            for (int i = 0; i < n; i++)
            {

                int chk = 0;
                for (int j = 0; j < i; j++)
                {

                    if (arr[i] <= arr[j]) continue;
                    chk = Math.Max(chk, max[j]);
                }

                max[i] = chk + arr[i];
            }

            int ret = 0;
            for (int i = 0; i < n; i++)
            {

                ret = Math.Max(max[i], ret);
            }

            Console.Write(ret);

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
// #include <cstdio>
int main(){
	int m,N,D[1002],A[1002],i,j;
	scanf("%d",&N);
	for(i=0;i<N;i++)scanf(" %d",&A[i]);
	D[0]=A[0];
	m=A[0];
	for(i=0; i<N; i++){
		D[i]=A[i];
		for(j=0; j<i; j++){
			if(A[i] > A[j]) if(D[i] < D[j]+A[i])D[i] = D[j]+A[i];
			m =m>D[i]?m:D[i];}}
	printf("%d", m);
}

#endif
}
