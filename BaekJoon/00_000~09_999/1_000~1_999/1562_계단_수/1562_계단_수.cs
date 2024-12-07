using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 7
이름 : 배성훈
내용 : 계단 수
    문제번호 : 1562번

    dp, 비트마스킹 문제다.
    아이디어는 다음과 같다.
    계단 수는 이전 계단수에서 앞자리에 끼워 넣는다.
    그리고 계단수에 사용된 숫자도 함께 기록한다.
    이에 dp[i][j][k] = val를 길이 i이고 앞의 숫자가 j이며
    사용된 숫자 상태를 k이고 val는 해당 계단수의 개수가 된다.

    그러면 점화식이
    dp[i][j][k] = dp[i - 1][j - 1][...] + dp[i - 1][j][...] + dp[i - 1][j + 1][...] 이 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1165
    {

        static void Main1165(string[] args)
        {

#if !FIND
            int MOD = 1_000_000_000;
            int[][][] dp;

            int n = int.Parse(Console.ReadLine());
            dp = new int[101][][];
            for (int i = 0; i <= 100; i++)
            {

                dp[i] = new int[10][];
                for (int j = 0; j < 10; j++)
                {

                    dp[i][j] = new int[1 << 10];
                }
            }

            for (int j = 1; j < 10; j++)
            {

                dp[1][j][1 << j] = 1;
            }

            for (int i = 2; i <= 100; i++)
            {

                for (int j = 0; j < 10; j++)
                {

                    int len = 1 << 10;
                    for (int k = 0; k < len; k++)
                    {

                        if (j >= 1)
                            dp[i][j][k | (1 << j)]
                                = (dp[i][j][k | (1 << j)] + dp[i - 1][j - 1][k]) % MOD;
                        if (j <= 8)
                            dp[i][j][k | (1 << j)] 
                                = (dp[i][j][k | (1 << j)] + dp[i - 1][j + 1][k]) % MOD;
                    }
                }
            }


            int ret = 0;
            for (int j = 0; j < 10; j++)
            {

                ret = (ret + dp[n][j][(1 << 10) - 1]) % MOD;
            }

            Console.Write(ret);

#else
            int[] dp = new int[101] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 3, 14, 37, 117, 287, 770, 1800, 4420, 9994, 23249, 51307, 115156, 249487, 546042, 1166004, 2505150, 5287425, 11200451, 23414399, 49050151, 101720223, 211185500, 434991097, 896444595, 835729973, 759735100, 660371284, 606485403, 657835457, 203722859, 732382676, 62947013, 711953864, 282534432, 532704326, 533430656, 490303185, 719078464, 867439170, 928508497, 826187014, 717575892, 566595461, 320670363, 655531616, 186881287, 277343131, 272775098, 352307918, 913419265, 227796381, 837613318, 359667139, 624615740, 709094500, 932384013, 157643024, 646028283, 396360486, 169463574, 365015614, 942074982, 423530743, 451163035, 615483377, 21727607, 324705955, 306613175, 326811178, 606166739, 701478626, 613754961, 205272431, 472587513, 735855947, 910895829, 510318720, 529015082, 557887447, 384840782, 896423761, 280533566, 960515389, 301710744, 609183295, 940630118, 923402943, 431914107, 270442392, 670667793 };
            int n = int.Parse(Console.ReadLine());

            Console.Write(dp[n]);
#endif
        }
    }

#if other
using System;

var n = int.Parse(Console.ReadLine()!);
const int Digit = 10;
const int BitMax = 1 << Digit;

var pre = new int[Digit, BitMax];
var cur = new int[Digit, BitMax];
for (int i = 0; i < Digit; i++)
    pre[i, 1 << i] = 1;

const int Mod = 1_000_000_000;
for (int i = 1; i < n; i++)
{
    for (int lastNum = 0; lastNum < Digit; lastNum++)
    {
        for (int preBit = 1; preBit < BitMax; preBit++)
        {
            var preNum = pre[lastNum, preBit];
            if (preNum == 0)
                continue;

            if (lastNum < Digit - 1)
                Update(lastNum + 1);
            if (lastNum > 0)
                Update(lastNum - 1);

            void Update(int newNum)
            {
                var newBit = preBit | (1 << newNum);
                ref var curNum = ref cur[newNum, newBit];
                curNum += preNum;
                curNum %= Mod;
            }
        }
    }
    (pre, cur) = (cur, pre);
    for (int lastNum = 0; lastNum < Digit; lastNum++)
        for (int preBit = 1; preBit < BitMax; preBit++)
            cur[lastNum, preBit] = 0;
}

long ret = 0;
for (int i = 1; i <= Digit - 1; i++)
    ret += pre[i, BitMax - 1];
ret %= Mod;
Console.Write(ret);
#endif
}
