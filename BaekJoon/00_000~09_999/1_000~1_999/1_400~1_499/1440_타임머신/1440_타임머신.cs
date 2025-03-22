using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 18
이름 : 배성훈
내용 : 타임머신
    문제번호 : 1440번

    구현, 브루트포스 문제다.
    시간:분:초, 시간:초:분, ... , 초:분:시간으로 읽을 수 있는지 확인하면 된다.
    여기서 시간은 1 ~ 12까지만 올 수 있고, 분 초는 0 ~ 59까지 올 수 있다.
    시간이 되는지 확인하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1419
    {

        static void Main1419(string[] args)
        {

            int[] time = Array.ConvertAll(Console.ReadLine().Split(':'), int.Parse);

            int ret = 0;

            Chk(time[0], time[1], time[2]);
            Chk(time[1], time[2], time[0]);
            Chk(time[2], time[0], time[1]);

            Console.Write(ret);
            void Chk(int _h, int _ms1, int _ms2)
            {

                if (_h <= 12 && _h != 0)
                {

                    if (_ms1 < 60 && _ms2 < 60)
                    {

                        ret += 2;
                    }
                }
            }
        }
    }
}
