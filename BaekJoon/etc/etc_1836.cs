using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 26
이름 : 배성훈
내용 : SW 수열 구하기
    문제번호 : 28065번

    해 구성하기
    남은 것 중 가장 작은 값 다음 남은 것 중 가장 큰 값
    남은 것 중 가장 작은 값... 순서로 진행하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1836
    {
        static void Main1836(string[] args)
        {

            
            int n = int.Parse(Console.ReadLine());

            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int i = 0;
            int s = 1;
            int e = n;
            while (true)
            {

                sw.Write(s++);
                sw.Write(' ');
                if (e < s) break;

                sw.Write(e--);
                sw.Write(' ');
                if (e < s) break;
            }
        }
    }
}
