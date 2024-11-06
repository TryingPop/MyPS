using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 25
이름 : 배성훈
내용 : 전구 상태 뒤집기
    문제번호 : 25634번

    dp, 누적합 문제다
    그리디하게 해결했다
    현재 초기 합을 구하고,
    반전 시켰을 때, 가장 큰 값을 찾아 차이를 더해주었다
*/

namespace BaekJoon.etc
{
    internal class etc_0616
    {

        static void Main616(string[] args)
        {

            StreamReader sr;
            int n;
            int[] pow;
            int[] on;
            int ret = 0;

            Solve();

            void Solve()
            {

                Input();
                int diff = -5_000;
                int cur = 0;
                for (int i = 0; i < n; i++)
                {

                    int add = on[i] * pow[i];
                    if (add + cur >= add) cur += add;
                    else cur = add;

                    if (diff < cur) diff = cur;
                }

                ret += diff;
                Console.WriteLine(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput());
                n = ReadInt();

                pow = new int[n];
                on = new int[n];

                for (int i = 0; i < n; i++)
                {

                    pow[i] = ReadInt();
                }

                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    ret += cur * pow[i];
                    on[i] = cur == 0 ? 1 : -cur;
                }

                sr.Close();
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

#if other
int N = int.Parse(Console.ReadLine());
int[] light = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
int[] onoff = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

int init = 0;
for (int i = 0; i < N; i++)
{
    if (onoff[i] == 1)
    {
        init += light[i];
        light[i] *= -1;
    }
}

int[] sum = new int[N];
int subsum = 0;
int max = int.MinValue;
for(int i = 0; i < N; i++)
{
    if (subsum > 0) subsum += light[i];
    else subsum = light[i];

    if (max < subsum) 
        max = subsum;
}

Console.WriteLine(init + max);
#endif
}
