using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 10
이름 : 배성훈
내용 : 돌 게임
    문제번호 : 9655번

    간단한 구현?문제다
    1, 3돌만 옮길 수 있기에 전체가 홀수개면 선턴이 홀수돌을 가져간다
    전체가 짝수인경우 선턴은 짝수돌만 가져간다 그래서 1개를 못가져가 패배한다
*/

namespace BaekJoon.etc
{
    internal class etc_0172
    {

        static void Main172(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            if (n % 2 == 0) Console.WriteLine("CY");
            else Console.WriteLine("SK");
        }
    }
}
