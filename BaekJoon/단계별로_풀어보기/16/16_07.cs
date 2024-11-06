using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 15
이름 : 배성훈
내용 : 알고리즘 수업 - 점근적 표기 1
    문제번호 : 24313번

    O(g(n)) = {f(n) | 모든 n >= m에 대하여 f(n) <= c * g(n)인 양의 상수 c와 m이 존재한다}

    모든 n >= m 에 대해 f(n) <= c * g(n) 명제를 묻는거였다;
    
    f(n) = inputs[0] * n + inputs[1],
    g(n) = n
    c = c
    m = m

    출제자의 예시를 보면,
    c = 8, m = 1, inputs[0] = 7, inputs[1] = 7 은 O(n)의 정의를 만족하지 않는다
    c = 8, m = 10, inputs[0] = 7, inputs[1] = 7 은 O(n)의 정의를 만족한다
*/

namespace BaekJoon._16
{
    internal class _16_07
    {

        static void Main7(string[] args)
        {

            int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int c = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine());

            if (inputs[1] <= (c - inputs[0]) * m && c >= inputs[0])
            {

                Console.WriteLine(1);
            }
            else
            {

                Console.WriteLine(0);
            }
        }
    }
}
