using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 7
이름 : 배성훈
내용 : 직사각형의 넓이
    문제번호 : 1085

    왼쪽 아래 꼭짓점은 (0, 0), 오른쪽 위 꼭짓점은 (w, h)인 직사각형에 대해서 
    직사각형 내부의 좌표 (x, y)에서 이 직사각형의 경계선까지 가는 거리의 최솟값을 구하는 프로그램을 작성하시오.
    
    첫째 줄에 "x y w h" 를 입력 받는다
*/

namespace BaekJoon._11
{
    internal class _11_02
    {

        static void Main2(string[] args)
        {

            int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), 
                input => int.Parse(input));

            int x = inputs[0];
            int y = inputs[1];
            int w = inputs[2];
            int h = inputs[3];

            int result = (new int[4] {x, y, w-x, h-y}).Min();
            Console.WriteLine(result);
        }
    }
}
