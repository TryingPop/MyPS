using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 22
이름 : 배성훈
내용 : 연도 진행바
    문제번호 : 1340번

    구현, 문자열, 파싱 문제다.
    날짜를 분단위로 계산해 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1440
    {

        static void Main1440(string[] args)
        {

            long TOTAL = 0, cur = 0;
            int year, month, day, h, m;

            Input();

            GetRet();

            void GetRet()
            {

                SetTotal();

                SetCur();

                decimal ret = cur * 100;
                ret /= TOTAL;
                Console.Write($"{ret:0.############}");
            }

            void SetCur()
            {

                int[] mTd = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                if (Chk366()) mTd[2]++;

                for (int i = 1; i <= 12; i++)
                {

                    mTd[i] += mTd[i - 1];
                }

                cur = mTd[month - 1] * 60 * 24;
                cur += (day - 1) * 60 * 24 + h * 60 + m;
            }

            void SetTotal()
            {

                int DAY = 60 * 24;
                TOTAL = DAY * 365;
                if (Chk366()) TOTAL += DAY;
            }

            bool Chk366()
            {

                if (year % 400 == 0) return true;
                else if (year % 100 == 0) return false;
                else if (year % 4 == 0) return true;
                else return false;
            }

            void Input()
            {
                
                string[] input = Console.ReadLine().Split();
                ReadMonth();

                ReadDay();

                year = int.Parse(input[2]);
                string[] temp = input[3].Split(':');
                h = int.Parse(temp[0]);
                m = int.Parse(temp[1]);

                void ReadDay()
                {

                    day = input[1][0] - '0';
                    day = day * 10 + input[1][1] - '0';
                }

                void ReadMonth()
                {

                    month = -1;
                    switch (input[0])
                    {

                        case "January":
                            month = 1;
                            return;

                        case "February":
                            month = 2;
                            return;

                        case "March":
                            month = 3;
                            return;

                        case "April":
                            month = 4;
                            return;

                        case "May":
                            month = 5;
                            return;

                        case "June":
                            month = 6;
                            return;

                        case "July":
                            month = 7;
                            return;

                        case "August":
                            month = 8;
                            return;

                        case "September":
                            month = 9;
                            return;

                        case "October":
                            month = 10;
                            return;

                        case "November":
                            month = 11;
                            return;

                        case "December":
                            month = 12;
                            return;
                    }
                }
            }
        }
    }
}
