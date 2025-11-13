using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 11. 11
이름 : 배성훈
내용 : 그거 왜 말해!
    문제번호 : 33985번

    애드 혹 문제다.
    문제 조건을 보면 처음은 무조건 A, 마지막은 B여야만 한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1970
    {

        static void Main1970(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());
            string str = sr.ReadLine();
            if (str[0] == 'A' && str[n - 1] == 'B') Console.Write("Yes");
            else Console.Write("No");
        }
    }
}
