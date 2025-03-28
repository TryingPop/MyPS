using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 27
이름 : 배성훈
내용 : 사람의 수
    문제번호 : 1206번

    브루트포스 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1474
    {

        static void Main1474(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());

            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {

                // 0.000 -> 형태로 주어지기에 정수로 변환
                string str = sr.ReadLine();
                arr[i] = ReadNum(str);
            }

            // 최대 범위
            int[] dp = new int[10_001];

            // 0.001단위이기에 1000명이면 무조건 가능하다!
            // 그래서 1000이하에서만 조사한다.
            int ret = 1_000;
            // cur은 현재 인원이다. 
            for (int cur = 1; cur < 1_000; cur++)
            {

                // 최대 점수
                int e = cur * 10_000;
                for (int i = 0; i <= e; i+= 1_000)
                {

                    // 가능한 점수를 dp에 기록
                    // HashSet처럼 쓴다.
                    int idx = i / cur;
                    dp[idx] = cur;
                }

                // 모두 만족하는지 확인용 flag
                bool flag = false;
                for (int i = 0; i < n; i++)
                {

                    if (dp[arr[i]] == cur) continue;
                    // 존재안하는 경우
                    flag = true;
                    break;
                }

                if (flag) continue;
                // 발견하면 최솟값이므로 바로 탈출
                ret = cur;
                break;
            }

            Console.Write(ret);

            int ReadNum(string _str)
            {

                // a.bcd 를 abcd 정수로 변환
                int ret = 0;
                for (int i = 0; i < _str.Length; i++)
                {

                    if (i == 1) continue;
                    ret = ret * 10 + _str[i] - '0';
                }

                return ret;
            }
        }
    }

#if other
// #include <stdio.h>
// #include <math.h>
int Number(int n, int a, int b, int c)     // x/n==0.abc? 
{
	int h=floor((a/10.0+b/100.0+c/1000.0)*n+0.9995);  //total score
	h*=1000;
	h/=n;
	if(h%10!=c) return 0;
	h/=10;
	if(h%10!=b) return 0;
	h/=10;
	if(h%10!=a) return 0;
	return 1;
}
int main()
{
	int N;
	int ans=1;
	int clear=1;
	int i=0, j=0;
	char a[101][7];
	
	scanf("%d", &N);
	for (i=0;i<N;i++)  //array init
	{
		for (j=0;j<6;j++)
		a[i][j]='0';
	}
	for (i=0;i<N;i++)
	{
		scanf("%s", a[i]);   //   x.abc
		if(a[i][2]=='.') a[i][2]='0';  //10.000
	}
		
	while(1)
	{
		clear=1;
		for(i=0;i<N;i++)
		{
			if (Number(ans, a[i][2]-'0', a[i][3]-'0', a[i][4]-'0')==0) clear=0;
		}
		if (clear==1) break;
		ans++;
	}
	printf("%d", ans);
}
#endif
}
