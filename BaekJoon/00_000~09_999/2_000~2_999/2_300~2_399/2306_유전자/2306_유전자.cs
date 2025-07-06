using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 5
이름 : 배성훈
내용 : 유전자
    문제번호 : 2306번

    dp 문제다.
    dp[i][j] = val를 i ~ j 사이의 문자열에 대해 가장 긴 길이를 val로 되게 한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1748
    {

        static void Main(string[] args)
        {

            string str;

            int n;
            int[][] dp;

            Input();

            SetStr();

            GetRet();

            void GetRet()
            {


#if first
                int ret = DFS(0, n - 1);
                

                int DFS(int _s, int _e)
                {

                    if (_e < _s) return 0;
                    else if (_s >= n || _e < 0) return 0;

                    ref int ret = ref dp[_s][_e];
                    if (ret != -1) return ret;
                    ret = 0;
                    // 양끝이 유전자 되는지 확인
                    if (ChkValid(_s, _e)) ret = 2 + DFS(_s + 1, _e - 1);
                    // 안되면 앞으로 1칸 뒤로 1칸씩 빼버리는 이동한다.
                    else ret = Math.Max(DFS(_s + 1, _e), DFS(_s, _e - 1));

                    for (int i = _s + 1; i <= _e; i++)
                    {

                        // 매칭이 되는 경우에 한해서만 띄엄띄엄 확인한다.
                        if (ChkValid(_s, i))
                            ret = Math.Max(ret, 2 + DFS(_s + 1, i - 1) + DFS(i + 1, _e));
                    }

                    return ret;

                    bool ChkValid(int _idx1, int _idx2)
                        => (str[_idx1] == 'a' && str[_idx2] == 't') 
                        || (str[_idx1] == 'g' && str[_idx2] == 'c');
                }
#else
                for (int len = 1; len < n; len++)
                {

                    for (int e = n - 1, s = e - len; s >= 0; s--, e--)
                    {

                        if ((str[s] == 'a' && str[e] == 't')
                            || (str[s] == 'g' && str[e] == 'c')) dp[s][e] = 2 + dp[s + 1][e - 1];

                        for (int m = s; m < e; m++)
                        {

                            dp[s][e] = Math.Max(dp[s][e], dp[s][m] + dp[m + 1][e]);
                        }
                    }
                }

                int ret = dp[0][n - 1];
#endif

                Console.Write(ret);
            }

            void SetStr()
            {

                n = str.Length;
                dp = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    dp[i] = new int[n];

#if first
                    Array.Fill(dp[i], -1);
#endif
                }

            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                str = sr.ReadLine();
            }
        }
    }

#if other
using System;

public class Program
{
    static void Main()
    {
        string dna = Console.ReadLine();
        int n = dna.Length;
        int[,] dp = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                dp[i, j] = -1;
            }
        }
        int DP(int l, int r)
        {
            if (l >= r)
                return 0;
            if (dp[l, r] != -1)
                return dp[l, r];
            int max = (dna[l] == 'a' && dna[r] == 't' || dna[l] == 'g' && dna[r] == 'c' ? 2 : 0) + DP(l + 1, r - 1);
            for (int i = l; i < r; i++)
            {
                max = Math.Max(max, DP(l, i) + DP(i + 1, r));
            }
            return dp[l, r] = max;
        }
        Console.Write(DP(0, n - 1));
    }
}
#elif other2
// #include <cstdio>
// #include <string.h>
char DNA[501];
short dp[501][501];
inline short max(short a, short b){return a>b ? a : b;}
int main(){
    scanf("%s", &DNA);
    int len=strlen(DNA);
    for(int i=1; i<=len; i++)
    for(int j=i-1; j>0; j--){
        if((DNA[j-1]=='a'&&DNA[i-1]=='t')||(DNA[j-1]=='g'&&DNA[i-1]=='c'))
            dp[j][i]=dp[j+1][i-1]+2;
        for(int k=j; k<i; k++)
            if(dp[j][i] < dp[j][k]+dp[k+1][i])
                dp[j][i]=dp[j][k]+dp[k+1][i];
    }
    printf("%d", dp[1][len]);
}

#endif

}
