using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 8
이름 : 배성훈
내용 : 카르텔 님 게임
    문제번호 : 30688번

    게임 이론 문제다
    ... DFS 로 A, B를 비교하면서 구현해봤는데, 1만 단위까지는 빠르게되나 100만 단위가면 시간초과 나온다
    규칙성으로 풀어야한다

    아이디어는 다음과 같다
    우선 돌을 가져가는 게임에서 N <= K인 경우 선턴이 이기는 것과 
    K + 1의 배수를 받았을 때 후발 주자가 1 ~ K개의 돌을 선택해서 가져갈 수 있다면
    후발 주자가 이기는 것은 자명하다

    선발 주자도 1 ~ K개를 가져간다면 K + 1의 배수로 만들어 이길 수 있다

    A, B 팀은 적어도 2개의 돌을 가져가야하므로 K + 1 개를 가져가는 전략을 취할 수 없다
    반면 C는 1개부터 가져갈 수 있으므로 K + 1개로 가져가는 전략을 취할 수 있다
    그래서 C에 초점을 맞춰 언제부터 K + 1로 항상 맞출 수 있는 경우는
    -> 2K + 2개인 경우다

    이후는 K = 6로하고 N을 2K + 2(=14)에서 3K + 2(20)까지의 경우로 보자
        14 : C는 합이 7개가 되게 가져가면 된다
        15 : C는 합이 8개가 되게 가져가면 된다 (A, B의 최소가 2이므로 8개가 된다!)
    그러면 결국 7개가되어, A, B가 어떻게해도 C가 이긴다

        16 : A, B가 가져가는 개수에 따라 가지치기를 해야한다
        A, B가 가져가서 13개 이하로 남으면 C는 7로 만들 수 있어 C가 이긴다
        이제 A, B가 2개만 가져가 남은게 14개인 경우는, C는 6개를 가져가면 된다
        그러면 A, B가 어떻게 가져가던 2 ~ 6이되고 다음턴에 C가 다가져가 이긴다

        17 ~ 20 : A, B가 가져가서 13 이하로 남은 경우, 7을 만들면 된다
        14 인 경우는 16에서의 경우와 같다
        15 이상인 경우는 14개가 되게만 가져가면 14인 경우와 일치하므로 C가 이긴다

    그러면 결국 2K + 2 ~ 3K + 2까지 확인했는데 C가 항상 이긴다
    이후는, C가 현재 수에서 K + 1 씩만 줄여가면서, 2K +2 ~ 3K + 2사이의 수로만 맞춰지고
    그래서 C가 이긴다

    이제 K + 3 ~ 2K + 1 이하를 보면
    여기는 A,B가 K + 1 개 C에게 넘겨줘서 C가 K + 1 전략에 당하는 구간이다
    K + 2 인 경우는, A, B 팀이 최소 2개를 가져가므로 K + 1로 못만들고 어떻게 가져가도 진다
    K + 1 인 경우도 K + 2와 같다

    모든 경우를 따져봤다
    그래서 N <= K, N + 3 <= K <= 2N +1 만 A, B가 이길 수 있다
    이외는 C가 다 이긴다!

    직접 해당 DFS로 여러 수를 검증해 봤다
*/

namespace BaekJoon.etc
{
    internal class etc_0163
    {

        static void Main163(string[] args)
        {

            string YES = "A and B win";
            string NO = "C win";

            StreamReader sr = new(Console.OpenStandardInput());

            int N = ReadInt();
            int K = ReadInt();

            sr.Close();

#if Find
            bool ret = false;
            if (N == K || (K + 3 <= N && N <= 2 * K + 1)) ret = true;

            Console.WriteLine(ret ? YES : NO);
#else

            int[] team1 = new int[N + 1];
            int[] team2 = new int[N + 1];
            bool ret = DFSTeam1(0);

            Console.WriteLine(ret ? NO : YES);

            bool DFSTeam1(int _n)
            {

                if (_n >= N) return true;
                if (team1[_n] != 0) return team1[_n] == 1;
                for (int i = K; i >= 2; i--)
                {

                    if (DFSTeam2(_n + i))
                    {

                        team1[_n] = -1;
                        return false;
                    }
                }

                team1[_n] = 1;
                return true;
            }

            bool DFSTeam2(int _n)
            {

                if (_n >= N) return true;
                if (team2[_n] != 0) return team2[_n] == 1;

                for (int i = K; i >= 1; i--)
                {

                    if (DFSTeam1(_n + i))
                    {

                        team2[_n] = -1;
                        return false;
                    }
                }

                team2[_n] = 1;
                return true;
            }
#endif

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
using System;
using System.IO;
using System.Linq;

// #nullable disable

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var nk = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var n = nk[0];
        var k = nk[1];

        var isCWinning = n == k + 1 || n == k + 2 || n >= 2 * k + 2;
        sw.WriteLine(isCWinning ? "C win" : "A and B win");

        //var isABWinning = new (bool winning, int move)[1 + n];
        //for (var stone = 1; stone <= n; stone++)
        //{
        //    // trivial case
        //    if (stone <= k)
        //    {
        //        isABWinning[stone] = (true, stone);
        //    }
        //    else
        //    {
        //        for (var rem1 = 2; rem1 <= k; rem1++)
        //        {
        //            var w = true;

        //            if (stone - rem1 <= k)
        //                w = false;
        //            else
        //                for (var rem2 = 1; rem2 <= k; rem2++)
        //                    w &= isABWinning[stone - rem1 - rem2].winning;

        //            if (w)
        //            {
        //                isABWinning[stone] = (w, rem1);
        //                break;
        //            }
        //        }
        //    }
        //}

    }
}

#endif
}
