using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 17
이름 : 배성훈
내용 : Monty's Hall
    문제번호 : 25257번

    수학, 확률론 문제다.
    잘 알려진 몬티홀 문제다.
    총 d개의 문이 존재하고 s개의 문을 선택한다.
    그리고 e개의 문을 공개한다.

    여기서 d > s + e이다.
    그래서 남은 문을 r = d - s - e라 하면
    r개의 문에 정답이 존재할 확률은 (d - s) / d이다.
    
    즉, 각각의 문에는 (d - s) / (d * (d - s - e))의 확률이 된다.
    이는 초기 선택한 s개의 문에서는 1 / d 확률이 높음을 알 수 있다.
    그래서 바꿀 수 있다면 무조건 바꿔야한다.

    그래서 s개 중에서 바꿀 수 있는것을 최대한 바꾸고, 나머지를 선택한 s개 중에서 택하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1895
    {

        static void Main1895(string[] args)
        {

            long d, s, e;

            Input();

            GetRet();

            void GetRet()
            {

                long sel = s - Math.Min(d - s - e, s);
                long ja = ((d - s) * s - e * sel);
                long mo = (d - s - e) * d;

                Console.Write($"{ja / (mo * 1.0):0.########}");
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                d = int.Parse(temp[0]);
                s = int.Parse(temp[1]);
                e = int.Parse(temp[2]);
            }
        }
    }
}
