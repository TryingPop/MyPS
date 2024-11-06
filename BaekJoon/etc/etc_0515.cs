using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 12
이름 : 배성훈
내용 : 색종이
    문제번호 : 2590번

    그리디 알고리즘, 많은 조건분기 문제다
    4, 5, 6은 그리디 하게 접근하면 된다
    종이 3개와 2개에서 조건이 많이 갈린다
    그래서, 해당 부분을 구현해보려고 했으나 2번 틀려서,
    범위가 100이라 100 * 100 * 100 = 100만 공간이라 dp를 이용해 풀었다
    다른 사람 풀이를 확인해보니, 큰거부터 없애면 되는거 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0515
    {

        static void Main515(string[] args)
        {

            int p1 = int.Parse(Console.ReadLine());
            int p2 = int.Parse(Console.ReadLine());
            int p3 = int.Parse(Console.ReadLine());
            int p4 = int.Parse(Console.ReadLine());
            int p5 = int.Parse(Console.ReadLine());
            int p6 = int.Parse(Console.ReadLine());

            int ret = p6;
            ret += p5;
            p1 -= p5 * 11;
            if (p1 < 0) p1 = 0;

            ret += p4;
            if (p2 >= p4 * 5)
            {

                p2 -= p4 * 5;
            }
            else
            {

                p1 += 4 * p2;
                p2 = 0;
                p1 -= 20 * p4;
                if (p1 < 0) p1 = 0;
            }
#if first
            int[,,] dp = new int[p3 + 1, p2 + 1, p1 + 1];
            dp[0, 0, 0] = 1;

            int[] diff = { 5, 3, 1, 0 };
            ret += DFS(p3, p2, p1);
            Console.WriteLine(ret - 1);

            int DFS(int _p3, int _p2, int _p1)
            {

                _p1 = _p1 < 0 ? 0 : _p1;
                _p2 = _p2 < 0 ? 0 : _p2;
                _p3 = _p3 < 0 ? 0 : _p3;

                if (dp[_p3, _p2, _p1] != 0) return dp[_p3, _p2, _p1];

                int ret = 10_000;
                dp[_p3, _p2, _p1] = ret;

                if (_p2 == 0 && _p3 == 0)
                {

                    ret = _p1 / 36;
                    if (_p1 % 36 > 0) ret++;
                    ret++;
                }
                else if (_p3 == 0)
                {

                    int calc = _p1 + _p2 * 4;
                    ret = calc / 36;
                    if (calc % 36 > 0) ret++;
                    ret++;
                }
                else
                {

                    for (int i = 0; i < 4; i++)
                    {

                        int chkP2;
                        int chkP1;
                        if (_p2 <= diff[i])
                        {

                            chkP2 = 0;
                            chkP1 = _p1 + 4 * _p2;
                        }
                        else 
                        { 

                            chkP2 = _p2 - diff[i];
                            chkP1 = _p1 + diff[i] * 4;
                        }

                        chkP1 -= (27 - 9 * i);
                        int calc = 1 + DFS(_p3 - i - 1, chkP2, chkP1);
                        ret = calc < ret ? calc : ret;
                    }
                }

                return dp[_p3, _p2, _p1] = ret;
            }
#else

            ret += p3 / 4;
            p3 %= 4;

            if (p3 == 1)
            {

                ret++;
                p3 = 0;
                p2 -= 5;
                p1 -= 7;

                if (p2 < 0)
                {

                    p1 += p2 * 4;
                    p2 = 0;
                }

                if (p1 < 0) p1 = 0;
            }
            else if (p3 == 2)
            {

                ret++;
                p3 = 0;
                p2 -= 3;
                p1 -= 6;

                if (p2 < 0)
                {

                    p1 += p2 * 4;
                    p2 = 0;
                }

                if (p1 < 0) p1 = 0;
            }
            else if (p3 == 3)
            {

                ret++;
                p3 = 0;
                p2 -= 1;
                p1 -= 5;

                if (p2 < 0)
                {

                    p1 -= 4;
                    p2 = 0;
                }

                if (p1 < 0) p1 = 0;
            }

            p1 += p2 * 4;
            p2 = 0;
            ret += p1 / 36;
            if (p1 % 36 > 0) ret++;

            Console.WriteLine(ret);
#endif
        }
    }
#if other
using System;

// CapitalLetters for class names and methods, camelCase for variable names.
// Write your code clearly enough so that it doesn't need to be commented, or at least, so that it rarely needs to be commented.

