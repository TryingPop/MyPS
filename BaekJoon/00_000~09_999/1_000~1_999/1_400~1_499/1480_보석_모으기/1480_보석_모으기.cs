using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 24
이름 : 배성훈
내용 : 보석 모으기
    문제번호 : 1480번

    dp, 비트마스킹 문제다.
    보석의 선택여부를 비트마스킹으로 나타낸 뒤 확인하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1456
    {

        static void Main1456(string[] args)
        {

            int n, m, c;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int[][][] dp = new int[m][][];
                for (int i = 0; i < m; i++)
                {

                    dp[i] = new int[c + 1][];
                    for (int j = 0; j <= c; j++)
                    {

                        dp[i][j] = new int[1 << n];
                        Array.Fill(dp[i][j], -1);
                    }
                }

                int E = (1 << n) - 1;

                int ret = DFS();
                Console.Write(ret);

                int DFS(int _bag = 0, int _weight = 0, int _state = 0)
                {

                    if (_bag == m) return -1;
                    else if (_state == E) return 0;

                    ref int ret = ref dp[_bag][_weight][_state];
                    if (ret != -1) return ret;

                    ret = 0;

                    for (int i = 0; i < n; i++)
                    {

                        if ((_state & (1 << i)) > 0) continue;
                        int curWeight = arr[i];
                        if (curWeight > c) continue;

                        int nextState = _state | (1 << i);
                        int chk;
                        if (curWeight + _weight > c)
                            chk = DFS(_bag + 1, curWeight, nextState);
                        else
                            chk = DFS(_bag, _weight + curWeight, nextState);

                        ret = Math.Max(ret, chk + 1);
                    }

                    return ret;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string[] temp = sr.ReadLine().Split();

                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);
                c = int.Parse(temp[2]);

                temp = sr.ReadLine().Split();

                arr = Array.ConvertAll(temp, int.Parse);
            }

        }
    }

#if other
// #nullable disable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var nmc = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var n = nmc[0];
        var m = nmc[1];
        var c = nmc[2];

        var weight = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

        // dp[jewel mask, remained empty bag count, empty space of current bag] = possiblity
        var dp = new bool[1 << n, m, 1 + c];
        var max = 0;

        dp[0, m - 1, c] = true;
        for (var mask = 0; mask < (1 << n); mask++)
        {
            for (var jewelIndex = 0; jewelIndex < n; jewelIndex++)
            {
                // Already taken
                if ((mask & (1 << jewelIndex)) != 0)
                    continue;

                var w = weight[jewelIndex];
                var newmask = mask | (1 << jewelIndex);

                for (var unopenedBagCount = 0; unopenedBagCount < m; unopenedBagCount++)
                    for (var remainedSpace = 0; remainedSpace <= c; remainedSpace++)
                    {
                        if (!dp[mask, unopenedBagCount, remainedSpace])
                            continue;

                        // open new bag
                        if (unopenedBagCount != 0 && c >= w)
                        {
                            dp[newmask, unopenedBagCount - 1, c - w] = true;
                            max = Math.Max(max, BitOperations.PopCount((uint)newmask));
                        }

                        // push to bag
                        if (remainedSpace >= w)
                        {
                            dp[newmask, unopenedBagCount, remainedSpace - w] = true;
                            max = Math.Max(max, BitOperations.PopCount((uint)newmask));
                        }
                    }
            }
        }

        sw.WriteLine(max);
    }
}

#endif
}
