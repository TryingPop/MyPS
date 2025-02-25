using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 26
이름 : 배성훈
내용 : D-Day
    문제번호 : 1308번

    구현 문제다.
    System 네임스페이스에 정의된 DateTime 구조체를 이용해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1296
    {

        static void Main1296(string[] args)
        {

            int curYear, curMonth, curDay;
            int nextYear, nextMonth, nextDay;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                curYear = int.Parse(temp[0]);
                curMonth = int.Parse(temp[1]);
                curDay = int.Parse(temp[2]);

                temp = Console.ReadLine().Split();
                nextYear = int.Parse(temp[0]);
                nextMonth = int.Parse(temp[1]);
                nextDay = int.Parse(temp[2]);
            }

            void GetRet()
            {

                bool flag = false;

                if (nextYear - curYear > 1_000)
                {

                    flag = true;
                }
                else if (nextYear - curYear == 1_000)
                {

                    if (nextMonth > curMonth) flag = true;
                    else if (nextMonth == curMonth && nextDay >= curDay) flag = true;
                }

                if (flag)
                {

                    Console.Write("gg");
                    return;
                }

                // 1000년 미만 보장
#if UseDateTime
                DateTime cur = new(curYear, curMonth, curDay);
                DateTime next = new(nextYear, nextMonth, nextDay);

                var diff = next - cur;
                int ret = diff.Days;

#else 

                // 먼저 year 맞추기

                int[] m = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

                int cur = GetDay(curYear, curMonth, curDay);
                int next = GetDay(nextYear, nextMonth, nextDay);

                int ret = next - cur;
                int GetDay(int _y, int _m, int _d)
                {

                    int y = _y - 1;
                    int ret = y * 365;
                    int leapYear = y / 4 - y / 100 + y / 400;
                    ret += leapYear;

                    for (int i = 1; i < _m; i++)
                    {

                        ret += m[i];
                    }
                    ret += _d;
                    if (_m > 2 && ChkLeapYear(_y)) ret++;
                    return ret;

                    bool ChkLeapYear(int _y)
                    {

                        if (_y % 4 != 0) return false;
                        else if (_y % 400 == 0) return true;
                        else if (_y % 100 == 0) return false;
                        else return true;
                    }
                }
                
#endif

                Console.Write("D-");
                Console.WriteLine(ret);
            }
        }
    }
}
