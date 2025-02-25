using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 5
이름 : 배성훈
내용 : NFC West vs North
    문제번호 : 10170번

    구현 문제다.
    예제 출력대로 한줄씩 출력했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1316
    {

        static void Main1316(string[] args)
        {

            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            sw.Write("NFC West       W   L  T\n");
            sw.Write("-----------------------\n");
            sw.Write("Seattle        13  3  0\n");
            sw.Write("San Francisco  12  4  0\n");
            sw.Write("Arizona        10  6  0\n");
            sw.Write("St. Louis      7   9  0\n");
            sw.Write('\n');
            sw.Write("NFC North      W   L  T\n");
            sw.Write("-----------------------\n");
            sw.Write("Green Bay      8   7  1\n");
            sw.Write("Chicago        8   8  0\n");
            sw.Write("Detroit        7   9  0\n");
            sw.Write("Minnesota      5  10  1\n");
        }
    }
}
