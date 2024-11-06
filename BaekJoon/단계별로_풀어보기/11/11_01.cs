using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 7
이름 : 배성훈
내용 : 직사각형의 넓이
    문제번호 : 27323

    가로 세로 길이가 주어질 때, 넓이 구하기
*/
namespace BaekJoon._11
{
    internal class _11_01
    {

        static void Main1(string[] args)
        {

            int height = int.Parse(Console.ReadLine());
            int width = int.Parse(Console.ReadLine());

            int area = width * height;

            Console.WriteLine(area);
        }
    }
}
