using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 28
이름 : 배성훈
내용 : 별 찍기 - 4
    문제번호 : 2441번
*/

namespace BaekJoon.etc
{
    internal class etc_1222
    {

        static void Main1222(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            for (int i = n; i > 0; i--)
            {

                for (int j = 0; j < n - i; j++)
                {

                    sw.Write(' ');
                }

                for (int j = 0; j < i; j++)
                {

                    sw.Write('*');
                }

                sw.Write('\n');
            }
        }
    }
}
