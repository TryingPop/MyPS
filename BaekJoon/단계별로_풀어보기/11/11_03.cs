using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 7
이름 : 배성훈
내용 : 네 번째 점
    문제번호 : 3009

    세 점이 주어졌을 때, 
    축에 평행한 직사각형을 만들기 위해서 필요한 네 번째 점을 찾는 프로그램을 작성하시오
*/
namespace BaekJoon._11
{
    internal class _11_03
    {

        static void Main3(string[] args)
        {

            int[] pos1, pos2, pos3;
            int x, y;

            pos1 = Array.ConvertAll(Console.ReadLine().Split(' '), input => int.Parse(input));
            pos2 = Array.ConvertAll(Console.ReadLine().Split(' '), input => int.Parse(input));
            pos3 = Array.ConvertAll(Console.ReadLine().Split(' '), input => int.Parse(input));

            if (pos1[0] == pos2[0])
            {

                x = pos3[0];
            }
            else if (pos2[0] == pos3[0])
            {

                x = pos1[0];
            }
            else
            {

                x = pos2[0];
            }

            if (pos1[1] == pos2[1])
            {

                y = pos3[1];
            }
            else if (pos2[1] == pos3[1])
            {

                y = pos1[1];
            }
            else
            {

                y = pos2[1];
            }

            Console.WriteLine($"{x} {y}");
        }
    }
}
