using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 15
이름 : 배성훈
내용 : 수학은 비대면강의입니다
    문제번호 : 19532번
*/

namespace BaekJoon._17
{
    internal class _17_01
    {

        static void Main1(string[] args)
        {

            int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int x = (inputs[2] * inputs[4] - inputs[1] * inputs[5]) / (inputs[0] * inputs[4] - inputs[1] * inputs[3]);
            int y = (inputs[2] * inputs[3] - inputs[0] * inputs[5]) / (inputs[1] * inputs[3] - inputs[0] * inputs[4]);

            Console.WriteLine($"{x} {y}");

        }
    }
}
