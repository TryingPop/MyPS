using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 단어 길이 재기
    문제번호 : 2743번
*/

namespace BaekJoon._23
{
    internal class _23_17
    {

        static void Main17(string[] args)
        {

            // Console.WriteLine(Console.ReadLine().Length);

            int c, n = 0;

            // 뒤에 13은 엔터키이다!
            // 13이 의미하는 것은 줄을 바꾸고, 맨 앞으로 가는 것을 의미한다
            // 비슷한 번호는 10으로 현재 열에서 줄 내림이다
            while (!((c = Console.Read()) == '\n' || c == -1 || c == 13))
            {

                n++;
            }

            Console.WriteLine(n);
        }
    }
}
