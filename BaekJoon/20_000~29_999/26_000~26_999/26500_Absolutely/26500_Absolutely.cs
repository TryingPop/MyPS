using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 20
이름 : 배성훈
내용 : Absolutely
    문제번호 : 26500번

    수학, 사칙연산 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1423
    {

        static void Main1423(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());

            for (int i = 0; i < n; i++)
            {

                string[] temp = sr.ReadLine().Split();
                double sub = double.Parse(temp[0]) - double.Parse(temp[1]);
                sub = Math.Abs(sub);

                sw.Write($"{sub:0.0}\n");
            }
        }
    }
}
