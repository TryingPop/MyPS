using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 16
이름 : 배성훈
내용 : 리조트
    문제번호 : 13302번

    dp 문제다.
    dp[i][j] = val를 i일에 쿠폰 j개를 가질 때 최솟값을 val로 해서
    dp를 설정하면 된다.
    n = 1인 반례를 처리 못해 한번 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_1194
    {

        static void Main1194(string[] args)
        {
            int MAX = 5;
            int INF = 5_000_000;
            int n, m;
            int[][] dp;
            int[] price, coupon, day;
            bool[] pass;


            Solve();
            void Solve()
            {

                Input();

                SetArr();

                GetRet();
            }

            void GetRet()
            {

                Console.Write(DFS());

                int DFS(int _day = 1, int _coupon = 0)
                {

                    ref int ret = ref dp[_day][_coupon];
                    if (ret != -1) return ret;
                    else if (_day > n) return ret = 0;
                    ret = INF;

                    if (pass[_day]) ret = Math.Min(ret, DFS(_day + 1, _coupon));
                    if (_coupon >= 3) ret = Math.Min(ret, DFS(_day + 1, _coupon - 3));

                    for (int i = 0; i < 3; i++)
                    {

                        ret = Math.Min(ret, DFS(_day + day[i], _coupon + coupon[i]) + price[i]);
                    }

                    return ret;
                }
            }

            void SetArr()
            {

                price = new int[3];
                price[0] = 10_000;
                price[1] = 25_000;
                price[2] = 37_000;

                coupon = new int[3];
                coupon[1] = 1;
                coupon[2] = 2;

                day = new int[3];
                day[0] = 1;
                day[1] = 3;
                day[2] = 5;

                dp = new int[n + MAX + 1][];
                for (int i = 0; i < dp.Length; i++)
                {

                    dp[i] = new int[n + 2];
                    Array.Fill(dp[i], -1);
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();
                pass = new bool[n + 1];
                for (int i = 0; i < m; i++)
                {

                    pass[ReadInt()] = true;
                }

                sr.Close();

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) { }
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == ' ' || c == '\n') return true;

                        ret = c - '0';
                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }
}
