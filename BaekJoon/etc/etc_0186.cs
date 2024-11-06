using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 11
이름 : 배성훈
내용 : 알 수 없는 번호
    문제번호 : 1338번

    ... 조건 체크 문제다

    다른 사람 팁을 봐서 풀었다;
    0 <= y < |x| , 여기서 |x|는 x의 절댓값 기호
    일 때 UNKNOWN을 출력해야한다 (Unknown이 아닌 Unknwon인건 또다른 함정;)
    이걸 연산 가능하게 처리해버려서 여러 번 틀렸다;
*/

namespace BaekJoon.etc
{
    internal class etc_0186
    {

        static void Main186(string[] args)
        {

            string UNKNOWN = "Unknwon Number";

            long[] range = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);
            long[] div = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            if (div[0] == 0)
            {

                Console.WriteLine(UNKNOWN);
                return;
            }

            if (range[0] > range[1])
            {

                long temp = range[0];
                range[0] = range[1];
                range[1] = temp;
            }

            if (div[0] < 0) div[0] = -div[0];

            if (div[1] < 0 || div[1] >= div[0]) 
            {

                Console.WriteLine(UNKNOWN);
                return;
            }

            long chk = range[0] % div[0];

            if (chk < 0)
            {

                chk += div[0];
            }

            long ret;
            if (chk > div[1]) ret = range[0] + div[0] - chk + div[1];
            else ret = range[0] + div[1] - chk;

            if (ret > range[1]) Console.WriteLine(UNKNOWN);
            else
            {

                if (ret + div[0] <= range[1]) Console.WriteLine(UNKNOWN);
                else Console.WriteLine(ret);
            }
        }
    }
}
