using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 28
이름 : 배성훈
내용 : 배스킨라빈스~N~귀엽고~깜찍하게~
    문제번호 : 25179번

    시작 수를 한번에 최대 부를 수 있는 수 + 1 로 나누었을 때 
    나머지가 1인 수로 상대가 부르게하면 무조건 이긴다
*/

namespace BaekJoon.etc
{
    internal class etc_0120
    {

        static void Main120(string[] args)
        {

            long[] arr = Console.ReadLine().Split(' ').Select(long.Parse).ToArray();

            bool isWin = (arr[0] % (arr[1] + 1)) != 1;

            Console.WriteLine(isWin ? "Can win" : "Can't win");
        }
    }
}
