using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 12
이름 : 배성훈
내용 : Four Squares
    문제번호 : 17626번

    dp, 브루트 포스 문제다
    제곱수의 합 (More Huge) 문제와 같아 해당 방법처럼 해결했다
    다른 사람 아이디어를 보니 dp로 풀 수 있었다
*/

namespace BaekJoon.etc
{
    internal class etc_1056
    {

        static void Main1056(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            if (ChkFour(n)) Console.Write(4);
            else if (ChkThree(n)) Console.Write(3);
            else if (ChkOne(n)) Console.Write(1);
            else Console.Write(2);

            bool ChkFour(int _chk)
            {

                
                while (_chk % 4 == 0) { _chk /= 4; }
                return _chk % 8 == 7;
            }

            bool ChkThree(int _chk)
            {

                int[] div = new int[50];
                int[] cnt = new int[50];

                int len = 0;

                GetDiv(_chk);

                for (int i = 0; i < len; i++)
                {

                    if (div[i] % 4 == 3 && cnt[i] % 2 == 1) return true;
                }

                return false;

                void GetDiv(int _chk)
                {

                    for (int i = 2; i * i <= _chk; i++)
                    {

                        if (_chk % i == 0) div[len++] = i;
                        while (_chk % i == 0)
                        {

                            _chk /= i;
                            cnt[len - 1]++;
                        }
                    }

                    if (_chk > 1) 
                    { 
                        
                        div[len] = _chk;
                        cnt[len++] = 1;
                    }
                }
            }

            bool ChkOne(int _chk)
            {

                int sq = (int)(Math.Sqrt(_chk) + 1e-9);
                return sq * sq == _chk;
            }
        }
    }

#if other
int n = int.Parse(Console.ReadLine());

int[] dp = new int [n + 1];

for (int i = 1; i <= n; i++)
{
    dp[i] = dp[i - 1] + 1;

    for (int j = 1; j * j <= i; j++)
    {
        dp[i] = Math.Min(dp[i], dp[i - j * j] + 1);
    }
}

Console.Write(dp[n]);
#endif
}
