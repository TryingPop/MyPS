using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 1
이름 : 배성훈
내용 : 별 찍기 - 3
    문제번호 : 2440번

    구현 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0856
    {

        static void Main856(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());

            for (int i = n; i > 0; i--)
            {

                for (int j = 0; j < i; j++)
                {

                    sw.Write('*');
                }

                sw.Write('\n');
            }

            sr.Close();
            sw.Close();
        }
    }
}
