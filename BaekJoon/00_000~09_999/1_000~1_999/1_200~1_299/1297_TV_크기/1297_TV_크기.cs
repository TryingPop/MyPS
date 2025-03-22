using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 21
이름 : 배성훈
내용 : TV 크기
    문제번호 : 1297번

    기하학, 피타고라스 정리 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1435
    {

        static void Main1435(string[] args)
        {

            string[] temp = Console.ReadLine().Split();
            int D = int.Parse(temp[0]);
            int W = int.Parse(temp[1]);
            int H = int.Parse(temp[2]);

            int PythaD = W * W + H * H;
            int PowD = D * D;
            double MUL = Math.Sqrt(PowD / (1.0 * PythaD));
            int w = (int)(W * MUL);
            int h = (int)(H * MUL);

            Console.Write($"{w} {h}");
        }
    }
}
