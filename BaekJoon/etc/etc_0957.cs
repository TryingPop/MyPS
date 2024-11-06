using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 9
이름 : 배성훈
내용 : 2007년
    문제번호 : 1924번

    수학, 구현 문제다
    날짜를 숫자로 바꿔서 풀면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0957
    {

        static void Main957(string[] args)
        {

            int mm, dd;

            Solve();
            void Solve()
            {

                Input();
                int[] m = new int[] { 0, 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                for (int i = 1; i < m.Length; i++)
                {

                    m[i] += m[i - 1];
                }

                string[] day = new string[] { "SUN", "MON", "TUE", "WED", "THU", "FRI", "SAT" };

                int ret = (m[mm] + dd) % 7;

                Console.WriteLine(day[ret]);
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                mm = int.Parse(temp[0]);
                dd = int.Parse(temp[1]);
            }
        }
    }
}
