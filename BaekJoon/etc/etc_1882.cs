using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 12
이름 : 배성훈
내용 : 누울 자리를 찾아라
    문제번호 : 1652번

    구현 문제다.
    문제에서 요구하는 것은 연속으로 2칸이 빈 공간인 개수를 찾아야 한다.
    예를들어 빈 공간을 '.' 막힌 곳을 'X'라하면
    ..X..인 해당 행에서 빈 공간은 '..' X '..' 으로 2개이다.
    .인 경우 연달아 카운트 해주면 된다.

    가로 세로 순서로 각각 확인해야 하므로
    전체 시간 복잡도는 맵의 크기 X 2이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1882
    {

        static void Main1882(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());
            string[] board = new string[n];
            for (int i = 0; i < n; i++)
            {

                board[i] = sr.ReadLine();
            }

            int[] dp = new int[n];
            int ret1 = 0;
            int ret2 = 0;
            for (int i = 0; i < n; i++)
            {

                int cur = 0;
                for (int j = 0; j < n; j++)
                {

                    if (board[i][j] == '.') cur++;
                    else 
                    {

                        if (cur > 1) ret1++;
                        cur = 0; 
                    }
                }

                if (cur > 1) ret1++;
                cur = 0;
                for (int j = 0; j < n; j++)
                {

                    if (board[j][i] == '.') cur++;
                    else 
                    {

                        if (cur > 1) ret2++;
                        cur = 0; 
                    }
                }
                if (cur > 1) ret2++;
            }

            Console.Write($"{ret1} {ret2}");
        }
    }
}
