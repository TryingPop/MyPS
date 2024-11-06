using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 9
이름 : 배성훈
내용 : 별 관찰
    문제번호 : 2859번

    수학, 정수론, 브루트포스 문제다
    합동식으로 빠르게 풀 수 있다
    여기서는 브루트포스로 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0488
    {

        static void Main488(string[] args)
        {

            string[] day = { "Saturday", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };

            int s1 = TimeToInt();
            int s2 = TimeToInt();
            int r1 = TimeToInt();
            int r2 = TimeToInt();

            int term = r1 * r2 / GetGCD(r1, r2);

            int start = s1 < s2 ? s2 : s1;
            int end = start + term;
            int ret = -1;
            for (int i = start; i < end; i++)
            {

                if ((i - s1) % r1 != 0 || (i - s2) % r2 != 0) continue;
                ret = i;
            }

            if (ret == -1) Console.WriteLine("Never");
            else
            {

                int d = ret / (24 * 60);
                int time = ret % (24 * 60);

                d %= 7;
                Console.WriteLine(day[d]);
                Console.WriteLine($"{time / 60:D2}:{time % 60:D2}");
            }

            int GetGCD(int _a, int _b)
            {

                if (_b < _a)
                {

                    int temp = _a;
                    _a = _b;
                    _b = temp;
                }

                while(_b > 0)
                {

                    int temp = _a % _b;
                    _a = _b;
                    _b = temp;
                }

                return _a;
            }

            int TimeToInt()
            {

                string str = Console.ReadLine();
                int h = str[0] - '0';
                h = h * 10 + str[1] - '0';

                int m = str[3] - '0';
                m = m * 10 + str[4] - '0';

                return h * 60 + m;
            }
        }
    }
}
