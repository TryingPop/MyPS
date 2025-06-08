using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 16
이름 : 배성훈
내용 : 열 개씩 끊어 출력하기
    문제번호 : 11721번

    구현, 문자열 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_1063
    {

        static void Main1063(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            string str = sr.ReadLine();
            sr.Close();

            StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            for (int i = 0; i < str.Length; i++)
            {

                sw.Write(str[i]);
                if (i % 10 == 9) sw.Write('\n');
            }

            sw.Close();
        }
    }
}
