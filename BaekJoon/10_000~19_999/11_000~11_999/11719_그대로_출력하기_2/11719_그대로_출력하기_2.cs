using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 28
이름 : 배성훈
내용 : 그대로 출력하기 2
    문제번호 : 11719번

    구현, 문자열 문제다
    입력이 없을 때까지 하나씩 읽어 그대로 출력했다
*/

namespace BaekJoon.etc
{
    internal class etc_1005
    {

        static void Main1005(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int c;
            while ((c = sr.Read()) != -1)
            {

                sw.Write((char)c);
            }

            sr.Close();
            sw.Close();
        }
    }
}
