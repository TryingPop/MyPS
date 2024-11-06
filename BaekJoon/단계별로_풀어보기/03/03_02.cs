using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 날짜 : 22.07.14
 * 내용 : 백준 3단계 2번 문제
 * 
 * A+B - 3
 * 케이스와 두 정수 A, B를 입력받아 각 케이스마다 A + B 출력하기
 */


namespace BaekJoon._03
{
    internal class _03_02
    {
        static void Main2(string[] args)
        {
            int t = int.Parse(Console.ReadLine());
            for (int i = 0; i < t; i++)
            {
                string[] strinput = Console.ReadLine().Split(' ');
                int[] inputs = Array.ConvertAll(strinput, int.Parse);
                if (inputs.Length == 2)
                {
                    Console.WriteLine(inputs[0] + inputs[1]);
                }
                else if (inputs.Length == 0)
                {
                    break;
                }
            }

        }
    }
}