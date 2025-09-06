using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 26
이름 : 배성훈
내용 : 팰린드롬 척화비
    문제번호 : 20944번

    애드 혹 문제다.
    한 단어로만 이루어진 문자열은 팰린드롬이고 동시에 수미상관이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1837
    {

        static void Main1837(string[] args)
        {

            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
                sw.Write('a');
        }
    }
}
