using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 3
이름 : 배성훈
내용 : 원형 댄스
    문제번호 : 2117번

    수학 문제다.
    아이디어는 다음과 같다.
    결과로 6이 올 위치를 0 ~ n - 1중에 택한다.
    그러면 좌우 경우를 나눌 수 있다.
    각 경우는 버블소트 경우의 수와 같고, 
    이는 구간의 길이를 len이라하면 (len-1)C2이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1512
    {

        static void Main1512(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            int ret = Cnt(n);

            for (int i = 1, j = n - 1; i <= j; i++, j--)
            {

                int chk = Cnt(i) + Cnt(j);
                ret = Math.Min(ret, chk);
            }

            Console.Write(ret);

            int Cnt(int _val)
            {

                return _val * (_val - 1) >> 1;
            }
        }
    }
}
