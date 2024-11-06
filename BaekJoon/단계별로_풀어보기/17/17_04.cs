using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 15
이름 : 배성훈
내용 : 이항계수 1
    문제번호 : 11050번
*/

namespace BaekJoon._17
{
    internal class _17_04
    {

        static void Main4(string[] args)
        {

            int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int result = 1;
            for (int i = inputs[0]; i > inputs[0] - inputs[1]; i--)
            {

                result *= i;
            }

            for (int j = 1; j <= inputs[1]; j++)
            {

                result /= j;
            }

            Console.WriteLine(result);
        }
    }
}
