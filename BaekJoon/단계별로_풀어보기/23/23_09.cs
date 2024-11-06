using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 꼬마 정민
    문제번호 : 11382번
*/

namespace BaekJoon._23
{
    internal class _23_09
    {

        static void Main9(string[] args)
        {

            long result = Console.ReadLine().Split(' ').Select(long.Parse).ToArray().Sum();

            Console.WriteLine(result);
        }
    }
}
