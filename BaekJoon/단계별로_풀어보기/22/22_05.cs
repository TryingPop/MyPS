using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성후
내용 : 최소공배수
    문제번호 : 13241번
*/

namespace BaekJoon._22
{
    internal class _22_05
    {

        static void Main2(string[] args)
        {

            long[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);
            long gcd = 1;

            {

                long a = inputs[0] > inputs[1] ? inputs[0] : inputs[1];
                long b = inputs[0] > inputs[1] ? inputs[1] : inputs[0];
                long r = 0;

                while (true)
                {

                    if (a % b == 0)
                    {

                        break;
                    }

                    r = a % b;
                    a = b;
                    b = r;
                }

                gcd = b;
            }

            long result = (inputs[0] / gcd) * inputs[1];
            Console.WriteLine(result);
        }
    }
}
