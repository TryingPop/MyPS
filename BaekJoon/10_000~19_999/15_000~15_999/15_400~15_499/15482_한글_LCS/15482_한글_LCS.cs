using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025.7. 27
이름 : 배성훈
내용 : 한글 LCS
    문제번호 : 15482번

    dp, utf-8 처리 문제다.
    
*/

namespace BaekJoon.etc
{
    internal class etc_1788
    {

        static void Main1788(string[] args)
        {

            string f, t;

            Input();

            GetRet();

            void GetRet()
            {

                int[][] lcs = new int[f.Length + 1][];
                for (int i = 0; i < lcs.Length; i++)
                {

                    lcs[i] = new int[t.Length + 1];
                    Array.Fill(lcs[i], -1);
                }

                DFS();

                Console.Write(lcs[0][0]);

                int DFS(int _f = 0, int _t = 0)
                {

                    if (_f == f.Length || _t == t.Length) return 0;
                    ref int ret = ref lcs[_f][_t];
                    if (ret != -1) return ret;
                    ret = 0;

                    if (f[_f] == t[_t]) ret = DFS(_f + 1, _t + 1) + 1;
                    else ret = Math.Max(DFS(_f + 1, _t), DFS(_f, _t + 1));

                    // ret = Math.Max(DFS(_f + 1, _t), DFS(_f, _t + 1));
                    // if (f[_f] == t[_t]) ret = Math.Max(ret, DFS(_f + 1, _t + 1) + 1);

                    return ret;
                }
            }

            void Input()
            {

                // Encoding을 Console에 저장된 기본 인코딩으로 한다.
                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536, encoding: Console.InputEncoding);
                
                f = sr.ReadLine();
                t = sr.ReadLine();
            }
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace c_pro
{
    class _9251
    {
        static void Main()
        {
            string LcsA = Console.ReadLine();
            string LcsB = Console.ReadLine();
            if (LcsA.Length < LcsB.Length)
            {
                var temp = LcsA;
                LcsA = LcsB;
                LcsB = temp;
            }

            int[] dp = new int[LcsB.Length + 1];


            for (int i = 1; i <= LcsA.Length; i++)
            {
                int prev = 0;
                for (int j = 1; j <= LcsB.Length; j++)
                {
                    int temp = dp[j];
                    if (LcsA[i - 1] == LcsB[j - 1])
                        dp[j] = prev + 1;
                    else
                        dp[j] = Math.Max(dp[j], dp[j - 1]);
                    prev = temp;
                }
            }

            Console.WriteLine(dp[LcsB.Length]);
        }
    }
}
#elif other2
// #include <iostream>
// #include <vector>
using namespace std;

int main(){
    int c;
    vector<int> str1;
    vector<int> str2;

    while(1){
        c = getchar();
        if(c == '\n') break;

        c = (c & 0x0f) << 12;
        c |= (getchar() & 0x3f) << 6;
        c |= (getchar() & 0x3f);

        str1.push_back(c);
    }

    while(1){
        c = getchar();
        if(c == EOF) break;

        c = (c & 0x0f) << 12;
        c |= (getchar() & 0x3f) << 6;
        c |= (getchar() & 0x3f);

        str2.push_back(c);
    }
    
    
    vector<int> dp(str2.size()+1, 0);
    vector<int> ndp(str2.size()+1, 0);

    for(int i = 1; i <= str1.size(); i++){
        for(int j = 1; j <= str2.size(); j++){
            if(str1[i-1] == str2[j-1]) ndp[j] = dp[j-1]+1;
            else ndp[j] = (ndp[j-1] < dp[j] ? dp[j] : ndp[j-1]);
        }
        swap(dp, ndp);
    }

    cout << dp[str2.size()];
}
#elif other3
using System;

#nullable disable

public class Program
{
    public static void Main()
    {
        //using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536, encoding: Encoding.UTF8);
        //using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536, encoding: Encoding.UTF8);

        var s1 = Console.ReadLine();
        var s2 = Console.ReadLine();

        var max = 0;
        var dp = new int[1 + s1.Length, 1 + s2.Length];

        for (var i1 = 1; i1 <= s1.Length; i1++)
            for (var i2 = 1; i2 <= s2.Length; i2++)
            {
                if (s1[i1 - 1] == s2[i2 - 1])
                    dp[i1, i2] = 1 + dp[i1 - 1, i2 - 1];
                else
                    dp[i1, i2] = Math.Max(dp[i1, i2 - 1], dp[i1 - 1, i2]);

                max = Math.Max(max, dp[i1, i2]);
            }

        Console.WriteLine(max);
    }
}
#endif
}
