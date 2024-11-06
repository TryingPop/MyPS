using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 11
이름 : 배성훈
내용 : 01타일
    문제번호 : 1904번
*/

namespace BaekJoon._14
{
    internal class _14_03
    {

        static void Main3(string[] args)
        {

            int inputs = int.Parse(Console.ReadLine());

            int f1 = 1, f2 = 2;
            int result;

            if (inputs == 1)
            {

                result = f1;
            }
            else
            {

                result = f2;
            }
            for (int  i = 2; i < inputs; i++)
            {

                result = (f2 + f1) % 15746;
                f1 = f2;
                f2 = result;
            }

            Console.WriteLine(result);
        }
    }
}
