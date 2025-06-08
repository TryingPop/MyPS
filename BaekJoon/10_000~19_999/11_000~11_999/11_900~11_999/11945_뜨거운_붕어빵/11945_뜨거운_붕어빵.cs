using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 3
이름 : 배성훈
내용 : 뜨거운 붕어빵
    문제번호 : 11945번

    구현, 문자열 문제다.
    문자열을 좌우 대칭되게 출력하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1511
    {

        static void Main1511(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            string input = sr.ReadLine();
            int[] size = Array.ConvertAll(input.Split(), int.Parse);
            for (int i = 0; i < size[0]; i++)
            {

                input = sr.ReadLine();
                for (int j = input.Length - 1; j >= 0; j--) 
                {

                    sw.Write(input[j]);
                }

                sw.Write('\n');
            }
        }
    }
}
