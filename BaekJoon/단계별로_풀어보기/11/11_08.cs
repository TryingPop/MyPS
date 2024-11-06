using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 7
이름 : 배성훈
내용 : 세 막대
    문제번호 : 14215번

    세 양의 정수가 주어질 때, 세 양의 정수는 양의 정수로 낮출 수 있다

    각 막대의 길이는 양의 정수이다
    세 막대를 이용해서 넓이가 양수인 삼각형을 만들 수 있어야 한다.
    각 막대를 이용해 삼각형이 되는 둘레를 최대값을 구하시오
*/

namespace BaekJoon._11
{
    internal class _11_08
    {

        static void Main8(string[] args)
        {

            int[] inputs;
            int max, others;

            inputs = Array.ConvertAll(Console.ReadLine().Split(' '), 
                input => int.Parse(input));

            if (inputs[0] > inputs[1] && inputs[0] > inputs[2])
            {

                max = inputs[0];
                others = inputs[1] + inputs[2];
            }
            else if (inputs[1] > inputs[2])
            {

                max = inputs[1];
                others = inputs[0] + inputs[2];
            }
            else
            {

                max = inputs[2];
                others = inputs[0] + inputs[1];
            }

            while( max >= others)
            {

                max--;
            }

            Console.WriteLine(max + others);
        }
    }
}
