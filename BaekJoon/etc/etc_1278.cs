using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 16
이름 : 배성훈
내용 : 데이터 만들기 1
    문제번호 : 7140번

    플로이드 워셜 코드를 보면 3중 포문을 돈다.
    그리고 100을 초과하면 counter로 TLE를 반환한다.
    그래서 100이 최대이다.

    이후 0으로 가는 것을 넣고, 0 1만 간선으로 이었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1278
    {

        static void Main1278(string[] args)
        {

            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            sw.Write("101\n");
            for (int i = 0; i < 101; i++)
            {

                sw.Write("0\n");
            }

            sw.Write("1\n");
            sw.Write("0 1");
        }
    }
}
