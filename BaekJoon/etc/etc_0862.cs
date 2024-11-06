using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 5
이름 : 배성훈
내용 : 우표 구매하기 (Easy)
    문제번호 : 27715번
*/

namespace BaekJoon.etc
{
    internal class etc_0862
    {

        static void Main862(string[] args)
        {

            int n, m, k, p;
            int[][] dp;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {


            }

            void Combi(int n , int r)
            {


            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);
                k = int.Parse(temp[2]);
                p = int.Parse(temp[3]);

                dp = new int[2][];
                dp[0] = new int[k + 1];
                dp[1] = new int[k + 1];
            }
        }
    }
}
