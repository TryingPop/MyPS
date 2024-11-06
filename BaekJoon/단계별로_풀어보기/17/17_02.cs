using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 15
이름 : 배성훈
내용 : 베라의 패션
    문제번호 : 15439번
*/
namespace BaekJoon._17
{
    internal class _17_02
    {

        static void Main2(string[] args)
        {


            int colors = int.Parse(Console.ReadLine());
            int result = colors * (colors - 1);

            Console.WriteLine(result);
        }
    }
}
