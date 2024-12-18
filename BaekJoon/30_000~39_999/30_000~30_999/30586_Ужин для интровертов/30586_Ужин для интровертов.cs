using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 18
이름 : 배성훈
내용 : Ужин для интровертов
    문제번호 : 30586번

    직접 그려서 해보면 결과 = 2 * n / (k + 2)가 성립함을 알 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1200
    {

        static void Main1200(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());

            int ret = 2 * n / (k + 2);
            Console.Write(ret);
        }
    }
}
