using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 10
이름 : 배성훈
내용 : 지그재그 서기
    문제번호 : 1146번

    dp, 조합론 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1758
    {

        static void Main1758(string[] args)
        {


#if TRASHCODE

            int[] ret = { 0, 1, 2, 4, 10, 32, 122, 544, 2770, 15872, 101042, 707584, 405530, 736512, 721962, 514624, 24290, 685952, 350882, 225664, 475050, 248192, 275802, 704704, 173810, 967232, 784722, 775744, 936570, 947072, 365642, 622784, 579330, 251712, 906562, 189824, 518090, 305152, 895482, 580864, 368850, 291392, 620402, 459904, 547610, 554432, 369322, 450944, 870370, 998272, 630242, 537984, 153130, 486912, 291162, 465024, 811890, 244352, 840082, 136064, 462650, 254592, 565002, 15104, 521410, 661632, 753922, 526144, 804170, 169472, 894842, 453184, 126930, 442112, 875762, 340224, 905690, 503552, 184682, 891264, 756450, 137792, 109602, 370304, 95210, 288832, 938522, 1344, 737970, 460672, 159442, 368384, 500730, 117312, 660362, 815424, 799490, 82752, 929282, 846464, 250250 };
            int n = int.Parse(Console.ReadLine());
            Console.Write(ret[n]);
#elif first
            long MOD = 1_000_000;

            int n = int.Parse(Console.ReadLine());
            if (n == 1)
            {

                Console.Write(1);
                return;
            }

            long[][][][] dp = new long[n + 1][][][];
            for (int i = 0; i <= n; i++)
            {

                dp[i] = new long[n + 1][][];

                for (int j = 0; j <= n; j++)
                {

                    dp[i][j] = new long[n + 1][];

                    for (int k = 0; k <= n; k++)
                    {

                        dp[i][j][k] = new long[2];
                        Array.Fill(dp[i][j][k], -1);
                    }
                }
            }

            long ret = 0;
            for (int i = 1; i <= n; i++)
            {

                for (int j = 0; j < 2; j++)
                {

                    ret = (ret + DFS(1, i - 1, n - i, j)) % MOD;
                }
            }

            Console.Write(ret);

            long DFS(int _dep, int _small, int _big, int _type)
            {

                ref long ret = ref dp[_dep][_small][_big][_type];
                if (ret != -1) return ret;
                ret = 0;

                if (_dep == n)
                {

                    // 정상적으로 모든 문자를 사용한 경우만 1개 카운팅
                    if (_small == 0 && _big == 0) ret = 1;
                    return ret;
                }

                if (_type == 0)
                {

                    // 현재 값보다 작은걸 선택
                    for (int i = _small - 1; i >= 0; i--)
                    {

                        // 선택한 것보다 큰 것 갯수 수정
                        ret = (ret + DFS(_dep + 1, i, _big + (_small - i - 1), 1)) % MOD;
                    }
                }
                else
                {

                    // 현재 값보다 큰 걸 선택
                    for (int i = _big - 1; i >= 0; i--)
                    {

                        // 선택한 것보다 작은거 갯수 수정
                        ret = (ret + DFS(_dep + 1, _small + (_big - i - 1), i, 0)) % MOD;
                    }
                }

                return ret;
            }
#else
            long MOD = 1_000_000;

            int n = int.Parse(Console.ReadLine());
            if (n == 1)
            {

                Console.Write(1);
                return;
            }

            long[][][] dp = new long[n + 1][][];
            for (int i = 0; i <= n; i++)
            {

                dp[i] = new long[n + 1][];

                for (int j = 0; j <= n; j++)
                {

                    dp[i][j] = new long[2];
                    Array.Fill(dp[i][j], -1);
                }
            }

            long ret = 0;
            for (int i = 1; i <= n; i++)
            {

                for (int j = 0; j < 2; j++)
                {

                    ret = (ret + DFS(1, i - 1, n - i, j)) % MOD;
                }
            }

            Console.Write(ret);

            long DFS(int _dep, int _small, int _big, int _type)
            {

                ref long ret = ref dp[_small][_big][_type];
                if (ret != -1) return ret;
                ret = 0;

                if (_dep == n)
                {

                    // 정상적으로 모든 문자를 사용한 경우만 1개 카운팅
                    if (_small == 0 && _big == 0) ret = 1;
                    return ret;
                }

                if (_type == 0)
                {

                    for (int i = _small - 1; i >= 0; i--)
                    {

                        ret = (ret + DFS(_dep + 1, i, _big + (_small - i - 1), 1)) % MOD;
                    }
                }
                else
                {

                    for (int i = _big - 1; i >= 0; i--)
                    {

                        ret = (ret + DFS(_dep + 1, _small + (_big - i - 1), i, 0)) % MOD;
                    }
                }

                return ret;
            }
#endif
        }
    }

#if other
// #include <stdio.h>
int m=1000000;
int n;
int a[999],s[999],i,j;
int main(){scanf("%d",&n);if(n==1)printf("1");else if(n==2)printf("2");else{
	s[2]=s[1]=1;
	for(i=3;i<=n;i++)
	{
		for(j=1;j<i-1;j++)
		{
			a[i-j]=s[j];
		}
		s[i]=0;
		for(j=i-1;j>0;j--){
			s[j]=(s[j+1]+a[j])%m;
		}
	}
	j=0;
	for(i=1;i<n;i++){
		j=(j+2*a[i]*i)%m;
	}
	printf("%d",j);
}}
#elif other2
using System;
using System.IO;
using System.Numerics;
namespace System {}
namespace System.IO {}
namespace System.Numerics {}

// #nullable disable

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        Solve(sr, sw);
    }

    public static void Solve(StreamReader sr, StreamWriter sw)
    {
        var n = Int32.Parse(sr.ReadLine());
        if (n == 1)
        {
            sw.WriteLine(1);
            return;
        }

        var mod = 2_000_000;
        var comb = new long[200, 200];

        for (var a = 0; a < 200; a++)
            for (var b = 0; b < 200; b++)
            {
                if (a < b)
                {
                    comb[a, b] = 0;
                }
                else if (a == 0)
                {
                    comb[a, b] = (b == 0 ? 1 : 0);
                }
                else if (b == 0)
                {
                    comb[a, b] = 1;
                }
                else
                {
                    comb[a, b] = (comb[a - 1, b - 1] + comb[a - 1, b]) % mod;
                }
            }

        var f = new long[1 + n];

        // 지그재그 서기 중 마지막이 증가하는 순인 것의 갯수
        // = 지그재기 서기 중 마지막이 감소하는 순인 것의 갯수
        // - g(1) = 1
        // - g(n) = f(n)/2 (n>=2)
        var g = new long[1 + n];

        f[0] = 1;
        f[1] = 1;
        g[0] = 1;
        g[1] = 1;

        for (var idx = 2; idx <= n; idx++)
        {
            for (var pivot = 1; pivot <= idx; pivot++)
            {
                var left = pivot - 1;
                var right = idx - pivot;

                f[idx] += ((comb[idx - 1, left] * g[left]) % mod * g[right]) % mod;
            }

            g[idx] = f[idx] / 2;
        }

        sw.WriteLine(f[^1] % 1_000_000);
    }
}
#endif
}
