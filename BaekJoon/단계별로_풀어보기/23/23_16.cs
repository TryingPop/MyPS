using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 문자와 문자열
    문제번호 : 27866번
*/

namespace BaekJoon._23
{
    internal class _23_16
    {

        static void Main16(string[] args)
        {

            // 입력
            string str = Console.ReadLine();
            int idx = int.Parse(Console.ReadLine());

            // 문자 출력 
            Console.WriteLine(str[idx - 1]);
        }
    }
}