namespace Testpad
{
    public class BaekJoon2590
    {
        // 그리디하게 접근해 보자
        static void Main()
        {
            int minPanels = 0; // 필요한 최소 판 수

            int[] papers = new int[6]; // 각 색종이별 수

            // 색종이의 장수를 받음
            for (int i = 0; i < 6; i++)
            {
                papers[i] = int.Parse(Console.ReadLine());
            }

            // 남는 공간을 없애는 방향으로 진행
            // 6x6짜리 색종이는 무조건 판 1장을 소모
            minPanels += papers[5];
            papers[5] = 0;

            // 5x5짜리 색종이는 1x1짜리를 끼워넣은 후, 판 1장을 소모
            minPanels += papers[4];
            papers[0] -= papers[4] * 11; // 25 + 11 = 36
            papers[4] = 0;
            papers[0] = Math.Max(0, papers[0]);

            // 4x4짜리 색종이는 1x1, 2x2짜리를 끼워넣고, 판 1장을 소모하되, 2x2짜리를 우선함
            while (papers[3] > 0)
            {
                papers[3]--;
                minPanels++;

                if (papers[1] >= 5) // 16 + 20 = 36
                {
                    papers[1] -= 5;
                }
                else
                {
                    papers[1] = Math.Max(0, papers[1]);
                    papers[0] -= 20 - papers[1] * 4;
                    papers[1] = 0;
                }
            }

            // 3x3짜리 색종이는 좀 복잡해지지만, 큰 색종이 먼저 제거하는 방식인건 같음
            while (papers[2] > 0)
            {
                minPanels++;

                if (papers[2] >= 4) // 9 * 4 = 36
                {
                    papers[2] -= 4;
                }
                else if (papers[2] == 3) // 남는 칸수 3 * 3
                {
                    papers[2] = 0;

                    if (papers[1] >= 1)
                    {
                        papers[1]--;
                        papers[0] -= 5;
                    }
                    else
                    {
                        papers[0] -= 9;
                    }
                }
                else if (papers[2] == 2) // 남는 칸수 3 * 6
                {
                    papers[2] = 0;

                    if (papers[1] >= 3)
                    {
                        papers[1] -= 3;
                        papers[0] -= 6;
                    }
                    else
                    {
                        papers[1] = Math.Max(0, papers[1]);
                        papers[0] -= 18 - 4 * papers[1];
                        papers[1] = 0;
                    }
                }
                else // 남는 칸수 ㄴ자 형 3 * 9
                {
                    papers[2] = 0;

                    if (papers[1] >= 5)
                    {
                        papers[1] -= 5;
                        papers[0] -= 7;
                    }
                    else
                    {
                        papers[1] = Math.Max(0, papers[1]);
                        papers[0] -= 27 - 4 * papers[1];
                        papers[1] = 0;
                    }
                }
            }

            // 2x2짜리가 남으면, 1x1짜리를 끼워넣고 판 소모
            while (papers[1] > 0)
            {
                minPanels++;

                if (papers[1] >= 9) // 4 * 9 = 36
                {
                    papers[1] -= 9;
                }
                else
                {
                    papers[0] -= 36 - papers[1] * 4;
                    papers[1] = 0;
                }
            }

            // 1x1짜리가 남으면, 그만큼의 판 소모
            while (papers[0] > 0)
            {
                minPanels++;

                papers[0] -= 36;
            }

            // 결과 출력
            Console.Write(minPanels);
        }
    }
}
#elif other2
using System;

namespace _1231
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int one = int.Parse(Console.ReadLine());
            int two = int.Parse(Console.ReadLine());
            int three = int.Parse(Console.ReadLine());
            int four = int.Parse(Console.ReadLine());
            int five = int.Parse(Console.ReadLine());
            int six = int.Parse(Console.ReadLine());

            int count = six;

            while (one != 0 || two != 0 || three != 0 || four != 0 || five != 0)
            {
                while (five > 0)
                {
                    int pan = 36;
                    five--;
                    pan -= 25;
                    if (one <= pan)
                        one = 0;
                    else
                        one -= pan;
                    count++;
                }
                while (four > 0)
                {
                    int pan = 36;
                    four--;
                    pan -= 16;
                    if (two > 5)
                    {
                        two -= 5;
                        pan -= 20;
                    }
                    else
                    {
                        pan -= 4 * two;
                        two = 0;
                    }
                    if (one <= pan)
                        one = 0;
                    else
                        one -= pan;
                    count++;
                }
                while (three > 0)
                {
                    int pan = 36;
                    if (three > 4)
                    {
                        three -= 4;
                        pan = 0;
                    }
                    else
                    {
                        pan -= 9 * three;
                        three = 0;
                    }
                    if (pan == 27 && two > 5)
                    {
                        two -= 5;
                        pan -= 20;
                    }
                    else if (pan == 27 && two <= 5)
                    {
                        pan -= 4 * two;
                        two = 0;
                    }
                    if (pan == 18 && two > 3)
                    {
                        two -= 3;
                        pan -= 12;
                    }
                    else if (pan == 18 && two <= 3)
                    {
                        pan -= 4 * two;
                        two = 0;
                    }
                    if (pan == 9 && two >= 1)
                    {
                        pan -= 4 * two;
                        two = 0;
                    }
                    if (one <= pan)
                        one = 0;
                    else
                        one -= pan;
                    count++;
                }
                while (two > 0)
                {
                    int pan = 36;
                    if (two > 9)
                    {
                        two -= 9;
                        pan = 0;
                    }
                    else
                    {
                        pan -= 4 * two;
                        two = 0;
                    }
                    if (one <= pan)
                        one = 0;
                    else
                        one -= pan;
                    count++;
                }
                while (one > 0)
                {
                    if (one > 36)
                        one -= 36;
                    else
                        one = 0;
                    count++;
                }
            }
            Console.WriteLine(count);
        }
    }
}
#endif
}
